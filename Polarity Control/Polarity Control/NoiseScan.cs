using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Graph = System.Windows.Forms.DataVisualization.Charting;

namespace Polarity_Control
{
    public partial class NoiseScanform : Form
    {
        Graph.Chart RXNoisechart;

        public NoiseScanform()
        {
            InitializeComponent();

       }

        private void Form3_Load(object sender, EventArgs e)
        {
            RXNoisechart = new Graph.Chart();
            RXNoisechart.Location = new System.Drawing.Point(10, 10);
            RXNoisechart.Size = new System.Drawing.Size(500, 400);
  

            RXNoisechart.ChartAreas.Add("draw");
            RXNoisechart.ChartAreas["draw"].AxisX.Minimum = -180;
            RXNoisechart.ChartAreas["draw"].AxisX.Maximum = +180;
            RXNoisechart.ChartAreas["draw"].AxisX.Interval = 45;
            RXNoisechart.ChartAreas["draw"].AxisX.MajorGrid.LineColor = Color.Black;
            RXNoisechart.ChartAreas["draw"].AxisX.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Solid;
            RXNoisechart.ChartAreas["draw"].AxisY.Minimum = -5;
            RXNoisechart.ChartAreas["draw"].AxisY.Maximum = 5;
            RXNoisechart.ChartAreas["draw"].AxisY.Interval = 1;
            RXNoisechart.ChartAreas["draw"].AxisY.MajorGrid.LineColor = Color.Black;
            RXNoisechart.ChartAreas["draw"].AxisY.MajorGrid.LineDashStyle = Graph.ChartDashStyle.Solid;
            RXNoisechart.ChartAreas["draw"].BackColor = Color.White;

            RXNoisechart.Series.Add("Noise");

            RXNoisechart.Series["Noise"].ChartType = Graph.SeriesChartType.Line;
            RXNoisechart.Series["Noise"].Color = Color.Red;
            RXNoisechart.Series["Noise"].BorderWidth = 2;
            RXNoisechart.Series["Noise"].LegendText = "Noise";
            RXNoisechart.Series["Noise"].IsVisibleInLegend = true;

            RXNoisechart.Series.Add("Previous");

            RXNoisechart.Series["Previous"].ChartType = Graph.SeriesChartType.Line;
            RXNoisechart.Series["Previous"].Color = Color.Green;
            RXNoisechart.Series["Previous"].BorderWidth = 2;
            RXNoisechart.Series["Previous"].LegendText = "Old";
            RXNoisechart.Series["Previous"].IsVisibleInLegend = true;
            RXNoisechart.Series["Previous"].Enabled = false;


            RXNoisechart.Series.Add("Delta");

            RXNoisechart.Series["Delta"].ChartType = Graph.SeriesChartType.Line;
            RXNoisechart.Series["Delta"].Color = Color.Blue;
            RXNoisechart.Series["Delta"].BorderWidth = 2;
            RXNoisechart.Series["Delta"].LegendText = "Delta";
            RXNoisechart.Series["Delta"].IsVisibleInLegend = true;
            RXNoisechart.Series["Delta"].Enabled = false;


            RXNoisechart.Series.Add("Average");

            RXNoisechart.Series["Average"].ChartType = Graph.SeriesChartType.Line;
            RXNoisechart.Series["Average"].Color = Color.Magenta;
            RXNoisechart.Series["Average"].BorderWidth = 2;
            RXNoisechart.Series["Average"].LegendText = "Delta";
            RXNoisechart.Series["Average"].IsVisibleInLegend = true;
            RXNoisechart.Series["Average"].Enabled = false;


            // Walk through and add the chart elements so they are there to update
            for (PolarityCount = 0; PolarityCount <= 60; PolarityCount++)
            {
                RXNoisechart.Series["Noise"].Points.AddXY((double)((PolarityCount * 6) - 180), 0.0);
                RXNoisechart.Series["Previous"].Points.AddXY((double)((PolarityCount * 6 - 180)), 0.0);
                RXNoisechart.Series["Delta"].Points.AddXY((double)((PolarityCount * 6 - 180)), 0.0);
                if(PolarityCount >= 30)
                {
                    RXNoisechart.Series["Average"].Points.AddXY((double)((PolarityCount * 6 - 180)), 0.0);
                }
            }

            Controls.Add(this.RXNoisechart);

            NoiseScanAveragecheckBox.Checked = Properties.Settings.Default.NoiseScanAverage;
            NoiseScanDeltacheckBox.Checked = Properties.Settings.Default.NoiseScanDelta;
            NoiseScanPreviouscheckBox.Checked = Properties.Settings.Default.NoiseScanPrevious;
        }


