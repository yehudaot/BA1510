using atpLib.Devices;
using atpLib.Scenarios;
using mfe_gui.Messages;
using mfe_gui.Scenarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mfe_gui
{
    public partial class frmSoftwareUpdate : Form
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger
        (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        Device device;
        Scenario softwareUpdateScenario;
        bool programming = false;

        public delegate void updateProgressDelegate(long total, int current, string status);

        public frmSoftwareUpdate()
        {
            InitializeComponent();
        }

        public void init(Device device)
        {
            this.device = device;
            this.btnStartUpdate.Enabled = false;
            this.btnStartVerify.Enabled = false;
            this.btnStopUpdate.Enabled = false;
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "hex";
            ofd.Filter = "hex files (*.hex)|*.hex|All files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                log.Error("an hex file must be selected!");
                txtFile.Text = "";
                return;
            }

            /* set file info */
            txtFile.Text = ofd.FileName;
            this.Text = this.Text + " - " + ofd.FileName;
            this.btnStartUpdate.Enabled = true;
            this.btnStartVerify.Enabled = true;
        }

        private void btnStartUpdate_Click(object sender, EventArgs e)
        {
            btnStopUpdate.Enabled = true;
            btnStartVerify.Enabled = false;
            btnStartUpdate.Enabled = false;
            softwareUpdateScenario = new SoftwareUpdateScenario("Software Update Scenario", device, txtFile.Text, SoftwareUpdateScenario.Operation.Program, updateProgressBarSoftwareUpdateFunc, operationFinishedFunc).backgroundStart();
        }
        
        void updateProgressBarSoftwareUpdateFunc(long total, long current, string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SoftwareUpdateScenario.SoftwareUpdateDelegate(updateProgressBarThreadSafe), new object[] { total, current, status });
            }
            else
            {
                updateProgressBarThreadSafe(total, current, status);
            }
        }

        void updateProgressBarThreadSafe(long total, long current, string status)
        {
            int val = (int)(((float)current / total) * 100);

            progressBar.Value = val;
            lblToolStrip.Text = status;
        }

        void operationFinishedFunc(SoftwareUpdateScenario.Operation operation)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SoftwareUpdateScenario.OperationFinishedDelegate(operationFinishedThreadSafe), new object[] { operation });
            }
            else
            {
                operationFinishedThreadSafe(operation);
            }
        }

        void operationFinishedThreadSafe(SoftwareUpdateScenario.Operation operation)
        {
            btnStartUpdate.Enabled = true;
            btnStartVerify.Enabled = true;
            btnStopUpdate.Enabled = false;
        }

        private void btnStopUpdate_Click(object sender, EventArgs e)
        {
            softwareUpdateScenario?.backgroundFinish();
            btnStartUpdate.Enabled = true;
            btnStartVerify.Enabled = true;
            btnStopUpdate.Enabled = false;
        }

        private void btnStartVerify_Click(object sender, EventArgs e)
        {
            btnStopUpdate.Enabled = true;
            btnStartUpdate.Enabled = false;
            btnStartVerify.Enabled = false;
            softwareUpdateScenario = new SoftwareUpdateScenario("Software Verify Scenario", device, txtFile.Text, SoftwareUpdateScenario.Operation.Verify, updateProgressBarSoftwareUpdateFunc, operationFinishedFunc).backgroundStart();
        }
    }
}
