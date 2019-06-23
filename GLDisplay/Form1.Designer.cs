﻿namespace GLDisplay
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericUpDownBrightness = new System.Windows.Forms.NumericUpDown();
            this.labelBrightness = new System.Windows.Forms.Label();
            this.labelGamma = new System.Windows.Forms.Label();
            this.numericUpDownGamma = new System.Windows.Forms.NumericUpDown();
            this.buttonRender = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.numericUpDownFocus = new System.Windows.Forms.NumericUpDown();
            this.buttonRandomize = new System.Windows.Forms.Button();
            this.IteratorSelectLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDownDOF = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFocus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDOF)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownBrightness
            // 
            this.numericUpDownBrightness.DecimalPlaces = 3;
            this.numericUpDownBrightness.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownBrightness.Location = new System.Drawing.Point(74, 75);
            this.numericUpDownBrightness.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownBrightness.Name = "numericUpDownBrightness";
            this.numericUpDownBrightness.Size = new System.Drawing.Size(89, 20);
            this.numericUpDownBrightness.TabIndex = 0;
            this.numericUpDownBrightness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBrightness.ValueChanged += new System.EventHandler(this.BrightnessOrGamma_ValueChanged);
            // 
            // labelBrightness
            // 
            this.labelBrightness.AutoSize = true;
            this.labelBrightness.Location = new System.Drawing.Point(12, 82);
            this.labelBrightness.Name = "labelBrightness";
            this.labelBrightness.Size = new System.Drawing.Size(56, 13);
            this.labelBrightness.TabIndex = 1;
            this.labelBrightness.Text = "Brightness";
            // 
            // labelGamma
            // 
            this.labelGamma.AutoSize = true;
            this.labelGamma.Location = new System.Drawing.Point(25, 109);
            this.labelGamma.Name = "labelGamma";
            this.labelGamma.Size = new System.Drawing.Size(43, 13);
            this.labelGamma.TabIndex = 1;
            this.labelGamma.Text = "Gamma";
            // 
            // numericUpDownGamma
            // 
            this.numericUpDownGamma.DecimalPlaces = 3;
            this.numericUpDownGamma.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownGamma.Location = new System.Drawing.Point(74, 107);
            this.numericUpDownGamma.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownGamma.Name = "numericUpDownGamma";
            this.numericUpDownGamma.Size = new System.Drawing.Size(89, 20);
            this.numericUpDownGamma.TabIndex = 0;
            this.numericUpDownGamma.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownGamma.ValueChanged += new System.EventHandler(this.BrightnessOrGamma_ValueChanged);
            // 
            // buttonRender
            // 
            this.buttonRender.Location = new System.Drawing.Point(15, 15);
            this.buttonRender.Name = "buttonRender";
            this.buttonRender.Size = new System.Drawing.Size(75, 23);
            this.buttonRender.TabIndex = 2;
            this.buttonRender.Text = "Start Render";
            this.buttonRender.UseVisualStyleBackColor = true;
            this.buttonRender.Click += new System.EventHandler(this.ButtonRender_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(96, 15);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 3;
            this.buttonStop.Text = "Stop Render";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // numericUpDownFocus
            // 
            this.numericUpDownFocus.DecimalPlaces = 3;
            this.numericUpDownFocus.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownFocus.Location = new System.Drawing.Point(12, 160);
            this.numericUpDownFocus.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownFocus.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownFocus.Name = "numericUpDownFocus";
            this.numericUpDownFocus.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownFocus.TabIndex = 5;
            this.numericUpDownFocus.Value = new decimal(new int[] {
            20,
            0,
            0,
            65536});
            this.numericUpDownFocus.ValueChanged += new System.EventHandler(this.NumericUpDownFocus_ValueChanged);
            // 
            // buttonRandomize
            // 
            this.buttonRandomize.Location = new System.Drawing.Point(15, 44);
            this.buttonRandomize.Name = "buttonRandomize";
            this.buttonRandomize.Size = new System.Drawing.Size(75, 23);
            this.buttonRandomize.TabIndex = 6;
            this.buttonRandomize.Text = "Randomize params";
            this.buttonRandomize.UseVisualStyleBackColor = true;
            this.buttonRandomize.Click += new System.EventHandler(this.ButtonRandomize_Click);
            // 
            // IteratorSelectLabel
            // 
            this.IteratorSelectLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IteratorSelectLabel.AutoSize = true;
            this.IteratorSelectLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.IteratorSelectLabel.Location = new System.Drawing.Point(577, 7);
            this.IteratorSelectLabel.Name = "IteratorSelectLabel";
            this.IteratorSelectLabel.Size = new System.Drawing.Size(211, 42);
            this.IteratorSelectLabel.TabIndex = 7;
            this.IteratorSelectLabel.Text = "< ( 1 ) / 5 >";
            this.IteratorSelectLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(12, 186);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Depth Fog";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // numericUpDownDOF
            // 
            this.numericUpDownDOF.DecimalPlaces = 3;
            this.numericUpDownDOF.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownDOF.Location = new System.Drawing.Point(12, 134);
            this.numericUpDownDOF.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDOF.Name = "numericUpDownDOF";
            this.numericUpDownDOF.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownDOF.TabIndex = 5;
            this.numericUpDownDOF.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownDOF.ValueChanged += new System.EventHandler(this.numericUpDownDOF_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "DOF Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Focus Distance";
            // 
            // saveImage
            // 
            this.saveImage.Location = new System.Drawing.Point(96, 44);
            this.saveImage.Name = "saveImage";
            this.saveImage.Size = new System.Drawing.Size(75, 23);
            this.saveImage.TabIndex = 11;
            this.saveImage.Text = "Save Image";
            this.saveImage.UseVisualStyleBackColor = true;
            this.saveImage.Click += new System.EventHandler(this.SaveImage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.saveImage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.IteratorSelectLabel);
            this.Controls.Add(this.buttonRandomize);
            this.Controls.Add(this.numericUpDownDOF);
            this.Controls.Add(this.numericUpDownFocus);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonRender);
            this.Controls.Add(this.numericUpDownGamma);
            this.Controls.Add(this.labelGamma);
            this.Controls.Add(this.numericUpDownBrightness);
            this.Controls.Add(this.labelBrightness);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFocus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDOF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelBrightness;
        private System.Windows.Forms.NumericUpDown numericUpDownBrightness;
        private System.Windows.Forms.Label labelGamma;
        private System.Windows.Forms.NumericUpDown numericUpDownGamma;
        private System.Windows.Forms.Button buttonRender;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.NumericUpDown numericUpDownFocus;
        private System.Windows.Forms.Button buttonRandomize;
        private System.Windows.Forms.Label IteratorSelectLabel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownDOF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveImage;
    }
}