        enum RXSCANHSTATE
        {
            DONE,
            GOTOZERO,
            WAITFORZERO,
            SCANNING,
            WAITFOR360
        }

        private RXSCANHSTATE RxScanState = RXSCANHSTATE.DONE;
        private int RxScanCount;
        private int PolarityCount;
        private double AverageMin;


        private void timer1_Tick(object sender, EventArgs e)
        {

           switch (RxScanState)
            {
                case RXSCANHSTATE.DONE:
                    // Start the rotors run to zero (-180)
                    GlobalVar.Main.SendPositionRequest(0, 0);

                    RxScanCount = 0;

                    RxScanState = RXSCANHSTATE.GOTOZERO;

                    break;

                case RXSCANHSTATE.GOTOZERO:
                    if (GlobalVar.Main.Antenna1CurrentPosition < 2)
                    {
                        RxScanCount = 0;
                        RxScanState = RXSCANHSTATE.WAITFORZERO;
                    }
                    else
                    {
                        RxScanCount++;
                        if (RxScanCount > 61)
                        {
                            RxScanState = RXSCANHSTATE.WAITFOR360;
                        }
                    }

                    break;

               case RXSCANHSTATE.WAITFORZERO:
                    RxScanCount++;
                    if (RxScanCount > 2)
                    {
                        PolarityCount = 0;
                        AverageMin = 5.0;
                        GlobalVar.Main.SendPositionRequest(360, 360);

                        RXNoisechart.Series["Previous"].Points[PolarityCount] = RXNoisechart.Series["Noise"].Points[PolarityCount];
                        RXNoisechart.Series["Noise"].Points[PolarityCount] = new Graph.DataPoint((PolarityCount * 6) - 180, GlobalVar.Main.RXNoise);
                        RXNoisechart.Update();
                        RXNoiseAverageAnglelabel.Text = "000";
                        RXNoiseAverageMinlabel.Text = "0.00";

                        RxScanState = RXSCANHSTATE.SCANNING;
                    }

                    break;


                case RXSCANHSTATE.SCANNING:
                    PolarityCount++;

                    if (PolarityCount > 60)
                    {
                        RxScanCount = 0;
                        RxScanState = RXSCANHSTATE.WAITFOR360;
                    }
                    else
                    {
                        RXNoisechart.Series["Previous"].Points[PolarityCount] = RXNoisechart.Series["Noise"].Points[PolarityCount];
                        RXNoisechart.Series["Noise"].Points[PolarityCount] = new Graph.DataPoint((double)((PolarityCount * 6) - 180), GlobalVar.Main.RXNoise);
                        if (PolarityCount >= 30)
                        {
                            double LowerValue = (double)RXNoisechart.Series["Noise"].Points[PolarityCount - 30].YValues.GetValue(0);
                            double UpperValue = (double)RXNoisechart.Series["Noise"].Points[PolarityCount].YValues.GetValue(0);

                            // Calculate the lower Delta value...
                            RXNoisechart.Series["Delta"].Points[PolarityCount - 30] = new Graph.DataPoint(((PolarityCount - 30) * 6.0) - 180, LowerValue - UpperValue);

                            // Calculate the upper Delta Value
                            RXNoisechart.Series["Delta"].Points[PolarityCount] = new Graph.DataPoint((PolarityCount * 6.0) - 180, UpperValue - LowerValue);

                            if (PolarityCount > 30)
                            {
                                // Calculate the exterpolated differential
                                double LowerBase = (double)RXNoisechart.Series["Delta"].Points[PolarityCount - 31].YValues.GetValue(0);
                                double LowerNext = (double)RXNoisechart.Series["Delta"].Points[PolarityCount - 30].YValues.GetValue(0);
                                double LowerStep = (LowerNext - LowerBase) / 6.0;
                                double UpperBase = (double)RXNoisechart.Series["Delta"].Points[PolarityCount - 1].YValues.GetValue(0);
                                double UpperNext = (double)RXNoisechart.Series["Delta"].Points[PolarityCount].YValues.GetValue(0);
                                double UpperStep = (UpperNext - UpperBase) / 6.0;

                                for (int i = 0; i < 6; i++)
                                {
                                    GlobalVar.Main.DeltaNoise[((PolarityCount - 31) * 6) + i] = LowerBase + (LowerStep * i);
                                    GlobalVar.Main.DeltaNoise[((PolarityCount - 1) * 6) + i] = UpperBase + (UpperStep * i);
                                }
                            }


                            // Calculate the average
                            double Average = 0.0;

                            for (int myPolarityCount = PolarityCount - 30; myPolarityCount <= PolarityCount; myPolarityCount++)
                            {
                                Average += (double)RXNoisechart.Series["Noise"].Points[myPolarityCount].YValues.GetValue(0);
                            }

                            Average /= 30.0;

                            RXNoisechart.Series["Average"].Points[PolarityCount - 30] = new Graph.DataPoint((PolarityCount * 6) - 180, Average);

                            if (AverageMin > Average)
                            {
                                AverageMin = Average;
                                RXNoiseAverageMinlabel.Text = Average.ToString("F2");
                                RXNoiseAverageAnglelabel.Text = Convert.ToString((PolarityCount * 6) - 180);
                            }
                        }
                        
                        RXNoisechart.Update();
                    }
 
                    break;

               case RXSCANHSTATE.WAITFOR360:
                    RxScanCount++;

                    if (RxScanCount > 2)
                    {
                        NoiseScantimer.Enabled = false;
                        GlobalVar.Main.InRXNoiseScan = false;
                        RxScanCount = 0;
                        RxScanState = RXSCANHSTATE.DONE;
                        ScanRXNoisebutton.BackColor = SystemColors.Control;
                    }

                   break;
            }
        }

