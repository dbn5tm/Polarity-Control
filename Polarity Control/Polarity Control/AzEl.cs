using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Polarity_Control;

namespace Polarity_Control
{
    public partial class AzElform : Form
    {
        public AzElform()
        {
            InitializeComponent();
        }

        private void AzElDrivecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AzElDrivecheckBox.Checked)
            {
                AzElDrivecheckBox.BackColor = Color.Lime;
            }
            else
            {
                AzElDrivecheckBox.BackColor = Color.Empty;
            }
        }

        private void AzElform_Load(object sender, EventArgs e)
        {

        }

        private void AzElform_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalVar.AzElForm = null;
        }

        private void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManual.Checked)
            {
                AzElDrivecheckBox.Checked = false;
                AzElDrivecheckBox.Enabled = false;
                chkManual.BackColor = Color.Lime;
            }
            else
            {
                AzElDrivecheckBox.Enabled = true;
                chkManual.BackColor = Color.Empty;
            }
        }

        struct OutQueueEntry
        {
            public byte[] Message;
            public UInt16 Length;
        };

        private void btnStop_Click(object sender, EventArgs e)
        {
            switch (GlobalVar.SetupForm.AzElRotorTypecomboBox.Text)
            {
                case "AlphaSpid RAS":
                    break;

                case "Yaesu GS232B":
                    try
                    {
                        byte[] Message = new byte[2];
                        UInt16 Length = 0;

                        Message[Length++] = 0x53;   // "S"
                        Message[Length++] = 0x0D;

                        OutQueueEntry myOutQueueEntry = new OutQueueEntry();
                        myOutQueueEntry.Message = Message;
                        myOutQueueEntry.Length = Length;

                        GlobalVar.Main.AzElserialPort.Write(myOutQueueEntry.Message, 0, myOutQueueEntry.Length);
                    }
                    catch 
                    { }
                    break;
            }
        }

        private void btnManualGo_Click(object sender, EventArgs e)
        {
            switch (GlobalVar.SetupForm.AzElRotorTypecomboBox.Text)
            {
                case "AlphaSpid RAS":
                    break;

                case "Yaesu GS232B":
                    try
                    {
                        byte[] Message = new byte[9];
                        UInt16 Length = 0;

                        // convert the azimuth to double
                        decimal Az = Convert.ToDecimal(AzElRequestAztextBox.Text);
                        string myAz = ((int)(Az)).ToString("D3");
                        Message[Length++] = 0x57;   // "W"
                        Message[Length++] = (byte)Convert.ToChar(myAz.Substring(0, 1));
                        Message[Length++] = (byte)Convert.ToChar(myAz.Substring(1, 1));
                        Message[Length++] = (byte)Convert.ToChar(myAz.Substring(2, 1));
                        Message[Length++] = 0x20;

                        // convert the elevation to double
                        decimal El = Convert.ToDecimal(AzElRequestEltextBox.Text);
                        string myEl = ((int)El).ToString("D3");
                        Message[Length++] = (byte)Convert.ToChar(myEl.Substring(0, 1));
                        Message[Length++] = (byte)Convert.ToChar(myEl.Substring(1, 1));
                        Message[Length++] = (byte)Convert.ToChar(myEl.Substring(2, 1));
                        Message[Length++] = 0x0D;

                        OutQueueEntry myOutQueueEntry = new OutQueueEntry();
                        myOutQueueEntry.Message = Message;
                        myOutQueueEntry.Length = Length;

                        GlobalVar.Main.AzElserialPort.Write(myOutQueueEntry.Message, 0, myOutQueueEntry.Length);
                    }
                    catch 
                    { }
                    break;
            }
        }
    }
}
