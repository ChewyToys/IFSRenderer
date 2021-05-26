﻿using IFSEngine.Rendering;
using OpenTK.Windowing.Common;
using OpenTK.WinForms;
using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
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

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private bool IsInteractionEnabled => Renderer is not null && Renderer.IsRendering && Renderer.UpdateDisplayOnRender;
        private KeyboardController keyboard;
        //last mouse position
        private float lastX;
        private float lastY;
        private readonly Key[] translateKeys = 
        {
            Key.W, Key.S, Key.D, Key.A, Key.Q, Key.E, Key.C
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
                Renderer.LoadedParams.Camera.FocusDistance += e.Delta * Renderer.LoadedParams.Camera.FocusDistance * 0.001;
                Renderer.InvalidateAccumulation();
            }
        }

        private void GLControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (IsInteractionEnabled)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.None;
                    float yawDelta = e.X - lastX;
                    float pitchDelta = e.Y - lastY;
                    Renderer.LoadedParams.Camera.RotateWithSensitivity(new Vector3(yawDelta, pitchDelta, 0.0f));
                    Renderer.InvalidateAccumulation();

                    //TODO: Mouse position reset
                    //if (VisualTreeHelper.HitTest(this, Mouse.GetPosition(this)) == null)
                    //{
                    //    var pos = this.PointToScreen(new Point(Width / 2, Height / 2));
                    //    SetCursorPos((int)pos.X, (int)pos.Y);
                    //}
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
                if (translateKeys.Any(k => keyboard.IsKeyDown(k)))
                {
                    //camera speed relates to focus distance
                    float magnitude = (float)Renderer.LoadedParams.Camera.FocusDistance * 2;
                    var direction = new Vector3(
                        ((keyboard.IsKeyDown(Key.D) ? 1 : 0) - (keyboard.IsKeyDown(Key.A) ? 1 : 0)),
                        ((keyboard.IsKeyDown(Key.E) ? 1 : 0) - ((keyboard.IsKeyDown(Key.C) || keyboard.IsKeyDown(Key.Q)) ? 1 : 0)),
                        ((keyboard.IsKeyDown(Key.W) ? 1 : 0) - (keyboard.IsKeyDown(Key.S) ? 1 : 0))
                    );
                    Renderer.LoadedParams.Camera.TranslateWithSensitivity(magnitude * direction);
                    Renderer.InvalidateAccumulation();
                }

                if (rotateKeys.Any(k => keyboard.IsKeyDown(k)))
                {
                    float magnitude = 3.0f;
                    var direction = new Vector3(
                        ((keyboard.IsKeyDown(Key.L) ? 1 : 0) - (keyboard.IsKeyDown(Key.J) ? 1 : 0)),
                        ((keyboard.IsKeyDown(Key.K) ? 1 : 0) - (keyboard.IsKeyDown(Key.I) ? 1 : 0)),
                        ((keyboard.IsKeyDown(Key.U) ? 1 : 0) - (keyboard.IsKeyDown(Key.O) ? 1 : 0))
                    );
                    Renderer.LoadedParams.Camera.RotateWithSensitivity(magnitude * direction);
                    Renderer.InvalidateAccumulation();
                }
            }
        }

    }
}