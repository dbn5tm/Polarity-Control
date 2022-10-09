namespace Polarity_Control
{
    partial class AzElform
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
            this.AzElRequestAztextBox = new System.Windows.Forms.TextBox();
            this.AzElCurrentAztextBox = new System.Windows.Forms.TextBox();
            this.AzElRequestEltextBox = new System.Windows.Forms.TextBox();
            this.AzElCurrentEltextBox = new System.Windows.Forms.TextBox();
            this.AzElRequestlabel = new System.Windows.Forms.Label();
            this.AzElCurrnetlabel = new System.Windows.Forms.Label();
            this.AzElAzlabel = new System.Windows.Forms.Label();
            this.AzElEllabel = new System.Windows.Forms.Label();
            this.AzElDrivecheckBox = new System.Windows.Forms.CheckBox();
            this.AzElMoonradioButton = new System.Windows.Forms.RadioButton();
            this.AzElSunradioButton = new System.Windows.Forms.RadioButton();
            this.AzElSourceradioButton = new System.Windows.Forms.RadioButton();
            this.AzElgroupBox = new System.Windows.Forms.GroupBox();
            this.chkManual = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnManualGo = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.AzElgroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AzElRequestAztextBox
            // 
            this.AzElRequestAztextBox.Location = new System.Drawing.Point(48, 33);
            this.AzElRequestAztextBox.Name = "AzElRequestAztextBox";
            this.AzElRequestAztextBox.Size = new System.Drawing.Size(47, 20);
            this.AzElRequestAztextBox.TabIndex = 0;
            this.AzElRequestAztextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AzElCurrentAztextBox
            // 
            this.AzElCurrentAztextBox.Location = new System.Drawing.Point(119, 33);
            this.AzElCurrentAztextBox.Name = "AzElCurrentAztextBox";
            this.AzElCurrentAztextBox.Size = new System.Drawing.Size(47, 20);
            this.AzElCurrentAztextBox.TabIndex = 1;
            this.AzElCurrentAztextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AzElRequestEltextBox
            // 
            this.AzElRequestEltextBox.Location = new System.Drawing.Point(48, 59);
            this.AzElRequestEltextBox.Name = "AzElRequestEltextBox";
            this.AzElRequestEltextBox.Size = new System.Drawing.Size(47, 20);
            this.AzElRequestEltextBox.TabIndex = 2;
            this.AzElRequestEltextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AzElCurrentEltextBox
            // 
            this.AzElCurrentEltextBox.Location = new System.Drawing.Point(119, 59);
            this.AzElCurrentEltextBox.Name = "AzElCurrentEltextBox";
            this.AzElCurrentEltextBox.Size = new System.Drawing.Size(47, 20);
            this.AzElCurrentEltextBox.TabIndex = 3;
            this.AzElCurrentEltextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AzElRequestlabel
            // 
            this.AzElRequestlabel.AutoSize = true;
            this.AzElRequestlabel.Location = new System.Drawing.Point(47, 13);
            this.AzElRequestlabel.Name = "AzElRequestlabel";
            this.AzElRequestlabel.Size = new System.Drawing.Size(47, 13);
            this.AzElRequestlabel.TabIndex = 4;
            this.AzElRequestlabel.Text = "Request";
            // 
            // AzElCurrnetlabel
            // 
            this.AzElCurrnetlabel.AutoSize = true;
            this.AzElCurrnetlabel.Location = new System.Drawing.Point(121, 13);
            this.AzElCurrnetlabel.Name = "AzElCurrnetlabel";
            this.AzElCurrnetlabel.Size = new System.Drawing.Size(41, 13);
            this.AzElCurrnetlabel.TabIndex = 5;
            this.AzElCurrnetlabel.Text = "Current";
            // 
            // AzElAzlabel
            // 
            this.AzElAzlabel.AutoSize = true;
            this.AzElAzlabel.Location = new System.Drawing.Point(13, 39);
            this.AzElAzlabel.Name = "AzElAzlabel";
            this.AzElAzlabel.Size = new System.Drawing.Size(19, 13);
            this.AzElAzlabel.TabIndex = 6;
            this.AzElAzlabel.Text = "Az";
            // 
            // AzElEllabel
            // 
            this.AzElEllabel.AutoSize = true;
            this.AzElEllabel.Location = new System.Drawing.Point(13, 62);
            this.AzElEllabel.Name = "AzElEllabel";
            this.AzElEllabel.Size = new System.Drawing.Size(16, 13);
            this.AzElEllabel.TabIndex = 7;
            this.AzElEllabel.Text = "El";
            // 
            // AzElDrivecheckBox
            // 
            this.AzElDrivecheckBox.AutoSize = true;
            this.AzElDrivecheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.AzElDrivecheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AzElDrivecheckBox.Location = new System.Drawing.Point(98, 115);
            this.AzElDrivecheckBox.Name = "AzElDrivecheckBox";
            this.AzElDrivecheckBox.Size = new System.Drawing.Size(130, 28);
            this.AzElDrivecheckBox.TabIndex = 32;
            this.AzElDrivecheckBox.Text = "Auto Track";
            this.toolTip1.SetToolTip(this.AzElDrivecheckBox, "Select to Drive rotor  with WSJT Settings");
            this.AzElDrivecheckBox.UseVisualStyleBackColor = false;
            this.AzElDrivecheckBox.CheckedChanged += new System.EventHandler(this.AzElDrivecheckBox_CheckedChanged);
            // 
            // AzElMoonradioButton
            // 
            this.AzElMoonradioButton.AutoSize = true;
            this.AzElMoonradioButton.Checked = true;
            this.AzElMoonradioButton.Location = new System.Drawing.Point(0, 8);
            this.AzElMoonradioButton.Name = "AzElMoonradioButton";
            this.AzElMoonradioButton.Size = new System.Drawing.Size(52, 17);
            this.AzElMoonradioButton.TabIndex = 33;
            this.AzElMoonradioButton.TabStop = true;
            this.AzElMoonradioButton.Text = "Moon";
            this.AzElMoonradioButton.UseVisualStyleBackColor = true;
            // 
            // AzElSunradioButton
            // 
            this.AzElSunradioButton.AutoSize = true;
            this.AzElSunradioButton.Location = new System.Drawing.Point(0, 24);
            this.AzElSunradioButton.Name = "AzElSunradioButton";
            this.AzElSunradioButton.Size = new System.Drawing.Size(44, 17);
            this.AzElSunradioButton.TabIndex = 34;
            this.AzElSunradioButton.Text = "Sun";
            this.AzElSunradioButton.UseVisualStyleBackColor = true;
            // 
            // AzElSourceradioButton
            // 
            this.AzElSourceradioButton.AutoSize = true;
            this.AzElSourceradioButton.Location = new System.Drawing.Point(0, 39);
            this.AzElSourceradioButton.Name = "AzElSourceradioButton";
            this.AzElSourceradioButton.Size = new System.Drawing.Size(59, 17);
            this.AzElSourceradioButton.TabIndex = 35;
            this.AzElSourceradioButton.Text = "Source";
            this.AzElSourceradioButton.UseVisualStyleBackColor = true;
            // 
            // AzElgroupBox
            // 
            this.AzElgroupBox.Controls.Add(this.AzElSourceradioButton);
            this.AzElgroupBox.Controls.Add(this.AzElMoonradioButton);
            this.AzElgroupBox.Controls.Add(this.AzElSunradioButton);
            this.AzElgroupBox.Location = new System.Drawing.Point(16, 79);
            this.AzElgroupBox.Name = "AzElgroupBox";
            this.AzElgroupBox.Size = new System.Drawing.Size(62, 59);
            this.AzElgroupBox.TabIndex = 36;
            this.AzElgroupBox.TabStop = false;
            // 
            // chkManual
            // 
            this.chkManual.AutoSize = true;
            this.chkManual.BackColor = System.Drawing.SystemColors.Control;
            this.chkManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkManual.Location = new System.Drawing.Point(98, 86);
            this.chkManual.Name = "chkManual";
            this.chkManual.Size = new System.Drawing.Size(97, 28);
            this.chkManual.TabIndex = 37;
            this.chkManual.Text = "Manual";
            this.toolTip1.SetToolTip(this.chkManual, "Select to Manually set rotor Az/El.");
            this.chkManual.UseVisualStyleBackColor = false;
            this.chkManual.CheckedChanged += new System.EventHandler(this.chkManual_CheckedChanged);
            // 
            // btnManualGo
            // 
            this.btnManualGo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnManualGo.BackColor = System.Drawing.Color.Yellow;
            this.btnManualGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManualGo.Location = new System.Drawing.Point(199, 87);
            this.btnManualGo.Name = "btnManualGo";
            this.btnManualGo.Size = new System.Drawing.Size(34, 23);
            this.btnManualGo.TabIndex = 38;
            this.btnManualGo.Text = "GO";
            this.toolTip1.SetToolTip(this.btnManualGo, "Press to send manual rotor settings.");
            this.btnManualGo.UseVisualStyleBackColor = false;
            this.btnManualGo.Click += new System.EventHandler(this.btnManualGo_Click);
            // 
            // btnStop
            // 
            this.btnStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStop.BackColor = System.Drawing.Color.Orange;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(183, 42);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(50, 23);
            this.btnStop.TabIndex = 39;
            this.btnStop.Text = "STOP";
            this.toolTip1.SetToolTip(this.btnStop, "Press to stop rotor movement.");
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // AzElform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 145);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnManualGo);
            this.Controls.Add(this.chkManual);
            this.Controls.Add(this.AzElgroupBox);
            this.Controls.Add(this.AzElDrivecheckBox);
            this.Controls.Add(this.AzElEllabel);
            this.Controls.Add(this.AzElAzlabel);
            this.Controls.Add(this.AzElCurrnetlabel);
            this.Controls.Add(this.AzElRequestlabel);
            this.Controls.Add(this.AzElCurrentEltextBox);
            this.Controls.Add(this.AzElRequestEltextBox);
            this.Controls.Add(this.AzElCurrentAztextBox);
            this.Controls.Add(this.AzElRequestAztextBox);
            this.Location = new System.Drawing.Point(465, 660);
            this.Name = "AzElform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AzEl";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AzElform_FormClosed);
            this.Load += new System.EventHandler(this.AzElform_Load);
            this.AzElgroupBox.ResumeLayout(false);
            this.AzElgroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AzElRequestlabel;
        private System.Windows.Forms.Label AzElCurrnetlabel;
        private System.Windows.Forms.Label AzElAzlabel;
        private System.Windows.Forms.Label AzElEllabel;
        public System.Windows.Forms.CheckBox AzElDrivecheckBox;
        public System.Windows.Forms.RadioButton AzElMoonradioButton;
        public System.Windows.Forms.RadioButton AzElSunradioButton;
        public System.Windows.Forms.RadioButton AzElSourceradioButton;
        private System.Windows.Forms.GroupBox AzElgroupBox;
        public System.Windows.Forms.TextBox AzElRequestAztextBox;
        public System.Windows.Forms.TextBox AzElCurrentAztextBox;
        public System.Windows.Forms.TextBox AzElRequestEltextBox;
        public System.Windows.Forms.TextBox AzElCurrentEltextBox;
        public System.Windows.Forms.CheckBox chkManual;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnManualGo;
        private System.Windows.Forms.Button btnStop;
    }
}