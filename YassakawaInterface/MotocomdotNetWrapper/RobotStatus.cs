using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotocomdotNetWrapper
{
    public class RobotStatus
    {
        #region MEMBERS
        public bool IsStep { get; set; }

        public bool Is1Cycle { get; set; }

        public bool IsAuto { get; set; }

        public bool IsOperating { get; set; }

        public bool IsSafeSpeed { get; set; }

        public bool IsTeach { get; set; }

        public bool IsPlay { get; set; }

        public bool IsCommandRemote { get; set; }

        public bool IsPlaybackBoxHold { get; set; }

        public bool IsExternalHold { get; set; }

        public bool IsPPHold { get; set; }

        public bool IsCommandHold { get; set; }

        public bool IsAlarm { get; set; }

        public bool IsError { get; set; }

        public bool IsServoOn { get; set; }
        #endregion MEMBERS

        public RobotStatus()
        {

        }
    }
}
