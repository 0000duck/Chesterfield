using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MotoCom32Net
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(true)]
    [Guid("7B292C73-345D-423D-BBB2-C3B7AAC328EE")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IRobotStatus
    {
        #region Properties
        bool IsStep
        {
            get;
            set;
        }
        bool Is1Cycle
        {
            get;
            set;
        }
        bool IsAuto
        {
            get;
            set;
        }
        bool IsOperating
        {
            get;
            set;
        }
        bool IsSafeSpeed
        {
            get;
            set;
        }
        bool IsTeach
        {
            get;
            set;
        }
        bool IsPlay
        {
            get;
            set;
        }
        bool IsCommandRemote
        {
            get;
            set;
        }
        bool IsPlaybackBoxHold
        {
            get;
            set;
        }
        bool IsExternalHold
        {
            get;
            set;
        }
        bool IsPPHold
        {
            get;
            set;
        }
        bool IsCommandHold
        {
            get;
            set;
        }
        bool IsAlarm
        {
            get;
            set;
        }
        bool IsError
        {
            get;
            set;
        }
        bool IsServoOn
        {
            get;
            set;
        }
        #endregion

        #region Methods

        #endregion
    }
    [ComVisible(true)]
    public class RobotStatus : IRobotStatus
    {
        private bool _isStep = false;
        private bool _is1Cycle = false;
        private bool _isAuto = false;
        private bool _isOperating = false;
        private bool _isSafeSpeed = false;
        private bool _isTeach = false;
        private bool _isPlay = false;
        private bool _isCommandRemote = false;
        private bool _isPlaybackBoxHold = false;
        private bool _isExternalHold = false;
        private bool _isPPHold = false;
        private bool _isCommandHold = false;
        private bool _isAlarm = false;
        private bool _isError = false;
        private bool _isServoOn = false;
        private bool _isHold = false;
        #region Properties
        public bool IsStep
        {
            get
            {
                return _isStep;
            }
            set
            {
                _isStep = value;
            }
        }
        public bool Is1Cycle
        {
            get
            {
                return _is1Cycle;
            }
            set
            {
                _is1Cycle = value;
            }
        }
        public bool IsAuto
        {
            get
            {
                return _isAuto;
            }
            set
            {
                _isAuto = value;
            }
        }
        public bool IsOperating
        {
            get
            {
                return _isOperating;
            }
            set
            {
                _isOperating = value;
            }
        }
        public bool IsSafeSpeed
        {
            get
            {
                return _isSafeSpeed;
            }
            set
            {
                _isSafeSpeed = value;
            }
        }
        public bool IsTeach
        {
            get
            {
                return _isTeach;
            }
            set
            {
                _isTeach = value;
            }
        }
        public bool IsPlay
        {
            get
            {
                return _isPlay;
            }
            set
            {
                _isPlay = value;
            }
        }
        public bool IsCommandRemote
        {
            get
            {
                return _isCommandRemote;
            }
            set
            {
                _isCommandRemote = value;
            }
        }
        public bool IsPlaybackBoxHold
        {
            get
            {
                return _isPlaybackBoxHold;
            }
            set
            {
                _isPlaybackBoxHold = value;
            }
        }
        public bool IsExternalHold
        {
            get
            {
                return _isExternalHold;
            }
            set
            {
                _isExternalHold = value;
            }
        }
        public bool IsPPHold
        {
            get
            {
                return _isPPHold;
            }
            set
            {
                _isPPHold = value;
            }
        }
        public bool IsCommandHold
        {
            get
            {
                return _isCommandHold;
            }
            set
            {
                _isCommandHold = value;
            }
        }
        public bool IsAlarm
        {
            get
            {
                return _isAlarm;
            }
            set
            {
                _isAlarm = value;
            }
        }
        public bool IsError
        {
            get
            {
                return _isError;
            }
            set
            {
                _isError = value;
            }
        }
        public bool IsServoOn
        {
            get
            {
                return _isServoOn;
            }
            set
            {
                _isServoOn = value;
            }
        }
        public bool IsHold
        {
            get
            {
                _isHold = (_isPlaybackBoxHold || _isPPHold || _isExternalHold || _isCommandHold);
                return _isHold;
            }
            set
            {
                _isHold = value;
            }
        }
        #endregion

        #region Constructor
        public RobotStatus()
        {

        }
        #endregion
    }
}
