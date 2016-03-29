using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TransferUnit3Pos
{
    [DataContract]
   public class ConfigData
    {
        [DataMember]
        public int DelaySq01 { get; set; } = 10;
        [DataMember]
        public int DelaySq05 { get; set; } = 10;
        [DataMember]
        public int DelaySq06Right { get; set; } = 10;
        [DataMember]
        public  int DelaySq06Left { get; set; } = 10;
        [DataMember]
        public int DelaySq10 { get; set; } = 10;
        [DataMember]
        public  int DelaySq11 { get; set; } = 10;
        [DataMember]
        public  int DelaySq07 { get; set; } = 10;
        [DataMember]
        public int DelayStart { get; set; } =10;
        [DataMember]
        public  int TimeoutStep { get; set; } =10;
        [DataMember]
        public int DalayStartCycle { get; set; } = 10;
        [DataMember]
        public  int GeneralTimeoutTime { get; set; } = 99;
        [DataMember]
        public string CycleMode { get; set; } = "startFromSq01";
    }
}