        private void ScanRXNoisebutton_Click(object sender, EventArgs e)
        {
            if (GlobalVar.Main.DrivecheckBox.Checked)
            {
                // Do not allow restart
                if (!(GlobalVar.Main.InRXNoiseScan))
                {
                    if (!(GlobalVar.Main.CalibrateStatus))
                    {
                        GlobalVar.Main.InRXNoiseScan = true;

                        // Seed the process
                        RxScanState = RXSCANHSTATE.DONE;

                        // Start the timer
                        NoiseScantimer.Enabled = true;

                        ScanRXNoisebutton.BackColor = Color.LimeGreen;
                    }
                }
            }
        }

        private void NoiseScanform_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalVar.Main.InRXNoiseScan = false;
            GlobalVar.Main.myNoiseScanForm = null;
        }

        private void RXNoiseAverageAnglelabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            decimal myRXOptimzeStart = Convert.ToDecimal(RXNoiseAverageAnglelabel.Text);

            if (myRXOptimzeStart >= 0 && myRXOptimzeStart <= 180)
            {
                GlobalVar.SetupForm.OptimizeStartUpDown.Value = myRXOptimzeStart;
            }
        }

        private void NoiseScanAveragecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NoiseScanAveragecheckBox.Checked)
            {
                RXNoisechart.Series["Average"].Enabled = true;
            }
            else
            {
                RXNoisechart.Series["Average"].Enabled = false;
            }

            RXNoisechart.Update();

            Properties.Settings.Default.NoiseScanAverage = NoiseScanAveragecheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void NoiseScanDeltacheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NoiseScanDeltacheckBox.Checked)
            {
                RXNoisechart.Series["Delta"].Enabled = true;
            }
            else
            {
                RXNoisechart.Series["Delta"].Enabled = false;
            }

            RXNoisechart.Update();

            Properties.Settings.Default.NoiseScanDelta = NoiseScanDeltacheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void NoiseScanPreviouscheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NoiseScanPreviouscheckBox.Checked)
            {
                RXNoisechart.Series["Previous"].Enabled = true;
            }
            else
            {
                RXNoisechart.Series["Previous"].Enabled = false;
            }

            RXNoisechart.Update();

            Properties.Settings.Default.NoiseScanPrevious = NoiseScanPreviouscheckBox.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
