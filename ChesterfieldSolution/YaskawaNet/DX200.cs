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

using netDxf;

using System.Configuration;

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
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            [param: MarshalAs(UnmanagedType.BStr)]
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

        double ActualRobotJointSpeed
        {
            get;
            set;
        }

        ArrayList AlarmList
        {
            get;
            set;
        }
        string AlarmMessage
        {
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            [param: MarshalAs(UnmanagedType.BStr)]
            set;
        }

        string CurrentJobFileName
        {
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            [param: MarshalAs(UnmanagedType.BStr)]
            set;
        }

        short CurrentLineNumber
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
        short ServoOn();
        short ServoOff();
        short HoldOn();
        short HoldOff();
        short JointJogMove(int joint, double speed);
        short JointsJogMove(int jointMask, double speed);
        short JointAbsoluteMove(int jointIndex, double speed);
        short JointsAbsoluteMove(int jointMask, double speed);
        short JointRelativeMove(int jointIndex, double speed);
        short JointsRelativeMove(int jointMask, double speed);
        short JointHomeMove(int jointIndex, double speed);
        short TCPJogMove(int tcpIndex, double speed);
        short TCPAxesJogMove(int jointMask, double speed);
        short TCPAbsoluteMove(int tcpIndex, double speed);
        short TCPAxesAbsoluteMove(int tcpMask, double speed);
        short TCPRelativeMove(int tcpIndex, double speed);
        short TCPAxesRelativeMove(int tcpMask, double speed);
        short TrackLinearJogMove(int jointIndex, double speed);
        short TrackLinearAbsoluteMove(int jointIndex, double speed);
        short TrackLinearRelativeMove(int jointIndex, double speed);
        short TurnTableJogMove(int jointIndex, double speed);

        [return: MarshalAs(UnmanagedType.Bool)]
        bool IsInMotion(int axis, int frame);

        short CaptureTrajectoryPoint();

        short CreateJobFile([MarshalAs(UnmanagedType.BStr)] string fileName);
        short DownloadJobFile([MarshalAs(UnmanagedType.BStr)] string fileName);
        short StartJob([MarshalAs(UnmanagedType.BStr)]string fileName);
        short DeleteJob([MarshalAs(UnmanagedType.BStr)]string fileName);
        short ContinueJob();
        short GetCurrentLine(short taskID);
        short SetCurrentLine(short line);
        short SetCurrentJob(short taskID, [MarshalAs(UnmanagedType.BStr)]string fileName, short lineNumber);
        [return: MarshalAs(UnmanagedType.BStr)]
        string GetCurrentJobName();

        short SetUserFrame();

        #endregion
    }
    /// <summary>
    /// DX200 robot controller implementation                                                                                                                                    A connection attempt
    /// </summary>
    [ComVisible(true)]
    public class DX200 : IDX200, INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constants

        const double _maxJointSpeed = 50.0;//percent
        const double _sJointMaxSpeed = 197.0;//degree/sec
        const double _lJointMaxSpeed = 190.0;//degree/sec
        const double _uJointMaxSpeed = 210.0;//degree/sec
        const double _rJointMaxSpeed = 410.0;//degree/sec
        const double _bJointMaxSpeed = 410.0;//degree/sec
        const double _tJointMaxSpeed = 620.0;//degree/sec

        const double _trackMaxSpeed = 500.0;//mm/sec

        const int _motocom32FunctionsWaitTime = 50;

        const double _maxControlPointSpeed = 500;
        const double _maxPositionAngularSpeed = 360;

        #endregion

        #region Fields

        private short _robotHandler = -1;
        private bool _useRoboDKSimulator = false;
        private string _commDir;
        private string _path;
        private string _ipAddress = "";
        private CommunicationStatus _communicationStatus = new CommunicationStatus();
        private volatile int _connectionCheckCounter = 0;
        private RobotFrameType _actualReferenceFrame = RobotFrameType.Robot;
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

        #region Threads fields

        Thread _getPositionsThread = null;
        Thread _getStatusThread = null;
        Thread _getAlarmsThread = null;
        Thread _getJobFileStatusThread = null;
        Thread _simulatorSyncThread = null;

        volatile bool _getPositionsThreadRun = false;
        volatile bool _getStatusThreadRun = false;
        volatile bool _getAlarmsThreadRun = false;
        volatile bool _getJobFileStatusRun = false;
        volatile bool _simulatorSyncThreadRun = false;

        #endregion

        private short _oldStatusD1 = -1;
        private short _oldStatusD2 = -1;
        readonly object _fileAccessDirLock = new object();
        readonly object _robotAccessLock = new object();
        readonly object _simulatorAccessLock = new object();

        private RobotModeType _actualMode = RobotModeType.TEACH;

        private MotionDirection _actualMotionDirection = MotionDirection.None;

        private double _stepJogging = 50.0;
        [MarshalAs(UnmanagedType.BStr)]
        private string _alarmMessage = string.Empty;

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

        private volatile RobotPosition _actualCapturedRobotTCPPosition = new RobotPosition();

        #endregion

        RobotMoveSpeedSelectionType _actualMoveSpeedSelection = RobotMoveSpeedSelectionType.ControlPoint;
        RobotMotionType _actualMotionType = RobotMotionType.LinearMotion;

        UserCoordinateSystem _actualUserCoordinateSystem = new UserCoordinateSystem();
        UserCoordinateSystem _reportedUserCoordinateSystem = new UserCoordinateSystem();

        private bool _autoStatusUpdate = false;

        private double _actualRobotJointSpeed = 0.0;

        //TODO:create speed array for joints and tcp

        private short _toolNumber = 0;

        ErrorData _robotError = null;
        ArrayList _alarmList = null;
        int _alarmsCounter = 0;

        Trajectory _actualJointsTrajectory = new Trajectory();
        Trajectory _actualTCPTrajectory = new Trajectory();
        JobFile _actualJobFile = new JobFile();
        string _currentJobFileName = string.Empty;
        short _currentLineNumber = 0;

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
        //TODO:Problem with Teach mode !!!!!!!!!!!!!!!!!!!!!!!!!
        public RobotModeType ActualMode
        {
            get
            {
                return _actualMode;
            }
            set
            {
                short returnValue = -1;
                int taskWaitTimeCounter = 0;
                Task<short> task = null;
                int counterSendCommand = 0;
                int maxSendCommands = 3;

                try
                {
                    lock (_robotAccessLock)
                    {
                        #region
                        if ((int)value > 0 && (int)value < 3)
                        {
                            task = Task<short>.Factory.StartNew(() =>
                            {
                                //Return Value
                                //0 : Normal completion
                                //Others: Error codes
                                return Motocom.BscSelectMode(_robotHandler, (short)value);
                            });

                            taskWaitTimeCounter = 0;

                            while (!task.IsCompleted)
                            {
                                #region
                                taskWaitTimeCounter++;
                                task.Wait(_motocom32FunctionsWaitTime);
                                if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                {
                                    break;
                                }
                                #endregion
                            }

                            if (task.IsCompleted)
                            {
                                #region
                                returnValue = Convert.ToInt16(task.Result);
                                //TODO:check return and send more if needed
                                if (returnValue != 0)
                                {
                                    #region
                                    counterSendCommand = 0;
                                    while (counterSendCommand < maxSendCommands)
                                    {
                                        #region

                                        counterSendCommand++;

                                        task = Task<short>.Factory.StartNew(() =>
                                        {
                                            //Return Value
                                            //0 : Normal completion
                                            //Others: Error codes
                                            return Motocom.BscSelectMode(_robotHandler, (short)value);
                                        });

                                        taskWaitTimeCounter = 0;

                                        while (!task.IsCompleted)
                                        {
                                            #region
                                            taskWaitTimeCounter++;
                                            task.Wait(_motocom32FunctionsWaitTime);
                                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                            {
                                                break;
                                            }
                                            #endregion
                                        }
                                        if (task.IsCompleted)
                                        {
                                            returnValue = Convert.ToInt16(task.Result);
                                            if (returnValue == 0)
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            returnValue = -1;
                                        }
                                        #endregion
                                    }
                                    if (counterSendCommand == maxSendCommands)
                                    {
                                        Debug.WriteLine("DX200::ActualMode()::Error::" + returnValue.ToString());
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                returnValue = -1;
                            }
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    returnValue = -1;
                    DiagnosticException.ExceptionHandler(ex);
                }

                _actualMode = (returnValue == 0) ? value : _actualMode;
                //_actualMode = value;

                OnNotifyPropertyChanged();
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
                    Debug.WriteLine("DX200:: desired get:position= " + _desiredRobotJointPosition.RobotPositions[0].ToString());
                    return _desiredRobotJointPosition;
                }
            }
            set
            {
                lock (_desiredRobotJointPositionLocker)
                {
                    _desiredRobotJointPosition = value;
                    Debug.WriteLine("DX200:: desired set:position= " + _desiredRobotJointPosition.RobotPositions[0].ToString());
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
                    //_statusTimer.Start();
                }
                else
                {
                    //_statusTimer.Stop();
                }
            }
        }
        public double ActualRobotJointSpeed
        {
            get
            {
                return _actualRobotJointSpeed;
            }
            set
            {
                if (value > 0 && value < 100)
                {
                    _actualRobotJointSpeed = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        public int AlarmsCounter
        {
            get
            {
                return _alarmsCounter;
            }
            set
            {
                _alarmsCounter = value;
                OnNotifyPropertyChanged();
            }
        }
        public ErrorData RobotError
        {
            get
            {
                return _robotError;
            }
            set
            {
                _robotError = value;
                OnNotifyPropertyChanged();
            }
        }
        public ArrayList AlarmList
        {
            get
            {
                return _alarmList;
            }
            set
            {
                _alarmList = value;
                OnNotifyPropertyChanged();
            }
        }
        public string AlarmMessage
        {
            get
            {
                string returnValue = string.Empty;
                returnValue = _alarmMessage;
                _alarmMessage = string.Empty;
                return returnValue;
            }
            set
            {
                _alarmMessage = value;
                OnNotifyPropertyChanged();
            }
        }

        public string CurrentJobFileName
        {
            get
            {
                return _currentJobFileName;
            }
            set
            {
                _currentJobFileName = value;
                OnNotifyPropertyChanged();
            }
        }
        public short CurrentLineNumber
        {
            get
            {
                return _currentLineNumber;
            }
            set
            {
                _currentLineNumber = value;
                OnNotifyPropertyChanged();
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
                    _ipAddress = (o != null) ? o as String : "192.168.255.1";
                }

                _getPositionsThread = new Thread(new ThreadStart(GetPositionsThread));
                _getPositionsThread.IsBackground = true;
                _getPositionsThread.Priority = ThreadPriority.Normal;

                _getStatusThread = new Thread(new ThreadStart(GetStatusThread));
                _getStatusThread.IsBackground = true;
                _getStatusThread.Priority = ThreadPriority.Normal;

                _getAlarmsThread = new Thread(new ThreadStart(GetAlarmsThread));
                _getAlarmsThread.IsBackground = true;
                _getAlarmsThread.Priority = ThreadPriority.Normal;

                _getJobFileStatusThread = new Thread(new ThreadStart(GetJobFileStatusThread));
                _getJobFileStatusThread.IsBackground = true;
                _getJobFileStatusThread.Priority = ThreadPriority.Normal;

                _simulatorSyncThread = new Thread(new ThreadStart(SimulatorSyncThread));
                _simulatorSyncThread.IsBackground = true;
                _simulatorSyncThread.Priority = ThreadPriority.Normal;
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ~DX200()
        {
            short returnValue = 0;
            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                _getStatusThreadRun = false;
                _getPositionsThreadRun = false;
                _getAlarmsThreadRun = false;

                //close if there is a valid handle
                if (_robotHandler >= 0)
                {
                    #region
                    task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Failed to release
                                    return Motocom.BscClose(_robotHandler);
                                });

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 5) //wait 250 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscConnect(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::~DX200()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }
        }

        #endregion

        #region Methods

        #region Initialization
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short Connect()
        {
            short returnValue = 0;
            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                if (_robotHandler != -1 && _robotHandler != -8)
                {
                    #region
                    task = Task<short>.Factory.StartNew(() =>
                               {
                                   //Return Value
                                   //0 : Error
                                   //1 : Normal completion
                                   //TODO:check why return 1 without connection to DX200 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                   return Motocom.BscConnect(_robotHandler);
                               });

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 5) //wait 250 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue == 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscConnect(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 1)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::Connect()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    _communicationStatus.connected = (returnValue > 0) ? true : false;

                    if (_communicationStatus.connected == true)
                    {
                        ActualMode = RobotModeType.PLAY;

                        _getStatusThreadRun = true;
                        _getPositionsThreadRun = true;
                        _getAlarmsThreadRun = true;
                        _getJobFileStatusRun = true;

                        _getStatusThread.Start();
                        _getPositionsThread.Start();
                        _getAlarmsThread.Start();
                        _getJobFileStatusThread.Start();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = 0;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short Disconnect()
        {
            short returnValue = 0;
            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                if (_robotHandler != -1 && _robotHandler != -8)
                {
                    #region
                    task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Error
                                    //1 : Normal completion
                                    //TODO:check why return 1 without connection to DX200 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                    return Motocom.BscDisConnect(_robotHandler);
                                });

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 5) //wait 250 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue == 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscDisConnect(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 1)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::Disconnect()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    _communicationStatus.connected = (returnValue > 0) ? false : true;

                    if (_communicationStatus.connected == false)
                    {
                        _getStatusThreadRun = false;
                        _getPositionsThreadRun = false;
                        _getAlarmsThreadRun = false;
                        _getJobFileStatusRun = false;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = 0;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short Initialize()
        {
            short returnValue = -1;
            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                #region

                //DxfDocument dxf = DxfDocument.Load("F:\\Yaskawa\\1304M1C.dxf");

                task = Task<short>.Factory.StartNew(() =>
                       {
                           //try to get a handle
                           //Return Value
                           //-1 : Acquisition Failure
                           //-8 : License Error
                           //Others: Communication handler ID number
                           return Motocom.BscOpen(_commDir, (short)RobotCommunicationType.ETHERNET_SERVER);
                       });

                while (!task.IsCompleted)
                {
                    #region
                    taskWaitTimeCounter++;
                    task.Wait(_motocom32FunctionsWaitTime);
                    if (taskWaitTimeCounter > 5) //wait 250 msec maximum
                    {
                        break;
                    }
                    #endregion
                }
                if (task.IsCompleted)
                {
                    #region
                    returnValue = Convert.ToInt16(task.Result);
                    //TODO:check return and send more if needed
                    if (returnValue != 0)
                    {
                        #region
                        counterSendCommand = 0;
                        while (counterSendCommand < maxSendCommands)
                        {
                            #region

                            counterSendCommand++;

                            task = Task<short>.Factory.StartNew(() =>
                            {
                                return Motocom.BscOpen(_commDir, (short)RobotCommunicationType.ETHERNET_SERVER);
                            });

                            taskWaitTimeCounter = 0;

                            while (!task.IsCompleted)
                            {
                                #region
                                taskWaitTimeCounter++;
                                task.Wait(_motocom32FunctionsWaitTime);
                                if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                {
                                    break;
                                }
                                #endregion
                            }
                            if (task.IsCompleted)
                            {
                                returnValue = Convert.ToInt16(task.Result);
                                if (returnValue >= 0)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                returnValue = -1;
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    returnValue = -1;
                }

                if (returnValue != 0)
                {
                    Debug.WriteLine("DX200::Initialize()::Motocom.BscOpen()::Error::" + returnValue.ToString());
                }

                _robotHandler = returnValue;

                if (_robotHandler >= 0 && _ipAddress != null)
                {
                    #region
                    task = Task<short>.Factory.StartNew(() =>
                     {
                         //Return Value
                         //0 : Error
                         //1 : Normal completion
                         //TODO:check why return 1 without connection to DX200 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                         return Motocom.BscSetEServer(_robotHandler, _ipAddress);
                     });

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue == 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscSetEServer(_robotHandler, _ipAddress);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 1)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 1)
                    {
                        Debug.WriteLine("DX200::Initialize()::Motocom.BscSetEServer()::Error::" + returnValue.ToString());
                    }

                    taskWaitTimeCounter = 0;

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Error
                        //1 : Normal completion
                        //Mode Server communication mode 1: Server mode,
                        //-1: Exclusive mode
                        return Motocom.BscSetEServerMode(_robotHandler, _ipAddress, 1);
                    });

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 5) //wait 250 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue == 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscSetEServerMode(_robotHandler, _ipAddress, -1);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 1)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 1)
                    {
                        Debug.WriteLine("DX200::Initialize()::Motocom.BscSetEServerMode()::Error::" + returnValue.ToString());
                    }
                    #endregion
                }
                else
                {
                    #region
                    returnValue = 0;
                    if (_robotHandler == -1)
                    {
                        Debug.WriteLine("DX200::Initialize()::Error::Acquisition Failure");
                    }
                    if (_robotHandler == -8)
                    {
                        Debug.WriteLine("DX200::Initialize()::Error::License Error");
                    }
                    #endregion
                }

                ActualMoveSpeedSelection = RobotMoveSpeedSelectionType.ControlPoint;
                ActualRobotJointSpeed = 0.0;
                ActualReferenceFrame = RobotFrameType.Base;

                ActualRobotJointPosition.Limits = DesiredRobotJointPosition.Limits = new double[12][]
                {
                   #region

		            new double [2]{ -180.0,180.0 } , //S degree
                    new double [2]{-105.0,155.0 } , //L degree
                    new double [2]{-80.0,160.0 } , //U degree
                    new double [2]{-200.0,200.0 } , //R degree
                    new double [2]{-150.0,150.0 } , //B degree
                    new double [2]{-455.0,455.0 }, //T degree
                    new double [2]{0.5,4000.0}, //Track mm
                    new double [2]{-720.0,720.0}, //Turntable
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0} 

	                #endregion
                };
                ActualRobotTCPPosition.Limits = DesiredRobotTCPPosition.Limits = new double[12][]
                {
                    #region 

		            new double [2]{ -500,500  } , //X mm
                    new double [2]{ -500, 500 } , //Y mm
                    new double [2]{ -505, 900 } , //Z mm
                    new double [2]{ -180, 180 } , //Rx degree
                    new double [2]{ -180, 180 } , //Ry degree
                    new double [2]{ -180,180  }, //Rz degree
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0},
                    new double [2]{0,0} 

	                #endregion
                };

                //Open the configuration file using the dll location
                System.Configuration.Configuration yaskawaNetDllConfiguration = ConfigurationManager.OpenExeConfiguration(this.GetType().Assembly.Location);
                // Get the appSettings section
                AppSettingsSection yaskawaNetDllConfigAppSettings = (AppSettingsSection)yaskawaNetDllConfiguration.GetSection("appSettings");
                if (yaskawaNetDllConfigAppSettings != null)
                {
                    #region
                    ActualRobotJointPosition.Limits[0] = DesiredRobotJointPosition.Limits[0] =
                                   new double[2]
                                   {
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["SJointLimitMin"].Value),
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["SJointLimitMax"].Value )
                                   };

                    ActualRobotJointPosition.Limits[1] = DesiredRobotJointPosition.Limits[1] =
                        new double[2]
                        {
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["LJointLimitMin"].Value),
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["LJointLimitMax"].Value )
                        };

                    ActualRobotJointPosition.Limits[2] = DesiredRobotJointPosition.Limits[2] =
                        new double[2]
                        {
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["UJointLimitMin"].Value),
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["UJointLimitMax"].Value )
                        };

                    ActualRobotJointPosition.Limits[3] = DesiredRobotJointPosition.Limits[3] =
                        new double[2]
                        {
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["RJointLimitMin"].Value),
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["RJointLimitMax"].Value )
                        };

                    ActualRobotJointPosition.Limits[4] = DesiredRobotJointPosition.Limits[4] =
                        new double[2]
                        {
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["BJointLimitMin"].Value),
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["BJointLimitMax"].Value )
                        };

                    ActualRobotJointPosition.Limits[5] = DesiredRobotJointPosition.Limits[5] =
                        new double[2]
                        {
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["TJointLimitMin"].Value),
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["TJointLimitMax"].Value )
                        };

                    ActualRobotJointPosition.Limits[6] = DesiredRobotJointPosition.Limits[6] =
                        new double[2]
                        {
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["TrackLimitMin"].Value),
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["TrackLimitMax"].Value )
                        };

                    ActualRobotJointPosition.Limits[7] = DesiredRobotJointPosition.Limits[7] =
                        new double[2]
                        {
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["TurntableLimitMin"].Value),
                            Convert.ToDouble(yaskawaNetDllConfigAppSettings.Settings["TurntableLimitMax"].Value )
                        }; 
                    #endregion
                }

                _actualUserCoordinateSystem.UserCoordinateName = "UF1";
                _actualUserCoordinateSystem.XX_X = 1000;
                _actualUserCoordinateSystem.XY_Y = 500;
                SetUserFrame();

                //ReadFile(AppDomain.CurrentDomain.BaseDirectory + "YaskawaNet\\ConfigurationFiles\\SC.PRM");

                //string jobName = GetCurrentJobName();

                #endregion
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
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
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        #endregion

        #region Status and alarms
        /// <summary>
        /// 
        /// </summary>
        public void GetStatus()
        {
            short returnValue = -1;
            short d1 = 0;
            short d2 = 0;

            try
            {
                #region

                returnValue = Motocom.BscGetStatus(_robotHandler, ref d1, ref d2);

                if (returnValue >= 0)
                {
                    #region
                    //check bits and set properties
                    if (d1 != _oldStatusD1 || d2 != _oldStatusD2)
                    {
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
                        _actualRobotStatus.IsServoOn = (d2 & (1 << 6)) > 0 ? true : false;

                        _oldStatusD1 = d1;
                        _oldStatusD2 = d2;
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    Debug.WriteLine("DX200::GetStatus()::Error::" + returnValue.ToString());
                }

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
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

            try
            {
                _frameName = new StringBuilder(_frameDictionary[ActualReferenceFrame]);

                returnValue = Motocom.BscIsRobotPos(_robotHandler, _frameName, isex, ref _rConf, ref _toolNumber, ref _currentRobotTCPPositionReal[0]);

                if (returnValue != 0)
                {
                    Debug.WriteLine("DX200::GetCurrentRobotTCPPosition()::Error::" + returnValue.ToString());
                }

                #region Not in use now
                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    pose = (_roboDKRobot != null) ? _roboDKRobot.Pose() : null;

                //    if (pose != null)
                //    {
                //        // update the pose as xyzwpr
                //        _currentRobotTCPPositionTemp = pose.ToTxyzRxyz();

                //        for (int index = 0; index < _currentRobotTCPPositionTemp.Length; index++)
                //        {
                //            _currentRobotTCPPositionSimulation[index] = _currentRobotTCPPositionTemp[index];
                //        }

                //        ActualRobotTCPPositionSimulator.RobotPositions = ReportedRobotTCPPositionSimulator.RobotPositions = _currentRobotTCPPositionSimulation;
                //    }
                //    #endregion
                //} 
                #endregion

#if DEBUG
                if (returnValue == 0)
                {
                    //_currentRobotTCPPositionReal.CopyTo(ActualRobotTCPPosition.RobotPositions, 0);
                    //_currentRobotTCPPositionReal.CopyTo(ReportedRobotTCPPosition.RobotPositions, 0);
                    ActualRobotTCPPosition.RobotPositions = ReportedRobotTCPPosition.RobotPositions = _currentRobotTCPPositionReal;
                }
                // _currentRobotTCPPositionSimulation.CopyTo(ActualRobotTCPPosition.RobotPositions, 0);
                // _currentRobotTCPPositionSimulation.CopyTo(ReportedRobotTCPPosition.RobotPositions, 0);
#else
                if (returnValue == 0)
                { 
                     _currentRobotTCPPositionReal.CopyTo(ActualRobotTCPPosition.RobotPositions, 0);
                    _currentRobotTCPPositionReal.CopyTo(ReportedRobotTCPPosition.RobotPositions, 0);                     
                    //ActualRobotTCPPosition.RobotPositions = ReportedRobotTCPPosition.RobotPositions = _currentRobotTCPPositionReal;
                }
#endif
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
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
            //double[] joints = null;

            try
            {
                returnValue = Motocom.BscIsLoc(_robotHandler, ispulse, ref _rConf, ref _currentRobotJointsPositionReal[0]);

                if (returnValue != 0)
                {
                    Debug.WriteLine("DX200::GetCurrentRobotJointsPosition()::Error::" + returnValue.ToString());
                }

                #region Not in use now
                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    joints = (_roboDKRobot != null) ? _roboDKRobot.Joints() : null;

                //    if (joints != null)
                //    {
                //        #region
                //        for (int index = 0; index < joints.Length; index++)
                //        {
                //            _currentRobotJointsPositionSimulation[index] = joints[index];
                //        }

                //        ActualRobotJointPositionSimulator.RobotPositions = ReportedRobotJointPositionSimulator.RobotPositions = _currentRobotJointsPositionSimulation;
                //        //_currentRobotJointsPositionSimulation.CopyTo(ActualRobotJointPositionSimulator.RobotPositions, 0);
                //        //_currentRobotJointsPositionSimulation.CopyTo(ReportedRobotJointPositionSimulator.RobotPositions, 0);
                //        #endregion
                //    }
                //    #endregion
                //} 
                #endregion

#if DEBUG
                if (returnValue == 0)
                {
                    ActualRobotJointPosition.RobotPulsePositions = ReportedRobotJointPosition.RobotPulsePositions = _currentRobotJointsPositionReal;
                    // _currentRobotJointsPositionReal.CopyTo(ActualRobotJointPosition.RobotPositions, 0);
                    // _currentRobotJointsPositionReal.CopyTo(ReportedRobotJointPosition.RobotPositions, 0);
                }
                //ActualRobotJointPosition.RobotPositions = ReportedRobotJointPosition.RobotPositions = _currentRobotJointsPositionSimulation;
                //_currentRobotJointsPositionSimulation.CopyTo(ActualRobotJointPosition.RobotPositions, 0);
                //_currentRobotJointsPositionSimulation.CopyTo(ReportedRobotJointPosition.RobotPositions, 0);
#else
                if (returnValue == 0)
                {     
                     ActualRobotJointPosition.RobotPositions= ReportedRobotJointPosition.RobotPositions=_currentRobotJointsPositionReal;              
                     _currentRobotJointsPositionReal.CopyTo(ActualRobotJointPosition.RobotPositions, 0);
                     _currentRobotJointsPositionReal.CopyTo(ReportedRobotJointPosition.RobotPositions, 0);
                }
#endif
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <param name="alarmlist"></param>
        /// <returns></returns>
        public int GetAlarms(out ErrorData error, out ArrayList alarmlist)
        {
            short returnValue = -1;
            short errorno = -1;
            StringBuilder errormsg = new StringBuilder(256);
            short alarmsubno = -1;
            StringBuilder alarmmsg = new StringBuilder(256);

            alarmlist = new ArrayList();
            error = new ErrorData();

            returnValue = Motocom.BscReadAlarmS(_robotHandler, ref errorno, errormsg);

            if (returnValue == 0)
            {
                #region
                error.ErrorNo = errorno;
                error.ErrorMsg = errormsg.ToString();

                returnValue = Motocom.BscGetFirstAlarmS(_robotHandler, ref alarmsubno, alarmmsg);

                if (returnValue > 0)
                {
                    #region
                    alarmlist.Add(new AlarmHistoryItem(returnValue, alarmsubno, alarmmsg.ToString()));
                    Debug.WriteLine("DX200::GetAlarms()::Error::Alarm detected::" + alarmmsg.ToString());
                    do
                    {
                        #region

                        //TODO:Vlad::try get all list of alarms
                        AlarmMessage = alarmmsg.ToString(); //read only last message

                        returnValue = Motocom.BscGetNextAlarmS(_robotHandler, ref alarmsubno, alarmmsg);

                        if (returnValue > 0)
                        {
                            alarmlist.Add(new AlarmHistoryItem(returnValue, alarmsubno, alarmmsg.ToString()));
                        }
                        #endregion
                    }
                    while (returnValue > 0);

                    #endregion

                    returnValue = Motocom.BscReset(_robotHandler);
                }
                #endregion
            }

            return alarmlist.Count;
        }
        #endregion

        #region Variables and IO
        /// <summary>
        /// Reads multiple variables of simple data type
        /// </summary>
        /// <param name="SimpleVar"></param>
        public void ReadSimpleTypeVariable(SimpleTypeVarList SimpleVar)
        {
            lock (_robotAccessLock)
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
            lock (_robotAccessLock)
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
            lock (_robotAccessLock)
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
            lock (_robotAccessLock)
            {
                short ret = Motocom.BscHostPutVarDataM(_robotHandler, (short)SimpleVar.VarType, SimpleVar.StartIndex, SimpleVar.ListSize, ref SimpleVar.VarListArray[0]);
                if (ret != 0)
                    throw new Exception("Error executing BscHostPutVarDataM");
            }
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
            lock (_robotAccessLock)
            {
                short ret = Motocom.BscWriteIO2(_robotHandler, Address, 1, ref iovalue);
                if (ret != 0)
                    throw new Exception("Error executing BscWriteIO2");

                return ret;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartAddress"></param>
        /// <param name="NumberOfGroups"></param>
        /// <param name="ioValues"></param>
        /// <returns></returns>
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
            lock (_robotAccessLock)
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

            lock (_robotAccessLock)
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

            lock (_robotAccessLock)
            {
                ret = Motocom.BscReadIO2(_robotHandler, Address, 1, ref IOVal);
                if (ret != 0)
                    throw new Exception("Error reading IO !");
            }
            return (IOVal > 0 ? true : false);
        }
        #endregion

        #region Job files methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public short ReadFile(string file)
        {
            short returnValue = 0;

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            StringBuilder _fName = new StringBuilder(Path.GetFileName(file), 255);
            StringBuilder _path = new StringBuilder(Path.GetDirectoryName(file), 255);

            try
            {
                #region

                lock (_fileAccessDirLock)
                {
                    lock (_robotAccessLock)
                    {
                        #region

                        task = Task<short>.Factory.StartNew(() =>
                        {
                            //Return Value
                            //0 : Normal completion
                            //Others: Receiving error
                            return Motocom.BscUpLoadEx(_robotHandler, _fName, _path, true);
                        });

                        taskWaitTimeCounter = 0;

                        while (!task.IsCompleted)
                        {
                            #region
                            taskWaitTimeCounter++;
                            task.Wait(_motocom32FunctionsWaitTime);
                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                            {
                                break;
                            }
                            #endregion
                        }

                        if (task.IsCompleted)
                        {
                            #region
                            returnValue = Convert.ToInt16(task.Result);
                            //TODO:check return and send more if needed
                            if (returnValue != 0)
                            {
                                #region
                                counterSendCommand = 0;
                                while (counterSendCommand < maxSendCommands)
                                {
                                    #region

                                    counterSendCommand++;

                                    task = Task<short>.Factory.StartNew(() =>
                                    {
                                        //Return Value
                                        //0 : Normal completion
                                        //Others: Receiving error
                                        return Motocom.BscUpLoadEx(_robotHandler, _fName, _path, true);
                                    });

                                    taskWaitTimeCounter = 0;

                                    while (!task.IsCompleted)
                                    {
                                        #region
                                        taskWaitTimeCounter++;
                                        task.Wait(_motocom32FunctionsWaitTime);
                                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                        {
                                            break;
                                        }
                                        #endregion
                                    }
                                    if (task.IsCompleted)
                                    {
                                        returnValue = Convert.ToInt16(task.Result);
                                        if (returnValue == 0)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        returnValue = -1;
                                    }
                                    #endregion
                                }
                                if (counterSendCommand == maxSendCommands)
                                {
                                    Debug.WriteLine("DX200::ReadFile()::Error::" + returnValue.ToString());
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            returnValue = -1;
                        }

                        #endregion
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        public short WriteFile(string file)
        {
            short returnValue = 0;

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            StringBuilder _fName = new StringBuilder(Path.GetFileName(file), 255);
            StringBuilder _path = new StringBuilder(Path.GetDirectoryName(file), 255);

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Transmission error
                        return Motocom.BscDownLoadEx(_robotHandler, _fName, _path, true);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }

                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscDownLoadEx(_robotHandler, _fName, _path, true);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::WriteFile()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue == 0)
                    {
                        // _actualTrajectory.Clear();
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="JobList"></param>
        /// <returns></returns>
        public int GetJobList(ArrayList JobList)
        {
            short ret;
            StringBuilder jobname = new StringBuilder(Motocom.MaxJobNameLength + 1);

            lock (_robotAccessLock)
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
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public short DeleteJob(string fileName)
        {
            short returnValue = -1;

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                if (fileName.ToLower().Contains(".jbi"))
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    lock (_robotAccessLock)
                    {
                        task = Task<short>.Factory.StartNew(() =>
                        {
                            //Return Value
                            //0 : Normal completion
                            //Others: Error codes
                            return Motocom.BscSelectJob(_robotHandler, fileName);
                        });

                        taskWaitTimeCounter = 0;

                        while (!task.IsCompleted)
                        {
                            #region
                            taskWaitTimeCounter++;
                            task.Wait(_motocom32FunctionsWaitTime);
                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                            {
                                break;
                            }
                            #endregion
                        }

                        if (task.IsCompleted)
                        {
                            #region
                            returnValue = Convert.ToInt16(task.Result);
                            //TODO:check return and send more if needed
                            if (returnValue != 0)
                            {
                                #region
                                counterSendCommand = 0;
                                while (counterSendCommand < maxSendCommands)
                                {
                                    #region

                                    counterSendCommand++;

                                    task = Task<short>.Factory.StartNew(() =>
                                    {
                                        //Return Value
                                        //0 : Normal completion
                                        //Others: Error codes
                                        return Motocom.BscSelectJob(_robotHandler, fileName);
                                    });

                                    taskWaitTimeCounter = 0;

                                    while (!task.IsCompleted)
                                    {
                                        #region
                                        taskWaitTimeCounter++;
                                        task.Wait(_motocom32FunctionsWaitTime);
                                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                        {
                                            break;
                                        }
                                        #endregion
                                    }
                                    if (task.IsCompleted)
                                    {
                                        returnValue = Convert.ToInt16(task.Result);
                                        if (returnValue == 0)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        returnValue = -1;
                                    }
                                    #endregion
                                }
                                if (counterSendCommand == maxSendCommands)
                                {
                                    Debug.WriteLine("DX200::DeleteJob()::SelectJob()::Error::" + returnValue.ToString());
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            returnValue = -1;
                        }

                        if (returnValue == 0)
                        {
                            #region
                            taskWaitTimeCounter = 0;
                            counterSendCommand = 0;

                            task = Task<short>.Factory.StartNew(() =>
                            {
                                //Return Value
                                //0 : Normal completion
                                //1 : Cannot delete
                                //Otherss: Error codes
                                return Motocom.BscDeleteJob(_robotHandler);
                            });

                            taskWaitTimeCounter = 0;

                            while (!task.IsCompleted)
                            {
                                #region
                                taskWaitTimeCounter++;
                                task.Wait(_motocom32FunctionsWaitTime);
                                if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                {
                                    break;
                                }
                                #endregion
                            }

                            if (task.IsCompleted)
                            {
                                #region
                                returnValue = Convert.ToInt16(task.Result);
                                //TODO:check return and send more if needed
                                if (returnValue != 0)
                                {
                                    #region
                                    counterSendCommand = 0;
                                    while (counterSendCommand < maxSendCommands)
                                    {
                                        #region

                                        counterSendCommand++;

                                        task = Task<short>.Factory.StartNew(() =>
                                        {
                                            //Return Value
                                            //0 : Normal completion
                                            //1 : Cannot delete
                                            //Others: Error codes
                                            return Motocom.BscDeleteJob(_robotHandler);
                                        });

                                        taskWaitTimeCounter = 0;

                                        while (!task.IsCompleted)
                                        {
                                            #region
                                            taskWaitTimeCounter++;
                                            task.Wait(_motocom32FunctionsWaitTime);
                                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                            {
                                                break;
                                            }
                                            #endregion
                                        }
                                        if (task.IsCompleted)
                                        {
                                            returnValue = Convert.ToInt16(task.Result);
                                            if (returnValue == 0)
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            returnValue = -1;
                                        }
                                        #endregion
                                    }
                                    if (counterSendCommand == maxSendCommands)
                                    {
                                        if (returnValue == 1)
                                        {
                                            Debug.WriteLine("DX200::DeleteJob()::BscDeleteJob()::Error::Cannot delete.");
                                        }
                                        else
                                        {
                                            Debug.WriteLine("DX200::DeleteJob()::BscDeleteJob()::Error::" + returnValue.ToString());
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                returnValue = -1;
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    Debug.WriteLine("DX200::DeleteJob()::Error::Current job name extension .JBI not specified.");
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public short StartJob(string fileName)
        {
            short returnValue = -1;

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                if (fileName.ToLower().Contains(".jbi"))
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    lock (_robotAccessLock)
                    {
                        task = Task<short>.Factory.StartNew(() =>
                        {
                            //Return Value
                            //0 : Normal completion
                            //Others: Error codes
                            return Motocom.BscSelectJob(_robotHandler, fileName);
                        });

                        taskWaitTimeCounter = 0;

                        while (!task.IsCompleted)
                        {
                            #region
                            taskWaitTimeCounter++;
                            task.Wait(_motocom32FunctionsWaitTime);
                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                            {
                                break;
                            }
                            #endregion
                        }

                        if (task.IsCompleted)
                        {
                            #region
                            returnValue = Convert.ToInt16(task.Result);
                            //TODO:check return and send more if needed
                            if (returnValue != 0)
                            {
                                #region
                                counterSendCommand = 0;
                                while (counterSendCommand < maxSendCommands)
                                {
                                    #region

                                    counterSendCommand++;

                                    task = Task<short>.Factory.StartNew(() =>
                                    {
                                        //Return Value
                                        //0 : Normal completion
                                        //Others: Error codes
                                        return Motocom.BscSelectJob(_robotHandler, fileName);
                                    });

                                    taskWaitTimeCounter = 0;

                                    while (!task.IsCompleted)
                                    {
                                        #region
                                        taskWaitTimeCounter++;
                                        task.Wait(_motocom32FunctionsWaitTime);
                                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                        {
                                            break;
                                        }
                                        #endregion
                                    }
                                    if (task.IsCompleted)
                                    {
                                        returnValue = Convert.ToInt16(task.Result);
                                        if (returnValue == 0)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        returnValue = -1;
                                    }
                                    #endregion
                                }
                                if (counterSendCommand == maxSendCommands)
                                {
                                    Debug.WriteLine("DX200::StartJob()::SelectJob()::Error::" + returnValue.ToString());
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            returnValue = -1;
                        }

                        if (returnValue == 0)
                        {
                            #region
                            taskWaitTimeCounter = 0;
                            counterSendCommand = 0;

                            task = Task<short>.Factory.StartNew(() =>
                            {
                                //Return Value
                                //0 : Normal completion
                                //1 : Current job name not specified
                                //Others : Error codes
                                return Motocom.BscStartJob(_robotHandler);
                            });

                            taskWaitTimeCounter = 0;

                            while (!task.IsCompleted)
                            {
                                #region
                                taskWaitTimeCounter++;
                                task.Wait(_motocom32FunctionsWaitTime);
                                if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                {
                                    break;
                                }
                                #endregion
                            }

                            if (task.IsCompleted)
                            {
                                #region
                                returnValue = Convert.ToInt16(task.Result);
                                //TODO:check return and send more if needed
                                if (returnValue != 0)
                                {
                                    #region
                                    counterSendCommand = 0;
                                    while (counterSendCommand < maxSendCommands)
                                    {
                                        #region

                                        counterSendCommand++;

                                        task = Task<short>.Factory.StartNew(() =>
                                        {
                                            //Return Value
                                            //0 : Normal completion
                                            //1 : Current job name not specified
                                            //Others : Error codes
                                            return Motocom.BscStartJob(_robotHandler);
                                        });

                                        taskWaitTimeCounter = 0;

                                        while (!task.IsCompleted)
                                        {
                                            #region
                                            taskWaitTimeCounter++;
                                            task.Wait(_motocom32FunctionsWaitTime);
                                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                            {
                                                break;
                                            }
                                            #endregion
                                        }
                                        if (task.IsCompleted)
                                        {
                                            returnValue = Convert.ToInt16(task.Result);
                                            if (returnValue == 0)
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            returnValue = -1;
                                        }
                                        #endregion
                                    }
                                    if (counterSendCommand == maxSendCommands)
                                    {
                                        if (returnValue == 1)
                                        {
                                            Debug.WriteLine("DX200::StartJob()::StartJob()::Error::Current job name not specified.");
                                        }
                                        else
                                        {
                                            Debug.WriteLine("DX200::StartJob()::StartJob()::Error::" + returnValue.ToString());
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                returnValue = -1;
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                else
                {
                    Debug.WriteLine("DX200::StartJob()::Error::Current job name extension .JBI not specified.");
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public string ChangeTask(short TaskID)
        {
            short ret;
            StringBuilder jobName = new StringBuilder(255);

            lock (_robotAccessLock)
            {
                if (TaskID > 0)
                {
                    ret = Motocom.BscChangeTask(_robotHandler, TaskID);
                    if (ret != 0)
                    {
                        throw new Exception("Error changing task !");
                    }
                }
                ret = Motocom.BscIsJobName(_robotHandler, jobName, 255);
                if (ret != 0)
                    throw new Exception("Error getting current job name !");
            }
            return jobName.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="fileName"></param>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public short SetCurrentJob(short taskID, string fileName, short lineNumber)
        {
            short returnValue = -1;

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                if (fileName.ToLower().Contains(".jbi"))
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    lock (_robotAccessLock)
                    {
                        #region
                        task = Task<short>.Factory.StartNew(() =>
                                       {
                                           //Return Value
                                           //0 : Normal completion
                                           //Others: Error codes
                                           return Motocom.BscChangeTask(_robotHandler, taskID);
                                       });

                        taskWaitTimeCounter = 0;

                        while (!task.IsCompleted)
                        {
                            #region
                            taskWaitTimeCounter++;
                            task.Wait(_motocom32FunctionsWaitTime);
                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                            {
                                break;
                            }
                            #endregion
                        }

                        if (task.IsCompleted)
                        {
                            #region
                            returnValue = Convert.ToInt16(task.Result);
                            //TODO:check return and send more if needed
                            if (returnValue != 0)
                            {
                                #region
                                counterSendCommand = 0;
                                while (counterSendCommand < maxSendCommands)
                                {
                                    #region

                                    counterSendCommand++;

                                    task = Task<short>.Factory.StartNew(() =>
                                    {
                                        //Return Value
                                        //0 : Normal completion
                                        //Others: Error codes
                                        return Motocom.BscChangeTask(_robotHandler, taskID);
                                    });

                                    taskWaitTimeCounter = 0;

                                    while (!task.IsCompleted)
                                    {
                                        #region
                                        taskWaitTimeCounter++;
                                        task.Wait(_motocom32FunctionsWaitTime);
                                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                        {
                                            break;
                                        }
                                        #endregion
                                    }
                                    if (task.IsCompleted)
                                    {
                                        returnValue = Convert.ToInt16(task.Result);
                                        if (returnValue == 0)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        returnValue = -1;
                                    }
                                    #endregion
                                }
                                if (counterSendCommand == maxSendCommands)
                                {
                                    Debug.WriteLine("DX200::SetCurrentJob()::BscChangeTask()::Error::" + returnValue.ToString());
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            returnValue = -1;
                        }

                        if (returnValue == 0)
                        {
                            #region
                            taskWaitTimeCounter = 0;
                            counterSendCommand = 0;

                            task = Task<short>.Factory.StartNew(() =>
                            {
                                //Return Value
                                //0 : Normal completion
                                //Others: Error codes
                                return Motocom.BscSelectJob(_robotHandler, fileName);
                            });

                            taskWaitTimeCounter = 0;

                            while (!task.IsCompleted)
                            {
                                #region
                                taskWaitTimeCounter++;
                                task.Wait(_motocom32FunctionsWaitTime);
                                if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                {
                                    break;
                                }
                                #endregion
                            }

                            if (task.IsCompleted)
                            {
                                #region
                                returnValue = Convert.ToInt16(task.Result);
                                //TODO:check return and send more if needed
                                if (returnValue != 0)
                                {
                                    #region
                                    counterSendCommand = 0;
                                    while (counterSendCommand < maxSendCommands)
                                    {
                                        #region

                                        counterSendCommand++;

                                        task = Task<short>.Factory.StartNew(() =>
                                        {
                                            //Return Value
                                            //0 : Normal completion
                                            //Others: Error codes
                                            return Motocom.BscSelectJob(_robotHandler, fileName);
                                        });

                                        taskWaitTimeCounter = 0;

                                        while (!task.IsCompleted)
                                        {
                                            #region
                                            taskWaitTimeCounter++;
                                            task.Wait(_motocom32FunctionsWaitTime);
                                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                            {
                                                break;
                                            }
                                            #endregion
                                        }
                                        if (task.IsCompleted)
                                        {
                                            returnValue = Convert.ToInt16(task.Result);
                                            if (returnValue == 0)
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            returnValue = -1;
                                        }
                                        #endregion
                                    }
                                    if (counterSendCommand == maxSendCommands)
                                    {
                                        Debug.WriteLine("DX200::SetCurrentJob()::BscSelectJob()::Error::" + returnValue.ToString());
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                returnValue = -1;
                            }

                            if (returnValue == 0)
                            {
                                #region
                                taskWaitTimeCounter = 0;
                                counterSendCommand = 0;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscSetLineNumber(_robotHandler, lineNumber);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }

                                if (task.IsCompleted)
                                {
                                    #region
                                    returnValue = Convert.ToInt16(task.Result);
                                    //TODO:check return and send more if needed
                                    if (returnValue != 0)
                                    {
                                        #region
                                        counterSendCommand = 0;
                                        while (counterSendCommand < maxSendCommands)
                                        {
                                            #region

                                            counterSendCommand++;

                                            task = Task<short>.Factory.StartNew(() =>
                                            {
                                                //Return Value
                                                //0 : Normal completion
                                                //Others: Error codes
                                                return Motocom.BscSetLineNumber(_robotHandler, lineNumber);
                                            });

                                            taskWaitTimeCounter = 0;

                                            while (!task.IsCompleted)
                                            {
                                                #region
                                                taskWaitTimeCounter++;
                                                task.Wait(_motocom32FunctionsWaitTime);
                                                if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                                {
                                                    break;
                                                }
                                                #endregion
                                            }
                                            if (task.IsCompleted)
                                            {
                                                returnValue = Convert.ToInt16(task.Result);
                                                if (returnValue == 0)
                                                {
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                returnValue = -1;
                                            }
                                            #endregion
                                        }
                                        if (counterSendCommand == maxSendCommands)
                                        {
                                            Debug.WriteLine("DX200::SetCurrentJob()::BscSetLineNumber()::Error::" + returnValue.ToString());
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    Debug.WriteLine("DX200::SetCurrentJob()::Error::Current job name extension .JBI not specified.");
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public short GetCurrentLine(short taskID)
        {
            short returnValue = -1;

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                if (taskID > 0)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    lock (_robotAccessLock)
                    {
                        #region
                        task = Task<short>.Factory.StartNew(() =>
                                        {
                                            //Return Value
                                            //0 : Normal completion
                                            //Others: Error codes
                                            return Motocom.BscChangeTask(_robotHandler, taskID);
                                        });

                        taskWaitTimeCounter = 0;

                        while (!task.IsCompleted)
                        {
                            #region
                            taskWaitTimeCounter++;
                            task.Wait(_motocom32FunctionsWaitTime);
                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                            {
                                break;
                            }
                            #endregion
                        }

                        if (task.IsCompleted)
                        {
                            #region
                            returnValue = Convert.ToInt16(task.Result);
                            //TODO:check return and send more if needed
                            if (returnValue != 0)
                            {
                                #region
                                counterSendCommand = 0;
                                while (counterSendCommand < maxSendCommands)
                                {
                                    #region

                                    counterSendCommand++;

                                    task = Task<short>.Factory.StartNew(() =>
                                    {
                                        //Return Value
                                        //0 : Normal completion
                                        //Others: Error codes
                                        return Motocom.BscChangeTask(_robotHandler, taskID);
                                    });

                                    taskWaitTimeCounter = 0;

                                    while (!task.IsCompleted)
                                    {
                                        #region
                                        taskWaitTimeCounter++;
                                        task.Wait(_motocom32FunctionsWaitTime);
                                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                        {
                                            break;
                                        }
                                        #endregion
                                    }
                                    if (task.IsCompleted)
                                    {
                                        returnValue = Convert.ToInt16(task.Result);
                                        if (returnValue == 0)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        returnValue = -1;
                                    }
                                    #endregion
                                }
                                if (counterSendCommand == maxSendCommands)
                                {
                                    Debug.WriteLine("DX200::GetCurrentLine()::BscChangeTask()::Error::" + returnValue.ToString());
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            returnValue = -1;
                        }

                        if (returnValue == 0)
                        {
                            #region
                            taskWaitTimeCounter = 0;
                            counterSendCommand = 0;

                            task = Task<short>.Factory.StartNew(() =>
                            {
                                //Return Value
                                //-1 : Acquisition Failure
                                //Others: Line numbers
                                return Motocom.BscIsJobLine(_robotHandler);
                            });

                            taskWaitTimeCounter = 0;

                            while (!task.IsCompleted)
                            {
                                #region
                                taskWaitTimeCounter++;
                                task.Wait(_motocom32FunctionsWaitTime);
                                if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                {
                                    break;
                                }
                                #endregion
                            }

                            if (task.IsCompleted)
                            {
                                #region
                                returnValue = Convert.ToInt16(task.Result);
                                //TODO:check return and send more if needed
                                if (returnValue < 0)
                                {
                                    #region
                                    counterSendCommand = 0;
                                    while (counterSendCommand < maxSendCommands)
                                    {
                                        #region

                                        counterSendCommand++;

                                        task = Task<short>.Factory.StartNew(() =>
                                        {
                                            //Return Value
                                            //-1 : Acquisition Failure
                                            //Others: Line numbers
                                            return Motocom.BscIsJobLine(_robotHandler);
                                        });

                                        taskWaitTimeCounter = 0;

                                        while (!task.IsCompleted)
                                        {
                                            #region
                                            taskWaitTimeCounter++;
                                            task.Wait(_motocom32FunctionsWaitTime);
                                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                            {
                                                break;
                                            }
                                            #endregion
                                        }
                                        if (task.IsCompleted)
                                        {
                                            returnValue = Convert.ToInt16(task.Result);
                                            if (returnValue > -1)
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            returnValue = -1;
                                        }
                                        #endregion
                                    }
                                    if (counterSendCommand == maxSendCommands)
                                    {
                                        Debug.WriteLine("DX200::StartJob()::StartJob()::Error::" + returnValue.ToString());
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                returnValue = -1;
                            }
                            #endregion
                        }
                        #endregion
                    }

                    #endregion
                }
                else
                {
                    Debug.WriteLine("DX200::GetCurrentLine()::Error::Task ID not in range.");
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public short SetCurrentLine(short line)
        {
            short returnValue = -1;

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                #region

                lock (_robotAccessLock)
                {
                    #region
                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscSetLineNumber(_robotHandler, line);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }

                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscSetLineNumber(_robotHandler, line);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::SetCurrentLine()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    #endregion
                }

                #endregion
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short ContinueJob()
        {
            short returnValue = 0;
            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscContinueJob(_robotHandler);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 1 second maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscContinueJob(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 1 second maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::ContinueJob()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public short CreateJobFile(string fileName)
        {
            short returnValue = 0;

            try
            {
                _actualJobFile.FileName = AppDomain.CurrentDomain.BaseDirectory + "YaskawaNet\\JobFiles\\" + fileName;
                //_actualJobFile.SerializeJointsPulseTrajectory(_actualJointsTrajectory);
                _actualJobFile.SerializeTCPTrajectory(_actualTCPTrajectory);
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public short DownloadJobFile(string fileName)
        {
            short returnValue = 0;

            try
            {
                WriteFile(AppDomain.CurrentDomain.BaseDirectory + "YaskawaNet\\JobFiles\\" + fileName);
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCurrentJobName()
        {
            short returnValue = -1;
            StringBuilder _currentJobName = new StringBuilder(Motocom.MaxJobNameLength + 1);

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //-1 : Acquisition Failure
                        //0 : Normal completion
                        return Motocom.BscIsJobName(_robotHandler, _currentJobName, (short)_currentJobName.Length);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }

                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //-1 : Acquisition Failure
                                    //0 : Normal completion
                                    return Motocom.BscIsJobName(_robotHandler, _currentJobName, (short)_currentJobName.Length);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::GetCurrentJobName()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return (returnValue == 0) ? _currentJobName.ToString() : string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flleName"></param>
        /// <param name="frameName"></param>
        /// <returns></returns>
        public short ConvertPulseJobToRelative(string flleName, string frameName)
        {
            short returnValue = -1;
            StringBuilder _fileName = new StringBuilder(flleName);
            StringBuilder _frameName = new StringBuilder(frameName);

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscConvertJobP2R(_robotHandler, _fileName, _frameName);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }

                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscConvertJobP2R(_robotHandler, _fileName, _frameName);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::ConvertPulseJobToRelative()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flleName"></param>
        /// <param name="conversionMethod"></param>
        /// <param name="referencePosition"></param>
        /// <returns></returns>
        public short ConvertRelativeJobToPulse(string flleName, short conversionMethod, string referencePosition)
        {
            short returnValue = -1;
            StringBuilder _fileName = new StringBuilder(flleName);
            StringBuilder _referencePosition = new StringBuilder(referencePosition);

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscConvertJobR2P(_robotHandler, _fileName, conversionMethod, _referencePosition);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }

                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscConvertJobR2P(_robotHandler, _fileName, conversionMethod, _referencePosition);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::ConvertRelativeJobToPulse()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        #endregion

        #region Movement

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short SetUserFrame()
        {
            short returnValue = 0;

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    task = Task<short>.Factory.StartNew(() =>
                      {
                          //Return Value
                          //0 : Normal completion
                          //Others: Error codes
                          return Motocom.BscPutUFrame(_robotHandler, new StringBuilder(_actualUserCoordinateSystem.UserCoordinateName),
                              ref _actualUserCoordinateSystem.UserCoordinateData[0]);
                      });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }

                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscPutUFrame(_robotHandler, new StringBuilder(_actualUserCoordinateSystem.UserCoordinateName),
                                        ref _actualUserCoordinateSystem.UserCoordinateData[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::SetUserFrame()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short GetUserFrame()
        {
            short returnValue = 0;

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //-1 : User coordinate name error
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscGetUFrame(_robotHandler, new StringBuilder(_reportedUserCoordinateSystem.UserCoordinateName),
                            ref _reportedUserCoordinateSystem.UserCoordinateData[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }

                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //-1 : User coordinate name error
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscGetUFrame(_robotHandler, new StringBuilder(_reportedUserCoordinateSystem.UserCoordinateName),
                                        ref _reportedUserCoordinateSystem.UserCoordinateData[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::SetUserFrame()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointJogMove(int jointIndex, double speed)
        {
            short returnValue = -1;
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] _currentRobotJointsPositionReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    if (speed != 0)
                    {
                        #region
                        if (_actualRobotStatus.IsCommandHold == true)
                        {
                            HoldOff();
                        }

                        simulatorSpeed = speed;

                        ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositionsReal, 0);

                        jointsPositionsReal[jointIndex] = (speed >= 0) ? DesiredRobotJointPosition.LimitsPulse[jointIndex][1] : DesiredRobotJointPosition.LimitsPulse[jointIndex][0];

                        if (jointIndex == 0)
                        {
                            speed = (Math.Abs(speed) > _sJointMaxSpeed) ? (speed / Math.Abs(speed)) * _sJointMaxSpeed : speed;
                            realRobotSpeed = Math.Abs(speed / (_sJointMaxSpeed / 100.0));
                        }
                        if (jointIndex == 1)
                        {
                            speed = (Math.Abs(speed) > _lJointMaxSpeed) ? (speed / Math.Abs(speed)) * _lJointMaxSpeed : speed;
                            realRobotSpeed = Math.Abs(speed / (_lJointMaxSpeed / 100.0));
                        }
                        if (jointIndex == 2)
                        {
                            speed = (Math.Abs(speed) > _uJointMaxSpeed) ? (speed / Math.Abs(speed)) * _uJointMaxSpeed : speed;
                            realRobotSpeed = Math.Abs(speed / (_uJointMaxSpeed / 100.0));
                        }
                        if (jointIndex == 3)
                        {
                            speed = (Math.Abs(speed) > _rJointMaxSpeed) ? (speed / Math.Abs(speed)) * _rJointMaxSpeed : speed;
                            realRobotSpeed = Math.Abs(speed / (_rJointMaxSpeed / 100.0));
                        }
                        if (jointIndex == 4)
                        {
                            speed = (Math.Abs(speed) > _bJointMaxSpeed) ? (speed / Math.Abs(speed)) * _bJointMaxSpeed : speed;
                            realRobotSpeed = Math.Abs(speed / (_bJointMaxSpeed / 100.0));
                        }
                        if (jointIndex == 5)
                        {
                            speed = (Math.Abs(speed) > _tJointMaxSpeed) ? (speed / Math.Abs(speed)) * _tJointMaxSpeed : speed;
                            realRobotSpeed = Math.Abs(speed / (_tJointMaxSpeed / 100.0));
                        }

                        _actualRobotJointSpeed = realRobotSpeed = (realRobotSpeed > _maxJointSpeed) ? _maxJointSpeed : realRobotSpeed;

                        task = Task<short>.Factory.StartNew(() =>
                        {
                            //Return Value
                            //0 : Normal completion
                            //Others: Error codes
                            return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                        });

                        taskWaitTimeCounter = 0;

                        while (!task.IsCompleted)
                        {
                            #region
                            taskWaitTimeCounter++;
                            task.Wait(_motocom32FunctionsWaitTime);
                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                            {
                                break;
                            }
                            #endregion
                        }
                        if (task.IsCompleted)
                        {
                            #region
                            returnValue = Convert.ToInt16(task.Result);
                            //TODO:check return and send more if needed
                            if (returnValue != 0)
                            {
                                #region
                                counterSendCommand = 0;
                                while (counterSendCommand < maxSendCommands)
                                {
                                    #region

                                    counterSendCommand++;

                                    task = Task<short>.Factory.StartNew(() =>
                                    {
                                        return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                                    });

                                    taskWaitTimeCounter = 0;

                                    while (!task.IsCompleted)
                                    {
                                        #region
                                        taskWaitTimeCounter++;
                                        task.Wait(_motocom32FunctionsWaitTime);
                                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                        {
                                            break;
                                        }
                                        #endregion
                                    }
                                    if (task.IsCompleted)
                                    {
                                        returnValue = Convert.ToInt16(task.Result);
                                        if (returnValue == 0)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        returnValue = -1;
                                    }
                                    #endregion
                                }
                                if (counterSendCommand == maxSendCommands)
                                {
                                    Debug.WriteLine("DX200::JointJogMove()::Error::Real robot connection problem.");
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            returnValue = -1;
                        }

                        if (returnValue != 0)
                        {
                            Debug.WriteLine("DX200::JointJogMove()::Error::" + returnValue.ToString());
                        }
                        Debug.WriteLine("DX200::JointJogMove:" + jointIndex.ToString() + "::" + "desired position: " + jointsPositionsReal[jointIndex].ToString());

                        #endregion
                    }
                }

                #region Not in use
                //if (_useRoboDKSimulator)
                //{
                //    #region

                //    //get actual robot position
                //    ReportedRobotJointPosition.RobotPositions.CopyTo(jointsPositionsSimulator, 0);

                //    _roboDKRobot.SetSpeed(-1, Math.Abs(simulatorSpeed));

                //    jointsPositionsSimulator[jointIndex] = (simulatorSpeed > 0) ? DesiredRobotJointPosition.Limits[jointIndex][1] : DesiredRobotJointPosition.Limits[jointIndex][0];

                //    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);

                //    #endregion
                //} 
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointMask"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointsJogMove(int jointMask, double speed)
        {
            short returnValue = -1;
            //double[] jointsPositionsSimulator = null;
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;

            try
            {
                #region
                Debug.WriteLine("DX200::JointsJogMove:" + jointMask.ToString());

                if (_actualRobotStatus.IsCommandHold == true)
                {
                    HoldOff();
                }

                realRobotSpeed = simulatorSpeed = speed;

                realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositionsReal, 0);

                for (int i = 0; i < jointsPositionsReal.Length; i++)
                {
                    #region
                    if ((jointMask & (1 << i)) > 0)
                    {
                        jointsPositionsReal[i] = (realRobotSpeed > 0) ? DesiredRobotJointPosition.LimitsPulse[i][1] : DesiredRobotJointPosition.LimitsPulse[i][0];
                    }
                    #endregion
                }

                _actualRobotJointSpeed = realRobotSpeed = 10;//TODO:for safety !!!!!!!!!!!!!!!!!!!! Math.Abs(realRobotSpeed);

                task = Task<short>.Factory.StartNew(() =>
                {
                    #region
                    //Return Value
                    //0 : Normal completion
                    //Others: Error codes
                    return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                    #endregion
                });

                taskWaitTimeCounter = 0;

                while (!task.IsCompleted)
                {
                    #region
                    taskWaitTimeCounter++;
                    task.Wait(_motocom32FunctionsWaitTime);
                    if (taskWaitTimeCounter > 5) //wait 250 msec maximum
                    {
                        break;
                    }
                    #endregion
                }
                if (task.IsCompleted)
                {
                    returnValue = Convert.ToInt16(task.Result);
                }
                else
                {
                    returnValue = -1;
                }

                #region Not in use
                //if (_useRoboDKSimulator)
                //{
                //    #region

                //    jointsPositionsSimulator = _roboDKRobot.Joints();

                //    if (jointsPositionsSimulator != null)
                //    {
                //        #region
                //        //get actual robot position
                //        for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                //        {
                //            jointsPositionsSimulator[i] = ReportedRobotJointPositionSimulator.RobotPositions[i];
                //        }

                //        _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                //        for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                //        {
                //            if ((jointMask & (1 << i)) > 0)
                //            {
                //                jointsPositionsSimulator[i] = (speed > 0) ? DesiredRobotJointPosition.Limits[i][1] : DesiredRobotJointPosition.Limits[i][0];
                //            }
                //        }

                //        _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                //        #endregion
                //    }

                //    #endregion
                //} 
                #endregion 
                #endregion
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointAbsoluteMove(int jointIndex, double speed)
        {
            short returnValue = -1;
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    simulatorSpeed = speed;

                    ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositionsReal, 0);
                    jointsPositionsReal[jointIndex] = DesiredRobotJointPosition.RobotPulsePositions[jointIndex];

                    if (jointIndex == 0)
                    {
                        speed = (speed > _sJointMaxSpeed) ? _sJointMaxSpeed : speed;
                        realRobotSpeed = Math.Abs(speed / (_sJointMaxSpeed / 100.0));
                    }
                    if (jointIndex == 1)
                    {
                        speed = (speed > _lJointMaxSpeed) ? _lJointMaxSpeed : speed;
                        realRobotSpeed = Math.Abs(speed / (_lJointMaxSpeed / 100.0));
                    }
                    if (jointIndex == 2)
                    {
                        speed = (speed > _uJointMaxSpeed) ? _uJointMaxSpeed : speed;
                        realRobotSpeed = Math.Abs(speed / (_uJointMaxSpeed / 100.0));
                    }
                    if (jointIndex == 3)
                    {
                        speed = (speed > _rJointMaxSpeed) ? _rJointMaxSpeed : speed;
                        realRobotSpeed = Math.Abs(speed / (_rJointMaxSpeed / 100.0));
                    }
                    if (jointIndex == 4)
                    {
                        speed = (speed > _bJointMaxSpeed) ? _bJointMaxSpeed : speed;
                        realRobotSpeed = Math.Abs(speed / (_bJointMaxSpeed / 100.0));
                    }
                    if (jointIndex == 5)
                    {
                        speed = (speed > _tJointMaxSpeed) ? _tJointMaxSpeed : speed;
                        realRobotSpeed = Math.Abs(speed / (_tJointMaxSpeed / 100.0));
                    }

                    _actualRobotJointSpeed = realRobotSpeed = (realRobotSpeed > _maxJointSpeed) ? _maxJointSpeed : realRobotSpeed;

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::JointAbsoluteMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::JointAbsoluteMove()::Error::" + returnValue.ToString());
                    }

                    #endregion
                }

                #region Not in use
                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    _roboDKRobot.SetSpeed(-1, Math.Abs(simulatorSpeed));

                //    ReportedRobotJointPosition.RobotPositions.CopyTo(jointsPositionsSimulator, 0);

                //    jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.RobotPositions[jointIndex];

                //    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                //    #endregion
                //} 
                #endregion
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointMask"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointsAbsoluteMove(int jointMask, double speed)
        {
            short returnValue = -1;
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                #region

                lock (_robotAccessLock)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                    realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                    ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositionsReal, 0);
                    for (int i = 0; i < jointsPositionsReal.Length; i++)
                    {
                        #region
                        if ((jointMask & (1 << i)) > 0)
                        {
                            jointsPositionsReal[i] = DesiredRobotJointPosition.RobotPulsePositions[i];
                        }
                        #endregion
                    }

                    _actualRobotJointSpeed = realRobotSpeed = 10;//TODO:for safety !!!!!!!!!!!!!!!!!!!! Math.Abs(realRobotSpeed);

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::JointAbsoluteMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::JointAbsoluteMove()::Error::" + returnValue.ToString());
                    }

                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                //    for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                //    {
                //        if ((jointMask & (1 << i)) > 0)
                //        {
                //            jointsPositionsSimulator[i] = DesiredRobotJointPosition.RobotPositions[i];
                //        }
                //    }

                //    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                //    #endregion
                //}

                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointHomeMove(int jointIndex, double speed)
        {
            short returnValue = -1;
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region
                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                    realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                    ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositionsReal, 0);
                    jointsPositionsReal[jointIndex] = DesiredRobotJointPosition.RobotHomePositions[jointIndex];

                    _actualRobotJointSpeed = realRobotSpeed = 10;//TODO:for safety !!!!!!!!!!!!!!!!!!!! Math.Abs(realRobotSpeed);

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::JointHomeMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::JointHomeMove()::Error::" + returnValue.ToString());
                    }
                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                //    jointsPositionsSimulator = ReportedRobotJointPosition.RobotPositions;

                //    jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.RobotHomePositions[jointIndex];

                //    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointParkMove(int jointIndex, double speed)
        {
            short returnValue = -1;
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                    realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                    ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositionsReal, 0);
                    jointsPositionsReal[jointIndex] = DesiredRobotJointPosition.RobotParkPositions[jointIndex];

                    _actualRobotJointSpeed = realRobotSpeed = 10;//TODO:for safety !!!!!!!!!!!!!!!!!!!! Math.Abs(realRobotSpeed);

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::JointParkMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::JointParkMove()::Error::" + returnValue.ToString());
                    }

                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                //    jointsPositionsSimulator = ReportedRobotJointPosition.RobotPositions;

                //    jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.RobotParkPositions[jointIndex];

                //    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointRelativeMove(int jointIndex, double speed)
        {
            short returnValue = -1;
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                    realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                    ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositionsReal, 0);
                    jointsPositionsReal[jointIndex] += DesiredRobotJointPosition.RobotPulsePositions[jointIndex];

                    _actualRobotJointSpeed = realRobotSpeed = 10;//TODO:for safety !!!!!!!!!!!!!!!!!!!! Math.Abs(realRobotSpeed);

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::JointRelativeMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::JointRelativeMove()::Error::" + returnValue.ToString());
                    }

                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    _roboDKRobot.SetSpeed(-1, Math.Abs(simulatorSpeed));

                //    ReportedRobotJointPosition.RobotPositions.CopyTo(jointsPositionsSimulator, 0);
                //    jointsPositionsSimulator[jointIndex] += DesiredRobotJointPosition.RobotPositions[jointIndex];

                //    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointMask"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short JointsRelativeMove(int jointMask, double speed)
        {
            short returnValue = -1;
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                    realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                    for (int i = 0; i < jointsPositionsReal.Length; i++)
                    {
                        #region
                        if ((jointMask & (1 << i)) > 0)
                        {
                            jointsPositionsReal[i] += DesiredRobotJointPosition.RobotPulsePositions[i];
                        }
                        #endregion
                    }

                    _actualRobotJointSpeed = realRobotSpeed = 10;//TODO:for safety !!!!!!!!!!!!!!!!!!!! Math.Abs(realRobotSpeed);

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::JointsRelativeMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::JointsRelativeMove()::Error::" + returnValue.ToString());
                    }

                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    _roboDKRobot.SetSpeed(-1, Math.Abs(simulatorSpeed));

                //    ReportedRobotJointPosition.RobotPositions.CopyTo(jointsPositionsSimulator, 0);
                //for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                //{
                //    #region
                //    if ((jointMask & (1 << i)) > 0)
                //    {
                //        jointsPositionsSimulator[i] += DesiredRobotJointPosition.RobotPulsePositions[i];
                //    }
                //    #endregion
                //}

                //    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return (short)returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TCPJogMove(int tcpIndex, double speed)
        {
            short returnValue = -1;
            double[] tcpAxesPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] tcpAxesPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] shortTCPAxesPositionsSimulator = new double[6] { 0, 0, 0, 0, 0, 0 };

            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                    realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsReal, 0);
                    tcpAxesPositionsReal[tcpIndex] = (speed > 0) ? DesiredRobotTCPPosition.Limits[tcpIndex][1] : DesiredRobotTCPPosition.Limits[tcpIndex][0];

                    realRobotSpeed = 100;//TODO:for safety !!!!!!!!!!!!!!!!!!!! Math.Abs(realRobotSpeed);

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscMovl(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[_actualMoveSpeedSelection]),
                            realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscMovl(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[_actualMoveSpeedSelection]),
                                        realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode,
                                        _toolNumber, ref tcpAxesPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::TCPJogMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::TCPJogMove()::Error::" + returnValue.ToString());
                    }

                    #endregion
                }

                #region Not in use
                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsSimulator, 0);
                //    for (int i = 0; i < shortTCPAxesPositionsSimulator.Length; i++)
                //    {
                //        shortTCPAxesPositionsSimulator[i] = tcpAxesPositionsSimulator[i];
                //    }

                //    shortTCPAxesPositionsSimulator[tcpIndex] = (simulatorSpeed > 0) ? DesiredRobotTCPPosition.Limits[tcpIndex][1] : DesiredRobotTCPPosition.Limits[tcpIndex][0];

                //    Mat movement_pose = Mat.FromTxyzRxyz(shortTCPAxesPositionsSimulator);

                //    _roboDKRobot.MoveJ(movement_pose, MOVE_BLOCKING);
                //    #endregion
                //} 
                #endregion
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpMask"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TCPAxesJogMove(int tcpMask, double speed)
        {
            RobotFunctionReturnType_2 returnValue = RobotFunctionReturnType_2.Other;
            double[] tcpAxesPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] tcpAxesPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] shortTCPAxesPositionsSimulator = new double[6] { 0, 0, 0, 0, 0, 0 };

            try
            {
                ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsReal, 0);

                for (int i = 0; i < tcpAxesPositionsReal.Length; i++)
                {
                    if ((tcpMask & (1 << i)) > 0)
                    {
                        tcpAxesPositionsReal[i] = (speed > 0) ? DesiredRobotTCPPosition.Limits[i][1] : DesiredRobotTCPPosition.Limits[i][0];
                    }
                }

                Motocom.BscMovj(_robotHandler, speed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);

                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsSimulator, 0);
                //    for (int i = 0; i < shortTCPAxesPositionsSimulator.Length; i++)
                //    {
                //        shortTCPAxesPositionsSimulator[i] = tcpAxesPositionsSimulator[i];
                //    }

                //    for (int i = 0; i < tcpAxesPositionsReal.Length; i++)
                //    {
                //        if ((tcpMask & (1 << i)) > 0)
                //        {
                //            shortTCPAxesPositionsSimulator[i] = (speed > 0) ? DesiredRobotTCPPosition.Limits[i][1] : DesiredRobotTCPPosition.Limits[i][0];
                //        }
                //    }

                //    Mat movement_pose = Mat.FromTxyzRxyz(shortTCPAxesPositionsSimulator);

                //    _roboDKRobot.MoveJ(movement_pose, MOVE_BLOCKING);
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
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
            short returnValue = -1;
            double[] tcpAxesPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] tcpAxesPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] shortTCPAxesPositionsSimulator = new double[6] { 0, 0, 0, 0, 0, 0 };

            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    Debug.WriteLine("DX200::TCPAbsoluteMove()::Warning::Running");

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsReal, 0);
                    tcpAxesPositionsReal[tcpIndex] = DesiredRobotTCPPosition.RobotPositions[tcpIndex];

                    _actualMoveSpeedSelection = (tcpIndex > -1 && tcpIndex < 3) ? RobotMoveSpeedSelectionType.ControlPoint : RobotMoveSpeedSelectionType.PositionAngular;

                    if (_actualMoveSpeedSelection == RobotMoveSpeedSelectionType.ControlPoint)
                    {
                        realRobotSpeed = Math.Abs(realRobotSpeed) < _maxControlPointSpeed ? Math.Abs(realRobotSpeed) : _maxControlPointSpeed;
                    }
                    if (_actualMoveSpeedSelection == RobotMoveSpeedSelectionType.PositionAngular)
                    {
                        realRobotSpeed = Math.Abs(realRobotSpeed) < _maxPositionAngularSpeed ? Math.Abs(realRobotSpeed) : _maxPositionAngularSpeed;
                    }

                    task = Task<short>.Factory.StartNew(() =>
                   {
                       //Return Value
                       //0 : Normal completion
                       //Others: Error codes
                       return Motocom.BscMovl(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[_actualMoveSpeedSelection]), realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                   });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscMovl(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[_actualMoveSpeedSelection]), realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                                    //return Motocom.BscMovj(_robotHandler, realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::TCPJogMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::TCPAbsoluteMove()::Error::" + returnValue.ToString());
                    }

                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsSimulator, 0);
                //    for (int i = 0; i < shortTCPAxesPositionsSimulator.Length; i++)
                //    {
                //        shortTCPAxesPositionsSimulator[i] = tcpAxesPositionsSimulator[i];
                //    }

                //    shortTCPAxesPositionsSimulator[tcpIndex] = DesiredRobotTCPPosition.RobotPositions[tcpIndex];

                //    Mat movement_pose = Mat.FromTxyzRxyz(shortTCPAxesPositionsSimulator);

                //    _roboDKRobot.MoveJ(movement_pose, MOVE_BLOCKING);
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpMask"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TCPAxesAbsoluteMove(int tcpMask, double speed)
        {
            short returnValue = -1;
            double[] tcpAxesPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] tcpAxesPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] shortTCPAxesPositionsSimulator = new double[6] { 0, 0, 0, 0, 0, 0 };

            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region
                    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsReal, 0);

                    for (int i = 0; i < tcpAxesPositionsReal.Length; i++)
                    {
                        if ((tcpMask & (1 << i)) > 0)
                        {
                            tcpAxesPositionsReal[i] = DesiredRobotTCPPosition.RobotPositions[i];
                        }
                    }

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    _actualMoveSpeedSelection = RobotMoveSpeedSelectionType.ControlPoint;

                    if (_actualMoveSpeedSelection == RobotMoveSpeedSelectionType.ControlPoint)
                    {
                        realRobotSpeed = Math.Abs(realRobotSpeed) < _maxControlPointSpeed ? Math.Abs(realRobotSpeed) : _maxControlPointSpeed;
                    }
                    if (_actualMoveSpeedSelection == RobotMoveSpeedSelectionType.PositionAngular)
                    {
                        realRobotSpeed = Math.Abs(realRobotSpeed) < _maxPositionAngularSpeed ? Math.Abs(realRobotSpeed) : _maxPositionAngularSpeed;
                    }

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscMovl(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[_actualMoveSpeedSelection]), realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscMovl(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[_actualMoveSpeedSelection]), realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                                    //return Motocom.BscMovj(_robotHandler, realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::TCPAxesAbsoluteMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::TCPAxesAbsoluteMove()::Error::" + returnValue.ToString());
                    }
                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsSimulator, 0);
                //    for (int i = 0; i < shortTCPAxesPositionsSimulator.Length; i++)
                //    {
                //        shortTCPAxesPositionsSimulator[i] = tcpAxesPositionsSimulator[i];
                //    }

                //    for (int i = 0; i < tcpAxesPositionsReal.Length; i++)
                //    {
                //        if ((tcpMask & (1 << i)) > 0)
                //        {
                //            shortTCPAxesPositionsSimulator[i] = DesiredRobotTCPPosition.RobotPositions[i];
                //        }
                //    }

                //    Mat movement_pose = Mat.FromTxyzRxyz(shortTCPAxesPositionsSimulator);

                //    _roboDKRobot.MoveJ(movement_pose, MOVE_BLOCKING);
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TCPRelativeMove(int tcpIndex, double speed)
        {
            short returnValue = -1;
            double[] tcpAxesPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] tcpAxesPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] shortTCPAxesPositionsSimulator = new double[6] { 0, 0, 0, 0, 0, 0 };

            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                    realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsReal, 0);
                    tcpAxesPositionsReal[tcpIndex] += DesiredRobotTCPPosition.RobotPositions[tcpIndex];

                    realRobotSpeed = 100;//TODO:for safety !!!!!!!!!!!!!!!!!!!! Math.Abs(realRobotSpeed);

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscMovl(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[_actualMoveSpeedSelection]), realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscMovl(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[_actualMoveSpeedSelection]), realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                                    //return Motocom.BscMovj(_robotHandler, realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::TCPJogMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::TCPRelativeMove()::Error::" + returnValue.ToString());
                    }

                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsSimulator, 0);
                //    for (int i = 0; i < shortTCPAxesPositionsSimulator.Length; i++)
                //    {
                //        shortTCPAxesPositionsSimulator[i] = tcpAxesPositionsSimulator[i];
                //    }

                //    shortTCPAxesPositionsSimulator[tcpIndex] += DesiredRobotTCPPosition.RobotPositions[tcpIndex];

                //    Mat movement_pose = Mat.FromTxyzRxyz(shortTCPAxesPositionsSimulator);

                //    _roboDKRobot.MoveJ(movement_pose, MOVE_BLOCKING);
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpMask"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TCPAxesRelativeMove(int tcpMask, double speed)
        {
            short returnValue = -1;
            double[] tcpAxesPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] tcpAxesPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] shortTCPAxesPositionsSimulator = new double[6] { 0, 0, 0, 0, 0, 0 };

            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                    realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsReal, 0);

                    for (int i = 0; i < tcpAxesPositionsReal.Length; i++)
                    {
                        if ((tcpMask & (1 << i)) > 0)
                        {
                            tcpAxesPositionsReal[i] += DesiredRobotTCPPosition.RobotPositions[i];
                        }
                    }

                    realRobotSpeed = 100;//TODO:for safety !!!!!!!!!!!!!!!!!!!! Math.Abs(realRobotSpeed);

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscMovl(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[_actualMoveSpeedSelection]), realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscMovl(_robotHandler, new StringBuilder(_moveSpeedSelectionDictionary[_actualMoveSpeedSelection]), realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                                    //return Motocom.BscMovj(_robotHandler, realRobotSpeed, new StringBuilder(_frameDictionary[_actualReferenceFrame]), _actualRConf.Formcode, _toolNumber, ref tcpAxesPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::TCPJogMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::TCPAxesRelativeMove()::Error::" + returnValue.ToString());
                    }

                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region

                //    ReportedRobotTCPPosition.RobotPositions.CopyTo(tcpAxesPositionsSimulator, 0);

                //    for (int i = 0; i < shortTCPAxesPositionsSimulator.Length; i++)
                //    {
                //        shortTCPAxesPositionsSimulator[i] = tcpAxesPositionsSimulator[i];
                //    }

                //    for (int i = 0; i < shortTCPAxesPositionsSimulator.Length; i++)
                //    {
                //        if ((tcpMask & (1 << i)) > 0)
                //        {
                //            shortTCPAxesPositionsSimulator[i] += DesiredRobotTCPPosition.RobotPositions[i];
                //        }
                //    }

                //    Mat movement_pose = Mat.FromTxyzRxyz(shortTCPAxesPositionsSimulator);

                //    _roboDKRobot.MoveJ(movement_pose, MOVE_BLOCKING);
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
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
            short returnValue = -1;
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] _currentRobotJointsPositionReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    if (speed != 0)
                    {
                        #region
                        if (_actualRobotStatus.IsCommandHold == true)
                        {
                            HoldOff();
                        }

                        simulatorSpeed = speed;

                        speed = (Math.Abs(speed) > _trackMaxSpeed) ? (speed / Math.Abs(speed)) * _trackMaxSpeed : speed;
                        realRobotSpeed = Math.Abs(speed / (_trackMaxSpeed / 100.0));

                        ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositionsReal, 0);

                        jointsPositionsReal[jointIndex] = (speed > 0) ? DesiredRobotJointPosition.LimitsPulse[jointIndex][1] : DesiredRobotJointPosition.LimitsPulse[jointIndex][0];

                        realRobotSpeed = Math.Abs(realRobotSpeed);

                        task = Task<short>.Factory.StartNew(() =>
                        {
                            //Return Value
                            //0 : Normal completion
                            //Others: Error codes
                            return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                        });

                        taskWaitTimeCounter = 0;

                        while (!task.IsCompleted)
                        {
                            #region
                            taskWaitTimeCounter++;
                            task.Wait(_motocom32FunctionsWaitTime);
                            if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                            {
                                break;
                            }
                            #endregion
                        }
                        if (task.IsCompleted)
                        {
                            #region
                            returnValue = Convert.ToInt16(task.Result);
                            //TODO:check return and send more if needed
                            if (returnValue != 0)
                            {
                                #region
                                counterSendCommand = 0;
                                while (counterSendCommand < maxSendCommands)
                                {
                                    #region

                                    counterSendCommand++;

                                    task = Task<short>.Factory.StartNew(() =>
                                    {
                                        return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                                    });

                                    taskWaitTimeCounter = 0;

                                    while (!task.IsCompleted)
                                    {
                                        #region
                                        taskWaitTimeCounter++;
                                        task.Wait(_motocom32FunctionsWaitTime);
                                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                        {
                                            break;
                                        }
                                        #endregion
                                    }
                                    if (task.IsCompleted)
                                    {
                                        returnValue = Convert.ToInt16(task.Result);
                                        if (returnValue == 0)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        returnValue = -1;
                                    }
                                    #endregion
                                }
                                if (counterSendCommand == maxSendCommands)
                                {
                                    Debug.WriteLine("DX200::TrackLinearJogMove()::Error::Real robot connection problem.");
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            returnValue = -1;
                        }

                        if (returnValue != 0)
                        {
                            Debug.WriteLine("DX200::TrackLinearJogMove()::Error::" + returnValue.ToString());
                        }

                        #endregion
                    }
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region

                //    if (jointsPositionsSimulator != null)
                //    {
                //        #region
                //        //get actual robot position

                //        ReportedRobotJointPosition.RobotPositions.CopyTo(jointsPositionsSimulator, 0);

                //        _roboDKRobot.SetSpeed(-1, Math.Abs(simulatorSpeed));

                //        jointsPositionsSimulator[jointIndex] = (simulatorSpeed > 0) ? 3500 : 0;// (simulatorSpeed > 0) ? DesiredRobotJointPosition.Limits[jointIndex][1] : DesiredRobotJointPosition.Limits[jointIndex][0];

                //        _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                //        #endregion
                //    }

                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TrackLinearAbsoluteMove(int jointIndex, double speed)
        {
            short returnValue = -1;
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                    realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                    ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositionsReal, 0);
                    jointsPositionsReal[jointIndex] = DesiredRobotJointPosition.RobotPulsePositions[jointIndex];

                    realRobotSpeed = 10;//TODO:for safety !!!!!!!!!!!!!!!!!!!! Math.Abs(realRobotSpeed);

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::JointAbsoluteMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::JointAbsoluteMove()::Error while moving");
                    }

                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    _roboDKRobot.SetSpeed(-1, Math.Abs(simulatorSpeed));

                //    ReportedRobotJointPosition.RobotPositions.CopyTo(jointsPositionsSimulator, 0);

                //    jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.RobotPositions[jointIndex];

                //    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING); 
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jointIndex"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public short TrackLinearRelativeMove(int jointIndex, double speed)
        {
            short returnValue = -1;
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] jointsPositionsReal = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double realRobotSpeed = 0.0;
            double simulatorSpeed = 0.0;

            Task<short> task = null;
            int taskWaitTimeCounter = 0;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    if (_actualRobotStatus.IsCommandHold == true)
                    {
                        HoldOff();
                    }

                    realRobotSpeed = simulatorSpeed = speed;

                    realRobotSpeed = (realRobotSpeed > 100) ? 100 : realRobotSpeed;
                    realRobotSpeed = (realRobotSpeed < -100) ? -100 : realRobotSpeed;

                    ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositionsReal, 0);
                    jointsPositionsReal[jointIndex] += DesiredRobotJointPosition.RobotPulsePositions[jointIndex];

                    realRobotSpeed = Math.Abs(realRobotSpeed);

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscPMovj(_robotHandler, realRobotSpeed, _toolNumber, ref jointsPositionsReal[0]);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::JointAbsoluteMove()::Error::Real robot connection problem.");
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    if (returnValue != 0)
                    {
                        Debug.WriteLine("DX200::JointAbsoluteMove()::Error while moving");
                    }

                    #endregion
                }

                //if (_useRoboDKSimulator)
                //{
                //    #region
                //    _roboDKRobot.SetSpeed(-1, Math.Abs(simulatorSpeed));

                //    ReportedRobotJointPosition.RobotPositions.CopyTo(jointsPositionsSimulator, 0);

                //    jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.RobotPositions[jointIndex];

                //    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING); 
                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
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
            //double[] jointsPositionsSimulator = null;
            double[] jointsPositions = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            try
            {
                ReportedRobotJointPosition.RobotPulsePositions.CopyTo(jointsPositions, 0);

                if (speed > 0)
                {
                    jointsPositions[jointIndex] = DesiredRobotJointPosition.LimitsPulse[jointIndex][1];
                }
                if (speed < 0)
                {
                    jointsPositions[jointIndex] = DesiredRobotJointPosition.LimitsPulse[jointIndex][0];
                }

                Motocom.BscPMovj(_robotHandler, speed, _toolNumber, ref jointsPositions[0]);

                //if (_useRoboDKSimulator)
                //{
                //    #region

                //    jointsPositionsSimulator = _roboDKRobot.Joints();

                //    if (jointsPositionsSimulator != null)
                //    {
                //        #region
                //        //get actual robot position
                //        for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                //        {
                //            jointsPositionsSimulator[i] = ReportedRobotJointPositionSimulator.RobotPositions[i];
                //        }

                //        _roboDKRobot.SetSpeed(-1, Math.Abs(speed));

                //        if (speed > 0)
                //        {
                //            jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.Limits[jointIndex][1];
                //        }
                //        if (speed < 0)
                //        {
                //            jointsPositionsSimulator[jointIndex] = DesiredRobotJointPosition.Limits[jointIndex][0];
                //        }

                //        _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                //        #endregion
                //    }

                //    #endregion
                //}
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
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
        /// <param name="positions"></param>
        /// <returns></returns>
        public short GetRobotPosition(StringBuilder frameName, short isEx, ref short rconf, ref short toolNumber, ref double positions)
        {
            short returnValue = -1;

            lock (_robotAccessLock)
            {
                returnValue = Motocom.BscIsRobotPos(_robotHandler, frameName, isEx, ref rconf, ref toolNumber, ref positions);
            }

            return returnValue;
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

            lock (_robotAccessLock)
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
            lock (_robotAccessLock)
            {
                short ret = Motocom.BscIsLoc(_robotHandler, isPulseOrXYZ, ref rconf, ref position);

                if (ret != 0)
                {
                    throw new Exception("Error reading position!");
                }

                return ret;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="frame"></param>
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
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// Sets Teach mode
        /// </summary>
        /// <summary>
        /// Sets Servo ON
        /// </summary>
        public short ServoOn()
        {
            short returnValue = 0;
            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    _actualRobotStatus.IsCommandHold = true;

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        //Takes 800 msec
                        return Motocom.BscServoOn(_robotHandler);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 20) //wait 1000 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }

                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscServoOn(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::SetServoOn()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// Sets Servo OFF
        /// </summary>
        public short ServoOff()
        {
            short returnValue = 0;
            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    _actualRobotStatus.IsCommandHold = true;

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        //Takes 400 msec
                        return Motocom.BscServoOff(_robotHandler);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }

                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    return Motocom.BscServoOff(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::SetServoOff()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short IsServo()
        {
            short returnValue = -1;

            Task<short> task = new Task<short>(() =>
            {
                //-1 : Acquisition Failure
                //0 : Servo OFF
                //1 : Servo ON
                return Motocom.BscIsServo(_robotHandler);
            });

            task.Start();
            task.Wait(_motocom32FunctionsWaitTime);
            if (task.IsCompleted)
            {
                returnValue = Convert.ToInt16(task.Result);
            }
            else
            {
                returnValue = -1;
            }

            return returnValue;
        }
        /// <summary>
        /// Sets Hold ON
        /// </summary>
        public short HoldOn()
        {
            short returnValue = 0;

            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    _actualRobotStatus.IsCommandHold = true;

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscHoldOn(_robotHandler);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                        {
                            break;
                        }
                        #endregion
                    }

                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscHoldOn(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 500 msec maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::HoldOn()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }

                    #endregion
                }

                if (_useRoboDKSimulator)
                {
                    #region

                    ReportedRobotJointPosition.RobotPositions.CopyTo(jointsPositionsSimulator, 0);

                    jointsPositionsSimulator[6] = (jointsPositionsSimulator[6] > 3500) ? 3500 : jointsPositionsSimulator[6];

                    if (_roboDKRobot != null)
                    {
                        _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// Sets Hold OFF
        /// </summary>
        public short HoldOff()
        {
            short returnValue = 0;
            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region
                    _actualRobotStatus.IsCommandHold = false;

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscHoldOff(_robotHandler);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 1 second maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscHoldOff(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 1 second maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::HoldOff()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short CaptureTrajectoryPoint()
        {
            short returnValue = 0;

            try
            {
                _actualJointsTrajectory.Add(ReportedRobotJointPosition);
                _actualTCPTrajectory.Add(ReportedRobotTCPPosition);
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short SetAutoMode()
        {
            short returnValue = 0;
            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscSelLoopCycle(_robotHandler);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 1 second maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscSelLoopCycle(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 1 second maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::SetAutoMode()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short SetOneCycleMode()
        {
            short returnValue = 0;
            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscSelOneCycle(_robotHandler);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 1 second maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscSelOneCycle(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 1 second maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::SetOneCycleMode()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public short SetStepMode()
        {
            short returnValue = 0;
            int taskWaitTimeCounter = 0;
            Task<short> task = null;
            int counterSendCommand = 0;
            int maxSendCommands = 3;

            try
            {
                lock (_robotAccessLock)
                {
                    #region

                    task = Task<short>.Factory.StartNew(() =>
                    {
                        //Return Value
                        //0 : Normal completion
                        //Others: Error codes
                        return Motocom.BscSelStepCycle(_robotHandler);
                    });

                    taskWaitTimeCounter = 0;

                    while (!task.IsCompleted)
                    {
                        #region
                        taskWaitTimeCounter++;
                        task.Wait(_motocom32FunctionsWaitTime);
                        if (taskWaitTimeCounter > 10) //wait 1 second maximum
                        {
                            break;
                        }
                        #endregion
                    }
                    if (task.IsCompleted)
                    {
                        #region
                        returnValue = Convert.ToInt16(task.Result);
                        //TODO:check return and send more if needed
                        if (returnValue != 0)
                        {
                            #region
                            counterSendCommand = 0;
                            while (counterSendCommand < maxSendCommands)
                            {
                                #region

                                counterSendCommand++;

                                task = Task<short>.Factory.StartNew(() =>
                                {
                                    //Return Value
                                    //0 : Normal completion
                                    //Others: Error codes
                                    return Motocom.BscSelStepCycle(_robotHandler);
                                });

                                taskWaitTimeCounter = 0;

                                while (!task.IsCompleted)
                                {
                                    #region
                                    taskWaitTimeCounter++;
                                    task.Wait(_motocom32FunctionsWaitTime);
                                    if (taskWaitTimeCounter > 10) //wait 1 second maximum
                                    {
                                        break;
                                    }
                                    #endregion
                                }
                                if (task.IsCompleted)
                                {
                                    returnValue = Convert.ToInt16(task.Result);
                                    if (returnValue == 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    returnValue = -1;
                                }
                                #endregion
                            }
                            if (counterSendCommand == maxSendCommands)
                            {
                                Debug.WriteLine("DX200::SetStepMode()::Error::" + returnValue.ToString());
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        returnValue = -1;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }

        #endregion

        #region Threads
        /// <summary>
        /// 
        /// </summary>
        private void GetStatusThread()
        {
            try
            {
                while (_getStatusThreadRun)
                {
                    lock (_robotAccessLock)
                    {
                        #region
                        try
                        {
                            //TODO:Check connection ping and any relevant parameter
                            //CheckConnection();
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
                        catch (Exception ex)
                        {
                            DiagnosticException.ExceptionHandler(ex);
                        }
                        #endregion
                    }

                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void CheckConnection()
        {
            try
            {
                _connectionCheckCounter++;
                if (_communicationStatus.connected == false && _connectionCheckCounter > 19)
                {
                    Debug.WriteLine("DX200::Robot not connected");
                    _connectionCheckCounter = 0;
                    if (DX200Communication.CheckPing(_ipAddress) == ConnectionStatus.Reachable)
                    {
                        Connect();
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void GetPositionsThread()
        {
            try
            {
                while (_getPositionsThreadRun)
                {
                    lock (_robotAccessLock)
                    {
                        #region
                        try
                        {
                            GetCurrentRobotTCPPosition();
                            GetCurrentRobotJointsPosition();
                        }
                        catch (Exception ex)
                        {
                            DiagnosticException.ExceptionHandler(ex);
                        }
                        #endregion
                    }

                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void GetAlarmsThread()
        {
            try
            {
                while (_getAlarmsThreadRun)
                {
                    lock (_robotAccessLock)
                    {
                        #region
                        try
                        {
                            _alarmsCounter = GetAlarms(out _robotError, out _alarmList);
                        }
                        catch (Exception ex)
                        {
                            DiagnosticException.ExceptionHandler(ex);
                        }
                        #endregion
                    }

                    Thread.Sleep(300);
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void GetJobFileStatusThread()
        {
            short returnValue = -1;
            short currentLineNumber = -1;
            StringBuilder _currentJobName = new StringBuilder(Motocom.MaxJobNameLength + 1);

            try
            {
                while (_getJobFileStatusRun)
                {
                    lock (_robotAccessLock)
                    {
                        #region
                        try
                        {
                            _currentJobName = new StringBuilder(Motocom.MaxJobNameLength + 1);
                            returnValue = Motocom.BscIsJobName(_robotHandler, _currentJobName, Motocom.MaxJobNameLength);

                            if (CurrentJobFileName != _currentJobName.ToString())
                            {
                                Debug.WriteLine("Warning::Current job file:" + _currentJobName.ToString());
                            }

                            CurrentJobFileName = (returnValue == 0) ? _currentJobName.ToString() : string.Empty;

                            currentLineNumber = Motocom.BscIsJobLine(_robotHandler);

                            if (CurrentLineNumber != currentLineNumber)
                            {
                                Debug.WriteLine("Warning::Current line:" + currentLineNumber);
                            }

                            CurrentLineNumber = currentLineNumber;
                        }
                        catch (Exception ex)
                        {
                            DiagnosticException.ExceptionHandler(ex);
                        }
                        #endregion
                    }

                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void SimulatorSyncThread()
        {
            bool equal = false;
            double[] jointsPositionsSimulator = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double simulatorSpeed = 0.0;

            try
            {
                simulatorSpeed = 300;
                _roboDKRobot.SetSpeed(-1, Math.Abs(simulatorSpeed));
                Thread.Sleep(100);

                while (_simulatorSyncThreadRun)
                {
                    lock (_simulatorAccessLock)
                    {
                        #region
                        try
                        {
                            if (_useRoboDKSimulator)
                            {
                                #region

                                equal = true;

                                for (int i = 0; i < jointsPositionsSimulator.Length; i++)
                                {
                                    if (jointsPositionsSimulator[i] != ReportedRobotJointPosition.RobotPositions[i])
                                    {
                                        if (i == 6) continue;
                                        equal = false;
                                        break;
                                    }
                                }

                                if (!equal)
                                {
                                    ReportedRobotJointPosition.RobotPositions.CopyTo(jointsPositionsSimulator, 0);

                                    jointsPositionsSimulator[6] = (jointsPositionsSimulator[6] > 3500) ? 3500 : jointsPositionsSimulator[6];

                                    _roboDKRobot.MoveJ(jointsPositionsSimulator, MOVE_BLOCKING);
                                }
                                #endregion
                            }
                        }
                        catch (Exception ex)
                        {
                            DiagnosticException.ExceptionHandler(ex);
                        }
                        #endregion
                    }

                    Thread.Sleep(300);
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
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

        #endregion

        #region ------------------------- RoboDK simulator -----------------------------

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
                            DiagnosticException.ExceptionHandler(new Exception("Problems using the RoboDK API. The RoboDK API is not available..."));
                        }
                    }
                }
                else
                {
                    returnedValue = false;
                    DiagnosticException.ExceptionHandler(new Exception("RoboDK has not been started"));
                }
            }
            catch (Exception ex)
            {
                returnedValue = false;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnedValue;
        }
        /// <summary>
        /// Update the ROBOT variable by choosing the robot available in the currently open station
        /// If more than one robot is available, a popup will be displayed
        /// </summary>
        public void SelectRoboDKRobot()
        {
            int _waitTimeCounter = 0;
            try
            {
                // select robot among available robots
                //ROBOT = RL.getItem("ABB IRB120", ITEM_TYPE_ROBOT); // select by name
                //ITEM = RL.ItemUserPick("Select an item"); // Select any item in the station
                _roboDKRobot = RDK.GetItemByName("Motoman MH24", ItemType.Robot);
                while (_roboDKRobot == null)
                {
                    _waitTimeCounter++;
                    if (_waitTimeCounter > 100)
                    {
                        break;
                    }

                    Thread.Sleep(100);
                }
                _waitTimeCounter = 0;
                _roboDKLinearTrack = RDK.GetItemByName("ABB IRBT 6004 Standard 3.5m Base", ItemType.Frame);
                while (_roboDKLinearTrack == null)
                {
                    _waitTimeCounter++;
                    if (_waitTimeCounter > 100)
                    {
                        break;
                    }

                    Thread.Sleep(100);
                }
                _roboDKTurnTable = RDK.GetItemByName("ABB IRBP L2000 L4000 Base", ItemType.Frame);
                _waitTimeCounter = 0;
                while (_roboDKLinearTrack == null)
                {
                    _waitTimeCounter++;
                    if (_waitTimeCounter > 100)
                    {
                        break;
                    }

                    Thread.Sleep(100);
                }
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
                    DiagnosticException.ExceptionHandler(new Exception("Robot not available. Load a file first"));
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        /// <summary>
        ///   void ShowRoboDKForm()
        /// </summary>
        public void ShowRoboDKForm()
        {
            RDK.SetWindowState(WindowState.Normal);
            //RDK.SetWindowState(WindowState.Maximized); // shows maximized
            //RDK.SetWindowState(WindowState.Cinema); // shows maximized
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
                DiagnosticException.ExceptionHandler(ex);
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
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        /// <summary>
        /// Check if the ROBOT object is available and valid. It will make sure that we can operate with the ROBOT object.
        /// </summary>
        /// <returns></returns>
        public bool Check_RobotDKRobot(bool ignore_busy_status = false)
        {
            bool returnValue = false;

            if (Check_RDK())
            {
                if (_roboDKRobot != null || _roboDKRobot.Valid())
                {
                    // Safe check: If we are doing non blocking movements, we can check if the robot is doing other movements with the Busy command
                    returnValue = (!MOVE_BLOCKING && (!ignore_busy_status && _roboDKRobot.Busy())) ? false : true;
                }
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="run"></param>
        public void RoboDKSimulatorRun(bool run)
        {
            int _waitTimeCounter = 0;
            string stationName = string.Empty;

            try
            {
                if (run == true)
                {
                    #region
                    RDK = new RoboDK();

                    // Check if RoboDK started properly
                    while (!Check_RDK())
                    {
                        #region
                        _waitTimeCounter++;
                        if (_waitTimeCounter > 100)
                        {
                            break;
                        }
                        Thread.Sleep(100);
                        #endregion
                    }

                    RDK.SetRunMode(RunMode.Simulate);

                    IItem activeStation = RDK.GetActiveStation();
                    _waitTimeCounter = 0;
                    while (activeStation == null)
                    {
                        #region
                        _waitTimeCounter++;
                        if (_waitTimeCounter > 100)
                        {
                            break;
                        }
                        Thread.Sleep(100);
                        #endregion
                    }

                    stationName = activeStation.Name();

                    if (stationName.IndexOf("ChesterfieldProject") < 0)
                    {
                        #region
                        RDK.AddFile(@"F:\Yaskawa\ChesterfieldProject.rdk");
                        activeStation = RDK.GetActiveStation();
                        _waitTimeCounter = 0;
                        while (activeStation == null)
                        {
                            _waitTimeCounter++;
                            if (_waitTimeCounter > 100)
                            {
                                break;
                            }
                            Thread.Sleep(100);
                        }
                        #endregion
                    }
                    // attempt to auto select the robot:
                    SelectRoboDKRobot();

                    ShowRoboDKForm();

                    #endregion

                    _simulatorSyncThreadRun = true;
                    _simulatorSyncThread.Start();
                }
                else
                {
                    _simulatorSyncThreadRun = false;
                    RDK.CloseRoboDK();
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        #endregion

        #endregion
    }
}
//DiagnosticException.globalStopWatch.Reset();
//DiagnosticException.globalStopWatch.Start();

//short ret = Motocom.BscServoOff(_robotHandler);

//DiagnosticException.globalStopWatch.Stop();
//long time = DiagnosticException.globalStopWatch.ElapsedMilliseconds;
