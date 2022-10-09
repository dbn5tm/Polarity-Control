using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using Polarity_Control;
using System.Text.RegularExpressions;



namespace Polarity_Control
{
    public partial class AntennaPolarityControl : Form
    {
        public string Version = "0.1.01";

        public AntennaPolarityControl()
        {
            InitializeComponent();

            // THIS IS NOT A GOOD IDEA!!!!  However most of the cross thread issues are with objects that are very static so
            // OK until I can fix it correctly.
            Control.CheckForIllegalCrossThreadCalls = false;

            GlobalVar.Main = this;
            GlobalVar.SetupForm = new SetupForm();

            Antenna1RXPolarityUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna1RXOffsetUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna1TXPolarityUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna1TXOffsetUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna2RXPolarityUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna2TXPolarityUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna2TXOffsetUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna2TrackOffsetUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            CQStartUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            CQIncrementUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            ScanStartUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            ScanIncrementUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;

            CQStartUpDown.Value = Properties.Settings.Default.CQStart;
            CQIncrementUpDown.Value = Properties.Settings.Default.CQIncrement;
            CQStepsUpDown.Value = Properties.Settings.Default.CQSteps;
            CQRepeatUpDown.Value = Properties.Settings.Default.CQRepeat;
            ScanStartUpDown.Value = Properties.Settings.Default.ScanStart;
            ScanIncrementUpDown.Value = Properties.Settings.Default.ScanIncrement;
            ScanStepsUpDown.Value = Properties.Settings.Default.ScanSteps;
            ScanRepeatUpDown.Value = Properties.Settings.Default.ScanRepeat;
            OptimizeRXcheckBox.Checked = Properties.Settings.Default.OptimizeRX;
            OptimizeTXcheckBox.Checked = Properties.Settings.Default.OptimizeTX;

            for (int i = 0; i < DeltaNoise.Length; i++)
            {
                DeltaNoise[i] = 0.0;
            }

            if (Antenna2TrackAntenna1checkBox.Checked)
            {
                Antenna2groupBox.Enabled = false;
            }
            else
            {
                Antenna2groupBox.Enabled = true;
            }

            InFirstTXSequence();


            // Open the Modbus serial port
            try
            {
                MODBUSPort.Open();
            }
            catch (Exception ex)
            {
                SerialFailCount = 0xFFFF;
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Serial Port Error", MessageBoxButtons.OK);
            }

            // Open the AzEl serial port
            /*
            try
            {
                AzElserialPort.Open();
                if (AzElserialPort.IsOpen)
                {
                    switch (GlobalVar.SetupForm.AzElRotorTypecomboBox.Text)
                    {
                        case "Yaesu GS232B":
                            AzElserialPort.DtrEnable = true;
                            Thread.Sleep(200);
                            AzElserialPort.Write("\r\r");     // set controller baud rate
                            Thread.Sleep(500);
                            AzElserialPort.Write("X1\r");   // set speed = slow
                            Thread.Sleep(500);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "AzEl Serial Port Error", MessageBoxButtons.OK);
            }*/
            // Start the worker threads
            MODBUSWorker.RunWorkerAsync();
            AzElWorker.RunWorkerAsync();

            // Set up and enable the file watcher
            AzElfileWatcher.Path = Properties.Settings.Default.WSJTFileDir;
            AzElfileWatcher.Filter = Path.GetFileName("AzEl.dat");
            AzElfileWatcher.EnableRaisingEvents = true;
        }

        static class Constants
        {
            public const int MAXSERIALFAILCOUNT = 3;
        }
        private static int CurrentCQStep = 0;
        private static int CurrentCQCount = 0;
        private static int CurrentScanStep = 0;
        private static int CurrentScanCount = 0;
        private static int SerialFailCount = 0;

        public static double Antenna1CurrentRequestedPosition = 0;
        public static double Antenna1CurrentReportedPosition = 0;
        private static double Antenna2CurrentRequestedPosition = 0;
        private static double Antenna2CurrentReportedPosition = 0;
        public volatile ushort Antenna1CurrentPosition = 0;
        public volatile ushort Antenna2CurrentPosition = 0;
        private static decimal ScanCurrentPosition = 0;
        private static decimal CQCurrentPosition = 0;
        public double RXNoise = 0;
        public bool InRXNoiseScan = false;
        public double[] DeltaNoise = new double[361];
        public volatile bool CalibrateStatus = false;
        private decimal CurrentAzValue = 0;
        private decimal CurrentElValue = 0;

        public bool InTXSequence()
        {
            int mySecond = System.DateTime.Now.Second + 1;
            int myMinute = System.DateTime.Now.Minute;
            bool ReturnValue = false;

            if (Sequence30radioButton.Checked)
            {
                if (SequenceTXFirstcheckBox.Checked)
                {
                    if ((mySecond >= 0 && mySecond <= GlobalVar.SetupForm.ThirtySecondUpDown.Value) ||
                        (mySecond >= (GlobalVar.SetupForm.ThirtySecondUpDown.Value + 30)))
                    {
                        ReturnValue = true;
                    }
                }
                else
                {
                    if (mySecond >= GlobalVar.SetupForm.ThirtySecondUpDown.Value && mySecond <= (GlobalVar.SetupForm.ThirtySecondUpDown.Value + 30))
                    {
                        ReturnValue = true;
                    }
                }
            }
            else if (Sequence60radioButton.Checked)
            {
                if (SequenceTXFirstcheckBox.Checked)
                {
                    if ((myMinute % 2) == 0)
                    {
                        if (mySecond <= GlobalVar.SetupForm.SixtySecondUpDown.Value)
                        {
                            ReturnValue = true;
                        }
                    }
                    else
                    {
                        if (mySecond > GlobalVar.SetupForm.SixtySecondUpDown.Value)
                        {
                            ReturnValue = true;
                        }
                    }
                }
                else
                {
                    if ((myMinute % 2) == 1)
                    {
                        if (mySecond <= GlobalVar.SetupForm.SixtySecondUpDown.Value)
                        {
                            ReturnValue = true;
                        }
                    }
                    else
                    {
                        if (mySecond > GlobalVar.SetupForm.SixtySecondUpDown.Value)
                        {
                            ReturnValue = true;
                        }
                    }
                }
            }

            return (ReturnValue);
        }

        private static bool OldInTXSequence = false;
        public bool InFirstTXSequence()
        {
            bool ReturnValue = false;
            bool myInTXSequence;

            myInTXSequence = InTXSequence();

            if (myInTXSequence && (myInTXSequence != OldInTXSequence))
            {
                ReturnValue = true;
            }

            OldInTXSequence = myInTXSequence;

            return (ReturnValue);
        }

        private static bool OldInRXSequence = false;
        public bool InFirstRXSequence()
        {
            bool ReturnValue = false;
            bool myInRXSequence;

            myInRXSequence = !InTXSequence();

            if (myInRXSequence && (myInRXSequence != OldInRXSequence))
            {
                ReturnValue = true;
            }

            OldInRXSequence = myInRXSequence;

            return (ReturnValue);
        }

        private void CalibrateButtion_Click(object sender, EventArgs e)
        {
            if (DrivecheckBox.Checked)
            {
                SendCalibrateRequest();
            }
        }

