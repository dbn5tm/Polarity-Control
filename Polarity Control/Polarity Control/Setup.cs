using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Polarity_Control;


namespace Polarity_Control
{   
   public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();

            // Restore setup form values
            SerialPortComboBox.Text = Properties.Settings.Default.PolaritySerialPortName;
            BaudRatecomboBox.Text = Properties.Settings.Default.PolaritySerialPortBaud;
            AzELFileDirectorytextBox.Text = Properties.Settings.Default.WSJTFileDir;
            RotorIncrementUpDown.Value = Properties.Settings.Default.RotorIncrement;
            SixtySecondUpDown.Value = Properties.Settings.Default.SixtySecondStart;
            ThirtySecondUpDown.Value = Properties.Settings.Default.ThirtySecondStart;
            OptimizeStartUpDown.Value = Properties.Settings.Default.OptimizeStart;
            StepUpDown.Value = Properties.Settings.Default.Step;
            AzElRotorSerialPortcomboBox.Text = Properties.Settings.Default.AzElSerialPortName;
            AzElRotorSerialBaudcomboBox.Text = Properties.Settings.Default.AzElSerialPortBaud;
            AzElRotorTypecomboBox.Text = Properties.Settings.Default.AzElRotorType;
            MaxElUpDown.Value = Properties.Settings.Default.MaxElev;
            MinElUpDown.Value = Properties.Settings.Default.MinElev;
            MaxAzUpDown.Value = Properties.Settings.Default.MaxAz;
            MinAzUpDown.Value = Properties.Settings.Default.MinAz;
            AzOffsetUpDown.Value = Properties.Settings.Default.AzOffset;
            ElOffsetUpDown.Value = Properties.Settings.Default.ElOffset;

            // Set the current port value into the combo box
            GlobalVar.Main.MODBUSPort.PortName = SerialPortComboBox.Text;
            GlobalVar.Main.AzElserialPort.PortName = AzElRotorSerialPortcomboBox.Text;

