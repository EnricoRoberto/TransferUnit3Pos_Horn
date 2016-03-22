using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferUnit3Pos
{
    public class Dati
    {
        // DATA TYPES
        public enum cycleMode
        {
            startFromSq01,
            startFromSq05,
        }

        public enum startCycle
        {
            // not in cycle
            NoCycle,
            fillTp,             // piece go to tp, afther will be adressed
            cycleStr_R,         // cycle str-> right     
            cycleStr_L,         // cycle str-> left     
            cycleStr_Str,       // cycle str-> str       
            cycleR_Str,         // cycle right -> str 
            cycleL_Str,         // cycle left -> str 
            cycleR_L,           // cycle right -> left        
            cycleL_R,           // cycle left -> right
        }
        public enum MainFunctions
        {
            NoCycle,
            fillTp,             // piece go to tp, afther will be adressed
            cycleStr_R,         // cycle str-> right     
            cycleStr_L,         // cycle str-> left     
            cycleStr_Str,       // cycle str-> str       
            cycleR_Str,         // cycle right -> str 
            cycleL_Str,         // cycle left -> str 
            cycleR_L,           // cycle right -> left        
            cycleL_R,           // cycle left -> right
        }
        public enum ManMainFunctions
        {
            NoCycle,
            fillTp,             // piece go to tp, afther will be adressed
            cycleStr_R,         // cycle str-> right     
            cycleStr_L,         // cycle str-> left     
            cycleStr_Str,       // cycle str-> str       
            cycleR_Str,         // cycle right -> str 
            cycleL_Str,         // cycle left -> str 
            cycleR_L,           // cycle right -> left        
            cycleL_R,           // cycle left -> right
        }

        public enum Steps
        {
            // Str  mean Strait direction
            // Rx   mean receive operation
            // Wait mean waiting of signals/free sensors ...
            // Cmd  mean command ( valve command, movemente....)
            // RL   mean from right to left direction
            // LR   mean from left to right direction
            // StrRL mean stright- right/left

            // not in cycle
            NoCycle,

            // wait addressing
            RxWaitAddrTp,
            WaitAddrTp,

            // cycle str-> right / left     StrRL
            RxUpstr_StrRL,
            WaitLift_StrRL,
            LiftCmd_StrRL,
            WaitFreeRight_StrRL,
            WaitFreeLeft_StrRL,
            DownstreamRight_StrRL,
            DownstreamLeft_StrRL,
            LiftIntCmd_StrRL,

            // cycle str-> str              StrStr
            RxUpstr_StrStr,
            WaitDown_StrStr,
            DownCmd_StrStr,
            Transfer_StrStr,
            IntCmd_StrStr,

            // cycle right/left -> str      RLStr
            LiftCmd_RLStr,
            WaitRxRight_RLStr,
            WaitRxLeft_RLStr,
            LiftIntCmd_RLStr,
            WaitStrFree_RLStr,
            DownCmd_RLStr,
            Transfer_Str_RLStr,
            LiftIntEndCmd_RLStr,

            // cycle right -> left          RL
            LiftUpCmd_RL,
            WaitRx_RL,
            LiftIntCmd_RL,

            // cycle left -> right          LR
            LiftUpCmd_LR,
            WaitRx_LR,
            LiftIntCmd_LR,

            // Horn function
            WaitEndHorn,
        }

        public enum InputPos
        {
            // Inputs, positions of the transfer unit
            UpPos,
            IntPos,
            DownPos,
        }
        public enum ManInputPos
        {
            // Manual force Inputs, positions of the transfer unit
            UpPos,
            IntPos,
            DownPos,
        }

        public enum OutCmd
        {
            // Outputs commands of the transfer unit
            UpCmd,
            IntCmd,
            DownCmd,
            NoCmd,
        }
        public enum LastOutCmd
        {
            // Outputs commands of the transfer unit
            UpCmd,
            IntCmd,
            DownCmd,
            NoCmd,
        }

        public enum OutMotCmd
        {
            // Outputs commands of the transfer unit
            FwCmd,
            BckwCmd,
            NoCmd,
        }

        public enum ManOutCmd
        {
            // Outputs commands of the transfer unit
            UpCmd,
            IntCmd,
            DownCmd,
            NoCmd,
        }
        public enum LastManOutCmd
        {
            // Outputs commands of the transfer unit
            UpCmd,
            IntCmd,
            DownCmd,
            NoCmd,
        }
        public enum ManOutMotCmd
        {
            // Outputs commands of the motor
            FwCmd,
            BckwCmd,
            NoCmd,
        }

        public enum StatusTp
        {
            Free,
            Busy,
            InAlarm,
        }
        public enum AlarmList
        {
            NoAlarms,
            WrongPosMustUp,
            WrongPosMustInt,
            WrongPosMustDown,
            TimeoutStep,
        }
        public enum PosToCheckAlarm
        {
            ceckMustUp,
            ceckMustInt,
            ceckMustDown,
            noCeck,
        }
        public enum ValveType
        {
            OneState,
            DoubleState,
            CenterOpenState,
            CenterCloseState,
        }

        public enum SensorMustOn
        {
            SQ05MustOn,
            SQ06LeftMustOn,
            SQ06RightMustOn,
            SQ07MustOn,
            SQ10MustOn,
            SQ11MustOn,
            SQ01MustOn,
            MustOff,
            UpMustOn,
            IntMustOn,
            DownMustOn,
            NoVisualization,
        }
        public enum SensorOn
        {
            SQ05On,
            SQ06LeftOn,
            SQ06RightOn,
            SQ07On,
            SQ10On,
            SQ11On,
            SQ01On,
        }

        public enum AutMan
        {
            Manual,
            Automatic,
        }
        public enum LastAutManMode
        {
            Aut,
            Man,
        }

        // VARIABLES
        public startCycle StartCycle = new startCycle();

        public MainFunctions MainFunction = new MainFunctions();
        public ManMainFunctions ManMainFunction = new ManMainFunctions();

        public Steps ActualStep = new Steps();

        public InputPos Inputpos = new InputPos();
        public ManInputPos ManInputpos = new ManInputPos();

        public OutCmd Outcmd = new OutCmd();
        public ManOutCmd ManOutcmd = new ManOutCmd();

        public OutMotCmd OutMotcmd = new OutMotCmd();
        public ManOutMotCmd ManOutMotcmd = new ManOutMotCmd();

        public StatusTp Statustp = new Dati.StatusTp();
        public AlarmList Alarmlist = new AlarmList();
        public PosToCheckAlarm CheckPos = new PosToCheckAlarm();

        public SensorMustOn MustOn = new SensorMustOn();
        public SensorOn On = new SensorOn();

        public AutMan AutManMode = new Dati.AutMan();
        public LastAutManMode lastAutManMode = new LastAutManMode();

        public cycleMode CycleMode = new Dati.cycleMode();

        public int timeoutTime = new int();

        public bool ManSQ05;
        public bool ManSQ06Left;
        public bool ManSQ06Right;
        public bool ManSQ07;
        public bool ManSQ10;
        public bool ManSQ11;
        public bool ManSQ01;
        public bool SQ05;
        public bool SQ06Left;
        public bool SQ06Right;
        public bool SQ07;
        public bool SQ10;
        public bool SQ11;
        public bool SQ01;

        public bool SQ05Delayed;
        public bool SQ06LeftDelayed;
        public bool SQ06RightDelayed;
        public bool SQ07Delayed;
        public bool SQ10Delayed;
        public bool SQ11Delayed;
        public bool SQ01Delayed;

        public bool MoveEnable;


    }
}