        private void Antenna1TXNoneradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna1TXNoneradioButton.Checked)
            {
                Antenna1TXPolarityUpDown.Enabled = true;
                Antenna1TXOffsetUpDown.Enabled = false;
            }
        }

        private void Antenna1TXAutoradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna1TXAutoradioButton.Checked)
            {
                Antenna1TXPolarityUpDown.Enabled = true;
                Antenna1TXOffsetUpDown.Enabled = false;
                Antenna1TXRateUpDown.Enabled = true;
            }
            else
            {
                Antenna1TXRateUpDown.Enabled = false;
            }
        }

        private void Antenna1TXeqRXradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna1TXeqRXradioButton.Checked)
            {
                Antenna1TXPolarityUpDown.Enabled = false;
                Antenna1TXOffsetUpDown.Enabled = true;
                Antenna1RXDPOLradioButton.Enabled = false;
                Antenna1RXeqTXradioButton.Enabled = false;
            }
            else
            {
                Antenna1RXDPOLradioButton.Enabled = true;
                Antenna1RXeqTXradioButton.Enabled = true;
            }
        }

        private void Antenna1TXDPOLradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna1TXDPOLradioButton.Checked)
            {
                Antenna1TXPolarityUpDown.Enabled = false;
                Antenna1TXOffsetUpDown.Enabled = true;
                Antenna1RXDPOLradioButton.Enabled = false;
            }
            else
            {
                Antenna1RXDPOLradioButton.Enabled = true;
            }
        }

        private void Antenna1TXCQradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna1TXCQradioButton.Checked)
            {
                Antenna1TXPolarityUpDown.Enabled = false;
                Antenna1TXOffsetUpDown.Enabled = false;
                CurrentCQCount = 0;
                CurrentCQStep = 0;
                CQCurrentPosition = CQStartUpDown.Value;
            }
        }

        private void Antenna2TXNoneradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna2TXNoneradioButton.Checked)
            {
                Antenna2TXPolarityUpDown.Enabled = true;
                Antenna2TXOffsetUpDown.Enabled = false;
                Antenna2TXRateUpDown.Enabled = false;
            }
        }

        private void Antenna2TXAutoradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna2TXAutoradioButton.Checked)
            {
                Antenna2TXPolarityUpDown.Enabled = true;
                Antenna2TXOffsetUpDown.Enabled = false;
                Antenna2TXRateUpDown.Enabled = true;
            }
        }

        private void Antenna2TXeqRXradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna2TXeqRXradioButton.Checked)
            {
                Antenna2TXPolarityUpDown.Enabled = false;
                Antenna2TXOffsetUpDown.Enabled = true;
                Antenna2TXRateUpDown.Enabled = false;
                Antenna2RXeqTXradioButton.Enabled = false;
            }
            else
            {
                Antenna2RXeqTXradioButton.Enabled = true;
            }
        }

        private void Antenna2TrackAntenna1checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna2TrackAntenna1checkBox.Checked)
            {
                Antenna2groupBox.Enabled = false;
                Antenna2TrackOffsetUpDown.Enabled = true;
            }
            else
            {
                Antenna2groupBox.Enabled = true;
                Antenna2TrackOffsetUpDown.Enabled = false;
            }
        }

        private void StepUpDown_ValueChanged(object sender, EventArgs e)
        {
            Antenna1RXPolarityUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna1RXOffsetUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna1TXPolarityUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna1TXOffsetUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna2RXPolarityUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna2TXPolarityUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna2TXOffsetUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            Antenna2TrackOffsetUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            CQStartUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            CQIncrementUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            ScanStartUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
            ScanIncrementUpDown.Increment = GlobalVar.SetupForm.StepUpDown.Value;
        }

        private void Antenna1RXDPOLradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna1RXDPOLradioButton.Checked)
            {
                Antenna1RXPolarityUpDown.Enabled = false;
                Antenna1RXOffsetUpDown.Enabled = true;
                Antenna1TXDPOLradioButton.Enabled = false;
                Antenna1TXeqRXradioButton.Enabled = false;
                NoiseAtReciprocalbutton.Enabled = false;
            }
            else
            {
                Antenna1TXDPOLradioButton.Enabled = true;
                Antenna1TXeqRXradioButton.Enabled = true;
            }
        }

        private void Antenna1RXNoneradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna1RXNoneradioButton.Checked)
            {
                Antenna1RXPolarityUpDown.Enabled = true;
                Antenna1RXOffsetUpDown.Enabled = false;
                NoiseAtReciprocalbutton.Enabled = true;
            }
        }

        private void Antenna1RXAutoradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna1RXAutoradioButton.Checked)
            {
                Antenna1RXPolarityUpDown.Enabled = true;
                Antenna1RXOffsetUpDown.Enabled = false;
                Antenna1RXRateUpDown.Enabled = true;
                NoiseAtReciprocalbutton.Enabled = true;
            }
            else
            {
                Antenna1RXRateUpDown.Enabled = false;
            }
        }

        private void Antenna1RXeqTXradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna1RXeqTXradioButton.Checked)
            {
                Antenna1RXPolarityUpDown.Enabled = false;
                Antenna1RXOffsetUpDown.Enabled = true;
                Antenna1TXDPOLradioButton.Enabled = false;
                Antenna1TXeqRXradioButton.Enabled = false;
                NoiseAtReciprocalbutton.Enabled = false;
            }
            else
            {
                Antenna1TXDPOLradioButton.Enabled = true;
                Antenna1TXeqRXradioButton.Enabled = true;
            }
        }

        private void Antenna1RXScanradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna1RXScanradioButton.Checked)
            {
                Antenna1RXPolarityUpDown.Enabled = false;
                Antenna1RXOffsetUpDown.Enabled = false;
                CurrentScanCount = 0;
                CurrentScanStep = 0;
                ScanCurrentPosition = ScanStartUpDown.Value;
                NoiseAtReciprocalbutton.Enabled = false;
            }
        }

        private void Antenna2RXNoneradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna2RXNoneradioButton.Checked)
            {
                Antenna2RXPolarityUpDown.ReadOnly = false;
                Antenna2RXRateUpDown.Enabled = false;
                Antenna2RXOffsetUpDown.Enabled = false;
            }
        }

        private void Antenna2RXAutoradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna2RXAutoradioButton.Checked)
            {
                Antenna2RXPolarityUpDown.ReadOnly = false;
                Antenna2RXRateUpDown.Enabled = true;
                Antenna2RXOffsetUpDown.Enabled = false;
            }
        }

        private void Antenna2RXeqTXradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (Antenna2RXeqTXradioButton.Checked)
            {
                Antenna2RXPolarityUpDown.ReadOnly = true;
                Antenna2RXRateUpDown.Enabled = false;
                Antenna2RXOffsetUpDown.Enabled = true;
                Antenna2TXeqRXradioButton.Enabled = false;
            }
            else
            {
                Antenna2TXeqRXradioButton.Enabled = true;
            }
        }

        private void Scantimer_Tick(object sender, EventArgs e)
        {
            // Use this timer to update the clock
            DateTime d = DateTime.Now.ToUniversalTime();
            ClocktextBox.Text = d.ToString("HH:mm:ss");

            double myAnt1TXPolarity = (double)Antenna1TXPolarityUpDown.Value;
            double myAnt1RXPolarity = (double)Antenna1RXPolarityUpDown.Value;
            double myAnt2TXPolarity = (double)Antenna2TXPolarityUpDown.Value;
            double myAnt2RXPolarity = (double)Antenna2RXPolarityUpDown.Value;

            // Calculate the new values
            if (Antenna1RXAutoradioButton.Checked)
            {
                double myRXRate = (double)Antenna1RXRateUpDown.Value;
                myAnt1RXPolarity = myAnt1RXPolarity + (myRXRate / 60.0);
            }
            else if (Antenna1RXeqTXradioButton.Checked)
            {
                myAnt1RXPolarity = myAnt1TXPolarity;
            }
            else if (Antenna1RXDPOLradioButton.Checked)
            {
                double myDPOL = (double)DPOLUpDown.Value;
                myAnt1RXPolarity = (myDPOL * 2.0) - myAnt1TXPolarity;
            }
            else if (Antenna1RXScanradioButton.Checked && (!(Antenna1TXCQradioButton.Checked)))
            {
                if (InFirstRXSequence())
                {
                    if (CurrentScanCount == 0 && CurrentScanStep == 0)
                    {
                        CurrentScanCount = 1;
                        ScanCurrentPosition = ScanStartUpDown.Value;
                    }
                    else
                    {
                        CurrentScanCount++;
                        if (CurrentScanCount > ScanRepeatUpDown.Value)
                        {
                            CurrentScanStep++;
                            if (CurrentScanStep > ScanStepsUpDown.Value)
                            {
                                CurrentScanStep = 0;
                                CurrentScanCount = 1;
                                ScanCurrentPosition = ScanStartUpDown.Value;
                            }
                            else
                            {
                                CurrentScanCount = 1;
                                ScanCurrentPosition += ScanIncrementUpDown.Value;
                            }
                        }
                    }
                }
                myAnt1RXPolarity = (double)ScanCurrentPosition;
            }


            if (Antenna1TXeqRXradioButton.Checked)
            {
                myAnt1TXPolarity = myAnt1RXPolarity;
            }
            else if (Antenna1TXAutoradioButton.Checked)
            {
                double myTXRate = (double)Antenna1TXRateUpDown.Value;
                myAnt1TXPolarity = myAnt1TXPolarity + (myTXRate / 60.0);
            }
            else if (Antenna1TXDPOLradioButton.Checked)
            {
                double myDPOL = (double)DPOLUpDown.Value;
                myAnt1TXPolarity = (myDPOL * 2.0) - myAnt1RXPolarity;
            }
            else if (Antenna1TXCQradioButton.Checked && (!(Antenna1RXScanradioButton.Checked)))
            {
                if (InFirstTXSequence())
                {
                    if (CurrentCQCount == 0 && CurrentCQStep == 0)
                    {
                        CurrentCQCount = 1;
                        CQCurrentPosition = CQStartUpDown.Value;
                    }
                    else
                    {
                        CurrentCQCount++;
                        if (CurrentCQCount > CQRepeatUpDown.Value)
                        {
                            CurrentCQStep++;
                            if (CurrentCQStep > CQStepsUpDown.Value)
                            {
                                CurrentCQStep = 0;
                                CurrentCQCount = 1;
                                CQCurrentPosition = CQStartUpDown.Value;
                            }
                            else
                            {
                                CurrentCQCount = 1;
                                CQCurrentPosition += CQIncrementUpDown.Value;
                            }
                        }
                    }
                }
                myAnt1TXPolarity = (double)CQCurrentPosition;
            }

            if (Antenna1TXCQradioButton.Checked && Antenna1RXScanradioButton.Checked)
            {
                if (InFirstTXSequence())
                {
                    if (CurrentCQCount == 0 && CurrentCQStep == 0)
                    {
                        CurrentCQCount = 1;
                        myAnt1TXPolarity = (double)CQStartUpDown.Value;
                        CurrentScanCount = 1;
                        CurrentScanStep = 0;
                        ScanCurrentPosition = ScanStartUpDown.Value;
                    }
                    else if (CurrentScanStep == ScanStepsUpDown.Value)
                    {
                        CurrentCQStep++;
                        if (CurrentCQStep > CQStepsUpDown.Value)
                        {
                            CurrentCQStep = 0;
                            CurrentCQCount = 1;
                            CQCurrentPosition = CQStartUpDown.Value;
                            ScanCurrentPosition = ScanStartUpDown.Value;
                        }
                        else
                        {
                            CurrentCQCount = 1;
                            CQCurrentPosition += CQIncrementUpDown.Value;
                        }
                    }
                }
                else if (InFirstRXSequence())
                {
                    CurrentScanCount++;
                    if (CurrentScanCount > ScanRepeatUpDown.Value)
                    {
                        CurrentScanStep++;
                        if (CurrentScanStep > ScanStepsUpDown.Value)
                        {
                            CurrentScanStep = 0;
                            CurrentScanCount = 1;
                            ScanCurrentPosition = ScanStartUpDown.Value;
                        }
                        else
                        {
                            CurrentScanCount = 1;
                            ScanCurrentPosition += ScanIncrementUpDown.Value;
                        }
                    }
                }
                myAnt1RXPolarity = (double)ScanCurrentPosition;
                myAnt1TXPolarity = (double)CQCurrentPosition;
            }

            if (Antenna2TrackAntenna1checkBox.Checked)
            {
                myAnt2RXPolarity = myAnt1RXPolarity + (double)Antenna2TrackOffsetUpDown.Value;
                myAnt2TXPolarity = myAnt1TXPolarity + (double)Antenna2TrackOffsetUpDown.Value;
            }
            else
            {
                if (Antenna2RXAutoradioButton.Checked)
                {
                    double myRXRate = (double)Antenna2RXRateUpDown.Value;
                    myAnt2RXPolarity = myAnt2RXPolarity + (myRXRate / 60.0);
                }

                if (Antenna2TXeqRXradioButton.Checked)
                {
                    myAnt2TXPolarity = myAnt2RXPolarity;
                }
                else if (Antenna2TXAutoradioButton.Checked)
                {
                    double myTXRate = (double)Antenna2TXRateUpDown.Value;
                    myAnt2TXPolarity = myAnt2TXPolarity + (myTXRate / 60.0);
                }
                else if (Antenna2RXeqTXradioButton.Checked)
                {
                    myAnt2RXPolarity = myAnt2TXPolarity;
                }
            }

            if (Antenna1RXOffsetUpDown.Enabled)
            {
                myAnt1RXPolarity += (double)Antenna1RXOffsetUpDown.Value;
            }

            if (Antenna1TXOffsetUpDown.Enabled)
            {
                myAnt1TXPolarity += (double)Antenna1TXOffsetUpDown.Value;
            }

            if (Antenna2RXOffsetUpDown.Enabled)
            {
                myAnt2RXPolarity += (double)Antenna2RXOffsetUpDown.Value;
            }

            if (Antenna2TXOffsetUpDown.Enabled)
            {
                myAnt2TXPolarity += (double)Antenna2TXOffsetUpDown.Value;
            }
            if (myAnt1TXPolarity == 180)
            {
                Console.Write("here");
            }
            if (myAnt1RXPolarity >= 180.0)
            {
                myAnt1RXPolarity -= 180.0;
            }
            else if (myAnt1RXPolarity <= -180.0)
            {
                myAnt1RXPolarity += 180.0;
            }


            if (myAnt1TXPolarity >= 180.0)
            {
                myAnt1TXPolarity -= 180.0;
            }
            else if (myAnt1TXPolarity <= -180.0)
            {
                myAnt1TXPolarity += 180.0;
            }


            if (myAnt2RXPolarity >= 180.0)
            {
                myAnt2RXPolarity -= 180.0;
            }
            else if (myAnt2RXPolarity <= -180.0)
            {
                myAnt2RXPolarity += 180.0;
            }


            if (myAnt2TXPolarity >= 180.0)
            {
                myAnt2TXPolarity -= 180.0;
            }
            else if (myAnt2TXPolarity <= -180.0)
            {
                myAnt2TXPolarity += 180.0;
            }

            // check if we do RX optimization
            if (OptimizeRXcheckBox.Checked)
            {
                // Perform the optimization on antenna 1
                if (myAnt1RXPolarity < (double)(GlobalVar.SetupForm.OptimizeStartUpDown.Value - 180))
                {
                    myAnt1RXPolarity += 180;
                }
                else if (myAnt1RXPolarity > (double)GlobalVar.SetupForm.OptimizeStartUpDown.Value)
                {
                    myAnt1RXPolarity -= 180;
                }

                // Now do it for ant2
                if (myAnt2RXPolarity < (double)(GlobalVar.SetupForm.OptimizeStartUpDown.Value - 180))
                {
                    myAnt2RXPolarity += 180;
                }
                else if (myAnt2RXPolarity > (double)GlobalVar.SetupForm.OptimizeStartUpDown.Value)
                {
                    myAnt2RXPolarity -= 180;
                }
            }

            // Check if we do TX optimization
            if (OptimizeTXcheckBox.Checked)
            {
                if (myAnt1TXPolarity > myAnt1RXPolarity)
                {
                    if ((myAnt1TXPolarity - myAnt1RXPolarity) > 90)
                    {
                        if (myAnt1TXPolarity > 0)
                        {
                            myAnt1TXPolarity -= 180;
                        }
                        else if (myAnt1TXPolarity < 0)
                        {
                            myAnt1TXPolarity += 180;
                        }
                    }
                }
                else if (myAnt1TXPolarity < myAnt1RXPolarity)
                {
                    if ((myAnt1RXPolarity - myAnt1TXPolarity) > 90)
                    {
                        if (myAnt1TXPolarity > 0)
                        {
                            myAnt1TXPolarity -= 180;
                        }
                        else if (myAnt1TXPolarity < 0)
                        {
                            myAnt1TXPolarity += 180;
                        }
                    }
                }

                if (myAnt2TXPolarity > myAnt2RXPolarity)
                {
                    if ((myAnt2TXPolarity - myAnt2RXPolarity) > 90)
                    {
                        if (myAnt2TXPolarity > 0)
                        {
                            myAnt2TXPolarity -= 180;
                        }
                        else if (myAnt2TXPolarity < 0)
                        {
                            myAnt2TXPolarity += 180;
                        }
                    }
                }
                else if (myAnt2TXPolarity < myAnt2RXPolarity)
                {
                    if ((myAnt2RXPolarity - myAnt2TXPolarity) > 90)
                    {
                        if (myAnt2TXPolarity > 0)
                        {
                            myAnt2TXPolarity -= 180;
                        }
                        else if (myAnt2TXPolarity < 0)
                        {
                            myAnt2TXPolarity += 180;
                        }
                    }
                }
            }
            

            Antenna1RXPolarityUpDown.Value = (decimal)myAnt1RXPolarity;
            Antenna1TXPolarityUpDown.Value = (decimal)myAnt1TXPolarity;
            Antenna2RXPolarityUpDown.Value = (decimal)myAnt2RXPolarity;
            Antenna2TXPolarityUpDown.Value = (decimal)myAnt2TXPolarity;

            // Now figure out if we need to update anything
            if (InTXSequence())
            {
                Antenna1CurrentRequestedPosition = myAnt1TXPolarity;
                Antenna2CurrentRequestedPosition = myAnt2TXPolarity;

                Antenna1RXPolarityUpDown.BackColor = SystemColors.Window;
                Antenna1TXPolarityUpDown.BackColor = Color.HotPink;
                Antenna2RXPolarityUpDown.BackColor = SystemColors.Window;
                Antenna2TXPolarityUpDown.BackColor = Color.HotPink;

                ClocktextBox.BackColor = Color.HotPink;
            }
            else
            {
                Antenna1CurrentRequestedPosition = myAnt1RXPolarity;
                Antenna2CurrentRequestedPosition = myAnt2RXPolarity;

                Antenna1RXPolarityUpDown.BackColor = Color.LimeGreen;
                Antenna1TXPolarityUpDown.BackColor = SystemColors.Window;
                Antenna2RXPolarityUpDown.BackColor = Color.LimeGreen;
                Antenna2TXPolarityUpDown.BackColor = SystemColors.Window;

                ClocktextBox.BackColor = Color.LimeGreen;
            }

            bool SendToRotor = false;

            if (Antenna1CurrentRequestedPosition > Antenna1CurrentReportedPosition)
            {
                if ((Antenna1CurrentRequestedPosition - Antenna1CurrentReportedPosition) >= (double)GlobalVar.SetupForm.RotorIncrementUpDown.Value)
                {
                    // Send the value to the rotor
                    SendToRotor = true;
                }
            }
            else
            {
                if ((Antenna1CurrentReportedPosition - Antenna1CurrentRequestedPosition) >= (double)GlobalVar.SetupForm.RotorIncrementUpDown.Value)
                {
                    // Send the value to the rotor
                    SendToRotor = true;
                }
            }

            if (Antenna2CurrentRequestedPosition > Antenna2CurrentReportedPosition)
            {
                if ((Antenna2CurrentRequestedPosition - Antenna2CurrentReportedPosition) >= (double)GlobalVar.SetupForm.RotorIncrementUpDown.Value)
                {
                    // Send the value to the rotor
                    SendToRotor = true;
                }
            }
            else
            {
                if ((Antenna2CurrentReportedPosition - Antenna2CurrentRequestedPosition) >= (double)GlobalVar.SetupForm.RotorIncrementUpDown.Value)
                {
                    // Send the value to the rotor
                    SendToRotor = true;
                }
            }

            if (SendToRotor && DrivecheckBox.Checked && (!(InRXNoiseScan)))
            {
                SendPositionRequest((ushort)(Antenna1CurrentRequestedPosition + 180), (ushort)(Antenna2CurrentRequestedPosition + 180));
            }
            

            Antenna1CurrentReportedPosition = (double)Antenna1CurrentPosition - 180;
            Antenna2CurrentReportedPosition = (double)Antenna2CurrentPosition - 180;

            // Write the value into the text box
            Antenna1RequesttextBox.Text = Antenna1CurrentRequestedPosition.ToString("N0");
            Antenna2RequesttextBox.Text = Antenna2CurrentRequestedPosition.ToString("N0");
            Antenna1CurrenttextBox.Text = Antenna1CurrentReportedPosition.ToString("N0");
            Antenna2CurrenttextBox.Text = Antenna2CurrentReportedPosition.ToString("N0");
            //CalibrateStatus = false;
            // Set the color of the calibrate button
            if (CalibrateStatus)
            {
                CalibrateButtion.BackColor = Color.HotPink;
            }
            else
            {
                CalibrateButtion.BackColor = SystemColors.ControlDark;
            }

            // set the Comm Status flag
            if (SerialFailCount == 0)
            {
                CommStatuslabel.Text = "Comm Good";
                CommStatuslabel.BackColor = Color.LimeGreen;
            }
            else if (SerialFailCount == 0xFFFF)
            {
                if (MODBUSPort.IsOpen)
                {
                    SerialFailCount = 0;
                }
                else
                {
                    CommStatuslabel.Text = "Select Port";
                    CommStatuslabel.BackColor = Color.HotPink;
                }
            }
            else if (SerialFailCount >= Constants.MAXSERIALFAILCOUNT)
            {
                CommStatuslabel.Text = "Comm Failed";
                CommStatuslabel.BackColor = Color.Yellow;
            }

            // Value and color reciprocal dental button
            double myDeltaNoise = DeltaNoise[(int)Antenna1RXPolarityUpDown.Value + 180];
            NoiseAtReciprocalbutton.Text = myDeltaNoise.ToString("F2");

            if (myDeltaNoise >= 0.0)
            {
                NoiseAtReciprocalbutton.BackColor = Color.LimeGreen;
            }
            else if (myDeltaNoise > -.5)
            {
                NoiseAtReciprocalbutton.BackColor = Color.Yellow;
            }
            else
            {
                NoiseAtReciprocalbutton.BackColor = Color.HotPink;
            }

            SendScanRequests();

            if (AzElserialPort.IsOpen)
            {
                AzElScan();

                if (GlobalVar.AzElForm != null)
                {
                    GlobalVar.AzElForm.AzElCurrentAztextBox.Text = CurrentAzValue.ToString();
                    GlobalVar.AzElForm.AzElCurrentEltextBox.Text = CurrentElValue.ToString();
                }
            }
        }

        struct OutQueueEntry
        {
            public byte[] Message;
            public UInt16 Length;
        };

        ConcurrentQueue<OutQueueEntry> MODBUSOutQueue = new ConcurrentQueue<OutQueueEntry>();

        private byte PLCAddress = 0x01;

        public UInt16 CRC16Calc(byte[] Message, int Length)
        {
            // Calculate and insert CRC16
            UInt16 crc = 0xFFFF;

            for (int CRCIndex = 0; CRCIndex < Length; CRCIndex++)
            {
                // XOR byte into least sig. byte of crc
                crc ^= Message[CRCIndex];

                // Loop over each bit
                for (int i = 8; i != 0; i--)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        // If the LSB is set so hift right and XOR 0xA001
                        crc >>= 1;
                        crc ^= 0xA001;

                    }
                    else
                    {
                        // If LSB is not set just shift right
                        crc >>= 1;
                    }
                }
            }

            return (crc);
        }

        public void SendPositionRequest(ushort Antenna1Position, ushort Antenna2Position)
        {
            try
            {
                byte[] Message = new byte[32];
                ushort Length = 0;
                ushort PositionAddress1 = 0x0000;  // DS1
                ushort PositionAddress2 = 0x0001;  // DS2
                ushort crc;

                // Build a write single register message
                Message[Length++] = PLCAddress;
                Message[Length++] = 0x06;

                // Load the point address
                Message[Length++] = Convert.ToByte((PositionAddress1 >> 8) & 0xFF);
                Message[Length++] = Convert.ToByte(PositionAddress1 & 0xFF);

                // Load Antenna1 Value
                Message[Length++] = Convert.ToByte((Antenna1Position >> 8) & 0xFF);
                Message[Length++] = Convert.ToByte(Antenna1Position & 0xFF);

                // Calc the CRC
                crc = CRC16Calc(Message, Length);

                // Append CRC to message
                Message[Length++] = Convert.ToByte(crc & 0xff);
                Message[Length++] = Convert.ToByte((crc >> 8) & 0xff);

                // Put the message on the queue
                OutQueueEntry MyOutQueueEntry = new OutQueueEntry();
                MyOutQueueEntry.Length = Length;
                MyOutQueueEntry.Message = Message;

                MODBUSOutQueue.Enqueue(MyOutQueueEntry);

                // Now build messaged for Antenna2
                Message = new byte[32];
                Length = 0;

                // Build a write single register message
                Message[Length++] = PLCAddress;
                Message[Length++] = 0x06;

                // Load the point address
                Message[Length++] = Convert.ToByte((PositionAddress2 >> 8) & 0xFF);
                Message[Length++] = Convert.ToByte(PositionAddress2 & 0xFF);

                // Load Antenna2 Value
                Message[Length++] = Convert.ToByte((Antenna2Position >> 8) & 0xFF);
                Message[Length++] = Convert.ToByte(Antenna2Position & 0xFF);

                // Calc the CRC
                crc = CRC16Calc(Message, Length);

                // Append CRC to message
                Message[Length++] = Convert.ToByte(crc & 0xff);
                Message[Length++] = Convert.ToByte((crc >> 8) & 0xff);

                // Put the message on the queue
                MyOutQueueEntry = new OutQueueEntry();
                MyOutQueueEntry.Length = Length;
                MyOutQueueEntry.Message = Message;

                MODBUSOutQueue.Enqueue(MyOutQueueEntry);
            }
            catch
            {
                // Piss and moan code goes here
            }
        }

        void SendCalibrateRequest()
        {
            try
            {
                byte[] Message = new byte[32];
                ushort Length = 0;
                ushort StartAddress = 0x4064;  // C101

                // Build a write single coil message
                Message[Length++] = PLCAddress;
                Message[Length++] = 0x05;

                // Load the point address
                Message[Length++] = Convert.ToByte((StartAddress >> 8) & 0xFF);
                Message[Length++] = Convert.ToByte(StartAddress & 0xFF);

                // Load Antenna2 Value
                Message[Length++] = 0xff;
                Message[Length++] = 0x00;

                // Calc the CRC
                UInt16 crc = CRC16Calc(Message, Length);

                // Append CRC to message
                Message[Length++] = Convert.ToByte(crc & 0xff);
                Message[Length++] = Convert.ToByte((crc >> 8) & 0xff);

                // Put the message on the 
                OutQueueEntry MyOutQueueEntry = new OutQueueEntry();
                MyOutQueueEntry.Length = Length;
                MyOutQueueEntry.Message = Message;

                MODBUSOutQueue.Enqueue(MyOutQueueEntry);
            }
            catch
            {
                // Piss and moan code goes here
            }
        }

        void SendScanRequests()
        {
            try
            {
                byte[] Message = new byte[32];
                ushort Length = 0;
                ushort CalibrateFlageStartAddress = 0x4064;  // C101
                ushort AntennaPositionStartAddress = 0x0064; // DS101
                ushort crc;

                // Build a read holding registers message
                Message[Length++] = PLCAddress;
                Message[Length++] = 0x03;

                // Load Antenna Position Start address
                Message[Length++] = Convert.ToByte((AntennaPositionStartAddress >> 8) & 0xFF);
                Message[Length++] = Convert.ToByte(AntennaPositionStartAddress & 0xFF);

                // Load the number of registers to read
                Message[Length++] = 0x00;
                Message[Length++] = 0x04;

                // Calc the CRC
                crc = CRC16Calc(Message, Length);

                // Append CRC to message
                Message[Length++] = Convert.ToByte(crc & 0xff);
                Message[Length++] = Convert.ToByte((crc >> 8) & 0xff);

                // Put the message on the 
                OutQueueEntry MyOutQueueEntry = new OutQueueEntry();
                MyOutQueueEntry.Length = Length;
                MyOutQueueEntry.Message = Message;

                MODBUSOutQueue.Enqueue(MyOutQueueEntry);

                // Now scan the Calibrate flag
                Message = new byte[32];
                Length = 0;

                // Build a read inputs message
                Message[Length++] = PLCAddress;
                Message[Length++] = 0x02;

                // Load Antenna Position Start address
                Message[Length++] = Convert.ToByte((CalibrateFlageStartAddress >> 8) & 0xFF);
                Message[Length++] = Convert.ToByte(CalibrateFlageStartAddress & 0xFF);

                // Load the number of inputs to read
                Message[Length++] = 0x00;
                Message[Length++] = 0x01;

                // Calc the CRC
                crc = CRC16Calc(Message, Length);

                // Append CRC to message
                Message[Length++] = Convert.ToByte(crc & 0xff);
                Message[Length++] = Convert.ToByte((crc >> 8) & 0xff);

                // Put the message on the 
                MyOutQueueEntry = new OutQueueEntry();
                MyOutQueueEntry.Length = Length;
                MyOutQueueEntry.Message = Message;

                MODBUSOutQueue.Enqueue(MyOutQueueEntry);
            }
            catch
            {
                // Piss and moan code goes here
            }
        }

        private void MODBUSWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] InBuffer = new byte[256];
            int InLength = 0;
            int BytesRead = 0;
            OutQueueEntry MyOutQueueEntry;

            while (true)
            {
                try
                {
                    // Check for any messages on output queue
                    while (MODBUSOutQueue.TryDequeue(out MyOutQueueEntry))
                    {
                        // Determine if receiving fixed or variable length
                        switch (MyOutQueueEntry.Message[1])
                        {
                            case 0x02:
                                InLength = 0x03;
                                break;

                            case 0x03:
                                InLength = 0x03;
                                break;

                            case 0x05:
                                InLength = 0x08;
                                break;

                            case 0x06:
                                InLength = 0x08;
                                break;

                            case 0x0f:
                                InLength = 0x08;
                                break;

                            default:
                                // Should not happen...
                                continue;
                        }

                        // Check if port is open
                        if (MODBUSPort.IsOpen)
                        {
                            // Clear the read buffer
                            MODBUSPort.DiscardInBuffer();

                            // Write this out the port
                            MODBUSPort.Write(MyOutQueueEntry.Message, 0, MyOutQueueEntry.Length);

                            BytesRead = 0;

                            while (BytesRead < InLength)
                            {
                                BytesRead += MODBUSPort.Read(InBuffer, BytesRead, InLength - BytesRead);
                            }

                            // Based pm message type figure out if we need more bytes
                            switch (InBuffer[1])
                            {
                                case 0x02:
                                case 0x03:
                                    InLength += InBuffer[2] + 2;
                                    break;

                                default:
                                    break;
                            }

                            while (BytesRead < InLength)
                            {
                                BytesRead += MODBUSPort.Read(InBuffer, BytesRead, InLength - BytesRead);
                            }

                            if ((InBuffer[1] & 0x80) != 0x80)
                            {
                                // Check a few things...
                                if ((MyOutQueueEntry.Message[0] == InBuffer[0]) &&
                                    (MyOutQueueEntry.Message[1] == InBuffer[1]) &&
                                    (CRC16Calc(InBuffer, BytesRead) == 0X0000))
                                {
                                    // Process based on type
                                    switch (InBuffer[1])
                                    {
                                        case 0x02:
                                            if ((InBuffer[3] & 0x01) == 0x01)
                                            {
                                                CalibrateStatus = true;
                                            }
                                            else
                                            {
                                                CalibrateStatus = false;
                                            }

                                            break;

                                        case 0x03:
                                            // Ignore the byte count
                                            Antenna1CurrentPosition = (ushort)((InBuffer[3] << 8) | InBuffer[4]);
                                            Antenna2CurrentPosition = (ushort)((InBuffer[5] << 8) | InBuffer[6]);
                                            break;

                                        case 0x05:
                                        case 0x06:
                                        case 0x0f:
                                            break;

                                        default:
                                            break;
                                    }

                                    SerialFailCount = 0;
                                }
                                else
                                {
                                    // Scan failed somehow
                                    if (SerialFailCount < Constants.MAXSERIALFAILCOUNT)
                                    {
                                        SerialFailCount += 1;
                                    }
                                }
                            }
                            else
                            {
                                // Scan failed somehow
                                if (SerialFailCount < Constants.MAXSERIALFAILCOUNT)
                                {
                                    SerialFailCount += 1;
                                }
                            }
                        }
                        else
                        {
                            SerialFailCount = 0xFFFF;
                        }
                        Thread.Sleep(100);
                    }
                    // Do not allow thread to be in a tight loop when nothting on queue
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    // Something went wrong
                    // Scan failed somehow
                    if (SerialFailCount < Constants.MAXSERIALFAILCOUNT)
                    {
                        SerialFailCount += 1;
                    }
                    else
                    {
                        MessageBox.Show(ex.ToString() + "\r\r Check that the correct Polarity port is assigned.");
                        GlobalVar.SetupForm.SerialPortComboBox.SelectedIndex = -1;
                    }
                }
            }
        }

        private void DrivecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DrivecheckBox.Checked)
            {
                DrivecheckBox.BackColor = Color.LimeGreen;
            }
            else
            {
                DrivecheckBox.BackColor = Color.Yellow;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 myAboutBox = new AboutBox1();

            myAboutBox.labelVersion.Text = "Version:  " + Version;

            myAboutBox.Show();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Close the port
            MODBUSPort.Close();

            // Exit the process
            System.Windows.Forms.Application.Exit();
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVar.SetupForm.Show();

        }

        private void ReadAzElFileTimer_Tick(object sender, EventArgs e)
        {

        }

        private void Resetbutton_Click(object sender, EventArgs e)
        {
            DrivecheckBox.Checked = false;

            Antenna1RXPolarityUpDown.Value = 0;
            Antenna1RXRateUpDown.Value = 0;
            Antenna1RXOffsetUpDown.Value = 0;
            Antenna1TXPolarityUpDown.Value = 0;
            Antenna1TXOffsetUpDown.Value = 0;
            Antenna1TXRateUpDown.Value = 0;
            Antenna1RXNoneradioButton.Checked = true;
            Antenna1TXeqRXradioButton.Checked = true;
            NoiseAtReciprocalbutton.Enabled = true;

            Antenna2RXPolarityUpDown.Value = 0;
            Antenna2RXOffsetUpDown.Value = 0;
            Antenna2RXRateUpDown.Value = 0;
            Antenna2TXPolarityUpDown.Value = 0;
            Antenna2TXOffsetUpDown.Value = 0;
            Antenna2TXRateUpDown.Value = 0;
            Antenna2TrackOffsetUpDown.Value = 0;
            Antenna2RXNoneradioButton.Checked = true;
            Antenna2TXNoneradioButton.Checked = true;
            Antenna2TrackAntenna1checkBox.Checked = true;
        }

        private void CQStartUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CQStart = CQStartUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void CQIncrementUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CQIncrement = CQIncrementUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void CQStepsUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CQSteps = CQStepsUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void CQRepeatUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.CQRepeat = CQRepeatUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void ScanStartUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ScanStart = ScanStartUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void ScanIncrementUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ScanIncrement = ScanIncrementUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void ScanStepsUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ScanSteps = ScanStepsUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void ScanRepeatUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ScanRepeat = ScanRepeatUpDown.Value;
            Properties.Settings.Default.Save();
        }

        private void OptimizeTXcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.OptimizeTX = OptimizeTXcheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void OptimizeRXcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.OptimizeRX = OptimizeRXcheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void Antenna1RXPolarityUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                if (!(OptimizeRXcheckBox.Checked))
                {
                    if (Antenna1RXPolarityUpDown.Value > 0)
                    {
                        Antenna1RXPolarityUpDown.Value -= 180;
                    }
                    else if (Antenna1RXPolarityUpDown.Value < 0)
                    {
                        Antenna1RXPolarityUpDown.Value += 180;
                    }
                }
            }
        }

        private void Antenna1TXPolarityUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                if (!(OptimizeTXcheckBox.Checked))
                {
                    if (Antenna1TXPolarityUpDown.Value > 0)
                    {
                        Antenna1TXPolarityUpDown.Value -= 180;
                    }
                    else if (Antenna1TXPolarityUpDown.Value < 0)
                    {
                        Antenna1TXPolarityUpDown.Value += 180;
                    }
                }
            }
        }

        private void Antenna2RXPolarityUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                if (!(OptimizeRXcheckBox.Checked))
                {
                    if (Antenna2RXPolarityUpDown.Value > 0)
                    {
                        Antenna2RXPolarityUpDown.Value -= 180;
                    }
                    else if (Antenna2RXPolarityUpDown.Value < 0)
                    {
                        Antenna2RXPolarityUpDown.Value += 180;
                    }
                }
            }
        }

        private void Antenna2TXPolarityUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                if (!(OptimizeTXcheckBox.Checked))
                {
                    if (Antenna2TXPolarityUpDown.Value > 0)
                    {
                        Antenna2TXPolarityUpDown.Value -= 180;
                    }
                    else if (Antenna2TXPolarityUpDown.Value < 0)
                    {
                        Antenna2TXPolarityUpDown.Value += 180;
                    }
                }
            }
        }

        public NoiseScanform myNoiseScanForm;
        private void noiseScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myNoiseScanForm != null)
            {
                if (myNoiseScanForm.WindowState == FormWindowState.Minimized)
                {
                    myNoiseScanForm.WindowState = FormWindowState.Normal;
                }
                else
                {
                    myNoiseScanForm.BringToFront();
                }
            }
            else
            {
                myNoiseScanForm = new NoiseScanform();
                myNoiseScanForm.Show();
            }
        }

        private void NoiseAtReciprocalbutton_MouseClick(object sender, MouseEventArgs e)
        {
            if (Antenna1RXPolarityUpDown.Value > 0)
            {
                Antenna1RXPolarityUpDown.Value -= 180;
            }
            else if (Antenna1RXPolarityUpDown.Value < 0)
            {
                Antenna1RXPolarityUpDown.Value += 180;
            }
            else
            {
                Antenna1RXPolarityUpDown.Value = 179.999M;
            }
        }

        private void AntennaPolarityControl_Load(object sender, EventArgs e)
        {
            this.Text += "     Version: " + Version;
        }

        private void AzElfileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            bool IsValid = false;

            try
            {
                // Build the file name
                string myString = GlobalVar.SetupForm.AzELFileDirectorytextBox.Text;

                // Check if there is a trailing slash
                if (myString.Substring(myString.Length - 1, 1) != "\\")
                {
                    myString += "\\";
                }

                // Append the file name
                myString += "AzEl.dat";

                var fs = new FileStream(myString, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs);

                try
                {
                    while (!(sr.EndOfStream))
                    {
                        string Line = sr.ReadLine();

                        if (Line.Contains("RPol"))
                        {
                            // close the file right away
                            sr.Close();

                            // for now make it valid
                            IsValid = true;

                            // Process the sequence
                            if (Line.Substring(0, 1) == "0")
                            {
                                SequenceTXFirstcheckBox.Checked = false;
                            }
                            else if (Line.Substring(0, 1) == "1")
                            {
                                SequenceTXFirstcheckBox.Checked = true;
                            }
                            else
                            {
                                IsValid = false;
                            }

                            // Now process the sequence
                            if (Line.Substring(3, 2) == "30")
                            {
                                Sequence30radioButton.Checked = true;
                            }
                            else if (Line.Substring(3, 2) == "60")
                            {
                                Sequence60radioButton.Checked = true;
                            }
                            else
                            {
                                IsValid = false;
                            }

                            // Get DPol
                            DPOLUpDown.Value = Convert.ToDecimal(Line.Substring(6, 8));

                            // Get the degradation
                            DGRtextBox.Text = Line.Substring(15, 8).Trim(); ;

                            // Get the MAX NR
                            MAXNRtextBox.Text = Line.Substring(24, 8).Trim();

                            // Eheck if we are in RX Mode
                            if (Line.Substring(59, 1) == "1")
                            {
                                // Get average Noise Value
                                double AverageNoise = Convert.ToDouble(Line.Substring(33, 12));

                                // Get rms Noise Value
                                double RMSNoise = Convert.ToDouble(Line.Substring(46, 12));

                                RMSNoise += 0.001;

                                RXNoise = 20.0 * Math.Log10((RMSNoise / 770) + 0.01);

                                RXNoisetextBox.Text = RXNoise.ToString("F1");

                                break;
                            }
                        }
                        else if (GlobalVar.AzElForm != null)
                        {
                            if (GlobalVar.AzElForm.AzElMoonradioButton.Checked)
                            {
                                if (Line.Contains("Moon"))
                                {
                                    AzElUpdate(Line);
                                }
                            }
                            else if (GlobalVar.AzElForm.AzElSunradioButton.Checked)
                            {
                                if (Line.Contains("Sun"))
                                {
                                    AzElUpdate(Line);
                                }
                            }
                            else if (GlobalVar.AzElForm.AzElSourceradioButton.Checked)
                            {
                                if (Line.Contains("Source"))
                                {
                                    AzElUpdate(Line);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    IsValid = false;
                }

                try
                {
                    sr.Close();
                }
                catch
                {

                }
            }
            catch
            {
                IsValid = false;
            }

            if (IsValid)
            {
                Sequencinglabel.BackColor = Color.LightGreen;
            }
            else
            {
                Sequencinglabel.BackColor = Color.Yellow;
            }
        }

        private void azElToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVar.AzElForm == null)
            {
                GlobalVar.AzElForm = new AzElform();
                GlobalVar.AzElForm.Show();
            }
        }

        ConcurrentQueue<OutQueueEntry> AzElOutQueue = new ConcurrentQueue<OutQueueEntry>();
        bool OldDrive = false;

        private void AzElUpdate(string Line)
        {
            bool Changed = false;

            try
            {
                if (!GlobalVar.AzElForm.chkManual.Checked)
                {
                    double azRound = System.Math.Round(Convert.ToDouble(Line.Substring(9, 5)));
                    double elRound = System.Math.Round(Convert.ToDouble(Line.Substring(15, 5)));

                    //if (GlobalVar.AzElForm.AzElRequestAztextBox.Text != Line.Substring(9, 5))

                    if (GlobalVar.AzElForm.AzElRequestAztextBox.Text != azRound.ToString())
                    {
                        GlobalVar.AzElForm.AzElRequestAztextBox.Text = azRound.ToString();
                        //GlobalVar.AzElForm.AzElRequestAztextBox.Text = Line.Substring(9, 5));
                        Changed = true;
                    }

                    //if (GlobalVar.AzElForm.AzElRequestEltextBox.Text != Line.Substring(15, 5))

                    if (GlobalVar.AzElForm.AzElRequestEltextBox.Text != elRound.ToString())
                    {
                        GlobalVar.AzElForm.AzElRequestEltextBox.Text = elRound.ToString();
                        //GlobalVar.AzElForm.AzElRequestEltextBox.Text = Line.Substring(15, 5);
                        Changed = true;
                    }
                    if (GlobalVar.AzElForm.AzElDrivecheckBox.Checked && !(OldDrive))
                    {
                        Changed = true;
                    }

                    OldDrive = GlobalVar.AzElForm.AzElDrivecheckBox.Checked;

                    // check if we send it to the rotor
                    if (GlobalVar.AzElForm.AzElDrivecheckBox.Checked && Changed)
                    {
                        switch (GlobalVar.SetupForm.AzElRotorTypecomboBox.Text)
                        {
                            case "AlphaSpid RAS":
                                byte[] Message = new byte[32];
                                UInt16 Length = 0;

                                // convert the azimuth to double
                                decimal Az = Convert.ToDecimal(GlobalVar.AzElForm.AzElRequestAztextBox.Text);


                                // Make sure it is constrainted 
                                if (Az >= 0 && Az <= 360)
                                {
                                    Message[Length++] = 0x57;
                                    Az += 360;
                                    string myAz = ((int)(Az)).ToString("D3");
                                    Message[Length++] = (byte)Convert.ToChar(myAz.Substring(0, 1));
                                    Message[Length++] = (byte)Convert.ToChar(myAz.Substring(1, 1));
                                    Message[Length++] = (byte)Convert.ToChar(myAz.Substring(2, 1));
                                    Message[Length++] = 0x30;
                                    Message[Length++] = 0x00;

                                    // convert the elevation to double
                                    decimal El = Convert.ToDecimal(GlobalVar.AzElForm.AzElRequestEltextBox.Text) * 10;
                                    if (El < 0)
                                    {
                                        El = 0;
                                    }
                                    else if (El > 900)
                                    {
                                        El = 900;
                                    }

                                    El += 360;
                                    string myEl = ((int)El).ToString("D4");
                                    Message[Length++] = (byte)Convert.ToChar(myEl.Substring(0, 1));
                                    Message[Length++] = (byte)Convert.ToChar(myEl.Substring(1, 1));
                                    Message[Length++] = (byte)Convert.ToChar(myEl.Substring(2, 1));
                                    Message[Length++] = (byte)Convert.ToChar(myEl.Substring(3, 1));
                                    Message[Length++] = 0x00;

                                    Message[Length++] = 0x2F;
                                    Message[Length++] = 0x20;

                                    OutQueueEntry myOutQueueEntry = new OutQueueEntry();
                                    myOutQueueEntry.Message = Message;
                                    myOutQueueEntry.Length = Length;

                                    AzElOutQueue.Enqueue(myOutQueueEntry);
                                }

                                break;

                            case "Yaesu GS232B":
                                try
                                {
                                    Message = new byte[9];
                                    Length = 0;

                                    // convert the azimuth to double
                                    Az = Convert.ToDecimal(GlobalVar.AzElForm.AzElRequestAztextBox.Text);

                                    // Make sure it is constrainted 
                                    if (Az < Properties.Settings.Default.MinAz)
                                        Az = Properties.Settings.Default.MinAz;

                                    else if (Az > Properties.Settings.Default.MaxAz)
                                        Az = Properties.Settings.Default.MaxAz;

                                    //Adjust Az for rotor offset
                                    Az = Az + Properties.Settings.Default.AzOffset;

                                    {
                                        string myAz = ((int)(Az)).ToString("D3");
                                        Message[Length++] = 0x57;   // "W"
                                        Message[Length++] = (byte)Convert.ToChar(myAz.Substring(0, 1));
                                        Message[Length++] = (byte)Convert.ToChar(myAz.Substring(1, 1));
                                        Message[Length++] = (byte)Convert.ToChar(myAz.Substring(2, 1));
                                        Message[Length++] = 0x20;

                                        decimal El = Convert.ToDecimal(GlobalVar.AzElForm.AzElRequestEltextBox.Text);

                                        if (El < Properties.Settings.Default.MinElev)
                                            El = Properties.Settings.Default.MinElev;

                                        else if (El > Properties.Settings.Default.MaxElev)
                                            El = Properties.Settings.Default.MaxElev;

                                        //Adjust Az for rotor offset
                                        El = El + Properties.Settings.Default.ElOffset;

                                        string myEl = ((int)El).ToString("D3");
                                        Message[Length++] = (byte)Convert.ToChar(myEl.Substring(0, 1));
                                        Message[Length++] = (byte)Convert.ToChar(myEl.Substring(1, 1));
                                        Message[Length++] = (byte)Convert.ToChar(myEl.Substring(2, 1));
                                        Message[Length++] = 0x0D;

                                        OutQueueEntry myOutQueueEntry = new OutQueueEntry();
                                        myOutQueueEntry.Message = Message;
                                        myOutQueueEntry.Length = Length;

                                        AzElOutQueue.Enqueue(myOutQueueEntry);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                                }
                                break;

                            case "Idiom Press":

                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        ASCIIEncoding AE;
        // Send queued command to rotor and wait for reply
        private void AzElWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            OutQueueEntry myOutQueueEntry;

            while (true)
            {
                while (AzElOutQueue.TryDequeue(out myOutQueueEntry))
                {
                    if (AzElserialPort.IsOpen)
                    {
                        switch (GlobalVar.SetupForm.AzElRotorTypecomboBox.Text)
                        {
                            case "AlphaSpid RAS":
                                try
                                {
                                    if (myOutQueueEntry.Length == 13)
                                    {
                                        byte[] InBuffer = new byte[12];
                                        int ReturnLength = 0;
                                        int BytesReturned = 0;

                                        // calculate the return length
                                        switch (myOutQueueEntry.Message[11])
                                        {
                                            case 0x0f:
                                                ReturnLength = 12;
                                                break;

                                            case 0x1f:
                                                ReturnLength = 12;
                                                break;

                                            case 0x2f:
                                                ReturnLength = 0;
                                                break;
                                        }

                                        AzElserialPort.DiscardInBuffer();
                                        AzElserialPort.Write(myOutQueueEntry.Message, 0, myOutQueueEntry.Length);

                                        while (BytesReturned < ReturnLength)
                                        {
                                            BytesReturned += AzElserialPort.Read(InBuffer, BytesReturned, ReturnLength - BytesReturned);
                                        }

                                        switch (myOutQueueEntry.Message[11])
                                        {
                                            case 0x0f:
                                                break;

                                            case 0x1f:
                                                if (GlobalVar.AzElForm != null)
                                                {
                                                    CurrentAzValue = (decimal)((InBuffer[1] * 100 + 
                                                        InBuffer[2] * 10 + InBuffer[3] + InBuffer[4] * .1) - 360);
                                                    CurrentElValue = (decimal)((InBuffer[6] * 100 + 
                                                        InBuffer[7] * 10 + InBuffer[8] + InBuffer[9] * .1) - 360);
                                                }

                                                break;

                                            case 0x2f:
                                                Thread.Sleep(500);
                                                break;
                                        }
                                    }
                                }
                                catch
                                {

                                }

                                break;
                            case "Yaesu GS232B":
                                try
                                {
                                    AE = new ASCIIEncoding();
                                    if (myOutQueueEntry.Length == 3) // if command  = C2
                                    {
                                        byte[] InBuffer = new byte[17];
                                        int ReturnLength = 16;
                                        int BytesReturned = 0;
                                        string sRtrBuf = "";

                                        AzElserialPort.DiscardInBuffer();
                                        AzElserialPort.Write(myOutQueueEntry.Message, 0, myOutQueueEntry.Length);

                                        while (BytesReturned < ReturnLength)
                                        {
                                            BytesReturned += AzElserialPort.Read(InBuffer, BytesReturned, ReturnLength - BytesReturned);
                                        }
                                        
                                        sRtrBuf += AE.GetString(InBuffer, 0, InBuffer.Length);
                                        switch (myOutQueueEntry.Message[0])
                                        {
                                            case 0x43:  // "C"
                                                if (GlobalVar.AzElForm != null)
                                                {
                                                    //String[] rEl = Regex.Split(sRtrBuf, @"+");
                                                    CurrentAzValue = Convert.ToDecimal(sRtrBuf.Substring(3,3));
                                                    CurrentElValue = Convert.ToDecimal(sRtrBuf.Substring(7, 3));
                                                    //Console.Write(CurrentElValue);
                                                }
                                                break;

                                            default:
                                                Thread.Sleep(500);
                                                break;
                                        }
                                    }
                                    if (myOutQueueEntry.Length == 9) // if command  = rotate (Wnnn nnn0x0d)
                                        AzElserialPort.Write(myOutQueueEntry.Message, 0, myOutQueueEntry.Length);
                                }
                                catch (Exception ex)
                                {
                                    //System.Windows.Forms.MessageBox.Show(ex.ToString());
                                }
                                break;

                            case "Idiom Press":

                                break;
                        }
                    }
                }
            }
        }

        // Poll the rotor for status
        private void AzElScan()
        {
            OutQueueEntry myOutQueueEntry = new OutQueueEntry();
            ushort Length = 0;

            switch (GlobalVar.SetupForm.AzElRotorTypecomboBox.Text)
            {
                case "AlphaSpid RAS":
                    byte[] aMessage = new byte[13];
                    aMessage[Length++] = 0x57;
                    aMessage[Length++] = 0x00;
                    aMessage[Length++] = 0x00;
                    aMessage[Length++] = 0x00;
                    aMessage[Length++] = 0x00;
                    aMessage[Length++] = 0x00;
                    aMessage[Length++] = 0x00;
                    aMessage[Length++] = 0x00;
                    aMessage[Length++] = 0x00;
                    aMessage[Length++] = 0x00;
                    aMessage[Length++] = 0x00;
                    aMessage[Length++] = 0x1f;
                    aMessage[Length++] = 0x20;

                    myOutQueueEntry.Message = aMessage;
                    myOutQueueEntry.Length = Length;
                    break;

                case "Yaesu GS232B":    // C2
                    byte[] yMessage = new byte[3];
                    yMessage[Length++] = 0x43;
                    yMessage[Length++] = 0x32;
                    yMessage[Length++] = 0x0D;

                    myOutQueueEntry.Message = yMessage;
                    myOutQueueEntry.Length = Length;

                    break;

                case "Idiom Press":

                    break;
            }

            AzElOutQueue.Enqueue(myOutQueueEntry);
        }
                
    }   
}


 



