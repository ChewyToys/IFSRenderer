﻿using IFSEngine.Rendering;
using OpenTK.Windowing.Common;
using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using WpfDisplay.Helper;

namespace IFSEngine.WPF.InteractiveDisplay
{
    /// <summary>
    /// Interaction logic for InteractiveDisplay.xaml
    /// </summary>
    public partial class InteractiveDisplay : WindowsFormsHost
    {
        public IGraphicsContext GraphicsContext => GLControl1.Context;
        public RendererGL Renderer { get; private set; }

        public ICommand InteractionStartedCommand
        {
            get { return (ICommand)GetValue(InteractionStartedCommandProperty); }
            set { SetValue(InteractionStartedCommandProperty, value); }
        }
        public static readonly DependencyProperty InteractionStartedCommandProperty =
            DependencyProperty.Register("InteractionStartedCommand", typeof(ICommand), typeof(InteractiveDisplay), new PropertyMetadata(null));

        public ICommand InteractionFinishedCommand
        {
            get { return (ICommand)GetValue(InteractionFinishedCommandProperty); }
            set { SetValue(InteractionFinishedCommandProperty, value); }
        }
        public static readonly DependencyProperty InteractionFinishedCommandProperty =
            DependencyProperty.Register("InteractionFinishedCommand", typeof(ICommand), typeof(InteractiveDisplay), new PropertyMetadata(null));

        private bool invertX;
        public bool InvertRotationAxisX
        {
            get { return (bool)GetValue(InvertRotationAxisXProperty); }
            set { SetValue(InvertRotationAxisXProperty, value); }
        }
        public static readonly DependencyProperty InvertRotationAxisXProperty =
            DependencyProperty.Register("InvertRotationAxisX", typeof(bool), typeof(InteractiveDisplay),
                new PropertyMetadata(false, (a,b) => { ((InteractiveDisplay)a).invertX = (bool)b.NewValue; }));

        private bool invertY;
        public bool InvertRotationAxisY
        {
            get { return (bool)GetValue(InvertRotationAxisYProperty); }
            set { SetValue(InvertRotationAxisYProperty, value); }
        }
        public static readonly DependencyProperty InvertRotationAxisYProperty =
            DependencyProperty.Register("InvertRotationAxisY", typeof(bool), typeof(InteractiveDisplay),
                new PropertyMetadata(false, (a, b) => { ((InteractiveDisplay)a).invertY = (bool)b.NewValue; }));

        private bool invertZ;
        public bool InvertRotationAxisZ
        {
            get { return (bool)GetValue(InvertRotationAxisZProperty); }
            set { SetValue(InvertRotationAxisZProperty, value); }
        }
        public static readonly DependencyProperty InvertRotationAxisZProperty =
            DependencyProperty.Register("InvertRotationAxisZ", typeof(bool), typeof(InteractiveDisplay),
                new PropertyMetadata(false, (a, b) => { ((InteractiveDisplay)a).invertZ = (bool)b.NewValue; }));



        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private bool IsInteractionEnabled => Renderer is not null && Renderer.IsRendering && Renderer.UpdateDisplayOnRender;
        private KeyboardController keyboard;
        //last mouse position
        private float lastX;
        private float lastY;
        private readonly Key[] translateKeys = 
        {
            Key.W, Key.S, Key.D, Key.A, Key.Q, Key.E
        };
        private readonly Key[] rotateKeys = 
        {
            Key.I, Key.K, Key.J, Key.L, Key.U, Key.O
        };

        public InteractiveDisplay()
        {
            InitializeComponent();
            keyboard = new KeyboardController(this);
            keyboard.KeyboardTick += KeydownHandler;
        }

        public void AttachRenderer(RendererGL renderer)
        {
            Renderer = renderer;
            Renderer.SetDisplayResolution(GLControl1.Width, GLControl1.Height);
            GLControl1.Resize += (s2, e2) => renderer.SetDisplayResolution(GLControl1.Width, GLControl1.Height);
        }

        private void GLControl1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (IsInteractionEnabled)
            {
                InteractionStartedCommand?.Execute(null);
                Renderer.LoadedParams.Camera.FocusDistance += e.Delta * Renderer.LoadedParams.Camera.FocusDistance * 0.001;
                Renderer.InvalidateHistogramBuffer();
                InteractionFinishedCommand?.Execute(null);
            }
        }

        private void GLControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (IsInteractionEnabled)
            {
                if (e.Button == MouseButtons.Left && GLControl1.Capture)
                {
                    if (Mouse.OverrideCursor == null)
                    {
                        Mouse.OverrideCursor = System.Windows.Input.Cursors.None;
                        InteractionStartedCommand?.Execute(null);//Hack
                    }

                    float yawDelta = e.X - lastX;
                    float pitchDelta = e.Y - lastY;
                    if (invertX)
                        yawDelta = -yawDelta;
                    if (invertY)
                        pitchDelta = -pitchDelta;

                    Renderer.LoadedParams.Camera.RotateWithSensitivity(new Vector3(yawDelta, pitchDelta, 0.0f));
                    Renderer.InvalidateHistogramBuffer();

                    //TODO: Mouse position reset
                    //if (VisualTreeHelper.HitTest(this, Mouse.GetPosition(this)) == null)
                    //{
                    //    var pos = this.PointToScreen(new Point(Width / 2, Height / 2));
                    //    SetCursorPos((int)pos.X, (int)pos.Y);
                    //}
                    InteractionFinishedCommand?.Execute(null);
                }
                else
                    Mouse.OverrideCursor = null;//no override
                lastX = e.X;
                lastY = e.Y;
            }
        }

        private void KeydownHandler(object sender, EventArgs e)
        {
            if (IsInteractionEnabled)
            {
                //TODO: InteractionStartedCommand?.Execute(null);
                if (translateKeys.Any(k => keyboard.IsKeyDown(k)))
                {
                    //camera speed relates to focus distance
                    float magnitude = (float)Renderer.LoadedParams.Camera.FocusDistance * 2;
                    var direction = new Vector3(
                        ((keyboard.IsKeyDown(Key.D) ? 1 : 0) - (keyboard.IsKeyDown(Key.A) ? 1 : 0)),
                        ((keyboard.IsKeyDown(Key.E) ? 1 : 0) - (keyboard.IsKeyDown(Key.Q) ? 1 : 0)),
                        ((keyboard.IsKeyDown(Key.W) ? 1 : 0) - (keyboard.IsKeyDown(Key.S) ? 1 : 0))
                    );
                    Renderer.LoadedParams.Camera.TranslateWithSensitivity(magnitude * direction);
                    Renderer.InvalidateHistogramBuffer();
                }

                if (rotateKeys.Any(k => keyboard.IsKeyDown(k)))
                {
                    float magnitude = 3.0f;
                    var direction = new Vector3(
                        ((keyboard.IsKeyDown(Key.L) ? 1 : 0) - (keyboard.IsKeyDown(Key.J) ? 1 : 0)),
                        ((keyboard.IsKeyDown(Key.K) ? 1 : 0) - (keyboard.IsKeyDown(Key.I) ? 1 : 0)),
                        ((keyboard.IsKeyDown(Key.U) ? 1 : 0) - (keyboard.IsKeyDown(Key.O) ? 1 : 0))
                    );
                    if (invertX)
                        direction.X = -direction.X;
                    if (invertY)
                        direction.Y = -direction.Y;
                    if (invertZ)
                        direction.Z = -direction.Z;

                    Renderer.LoadedParams.Camera.RotateWithSensitivity(magnitude * direction);
                    Renderer.InvalidateHistogramBuffer();
                }
            }
        }
    }
}
