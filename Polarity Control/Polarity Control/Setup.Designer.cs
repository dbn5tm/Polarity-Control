namespace Polarity_Control
{
    partial class SetupForm
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
            this.SerialPortComboBox = new System.Windows.Forms.ComboBox();
            this.BaudRatecomboBox = new System.Windows.Forms.ComboBox();
            this.BaudRatelabel = new System.Windows.Forms.Label();
            this.Portnamelabel = new System.Windows.Forms.Label();
            this.SerialPortSetupgroupBox = new System.Windows.Forms.GroupBox();
            this.RotorCompensationgroupBox = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.AzOffsetUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.ElOffsetUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.MaxAzUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.MinAzUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.MaxElUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.MinElUpDown = new System.Windows.Forms.NumericUpDown();
            this.OptimizeStartlabel = new System.Windows.Forms.Label();
            this.OptimizeStartUpDown = new System.Windows.Forms.NumericUpDown();
            this.ThirtySecondStartlabel = new System.Windows.Forms.Label();
            this.SixtySecondStartlabel = new System.Windows.Forms.Label();
            this.SixtySecondUpDown = new System.Windows.Forms.NumericUpDown();
            this.ThirtySecondUpDown = new System.Windows.Forms.NumericUpDown();
            this.RotorIncrementlabel = new System.Windows.Forms.Label();
            this.RotorIncrementUpDown = new System.Windows.Forms.NumericUpDown();
            this.SetupDonebutton = new System.Windows.Forms.Button();
            this.AzELFileDirectorytextBox = new System.Windows.Forms.TextBox();
            this.AzElDirectorylabel = new System.Windows.Forms.Label();
            this.StepUpDown = new System.Windows.Forms.NumericUpDown();
            this.Steplabel = new System.Windows.Forms.Label();
            this.AZElSerialPortgroupBox = new System.Windows.Forms.GroupBox();
            this.AzElRotorTypecomboBox = new System.Windows.Forms.ComboBox();
            this.AzElRotorSerialBaudcomboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AzElRotorSerialPortcomboBox = new System.Windows.Forms.ComboBox();
            this.ElSerialPortgroupBox = new System.Windows.Forms.GroupBox();
            this.ElRotorTypecomboBox = new System.Windows.Forms.ComboBox();
            this.ElRotorSerialBaudcomboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ElRotorSerialPortcomboBox = new System.Windows.Forms.ComboBox();
            this.AzElSeperatecheckBox = new System.Windows.Forms.CheckBox();
            this.SerialPortSetupgroupBox.SuspendLayout();
            this.RotorCompensationgroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AzOffsetUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElOffsetUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAzUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinAzUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxElUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinElUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptimizeStartUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SixtySecondUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThirtySecondUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotorIncrementUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StepUpDown)).BeginInit();
            this.AZElSerialPortgroupBox.SuspendLayout();
            this.ElSerialPortgroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SerialPortComboBox
            // 
            this.SerialPortComboBox.FormattingEnabled = true;
            this.SerialPortComboBox.Location = new System.Drawing.Point(6, 15);
            this.SerialPortComboBox.Name = "SerialPortComboBox";
            this.SerialPortComboBox.Size = new System.Drawing.Size(89, 21);
            this.SerialPortComboBox.Sorted = true;
            this.SerialPortComboBox.TabIndex = 0;
            this.SerialPortComboBox.DropDown += new System.EventHandler(this.SerialPortcomboBox_DropDown);
            this.SerialPortComboBox.SelectedIndexChanged += new System.EventHandler(this.SerialPortcomboBox_SelectedIndexChanged);
            // 
            // BaudRatecomboBox
            // 
            this.BaudRatecomboBox.FormattingEnabled = true;
            this.BaudRatecomboBox.Items.AddRange(new object[] {
            "1200 N-8-1",
            "2400 N-8-1",
            "4800 N-8-1",
            "9600 N-8-1",
            "19200 N-8-1",
            "38400 N-8-1",
            ""});
            this.BaudRatecomboBox.Location = new System.Drawing.Point(6, 46);
            this.BaudRatecomboBox.Name = "BaudRatecomboBox";
            this.BaudRatecomboBox.Size = new System.Drawing.Size(89, 21);
            this.BaudRatecomboBox.TabIndex = 1;
            this.BaudRatecomboBox.Text = "9600 N-8-1";
            this.BaudRatecomboBox.SelectedIndexChanged += new System.EventHandler(this.BaudcomboBox_SelectedIndexChanged);
            // 
            // BaudRatelabel
            // 
            this.BaudRatelabel.AutoSize = true;
            this.BaudRatelabel.Location = new System.Drawing.Point(101, 51);
            this.BaudRatelabel.Name = "BaudRatelabel";
            this.BaudRatelabel.Size = new System.Drawing.Size(58, 13);
            this.BaudRatelabel.TabIndex = 2;
            this.BaudRatelabel.Text = "Baud Rate";
            // 
            // Portnamelabel
            // 
            this.Portnamelabel.AutoSize = true;
            this.Portnamelabel.Location = new System.Drawing.Point(101, 24);
            this.Portnamelabel.Name = "Portnamelabel";
            this.Portnamelabel.Size = new System.Drawing.Size(57, 13);
            this.Portnamelabel.TabIndex = 3;
            this.Portnamelabel.Text = "Port Name";
            // 
            // SerialPortSetupgroupBox
            // 
            this.SerialPortSetupgroupBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.SerialPortSetupgroupBox.Controls.Add(this.BaudRatecomboBox);
            this.SerialPortSetupgroupBox.Controls.Add(this.BaudRatelabel);
            this.SerialPortSetupgroupBox.Controls.Add(this.Portnamelabel);
            this.SerialPortSetupgroupBox.Controls.Add(this.SerialPortComboBox);
            this.SerialPortSetupgroupBox.Location = new System.Drawing.Point(13, 12);
            this.SerialPortSetupgroupBox.Name = "SerialPortSetupgroupBox";
            this.SerialPortSetupgroupBox.Size = new System.Drawing.Size(161, 75);
            this.SerialPortSetupgroupBox.TabIndex = 4;
            this.SerialPortSetupgroupBox.TabStop = false;
            this.SerialPortSetupgroupBox.Text = "Polarity Serial Port";
            // 
            // RotorCompensationgroupBox
            // 
            this.RotorCompensationgroupBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.RotorCompensationgroupBox.Controls.Add(this.label10);
            this.RotorCompensationgroupBox.Controls.Add(this.AzOffsetUpDown);
            this.RotorCompensationgroupBox.Controls.Add(this.label9);
            this.RotorCompensationgroupBox.Controls.Add(this.ElOffsetUpDown);
            this.RotorCompensationgroupBox.Controls.Add(this.label7);
            this.RotorCompensationgroupBox.Controls.Add(this.MaxAzUpDown);
            this.RotorCompensationgroupBox.Controls.Add(this.label8);
            this.RotorCompensationgroupBox.Controls.Add(this.MinAzUpDown);
            this.RotorCompensationgroupBox.Controls.Add(this.label1);
            this.RotorCompensationgroupBox.Controls.Add(this.MaxElUpDown);
            this.RotorCompensationgroupBox.Controls.Add(this.label6);
            this.RotorCompensationgroupBox.Controls.Add(this.MinElUpDown);
            this.RotorCompensationgroupBox.Controls.Add(this.OptimizeStartlabel);
            this.RotorCompensationgroupBox.Controls.Add(this.OptimizeStartUpDown);
            this.RotorCompensationgroupBox.Controls.Add(this.ThirtySecondStartlabel);
            this.RotorCompensationgroupBox.Controls.Add(this.SixtySecondStartlabel);
            this.RotorCompensationgroupBox.Controls.Add(this.SixtySecondUpDown);
            this.RotorCompensationgroupBox.Controls.Add(this.ThirtySecondUpDown);
            this.RotorCompensationgroupBox.Controls.Add(this.RotorIncrementlabel);
            this.RotorCompensationgroupBox.Controls.Add(this.RotorIncrementUpDown);
            this.RotorCompensationgroupBox.Location = new System.Drawing.Point(183, 12);
            this.RotorCompensationgroupBox.Name = "RotorCompensationgroupBox";
            this.RotorCompensationgroupBox.Size = new System.Drawing.Size(260, 224);
            this.RotorCompensationgroupBox.TabIndex = 5;
            this.RotorCompensationgroupBox.TabStop = false;
            this.RotorCompensationgroupBox.Text = "Rotor Setup";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(208, 188);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "Az Offset";
            // 
            // AzOffsetUpDown
            // 
            this.AzOffsetUpDown.Location = new System.Drawing.Point(150, 184);
            this.AzOffsetUpDown.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.AzOffsetUpDown.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.AzOffsetUpDown.Name = "AzOffsetUpDown";
            this.AzOffsetUpDown.Size = new System.Drawing.Size(56, 20);
            this.AzOffsetUpDown.TabIndex = 39;
            this.AzOffsetUpDown.ValueChanged += new System.EventHandler(this.AzOffsetUpDown_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(208, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "El Offset";
            // 
            // ElOffsetUpDown
            // 
            this.ElOffsetUpDown.Location = new System.Drawing.Point(150, 134);
            this.ElOffsetUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.ElOffsetUpDown.Name = "ElOffsetUpDown";
            this.ElOffsetUpDown.Size = new System.Drawing.Size(56, 20);
            this.ElOffsetUpDown.TabIndex = 37;
            this.ElOffsetUpDown.ValueChanged += new System.EventHandler(this.ElOffsetUpDown_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(75, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "Max Azimuth";
            // 
            // MaxAzUpDown
            // 
            this.MaxAzUpDown.Location = new System.Drawing.Point(10, 196);
            this.MaxAzUpDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.MaxAzUpDown.Name = "MaxAzUpDown";
            this.MaxAzUpDown.Size = new System.Drawing.Size(56, 20);
            this.MaxAzUpDown.TabIndex = 35;
            this.MaxAzUpDown.Value = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.MaxAzUpDown.ValueChanged += new System.EventHandler(this.MaxAzUpDown_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(75, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Min Azimuth";
            // 
            // MinAzUpDown
            // 
            this.MinAzUpDown.Location = new System.Drawing.Point(10, 171);
            this.MinAzUpDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.MinAzUpDown.Name = "MinAzUpDown";
            this.MinAzUpDown.Size = new System.Drawing.Size(56, 20);
            this.MinAzUpDown.TabIndex = 33;
            this.MinAzUpDown.ValueChanged += new System.EventHandler(this.MinAzUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Max Elevation";
            // 
            // MaxElUpDown
            // 
            this.MaxElUpDown.Location = new System.Drawing.Point(10, 145);
            this.MaxElUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.MaxElUpDown.Name = "MaxElUpDown";
            this.MaxElUpDown.Size = new System.Drawing.Size(56, 20);
            this.MaxElUpDown.TabIndex = 31;
            this.MaxElUpDown.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.MaxElUpDown.ValueChanged += new System.EventHandler(this.MaxElUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Min Elevation";
            // 
            // MinElUpDown
            // 
            this.MinElUpDown.Location = new System.Drawing.Point(10, 120);
            this.MinElUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.MinElUpDown.Name = "MinElUpDown";
            this.MinElUpDown.Size = new System.Drawing.Size(56, 20);
            this.MinElUpDown.TabIndex = 29;
            this.MinElUpDown.ValueChanged += new System.EventHandler(this.MinElUpDown_ValueChanged);
            // 
            // OptimizeStartlabel
            // 
            this.OptimizeStartlabel.AutoSize = true;
            this.OptimizeStartlabel.Location = new System.Drawing.Point(75, 99);
            this.OptimizeStartlabel.Name = "OptimizeStartlabel";
            this.OptimizeStartlabel.Size = new System.Drawing.Size(125, 13);
            this.OptimizeStartlabel.TabIndex = 26;
            this.OptimizeStartlabel.Text = "RX Optimize Range Start";
            // 
            // OptimizeStartUpDown
            // 
            this.OptimizeStartUpDown.Location = new System.Drawing.Point(10, 95);
            this.OptimizeStartUpDown.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.OptimizeStartUpDown.Name = "OptimizeStartUpDown";
            this.OptimizeStartUpDown.Size = new System.Drawing.Size(56, 20);
            this.OptimizeStartUpDown.TabIndex = 25;
            this.OptimizeStartUpDown.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.OptimizeStartUpDown.ValueChanged += new System.EventHandler(this.RXOptimizeStartUpDown_ValueChanged);
            // 
            // ThirtySecondStartlabel
            // 
            this.ThirtySecondStartlabel.AutoSize = true;
            this.ThirtySecondStartlabel.Location = new System.Drawing.Point(75, 73);
            this.ThirtySecondStartlabel.Name = "ThirtySecondStartlabel";
            this.ThirtySecondStartlabel.Size = new System.Drawing.Size(51, 13);
            this.ThirtySecondStartlabel.TabIndex = 24;
            this.ThirtySecondStartlabel.Text = "30S Start";
            // 
            // SixtySecondStartlabel
            // 
            this.SixtySecondStartlabel.AutoSize = true;
            this.SixtySecondStartlabel.Location = new System.Drawing.Point(75, 48);
            this.SixtySecondStartlabel.Name = "SixtySecondStartlabel";
            this.SixtySecondStartlabel.Size = new System.Drawing.Size(51, 13);
            this.SixtySecondStartlabel.TabIndex = 23;
            this.SixtySecondStartlabel.Text = "60S Start";
            // 
            // SixtySecondUpDown
            // 
            this.SixtySecondUpDown.Location = new System.Drawing.Point(10, 45);
            this.SixtySecondUpDown.Maximum = new decimal(new int[] {
            52,
            0,
            0,
            0});
            this.SixtySecondUpDown.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.SixtySecondUpDown.Name = "SixtySecondUpDown";
            this.SixtySecondUpDown.Size = new System.Drawing.Size(56, 20);
            this.SixtySecondUpDown.TabIndex = 22;
            this.SixtySecondUpDown.Value = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.SixtySecondUpDown.ValueChanged += new System.EventHandler(this.SixtySecondUpDown_ValueChanged);
            // 
            // ThirtySecondUpDown
            // 
            this.ThirtySecondUpDown.Location = new System.Drawing.Point(10, 70);
            this.ThirtySecondUpDown.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.ThirtySecondUpDown.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.ThirtySecondUpDown.Name = "ThirtySecondUpDown";
            this.ThirtySecondUpDown.Size = new System.Drawing.Size(56, 20);
            this.ThirtySecondUpDown.TabIndex = 21;
            this.ThirtySecondUpDown.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.ThirtySecondUpDown.ValueChanged += new System.EventHandler(this.ThirtySecondUpDown_ValueChanged);
            // 
            // RotorIncrementlabel
            // 
            this.RotorIncrementlabel.AutoSize = true;
            this.RotorIncrementlabel.Location = new System.Drawing.Point(75, 23);
            this.RotorIncrementlabel.Name = "RotorIncrementlabel";
            this.RotorIncrementlabel.Size = new System.Drawing.Size(83, 13);
            this.RotorIncrementlabel.TabIndex = 20;
            this.RotorIncrementlabel.Text = "Rotor Increment";
            // 
            // RotorIncrementUpDown
            // 
            this.RotorIncrementUpDown.Location = new System.Drawing.Point(10, 20);
            this.RotorIncrementUpDown.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.RotorIncrementUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.RotorIncrementUpDown.Name = "RotorIncrementUpDown";
            this.RotorIncrementUpDown.Size = new System.Drawing.Size(56, 20);
            this.RotorIncrementUpDown.TabIndex = 19;
            this.RotorIncrementUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.RotorIncrementUpDown.ValueChanged += new System.EventHandler(this.RotorIncrementUpDown_ValueChanged);
            // 
            // SetupDonebutton
            // 
            this.SetupDonebutton.Location = new System.Drawing.Point(170, 320);
            this.SetupDonebutton.Name = "SetupDonebutton";
            this.SetupDonebutton.Size = new System.Drawing.Size(75, 23);
            this.SetupDonebutton.TabIndex = 7;
            this.SetupDonebutton.Text = "Done";
            this.SetupDonebutton.UseVisualStyleBackColor = true;
            this.SetupDonebutton.Click += new System.EventHandler(this.SetupDonebutton_Click);
            // 
            // AzELFileDirectorytextBox
            // 
            this.AzELFileDirectorytextBox.Location = new System.Drawing.Point(191, 260);
            this.AzELFileDirectorytextBox.Name = "AzELFileDirectorytextBox";
            this.AzELFileDirectorytextBox.Size = new System.Drawing.Size(182, 20);
            this.AzELFileDirectorytextBox.TabIndex = 8;
            this.AzELFileDirectorytextBox.Text = "e:/wsjt/wsjtx-10.0\r\n";
            this.AzELFileDirectorytextBox.TextChanged += new System.EventHandler(this.AzELFileDirectorytextBox_TextChanged);
            // 
            // AzElDirectorylabel
            // 
            this.AzElDirectorylabel.AutoSize = true;
            this.AzElDirectorylabel.Location = new System.Drawing.Point(192, 244);
            this.AzElDirectorylabel.Name = "AzElDirectorylabel";
            this.AzElDirectorylabel.Size = new System.Drawing.Size(143, 13);
            this.AzElDirectorylabel.TabIndex = 9;
            this.AzElDirectorylabel.Text = "WSJT AzEl.dat File Directory";
            // 
            // StepUpDown
            // 
            this.StepUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.StepUpDown.Location = new System.Drawing.Point(305, 286);
            this.StepUpDown.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.StepUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.StepUpDown.Name = "StepUpDown";
            this.StepUpDown.Size = new System.Drawing.Size(55, 22);
            this.StepUpDown.TabIndex = 10;
            this.StepUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.StepUpDown.ValueChanged += new System.EventHandler(this.StepUpDown_ValueChanged);
            // 
            // Steplabel
            // 
            this.Steplabel.AutoSize = true;
            this.Steplabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Steplabel.Location = new System.Drawing.Point(366, 290);
            this.Steplabel.Name = "Steplabel";
            this.Steplabel.Size = new System.Drawing.Size(29, 13);
            this.Steplabel.TabIndex = 15;
            this.Steplabel.Text = "Step";
            // 
            // AZElSerialPortgroupBox
            // 
            this.AZElSerialPortgroupBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.AZElSerialPortgroupBox.Controls.Add(this.AzElRotorTypecomboBox);
            this.AZElSerialPortgroupBox.Controls.Add(this.AzElRotorSerialBaudcomboBox);
            this.AZElSerialPortgroupBox.Controls.Add(this.label2);
            this.AZElSerialPortgroupBox.Controls.Add(this.label3);
            this.AZElSerialPortgroupBox.Controls.Add(this.AzElRotorSerialPortcomboBox);
            this.AZElSerialPortgroupBox.Location = new System.Drawing.Point(13, 95);
            this.AZElSerialPortgroupBox.Name = "AZElSerialPortgroupBox";
            this.AZElSerialPortgroupBox.Size = new System.Drawing.Size(161, 102);
            this.AZElSerialPortgroupBox.TabIndex = 16;
            this.AZElSerialPortgroupBox.TabStop = false;
            this.AZElSerialPortgroupBox.Text = "AzEl Serial Port";
            // 
            // AzElRotorTypecomboBox
            // 
            this.AzElRotorTypecomboBox.FormattingEnabled = true;
            this.AzElRotorTypecomboBox.Items.AddRange(new object[] {
            "AlphaSpid RAS",
            "Yaesu GS232B",
            "Idiom Press"});
            this.AzElRotorTypecomboBox.Location = new System.Drawing.Point(6, 75);
            this.AzElRotorTypecomboBox.Name = "AzElRotorTypecomboBox";
            this.AzElRotorTypecomboBox.Size = new System.Drawing.Size(89, 21);
            this.AzElRotorTypecomboBox.TabIndex = 4;
            this.AzElRotorTypecomboBox.Text = "AlpaSpid RAS";
            this.AzElRotorTypecomboBox.SelectedIndexChanged += new System.EventHandler(this.AzElRotorTypecomboBox_SelectedIndexChanged);
            // 
            // AzElRotorSerialBaudcomboBox
            // 
            this.AzElRotorSerialBaudcomboBox.FormattingEnabled = true;
            this.AzElRotorSerialBaudcomboBox.Items.AddRange(new object[] {
            "600 N-8-1",
            "1200 N-8-1",
            "2400 N-8-1",
            "4800 N-8-1",
            "9600 N-8-1",
            "19200 N-8-1",
            "38400 N-8-1"});
            this.AzElRotorSerialBaudcomboBox.Location = new System.Drawing.Point(6, 46);
            this.AzElRotorSerialBaudcomboBox.Name = "AzElRotorSerialBaudcomboBox";
            this.AzElRotorSerialBaudcomboBox.Size = new System.Drawing.Size(89, 21);
            this.AzElRotorSerialBaudcomboBox.TabIndex = 1;
            this.AzElRotorSerialBaudcomboBox.Text = "600 N-8-1";
            this.AzElRotorSerialBaudcomboBox.SelectedIndexChanged += new System.EventHandler(this.AzElRotorBaudcomboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Baud Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Port Name";
            // 
            // AzElRotorSerialPortcomboBox
            // 
            this.AzElRotorSerialPortcomboBox.FormattingEnabled = true;
            this.AzElRotorSerialPortcomboBox.Location = new System.Drawing.Point(6, 19);
            this.AzElRotorSerialPortcomboBox.Name = "AzElRotorSerialPortcomboBox";
            this.AzElRotorSerialPortcomboBox.Size = new System.Drawing.Size(89, 21);
            this.AzElRotorSerialPortcomboBox.Sorted = true;
            this.AzElRotorSerialPortcomboBox.TabIndex = 0;
            this.AzElRotorSerialPortcomboBox.DropDown += new System.EventHandler(this.AzElRotorSerialPortcomboBox_DropDown);
            this.AzElRotorSerialPortcomboBox.SelectedIndexChanged += new System.EventHandler(this.AzElRotorSerialPortcomboBox_SelectedIndexChanged);
            // 
            // ElSerialPortgroupBox
            // 
            this.ElSerialPortgroupBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ElSerialPortgroupBox.Controls.Add(this.ElRotorTypecomboBox);
            this.ElSerialPortgroupBox.Controls.Add(this.ElRotorSerialBaudcomboBox);
            this.ElSerialPortgroupBox.Controls.Add(this.label4);
            this.ElSerialPortgroupBox.Controls.Add(this.label5);
            this.ElSerialPortgroupBox.Controls.Add(this.ElRotorSerialPortcomboBox);
            this.ElSerialPortgroupBox.Enabled = false;
            this.ElSerialPortgroupBox.Location = new System.Drawing.Point(13, 207);
            this.ElSerialPortgroupBox.Name = "ElSerialPortgroupBox";
            this.ElSerialPortgroupBox.Size = new System.Drawing.Size(161, 102);
            this.ElSerialPortgroupBox.TabIndex = 17;
            this.ElSerialPortgroupBox.TabStop = false;
            this.ElSerialPortgroupBox.Text = "El Serial Port";
            // 
            // ElRotorTypecomboBox
            // 
            this.ElRotorTypecomboBox.FormattingEnabled = true;
            this.ElRotorTypecomboBox.Items.AddRange(new object[] {
            "AlphaSpid RAS",
            "Yeasu",
            "Idiom Press"});
            this.ElRotorTypecomboBox.Location = new System.Drawing.Point(6, 75);
            this.ElRotorTypecomboBox.Name = "ElRotorTypecomboBox";
            this.ElRotorTypecomboBox.Size = new System.Drawing.Size(89, 21);
            this.ElRotorTypecomboBox.TabIndex = 4;
            this.ElRotorTypecomboBox.Text = "AlpaSpid RAS";
            // 
            // ElRotorSerialBaudcomboBox
            // 
            this.ElRotorSerialBaudcomboBox.FormattingEnabled = true;
            this.ElRotorSerialBaudcomboBox.Items.AddRange(new object[] {
            "600 N-8-1",
            "1200 N-8-1",
            "2400 N-8-1",
            "4800 N-8-1",
            "9600 N-8-1",
            "19200 N-8-1",
            "38400 N-8-1"});
            this.ElRotorSerialBaudcomboBox.Location = new System.Drawing.Point(6, 46);
            this.ElRotorSerialBaudcomboBox.Name = "ElRotorSerialBaudcomboBox";
            this.ElRotorSerialBaudcomboBox.Size = new System.Drawing.Size(89, 21);
            this.ElRotorSerialBaudcomboBox.TabIndex = 1;
            this.ElRotorSerialBaudcomboBox.Text = "600 N-8-1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Baud Rate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(101, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Port Name";
            // 
            // ElRotorSerialPortcomboBox
            // 
            this.ElRotorSerialPortcomboBox.FormattingEnabled = true;
            this.ElRotorSerialPortcomboBox.Location = new System.Drawing.Point(6, 19);
            this.ElRotorSerialPortcomboBox.Name = "ElRotorSerialPortcomboBox";
            this.ElRotorSerialPortcomboBox.Size = new System.Drawing.Size(89, 21);
            this.ElRotorSerialPortcomboBox.Sorted = true;
            this.ElRotorSerialPortcomboBox.TabIndex = 0;
            // 
            // AzElSeperatecheckBox
            // 
            this.AzElSeperatecheckBox.AutoSize = true;
            this.AzElSeperatecheckBox.Location = new System.Drawing.Point(191, 289);
            this.AzElSeperatecheckBox.Name = "AzElSeperatecheckBox";
            this.AzElSeperatecheckBox.Size = new System.Drawing.Size(102, 17);
            this.AzElSeperatecheckBox.TabIndex = 18;
            this.AzElSeperatecheckBox.Text = "Seperate AZ EL";
            this.AzElSeperatecheckBox.UseVisualStyleBackColor = true;
            this.AzElSeperatecheckBox.CheckedChanged += new System.EventHandler(this.AzElSeperatecheckBox_CheckedChanged);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 348);
            this.ControlBox = false;
            this.Controls.Add(this.AzElSeperatecheckBox);
            this.Controls.Add(this.ElSerialPortgroupBox);
            this.Controls.Add(this.AZElSerialPortgroupBox);
            this.Controls.Add(this.Steplabel);
            this.Controls.Add(this.StepUpDown);
            this.Controls.Add(this.AzElDirectorylabel);
            this.Controls.Add(this.AzELFileDirectorytextBox);
            this.Controls.Add(this.SetupDonebutton);
            this.Controls.Add(this.RotorCompensationgroupBox);
            this.Controls.Add(this.SerialPortSetupgroupBox);
            this.Location = new System.Drawing.Point(0, 660);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "System Setup";
            this.SerialPortSetupgroupBox.ResumeLayout(false);
            this.SerialPortSetupgroupBox.PerformLayout();
            this.RotorCompensationgroupBox.ResumeLayout(false);
            this.RotorCompensationgroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AzOffsetUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ElOffsetUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAzUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinAzUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxElUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinElUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OptimizeStartUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SixtySecondUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThirtySecondUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotorIncrementUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StepUpDown)).EndInit();
            this.AZElSerialPortgroupBox.ResumeLayout(false);
            this.AZElSerialPortgroupBox.PerformLayout();
            this.ElSerialPortgroupBox.ResumeLayout(false);
            this.ElSerialPortgroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BaudRatelabel;
        private System.Windows.Forms.Label Portnamelabel;
        public System.Windows.Forms.ComboBox SerialPortComboBox;
        private System.Windows.Forms.GroupBox SerialPortSetupgroupBox;
        private System.Windows.Forms.GroupBox RotorCompensationgroupBox;
        private System.Windows.Forms.Button SetupDonebutton;
        private System.Windows.Forms.Label RotorIncrementlabel;
        private System.Windows.Forms.Label AzElDirectorylabel;
        public System.Windows.Forms.ComboBox BaudRatecomboBox;
        public System.Windows.Forms.NumericUpDown RotorIncrementUpDown;
        public System.Windows.Forms.TextBox AzELFileDirectorytextBox;
        private System.Windows.Forms.Label ThirtySecondStartlabel;
        private System.Windows.Forms.Label SixtySecondStartlabel;
        public System.Windows.Forms.NumericUpDown ThirtySecondUpDown;
        public System.Windows.Forms.NumericUpDown SixtySecondUpDown;
        private System.Windows.Forms.Label OptimizeStartlabel;
        public System.Windows.Forms.NumericUpDown OptimizeStartUpDown;
        private System.Windows.Forms.Label Steplabel;
        public System.Windows.Forms.NumericUpDown StepUpDown;
        private System.Windows.Forms.GroupBox AZElSerialPortgroupBox;
        public System.Windows.Forms.ComboBox AzElRotorSerialBaudcomboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox AzElRotorSerialPortcomboBox;
        public System.Windows.Forms.ComboBox AzElRotorTypecomboBox;
        private System.Windows.Forms.GroupBox ElSerialPortgroupBox;
        public System.Windows.Forms.ComboBox ElRotorTypecomboBox;
        public System.Windows.Forms.ComboBox ElRotorSerialBaudcomboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox ElRotorSerialPortcomboBox;
        private System.Windows.Forms.CheckBox AzElSeperatecheckBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown MaxElUpDown;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown MinElUpDown;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown MaxAzUpDown;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown MinAzUpDown;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.NumericUpDown AzOffsetUpDown;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.NumericUpDown ElOffsetUpDown;
    }
}