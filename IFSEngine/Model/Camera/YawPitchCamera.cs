﻿using IFSEngine.Helper;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using IFSEngine.Model;

namespace IFSEngine.Model.Camera
{

    public class YawPitchCamera : CameraBase
    {

        // Default YawPitchCamera values
        private const float Yaw = -90.0f;
        private const float Pitch = 0.0f;
        private Vector3 worldUp { get; } = new Vector3(0.0f, 1.0f, 0.0f);

        // Euler Angles
        private float yaw;
        private float pitch;


        public YawPitchCamera(float yaw = Yaw, float pitch = Pitch) : this(new Vector3(0.0f, 0.0f, 2.0f), new Vector3(0.0f, 1.0f, 0.0f), new Vector3(0.0f, 0.0f, -1.0f), yaw, pitch)
        {
        }

        // Constructor with vectors
        public YawPitchCamera(Vector3 position, Vector3 up, Vector3 front, float yaw = Yaw, float pitch = Pitch)
        {
            this.yaw = yaw;
            this.pitch = pitch;
            UpdateCamera();
        }        

        // Processes input received from a mouse input system. Expects the offset value in both the x and y direction.
        public override void ProcessMouseMovement(float xoffset, float yoffset)
        {
            xoffset *= Sensitivity;
            yoffset *= Sensitivity;

            yaw += xoffset;
            pitch += yoffset;


            if (pitch > 89.0f)
                pitch = 89.0f;
            if (pitch < -89.0f)
                pitch = -89.0f;

            // Update Front, Right and Up Vectors using the updated Euler angles
            UpdateCamera();
        }

        protected override void RefreshCameraValues()
        {
            // Calculate the new Front vector
            Vector3 front = new Vector3();
            front.X = (float)(Math.Cos(NumericExtensions.ToRadians(yaw)) * Math.Cos(NumericExtensions.ToRadians(pitch)));
            front.Y = (float)(Math.Sin(NumericExtensions.ToRadians(pitch)));
            front.Z = (float)(Math.Sin(NumericExtensions.ToRadians(yaw)) * Math.Cos(NumericExtensions.ToRadians(pitch)));
            this.forward = front.Normalized();
            // Also re-calculate the Right and Up vector
            right = Vector3.Cross(this.forward, worldUp).Normalized();// Normalize the vectors, because their length gets closer to 0 the more you look up or down which results in slower movement.
            up = Vector3.Cross(right, this.forward).Normalized();
        }

        protected override void SetViewProjMatrix()
        {
            Params.viewProjMatrix = Matrix4.LookAt(position, position + forward, worldUp) * projectionMatrix;
        }
    }
}
