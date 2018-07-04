using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using atpLib.Scenarios;
using atpLib.Devices;
using System.IO;
using mfe_gui.Messages;
using Newtonsoft.Json;
using System.Reflection;
using atpLib.CRC;
using mfe_gui.Scenarios;
using static mfe_gui.Scenarios.CommTestScenario;

namespace mfe_gui
{
    public partial class frmMain : Form
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        RFC1662BinarySerialDevice device;
        CRC8 crc = new CRC8();

        string[] generalCalibNames;

        public frmMain()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
        }

        private void Grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var grid = sender as DataGridView;
            UInt16 n;
            if (e.ColumnIndex > 0 && 
                (string.IsNullOrEmpty(e.FormattedValue.ToString()) || UInt16.TryParse(e.FormattedValue.ToString(), out n) == false))
            {
                grid.Rows[e.RowIndex].ErrorText = "The value must be an unsigned 16 bit number!";
                e.Cancel = true;
            }
            else
            {
                grid.Rows[e.RowIndex].ErrorText = "";
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            /* print the program and atplib versions */
            this.Text += " - " + typeof(frmMain).Assembly.GetName().Version;
            log.Info("Statring Program, Version - " + typeof(frmMain).Assembly.GetName().Version +
                ", AtpLib - " + typeof(atpLib.Devices.Device).Assembly.GetName().Version);


            /* load default comport */
            loadDefaultcomPort();

            generalCalibNames = new string[] {
                "CAL_TBL_VERSION",
                "SERIAL_NUM",
                "TEMP_MULT",
                "FWD_MULT",
                "REV_TRESH",
                "INP_PWR_MULT",
                "PWR_CURRENT_MULT",
                "PRE_AMP_MULT",
                "ISENSE_PA1_MULT",
                "ISENSE_PA2_MULT",
                "BOOT_WAIT_TIME_USEC",
                "TX_ON_TIMING_USEC",
                "TX_OFF_TIMING_USEC",
                "PA_ON_TIMING_USEC",
                "PA_OFF_TIMING_USEC",
                "ANT_SEL_TIMING_USEC",
                "FWD_SAMP_TIMING_USEC",
                "REV_SAMP_TIMING_USEC",
                "INP_PWR_SAMP_TIMING_USEC",
            };

            gridGeneralCalib.Columns.Add("Key", "Parameter #");
            gridGeneralCalib.Columns.Add("Value", "Value");
            gridGeneralCalib.Columns["Key"].ReadOnly = true;

            foreach (string s in generalCalibNames)
            {
                gridGeneralCalib.Rows.Add(s, "0");
            }

            gridLowCalib.Columns.Add("Key", "Parameter #");
            gridLowCalib.Columns.Add("Value", "Value");
            gridLowCalib.Columns["Key"].ReadOnly = true;

            for(int i=0; i<32; i++)
            {
                gridLowCalib.Rows.Add(i.ToString(), "0");
            }

            gridHighCalib.Columns.Add("Key", "Parameter #");
            gridHighCalib.Columns.Add("Value", "Value");
            gridHighCalib.Columns["Key"].ReadOnly = true;

            for (int i = 0; i < 32; i++)
            {
                gridHighCalib.Rows.Add(i.ToString(), "0");
            }

            /* populate combo boxes */
            /* Gain Values */
            Dictionary<string, int> gainControlValues = new Dictionary<string, int>();
            gainControlValues.Add("Off", 0);
            gainControlValues.Add("Level 1", 1);
            gainControlValues.Add("Level 2", 2);
            gainControlValues.Add("Level 3", 3);
            gainControlValues.Add("Level 4", 4);

            cmbPAGain.DataSource = new BindingSource(gainControlValues, null);
            cmbPAGain.DisplayMember = "Key";
            cmbPAGain.ValueMember = "Value";

            /* Mode Values */
            Dictionary<string, int> ModeControlValues = new Dictionary<string, int>();
            ModeControlValues.Add("Operational", (int)ChangeModeMessage.Mode.Operational);
            ModeControlValues.Add("Technician", (int)ChangeModeMessage.Mode.Technician);
            ModeControlValues.Add("Maintanance", (int)ChangeModeMessage.Mode.Maintanance);

            cmbMode.DataSource = new BindingSource(ModeControlValues, null);
            cmbMode.DisplayMember = "Key";
            cmbMode.ValueMember = "Value";
        }

        private void loadDefaultcomPort()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            string comport;
            try
            {
                comport = config.AppSettings.Settings["DefaultComPort"].Value;
                if(comport == "")
                {
                    throw new NullReferenceException();
                }
            }
            catch(NullReferenceException)
            {
                log.Info("could not read defualt comport from configuration, using COM1 as defualt");
                comport = "1";
            }
            txtDefaultIComPort.Text = comport.ToString();
        }

        private string integerToBinaryString(int data, int numberOfBits, bitName_t[] bitNames)
        {
            StringBuilder sb = new StringBuilder();
            if(bitNames!= null && bitNames.Length != numberOfBits)
            {
                throw new Exception("Error in length of name string array!");
            }
            else
            {
                /* if name array exists */
                sb.AppendLine("0x" + data.ToString("X"));
            }

            for(int i = numberOfBits; i>0; i--)
            {
                int v = data & (1 << (i-1));
                if (bitNames != null)
                {
                    if(bitNames[i - 1].bitName == "")
                    {
                        continue;
                    }
                    sb.Append("--->");
                    sb.Append(bitNames[i-1].bitName);
                    sb.Append(" - ");
                    sb.AppendLine(v > 0 ? bitNames[i-1].high : bitNames[i-1].low);
                }
                else
                {
                    sb.Append(v > 0 ? "1" : "0");
                    sb.Append(" ");
                } 
                    
            }
            return sb.ToString().TrimEnd();
        }
        
        struct bitName_t
        {
            public string bitName, low, high;
            public bitName_t(string name, string low, string high)
            {
                this.bitName = name;
                this.low = low;
                this.high = high;
            }
        };

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private string getComPort()
        {
            string comport = txtDefaultIComPort.Text;
            comport = comport.ToUpper();
            if (!comport.StartsWith("COM"))
            {
                comport = "COM" + comport;
            }
            return comport;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            try
            {
                if (device == null || !device.isConnected())
                {
                    string comport = getComPort();


                    /* currently not connected */
                    // device = new BinaryTCPDevice(txtDefaultIComPort.Text);
                    device = new RFC1662BinarySerialDevice(new BinarySerialDevice(getComPort(), 921600, System.IO.Ports.Parity.None), crc);
                    if (device.connect())
                    {
                        log.Info("Connected to " + comport);
                        b.Text = "Disconnect";
                        b.BackColor = Color.LightGreen;
                    }
                }
                else
                {
                    device.disconnect();
                    device = null;
                    b.Text = "Connect";
                    b.BackColor = Color.Red;
                }
            }
            catch(Exception ex)
            {
                log.Error("error in entered COMPORT: " + txtDefaultIComPort.Text, ex);
            }            
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (device != null && device.isConnected())
            {
                device.disconnect();
            }
        }

        private void btnSaveComPort_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

            string comport = getComPort();
            config.AppSettings.Settings.Remove("DefaultComPort");
            config.AppSettings.Settings.Add("DefaultComPort", comport);
            config.Save(ConfigurationSaveMode.Modified);

            if(comport != txtDefaultIComPort.Text)
            {
                txtDefaultIComPort.Text = comport;
            }
        }

        private void btnSendControlMsg_Click(object sender, EventArgs e)
        {
            UInt16 identifier = Convert.ToUInt16(txtIdentifier.Text);
            txtIdentifier.Text = (identifier + 1).ToString();
            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Send Control Message", 
                new ControlMessage(radTx.Checked, Convert.ToInt32(cmbPAGain.SelectedValue), (radAnt0.Checked ? ControlMessage.TxAntenna.ANT0 : ControlMessage.TxAntenna.ANT1), 
                (radHigh.Checked ? ControlMessage.Frequency.HIGH : ControlMessage.Frequency.LOW), chkReset.Checked, identifier, chkDontUpdate.Checked), true, false, device).run(10000);

            if (r?.result == Scenario.ScenarioResult.RunResult.Pass)
            {
                GetRawStatusResponse resp = (GetRawStatusResponse)r.resultObj;

                StringBuilder sb = new StringBuilder();
                sb.Append("TTI Counter: ");
                sb.AppendLine(resp.ttiCounter.ToString());
                sb.Append("Identifier: ");
                sb.AppendLine(resp.Identifier.ToString());

                sb.AppendLine("Forward Power: ");
                foreach (UInt16 f in resp.fwdPower)
                {
                    sb.Append(f.ToString() + " ");
                }
                
                sb.AppendLine("");
                sb.AppendLine("Input Power: ");
                foreach (UInt16 f in resp.inputPower)
                {
                    sb.Append(f.ToString() + " ");
                }
                sb.AppendLine("");
                sb.Append("Power Difference Status: ");
                if (resp.reversePowerStatus > 0)
                    sb.AppendLine("Pass ");
                else
                    sb.AppendLine("Fail ");
                sb.Append("Temperature: ");
                sb.AppendLine(resp.temperature.ToString());
                sb.Append("Amplifier Current: ");
                sb.AppendLine(resp.powerAmplifierCurrent.ToString());
                sb.Append("Pwr-Amp Gain: ");
                sb.AppendLine(resp.paGain.ToString());
                sb.Append("Antenna: ");
                sb.AppendLine(resp.txAnt.ToString());
                sb.Append("Frequncy: ");
                sb.AppendLine(resp.frequency.ToString());
                txtStatus.Text = sb.ToString();
            }
        }

        private void btnGetMomenteryStatus_Click(object sender, EventArgs e)
        {
            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Get Momentery Status", new GetMomenteryStatusMessage(), true, false, device).run();
            if (r != null && r.result == Scenario.ScenarioResult.RunResult.Pass)
            {
                GetMomenteryStatusResponse resp = (GetMomenteryStatusResponse)r.resultObj;

                StringBuilder sb = new StringBuilder();
                sb.Append("Pwr-Amp Gain: ");
                sb.AppendLine(resp.paGain.ToString());
                sb.Append("Mode: ");
                sb.AppendLine(resp.mode.ToString());
                sb.Append("Antenna: ");
                sb.AppendLine(resp.txAnt.ToString());
                sb.Append("Frequncy: ");
                sb.AppendLine(resp.frequency.ToString());
                //yehuda 22.5.18 add pre_amp to bit status
                sb.AppendLine("Pre-Amp Power: ");
                sb.Append(resp.preAmpPower1.ToString());
                sb.Append(" ");
                sb.Append(resp.preAmpPower2.ToString());
                sb.Append(" ");
                sb.Append(resp.preAmpPower3.ToString());
                sb.Append(" ");
                sb.Append(resp.preAmpPower4.ToString());
                sb.Append(" ");

                sb.AppendLine("");
                sb.Append("Reverse Power: ");
                sb.Append(resp.reversePower1.ToString());
                sb.Append(" ");
                sb.Append(resp.reversePower2.ToString());
                sb.Append(" ");
                sb.Append(resp.reversePower3.ToString());
                sb.Append(" ");
                sb.Append(resp.reversePower4.ToString());
                sb.Append(" ");
                
                txtStatus.Text = sb.ToString();
            }
        }

        private void btnGetVersion_Click(object sender, EventArgs e)
        {
            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Get Version", new GetVersionMessage(), true, false, device).run();
            if (r != null && r.result == Scenario.ScenarioResult.RunResult.Pass)
            {
                GetVersionResponse resp = (GetVersionResponse)r.resultObj;
                log.Info(resp.ToString());

            }
        }

        private void btnSetMode_Click(object sender, EventArgs e)
        {
            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Change Mode", new ChangeModeMessage((ChangeModeMessage.Mode)cmbMode.SelectedValue), true, false, device).run();
            if (r != null && r.result == Scenario.ScenarioResult.RunResult.Pass)
            {

            }
        }

        #region General Calib Related
        private void btnLoadGeneralCalib_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "txt";
            ofd.Filter = "Calibration Text File (*.json)|*.json|All files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                log.Error("a file must be selected!");
                return;
            }


            try
            {
                gridGeneralCalib.Rows.Clear();
                GeneralCalibrationTable tbl = JsonConvert.DeserializeObject<GeneralCalibrationTable>(File.ReadAllText(ofd.FileName));

                FieldInfo[] fields = typeof(GeneralCalibrationTable).GetFields();
                foreach (FieldInfo f in fields)
                {
                    gridGeneralCalib.Rows.Add(f.Name, f.GetValue(tbl));
                }
            }
            catch(Exception ex)
            {
                log.Error("Could not open the selected file!", ex);
            }
            
        }

        private void btnSaveGeneralCalib_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "txt";
            sfd.Filter = "Calibration Text File (*.json)|*.json|All files (*.*)|*.*";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                log.Error("a file must be selected!");
                return;
            }

            GeneralCalibrationTable tbl = new GeneralCalibrationTable();
            foreach (DataGridViewRow r in gridGeneralCalib.Rows)
            {
                FieldInfo valueField = typeof(GeneralCalibrationTable).GetField(r.Cells["Key"].Value.ToString(), BindingFlags.Public | BindingFlags.Instance);
                valueField.SetValue(tbl, Convert.ToUInt16(r.Cells["Value"].Value));
            }

            String jsonString = JsonConvert.SerializeObject(tbl);

            File.WriteAllText(sfd.FileName, jsonString);
            
        }

        private void btnSetGeneralCalib_Click(object sender, EventArgs e)
        {
            GeneralCalibrationTable tbl = new GeneralCalibrationTable();
            foreach (DataGridViewRow row in gridGeneralCalib.Rows)
            {
                FieldInfo valueField = typeof(GeneralCalibrationTable).GetField(row.Cells["Key"].Value.ToString(), BindingFlags.Public | BindingFlags.Instance);
                valueField.SetValue(tbl, Convert.ToUInt16(row.Cells["Value"].Value));
            }

            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Set General Calibration Data", new SetCalibrationTableMessage(tbl, CalibrationTable.TableType.General), true, false, device).run();
            if (r != null && r.result == Scenario.ScenarioResult.RunResult.Pass)
            {
                AckResponse resp = (AckResponse)r.resultObj;
                log.Info("got an ack response opcode: " + resp.opcode.ToString());
            }
        }

        private void btnGetGeneralCalibData_Click(object sender, EventArgs e)
        {
            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Get General Calibration Data", new GetCalibrationTableMessage(CalibrationTable.TableType.General), true, false, device).run();
            if (r != null && r.result == Scenario.ScenarioResult.RunResult.Pass)
            {
                GetCalibrationTableResponse resp = (GetCalibrationTableResponse)r.resultObj;
                GeneralCalibrationTable tbl = (GeneralCalibrationTable)resp.table;

                try
                {
                    gridGeneralCalib.Rows.Clear();

                    FieldInfo[] fields = typeof(GeneralCalibrationTable).GetFields();
                    foreach (FieldInfo f in fields)
                    {
                        gridGeneralCalib.Rows.Add(f.Name, f.GetValue(tbl));
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Could not load calibration data!", ex);
                }
            }
        }
        #endregion
        #region Power Calib Related
        private void loadPowerCalibTable(DataGridView grid)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "txt";
            ofd.Filter = "Calibration Text File (*.json)|*.json|All files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                log.Error("a file must be selected!");
                return;
            }

            try
            {
                grid.Rows.Clear();
                PowerCalibrationTable tbl = JsonConvert.DeserializeObject<PowerCalibrationTable>(File.ReadAllText(ofd.FileName));

                int i = 0;
                foreach (UInt16 u in tbl.PA_GAIN_VALUES)
                {
                    grid.Rows.Add(i++, u);
                }
            }
            catch (Exception ex)
            {
                log.Error("Could not open the selected file!", ex);
            }
        }

        private void savePowerCalibTable(DataGridView grid)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "txt";
            sfd.Filter = "Calibration Text File (*.json)|*.json|All files (*.*)|*.*";
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                log.Error("a file must be selected!");
                return;
            }

            PowerCalibrationTable tbl = new PowerCalibrationTable();

            int i = 0;
            foreach (DataGridViewRow r in grid.Rows)
            {
                tbl.PA_GAIN_VALUES[i++] = Convert.ToUInt16(r.Cells["Value"].Value);
            }

            String jsonString = JsonConvert.SerializeObject(tbl);

            File.WriteAllText(sfd.FileName, jsonString);
        }
        
        private void btnLoadLowCalib_Click(object sender, EventArgs e)
        {
            loadPowerCalibTable(gridLowCalib);
        }

        private void btnSaveLowCalib_Click(object sender, EventArgs e)
        {
            savePowerCalibTable(gridLowCalib);
        }

        private void btnLoadHighCalib_Click(object sender, EventArgs e)
        {
            loadPowerCalibTable(gridHighCalib);
        }

        private void btnSaveHighCalib_Click(object sender, EventArgs e)
        {
            savePowerCalibTable(gridHighCalib);
        }


        private void setPowerCalibTable(DataGridView grid, CalibrationTable.TableType tableType)
        {
            PowerCalibrationTable tbl = new PowerCalibrationTable();
            int i = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {
                tbl.PA_GAIN_VALUES[i++] = Convert.ToUInt16(row.Cells["Value"].Value);
            }

            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Set " + tableType.ToString() + " Calibration Data", new SetCalibrationTableMessage(tbl, tableType), true, false, device).run();
            if (r != null && r.result == Scenario.ScenarioResult.RunResult.Pass)
            {
                AckResponse resp = (AckResponse)r.resultObj;
                log.Info("got an ack response opcode: " + resp.opcode.ToString());
            }
        }

        private void getPowerCalibTable(DataGridView grid, CalibrationTable.TableType tableType)
        {
            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Get " + tableType.ToString() + " Calibration Data", new GetCalibrationTableMessage(tableType), true, false, device).run();
            if (r != null && r.result == Scenario.ScenarioResult.RunResult.Pass)
            {
                GetCalibrationTableResponse resp = (GetCalibrationTableResponse)r.resultObj;
                PowerCalibrationTable tbl = (PowerCalibrationTable)resp.table;

                try
                {
                    grid.Rows.Clear();

                    for (int i = 0; i < tbl.PA_GAIN_VALUES.Length; i++)
                    {
                        grid.Rows.Add(i.ToString(), tbl.PA_GAIN_VALUES[i]);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Could not load calibration data!", ex);
                }
            }
        }
        
        private void btnSetLowCalib_Click(object sender, EventArgs e)
        {
            setPowerCalibTable(gridLowCalib, CalibrationTable.TableType.Low);
        }

        private void btnSetHighCalib_Click(object sender, EventArgs e)
        {
            setPowerCalibTable(gridHighCalib, CalibrationTable.TableType.High);
        }

        private void btnGetLowCalibData_Click(object sender, EventArgs e)
        {
            getPowerCalibTable(gridLowCalib, CalibrationTable.TableType.Low);
        }

        private void btnGetHighCalib_Click(object sender, EventArgs e)
        {
            getPowerCalibTable(gridHighCalib, CalibrationTable.TableType.High);
        }
#endregion

        private void lstLog_MouseMove(object sender, MouseEventArgs e)
        {
            int idx = lstLog.IndexFromPoint(e.Location);
            if (idx >= 0)
            {
                string s = toolTip1.GetToolTip(lstLog);
                if (s != lstLog.Items[idx].ToString())
                {
                    toolTip1.SetToolTip(lstLog, lstLog.Items[idx].ToString());
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "txt";
            ofd.Filter = "HEX File (*.hex)|*.hex|All files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                log.Error("a file must be selected!");
                return;
            }

            HexConverter h = new HexConverter(ofd.FileName);
            h.Process();

            KeyValuePair<UInt32, byte[]> bla;
            while(h.getNextLine(false, out bla) == true)
            {

            }
        }

        private void softwareUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSoftwareUpdate f = new frmSoftwareUpdate();
            f.init(device);
            f.ShowDialog();
            f.Dispose();
            f = null;
        }

        private void btnGetRawStatus_Click(object sender, EventArgs e)
        {
            Scenario.ScenarioResult r = new SingleMessageSingleDeviceScenario("Get Raw Status", new GetRawStatusMessage(), true, false, device).run(1300);
            if (r != null && r.result == Scenario.ScenarioResult.RunResult.Pass)
            {
                GetRawStatusResponse resp = (GetRawStatusResponse)r.resultObj;

                StringBuilder sb = new StringBuilder();
                sb.Append("TTI Counter: ");
                sb.AppendLine(resp.ttiCounter.ToString());
                sb.Append("Identifier: ");
                sb.AppendLine(resp.Identifier.ToString());

                sb.AppendLine("Forward Power: ");
                foreach (UInt16 f in resp.fwdPower)
                {
                    sb.Append(f.ToString() + " ");
                }
               
                sb.AppendLine("");
                sb.AppendLine("Input Power: ");
                foreach (UInt16 f in resp.inputPower)
                {
                    sb.Append(f.ToString() + " ");
                }

                sb.AppendLine("");
                sb.Append("Power Difference Status: ");
                if (resp.reversePowerStatus > 0)
                    sb.AppendLine("Pass ");
                else
                    sb.AppendLine("Fail ");
                 txtStatus.Text = sb.ToString();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            int nTimes = 0;
            int waitTimeMS = 0;
            try
            {
                nTimes = Convert.ToInt32(txtCommTestIterations.Text);
                waitTimeMS = Convert.ToInt32(txtCommTestWaitTime.Text);
            } catch (FormatException)
            {
                log.Error("Error in entered parameters, please enter correct values");
                return;
            }

            try
            {
                Scenario.ScenarioResult r = new CommTestScenario("Test Comm Scenario", device, nTimes, waitTimeMS).run((nTimes * (waitTimeMS + 1000) * 2));
                if (r == null || r.result != Scenario.ScenarioResult.RunResult.Pass)
                {
                    throw new CommTestScenarioException();
                }
            }
            catch(Exception ex)
            {
                log.Error(ex.ToString());
            }
        }

        private void btnSetControlIdentifier_Click(object sender, EventArgs e)
        {
            try
            {
                /* cast to uint to check for valid number */
                UInt32 val = Convert.ToUInt32(txtSetControlIdentifier.Text);
                txtIdentifier.Text = val.ToString();
            } catch(FormatException)
            {
                log.Error("Wrong number entered in 'Set Control Identifier', Can't set this value");
            }
        }
    }
}
