using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System.Collections.ObjectModel;

using RoboDk;
using RoboDk.API;
using RoboDk.API.Model;
using RoboDk.API.Exceptions;

namespace YaskawaNet
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(true)]
    [Guid("28AB703B-42F0-4556-BB0F-32A2D7972CFC")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IDX200
    {
        #region Properties
        string IPAddress
        {
            get;
            set;
        }
        short ToolNumber
        {
            get;
            set;
        }
        string CommDir
        {
            get;
            set;
        }
        CommunicationStatus CommunicationStatus
        {
            get;
            set;
        }
        bool UseRoboDKSimulator
        {
            [return: MarshalAs(UnmanagedType.Bool)]
            get;
            [param: MarshalAs(UnmanagedType.Bool)]
            set;
        }
        RobotFrameType ActualReferenceFrame
        {
            get;
            set;
        }
        Configuration ActualRConf
        {
            get;
            set;
        }
        RobotModeType ActualMode
        {
            get;
            set;
        }
        RobotStatusInformation ActualRobotStatus
        {
            get;
            set;
        }

        RobotPositionVariable ActualRobotPositionVariable
        {
            get;
            set;
        }
        RobotPositionVariable DesiredRobotPositionVariable
        {
            get;
            set;
        }
        RobotPositionVariable ReportedRobotPositionVariable
        {
            get;
            set;
        }

        RobotPosition ActualRobotTCPPosition
        {
            get;
            set;
        }
        RobotPosition DesiredRobotTCPPosition
        {
            get;
            set;
        }
        RobotPosition ReportedRobotTCPPosition
        {
            get;
            set;
        }

        RobotPosition ActualRobotJointPosition
        {
            get;
            set;
        }
        RobotPosition DesiredRobotJointPosition
        {
            get;
            set;
        }
        RobotPosition ReportedRobotJointPosition
        {
            get;
            set;
        }

        RobotPosition ActualRobotIncrementPosition
        {
            get;
            set;
        }
        RobotPosition DesiredRobotIncrementPosition
        {
            get;
            set;
        }
        RobotPosition ReportedRobotIncrementPosition
        {
            get;
            set;
        }

        RobotMoveSpeedSelectionType ActualMoveSpeedSelection
        {
            get;
            set;
        }

        RobotMotionType ActualMotionType
        {
            get;
            set;
        }

        double ActualRobotSpeed
        {
            get;
            set;
        }

        RobotPosition[] PositionTrajectory
        {
            get;
            set;
        }

        #endregion

        #region Methods
        short Initialize();
        short Connect();
        short Disconnect();
        short SetServer();
        void SetServoOn();
        void SetServoOff();
        short JointJogMove(int joint, double speed);
        short JointsJogMove(int jointMask, double speed);
        short JointAbsoluteMove(int jointIndex, double speed);
        short JointsAbsoluteMove(int jointMask, double speed);
        short JointRelativeMove(int jointIndex, double speed);
        short JointsRelativeMove(int jointMask, double speed);
        short JointHomeMove(int jointIndex, double speed);
        short TCPJogMove(int tcpIndex, double speed);
        short TCPAbsoluteMove(int tcpIndex, double speed);
        short TCPRelativeMove(int tcpIndex, double speed);
        short TrackLinearJogMove(int jointIndex, double speed);
        short TurnTableJogMove(int jointIndex, double speed);
        short HoldOn();
        short HoldOff();

        [return: MarshalAs(UnmanagedType.Bool)]
        bool IsInMotion(int axis, int frame);

        #endregion
    }
    /// <summary>
    /// DX200 robot controller implementation
    /// </summary>
    [ComVisible(true)]
    public class DX200 : IDX200, INotifyPropertyChanged
    {
        //TODO:set diagnostic exception hadler for all functions
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Fields

        private short _robotHandler = -1;
        private bool _useRoboDKSimulator = false;
        private string _commDir;
        private string _path;
        private string _ipAddress = "";
        private CommunicationStatus _communicationStatus = new CommunicationStatus();
        private volatile int _connectionCheckCounter = 0;
        private RobotFrameType _actualReferenceFrame = RobotFrameType.Base;
        private Dictionary<RobotFrameType, string> _frameDictionary = new Dictionary<RobotFrameType, string>()
        {
            #region
		    {RobotFrameType.Base,"BASE"},
            {RobotFrameType.Robot,"ROBOT"},
            {RobotFrameType.User1,"UF1"},
            {RobotFrameType.User2,"UF2"},
            {RobotFrameType.User3,"UF3"},
            {RobotFrameType.User4,"UF4"},
            {RobotFrameType.User5,"UF5"},
            {RobotFrameType.User6,"UF6"},
            {RobotFrameType.User7,"UF7"},
            {RobotFrameType.User8,"UF8"},
            {RobotFrameType.User9,"UF9"},
            {RobotFrameType.User10,"UF10"},
            {RobotFrameType.User11,"UF11"},
            {RobotFrameType.User12,"UF12"},
            {RobotFrameType.User13,"UF13"},
            {RobotFrameType.User14,"UF14"},
            {RobotFrameType.User15,"UF15"},
            {RobotFrameType.User16,"UF16"},
            {RobotFrameType.User17,"UF17"},
            {RobotFrameType.User18,"UF18"},
            {RobotFrameType.User19,"UF19"},
            {RobotFrameType.User20,"UF20"},
            {RobotFrameType.User21,"UF21"},
            {RobotFrameType.User22,"UF22"},
            {RobotFrameType.User23,"UF23"},
            {RobotFrameType.User24,"UF24"},
            {RobotFrameType.User25,"UF25"},
            {RobotFrameType.User26,"UF26"},
            {RobotFrameType.User27,"UF27"},
            {RobotFrameType.User28,"UF28"},
            {RobotFrameType.User29,"UF29"},
            {RobotFrameType.User30,"UF30"},
            {RobotFrameType.User31,"UF31"},
            {RobotFrameType.User32,"UF32"},
            {RobotFrameType.User33,"UF33"},
            {RobotFrameType.User34,"UF34"},
            {RobotFrameType.User35,"UF35"},
            {RobotFrameType.User36,"UF36"},
            {RobotFrameType.User37,"UF37"},
            {RobotFrameType.User38,"UF38"},
            {RobotFrameType.User39,"UF39"},
            {RobotFrameType.User40,"UF40"},
            {RobotFrameType.User41,"UF41"},
            {RobotFrameType.User42,"UF42"},
            {RobotFrameType.User43,"UF43"},
            {RobotFrameType.User44,"UF44"},
            {RobotFrameType.User45,"UF45"},
            {RobotFrameType.User46,"UF46"},
            {RobotFrameType.User47,"UF47"},
            {RobotFrameType.User48,"UF48"},
            {RobotFrameType.User49,"UF49"},
            {RobotFrameType.User50,"UF50"},
            {RobotFrameType.User51,"UF51"},
            {RobotFrameType.User52,"UF52"},
            {RobotFrameType.User53,"UF53"},
            {RobotFrameType.User54,"UF54"},
            {RobotFrameType.User55,"UF55"},
            {RobotFrameType.User56,"UF56"},
            {RobotFrameType.User57,"UF57"},
            {RobotFrameType.User58,"UF58"},
            {RobotFrameType.User59,"UF59"},
            {RobotFrameType.User60,"UF60"},
            {RobotFrameType.User61,"UF61"},
            {RobotFrameType.User62,"UF62"} ,
            {RobotFrameType.User63,"UF63"} ,
            {RobotFrameType.User64,"UF64"} ,
            {RobotFrameType.Tool,"TOOL"} ,
            {RobotFrameType.MasterTool,"MASTERTOOL"} 
	#endregion
        };
        private Dictionary<RobotMoveSpeedSelectionType, string> _moveSpeedSelectionDictionary = new Dictionary<RobotMoveSpeedSelectionType, string>()
        {
            {RobotMoveSpeedSelectionType.ControlPoint,"V"},
            {RobotMoveSpeedSelectionType.PositionAngular,"VR"},
            {RobotMoveSpeedSelectionType.JointSpeed,"VJ"}
        };
        private Dictionary<RobotMotionType, string> _motionDictionary = new Dictionary<RobotMotionType, string>()
        {
            {RobotMotionType.JointMotion,"MOVJ"},
            {RobotMotionType.LinearMotion,"MOVL"}
        };

        private Configuration _actualRConf = new Configuration(0);
        private RobotStatusInformation _actualRobotStatus = new RobotStatusInformation();

        #region Thread timers
        System.Threading.Timer _statusTimer = null;
        readonly object _lockStatusTimer = new object();

        System.Threading.Timer _getPositionsTimer = null;
        readonly object _lockGetPositionsTimer = new object();

        private int _statusTimerInterval = 100;
        private int _getPositionsTimerInterval = 100;
        #endregion

        private short _oldStatusD1 = -1;
        private short _oldStatusD2 = -1;
        private static Object _FileAccessDirLock = new Object();
        private Object _DX200AccessLock = new Object();

        private RobotModeType _actualMode = RobotModeType.TEACH;

        private MotionDirection _actualMotionDirection = MotionDirection.None;

        private double _stepJogging = 50.0;

        #region Positions variables
        readonly object _desiredRobotJointPositionLocker = new object();
        readonly object _reportedRobotJointPositionLocker = new object();
        readonly object _reportedRobotCartesianPositionLocker = new object();
        readonly object _reportedSJointPositionLocker = new object();

        private RobotPositionVariable _actualRobotPositionVariable = new RobotPositionVariable();
        private RobotPositionVariable _desiredRobotPositionVariable = new RobotPositionVariable();
        private RobotPositionVariable _reportedRobotPositionVariable = new RobotPositionVariable();

        private volatile RobotPosition _actualRobotTCPPosition = new RobotPosition();
        private volatile RobotPosition _desiredRobotTCPPosition = new RobotPosition();
        private volatile RobotPosition _reportedRobotTCPPosition = new RobotPosition();

        private volatile RobotPosition _actualRobotJointPosition = new RobotPosition();
        private volatile RobotPosition _desiredRobotJointPosition = new RobotPosition();
        private volatile RobotPosition _reportedRobotJointPosition = new RobotPosition();

        private volatile RobotPosition _actualRobotTCPPositionSimulator = new RobotPosition();
        private volatile RobotPosition _desiredRobotTCPPositionSimulator = new RobotPosition();
        private volatile RobotPosition _reportedRobotTCPPositionSimulator = new RobotPosition();

        private volatile RobotPosition _actualRobotJointPositionSimulator = new RobotPosition();
        private volatile RobotPosition _desiredRobotJointPositionSimulator = new RobotPosition();
        private volatile RobotPosition _reportedRobotJointPositionSimulator = new RobotPosition();

        private RobotPosition _actualRobotIncrementPosition = new RobotPosition();
        private RobotPosition _desiredRobotIncrementPosition = new RobotPosition();
        private RobotPosition _reportedRobotIncrementPosition = new RobotPosition();

        private ObservableCollection<RobotPositionVariable> _variableTrajectory = new ObservableCollection<RobotPositionVariable>();
        private RobotPosition[] _positionTrajectory = new RobotPosition[5];

        #endregion

        RobotMoveSpeedSelectionType _actualMoveSpeedSelection = RobotMoveSpeedSelectionType.ControlPoint;
        RobotMotionType _actualMotionType = RobotMotionType.LinearMotion;

        UserCoordinateSystem _userCoordinateSystem_1 = new UserCoordinateSystem();

        private bool _autoStatusUpdate = false;

        private double _actualRobotSpeed = 0.0;
        private double _desiredRobotSpeed = 0.0;
        private double _reportedRobotSpeed = 0.0;

        //TODO:create speed array for joints and tcp

        private short _toolNumber = 0;

        private volatile bool _jogRunning = false;
        private volatile bool _isBusy = false;

        #endregion

        #region Properties

        public string IPAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
                OnNotifyPropertyChanged();
            }
        }
        public short ToolNumber
        {
            get
            {
                return _toolNumber;
            }
            set
            {
                _toolNumber = value;
                OnNotifyPropertyChanged();
            }
        }
        public string CommDir
        {
            get
            {
                return _commDir;
            }
            set
            {
                _commDir = value;
                OnNotifyPropertyChanged();
            }
        }

        public CommunicationStatus CommunicationStatus
        {
            get
            {
                return _communicationStatus;
            }
            set
            {
                _communicationStatus = value;
            }
        }

        public bool UseRoboDKSimulator
        {
            get
            {
                return _useRoboDKSimulator;
            }
            set
            {
                _useRoboDKSimulator = value;
                RoboDKSimulatorRun(_useRoboDKSimulator);
            }
        }

        public RobotFrameType ActualReferenceFrame
        {
            get
            {
                return _actualReferenceFrame;
            }
            set
            {
                _actualReferenceFrame = value;
                OnNotifyPropertyChanged();
            }
        }
        public Configuration ActualRConf
        {
            get
            {
                return _actualRConf;
            }
            set
            {
                _actualRConf = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotStatusInformation ActualRobotStatus
        {
            get
            {
                return _actualRobotStatus;
            }
            set
            {
                _actualRobotStatus = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotModeType ActualMode
        {
            get
            {
                return _actualMode;
            }
            set
            {
                if ((short)value > 0 && (short)value < 3)
                {
                    short returnValue = (_robotHandler != 0) ? Motocom.BscSelectMode(_robotHandler, (short)value) : (short)1;
                    _actualMode = (returnValue == 0) ? value : _actualMode;

                    OnNotifyPropertyChanged();
                }
            }
        }

        public RobotPositionVariable ActualRobotPositionVariable
        {
            get
            {
                return _actualRobotPositionVariable;
            }
            set
            {
                _actualRobotPositionVariable = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotPositionVariable DesiredRobotPositionVariable
        {
            get
            {
                return _desiredRobotPositionVariable;
            }
            set
            {
                _desiredRobotPositionVariable = value;

                OnNotifyPropertyChanged();
            }
        }
        public RobotPositionVariable ReportedRobotPositionVariable
        {
            get
            {
                return _reportedRobotPositionVariable;
            }
            set
            {
                _reportedRobotPositionVariable = value;
                OnNotifyPropertyChanged();
            }
        }

        public RobotPosition ActualRobotTCPPosition
        {
            get
            {
                return _actualRobotTCPPosition;
            }
            set
            {
                _actualRobotTCPPosition = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotPosition DesiredRobotTCPPosition
        {
            get
            {
                return _desiredRobotTCPPosition;
            }
            set
            {
                _desiredRobotTCPPosition = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotPosition ReportedRobotTCPPosition
        {
            get
            {
                return _reportedRobotTCPPosition;
            }
            set
            {
                _reportedRobotTCPPosition = value;
                OnNotifyPropertyChanged();
            }
        }

        public RobotPosition ActualRobotJointPosition
        {
            get
            {
                return _actualRobotJointPosition;
            }
            set
            {
                _actualRobotJointPosition = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotPosition DesiredRobotJointPosition
        {
            get
            {
                lock (_desiredRobotJointPositionLocker)
                {
                    Debug.WriteLine("C# module desired get:position= " + _desiredRobotJointPosition.RobotPositions[0].ToString());
                    return _desiredRobotJointPosition;
                }
            }
            set
            {
                lock (_desiredRobotJointPositionLocker)
                {
                    _desiredRobotJointPosition = value;
                    Debug.WriteLine("C# module desired set:position= " + _desiredRobotJointPosition.RobotPositions[0].ToString());
                    OnNotifyPropertyChanged();
                }
            }
        }
        public RobotPosition ReportedRobotJointPosition
        {
            get
            {
                lock (_reportedRobotJointPositionLocker)
                {
                    return _reportedRobotJointPosition;
                }
            }
            set
            {
                lock (_reportedRobotJointPositionLocker)
                {
                    _reportedRobotJointPosition = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        public RobotPosition ActualRobotTCPPositionSimulator
        {
            get
            {
                return _actualRobotTCPPositionSimulator;
            }
            set
            {
                _actualRobotTCPPositionSimulator = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotPosition DesiredRobotTCPPositionSimulator
        {
            get
            {
                return _desiredRobotTCPPositionSimulator;
            }
            set
            {
                _desiredRobotTCPPositionSimulator = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotPosition ReportedRobotTCPPositionSimulator
        {
            get
            {
                return _reportedRobotTCPPositionSimulator;
            }
            set
            {
                _reportedRobotTCPPositionSimulator = value;
                OnNotifyPropertyChanged();
            }
        }

        public RobotPosition ActualRobotJointPositionSimulator
        {
            get
            {
                return _actualRobotJointPositionSimulator;
            }
            set
            {
                _actualRobotJointPositionSimulator = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotPosition DesiredRobotJointPositionSimulator
        {
            get
            {
                lock (_desiredRobotJointPositionLocker)
                {
                    return _desiredRobotJointPositionSimulator;
                }
            }
            set
            {
                lock (_desiredRobotJointPositionLocker)
                {
                    _desiredRobotJointPositionSimulator = value;
                    OnNotifyPropertyChanged();
                }
            }
        }
        public RobotPosition ReportedRobotJointPositionSimulator
        {
            get
            {
                lock (_reportedRobotJointPositionLocker)
                {
                    return _reportedRobotJointPositionSimulator;
                }
            }
            set
            {
                lock (_reportedRobotJointPositionLocker)
                {
                    _reportedRobotJointPositionSimulator = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        public RobotPosition ActualRobotIncrementPosition
        {
            get
            {
                return _actualRobotIncrementPosition;
            }
            set
            {
                _actualRobotIncrementPosition = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotPosition DesiredRobotIncrementPosition
        {
            get
            {
                return _desiredRobotIncrementPosition;
            }
            set
            {
                _desiredRobotIncrementPosition = value;
                OnNotifyPropertyChanged();
            }
        }
        public RobotPosition ReportedRobotIncrementPosition
        {
            get
            {
                return _reportedRobotIncrementPosition;
            }
            set
            {
                _reportedRobotIncrementPosition = value;
                OnNotifyPropertyChanged();
            }
        }

        public RobotPosition[] PositionTrajectory
        {
            get
            {
                return _positionTrajectory;
            }
            set
            {
                _positionTrajectory = value;
            }
        }

        public RobotMoveSpeedSelectionType ActualMoveSpeedSelection
        {
            get
            {
                return _actualMoveSpeedSelection;
            }
            set
            {
                _actualMoveSpeedSelection = value;
                OnNotifyPropertyChanged();
            }
        }

        public RobotMotionType ActualMotionType
        {
            get
            {
                return _actualMotionType;
            }
            set
            {
                _actualMotionType = value;
                OnNotifyPropertyChanged();
            }
        }

        public bool AutoStatusUpdate
        {
            get
            {
                return _autoStatusUpdate;
            }
            set
            {
                _autoStatusUpdate = value;
                if (_autoStatusUpdate)
                {
                    //** Initialize status timer **
                    _statusTimer.Change(0, 100); //enable timer
                }
                else
                {
                    _statusTimer.Change(Timeout.Infinite, Timeout.Infinite);//timer disabled
                }
            }
        }
        public double ActualRobotSpeed
        {
            get
            {
                return _actualRobotSpeed;
            }
            set
            {
                if (value > 0 && value < 100)
                {
                    _actualRobotSpeed = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <param name="Path"></param>
        public DX200(string IPAddress, string Path)
        {
            _ipAddress = IPAddress;
            _path = Path;

            //** Initialize communication **
            short ret;

            //try to get a handle
            _robotHandler = Motocom.BscOpen(_commDir, 256);

            if (_robotHandler >= 0)
            {
                //set IP Address
                ret = Motocom.BscSetEServer(_robotHandler, _ipAddress);
                if (ret != 1)
                {
                    throw new Exception("Could not set IP address !");
                }
                else
                    throw new Exception("Could not get a handle. Check Hardware key !");

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DX200()
        {
            try
            {
                _commDir = AppDomain.CurrentDomain.BaseDirectory;
                if (_commDir.Substring(_commDir.Length - 1, 1) != "\\")
                {
                    _commDir = _commDir + "\\";
                }

                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\ScanMaster\CSI\Motion Control"))
                {
                    Object o = (key != null) ? key.GetValue("defaultIPAddress") : null;
                    _ipAddress = (o != null) ? o as String : "192.168.2.15";
                }

                _statusTimer = new System.Threading.Timer(StatusTimerTick, null, Timeout.Infinite, Timeout.Infinite);//timer disabled
                                                                                                                     //_statusTimer.Change(Timeout.Infinite, Timeout.Infinite);//timer disabled
                                                                                                                     //_statusTimer.Change(0, 50); //enable timer
                _getPositionsTimer = new System.Threading.Timer(GetPositionsTimerTick, null, Timeout.Infinite, Timeout.Infinite);//timer disabled

                _statusTimer.Change(0, 100);
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ~DX200()
        {
            short ret;

            _statusTimer.Change(Timeout.Infinite, Timeout.Infinite);//timer disabled
            _getPositionsTimer.Change(Timeout.Infinite, Timeout.Infinite);//timer disabled
            //close if there is a valid handle
            if (_robotHandler >= 0)
                ret = Motocom.BscClose(_robotHandler);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Return Value (Motocom32_Function_Reference)
        ///0 : Error
        ///1 : Normal completion
        /// </summary>
        /// <returns></returns>
        public short Connect()
        {
            short returnValue = 0;

            try
            {
                if (_robotHandler != -1 && _robotHandler != -8)
                {
                    returnValue = Motocom.BscConnect(_robotHandler);

                    _communicationStatus.connected = (returnValue > 0) ? true : false;

                    if (_communicationStatus.connected == true)
                    {
                        _statusTimer.Change(0, 100); //enable timer
                        _getPositionsTimer.Change(0, 100); //enable timer
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = 0;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// Return Value (Motocom32_Function_Reference)
        ///0 : Error
        ///1 : Normal completion
        /// </summary>
        /// <returns></returns>
        public short Disconnect()
        {
            short returnValue = 0;

            try
            {
                if (_robotHandler != -1 && _robotHandler != -8)
                {
                    returnValue = Motocom.BscDisConnect(_robotHandler);
                    if (returnValue > 0)
                    {
                        _statusTimer.Change(Timeout.Infinite, Timeout.Infinite);//timer disabled
                        _getPositionsTimer.Change(Timeout.Infinite, Timeout.Infinite);//timer disabled
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = 0;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short Initialize()
        {
            short returnValue = 0;

            try
            {
                //try to get a handle
                _robotHandler = Motocom.BscOpen(_commDir, (short)RobotCommunicationType.ETHERNET_SERVER);

                if (_robotHandler >= 0 && _ipAddress != null)
                {
                    //set IP Address
                    //Return Value
                    //0 : Error
                    //1 : Normal completion
                    returnValue = Motocom.BscSetEServer(_robotHandler, _ipAddress);
                }
                else
                {
                    returnValue = 0;
                }

                ActualMoveSpeedSelection = RobotMoveSpeedSelectionType.ControlPoint;
                ActualRobotSpeed = 0.0;
                ActualMode = RobotModeType.TEACH;
                ActualReferenceFrame = RobotFrameType.Base;

                ActualRobotJointPosition.Limits = DesiredRobotJointPosition.Limits = new double[12][]
                {
                    new double [2]{ -180.0,180.0 } ,
                    new double [2]{-105.0,155.0 } ,
                    new double [2]{-170.0,240.0 } ,
                    new double [2]{-200.0,200.0 } ,
                    new double [2]{-150.0,150.0 } ,
                    new double [2]{-455.0,455.0 },
                    new double [2]{0,3500},
                    new double [2]{-720.0,720.0},
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0}
                };
                ActualRobotTCPPosition.Limits = DesiredRobotTCPPosition.Limits = new double[12][]
                {
                    new double [2]{ -500,500  } ,
                    new double [2]{ -500, 500 } ,
                    new double [2]{ -500, 500 } ,
                    new double [2]{ -500, 500 } ,
                    new double [2]{ -500, 500 } ,
                    new double [2]{ -500,500  },
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0}
                };
            }
            catch (Exception ex)
            {
                returnValue = 0;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            bool returnValue = false;

            try
            {
                _commDir = Directory.GetCurrentDirectory();
                if (_commDir.Substring(_commDir.Length - 1, 1) != "\\")
                {
                    _commDir = _commDir + "\\";
                }

                _robotHandler = Motocom.BscOpen(_commDir, (short)RobotCommunicationType.ETHERNET_SERVER);

                returnValue = (_robotHandler != -1 && _robotHandler != -8);
            }
            catch (Exception ex)
            {
                returnValue = false;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short SetServer()
        {
            short returnValue = 0;

            try
            {
                if (_robotHandler >= 0)
                {
                    returnValue = Motocom.BscSetEServer(_robotHandler, _ipAddress);
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// Reads status of the controller
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        public void GetStatus()
        {
            short d1 = 0;
            short d2 = 0;

            try
            {
                #region
                lock (_DX200AccessLock)
                {
                    //short ret = Motocom.BscGetStatus(_robotHandler, ref d1, ref d2);

                    //if (ret == 0)
                    //{
                    #region
                    //check bits and set properties
                    //if (d1 != _oldStatusD1 || d2 != _oldStatusD2)
                    //{
                    #region
                    _actualRobotStatus.IsStep = (d1 & (1 << 0)) > 0 ? true : false;
                    _actualRobotStatus.Is1Cycle = (d1 & (1 << 1)) > 0 ? true : false;
                    _actualRobotStatus.IsAuto = (d1 & (1 << 2)) > 0 ? true : false;
                    _actualRobotStatus.IsOperating = (d1 & (1 << 3)) > 0 ? true : false;
                    _actualRobotStatus.IsSafeSpeed = (d1 & (1 << 4)) > 0 ? true : false;
                    _actualRobotStatus.IsTeach = (d1 & (1 << 5)) > 0 ? true : false;
                    _actualRobotStatus.IsPlay = (d1 & (1 << 6)) > 0 ? true : false;
                    _actualRobotStatus.IsCommandRemote = (d1 & (1 << 7)) > 0 ? true : false;
                    _actualRobotStatus.IsPlaybackBoxHold = (d2 & (1 << 0)) > 0 ? true : false;
                    _actualRobotStatus.IsPPHold = (d2 & (1 << 1)) > 0 ? true : false;
                    _actualRobotStatus.IsExternalHold = (d2 & (1 << 2)) > 0 ? true : false;
                    _actualRobotStatus.IsCommandHold = (d2 & (1 << 3)) > 0 ? true : false;
                    _actualRobotStatus.IsAlarm = (d2 & (1 << 4)) > 0 ? true : false;
                    _actualRobotStatus.IsError = (d2 & (1 << 5)) > 0 ? true : false;
                    _actualRobotStatus.IsServoOn = true;// (d2 & (1 << 6)) > 0 ? true : false;

                    _oldStatusD1 = d1;
                    _oldStatusD2 = d2;
                    #endregion
                    //}
                    #endregion
                    //}
                }
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="robotStatus"></param>
        /// <returns></returns>
        public short GetStatus(out RobotStatus robotStatus)
        {
            short returnValue = -1;
            short d1 = 0;
            short d2 = 0;
            robotStatus = new RobotStatus();

            try
            {
                lock (_DX200AccessLock)
                {
                    returnValue = Motocom.BscGetStatus(_robotHandler, ref d1, ref d2);

                    if (returnValue == 0)
                    {
                        //check bits and set properties
                        if (d1 != _oldStatusD1 || d2 != _oldStatusD2)
                        {
                            #region
                            robotStatus.IsStep = (d1 & (1 << 0)) > 0 ? true : false;
                            robotStatus.Is1Cycle = (d1 & (1 << 1)) > 0 ? true : false;
                            robotStatus.IsAuto = (d1 & (1 << 2)) > 0 ? true : false;
                            robotStatus.IsOperating = (d1 & (1 << 3)) > 0 ? true : false;
                            robotStatus.IsSafeSpeed = (d1 & (1 << 4)) > 0 ? true : false;
                            robotStatus.IsTeach = (d1 & (1 << 5)) > 0 ? true : false;
                            robotStatus.IsPlay = (d1 & (1 << 6)) > 0 ? true : false;
                            robotStatus.IsCommandRemote = (d1 & (1 << 7)) > 0 ? true : false;

                            robotStatus.IsPlaybackBoxHold = (d2 & (1 << 0)) > 0 ? true : false;
                            robotStatus.IsPPHold = (d2 & (1 << 1)) > 0 ? true : false;
                            robotStatus.IsExternalHold = (d2 & (1 << 2)) > 0 ? true : false;
                            robotStatus.IsCommandHold = (d2 & (1 << 3)) > 0 ? true : false;
                            robotStatus.IsAlarm = (d2 & (1 << 4)) > 0 ? true : false;
                            robotStatus.IsError = (d2 & (1 << 5)) > 0 ? true : false;
                            robotStatus.IsServoOn = (d2 & (1 << 6)) > 0 ? true : false;
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short GetCurrentRobotTCPPosition()
        {
            short returnValue = -1;
            double[] _currentRobotTCPPositionReal = new double[12];
            double[] _currentRobotTCPPositionSimulation = new double[12];
            double[] _currentRobotTCPPositionTemp = new double[12];
            short _rConf = 0;
            short isex = 1;
            StringBuilder _frameName = null;
            Mat pose = null;

            try
            {
                _frameName = new StringBuilder(_frameDictionary[RobotFrameType.Robot]);

                returnValue = Motocom.BscIsRobotPos(_robotHandler, _frameName, isex, ref _rConf, ref _toolNumber, ref _currentRobotTCPPositionReal[0]);

                if (_useRoboDKSimulator)
                {
                    #region
                    pose = _roboDKRobot.Pose();

                    if (pose != null)
                    {
                        // update the pose as xyzwpr
                        _currentRobotTCPPositionTemp = pose.ToTxyzRxyz();

                        for (int index = 0; index < _currentRobotTCPPositionTemp.Length; index++)
                        {
                            _currentRobotTCPPositionSimulation[index] = _currentRobotTCPPositionTemp[index];
                        }

                        ActualRobotTCPPositionSimulator.RobotPositions = ReportedRobotTCPPositionSimulator.RobotPositions = _currentRobotTCPPositionSimulation;
                    }
                    #endregion
                }

#if DEBUG

                ActualRobotTCPPosition.RobotPositions = ReportedRobotTCPPosition.RobotPositions = _currentRobotTCPPositionSimulation;

#else
                if (returnValue == 0)
                {                      
                    ActualRobotTCPPosition.RobotPositions = ReportedRobotTCPPosition.RobotPositions = _currentRobotTCPPositionReal;
                }
#endif
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short GetCurrentRobotJointsPosition()
        {
            short returnValue = -1;
            double[] _currentRobotJointsPositionReal = new double[12];
            double[] _currentRobotJointsPositionSimulation = new double[12];
            short _rConf = 0;
            short ispulse = 1;//joints coordinate system
            double[] joints = null;

            try
            {
                returnValue = Motocom.BscIsLoc(_robotHandler, ispulse, ref _rConf, ref _currentRobotJointsPositionReal[0]);

                if (_useRoboDKSimulator)
                {
                    #region
                    joints = _roboDKRobot.Joints();

                    if (joints != null)
                    {
                        #region
                        for (int index = 0; index < joints.Length; index++)
                        {
                            _currentRobotJointsPositionSimulation[index] = joints[index];
                        }

                        ActualRobotJointPositionSimulator.RobotPositions = ReportedRobotJointPositionSimulator.RobotPositions = _currentRobotJointsPositionSimulation;
                        #endregion
                    }
                    #endregion
                }

#if DEBUG

                ActualRobotJointPosition.RobotPositions = ReportedRobotJointPosition.RobotPositions = _currentRobotJointsPositionSimulation;
#else
                if (returnValue == 0)
                {
                    ActualRobotJointPosition.RobotPositions = ReportedRobotJointPosition.RobotPositions = _currentRobotJointsPositionReal;
                }
#endif
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameName"></param>
        /// <returns></returns>
        public short GetCurrentRobotPosition(string frameName)
        {
            short returnValue = -1;
            double[] _currentRobotPosition = new double[12];
            short _rConf = 0;
            short isex = 1;
            StringBuilder _frameName = null;

            try
            {
                lock (_DX200AccessLock)
                {
                    _frameName = new StringBuilder(frameName);

                    returnValue = Motocom.BscIsRobotPos(_robotHandler, _frameName, isex, ref _rConf, ref _toolNumber, ref _currentRobotPosition[0]);

                    if (returnValue == 0)
                    {
                        ActualRobotTCPPosition.RobotPositions = ReportedRobotTCPPosition.RobotPositions = _currentRobotPosition;
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// Retrieves joblist from controller
        /// </summary>
        /// <param name="JobList">ArrayList that will receive the jobs</param>
        /// <returns>Number of jobs in joblist</returns>
        public int GetJobList(ArrayList JobList)
        {
            short ret;
            StringBuilder jobname = new StringBuilder(Motocom.MaxJobNameLength + 1);

            lock (_DX200AccessLock)
            {
                JobList.Clear();

                ret = Motocom.BscFindFirst(_robotHandler, jobname, Motocom.MaxJobNameLength + 1);
                if (ret < -1)
                    throw new Exception("Error reading job list !");
                if (ret == 0)
                {
                    JobList.Add(jobname.ToString());
                    do
                    {
                        ret = Motocom.BscFindNext(_robotHandler, jobname, Motocom.MaxJobNameLength + 1);
                        if (ret < -1)
                            throw new Exception("Error reading job list !");
                        if (ret == 0)
                            JobList.Add(jobname.ToString());
                    }
                    while (ret == 0);
                }
            }
            return JobList.Count;
        }
        /// <summary>
        /// Deletes a job on the controller
        /// </summary>
        /// <param name="JobName">Name of job to delete</param>
        public void DeleteJob(string JobName)
        {
            short ret;

            if (!JobName.ToLower().Contains(".jbi"))
                throw new Exception("Error *.jbi jobname extension is missing !");

            lock (_DX200AccessLock)
            {

                ret = Motocom.BscSelectJob(_robotHandler, JobName);
                if (ret == 0)
                {
                    ret = Motocom.BscDeleteJob(_robotHandler);
                    if (ret != 0)
                    {
                        throw new Exception("Error deleting job !");
                    }
                }
                else
                {
                    throw new Exception("Error selecting job !");
                }
            }
        }
        /// <summary>
        /// Retrieves alarm and error status of the controller
        /// </summary>
        /// <param name="error">Object to hold error information</param>
        /// <param name="alarmlist">ArrayList that holds objects with alarm information</param>
        /// <returns>Number of active alarms</returns>
        public int GetAlarm(out ErrorData error, out ArrayList alarmlist)
        {
            short ret;
            short errorno = -1;
            StringBuilder errormsg = new StringBuilder(256);
            short alarmsubno = -1;
            StringBuilder alarmmsg = new StringBuilder(256);

            lock (_DX200AccessLock)
            {
                alarmlist = new ArrayList();
                error = new ErrorData();

                ret = Motocom.BscReadAlarmS(_robotHandler, ref errorno, errormsg);
                if (ret != 0)
                    throw new Exception("Error reading error/alarm information !");

                error.ErrorNo = errorno;
                error.ErrorMsg = errormsg.ToString();

                ret = Motocom.BscGetFirstAlarmS(_robotHandler, ref alarmsubno, alarmmsg);
                if (ret < 0)
                    throw new Exception("Error reading alarms !");

                if (ret > 0)
                {
                    alarmlist.Add(new AlarmHistoryItem(ret, alarmsubno, alarmmsg.ToString()));
                    do
                    {
                        ret = Motocom.BscGetNextAlarmS(_robotHandler, ref alarmsubno, alarmmsg);
                        if (ret < 0)
                            throw new Exception("Error reading alarms !");
                        if (ret > 0)
                            alarmlist.Add(new AlarmHistoryItem(ret, alarmsubno, alarmmsg.ToString()));
                    }
                    while (ret > 0);
                }
            }
            return alarmlist.Count;
        }
        /// <summary>
        /// Resets an alarm
        /// </summary>
        public short ResetAlarm()
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscReset(_robotHandler);
                if (ret != 0)
                    throw new Exception("Error executing BscReset");
                return ret;
            }
        }
        /// <summary>
        /// Sets Teach mode
        /// </summary>
        /// <summary>
        /// Sets Servo ON
        /// </summary>
        public void SetServoOn()
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscServoOn(_robotHandler);
                if (ret != 0)
                {
                    throw new Exception("Error executing BscServoON");
                }
            }
        }
        /// <summary>
        /// Sets Servo OFF
        /// </summary>
        public void SetServoOff()
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscServoOff(_robotHandler);
                if (ret != 0)
                {
                    throw new Exception("Error executing BscServoOff");
                }
            }
        }
        public short IsServo()
        {
            return Motocom.BscIsServo(_robotHandler);
        }
        /// <summary>
        /// Sets Hold ON
        /// </summary>
        public short HoldOn()
        {
            short returnValue = 0;

            try
            {
                lock (_DX200AccessLock)
                {
                    returnValue = Motocom.BscHoldOn(_robotHandler);

                    if (_useRoboDKSimulator)
                    {
                        _roboDKRobot.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// Sets Hold OFF
        /// </summary>
        public short HoldOff()
        {
            short returnValue = 0;

            try
            {
                lock (_DX200AccessLock)
                {
                    returnValue = Motocom.BscHoldOff(_robotHandler);

                    if (_useRoboDKSimulator)
                    {
                        _roboDKRobot.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
        /// <summary>
        /// Starts operation from the current line of current job
        /// </summary>
        public short Start()
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscContinueJob(_robotHandler);
                if (ret != 0)
                    throw new Exception("Error executing BscContinueJob");

                return ret;
            }
        }
        /// <summary>
        /// Reads multiple variables of simple data type
        /// </summary>
        /// <param name="SimpleVar"></param>
        public void ReadSimpleTypeVariable(SimpleTypeVarList SimpleVar)
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscHostGetVarDataM(_robotHandler, (short)SimpleVar.VarType, SimpleVar.StartIndex, SimpleVar.ListSize, ref SimpleVar.VarListArray[0]);
                if (ret != 0)
                    throw new Exception("Error executing BscHostGetVarDataM");
            }
        }
        /// <summary>
        /// Reads position variable
        /// </summary>
        /// <param name="Index">Index of variable to read</param>
        /// <param name="PosVar">Object receiving results</param>
        public short ReadPositionVariable(short Index, out RobotPositionVariable PosVar)
        {
            lock (_DX200AccessLock)
            {
                PosVar = new RobotPositionVariable();

                StringBuilder StringVal = new StringBuilder(256);
                double[] PosVarArray = new double[12];
                short returnValue = Motocom.BscHostGetVarData(_robotHandler, (short)RobotVariableType.RobotAxisPosition, Index, ref PosVarArray[0], StringVal);
                PosVar.NumVarStorArea = (returnValue == 0) ? PosVarArray : PosVar.NumVarStorArea;

                return returnValue;
            }
        }
        /// <summary>
        /// Writes position variable
        /// </summary>
        /// <param name="Index">Index of variable to write</param>
        /// <param name="PosVar">Object containg values to write</param>
        public short WritePositionVariable(short Index, RobotPositionVariable PosVar)
        {
            lock (_DX200AccessLock)
            {
                StringBuilder StringVal = new StringBuilder(256);
                double[] PosVarArray = PosVar.NumVarStorArea;
                short ret = Motocom.BscHostPutVarData(_robotHandler, 4, Index, ref PosVarArray[0], StringVal);
                if (ret != 0)
                    throw new Exception("Error executing BscHostPutVarData");
                return ret;
            }
        }
        /// <summary>
        /// Writes multiple simple type variables
        /// </summary>
        /// <param name="SimpleVar"></param>
        public void WriteSimpleTypeVariable(SimpleTypeVarList SimpleVar)
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscHostPutVarDataM(_robotHandler, (short)SimpleVar.VarType, SimpleVar.StartIndex, SimpleVar.ListSize, ref SimpleVar.VarListArray[0]);
                if (ret != 0)
                    throw new Exception("Error executing BscHostPutVarDataM");
            }
        }
        /// <summary>
        /// Download a file from controller
        /// </summary>
        /// <param name="Filetitle">Name of the file</param>
        /// <param name="Path">Folder to store the file</param>
        public void ReadFile(string Filetitle, string Path)
        {
            StringBuilder _Filetitle = new StringBuilder(Filetitle, 255);
            short ret;
            if (Path.Substring(Path.Length - 1, 1) != "\\")
                Path = Path + "\\";
            lock (_FileAccessDirLock)
            {
                lock (_DX200AccessLock)
                {
                    ret = Motocom.BscUpLoad(_robotHandler, _Filetitle);
                }
                if (ret != 0)
                    throw new Exception("Error executing BscUpLoadEx");
                else
                    File.Copy(_commDir + Filetitle, Path + Filetitle, true);
            }
        }
        /// <summary>
        /// Reads file and stores it to default folder
        /// </summary>
        /// <param name="Filetitle">Filename</param>
        public void ReadFile(string Filetitle)
        {
            ReadFile(Filetitle, _path);
        }
        /// <summary>
        /// Writes one single IO
        /// </summary>
        /// <param name="Address">Address of IO</param>
        /// <param name="value">Value of IO</param>
        public short WriteSingleIO(int Address, bool value)
        {
            //todo:check if it returns a byte with 1/0 or a group of 8 buts of 0/1.
            //todo:check if it should be 8 or 10.
            int BaseAddress = Address / 8 * 8;
            short iovalue;
            if (value)
                iovalue = (short)(1 << (Address - BaseAddress));
            else
                iovalue = 0;
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscWriteIO2(_robotHandler, Address, 1, ref iovalue);
                if (ret != 0)
                    throw new Exception("Error executing BscWriteIO2");

                return ret;
            }
        }
        /// <summary>
        /// Reads multiple IO groups
        /// </summary>
        /// <param name="StartAddress">Address  of first group</param>
        /// <param name="NumberOfGroups">Number of groups to read</param>
        /// <returns>Array of binary codes representing each group</returns>
        public short ReadIOGroups(int StartAddress, short NumberOfGroups, out short[] ioValues)
        {
            //todo:check if it returns a byte with 1/0 or a group of 8 buts of 0/1.
            //todo:check if it should be 8 or 10.
            int BaseAddress = (int)StartAddress / 8 * 8;

            if (StartAddress != BaseAddress)
                throw new Exception("Start address has to be first address of a group");
            if (NumberOfGroups > 32)
                throw new Exception("Maximum group number to read is 32");

            ioValues = new short[32];
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscReadIO2(_robotHandler, StartAddress, (short)(NumberOfGroups * 8), ref ioValues[0]);
                if (ret != 0)
                    throw new Exception("Error executing BscReadIO2");

                return ret;
            }
        }
        /// <summary>
        /// Writes multiple IO groups
        /// </summary>
        /// <param name="StartAddress">Address  of first group</param>
        /// <param name="NumberOfGroups">Number of groups to write</param>
        /// <param name="IOGroupValues">Values of each group</param>
        public short WriteIOGroups(int StartAddress, short NumberOfGroups, short[] IOGroupValues)
        {
            //todo:check if it returns a byte with 1/0 or a group of 8 buts of 0/1.
            //todo:check if it should be 8 or 10.
            int BaseAddress = (int)StartAddress / 8 * 8;

            if (StartAddress != BaseAddress)
                throw new Exception("Start address has to be first address of a group");
            if (NumberOfGroups > 32)
                throw new Exception("Maximum group number to write is 32");

            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscWriteIO2(_robotHandler, StartAddress, (short)(NumberOfGroups * 8), ref IOGroupValues[0]);
                if (ret != 0)
                    throw new Exception("Error executing BscWriteIO2");

                return ret;
            }
        }
        /// <summary>
        /// Reads one single IO
        /// </summary>
        /// <param name="Address">Address of IO to read</param>
        /// <returns>IO status</returns>
        public bool ReadSingleIO(int Address)
        {
            short ret;
            short IOVal = 0;

            lock (_DX200AccessLock)
            {
                ret = Motocom.BscReadIO2(_robotHandler, Address, 1, ref IOVal);
                if (ret != 0)
                    throw new Exception("Error reading IO !");
            }
            return (IOVal > 0 ? true : false);
        }
        /// <summary>
        /// Uploads file to the controller
        /// </summary>
        /// <param name="Filename">Filename including path</param>
        public void WriteFile(string Filename)
        {
            StringBuilder _Filetitle = new StringBuilder(Path.GetFileName(Filename), 255);
            short ret;
            lock (_FileAccessDirLock)
            {
                if (Filename != _commDir + _Filetitle)
                    File.Copy(Filename, _commDir + _Filetitle, true);

                lock (_DX200AccessLock)
                {
                    ret = Motocom.BscDownLoad(_robotHandler, _Filetitle);
                }
                if (ret != 0)
                    throw new Exception("Error executing BscDownLoad");
            }
        }
        /// <summary>
        /// Calls and executes specified job
        /// </summary>
        /// <param name="JobName">Jobname to execute</param>
        public void StartJob(string JobName)
        {
            short ret;

            if (!JobName.ToLower().Contains(".jbi"))
                throw new Exception("Error *.jbi jobname extension is missing !");

            lock (_DX200AccessLock)
            {

                ret = Motocom.BscSelectJob(_robotHandler, JobName);
                if (ret == 0)
                {
                    ret = Motocom.BscStartJob(_robotHandler);
                    if (ret != 0)
                        throw new Exception("Error starting job !");
                }
                else
                    throw new Exception("Error selecting job !");
            }
        }
        /// <summary>
        /// Returns executing job of task 0
        /// </summary>
        /// <returns>Jobname</returns>
        public string GetCurrentJob()
        {
            return GetCurrentJob(0);
        }
        /// <summary>
        /// Returns executing job of specified task
        /// </summary>
        /// <param name="TaskID">Task ID</param>
        /// <returns>Jobname</returns>
        public string GetCurrentJob(short TaskID)
        {
            short ret;
            StringBuilder jobname = new StringBuilder(255);

            lock (_DX200AccessLock)
            {
                if (TaskID > 0)
                {
                    ret = Motocom.BscChangeTask(_robotHandler, TaskID);
                    if (ret != 0)
                    {
                        throw new Exception("Error changing task !");
                    }
                }
                ret = Motocom.BscIsJobName(_robotHandler, jobname, 255);
                if (ret != 0)
                    throw new Exception("Error getting current job name !");
            }
            return jobname.ToString();
        }
        /// <summary>
        /// Sets executing job and cursor
        /// </summary>
        /// <param name="TaskID">Task ID</param>
        /// <param name="JobName">Jobname</param>
        /// <param name="linenumber">Line number</param>
        public void SetCurrentJob(short TaskID, string JobName, short linenumber)
        {
            short ret;

            if (!JobName.ToLower().Contains(".jbi"))
                throw new Exception("Error *.jbi jobname extension is missing !");

            lock (_DX200AccessLock)
            {
                if (TaskID > 0)
                {
                    ret = Motocom.BscChangeTask(_robotHandler, TaskID);
                    if (ret != 0)
                    {
                        throw new Exception("Error changing task !");
                    }
                }
                ret = Motocom.BscSelectJob(_robotHandler, JobName);
                if (ret == 0)
                {
                    ret = Motocom.BscSetLineNumber(_robotHandler, linenumber);
                    if (ret != 0)
                        throw new Exception("Error setting current job line !");
                }
            }
        }
        /// <summary>
        /// Returns current job line of specified task
        /// </summary>
        /// <param name="TaskID">Task ID</param>
        /// <returns>Job line number</returns>
        public short GetCurrentLine(short TaskID)
        {
            short ret;

            lock (_DX200AccessLock)
            {
                if (TaskID > 0)
                {
                    ret = Motocom.BscChangeTask(_robotHandler, TaskID);
                    if (ret != 0)
                    {
                        throw new Exception("Error changing task !");
                    }
                }
                ret = Motocom.BscIsJobLine(_robotHandler);
                if (ret == -1)
                    throw new Exception("Error getting current job line !");
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        public bool IsInMotion(int axis, int frame)
        {
            bool returnValue = false;

            try
            {
                returnValue = (frame == 0) ? ActualRobotJointPosition.InMotionArray[axis] : ActualRobotTCPPosition.InMotionArray[axis];
            }
            catch (Exception ex)
            {
                returnValue = false;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }

        #region Movement
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNo"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public short Movl(StringBuilder moveSpeedSelection, double speed, StringBuilder frameName, short rconf, short toolNo, ref double targetPosition)
        {
            lock (_DX200AccessLock)
            {
                return Motocom.BscMovl(_robotHandler, moveSpeedSelection, speed, frameName, rconf, toolNo, ref targetPosition);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNo"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public short MovJ(double speed, StringBuilder frameName, short rconf, short toolNo, ref double targetPosition)
        {
            lock (_DX200AccessLock)
            {
                return Motocom.BscMovj(_robotHandler, speed, frameName, rconf, toolNo, ref targetPosition);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="toolNo"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public short MovlJoint(StringBuilder moveSpeedSelection, double speed, short toolNo, ref double targetPosition)
        {
            lock (_DX200AccessLock)
            {
                return Motocom.BscPMovl(_robotHandler, moveSpeedSelection, speed, toolNo, ref targetPosition);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="toolNo"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public short MovjJoint(double speed, short toolNo, ref double targetPosition)
        {
            lock (_DX200AccessLock)
            {
                return Motocom.BscPMovj(_robotHandler, speed, toolNo, ref targetPosition);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="toolNo"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short IncrementMove(StringBuilder moveSpeedSelection, double speed, StringBuilder frameName, short toolNo, ref double incrementValue)
        {
            lock (_DX200AccessLock)
            {
                return Motocom.BscImov(_robotHandler, moveSpeedSelection, speed, frameName, toolNo, ref incrementValue);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="toolNo"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short IncrementMove(RobotMoveSpeedSelectionType moveSpeedSelection, double speed, RobotFrameType frameName, short toolNo)
        {
            lock (_DX200AccessLock)
            {
                return Motocom.BscImov(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[moveSpeedSelection]),
                    speed, new StringBuilder(_frameDictionary[frameName]), toolNo, ref _desiredRobotIncrementPosition.RobotPositions[0]);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="toolNo"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short IncrementMove(RobotMoveSpeedSelectionType moveSpeedSelection, double speed)
        {
            lock (_DX200AccessLock)
            {
                return Motocom.BscImov(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[moveSpeedSelection]),
                    speed, new StringBuilder(_frameDictionary[ActualReferenceFrame]), ToolNumber, ref _desiredRobotIncrementPosition.RobotPositions[0]);
            }
        }
        /// <summary>
        ///  short IncrementMove(double speed)
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short IncrementMove(double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;

            try
            {
                lock (_DX200AccessLock)
                {
                    returnValue = (RobotFunctionReturnType_2)(Motocom.BscImov(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[ActualMoveSpeedSelection]),
                        speed, new StringBuilder(_frameDictionary[ActualReferenceFrame]), ToolNumber, ref _desiredRobotIncrementPosition.RobotPositions[0]));
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointJogMove(int jointIndex, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] jointsPositionsSimulator = null;
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            try
            {
                jointsPositionsReal = ReportedRobotJointPosition.RobotPulsePositions;
                jointsPositionsReal[jointIndex] = (speed > 0) ? DesiredRobotJointPosition.LimitsPulse[jointIndex][1] : DesiredRobotJointPosition.LimitsPulse[jointIndex][0];
                Motocom.BscPMovj(_robotHandler, speed, _toolNumber, ref jointsPositionsReal[0]);

                if (_useRoboDKSimulator)
                {
                    #region

                    jointsPositionsSimulator = _roboDKRobot.Joints();

                    //get actual robot position
                    for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                    {
                        jointsPositionsSimulator[i] = ReportedRobotJointPositionSimulator.RobotPositions[i];
                    }

                    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                    if (speed > 0)
                    {
                        jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.Limits[jointIndex][1];
                    }
                    if (speed < 0)
                    {
                        jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.Limits[jointIndex][0];
                    }

                    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointMask"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointsJogMove(int jointMask, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] jointsPositionsSimulator = null;
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            try
            {
                for (int i = 0; i < jointsPositionsReal.Length; i++)
                {
                    if ((jointMask & (1 << i)) > 0)
                    {
                        jointsPositionsReal[i] = (speed > 0) ? DesiredRobotJointPosition.LimitsPulse[i][1] : DesiredRobotJointPosition.LimitsPulse[i][0];
                    }
                }

                returnValue = (RobotFunctionReturnType_2)Motocom.BscPMovj(_robotHandler, speed, _toolNumber, ref jointsPositionsReal[0]);

                if (_useRoboDKSimulator)
                {
                    #region

                    jointsPositionsSimulator = _roboDKRobot.Joints();

                    //get actual robot position
                    for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                    {
                        jointsPositionsSimulator[i] = ReportedRobotJointPositionSimulator.RobotPositions[i];
                    }

                    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                    for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                    {
                        if ((jointMask & (1 << i)) > 0)
                        {
                            jointsPositionsSimulator[i] = (speed > 0) ? DesiredRobotJointPosition.Limits[i][1] : DesiredRobotJointPosition.Limits[i][0];
                        }
                    }

                    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointAbsoluteMove(int jointIndex, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            try
            {
                #region

                jointsPositionsReal = DesiredRobotJointPosition.RobotPulsePositions;
                returnValue = (RobotFunctionReturnType_2)Motocom.BscPMovj(_robotHandler, speed, _toolNumber, ref jointsPositionsReal[0]);

                if (_useRoboDKSimulator)
                {
                    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                    jointsPositionsSimulator = ReportedRobotJointPosition.RobotPositions;

                    jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.RobotPositions[jointIndex];

                    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                }

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointsAbsoluteMove(int jointMask, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            try
            {
                #region

                for (int i = 0; i < jointsPositionsReal.Length; i++)
                {
                    #region
                    if ((jointMask & (1 << i)) > 0)
                    {
                        jointsPositionsReal[i] = DesiredRobotJointPosition.RobotPulsePositions[i];
                    }
                    #endregion
                }

                returnValue = (RobotFunctionReturnType_2)Motocom.BscPMovj(_robotHandler, speed, _toolNumber, ref jointsPositionsReal[0]);

                if (_useRoboDKSimulator)
                {
                    #region
                    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                    for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                    {
                        if ((jointMask & (1 << i)) > 0)
                        {
                            jointsPositionsSimulator[i] = DesiredRobotJointPosition.RobotPositions[i];
                        }
                    }

                    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                    #endregion
                }

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointHomeMove(int jointIndex, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] joints = new double[6] { 0, 0, 0, 0, 0, 0 };

            try
            {
                #region

                //get actual robot position
                for (int i = 0; i < joints.Length; i++)
                {
                    joints[i] = ReportedRobotJointPosition.RobotPositions[i];
                }

                _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                joints[jointIndex] = DesiredRobotJointPosition.RobotHomePositions[jointIndex];

                _roboDKRobot.MoveJ(joints, MOVE_BLOCKING);

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointParkMove(int jointIndex, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] joints = new double[6] { 0, 0, 0, 0, 0, 0 };

            try
            {
                #region

                //get actual robot position
                for (int i = 0; i < joints.Length; i++)
                {
                    joints[i] = ReportedRobotJointPosition.RobotPositions[i];
                }

                _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                joints[jointIndex] = DesiredRobotJointPosition.RobotParkPositions[jointIndex];

                _roboDKRobot.MoveJ(joints, MOVE_BLOCKING);

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointRelativeMove(int jointIndex, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            try
            {
                #region

                jointsPositionsReal[jointIndex] += DesiredRobotJointPosition.RobotPulsePositions[jointIndex];
                returnValue = (RobotFunctionReturnType_2)Motocom.BscPMovj(_robotHandler, speed, _toolNumber, ref jointsPositionsReal[0]);

                if (_useRoboDKSimulator)
                {
                    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                    jointsPositionsSimulator = ReportedRobotJointPosition.RobotPositions;
                    jointsPositionsSimulator[jointIndex] += DesiredRobotJointPosition.RobotPositions[jointIndex];

                    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                }

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointsRelativeMove(int jointMask, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            try
            {
                #region

                for (int i = 0; i < jointsPositionsReal.Length; i++)
                {
                    #region
                    if ((jointMask & (1 << i)) > 0)
                    {
                        jointsPositionsReal[i] += DesiredRobotJointPosition.RobotPulsePositions[i];
                    }
                    #endregion
                }

                returnValue = (RobotFunctionReturnType_2)Motocom.BscPMovj(_robotHandler, speed, _toolNumber, ref jointsPositionsReal[0]);

                if (_useRoboDKSimulator)
                {
                    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                    jointsPositionsSimulator = ReportedRobotJointPosition.RobotPositions;

                    for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                    {
                        if ((jointMask & (1 << i)) > 0)
                        {
                            jointsPositionsSimulator[i] += DesiredRobotJointPosition.RobotPositions[i];
                        }
                    }

                    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                }

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TCPJogMove(int tcpIndex, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] tcpAxes = new double[6] { 0, 0, 0, 0, 0, 0 };

            //get actual robot position
            for (int i = 0; i < tcpAxes.Length; i++)
            {
                tcpAxes[i] = ReportedRobotTCPPosition.RobotPositions[i];
            }

            try
            {
                #region

                if (speed > 0)
                {
                    tcpAxes[tcpIndex] = DesiredRobotTCPPosition.Limits[tcpIndex][1];
                }
                if (speed < 0)
                {
                    tcpAxes[tcpIndex] = DesiredRobotTCPPosition.Limits[tcpIndex][0];
                }

                Mat movement_pose = Mat.FromTxyzRxyz(tcpAxes);

                _roboDKRobot.MoveJ(movement_pose, MOVE_BLOCKING);

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TCPAbsoluteMove(int tcpIndex, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] tcpAxes = new double[6] { 0, 0, 0, 0, 0, 0 };

            //get actual robot position
            for (int i = 0; i < tcpAxes.Length; i++)
            {
                tcpAxes[i] = ReportedRobotTCPPosition.RobotPositions[i];
            }

            try
            {
                #region

                tcpAxes[tcpIndex] = DesiredRobotTCPPosition.RobotPositions[tcpIndex];

                Mat movement_pose = Mat.FromTxyzRxyz(tcpAxes);

                _roboDKRobot.MoveJ(movement_pose, MOVE_BLOCKING);

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TCPRelativeMove(int tcpIndex, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] tcpAxes = new double[6] { 0, 0, 0, 0, 0, 0 };

            try
            {
                #region

                tcpAxes[tcpIndex] = DesiredRobotTCPPosition.RobotPositions[tcpIndex];

                Mat movement_pose = Mat.FromTxyzRxyz(tcpAxes);

                // the the current position of the robot (as a 4x4 matrix)
                Mat robot_pose = _roboDKRobot.Pose();

                // Calculate the new position of the robot
                Mat new_robot_pose;

                Mat transformation_axes = new Mat(robot_pose);
                transformation_axes.setPos(0, 0, 0);
                Mat movement_pose_aligned = transformation_axes.inv() * movement_pose * transformation_axes;
                new_robot_pose = robot_pose * movement_pose_aligned;

                _roboDKRobot.MoveJ(new_robot_pose, MOVE_BLOCKING);

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TrackLinearJogMove(int jointIndex, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] jointsPositionsSimulator = null;
            double[] jointsPositions = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            try
            {
                jointsPositions = ReportedRobotJointPosition.RobotPulsePositions;

                if (speed > 0)
                {
                    jointsPositions[jointIndex] = DesiredRobotJointPosition.LimitsPulse[jointIndex][1];
                }
                if (speed < 0)
                {
                    jointsPositions[jointIndex] = DesiredRobotJointPosition.LimitsPulse[jointIndex][0];
                }

                Motocom.BscPMovj(_robotHandler, speed, _toolNumber, ref jointsPositions[0]);

                if (_useRoboDKSimulator)
                {
                    #region

                    jointsPositionsSimulator = _roboDKRobot.Joints();

                    //get actual robot position
                    for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                    {
                        jointsPositionsSimulator[i] = ReportedRobotJointPositionSimulator.RobotPositions[i];
                    }

                    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                    if (speed > 0)
                    {
                        jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.Limits[jointIndex][1];
                    }
                    if (speed < 0)
                    {
                        jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.Limits[jointIndex][0];
                    }

                    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TurnTableJogMove(int jointIndex, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] jointsPositionsSimulator = null;
            double[] jointsPositions = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            try
            {
                jointsPositions = ReportedRobotJointPosition.RobotPulsePositions;

                if (speed > 0)
                {
                    jointsPositions[jointIndex] = DesiredRobotJointPosition.LimitsPulse[jointIndex][1];
                }
                if (speed < 0)
                {
                    jointsPositions[jointIndex] = DesiredRobotJointPosition.LimitsPulse[jointIndex][0];
                }

                Motocom.BscPMovj(_robotHandler, speed, _toolNumber, ref jointsPositions[0]);

                if (_useRoboDKSimulator)
                {
                    #region

                    jointsPositionsSimulator = _roboDKRobot.Joints();

                    //get actual robot position
                    for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                    {
                        jointsPositionsSimulator[i] = ReportedRobotJointPositionSimulator.RobotPositions[i];
                    }

                    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                    if (speed > 0)
                    {
                        jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.Limits[jointIndex][1];
                    }
                    if (speed < 0)
                    {
                        jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.Limits[jointIndex][0];
                    }

                    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="toolNo"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short IncrementMove()
        {
            lock (_DX200AccessLock)
            {
                return Motocom.BscImov(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[ActualMoveSpeedSelection]),
                    ActualRobotSpeed, new StringBuilder(_frameDictionary[ActualReferenceFrame]), ToolNumber, ref _desiredRobotIncrementPosition.RobotPositions[0]);
            }
        }
        /// <summary>
        /// Moving the robot with incremental position value in a linear motion in specified frame type.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short MoveLinearIncrementCartesian(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, ref double incrementValue)
        {
            StringBuilder moveSpeedSelectionSB = new StringBuilder(moveSpeedSelection);
            StringBuilder framNameSB = new StringBuilder(frameName);

            return IncrementMove(moveSpeedSelectionSB, speed, framNameSB, toolNumber, ref incrementValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameName"></param>
        /// <param name="isEx"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public short GetRobotPosition(StringBuilder frameName, short isEx, ref short rconf, ref short toolNumber, ref double positions)
        {
            RobotFunctionReturnType_1 returnValue = RobotFunctionReturnType_1.AcquisitionFailure;

            lock (_DX200AccessLock)
            {
                returnValue = (RobotFunctionReturnType_1)(Motocom.BscIsRobotPos(_robotHandler, frameName, isEx, ref rconf, ref toolNumber, ref positions));
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameName"></param>
        /// <param name="isEx"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public short GetRobotPosition(StringBuilder frameName, short isEx, ref short rconf, ref short toolNumber)
        {
            RobotFunctionReturnType_1 returnValue = RobotFunctionReturnType_1.AcquisitionFailure;
            double[] positions = new double[12];

            lock (_DX200AccessLock)
            {
                returnValue = (RobotFunctionReturnType_1)(Motocom.BscIsRobotPos(_robotHandler, frameName, isEx, ref rconf, ref toolNumber, ref positions[0]));
                if (returnValue == RobotFunctionReturnType_1.NormalCompletion)
                {
                    positions.CopyTo(ActualRobotTCPPosition.RobotPositions, 0);
                }
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isPulseOrXYZ"></param>
        /// <param name="rconf"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public short GetRobotPosition(short isPulseOrXYZ, ref short rconf, ref double position)
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscIsLoc(_robotHandler, isPulseOrXYZ, ref rconf, ref position);

                if (ret != 0)
                {
                    throw new Exception("Error reading position!");
                }

                return ret;
            }
        }
        ///// <summary>
        ///// Starts a continues Jogging in all zxes (X,Y,Z)
        ///// </summary>
        ///// <param name="speed">The speed for the Jogging in (X,Y,Z) axes.</param>
        //public void StartContinuesJog(double[] speed)
        //{
        //    //get the current position
        //    double[] targetPosition = new double[12];
        //    short rconf = 0;
        //    GetCurrentPosition(false, ref rconf, ref targetPosition[0]);

        //    //todo: need to change the scale with config parameter.
        //    double scale = 5;
        //    for (int i = 0; i < speed.Length; i++)
        //    {
        //        targetPosition[i] += speed[i] * scale;
        //    }

        //    double totalSpeed = Math.Sqrt(Math.Pow(speed[0], 2) + Math.Pow(speed[1], 2) + Math.Pow(speed[2], 2));

        //    MoveLinearCartesianTarget("V", totalSpeed, "ROBOT", 0, 0, ref targetPosition[0]);
        //}
        ///// <summary>
        ///// Stop a continues Jogging in all zxes (X,Y,Z)
        ///// </summary>
        //public void StopContinuesJog()
        //{
        //    //hold off to stop the jogging in x
        //    HoldOnExecution();
        //    //hold on so , next tasks would starts.
        //    HoldOffExecution();
        //}
        ///// <summary>
        ///// Start a continues Jogging ony in X axis.
        ///// </summary>
        ///// <param name="speed">The speed for the Jogging.</param>
        //public void StartContinuesJogX(double speed)
        //{
        //    StartContinuesJog(new double[] { speed, 0, 0 });
        //}
        ///// <summary>
        ///// Stop the continues Jogging for the X axis.
        ///// </summary>
        //public void StopContinuesJogX()
        //{
        //    StopContinuesJog();
        //}
        ///// <summary>
        ///// Start a continues Jogging ony in Y axis.
        ///// </summary>
        ///// <param name="speed">The speed for the Jogging.</param>
        //public void StartContinuesJogY(double speed)
        //{
        //    StartContinuesJog(new double[] { 0, speed, 0 });
        //}
        ///// <summary>
        ///// Stop the continues Jogging for the Y axis.
        ///// </summary>
        //public void StopContinuesJogY()
        //{
        //    StopContinuesJog();
        //}
        ///// <summary>
        ///// Start a continues Jogging ony in Z axis.
        ///// </summary>
        ///// <param name="speed">The speed for the Jogging.</param>
        //public void StartContinuesJogZ(double speed)
        //{
        //    StartContinuesJog(new double[] { 0, 0, speed });
        //}
        ///// <summary>
        ///// Stop the continues Jogging for the Z axis.
        ///// </summary>
        //public void StopContinuesJogZ()
        //{
        //    StopContinuesJog();
        //}
        #endregion

        #endregion

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void StatusTimerTick(object state)
        {
            try
            {
                lock (_lockStatusTimer)
                {
                    //TODO:Check connection ping and any relevant parameter
                    CheckConnection();
#if DEBUG
                    //TODO:check if it possible fix problem with ping in thread
                    _communicationStatus.connectionStatus = ConnectionStatus.Reachable;
#else
                     _communicationStatus.connectionStatus = DX200Communication.CheckPing(_ipAddress);
#endif

                    _communicationStatus.ipAddress = _ipAddress;

                    GetStatus();

                    //TODO:if not connected try connect
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        private void CheckConnection()
        {
            try
            {
                _connectionCheckCounter++;
                if (_communicationStatus.connected == false && _connectionCheckCounter > 19)
                {
                    Debug.WriteLine("YaskawaNet message:Robot not connected");
                    _connectionCheckCounter = 0;
                    if (DX200Communication.CheckPing(_ipAddress) == ConnectionStatus.Reachable)
                    {
                        Connect();
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void GetPositionsTimerTick(object state)
        {
            try
            {
                lock (_lockGetPositionsTimer)
                {
                    GetCurrentRobotTCPPosition();
                    GetCurrentRobotJointsPosition();
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnNotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region RoboDK fields
        // RDK holds the main object to interact with RoboDK.
        // The RoboDK application starts when a RoboDK object is created.
        RoboDK RDK = null;
        // Keep the ROBOT item as a global variable
        IItem _roboDKRobot = null;
        IItem _roboDKLinearTrack = null;
        IItem _roboDKTurnTable = null;
        // Define if the robot movements will be blocking
        const bool MOVE_BLOCKING = false;
        #endregion

        #region RoboDK methods
        /// <summary>
        /// Check if the RDK object is ready.
        /// Returns True if the RoboDK API is available or False if the RoboDK API is not available.
        /// </summary>
        /// <returns></returns>
        public bool Check_RDK()
        {
            bool returnedValue = false;

            try
            {
                // check if the RDK object has been initialized:
                if (RDK != null)
                {
                    returnedValue = true;
                    // Check if the RDK API is connected
                    if (!RDK.Connected())
                    {
                        // Attempt to connect to the RDK API
                        if (!RDK.Connect())
                        {
                            returnedValue = false;
                            DiagnosticException.ExceptionHandler("Problems using the RoboDK API. The RoboDK API is not available...");
                        }
                    }
                }
                else
                {
                    returnedValue = false;
                    DiagnosticException.ExceptionHandler("RoboDK has not been started");
                }
            }
            catch (Exception ex)
            {
                returnedValue = false;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnedValue;
        }
        /// <summary>
        /// Update the ROBOT variable by choosing the robot available in the currently open station
        /// If more than one robot is available, a popup will be displayed
        /// </summary>
        public void SelectRoboDKRobot()
        {
            try
            {
                // select robot among available robots
                //ROBOT = RL.getItem("ABB IRB120", ITEM_TYPE_ROBOT); // select by name
                //ITEM = RL.ItemUserPick("Select an item"); // Select any item in the station
                _roboDKRobot = RDK.GetItemByName("Motoman MH24", ItemType.Robot);
                _roboDKLinearTrack = RDK.GetItemByName("ABB IRBT 6004 Standard 3.5m Base", ItemType.Frame);
                _roboDKTurnTable = RDK.GetItemByName("ABB IRBP L2000 L4000 Base", ItemType.Frame);
                if (_roboDKRobot.Valid())
                {
                    // This will create a new communication link (another instance of the RoboDK API), this is useful if we are moving 2 robots at the same time. 
                    //_roboDKRobot.NewLink();
                    string _robotName = _roboDKRobot.Name();
                    string _robotLinearTrackName = _roboDKLinearTrack.Name();
                    string _robotTurnTableName = _roboDKTurnTable.Name();

                }
                else
                {
                    DiagnosticException.ExceptionHandler("Robot not available. Load a file first");
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        /// <summary>
        ///   void ShowRoboDKForm()
        /// </summary>
        public void ShowRoboDKForm()
        {
            RDK.SetWindowState(WindowState.Normal); // removes Cinema mode and shows the screen
                                                    //RDK.SetWindowState(WindowState.Maximized); // shows maximized
            RDK.SetWindowState(WindowState.Cinema); // shows maximized
        }
        /// <summary>
        /// Move TCP robot.
        /// </summary>
        private void IncrementalJointMoveRoboDK()
        {
            double[] joints = new double[6] { 0, 0, 0, 0, 0, 0 };

            try
            {
                //TODO:Check.Problem with speed.
                //if (!Check_RobotDKRobot()) { return; }

                // get the moving axis (1, 2, 3, 4, 5 or 6)

                for (int index = 0; index < joints.Length; index++)
                {
                    joints[index] = DesiredRobotJointPosition.RobotPositions[index];
                }

                _roboDKRobot.MoveJ(joints, MOVE_BLOCKING);
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        /// <summary>
        /// Move TCP
        /// </summary>
        /// <param name="relativeMove"></param>
        private void IncrementalTCPMoveRoboDK(bool relativeMove)
        {
            double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };

            try
            {
                #region

                for (int index = 0; index < move_xyzwpr.Length; index++)
                {
                    move_xyzwpr[index] = _desiredRobotTCPPosition.RobotPositions[index];
                }

                Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);

                // the the current position of the robot (as a 4x4 matrix)
                Mat robot_pose = _roboDKRobot.Pose();

                // Calculate the new position of the robot
                Mat new_robot_pose;
                if (relativeMove)
                {
                    // if the movement is relative to the TCP we must POST MULTIPLY the movement
                    new_robot_pose = robot_pose * movement_pose;
                }
                else
                {
                    // if the movement is relative to the reference frame we must PRE MULTIPLY the XYZ translation:
                    // new_robot_pose = movement_pose * robot_pose;
                    // Note: Rotation applies from the robot axes.

                    Mat transformation_axes = new Mat(robot_pose);
                    transformation_axes.setPos(0, 0, 0);
                    Mat movement_pose_aligned = transformation_axes.inv() * movement_pose * transformation_axes;
                    new_robot_pose = robot_pose * movement_pose_aligned;
                }

                // Then, we can do the movement:

                _roboDKRobot.MoveJ(new_robot_pose, MOVE_BLOCKING);

                //// Some tips:
                //// retrieve the current pose of the robot: the active TCP with respect to the current reference frame
                //// Tip 1: use
                //// ROBOT.setFrame()
                //// to set a reference frame (object link or pose)
                ////
                //// Tip 2: use
                //// ROBOT.setTool()
                //// to set a tool frame (object link or pose)
                ////
                //// Tip 3: use
                //// ROBOT.MoveL_Collision(j1, new_move)
                //// to test if a movement is feasible by the robot before doing the movement
                //// 
                #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        /// <summary>
        /// Check if the ROBOT object is available and valid. It will make sure that we can operate with the ROBOT object.
        /// </summary>
        /// <returns></returns>
        public bool Check_RobotDKRobot(bool ignore_busy_status = false)
        {
            if (!Check_RDK())
            {
                return false;
            }
            if (_roboDKRobot == null || !_roboDKRobot.Valid())
            {
                //notifybar.Text = "A robot has not been selected. Load a station or a robot file first.";
                return false;
            }
            try
            {
                //notifybar.Text = "Using robot: " + ROBOT.Name();
            }
            catch (RdkException rdkex)
            {
                //notifybar.Text = "The robot has been deleted: " + rdkex.Message;
                return false;
            }

            // Safe check: If we are doing non blocking movements, we can check if the robot is doing other movements with the Busy command
            if (!MOVE_BLOCKING && (!ignore_busy_status && _roboDKRobot.Busy()))
            {
                //notifybar.Text = "The robot is busy!! Try later...";
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="run"></param>
        public void RoboDKSimulatorRun(bool run)
        {
            try
            {
                if (run == true)
                {
                    #region
                    RDK = new RoboDK();

                    // Check if RoboDK started properly
                    if (Check_RDK())
                    {
                        RDK.SetRunMode(RunMode.Simulate);

                        Thread.Sleep(100);

                        IItem activeStation = RDK.GetActiveStation();

                        Thread.Sleep(100);

                        string stationName = activeStation.Name();

                        Thread.Sleep(100);

                        if (stationName.IndexOf("ChesterfieldProject") < 0)
                        {
                            RDK.AddFile(@"F:\Yaskawa\ChesterfieldProject.rdk");
                        }
                        // attempt to auto select the robot:
                        SelectRoboDKRobot();
                    }

                    ShowRoboDKForm();

                    _getPositionsTimer.Change(0, 100); //enable timer 
                    #endregion
                }
                else
                {
                    RDK.CloseRoboDK();
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        #endregion
    }
}