            // Set the current Baud rate
            SetBaud(GlobalVar.Main.MODBUSPort,  BaudRatecomboBox.Text);
            SetBaud(GlobalVar.Main.AzElserialPort, AzElRotorSerialBaudcomboBox.Text);
        }


        private void SetBaud(SerialPort mySerialPort, string Baud)
        {
            switch (Baud)
            {       
                case "600 N-8-1":
                    mySerialPort.BaudRate = 600;
                    mySerialPort.Parity = System.IO.Ports.Parity.None;
                    mySerialPort.StopBits = System.IO.Ports.StopBits.One;

                    break;

                case "1200 N-8-1":
                    mySerialPort.BaudRate = 1200;
                    mySerialPort.Parity = System.IO.Ports.Parity.None;
                    mySerialPort.StopBits = System.IO.Ports.StopBits.One;

                    break;

                case "2400 N-8-1":
                    mySerialPort.BaudRate = 2400;
                    mySerialPort.Parity = System.IO.Ports.Parity.None;
                    mySerialPort.StopBits = System.IO.Ports.StopBits.One;

                    break;

                case "4800 N-8-1":
                    mySerialPort.BaudRate = 4800;
                    mySerialPort.Parity = System.IO.Ports.Parity.None;
                    mySerialPort.StopBits = System.IO.Ports.StopBits.One;

                    break;

                case "9600 N-8-1":
                    mySerialPort.BaudRate = 9600;
                    mySerialPort.DataBits = 8;
                    mySerialPort.Parity = System.IO.Ports.Parity.None;
                    mySerialPort.StopBits = System.IO.Ports.StopBits.One;

                    break;

                case "19200 N-8-1":
                    mySerialPort.BaudRate = 19200;
                    mySerialPort.Parity = System.IO.Ports.Parity.None;
                    mySerialPort.StopBits = System.IO.Ports.StopBits.One;

                    break;

                case "38400 N-8-1":
                    mySerialPort.BaudRate = 38400;
                    mySerialPort.Parity = System.IO.Ports.Parity.None;
                    mySerialPort.StopBits = System.IO.Ports.StopBits.One;

                    break;

                default:
                    // No change
                    break;
            }
        }

        private void SerialPortcomboBox_DropDown(object sender, EventArgs e)
        {
            SerialPortComboBox.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                SerialPortComboBox.Items.Add(port);
            }
        }

        private void BaudcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save this to program default
            Properties.Settings.Default.PolaritySerialPortBaud = BaudRatecomboBox.Text;
            Properties.Settings.Default.Save();
        }

        private void SerialPortcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save to the program defaults
            Properties.Settings.Default.PolaritySerialPortName = SerialPortComboBox.Text;
            Properties.Settings.Default.Save();
            
            // Check if the port is open
            if (GlobalVar.Main.MODBUSPort.IsOpen)
            {
                GlobalVar.Main.MODBUSPort.Close();
            }

            // copy the new name in
            GlobalVar.Main.MODBUSPort.PortName = SerialPortComboBox.Text;

            //  Try to open the port
            try
            {
                GlobalVar.Main.MODBUSPort.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Serial Port Error", MessageBoxButtons.OK);
            }
        }

        private void SetupDonebutton_Click(object sender, EventArgs e)
        {
            GlobalVar.SetupForm.Hide();
        }

        private void RotorIncrementUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Save it
            Properties.Settings.Default.RotorIncrement = RotorIncrementUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void AzELFileDirectorytextBox_TextChanged(object sender, EventArgs e)
        {
            // Update the AzEL filter
            GlobalVar.Main.AzElfileWatcher.Path = "e:/wsjt/wsjt-10.0";
            //GlobalVar.Main.AzElfileWatcher.Path = AzELFileDirectorytextBox.Text;

            // Save it
            Properties.Settings.Default.WSJTFileDir = "e:/wsjt/wsjt-10.0"; //AzELFileDirectorytextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void SixtySecondUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SixtySecondStart = SixtySecondUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void ThirtySecondUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ThirtySecondStart = ThirtySecondUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void RXOptimizeStartUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.OptimizeStart = OptimizeStartUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void StepUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Step = StepUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void AzElRotorBaudcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AzElSerialPortBaud = AzElRotorSerialBaudcomboBox.Text;
            Properties.Settings.Default.Save();

            SetBaud(GlobalVar.Main.AzElserialPort, AzElRotorSerialBaudcomboBox.Text);
        }

        private void AzElRotorTypecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AzElRotorType = AzElRotorTypecomboBox.Text;
            Properties.Settings.Default.Save();
        }

        private void AzElRotorSerialPortcomboBox_DropDown(object sender, EventArgs e)
        {
            AzElRotorSerialPortcomboBox.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                AzElRotorSerialPortcomboBox.Items.Add(port);
            }
        }

        private void AzElRotorSerialPortcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AzElSerialPortName = AzElRotorSerialPortcomboBox.Text;
            Properties.Settings.Default.Save();

            // Check if the port is open
            if (GlobalVar.Main.AzElserialPort.IsOpen)
            {
                GlobalVar.Main.AzElserialPort.Close();
            }

            // copy the new name in
            GlobalVar.Main.AzElserialPort.PortName = SerialPortComboBox.Text;

            //  Try to open the port
            try
            {
                GlobalVar.Main.AzElserialPort.Open();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Serial Port Error");
            }



        }

        private void AzElSeperatecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AzElSeperatecheckBox.Checked)
            {
                AZElSerialPortgroupBox.Text = "AZ Serial Port";
                AZElSerialPortgroupBox.Enabled = true;
            }
            else
            {
                AZElSerialPortgroupBox.Text = "AzEL Serial Port";
                ElSerialPortgroupBox.Enabled = false;
            }
        }

        private void MinElUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MinElev = MinElUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void MaxElUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MaxElev = MaxElUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void MinAzUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MinAz = MinAzUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void MaxAzUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.MaxAz = MaxAzUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void ElOffsetUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ElOffset = ElOffsetUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void AzOffsetUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AzOffset = AzOffsetUpDown.Value;
            Properties.Settings.Default.Save();
        }

    }
}
