using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransferUnit3Pos
{
    public partial class ConfigUserPage : UserControl
    {
        private string fileName = @"D:\COMMESSE\TransferUnit3Pos.txt";
        ConfigStore readWriteStore = new ConfigStore();
        ConfigData dataStoreClass = new ConfigData();
        public ConfigData dataReaded;
        NumericUpDown numDelaySq01 = new NumericUpDown();

        public ConfigUserPage()
        {
            InitializeComponent();
        }
        private void ConfigUserPage_Load(object sender, EventArgs e)
        {
            refresh(fileName);
            showInNumUpDown();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh(fileName);
            showInNumUpDown();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            write(fileName);
        }

        public void write(string fileName)
        {
            dataStoreClass.DelaySq01 = int.Parse(numericUpDownSq01.Value.ToString());
            dataStoreClass.DelaySq05 = int.Parse(numericUpDownSq05.Value.ToString());
            dataStoreClass.DelaySq06Right = int.Parse(numericUpDownSq06Right.Value.ToString());
            dataStoreClass.DelaySq06Left = int.Parse(numericUpDownSq06Left.Value.ToString());
            dataStoreClass.DelaySq11 = int.Parse(numericUpDownSq11.Value.ToString());
            dataStoreClass.DelaySq10 = int.Parse(numericUpDownSq10.Value.ToString());
            dataStoreClass.DalayStartCycle = int.Parse(numericUpDownStartCycle.Value.ToString());
            dataStoreClass.GeneralTimeoutTime = int.Parse(numericUpDownGenTimeoutTime.Value.ToString());

            readWriteStore.WriteConfig<ConfigData>(dataStoreClass, fileName);
        }
        public void refresh(string fileName)
        {
            dataReaded = readWriteStore.ReadConfig<ConfigData>(fileName);
    
        }

        private void showInNumUpDown()
        {
            numericUpDownSq01.Value= dataReaded.DelaySq01;
            numericUpDownSq05.Value = dataReaded.DelaySq05;
            numericUpDownSq06Left.Value = dataReaded.DelaySq06Left;
            numericUpDownSq06Right.Value = dataReaded.DelaySq06Right;
            numericUpDownSq11.Value = dataReaded.DelaySq11;
            numericUpDownSq10.Value = dataReaded.DelaySq10;
            numericUpDownStartCycle.Value = dataReaded.DalayStartCycle;
            numericUpDownGenTimeoutTime.Value = dataReaded.GeneralTimeoutTime;
        }

        private void numericUpDownSq01_ValueChanged(object sender, EventArgs e)
        {
            // write(fileName);
        }
        private void numericUpDownSq05_ValueChanged(object sender, EventArgs e)
        {
            // write(fileName);
        }
        private void numericUpDownSq06Right_ValueChanged(object sender, EventArgs e)
        {
            // write(fileName);
        }
        private void numericUpDownSq06Left_ValueChanged(object sender, EventArgs e)
        {
            // write(fileName);
        }
        private void numericUpDownSq11_ValueChanged(object sender, EventArgs e)
        {
            // write(fileName);
        }
        private void numericUpDownSq10_ValueChanged(object sender, EventArgs e)
        {
            // write(fileName);
        }
        private void numericUpDownStartCycle_ValueChanged(object sender, EventArgs e)
        {
            // write(fileName);
        }
    }
}
