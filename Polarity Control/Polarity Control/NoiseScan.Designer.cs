namespace Polarity_Control
{
    partial class NoiseScanform
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
            this.components = new System.ComponentModel.Container();
            this.NoiseScantimer = new System.Windows.Forms.Timer(this.components);
            this.ScanRXNoisebutton = new System.Windows.Forms.Button();
            this.RXNoiseRedlabel = new System.Windows.Forms.Label();
            this.RXNoiseAverageMinlabel = new System.Windows.Forms.Label();
            this.RXNoiseAverageAnglelabel = new System.Windows.Forms.Label();
            this.RXNoiseMinAveragelLabellabel = new System.Windows.Forms.Label();
            this.RXNoiseMinAngleLabellabel = new System.Windows.Forms.Label();
            this.NoiseScanAveragecheckBox = new System.Windows.Forms.CheckBox();
            this.NoiseScanDeltacheckBox = new System.Windows.Forms.CheckBox();
            this.NoiseScanPreviouscheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // NoiseScantimer
            // 
            this.NoiseScantimer.Interval = 1000;
            this.NoiseScantimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ScanRXNoisebutton
            // 
            this.ScanRXNoisebutton.Location = new System.Drawing.Point(232, 427);
            this.ScanRXNoisebutton.Name = "ScanRXNoisebutton";
            this.ScanRXNoisebutton.Size = new System.Drawing.Size(75, 23);
            this.ScanRXNoisebutton.TabIndex = 0;
            this.ScanRXNoisebutton.Text = "Scan Noise";
            this.ScanRXNoisebutton.UseVisualStyleBackColor = true;
            this.ScanRXNoisebutton.Click += new System.EventHandler(this.ScanRXNoisebutton_Click);
            // 
            // RXNoiseRedlabel
            // 
            this.RXNoiseRedlabel.AutoSize = true;
            this.RXNoiseRedlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RXNoiseRedlabel.ForeColor = System.Drawing.Color.Red;
            this.RXNoiseRedlabel.Location = new System.Drawing.Point(142, 421);
            this.RXNoiseRedlabel.Name = "RXNoiseRedlabel";
            this.RXNoiseRedlabel.Size = new System.Drawing.Size(48, 13);
            this.RXNoiseRedlabel.TabIndex = 1;
            this.RXNoiseRedlabel.Text = "Current";
            // 
            // RXNoiseAverageMinlabel
            // 
            this.RXNoiseAverageMinlabel.AutoSize = true;
            this.RXNoiseAverageMinlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RXNoiseAverageMinlabel.Location = new System.Drawing.Point(377, 440);
            this.RXNoiseAverageMinlabel.Name = "RXNoiseAverageMinlabel";
            this.RXNoiseAverageMinlabel.Size = new System.Drawing.Size(32, 13);
            this.RXNoiseAverageMinlabel.TabIndex = 3;
            this.RXNoiseAverageMinlabel.Text = "0.00";
            // 
            // RXNoiseAverageAnglelabel
            // 
            this.RXNoiseAverageAnglelabel.AutoSize = true;
            this.RXNoiseAverageAnglelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RXNoiseAverageAnglelabel.Location = new System.Drawing.Point(461, 440);
            this.RXNoiseAverageAnglelabel.Name = "RXNoiseAverageAnglelabel";
            this.RXNoiseAverageAnglelabel.Size = new System.Drawing.Size(28, 13);
            this.RXNoiseAverageAnglelabel.TabIndex = 5;
            this.RXNoiseAverageAnglelabel.Text = "000";
            this.RXNoiseAverageAnglelabel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RXNoiseAverageAnglelabel_MouseDoubleClick);
            // 
            // RXNoiseMinAveragelLabellabel
            // 
            this.RXNoiseMinAveragelLabellabel.AutoSize = true;
            this.RXNoiseMinAveragelLabellabel.Location = new System.Drawing.Point(361, 424);
            this.RXNoiseMinAveragelLabellabel.Name = "RXNoiseMinAveragelLabellabel";
            this.RXNoiseMinAveragelLabellabel.Size = new System.Drawing.Size(67, 13);
            this.RXNoiseMinAveragelLabellabel.TabIndex = 6;
            this.RXNoiseMinAveragelLabellabel.Text = "Min Average";
            // 
            // RXNoiseMinAngleLabellabel
            // 
            this.RXNoiseMinAngleLabellabel.AutoSize = true;
            this.RXNoiseMinAngleLabellabel.Location = new System.Drawing.Point(449, 424);
            this.RXNoiseMinAngleLabellabel.Name = "RXNoiseMinAngleLabellabel";
            this.RXNoiseMinAngleLabellabel.Size = new System.Drawing.Size(61, 13);
            this.RXNoiseMinAngleLabellabel.TabIndex = 7;
            this.RXNoiseMinAngleLabellabel.Text = "Min Polarity";
            // 
            // NoiseScanAveragecheckBox
            // 
            this.NoiseScanAveragecheckBox.AutoSize = true;
            this.NoiseScanAveragecheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoiseScanAveragecheckBox.ForeColor = System.Drawing.Color.Magenta;
            this.NoiseScanAveragecheckBox.Location = new System.Drawing.Point(46, 420);
            this.NoiseScanAveragecheckBox.Name = "NoiseScanAveragecheckBox";
            this.NoiseScanAveragecheckBox.Size = new System.Drawing.Size(73, 17);
            this.NoiseScanAveragecheckBox.TabIndex = 8;
            this.NoiseScanAveragecheckBox.Text = "Average";
            this.NoiseScanAveragecheckBox.UseVisualStyleBackColor = true;
            this.NoiseScanAveragecheckBox.CheckedChanged += new System.EventHandler(this.NoiseScanAveragecheckBox_CheckedChanged);
            // 
            // NoiseScanDeltacheckBox
            // 
            this.NoiseScanDeltacheckBox.AutoSize = true;
            this.NoiseScanDeltacheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoiseScanDeltacheckBox.ForeColor = System.Drawing.Color.Blue;
            this.NoiseScanDeltacheckBox.Location = new System.Drawing.Point(46, 440);
            this.NoiseScanDeltacheckBox.Name = "NoiseScanDeltacheckBox";
            this.NoiseScanDeltacheckBox.Size = new System.Drawing.Size(56, 17);
            this.NoiseScanDeltacheckBox.TabIndex = 9;
            this.NoiseScanDeltacheckBox.Text = "Delta";
            this.NoiseScanDeltacheckBox.UseVisualStyleBackColor = true;
            this.NoiseScanDeltacheckBox.CheckedChanged += new System.EventHandler(this.NoiseScanDeltacheckBox_CheckedChanged);
            // 
            // NoiseScanPreviouscheckBox
            // 
            this.NoiseScanPreviouscheckBox.AutoSize = true;
            this.NoiseScanPreviouscheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoiseScanPreviouscheckBox.ForeColor = System.Drawing.Color.Green;
            this.NoiseScanPreviouscheckBox.Location = new System.Drawing.Point(125, 440);
            this.NoiseScanPreviouscheckBox.Name = "NoiseScanPreviouscheckBox";
            this.NoiseScanPreviouscheckBox.Size = new System.Drawing.Size(75, 17);
            this.NoiseScanPreviouscheckBox.TabIndex = 10;
            this.NoiseScanPreviouscheckBox.Text = "Previous";
            this.NoiseScanPreviouscheckBox.UseVisualStyleBackColor = true;
            this.NoiseScanPreviouscheckBox.CheckedChanged += new System.EventHandler(this.NoiseScanPreviouscheckBox_CheckedChanged);
            // 
            // NoiseScanform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 462);
            this.Controls.Add(this.NoiseScanPreviouscheckBox);
            this.Controls.Add(this.NoiseScanDeltacheckBox);
            this.Controls.Add(this.NoiseScanAveragecheckBox);
            this.Controls.Add(this.RXNoiseMinAngleLabellabel);
            this.Controls.Add(this.RXNoiseMinAveragelLabellabel);
            this.Controls.Add(this.RXNoiseAverageAnglelabel);
            this.Controls.Add(this.RXNoiseAverageMinlabel);
            this.Controls.Add(this.RXNoiseRedlabel);
            this.Controls.Add(this.ScanRXNoisebutton);
            this.MaximizeBox = false;
            this.Name = "NoiseScanform";
            this.ShowInTaskbar = false;
            this.Text = "Noise Scan";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NoiseScanform_FormClosed);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer NoiseScantimer;
        private System.Windows.Forms.Button ScanRXNoisebutton;
        private System.Windows.Forms.Label RXNoiseRedlabel;
        private System.Windows.Forms.Label RXNoiseAverageMinlabel;
        private System.Windows.Forms.Label RXNoiseAverageAnglelabel;
        private System.Windows.Forms.Label RXNoiseMinAveragelLabellabel;
        private System.Windows.Forms.Label RXNoiseMinAngleLabellabel;
        private System.Windows.Forms.CheckBox NoiseScanAveragecheckBox;
        private System.Windows.Forms.CheckBox NoiseScanDeltacheckBox;
        private System.Windows.Forms.CheckBox NoiseScanPreviouscheckBox;

    }
}