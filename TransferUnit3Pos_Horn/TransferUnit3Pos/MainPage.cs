using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransferUnit3Pos
{
    //  Istanza Classe per dati

    public partial class MainPage : Form
    {
        WorkerThreadExampleBis work = new WorkerThreadExampleBis();
        ConfigStore readWriteStore = new ConfigStore();
        ConfigData dataStoreClass = new ConfigData();
        TT tt = new TT();
        private Dati data = new Dati();

        public bool SqDelayElapsed = new bool();
        public Object thisLock = new Object();
        private bool GenTimeElapsed;
        public bool blinkOn = false;
        public ConfigData dataReaded;
        private string fileName = @"D:\COMMESSE\TransferUnit3Pos.txt";
        public bool ManMotRightOn = false;
        public bool ManMotLeftOn = false;

        // MAIN PAGE LOAD
        public MainPage()
        {
            InitializeComponent();
            SetInitVar();
            pictureUpCmd.Hide();
            pictureIntCmd.Hide();
            pictureDownCmd.Hide();
            pictureRightCmd.Hide();
            pictureLeftCmd.Hide();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }
        private void MainPage_Load(object sender, EventArgs e)
        {
            StartCycleList.Items.Add(Dati.ManMainFunctions.NoCycle);
            StartCycleList.Items.Add(Dati.ManMainFunctions.cycleStr_Str);
            StartCycleList.Items.Add(Dati.ManMainFunctions.cycleStr_R);
            StartCycleList.Items.Add(Dati.ManMainFunctions.cycleStr_L);
            StartCycleList.Items.Add(Dati.ManMainFunctions.cycleR_Str);
            StartCycleList.Items.Add(Dati.ManMainFunctions.cycleL_Str);
            StartCycleList.Items.Add(Dati.ManMainFunctions.cycleL_R);
            StartCycleList.Items.Add(Dati.ManMainFunctions.cycleR_L);
            StartCycleList.Items.Add(Dati.ManMainFunctions.fillTp);
        }

        // MAIN PAGE CLOSE
        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            work.stop();
        }

        

        // AUTOMATIC/MANUAL MODE
        private void btnAutMan_Click(object sender, EventArgs e)
        {
            if (data.AutManMode == Dati.AutMan.Automatic)
            {
                data.AutManMode = Dati.AutMan.Manual;
            }
            else
            {
                data.AutManMode = Dati.AutMan.Automatic;
            }

        }

        // PROGRAMM TIMER
        private void TimerCycleView_Tick(object sender, EventArgs e)
        {
            OnTimedEventCycle();
            OnTimeEventAutManMode();
        }
        private void OnTimedEventCycle()
        {
            textBox1.Text = data.MainFunction.ToString();
            textBox2.Text = data.ActualStep.ToString();

            loadConfig();

            switch (data.ManInputpos)
            {
                case Dati.ManInputPos.UpPos:
                    UpPosCheck.Checked = true;
                    IntPosCheck.Checked = false;
                    DownPosCheck.Checked = false;
                    UpPosCheckSim.Checked = true;

                    UpPosCheck.BackColor = Color.GreenYellow;
                    IntPosCheck.BackColor = this.BackColor;
                    DownPosCheck.BackColor = this.BackColor;

                    pictureUpPosOn.Show();
                    pictureUpPosOff.Hide();

                    pictureIntPosOn.Hide();
                    pictureIntPosOff.Show();

                    pictureDownPosOn.Hide();
                    pictureDownPosOff.Show();


                    data.Inputpos = Dati.InputPos.UpPos;
                    break;
                case Dati.ManInputPos.IntPos:
                    UpPosCheck.Checked = false;
                    IntPosCheck.Checked = true;
                    DownPosCheck.Checked = false;
                    IntPosCheckSim.Checked = true;

                    UpPosCheck.BackColor = this.BackColor;
                    IntPosCheck.BackColor = Color.GreenYellow;
                    DownPosCheck.BackColor = this.BackColor;

                    pictureUpPosOn.Hide();
                    pictureUpPosOff.Show();

                    pictureIntPosOn.Show();
                    pictureIntPosOff.Hide();

                    pictureDownPosOn.Hide();
                    pictureDownPosOff.Show();

                    data.Inputpos = Dati.InputPos.IntPos;
                    break;
                case Dati.ManInputPos.DownPos:
                    UpPosCheck.Checked = false;
                    IntPosCheck.Checked = false;
                    DownPosCheck.Checked = true;
                    DownPosCheckSim.Checked = true;

                    UpPosCheck.BackColor = this.BackColor;
                    IntPosCheck.BackColor = this.BackColor;
                    DownPosCheck.BackColor = Color.GreenYellow;

                    pictureUpPosOn.Hide();
                    pictureUpPosOff.Show();

                    pictureIntPosOn.Hide();
                    pictureIntPosOff.Show();

                    pictureDownPosOn.Show();
                    pictureDownPosOff.Hide();

                    data.Inputpos = Dati.InputPos.DownPos;
                    break;
                default:
                    break;
            }
            // color visualisations
            switch (data.Outcmd)
            {
                case Dati.OutCmd.UpCmd:
                    UpCmdCheck.Checked = true;
                    IntCmdCheck.Checked = false;
                    DownCmdCheck.Checked = false;

                    UpCmdCheck.BackColor = Color.GreenYellow;
                    IntCmdCheck.BackColor = this.BackColor;
                    DownCmdCheck.BackColor = this.BackColor;

                    //*                    pictureUpCmd.Show();
                    pictureIntCmd.Hide();
                    pictureDownCmd.Hide();
                    break;
                case Dati.OutCmd.IntCmd:
                    UpCmdCheck.Checked = false;
                    IntCmdCheck.Checked = true;
                    DownCmdCheck.Checked = false;

                    UpCmdCheck.BackColor = this.BackColor;
                    IntCmdCheck.BackColor = Color.GreenYellow;
                    DownCmdCheck.BackColor = this.BackColor;

                    pictureUpCmd.Hide();
                    //*                   pictureIntCmd.Show();
                    pictureDownCmd.Hide();
                    break;
                case Dati.OutCmd.DownCmd:
                    UpCmdCheck.Checked = false;
                    IntCmdCheck.Checked = false;
                    DownCmdCheck.Checked = true;

                    UpCmdCheck.BackColor = this.BackColor;
                    IntCmdCheck.BackColor = this.BackColor;
                    DownCmdCheck.BackColor = Color.GreenYellow;

                    pictureUpCmd.Hide();
                    pictureIntCmd.Hide();
                    //*                   pictureDownCmd.Show();
                    break;
                case Dati.OutCmd.NoCmd:
                    UpCmdCheck.Checked = false;
                    IntCmdCheck.Checked = false;
                    DownCmdCheck.Checked = false;

                    UpCmdCheck.BackColor = this.BackColor;
                    IntCmdCheck.BackColor = this.BackColor;
                    DownCmdCheck.BackColor = this.BackColor;

                    pictureUpCmd.Hide();
                    pictureIntCmd.Hide();
                    pictureDownCmd.Hide();
                    break;
                default:
                    pictureUpCmd.Hide();
                    pictureDownCmd.Hide();
                    break;
            }

            switch (data.OutMotcmd)
            {
                case Dati.OutMotCmd.FwCmd:
                    pictureRightCmd.Show();
                    pictureLeftCmd.Hide();
                    break;
                case Dati.OutMotCmd.BckwCmd:
                    pictureLeftCmd.Show();
                    pictureRightCmd.Hide();
                    break;
                case Dati.OutMotCmd.NoCmd:
                    pictureLeftCmd.Hide();
                    pictureRightCmd.Hide();
                    break;
                default:
                    break;
            }

            lock (data)
            {
                data.SQ05 = data.ManSQ05;
                data.SQ06Left = data.ManSQ06Left;
                data.SQ06Right = data.ManSQ06Right;
                data.SQ07 = data.ManSQ07;
                data.SQ10 = data.ManSQ10;
                data.SQ11 = data.ManSQ11;
                data.SQ01 = data.ManSQ01;            // upstream

                switch (data.ActualStep)
                {
                    case Dati.Steps.NoCycle:

                        //////////////////
                        // Start cycles
                        /////////////////
                        switch (data.StartCycle)
                        {
                            case Dati.startCycle.NoCycle:
                                NoCycle();
                                break;
                            case Dati.startCycle.fillTp:
                                fillTp();
                                break;
                            case Dati.startCycle.cycleStr_R:
                                cycleStr_R();
                                break;
                            case Dati.startCycle.cycleStr_L:
                                cycleStr_L();
                                break;
                            case Dati.startCycle.cycleStr_Str:
                                cycleStr_Str();
                                break;
                            case Dati.startCycle.cycleR_Str:
                                cycleR_Str();
                                break;
                            case Dati.startCycle.cycleL_Str:
                                cycleL_Str();
                                break;
                            case Dati.startCycle.cycleR_L:
                                cycleR_L();
                                break;
                            case Dati.startCycle.cycleL_R:
                                cycleL_R();
                                break;
                            default:
                                break;
                        }
                        break;
                    case Dati.Steps.RxWaitAddrTp:
                        RxWaitaddrTp();
                        break;
                    case Dati.Steps.WaitAddrTp:
                        WaitaddrTp();
                        break;
                    case Dati.Steps.RxUpstr_StrRL:
                        RxUpstr_StrRL();
                        break;
                    case Dati.Steps.WaitLift_StrRL:
                        WaitLift_StrRL();
                        break;
                    case Dati.Steps.LiftCmd_StrRL:
                        LiftCmd_StrRL();
                        break;
                    case Dati.Steps.WaitFreeRight_StrRL:
                        WaitFreeRight_StrRL();
                        break;
                    case Dati.Steps.WaitFreeLeft_StrRL:
                        WaitFreeLeft_StrRL();
                        break;
                    case Dati.Steps.DownstreamRight_StrRL:
                        DownstreamRight_StrRL();
                        break;
                    case Dati.Steps.DownstreamLeft_StrRL:
                        DownstreamLeft_StrRL();
                        break;
                    case Dati.Steps.LiftIntCmd_StrRL:
                        LiftIntCmd_StrRL();
                        break;
                    case Dati.Steps.RxUpstr_StrStr:
                        RxUpstr_StrStr();
                        break;
                    case Dati.Steps.WaitDown_StrStr:
                        WaitDown_StrStr();
                        break;
                    case Dati.Steps.DownCmd_StrStr:
                        DownCmd_StrStr();
                        break;
                    case Dati.Steps.Transfer_StrStr:
                        Transfer_StrStr();
                        break;
                    case Dati.Steps.IntCmd_StrStr:
                        IntCmd_StrStr();
                        break;
                    case Dati.Steps.LiftCmd_RLStr:
                        LiftCmd_RLStr();
                        break;
                    case Dati.Steps.WaitRxRight_RLStr:
                        WaitRxRight_RLStr();
                        break;
                    case Dati.Steps.WaitRxLeft_RLStr:
                        WaitRxLeft_RLStr();
                        break;
                    case Dati.Steps.LiftIntCmd_RLStr:
                        LiftIntCmd_RLStr();
                        break;
                    case Dati.Steps.WaitStrFree_RLStr:
                        WaitStrFree_RLStr();
                        break;
                    case Dati.Steps.DownCmd_RLStr:
                        DownCmd_RLStr();
                        break;
                    case Dati.Steps.LiftIntEndCmd_RLStr:
                        LiftIntEndCmd_RLStr();
                        break;
                    case Dati.Steps.Transfer_Str_RLStr:
                        Transfer_Str_RLStr();
                        break;
                    case Dati.Steps.LiftUpCmd_RL:
                        LiftUpCmd_RL();
                        break;
                    case Dati.Steps.WaitRx_RL:
                        WaitRx_RL();
                        break;
                    case Dati.Steps.LiftIntCmd_RL:
                        LiftIntCmd_RL();
                        break;
                    case Dati.Steps.LiftUpCmd_LR:
                        LiftUpCmd_LR();
                        break;
                    case Dati.Steps.WaitRx_LR:
                        WaitRx_LR();
                        break;
                    case Dati.Steps.LiftIntCmd_LR:
                        LiftIntCmd_LR();
                        break;
                    case Dati.Steps.WaitEndHorn:
                        WaitEndHorn();
                        break;
                    default:
                        break;
                }
            }

            GeneralStatusTp();
            if (data.ActualStep == Dati.Steps.NoCycle)
            {
                Alarms(Dati.PosToCheckAlarm.ceckMustInt, GenTimeElapsed);
            }
            timerSqDelay.Interval = DelaySet(data.ActualStep);

        }
        private void OnTimeEventAutManMode()
        {
            switch (data.AutManMode)
            {
                case Dati.AutMan.Manual:

                    // reset timout's
                    TimeoutReset();

                    ManCmdOutputs();
                    ManMotCmdOutputs();

                    btnAutMan.BackColor = Color.Orange;
                    btnAutMan.ForeColor = Color.Black;

                    btnUp.Visible = true;
                    btnInt.Visible = true;
                    btnDown.Visible = true;
                    btnRight.Visible = true;
                    btnLeft.Visible = true;
                    labelManualTpCmd.Visible = true;
                    labelManualMtCmd.Visible = true;
                    labelManualCmd.Visible = true;

                    btnUp.BackColor = Color.Orange;
                    btnInt.BackColor = Color.Orange;
                    btnDown.BackColor = Color.Orange;
                    btnDown.BackColor = Color.Orange;
                    btnRight.BackColor = Color.Orange;
                    btnLeft.BackColor = Color.Orange;
                    labelManualCmd.BackColor = Color.Orange;
                    labelManualMtCmd.BackColor = Color.Orange;
                    labelManualTpCmd.BackColor = Color.Orange;


                    btnUp.ForeColor = Color.Black;
                    btnInt.ForeColor = Color.Black;
                    btnDown.ForeColor = Color.Black;
                    btnRight.ForeColor = Color.Black;
                    btnLeft.ForeColor = Color.Black;
                    labelManualMtCmd.ForeColor = Color.Black;
                    labelManualTpCmd.ForeColor = Color.Black;
                    labelManualCmd.ForeColor = Color.Black;

                    if (data.lastAutManMode == Dati.LastAutManMode.Aut)
                    {
                        data.Outcmd = Dati.OutCmd.NoCmd;
                        data.OutMotcmd = Dati.OutMotCmd.NoCmd;
                        data.lastAutManMode = Dati.LastAutManMode.Man;
                    }
                    break;
                case Dati.AutMan.Automatic:
                    btnAutMan.BackColor = Color.Black;
                    btnAutMan.ForeColor = Color.White;

                    ManMotRightOn = false;
                    ManMotLeftOn = false;

                    btnUp.Visible = false;
                    btnInt.Visible = false;
                    btnDown.Visible = false;
                    btnRight.Visible = false;
                    btnLeft.Visible = false;
                    labelManualCmd.Visible = false;
                    labelManualTpCmd.Visible = false;
                    labelManualMtCmd.Visible = false;

                    if (data.lastAutManMode == Dati.LastAutManMode.Man)
                    {
                        data.Outcmd = Dati.OutCmd.NoCmd;
                        data.OutMotcmd = Dati.OutMotCmd.NoCmd;
                        data.lastAutManMode = Dati.LastAutManMode.Aut;
                    }

                    break;
                default:
                    break;
            }
        }

        // UI CONTROLS
        private void startPlc_Click(object sender, EventArgs e)
        {
            work.Main();
            button1.BackColor = Color.GreenYellow;
            button2.BackColor = Color.AliceBlue;

            TimerCycleView.Interval = 5;
            TimerCycleView.Enabled = true;

            timerBlink.Interval = 300;
            timerBlink.Enabled = true;

            //   textBox1.Text = "Timer parte";
            textBox1.BackColor = Color.GreenYellow;
            textBox3.Text = "normal running";
        }
        private void stopPlc_Click(object sender, EventArgs e)
        {
            work.stop();

            button1.BackColor = Color.AliceBlue;
            button2.BackColor = Color.Red;

            TimerCycleView.Enabled = false;
            timerBlink.Enabled = false;
            textBox1.BackColor = Color.Red;
            textBox3.Text = "-----------------";

            tt.start();

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int i = StartCycleList.SelectedIndex;
            switch (i)
            {
                case 0:
                    break;
                case 1:
                    data.StartCycle = Dati.startCycle.cycleStr_Str;
                    break;
                case 2:
                    data.StartCycle = Dati.startCycle.cycleStr_R;
                    break;
                case 3:
                    data.StartCycle = Dati.startCycle.cycleStr_L;
                    break;
                case 4:
                    data.StartCycle = Dati.startCycle.cycleR_Str;
                    break;
                case 5:
                    data.StartCycle = Dati.startCycle.cycleL_Str;
                    break;
                case 6:
                    data.StartCycle = Dati.startCycle.cycleL_R;
                    break;
                case 7:
                    data.StartCycle = Dati.startCycle.cycleR_L;
                    break;
                case 8:
                    data.StartCycle = Dati.startCycle.fillTp;
                    break;
                default:
                    break;
            }

        }
        private void SQ06RightCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (TimerCycleView.Enabled & this.SQ06RightCheck.Checked)
            {
                data.ManSQ06Right = true;
            }
            if (TimerCycleView.Enabled & !this.SQ06RightCheck.Checked)
            {
                data.ManSQ06Right = false;
            }
            //   textBox1.Text = "SQ06Right è nello stato di " + data.ManSQ06Right.ToString();
        }
        private void SQ06LeftCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (TimerCycleView.Enabled & this.SQ06LeftCheck.Checked)
            {
                data.ManSQ06Left = true;
            }
            if (TimerCycleView.Enabled & !this.SQ06LeftCheck.Checked)
            {
                data.ManSQ06Left = false;
            }
            //   textBox1.Text = "SQ06Left è nello stato di " + data.ManSQ06Left.ToString();
        }
        private void SQ10Check_CheckedChanged(object sender, EventArgs e)
        {
            if (TimerCycleView.Enabled & this.SQ10Check.Checked)
            {
                data.ManSQ10 = true;
            }
            if (TimerCycleView.Enabled & !this.SQ10Check.Checked)
            {
                data.ManSQ10 = false;
            }
            //    textBox1.Text = "SQ10 è nello stato di " + data.ManSQ10.ToString();
        }
        private void SQ05Check_CheckedChanged(object sender, EventArgs e)
        {
            if (TimerCycleView.Enabled & this.SQ05Check.Checked)
            {
                data.ManSQ05 = true;
            }
            if (TimerCycleView.Enabled & !this.SQ05Check.Checked)
            {
                data.ManSQ05 = false;
            }
            //     textBox1.Text = "SQ05 è nello stato di " + data.ManSQ05.ToString();

        }
        private void SQ07Check_CheckedChanged(object sender, EventArgs e)
        {
            if (TimerCycleView.Enabled & this.SQ07Check.Checked)
            {
                data.ManSQ07 = true;
            }
            if (TimerCycleView.Enabled & !this.SQ07Check.Checked)
            {
                data.ManSQ07 = false;
            }
            //    textBox1.Text = "SQ07 è nello stato di " + data.ManSQ07.ToString();
        }
        private void SQ01Upstream_CheckedChanged(object sender, EventArgs e)
        {
            if (TimerCycleView.Enabled & this.SQ01UpstreamCeck.Checked)
            {
                data.ManSQ01 = true;
            }
            if (TimerCycleView.Enabled & !this.SQ01UpstreamCeck.Checked)
            {
                data.ManSQ01 = false;
            }
            //     textBox1.Text = "SQ01 è nello stato di " + data.ManSQ01.ToString();
        }
        private void SQ11Check_CheckedChanged(object sender, EventArgs e)
        {
            if (TimerCycleView.Enabled & this.SQ11Check.Checked)
            {
                data.ManSQ11 = true;
            }
            if (TimerCycleView.Enabled & !this.SQ11Check.Checked)
            {
                data.ManSQ11 = false;
            }
            //    textBox1.Text = "SQ11 è nello stato di " + data.ManSQ11.ToString();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (TimerCycleView.Enabled)
            {
                data.ManInputpos = Dati.ManInputPos.UpPos;
                textBox1.Text = data.ManInputpos.ToString();
                this.DownPosCheckSim.Checked = false;
                this.IntPosCheckSim.Checked = false;
            }
        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (TimerCycleView.Enabled)
            {
                data.ManInputpos = Dati.ManInputPos.IntPos;
                textBox1.Text = data.ManInputpos.ToString();
                this.DownPosCheckSim.Checked = false;
                this.UpPosCheckSim.Checked = false;
            }
        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (TimerCycleView.Enabled)
            {
                data.ManInputpos = Dati.ManInputPos.DownPos;
                textBox1.Text = data.ManInputpos.ToString();
                this.IntPosCheckSim.Checked = false;
                this.UpPosCheckSim.Checked = false;
            }
        }
        private void ResetTp_Click(object sender, EventArgs e)
        {
            GeneralResetTp();
        }

        // UI MANUAL COMMANDS
        private void btnUp_Click(object sender, EventArgs e)
        {
            data.ManOutcmd = Dati.ManOutCmd.UpCmd;
        }
        private void btnInt_Click(object sender, EventArgs e)
        {
            data.ManOutcmd = Dati.ManOutCmd.IntCmd;
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            data.ManOutcmd = Dati.ManOutCmd.DownCmd;
        }
        private void btnRight_Click(object sender, EventArgs e)
        {
            if (!ManMotRightOn)
            {
                ManMotRightOn = true;
            }
            else
            {
                ManMotRightOn = false;
            }

            //*      data.ManOutMotcmd = Dati.ManOutMotCmd.FwCmd;
        }
        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (!ManMotLeftOn)
            {
                ManMotLeftOn = true;
            }
            else
            {
                ManMotLeftOn = false;
            }
            //*     data.ManOutMotcmd = Dati.ManOutMotCmd.BckwCmd;
        }

        // OPEN CONFIG DIALOG FORM
        private void btnConfig_Click(object sender, EventArgs e)
        {
            using (ConfigPage configPage = new ConfigPage())
            {
                configPage.ShowDialog();
            }
        }

        // MOVE ENABLE
        private bool moveEnable()
        {
            bool ok = false;

            if (data.Alarmlist == Dati.AlarmList.NoAlarms & data.AutManMode == Dati.AutMan.Automatic)
            {
                ok = true;
            }

            return ok;
        }

        // START CYCLES
        private void NoCycle()
        {
            TpCmdOutputs(Dati.OutCmd.NoCmd);
            data.MustOn = Dati.SensorMustOn.MustOff;
        }
        private void fillTp()
        {
            data.MainFunction = Dati.MainFunctions.fillTp;

            if (!data.SQ01 & !data.SQ05)
            {
                data.MustOn = Dati.SensorMustOn.SQ01MustOn;
            }

            else
            {
                data.MustOn = Dati.SensorMustOn.SQ05MustOn;

                if (data.AutManMode == Dati.AutMan.Automatic)
                {
                    data.ActualStep = Dati.Steps.RxWaitAddrTp;
                }
            }
        }
        private void cycleStr_R()
        {
            switch (data.CycleMode)
            {
                case Dati.cycleMode.startFromSq01:
                    data.MustOn = Dati.SensorMustOn.SQ01MustOn;
                    if (!data.SQ05 & data.SQ01 & data.AutManMode == Dati.AutMan.Automatic)
                    {
                        data.MainFunction = Dati.MainFunctions.cycleStr_R;
                        data.ActualStep = Dati.Steps.RxUpstr_StrRL;
                        TimeoutReset();
                        data.MustOn = Dati.SensorMustOn.MustOff;
                    }
                    break;
                case Dati.cycleMode.startFromSq05:
                    break;
                default:
                    break;
            }
        }
        private void cycleStr_L()
        {
            switch (data.CycleMode)
            {
                case Dati.cycleMode.startFromSq01:
                    data.MustOn = Dati.SensorMustOn.SQ01MustOn;
                    if (!data.SQ05 & data.SQ01 & data.AutManMode == Dati.AutMan.Automatic)
                    {
                        data.MainFunction = Dati.MainFunctions.cycleStr_L;
                        data.ActualStep = Dati.Steps.RxUpstr_StrRL;
                        data.MustOn = Dati.SensorMustOn.MustOff;
                    }
                    break;
                case Dati.cycleMode.startFromSq05:
                    break;
                default:
                    break;
            }
        }
        private void cycleStr_Str()
        {

            switch (data.CycleMode)
            {
                case Dati.cycleMode.startFromSq01:
                    data.MustOn = Dati.SensorMustOn.SQ01MustOn;
                    if (data.SQ01 & moveEnable())
                    {
                        data.MainFunction = Dati.MainFunctions.cycleStr_Str;
                        data.ActualStep = Dati.Steps.RxUpstr_StrStr;
                        data.MustOn = Dati.SensorMustOn.MustOff;
                    }
                    break;
                case Dati.cycleMode.startFromSq05:
                    break;
                default:
                    break;
            }
        }
        private void cycleR_Str()
        {
            data.MustOn = Dati.SensorMustOn.SQ11MustOn;
            if (data.SQ11 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.MainFunction = Dati.MainFunctions.cycleR_Str;
                data.ActualStep = Dati.Steps.LiftCmd_RLStr;
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void cycleL_Str()
        {
            data.MustOn = Dati.SensorMustOn.SQ10MustOn;
            if (data.SQ10 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.MainFunction = Dati.MainFunctions.cycleL_Str;
                data.ActualStep = Dati.Steps.LiftCmd_RLStr;
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void cycleR_L()
        {
            data.MustOn = Dati.SensorMustOn.SQ11MustOn;
            if (data.SQ11 & !data.SQ10 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.MainFunction = Dati.MainFunctions.cycleR_L;
                data.ActualStep = Dati.Steps.LiftUpCmd_RL;
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void cycleL_R()
        {
            data.MustOn = Dati.SensorMustOn.SQ10MustOn;
            if (data.SQ10 & !data.SQ11 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.MainFunction = Dati.MainFunctions.cycleL_R;
                data.ActualStep = Dati.Steps.LiftUpCmd_LR;
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }

        // CYCLES
        //StrRL
        private void RxWaitaddrTp()
        {
            Alarms(Dati.PosToCheckAlarm.ceckMustInt, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ05MustOn;
            if (data.SQ05 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.WaitAddrTp;
                TimeoutReset();
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void WaitaddrTp()
        {
            data.MustOn = Dati.SensorMustOn.NoVisualization;
            Alarms(Dati.PosToCheckAlarm.ceckMustInt, GenTimeElapsed);
            switch (data.StartCycle)
            {
                case Dati.startCycle.NoCycle:
                    NoCycle();
                    break;
                case Dati.startCycle.fillTp:
                    break;
                case Dati.startCycle.cycleStr_R:
                    data.MainFunction = Dati.MainFunctions.cycleStr_R;
                    data.ActualStep = Dati.Steps.LiftCmd_StrRL;
                    TimeoutReset();
                    data.MustOn = Dati.SensorMustOn.MustOff;
                    data.ActualStep = Dati.Steps.LiftCmd_StrRL;
                    break;
                case Dati.startCycle.cycleStr_L:
                    data.MainFunction = Dati.MainFunctions.cycleStr_L;
                    data.ActualStep = Dati.Steps.LiftCmd_StrRL;
                    TimeoutSet(data.timeoutTime);
                    data.MustOn = Dati.SensorMustOn.MustOff;
                    break;
                case Dati.startCycle.cycleStr_Str:
                    data.MainFunction = Dati.MainFunctions.cycleStr_Str;
                    data.ActualStep = Dati.Steps.DownCmd_StrStr;
                    TimeoutSet(data.timeoutTime);
                    data.MustOn = Dati.SensorMustOn.MustOff;
                    break;
                case Dati.startCycle.cycleR_Str:
                    cycleR_Str();
                    break;
                case Dati.startCycle.cycleL_Str:
                    cycleL_Str();
                    break;
                case Dati.startCycle.cycleR_L:
                    cycleR_L();
                    break;
                case Dati.startCycle.cycleL_R:
                    cycleL_R();
                    break;
                default:
                    break;
            }
        }
        // StrStr
        private void RxUpstr_StrStr()
        {
            Alarms(Dati.PosToCheckAlarm.ceckMustInt, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ05MustOn;
            if (data.SQ05Delayed & data.SQ05 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.WaitDown_StrStr;
                TimeoutReset(); ;
                data.MustOn = Dati.SensorMustOn.MustOff;
                data.SQ05Delayed = false;
            }
        }
        private void WaitDown_StrStr()
        {
            data.MustOn = Dati.SensorMustOn.NoVisualization;
            Alarms(Dati.PosToCheckAlarm.ceckMustInt, GenTimeElapsed);
            if (!data.SQ07 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.DownCmd_StrStr;
            }
        }
        private void DownCmd_StrStr()
        {
            TpCmdOutputs(Dati.OutCmd.DownCmd);
            data.MustOn = Dati.SensorMustOn.DownMustOn;
            TimeoutSet(data.timeoutTime);
            if (data.Inputpos == Dati.InputPos.DownPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.Transfer_StrStr;
                TimeoutReset(); ;
            }
        }
        private void Transfer_StrStr()
        {
            Alarms(Dati.PosToCheckAlarm.ceckMustDown, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ07MustOn;
            if (data.SQ07 || data.SQ06Left & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.IntCmd_StrStr;
                TimeoutReset();
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void IntCmd_StrStr()
        {
            data.MustOn = Dati.SensorMustOn.IntMustOn;
            TpCmdOutputs(Dati.OutCmd.IntCmd);
            TimeoutSet(data.timeoutTime);
            if (data.Inputpos == Dati.InputPos.IntPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.WaitEndHorn;
            }
        }
        // Str R-L
        private void RxUpstr_StrRL()
        {
            Alarms(Dati.PosToCheckAlarm.ceckMustInt, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ05MustOn;
            if (data.SQ05Delayed & data.SQ05 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.LiftCmd_StrRL;
                TimeoutReset();
                data.MustOn = Dati.SensorMustOn.MustOff;
                data.SQ05Delayed = false;
            }
        }
        private void WaitLift_StrRL()
        {
            data.MustOn = Dati.SensorMustOn.NoVisualization;
            Alarms(Dati.PosToCheckAlarm.ceckMustInt, GenTimeElapsed);
            TpCmdOutputs(Dati.OutCmd.UpCmd);
        }
        private void LiftCmd_StrRL()
        {
            TpCmdOutputs(Dati.OutCmd.UpCmd);
            data.MustOn = Dati.SensorMustOn.UpMustOn;
            Alarms(Dati.PosToCheckAlarm.ceckMustInt, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            if (data.Inputpos == Dati.InputPos.UpPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.WaitFreeLeft_StrRL;
                TimeoutReset();
                TpCmdOutputs(Dati.OutCmd.NoCmd);

                if (data.MainFunction == Dati.MainFunctions.cycleStr_R)
                {
                    data.ActualStep = Dati.Steps.WaitFreeRight_StrRL;
                }
                if (data.MainFunction == Dati.MainFunctions.cycleStr_L)
                {
                    data.ActualStep = Dati.Steps.WaitFreeLeft_StrRL;
                }
            }
        }
        private void WaitFreeRight_StrRL()
        {
            data.MustOn = Dati.SensorMustOn.NoVisualization;
            Alarms(Dati.PosToCheckAlarm.ceckMustUp, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            if (!data.SQ11 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.DownstreamRight_StrRL;
                TimeoutReset();
            }
        }
        private void WaitFreeLeft_StrRL()
        {
            data.MustOn = Dati.SensorMustOn.NoVisualization;
            Alarms(Dati.PosToCheckAlarm.ceckMustUp, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            if (data.SQ10 == false & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.DownstreamLeft_StrRL;
                TimeoutReset();
            }
        }
        private void DownstreamRight_StrRL()
        {
            MotCmdOutputs(Dati.OutMotCmd.FwCmd);
            Alarms(Dati.PosToCheckAlarm.ceckMustUp, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ11MustOn;
            if (data.SQ11Delayed & data.SQ11 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.LiftIntCmd_StrRL;
                TpCmdOutputs(Dati.OutCmd.IntCmd);
                MotCmdOutputs(Dati.OutMotCmd.NoCmd);
                TimeoutReset();
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void DownstreamLeft_StrRL()
        {
            MotCmdOutputs(Dati.OutMotCmd.BckwCmd);
            Alarms(Dati.PosToCheckAlarm.ceckMustUp, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ10MustOn;
            if (data.SQ10Delayed & data.SQ10 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.LiftIntCmd_StrRL;
                MotCmdOutputs(Dati.OutMotCmd.NoCmd);
                TimeoutReset();
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void LiftIntCmd_StrRL()
        {
            TpCmdOutputs(Dati.OutCmd.IntCmd);
            data.MustOn = Dati.SensorMustOn.IntMustOn;
            TimeoutSet(data.timeoutTime);
            if (data.Inputpos == Dati.InputPos.IntPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                TpCmdOutputs(Dati.OutCmd.NoCmd);
                data.ActualStep = Dati.Steps.NoCycle;
                data.MainFunction = Dati.MainFunctions.NoCycle;
                data.StartCycle = Dati.startCycle.NoCycle;
                TimeoutReset();
                StartCycleList.SetSelected(StartCycleList.SelectedIndex, false);
            }
        }
        // RLStr
        private void LiftCmd_RLStr()
        {
            TpCmdOutputs(Dati.OutCmd.UpCmd);
            data.MustOn = Dati.SensorMustOn.UpMustOn;
            TimeoutSet(data.timeoutTime);
            if (data.Inputpos == Dati.InputPos.UpPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                TpCmdOutputs(Dati.OutCmd.NoCmd);

                if (data.MainFunction == Dati.MainFunctions.cycleR_Str)
                {
                    data.ActualStep = Dati.Steps.WaitRxRight_RLStr;
                    MotCmdOutputs(Dati.OutMotCmd.BckwCmd);
                    TimeoutReset(); ;
                }
                if (data.MainFunction == Dati.MainFunctions.cycleL_Str)
                {
                    data.ActualStep = Dati.Steps.WaitRxLeft_RLStr;
                    MotCmdOutputs(Dati.OutMotCmd.FwCmd);
                    TimeoutReset(); ;
                }
            }
        }
        private void WaitRxRight_RLStr()
        {
            Alarms(Dati.PosToCheckAlarm.ceckMustUp, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ06RightMustOn;
            if (data.SQ06RightDelayed & data.SQ06Right & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.LiftIntCmd_RLStr;
                MotCmdOutputs(Dati.OutMotCmd.NoCmd);
                TimeoutReset();
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void WaitRxLeft_RLStr()
        {
            Alarms(Dati.PosToCheckAlarm.ceckMustUp, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ06LeftMustOn;
            if (data.SQ06LeftDelayed & data.SQ06Left & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.LiftIntCmd_RLStr;
                MotCmdOutputs(Dati.OutMotCmd.NoCmd);
                TimeoutReset();
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void LiftIntCmd_RLStr()
        {
            TpCmdOutputs(Dati.OutCmd.IntCmd);
            data.MustOn = Dati.SensorMustOn.IntMustOn;
            TimeoutSet(data.timeoutTime);
            if (data.Inputpos == Dati.InputPos.IntPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.WaitStrFree_RLStr;
                TimeoutReset();
                TpCmdOutputs(Dati.OutCmd.NoCmd);
            }
        }
        private void WaitStrFree_RLStr()
        {
            data.MustOn = Dati.SensorMustOn.NoVisualization;
            Alarms(Dati.PosToCheckAlarm.ceckMustInt, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            if (!data.SQ07 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.DownCmd_RLStr;
                TimeoutReset(); ;
            }
        }
        private void DownCmd_RLStr()
        {
            TpCmdOutputs(Dati.OutCmd.DownCmd);
            data.MustOn = Dati.SensorMustOn.DownMustOn;
            TimeoutSet(data.timeoutTime);
            if (data.Inputpos == Dati.InputPos.DownPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.Transfer_Str_RLStr;
                TimeoutReset(); ;
            }
        }
        private void Transfer_Str_RLStr()
        {
            Alarms(Dati.PosToCheckAlarm.ceckMustDown, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ07MustOn;
            if (data.SQ07Delayed & data.SQ07 || data.SQ06Left & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.LiftIntEndCmd_RLStr;
                TimeoutReset();
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void LiftIntEndCmd_RLStr()
        {
            TpCmdOutputs(Dati.OutCmd.IntCmd);
            data.MustOn = Dati.SensorMustOn.IntMustOn;
            TimeoutSet(data.timeoutTime);

            if (data.Inputpos == Dati.InputPos.IntPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.WaitEndHorn;
            }
        }
        // RL
        private void LiftUpCmd_RL()
        {
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.UpMustOn;
            if (data.Inputpos == Dati.InputPos.UpPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.WaitRx_RL;
                TimeoutReset(); ;
            }
        }
        private void WaitRx_RL()
        {
            MotCmdOutputs(Dati.OutMotCmd.BckwCmd);
            Alarms(Dati.PosToCheckAlarm.ceckMustUp, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ10MustOn;
            if (data.SQ10 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.LiftIntCmd_RL;
                MotCmdOutputs(Dati.OutMotCmd.NoCmd);
                TimeoutReset();
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void LiftIntCmd_RL()
        {
            TpCmdOutputs(Dati.OutCmd.IntCmd);
            data.MustOn = Dati.SensorMustOn.IntMustOn;
            TimeoutSet(data.timeoutTime);
            if (data.Inputpos == Dati.InputPos.IntPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.NoCycle;
                data.MainFunction = Dati.MainFunctions.NoCycle;
                data.StartCycle = Dati.startCycle.NoCycle;
                TimeoutReset();
                TpCmdOutputs(Dati.OutCmd.NoCmd);
                StartCycleList.SetSelected(StartCycleList.SelectedIndex, false);
            }
        }

        // LR
        private void LiftUpCmd_LR()
        {
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.UpMustOn;
            if (data.Inputpos == Dati.InputPos.UpPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.WaitRx_LR;
                TimeoutReset();
            }
        }
        private void WaitRx_LR()
        {
            MotCmdOutputs(Dati.OutMotCmd.FwCmd);
            Alarms(Dati.PosToCheckAlarm.ceckMustUp, GenTimeElapsed);
            TimeoutSet(data.timeoutTime);
            data.MustOn = Dati.SensorMustOn.SQ11MustOn;
            if (data.SQ11 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.LiftIntCmd_LR;
                MotCmdOutputs(Dati.OutMotCmd.NoCmd);
                TimeoutReset();
                data.MustOn = Dati.SensorMustOn.MustOff;
            }
        }
        private void LiftIntCmd_LR()
        {
            TpCmdOutputs(Dati.OutCmd.IntCmd);
            data.MustOn = Dati.SensorMustOn.IntMustOn;
            TimeoutSet(data.timeoutTime);
            if (data.Inputpos == Dati.InputPos.IntPos & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.NoCycle;
                data.MainFunction = Dati.MainFunctions.NoCycle;
                data.StartCycle = Dati.startCycle.NoCycle;
                TimeoutReset();
                TpCmdOutputs(Dati.OutCmd.NoCmd);
                StartCycleList.SetSelected(StartCycleList.SelectedIndex, false);
            }
        }

        // wait end Horn ||
        private void WaitEndHorn()
        {
             if (data.SQ07 & data.AutManMode == Dati.AutMan.Automatic)
            {
                data.ActualStep = Dati.Steps.NoCycle;
                data.MainFunction = Dati.MainFunctions.NoCycle;
                data.StartCycle = Dati.startCycle.NoCycle;
                TimeoutReset();
                TpCmdOutputs(Dati.OutCmd.NoCmd);
                StartCycleList.SetSelected(StartCycleList.SelectedIndex, false);
            }
        }

        // AUTOMATIC OUTPUT COMMANDS
        private void TpCmdOutputs(Dati.OutCmd cmd)
        {
            if (data.AutManMode == Dati.AutMan.Automatic)
            {
                switch (cmd)
                {
                    case Dati.OutCmd.UpCmd:
                        data.Outcmd = Dati.OutCmd.UpCmd;
                        break;
                    case Dati.OutCmd.IntCmd:
                        data.Outcmd = Dati.OutCmd.IntCmd;
                        break;
                    case Dati.OutCmd.DownCmd:
                        data.Outcmd = Dati.OutCmd.DownCmd;
                        break;
                    case Dati.OutCmd.NoCmd:
                        data.Outcmd = Dati.OutCmd.NoCmd;
                        break;
                    default:
                        break;
                }
            }
        }
        private void MotCmdOutputs(Dati.OutMotCmd cmd)
        {
            switch (cmd)
            {
                case Dati.OutMotCmd.FwCmd:
                    data.OutMotcmd = Dati.OutMotCmd.FwCmd;
                    break;
                case Dati.OutMotCmd.BckwCmd:
                    data.OutMotcmd = Dati.OutMotCmd.BckwCmd;
                    break;
                case Dati.OutMotCmd.NoCmd:
                    data.OutMotcmd = Dati.OutMotCmd.NoCmd;
                    break;
                default:
                    break;
            }
        }

        // MANUAL OUTPUT COMMANDS
        private void ManCmdOutputs()
        {
            switch (data.ManOutcmd)
            {
                case Dati.ManOutCmd.UpCmd:
                    if (data.Inputpos != Dati.InputPos.UpPos)
                    {
                        data.Outcmd = Dati.OutCmd.UpCmd;
                    }
                    else
                    {
                        data.Outcmd = Dati.OutCmd.NoCmd;
                        data.ManOutcmd = Dati.ManOutCmd.NoCmd;
                    }
                    break;
                case Dati.ManOutCmd.IntCmd:
                    if (data.Inputpos != Dati.InputPos.IntPos)
                    {
                        data.Outcmd = Dati.OutCmd.IntCmd;
                    }
                    else
                    {
                        data.Outcmd = Dati.OutCmd.NoCmd;
                        data.ManOutcmd = Dati.ManOutCmd.NoCmd;
                    }
                    break;
                case Dati.ManOutCmd.DownCmd:
                    data.Outcmd = Dati.OutCmd.DownCmd;
                    break;
                case Dati.ManOutCmd.NoCmd:
                    data.Outcmd = Dati.OutCmd.NoCmd;
                    break;
                default:
                    break;
            }
        }
        private void ManMotCmdOutputs()
        {
            if (ManMotRightOn)
            {
                ManMotLeftOn = false;
                data.OutMotcmd = Dati.OutMotCmd.FwCmd;

            }
            if (ManMotLeftOn)
            {
                ManMotRightOn = false;
                data.OutMotcmd = Dati.OutMotCmd.BckwCmd;
            }

            if (!ManMotLeftOn & !ManMotRightOn)
            {
                data.OutMotcmd = Dati.OutMotCmd.NoCmd;
            }


        }

        // TIMEOUT'S
        private void TimerGeneralTimeout_Tick(object sender, EventArgs e)
        {
            GenTimeElapsed = true;
        }
        private void TimeoutSet(int t)
        {
            TimerGeneralTimeout.Enabled = true;
            TimerGeneralTimeout.Interval = t;
            if (GenTimeElapsed)
            {
                textBox3.Text = "Timeout : " + data.ActualStep.ToString();
                textBox3.BackColor = Color.Tomato;
            }
        }
        private void TimeoutReset()
        {
            TimerGeneralTimeout.Enabled = false;
            GenTimeElapsed = false;
            textBox3.Text = "normal running";
            textBox3.BackColor = this.BackColor;
        }

        // SENSOR DELAY 
        private int DelaySet(Dati.Steps step)
        {
            int t = 10;
            dataReaded = readWriteStore.ReadConfig<ConfigData>(fileName);
            switch (data.ActualStep)
            {
                case Dati.Steps.NoCycle:
                    timerSqDelay.Enabled = false;
                    data.SQ05Delayed = false;
                    data.SQ10Delayed = false;
                    data.SQ11Delayed = false;
                    data.SQ07Delayed = false;
                    data.SQ01Delayed = false;
                    data.SQ06LeftDelayed = false;
                    data.SQ06RightDelayed = false;

                    SqDelayElapsed = false;
                    break;
                case Dati.Steps.RxWaitAddrTp:
                    if (data.SQ05)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                    }
                    t = dataReaded.DelaySq05;
                    if (SqDelayElapsed)
                    {
                        data.SQ05Delayed = true;
                    }
                    break;
                case Dati.Steps.WaitAddrTp:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.RxUpstr_StrRL:
                    if (data.SQ05)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                    }
                    t = dataReaded.DelaySq05;
                    if (SqDelayElapsed)
                    {
                        data.SQ05Delayed = true;
                    }
                    break;
                case Dati.Steps.WaitLift_StrRL:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.LiftCmd_StrRL:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.WaitFreeRight_StrRL:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.WaitFreeLeft_StrRL:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.DownstreamRight_StrRL:
                    if (data.SQ11)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                        SqDelayElapsed = false;
                    }
                    t = dataReaded.DelaySq11;
                    if (SqDelayElapsed)
                    {
                        data.SQ11Delayed = true;
                    }
                    break;
                case Dati.Steps.DownstreamLeft_StrRL:
                    if (data.SQ10)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                        SqDelayElapsed = false;
                    }
                    t = dataReaded.DelaySq10;
                    if (SqDelayElapsed)
                    {
                        data.SQ10Delayed = true;
                    }
                    break;
                case Dati.Steps.LiftIntCmd_StrRL:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.RxUpstr_StrStr:
                    if (data.SQ05)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                    }
                    t = dataReaded.DelaySq05;
                    if (SqDelayElapsed)
                    {
                        data.SQ05Delayed = true;
                    }
                    break;
                case Dati.Steps.WaitDown_StrStr:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.DownCmd_StrStr:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.Transfer_StrStr:
                    if (data.SQ07)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                    }
                    t = dataStoreClass.DelaySq07;
                    if (SqDelayElapsed)
                    {
                        data.SQ07Delayed = true;
                    }
                    break;
                case Dati.Steps.IntCmd_StrStr:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.LiftCmd_RLStr:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.WaitRxRight_RLStr:
                    if (data.SQ06Right)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                    }
                    t = dataReaded.DelaySq06Right;
                    if (SqDelayElapsed)
                    {
                        data.SQ06RightDelayed = true;
                    }
                    break;
                case Dati.Steps.WaitRxLeft_RLStr:
                    if (data.SQ06Left)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                    }
                    t = dataReaded.DelaySq06Left;
                    if (SqDelayElapsed)
                    {
                        data.SQ06LeftDelayed = true;
                    }
                    break;
                case Dati.Steps.LiftIntCmd_RLStr:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.WaitStrFree_RLStr:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.DownCmd_RLStr:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.Transfer_Str_RLStr:
                    if (data.SQ07)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                    }
                    t = dataReaded.DelaySq07;
                    if (SqDelayElapsed)
                    {
                        data.SQ07Delayed = true;
                    }
                    break;
                case Dati.Steps.LiftIntEndCmd_RLStr:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.LiftUpCmd_RL:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.WaitRx_RL:
                    if (data.SQ10)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                    }
                    t = dataReaded.DelaySq10;
                    if (SqDelayElapsed)
                    {
                        data.SQ10Delayed = true;
                    }
                    break;
                case Dati.Steps.LiftIntCmd_RL:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.LiftUpCmd_LR:
                    timerSqDelay.Enabled = false;
                    break;
                case Dati.Steps.WaitRx_LR:
                    if (data.SQ11)
                    {
                        timerSqDelay.Enabled = true;
                    }
                    else
                    {
                        timerSqDelay.Enabled = false;
                    }
                    t = dataReaded.DelaySq11;
                    if (SqDelayElapsed)
                    {
                        data.SQ11Delayed = true;
                    }
                    break;
                case Dati.Steps.LiftIntCmd_LR:
                    timerSqDelay.Enabled = false;
                    break;
                default:
                    break;
            }
            return t;
        }
        private void timerSqDelay_Tick(object sender, EventArgs e)
        {
            if (!SqDelayElapsed)
            {
                SqDelayElapsed = true;
            }
        }

        // RESET
        private bool GeneralResetTp()
        {
            bool resetDone = false;

            data.ActualStep = Dati.Steps.NoCycle;
            data.MainFunction = Dati.MainFunctions.NoCycle;
            data.StartCycle = Dati.startCycle.NoCycle;
            data.Outcmd = Dati.OutCmd.IntCmd;
            TimeoutSet(data.timeoutTime);

            SQ05Check.BackColor = this.BackColor;
            SQ06RightCheck.BackColor = this.BackColor;
            SQ06LeftCheck.BackColor = this.BackColor;
            SQ07Check.BackColor = this.BackColor;
            SQ10Check.BackColor = this.BackColor;
            SQ11Check.BackColor = this.BackColor;
            SQ01UpstreamCeck.BackColor = this.BackColor;
            data.MustOn = Dati.SensorMustOn.MustOff;
            timerSqDelay.Interval = 10;
            data.SQ05Delayed = false;
            ManMotRightOn = false;
            ManMotLeftOn = false;

            if (StartCycleList.SelectedIndex >= 0)
            {
                StartCycleList.SetSelected(StartCycleList.SelectedIndex, false);
            }
            if (data.Inputpos == Dati.InputPos.IntPos & data.Inputpos != Dati.InputPos.UpPos & data.Inputpos != Dati.InputPos.DownPos)
            {
                resetDone = true;
                TimeoutReset();
                TpCmdOutputs(Dati.OutCmd.NoCmd);
                MotCmdOutputs(Dati.OutMotCmd.NoCmd);
            }
            return resetDone;
        }

        // INIT 
        public void SetInitVar()
        {
            data.ManInputpos = Dati.ManInputPos.IntPos;
            TpCmdOutputs(Dati.OutCmd.NoCmd);
            MotCmdOutputs(Dati.OutMotCmd.NoCmd);
            data.timeoutTime = 10000;
            data.MustOn = Dati.SensorMustOn.MustOff;
            data.AutManMode = Dati.AutMan.Automatic;
            data.ManOutcmd = Dati.ManOutCmd.NoCmd;
            data.ManOutMotcmd = Dati.ManOutMotCmd.NoCmd;
            timerSqDelay.Interval = 10;
            data.SQ05Delayed = false;
            ManMotRightOn = false;
            ManMotLeftOn = false;

            // manual buttons
            btnAutMan.BackColor = Color.Black;
            btnAutMan.ForeColor = Color.White;

            btnUp.Visible = false;
            btnInt.Visible = false;
            btnDown.Visible = false;
            btnRight.Visible = false;
            btnLeft.Visible = false;

            labelManualCmd.Visible = false;
            labelManualTpCmd.Visible = false;
            labelManualMtCmd.Visible = false;

            data.lastAutManMode = Dati.LastAutManMode.Aut;
            data.CycleMode = Dati.cycleMode.startFromSq01;
        }

        // GENERAL STATUS
        private void GeneralStatusTp()
        {
            if (data.ActualStep == Dati.Steps.NoCycle)
            {
                // start position
                if (data.Inputpos == Dati.InputPos.IntPos & data.Inputpos != Dati.InputPos.UpPos & data.Inputpos != Dati.InputPos.DownPos)
                {
                    data.Statustp = Dati.StatusTp.Free;
                }
                else
                {
                    data.Statustp = Dati.StatusTp.InAlarm;
                }
            }
            else
            {
                if (GenTimeElapsed)
                {
                    data.Statustp = Dati.StatusTp.InAlarm;
                }
                else
                {
                    data.Statustp = Dati.StatusTp.Busy;
                }
            }
            // Main lable color
            switch (data.Statustp)
            {
                case Dati.StatusTp.Free:
                    label5.BackColor = Color.Aqua;
                    break;
                case Dati.StatusTp.Busy:
                    label5.BackColor = Color.Yellow;
                    break;
                case Dati.StatusTp.InAlarm:
                    label5.BackColor = Color.Red;
                    break;
                default:
                    break;
            }
            switch (data.AutManMode)
            {
                case Dati.AutMan.Manual:
                    label5.BackColor = Color.Orange;
                    break;
                case Dati.AutMan.Automatic:
                    break;
                default:
                    break;
            }
        }

        // ALARMS
        private Dati.AlarmList Alarms(Dati.PosToCheckAlarm Check, bool timeout)
        {
            if (!timeout & data.AutManMode == Dati.AutMan.Automatic)
            {
                switch (Check)
                {
                    case Dati.PosToCheckAlarm.ceckMustUp:
                        if (data.Inputpos != Dati.InputPos.UpPos)
                        {
                            data.Alarmlist = Dati.AlarmList.WrongPosMustUp;
                        }
                        else
                        {
                            data.Alarmlist = Dati.AlarmList.NoAlarms;
                            textBox3.BackColor = this.BackColor;
                        }
                        break;
                    case Dati.PosToCheckAlarm.ceckMustInt:
                        if (data.Inputpos != Dati.InputPos.IntPos)
                        {
                            data.Alarmlist = Dati.AlarmList.WrongPosMustInt;
                        }
                        else
                        {
                            data.Alarmlist = Dati.AlarmList.NoAlarms;
                            textBox3.BackColor = this.BackColor;
                        }
                        break;
                    case Dati.PosToCheckAlarm.ceckMustDown:
                        if (data.Inputpos != Dati.InputPos.DownPos)
                        {
                            data.Alarmlist = Dati.AlarmList.WrongPosMustDown;
                        }
                        else
                        {
                            data.Alarmlist = Dati.AlarmList.NoAlarms;
                            textBox3.BackColor = this.BackColor;
                        }
                        break;
                    case Dati.PosToCheckAlarm.noCeck:
                        data.Alarmlist = Dati.AlarmList.NoAlarms;
                        textBox3.BackColor = this.BackColor;
                        break;
                    default:
                        data.Alarmlist = Dati.AlarmList.NoAlarms;
                        textBox3.BackColor = this.BackColor;
                        break;
                }
                if (data.Alarmlist != Dati.AlarmList.NoAlarms)
                {
                    textBox3.BackColor = Color.Tomato;
                }
            }
            textBox3.Text = data.Alarmlist.ToString();
            return data.Alarmlist;
        }

        // BLINKING
        private void timerDelay_Tick(object sender, EventArgs e)
        {
            blink(data.MustOn, data.AutManMode);
        }
        private bool blink(Dati.SensorMustOn val, Dati.AutMan mode)
        {
            if (mode == Dati.AutMan.Automatic)
            {
                switch (val)
                {
                    case Dati.SensorMustOn.SQ05MustOn:
                        if (blinkOn)
                        {
                            blinkOn = false;
                            SQ05Check.BackColor = this.BackColor;
                        }
                        else
                        {
                            blinkOn = true;
                            SQ05Check.BackColor = Color.Orange;
                        }
                        break;
                    case Dati.SensorMustOn.SQ06LeftMustOn:
                        if (blinkOn)
                        {
                            blinkOn = false;
                            SQ06LeftCheck.BackColor = this.BackColor;
                        }
                        else
                        {
                            blinkOn = true;
                            SQ06LeftCheck.BackColor = Color.Orange;
                        }
                        break;
                    case Dati.SensorMustOn.SQ06RightMustOn:
                        if (blinkOn)
                        {
                            blinkOn = false;
                            SQ06RightCheck.BackColor = this.BackColor;
                        }
                        else
                        {
                            blinkOn = true;
                            SQ06RightCheck.BackColor = Color.Orange;
                        }
                        break;
                    case Dati.SensorMustOn.SQ07MustOn:
                        if (blinkOn)
                        {
                            blinkOn = false;
                            SQ07Check.BackColor = this.BackColor;
                        }
                        else
                        {
                            blinkOn = true;
                            SQ07Check.BackColor = Color.Orange;
                        }
                        break;
                    case Dati.SensorMustOn.SQ10MustOn:
                        if (blinkOn)
                        {
                            blinkOn = false;
                            SQ10Check.BackColor = this.BackColor;
                        }
                        else
                        {
                            blinkOn = true;
                            SQ10Check.BackColor = Color.Orange;
                        }
                        break;
                    case Dati.SensorMustOn.SQ11MustOn:
                        if (blinkOn)
                        {
                            blinkOn = false;
                            SQ11Check.BackColor = this.BackColor;
                        }
                        else
                        {
                            blinkOn = true;
                            SQ11Check.BackColor = Color.Orange;
                        }
                        break;
                    case Dati.SensorMustOn.SQ01MustOn:
                        if (blinkOn)
                        {
                            blinkOn = false;
                            SQ01UpstreamCeck.BackColor = this.BackColor;
                        }
                        else
                        {
                            blinkOn = true;
                            SQ01UpstreamCeck.BackColor = Color.Orange;
                        }
                        break;
                    case Dati.SensorMustOn.MustOff:
                        SQ05Check.BackColor = this.BackColor;
                        SQ06RightCheck.BackColor = this.BackColor;
                        SQ06LeftCheck.BackColor = this.BackColor;
                        SQ07Check.BackColor = this.BackColor;
                        SQ10Check.BackColor = this.BackColor;
                        SQ11Check.BackColor = this.BackColor;
                        SQ01UpstreamCeck.BackColor = this.BackColor;
                        break;
                    case Dati.SensorMustOn.DownMustOn:
                        if (blinkOn)
                        {
                            blinkOn = false;
                            pictureDownCmd.Hide();
                        }
                        else
                        {
                            blinkOn = true;
                            pictureDownCmd.Show();
                        }
                        break;
                    case Dati.SensorMustOn.UpMustOn:
                        if (blinkOn)
                        {
                            blinkOn = false;
                            pictureUpCmd.Hide();
                        }
                        else
                        {
                            blinkOn = true;
                            pictureUpCmd.Show();
                        }
                        break;
                    case Dati.SensorMustOn.IntMustOn:
                        if (blinkOn)
                        {
                            blinkOn = false;
                            pictureIntCmd.Hide();
                        }
                        else
                        {
                            blinkOn = true;
                            pictureIntCmd.Show();
                        }
                        break;
                    case Dati.SensorMustOn.NoVisualization:
                        blinkOn = false;
                        break;
                    default:
                        break;
                }
            }
            return blinkOn;
        }

        // LOAD CONFIG FILE
        private void loadConfig()
        {
            data.timeoutTime = readWriteStore.ReadConfig<ConfigData>(fileName).GeneralTimeoutTime;
        }
    }
}
