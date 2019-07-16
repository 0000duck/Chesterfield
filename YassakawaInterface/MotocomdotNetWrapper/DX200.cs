using System;
using System.Threading;
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

namespace MotoCom32Net
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
        bool IsTeach
        {
            get;
        }
        bool IsPlay
        {
            get;
        }
        string CommDir
        {
            get;
            set;
        }
        FrameType ActualReferenceFrame
        {
            get;
            set;
        }
        Configuration ActualRConf
        {
            get;
            set;
        }
        double ActualSpeed
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
        void SetTeachMode();
        void SetPlayMode();
        void SetServoOn();
        void SetServoOff();
        #endregion
    }
    /// <summary>
    /// DX200 robot controller implementation
    /// </summary>
    [ComVisible(true)]
    public class DX200 : IDX200, INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler StatusChanged;
        #endregion

        #region Fields

        private short _handle = -1;
        private string _commDir;
        private string _path;
        private string _ipAddress = "";
        private FrameType _actualReferenceFrame = FrameType.Base;
        private string _actualReferenceFrameString = "BASE";
        private string[] _coordinateNames =
        {
            #region 
		    "BASE",
            "ROBOT" ,
            "UF1",
            "UF2",
            "UF3",
            "UF4",
            "UF5",
            "UF6",
            "UF7",
            "UF8",
            "UF9",
            "UF10",
            "UF11",
            "UF12",
            "UF13",
            "UF14",
            "UF15",
            "UF16",
            "UF17",
            "UF18",
            "UF19",
            "UF20",
            "UF21",
            "UF22",
            "UF23",
            "UF24",
            "UF25",
            "UF26",
            "UF27",
            "UF28",
            "UF29",
            "UF30",
            "UF31",
            "UF32",
            "UF33",
            "UF34",
            "UF35",
            "UF36",
            "UF37",
            "UF38",
            "UF39",
            "UF40",
            "UF41",
            "UF42",
            "UF43",
            "UF44",
            "UF45",
            "UF46",
            "UF47",
            "UF48",
            "UF49",
            "UF50",
            "UF51",
            "UF52",
            "UF53",
            "UF54",
            "UF55",
            "UF56",
            "UF57",
            "UF58",
            "UF59",
            "UF60",
            "UF61",
            "UF62",
            "UF63",
            "UF64",
            "TOOL",
            "MASTERTOOL" 
	#endregion
        };
        private Configuration _actualRConf = new Configuration(0);
        System.Threading.Timer _statusTimer = null;
        readonly object _lockStatusTimer = new object();
        private short _oldStatusD1 = -1;
        private short _oldStatusD2 = -1;
        private static Object _FileAccessDirLock = new Object();
        private Object _DX200AccessLock = new Object();
        private bool _isError;
        private bool _isCommandRemote;
        private bool _isStep;
        private bool _is1Cycle;
        private bool _isAuto;
        private bool _isOperating;
        private bool _isSafeSpeed;
        private bool _isTeach;
        private bool _isPlay;
        private bool _isPlaybackBoxHold;
        private bool _isPPHold;
        private bool _isExternalHold;
        private bool _isCommandHold;
        private bool _isAlarm;
        private bool _isServoOn;
        private bool _autoStatusUpdate = false;

        private double _actualSpeed = 0.0;
        private double _desiredSpeed = 0.0;
        private double _reportedSpeed = 0.0;

        private short _toolNumber = 0;

        private double[] _actualPosition = new double[12];
        private double[] _desiredPosition = new double[12];
        private double[] _reportedPosition = new double[12];

        private double _incrementStep = 1.0;//mm
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
        public FrameType ActualReferenceFrame
        {
            get
            {
                return _actualReferenceFrame;
            }
            set
            {
                _actualReferenceFrame = value;
                _actualReferenceFrameString = _coordinateNames[(int)value];
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

        public bool IsStep
        {
            get
            {
                return _isStep;
            }
        }
        public bool Is1Cycle
        {
            get
            {
                return _is1Cycle;
            }
        }
        public bool IsAuto
        {
            get
            {
                return _isAuto;
            }
        }
        public bool IsOperating
        {
            get
            {
                return _isOperating;
            }
        }
        public bool IsSafeSpeed
        {
            get
            {
                return _isSafeSpeed;
            }
        }
        public bool IsTeach
        {
            get
            {
                return _isTeach;
            }
        }
        public bool IsPlay
        {
            get
            {
                return _isPlay;
            }
        }
        public bool IsCommandRemote
        {
            get
            {
                return _isCommandRemote;
            }
        }
        public bool IsPlaybackBoxHold
        {
            get
            {
                return _isPlaybackBoxHold;
            }
        }
        public bool IsPPHold
        {
            get
            {
                return _isPPHold;
            }
        }
        public bool IsExternalHold
        {
            get
            {
                return _isExternalHold;
            }
        }
        public bool IsCommandHold
        {
            get
            {
                return _isCommandHold;
            }
        }
        public bool IsAlarm
        {
            get
            {
                return _isAlarm;
            }
        }
        public bool IsError
        {
            get
            {
                return _isError;
            }
        }
        public bool IsServoOn
        {
            get
            {
                return _isServoOn;
            }
        }
        public bool IsHold
        {
            get
            {
                return (_isPlaybackBoxHold || _isPPHold || _isExternalHold || _isCommandHold);
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
                    _statusTimer.Change(0, 500); //enable timer
                }
                else
                {
                    _statusTimer.Change(Timeout.Infinite, Timeout.Infinite);//timer disabled
                }
            }
        }
        public double ActualSpeed
        {
            get
            {
                return _actualSpeed;
            }
            set
            {
                _actualSpeed = value;
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
            _handle = Motocom.BscOpen(_commDir, 256);

            if (_handle >= 0)
            {
                //set IP Address
                ret = Motocom.BscSetEServer(_handle, _ipAddress);
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
            //close if there is a valid handle
            if (_handle >= 0)
                ret = Motocom.BscClose(_handle);
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
                if (_handle != -1 && _handle != -8)
                {
                    returnValue = Motocom.BscConnect(_handle);
                    if (returnValue > 0)
                    {
                        _statusTimer.Change(0, 50); //enable timer
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
                if (_handle != -1 && _handle != -8)
                {
                    returnValue = Motocom.BscDisConnect(_handle);
                    if (returnValue > 0)
                    {
                        _statusTimer.Change(Timeout.Infinite, Timeout.Infinite);//timer disabled
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
                _handle = Motocom.BscOpen(_commDir, (short)CommunicationType.ETHERNET_SERVER);

                if (_handle >= 0 && _ipAddress != null)
                {
                    //set IP Address
                    //Return Value
                    //0 : Error
                    //1 : Normal completion
                    returnValue = Motocom.BscSetEServer(_handle, _ipAddress);
                }
                else
                {
                    returnValue = 0;
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

                _handle = Motocom.BscOpen(_commDir, (short)CommunicationType.ETHERNET_SERVER);

                returnValue = (_handle != -1 && _handle != -8);
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
                if (_handle >= 0)
                {
                    returnValue = Motocom.BscSetEServer(_handle, _ipAddress);
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
        public void UpdateStatus(ref short d1, ref short d2)
        {
            lock (_DX200AccessLock)
            {
                #region
                short ret = Motocom.BscGetStatus(_handle, ref d1, ref d2);

                if (ret != 0)
                {
                    throw new Exception("Error getting status !");
                }
                else
                {
                    #region
                    //check bits and set properties
                    if (d1 != _oldStatusD1 || d2 != _oldStatusD2)
                    {
                        #region
                        _isStep = (d1 & (1 << 0)) > 0 ? true : false;
                        _is1Cycle = (d1 & (1 << 1)) > 0 ? true : false;
                        _isAuto = (d1 & (1 << 2)) > 0 ? true : false;
                        _isOperating = (d1 & (1 << 3)) > 0 ? true : false;
                        _isSafeSpeed = (d1 & (1 << 4)) > 0 ? true : false;
                        _isTeach = (d1 & (1 << 5)) > 0 ? true : false;
                        _isPlay = (d1 & (1 << 6)) > 0 ? true : false;
                        _isCommandRemote = (d1 & (1 << 7)) > 0 ? true : false;

                        _isPlaybackBoxHold = (d2 & (1 << 0)) > 0 ? true : false;
                        _isPPHold = (d2 & (1 << 1)) > 0 ? true : false;
                        _isExternalHold = (d2 & (1 << 2)) > 0 ? true : false;
                        _isCommandHold = (d2 & (1 << 3)) > 0 ? true : false;
                        _isAlarm = (d2 & (1 << 4)) > 0 ? true : false;
                        _isError = (d2 & (1 << 5)) > 0 ? true : false;
                        _isServoOn = (d2 & (1 << 6)) > 0 ? true : false;

                        _oldStatusD1 = d1;
                        _oldStatusD2 = d2;

                        //Raise StatusChanged event to notify clients
                        if (StatusChanged != null)
                            StatusChanged(this, null);
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void UpdateStatus()
        {
            short d1 = 0, d2 = 0;
            UpdateStatus(ref d1, ref d2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="robotStatus"></param>
        /// <returns></returns>
        public short GetStatus(out RobotStatus robotStatus)
        {
            robotStatus = new RobotStatus();

            lock (_DX200AccessLock)
            {
                short d1 = 0;
                short d2 = 0; ;
                short ret = Motocom.BscGetStatus(_handle, ref d1, ref d2);

                if (ret != 0)
                    throw new Exception("Error getting status !");
                else
                {
                    //check bits and set properties
                    if (d1 != _oldStatusD1 || d2 != _oldStatusD2)
                    {
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
                    }
                }

                return ret;
            }
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

                ret = Motocom.BscFindFirst(_handle, jobname, Motocom.MaxJobNameLength + 1);
                if (ret < -1)
                    throw new Exception("Error reading job list !");
                if (ret == 0)
                {
                    JobList.Add(jobname.ToString());
                    do
                    {
                        ret = Motocom.BscFindNext(_handle, jobname, Motocom.MaxJobNameLength + 1);
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

                ret = Motocom.BscSelectJob(_handle, JobName);
                if (ret == 0)
                {
                    ret = Motocom.BscDeleteJob(_handle);
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

                ret = Motocom.BscReadAlarmS(_handle, ref errorno, errormsg);
                if (ret != 0)
                    throw new Exception("Error reading error/alarm information !");

                error.ErrorNo = errorno;
                error.ErrorMsg = errormsg.ToString();

                ret = Motocom.BscGetFirstAlarmS(_handle, ref alarmsubno, alarmmsg);
                if (ret < 0)
                    throw new Exception("Error reading alarms !");

                if (ret > 0)
                {

                    alarmlist.Add(new AlarmHistoryItem(ret, alarmsubno, alarmmsg.ToString()));

                    do
                    {
                        ret = Motocom.BscGetNextAlarmS(_handle, ref alarmsubno, alarmmsg);
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
                short ret = Motocom.BscReset(_handle);
                if (ret != 0)
                    throw new Exception("Error executing BscReset");
                return ret;
            }
        }
        /// <summary>
        /// Sets Teach mode
        /// </summary>
        public void SetTeachMode()
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscSelectMode(_handle, 1);
                if (ret != 0)
                    throw new Exception("Error executing BscSelectMode");
            }
        }
        /// <summary>
        /// Sets Play mode
        /// </summary>
        public void SetPlayMode()
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscSelectMode(_handle, 2);
                if (ret != 0)
                    throw new Exception("Error executing BscSelectMode");
            }
        }
        /// <summary>
        /// Sets Servo ON
        /// </summary>
        public void SetServoOn()
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscServoOn(_handle);
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
                short ret = Motocom.BscServoOff(_handle);
                if (ret != 0)
                {
                    throw new Exception("Error executing BscServoOff");
                }
            }
        }
        public short IsServo()
        {
            return Motocom.BscIsServo(_handle);
        }
        /// <summary>
        /// Sets Hold ON
        /// </summary>
        public short SetHoldOn()
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscHoldOn(_handle);
                if (ret != 0)
                    throw new Exception("Error executing BscHoldOn");

                return ret;
            }
        }
        /// <summary>
        /// Sets Hold OFF
        /// </summary>
        public short SetHoldOff()
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscHoldOff(_handle);
                if (ret != 0)
                    throw new Exception("Error executing BscHoldOff");

                return ret;
            }
        }
        /// <summary>
        /// Starts operation from the current line of current job
        /// </summary>
        public short Start()
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscContinueJob(_handle);
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
                short ret = Motocom.BscHostGetVarDataM(_handle, (short)SimpleVar.VarType, SimpleVar.StartIndex, SimpleVar.ListSize, ref SimpleVar.VarListArray[0]);
                if (ret != 0)
                    throw new Exception("Error executing BscHostGetVarDataM");
            }
        }
        /// <summary>
        /// Reads position variable
        /// </summary>
        /// <param name="Index">Index of variable to read</param>
        /// <param name="PosVar">Object receiving results</param>
        public short ReadPositionVariable(short Index, out RobPosVar PosVar)
        {
            lock (_DX200AccessLock)
            {
                PosVar = new RobPosVar();

                StringBuilder StringVal = new StringBuilder(256);
                double[] PosVarArray = new double[12];
                short ret = Motocom.BscHostGetVarData(_handle, 4, Index, ref PosVarArray[0], StringVal);
                if (ret != 0)
                    throw new Exception("Error executing BscHostGetVarData");
                PosVar.HostGetVarDataArray = PosVarArray;

                return ret;
            }
        }
        /// <summary>
        /// Writes position variable
        /// </summary>
        /// <param name="Index">Index of variable to write</param>
        /// <param name="PosVar">Object containg values to write</param>
        public short WritePositionVariable(short Index, RobPosVar PosVar)
        {
            lock (_DX200AccessLock)
            {
                StringBuilder StringVal = new StringBuilder(256);
                double[] PosVarArray = PosVar.HostGetVarDataArray;
                short ret = Motocom.BscHostPutVarData(_handle, 4, Index, ref PosVarArray[0], StringVal);
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
                short ret = Motocom.BscHostPutVarDataM(_handle, (short)SimpleVar.VarType, SimpleVar.StartIndex, SimpleVar.ListSize, ref SimpleVar.VarListArray[0]);
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
                    ret = Motocom.BscUpLoad(_handle, _Filetitle);
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
                short ret = Motocom.BscWriteIO2(_handle, Address, 1, ref iovalue);
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
                short ret = Motocom.BscReadIO2(_handle, StartAddress, (short)(NumberOfGroups * 8), ref ioValues[0]);
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
                short ret = Motocom.BscWriteIO2(_handle, StartAddress, (short)(NumberOfGroups * 8), ref IOGroupValues[0]);
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
                ret = Motocom.BscReadIO2(_handle, Address, 1, ref IOVal);
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
                    ret = Motocom.BscDownLoad(_handle, _Filetitle);
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

                ret = Motocom.BscSelectJob(_handle, JobName);
                if (ret == 0)
                {
                    ret = Motocom.BscStartJob(_handle);
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
                    ret = Motocom.BscChangeTask(_handle, TaskID);
                    if (ret != 0)
                    {
                        throw new Exception("Error changing task !");
                    }
                }
                ret = Motocom.BscIsJobName(_handle, jobname, 255);
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
                    ret = Motocom.BscChangeTask(_handle, TaskID);
                    if (ret != 0)
                    {
                        throw new Exception("Error changing task !");
                    }
                }
                ret = Motocom.BscSelectJob(_handle, JobName);
                if (ret == 0)
                {
                    ret = Motocom.BscSetLineNumber(_handle, linenumber);
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
                    ret = Motocom.BscChangeTask(_handle, TaskID);
                    if (ret != 0)
                    {
                        throw new Exception("Error changing task !");
                    }
                }
                ret = Motocom.BscIsJobLine(_handle);
                if (ret == -1)
                    throw new Exception("Error getting current job line !");
            }
            return ret;
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
                return Motocom.BscMovl(_handle, moveSpeedSelection, speed, frameName, rconf, toolNo, ref targetPosition);
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
                return Motocom.BscMovj(_handle, speed, frameName, rconf, toolNo, ref targetPosition);
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
                return Motocom.BscPMovl(_handle, moveSpeedSelection, speed, toolNo, ref targetPosition);
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
                return Motocom.BscPMovj(_handle, speed, toolNo, ref targetPosition);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="toolNo"></param>
        /// <param name="increamentValue"></param>
        /// <returns></returns>
        public short IMov(StringBuilder moveSpeedSelection, double speed, StringBuilder frameName, short toolNo, ref double increamentValue)
        {
            lock (_DX200AccessLock)
            {
                return Motocom.BscImov(_handle, moveSpeedSelection, speed, frameName, toolNo, ref increamentValue);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameName"></param>
        /// <param name="isEx"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public short GetRobotPosition(StringBuilder frameName, short isEx, ref short rconf, ref short toolNumber, ref double position)
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscIsRobotPos(_handle, frameName, isEx, ref rconf, ref toolNumber, ref position);
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
        /// <param name="isPulseOrXYZ"></param>
        /// <param name="rconf"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public short GetRobotPosition(short isPulseOrXYZ, ref short rconf, ref double position)
        {
            lock (_DX200AccessLock)
            {
                short ret = Motocom.BscIsLoc(_handle, isPulseOrXYZ, ref rconf, ref position);

                if (ret != 0)
                {
                    throw new Exception("Error reading position!");
                }

                return ret;
            }
        }

        #endregion

        #region Event handler
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void StatusTimerTick(object state)
        {
            short d1 = 0;
            short d2 = 0;

            try
            {
                lock (_lockStatusTimer)
                {
                    UpdateStatus(ref d1, ref d2);
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
    }
}
