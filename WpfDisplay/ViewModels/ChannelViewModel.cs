﻿#nullable enable
using Cavern.Channels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IFSEngine.Animation;
using IFSEngine.Animation.ChannelDrivers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WpfDisplay.Helper;

namespace WpfDisplay.ViewModels;

public partial class ChannelViewModel : ObservableObject
{
    public string Name => channel.Name;
    public string Path { get; }

    public AnimationViewModel AnimationVM => _vm;

    private readonly AnimationViewModel _vm;
    public readonly Channel channel;
    [ObservableProperty] private ObservableCollection<KeyframeViewModel> _keyframes = new();
    [ObservableProperty] private bool _isEditing = false;
    private bool _hasDetails = false;
    public bool HasDetails
    { 
        get => _hasDetails;
        set
        {
            if (!value)//Remove audio channel
            {
                channel.AudioChannelDriver = null;
            }
            else
            {
                channel.AudioChannelDriver ??= new AudioChannelDriver();
                channel.AudioChannelDriver.AudioChannelId = (int)SelectedAudioChannelOption;
                channel.AudioChannelDriver.SetSamplerFunction(Sampler);
                EffectSlider.Value = EffectSlider.Value;//ugh
                MinFreqSlider.Value = MinFreqSlider.Value;//ugh
                MaxFreqSlider.Value = MaxFreqSlider.Value;//ugh
            }
            SetProperty(ref _hasDetails, value);
        }
    }
    [ObservableProperty] private ReferenceChannel _selectedAudioChannelOption = default!;

    private float Sampler(AudioChannelDriver d, double t)
    {
        if (_vm.LoadedAudioClip is null)
            return 0.0f;
        return CavernHelper.CavernSampler(_vm.LoadedAudioClip, _vm.AudioClipCache!, d.AudioChannelId, d.MinFrequency, d.MaxFrequency, t);
    }

    private ValueSliderViewModel? _effectSlider;
    public ValueSliderViewModel EffectSlider => _effectSlider ??= new ValueSliderViewModel(_vm.Workspace)
    {
        Label = "Effect strength",
        ToolTip = "Effect strength",
        DefaultValue = 1,
        GetV = () => channel.AudioChannelDriver?.EffectMultiplier ?? 1,
        SetV = (value) =>
        {
            if (channel.AudioChannelDriver is not null)
                channel.AudioChannelDriver.EffectMultiplier = value;
            _vm.Workspace.Renderer.InvalidateParamsBuffer();
        },
        Increment = 0.01,
        ValueWillChange = _vm.Workspace.TakeSnapshot
    };

    private ValueSliderViewModel? _minFreqSlider;
    public ValueSliderViewModel MinFreqSlider => _minFreqSlider ??= new ValueSliderViewModel(_vm.Workspace)
    {
        Label = "Min. Frequency",
        ToolTip = "Minimum Frequency",
        DefaultValue = 0,
        GetV = () => channel.AudioChannelDriver?.MinFrequency ?? 0,
        SetV = (value) =>
        {
            if(channel.AudioChannelDriver is not null)
                channel.AudioChannelDriver.MinFrequency = (int)value;
            _vm.Workspace.Renderer.InvalidateParamsBuffer();
        },
        Increment = 1,
        MinValue = 0,
        ValueWillChange = _vm.Workspace.TakeSnapshot
    };

    private ValueSliderViewModel? _maxFreqSlider;
    public ValueSliderViewModel MaxFreqSlider => _maxFreqSlider ??= new ValueSliderViewModel(_vm.Workspace)
    {
        Label = "Max. Frequency",
        ToolTip = "Maximum Frequency",
        DefaultValue = 20000,
        GetV = () => channel.AudioChannelDriver?.MaxFrequency ?? 0,
        SetV = (value) =>
        {
            if (channel.AudioChannelDriver is not null)
                channel.AudioChannelDriver.MaxFrequency = (int)value;
            _vm.Workspace.Renderer.InvalidateParamsBuffer();
        },
        Increment = 1,
        MaxValue = 20000,
        ValueWillChange = _vm.Workspace.TakeSnapshot
    };

    public ChannelViewModel(AnimationViewModel vm, string path, Channel c)
    {
        _vm = vm;
        Path = path;
        channel = c;

        SelectedAudioChannelOption = (ReferenceChannel)(c.AudioChannelDriver?.AudioChannelId ?? 0);
        HasDetails = c.AudioChannelDriver is not null;

        UpdateKeyframes();
    }

    public void RemoveKeyframe(KeyframeViewModel kvm)
    {
        Keyframes.Remove(kvm);
        channel.Keyframes.Remove(kvm._k);
    }

    public void UpdateKeyframes()
    {
        var removed = Keyframes.Where(k => !channel.Keyframes.Contains(k._k));
        foreach (var kvm in removed)
            Keyframes.Remove(kvm);
        var newKeyframes = channel.Keyframes.Where(k => !Keyframes.Any(kvm => kvm._k == k));
        foreach (var k in newKeyframes)
            Keyframes.Add(new KeyframeViewModel(_vm, this, k, false));
    }

    [RelayCommand]
    public void EditChannel()
    {
        IsEditing = !IsEditing;
    }

    [RelayCommand]
    public void InsertKeyframe()
    {
        if(!_vm.KeyframeInsertPosition.HasValue) throw new InvalidOperationException();
        var keyframePosition = TimeOnly.FromTimeSpan(TimeSpan.FromSeconds(_vm.KeyframeInsertPosition.Value));
        var eval = channel.EvaluateAt(_vm.KeyframeInsertPosition.Value);
        AnimationVM.AddOrUpdateChannel(Name, Path, eval, keyframePosition);
        _vm.KeyframeInsertPosition = null;
    }

}
