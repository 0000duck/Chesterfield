using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace YaskawaNet
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(true)]
    [Guid("7D416892-775F-4BAB-9B0D-B7AC84F9439F")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IRobotPosition
    {
        #region Properties

        double ActualSpeed
        {
            get;
            set;
        }
        double[] SpeedLimits
        {
            get;
            set;
        }

        double SAxis
        {
            get;
            set;
        }
        bool SAxisInMotion
        {
            get;
        }
        bool SAxisInHome
        {
            get;
        }
        double MinSJointPosition
        {
            get;
        }
        double MaxSJointPosition
        {
            get;
        }
        double SJointHomePosition
        {
            get;
            set;
        }

        double LAxis
        {
            get;
            set;
        }
        bool LAxisInMotion
        {
            get;
        }
        bool LAxisInHome
        {
            get;
        }
        double MinLJointPosition
        {
            get;
        }
        double MaxLJointPosition
        {
            get;
        }
        double LJointHomePosition
        {
            get;
            set;
        }

        double UAxis
        {
            get;
            set;
        }
        bool UAxisInMotion
        {
            get;
        }
        bool UAxisInHome
        {
            get;
        }
        double MinUJointPosition
        {
            get;
        }
        double MaxUJointPosition
        {
            get;
        }
        double UJointHomePosition
        {
            get;
            set;
        }

        double RAxis
        {
            get;
            set;
        }
        bool RAxisInMotion
        {
            get;
        }
        bool RAxisInHome
        {
            get;
        }
        double MinRJointPosition
        {
            get;
        }
        double MaxRJointPosition
        {
            get;
        }
        double RJointHomePosition
        {
            get;
            set;
        }

        double BAxis
        {
            get;
            set;
        }
        bool BAxisInMotion
        {
            get;
        }
        bool BAxisInHome
        {
            get;
        }
        double MinBJointPosition
        {
            get;
        }
        double MaxBJointPosition
        {
            get;
        }
        double BJointHomePosition
        {
            get;
            set;
        }

        double TAxis
        {
            get;
            set;
        }
        bool TAxisInMotion
        {
            get;
        }
        bool TAxisInHome
        {
            get;
        }
        double MinTJointPosition
        {
            get;
        }
        double MaxTJointPosition
        {
            get;
        }
        double TJointHomePosition
        {
            get;
            set;
        }

        double X
        {
            get;
            set;
        }
        bool XAxisInMotion
        {
            get;
        }
        bool XAxisInHome
        {
            get;
        }
        double MinXAxisPosition
        {
            get;
        }
        double MaxXAxisPosition
        {
            get;
        }
        double XAxisHomePosition
        {
            get;
            set;
        }

        double Y
        {
            get;
            set;
        }
        double Z
        {
            get;
            set;
        }
        double Rx
        {
            get;
            set;
        }
        double Ry
        {
            get;
            set;
        }
        double Rz
        {
            get;
            set;
        }
        double E7Axis
        {
            get;
            set;
        }
        double E8Axis
        {
            get;
            set;
        }
        double E9Axis
        {
            get;
            set;
        }
        double E10Axis
        {
            get;
            set;
        }
        double E11Axis
        {
            get;
            set;
        }
        double E12Axis
        {
            get;
            set;
        }

        [ComVisible(false)]
        double[][] Limits
        {
            get;
            set;
        }
        [ComVisible(false)]
        double[][] LimitsPulse
        {
            get;
            set;
        }

        double[] RobotPositions
        {
            get;
            set;
        }
        double[] RobotHomePositions
        {
            get;
            set;
        }
        double[] RobotParkPositions
        {
            get;
            set;
        }
        double[] RobotPulsePositions
        {
            get;
            set;
        }
        double[] RobotPulseHomePositions
        {
            get;
            set;
        }
        double[] RobotPulseParkPositions
        {
            get;
            set;
        }

        bool[] InMotionArray
        {
            get;
            set;
        }

        #endregion

        #region Methods

        #endregion
    }
    [ComVisible(true)]
    public class RobotPosition : IRobotPosition
    {
        #region Constants

        const double SJointPulsesDegreeRatio = 1341.380;
        const double LJointPulsesDegreeRatio = 1907.674;//-
        const double UJointPulsesDegreeRatio = 1592.889;
        const double RJointPulsesDegreeRatio = 1022.862;//-
        const double BJointPulsesDegreeRatio = 986.074;
        const double TJointPulsesDegreeRatio = 631.299;//-
        const double TrackerPulsesMmRatio = 159.278;

        #endregion

        #region Fields

        volatile double[] _actualPositions = new double[12];
        volatile double[] _homePositions = new double[12];
        volatile double[] _parkPositions = new double[12];
        volatile double[] _pulseHomePositions = new double[12];
        volatile double[] _pulseParkPositions = new double[12];
        volatile double[] _pulsePositions = new double[12];

        readonly object _sAxisLocker = new object();
        readonly object _lAxisLocker = new object();
        readonly object _uAxisLocker = new object();
        readonly object _rAxisLocker = new object();
        readonly object _bAxisLocker = new object();
        readonly object _tAxisLocker = new object();

        readonly object _xAxisLocker = new object();

        readonly object _positionsLocker = new object();
        readonly object _homePositionsLocker = new object();
        readonly object _parkPositionsLocker = new object();
        readonly object _pulsePositionsLocker = new object();
        readonly object _pulseHomePositionsLocker = new object();
        readonly object _pulseParkPositionsLocker = new object();

        private double _actualSpeed = 0.0;
        private double[] _speedLimits = new double[12] { 50.0, 197.0, 190.0, 210.0, 410.0, 410.0, 620.0, 0, 0, 0, 0, 0 }; //MH24 datasheet

        private bool _sJointInHome = false;
        private double _actualSJointPosition = 0.0;
        private double _minSJointPosition = -180.0;
        private double _maxSJointPosition = 180.0;
        private double _sJointHomePosition = 0;

        private bool _lJointInHome = false;
        private double _actualLJointPosition = 0.0;
        private double _minLJointPosition = -105.0;
        private double _maxLJointPosition = 155.0;
        private double _lJointHomePosition = 0;

        private bool _uJointInHome = false;
        private double _actualUJointPosition = 0.0;
        private double _minUJointPosition = -170.0;
        private double _maxUJointPosition = 240.0;
        private double _uJointHomePosition = 0;

        private bool _rJointInHome = false;
        private double _actualRJointPosition = 0.0;
        private double _minRJointPosition = -200.0;
        private double _maxRJointPosition = 200.0;
        private double _rJointHomePosition = 0;

        private bool _bJointInHome = false;
        private double _actualBJointPosition = 0.0;
        private double _minBJointPosition = -150.0;
        private double _maxBJointPosition = 150.0;
        private double _bJointHomePosition = 0;

        private bool _tJointInHome = false;
        private double _actualTJointPosition = 0.0;
        private double _minTJointPosition = -150.0;
        private double _maxTJointPosition = 150.0;
        private double _tJointHomePosition = 0;

        private bool _xAxisInHome = false;
        private double _actualXAxisPosition = 0.0;
        private double _minXAxisPosition = -1500.0;
        private double _maxXAxisPosition = 1500.0;
        private double _xAxisHomePosition = 0;

        private double _actualYAxisPosition = 0.0;
        private double _actualZAxisPosition = 0.0;
        private double _actualRxAxisPosition = 0.0;
        private double _actualRyAxisPosition = 0.0;
        private double _actualRzAxisPosition = 0.0;

        private double[][] _limits = new double[12][]
            {
                new double [2]{0,0} ,
                new double [2]{0,0} ,
                new double [2]{0,0} ,
                new double [2]{0,0} ,
                new double [2]{0,0} ,
                new double [2]{0,0},
                new double [2]{0,0},
                new double [2]{0,0},
                new double [2]{0,0},
                new double [2]{0,0},
                new double [2]{0,0},
                new double [2]{0,0}
            };

        private double[][] _limitsPulse = new double[12][]
            {
                new double [2]{0,0} ,
                new double [2]{0,0} ,
                new double [2]{0,0} ,
                new double [2]{0,0} ,
                new double [2]{0,0} ,
                new double [2]{0,0},
                new double [2]{0,0},
                new double [2]{0,0},
                new double [2]{0,0},
                new double [2]{0,0},
                new double [2]{0,0},
                new double [2]{0,0}
            };

        private bool[] _inMotionArray = new bool[12];

        #endregion

        #region Properties

        public RobotPosition(double e1axis, double e2axis, double e3axis, double e4axis, double e5axis, double e6axis)
        {
            X = e1axis;
            Y = e2axis;
            Z = e3axis;
            Rx = e4axis;
            Ry = e5axis;
            Rz = e6axis;

            SAxis = e1axis;
            LAxis = e2axis;
            UAxis = e3axis;
            RAxis = e4axis;
            BAxis = e5axis;
            TAxis = e6axis;

            E7Axis = 0;
            E8Axis = 0;
            E9Axis = 0;
            E10Axis = 0;
            E11Axis = 0;
            E12Axis = 0;
        }
        public RobotPosition(double e1axis, double e2axis, double e3axis, double e4axis, double e5axis, double e6axis, int e7axis, int e8axis, int e9axis, int e10axis, int e11axis, int e12axis)
        {
            X = e1axis;
            Y = e2axis;
            Z = e3axis;
            Rx = e4axis;
            Ry = e5axis;
            Rz = e6axis;

            SAxis = e1axis;
            LAxis = e2axis;
            UAxis = e3axis;
            RAxis = e4axis;
            BAxis = e5axis;
            TAxis = e6axis;

            E7Axis = e7axis;
            E8Axis = e8axis;
            E9Axis = e9axis;

            E10Axis = e10axis;
            E11Axis = e11axis;
            E12Axis = e12axis;
        }
        public RobotPosition()
        {
            SAxis = 0;
            LAxis = 0;
            UAxis = 0;
            RAxis = 0;
            BAxis = 0;
            TAxis = 0;

            X = 0.0;
            Y = 0.0;
            Z = 0.0;
            Rx = 0.0;
            Ry = 0.0;
            Rz = 0.0;

            E7Axis = 0;
            E8Axis = 0;
            E9Axis = 0;
            E10Axis = 0;
            E11Axis = 0;
            E12Axis = 0;

            _actualPositions.Initialize();
        }
        public RobotPosition(double[] positions)
        {
            positions.CopyTo(_actualPositions, 0);
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
        public double[] SpeedLimits
        {
            get
            {
                return _speedLimits;
            }
            set
            {
                _speedLimits = value;
            }
        }

        public double SAxis
        {
            get
            {
                lock (_sAxisLocker)
                {
                    return _actualPositions[0];
                }
            }
            set
            {
                lock (_sAxisLocker)
                {
                    _inMotionArray[0] = (_actualPositions[0] != value);
                    _actualPositions[0] = value;
                    _pulsePositions[0] = _actualPositions[0] * SJointPulsesDegreeRatio;

                    _sJointInHome = (_actualSJointPosition == _homePositions[0]);
                }
            }
        }
        public bool SAxisInMotion
        {
            get
            {
                lock (_sAxisLocker)
                {
                    return _inMotionArray[0];
                }
            }
        }
        public bool SAxisInHome
        {
            get
            {
                lock (_sAxisLocker)
                {
                    return _sJointInHome;
                }
            }
        }
        public double MinSJointPosition
        {
            get
            {
                lock (_sAxisLocker)
                {
                    return _minSJointPosition;
                }
            }
            set
            {
                lock (_sAxisLocker)
                {
                    if (value >= -180.0 && value <= 180.0)
                    {
                        _limits[0][0] = _minSJointPosition = value;
                    }
                }
            }
        }
        public double MaxSJointPosition
        {
            get
            {
                lock (_sAxisLocker)
                {
                    return _maxSJointPosition;

                }
            }
            set
            {
                lock (_sAxisLocker)
                {
                    if (value >= -180.0 && value <= 180.0)
                    {
                        _limits[0][1] = _maxSJointPosition = value;
                    }
                }
            }
        }
        public double SJointHomePosition
        {
            get
            {
                lock (_sAxisLocker)
                {
                    return _sJointHomePosition;

                }
            }
            set
            {
                lock (_sAxisLocker)
                {
                    _homePositions[0] = _sJointHomePosition = value;
                }
            }
        }

        public double LAxis
        {
            get
            {
                lock (_lAxisLocker)
                {
                    return _actualPositions[1];
                }
            }
            set
            {
                lock (_lAxisLocker)
                {
                    _inMotionArray[1] = (_actualPositions[1] != value);
                    _actualPositions[1] = value;
                    _pulsePositions[1] = _actualPositions[1] * LJointPulsesDegreeRatio;

                    _lJointInHome = (_actualLJointPosition == _homePositions[1]);
                }
            }
        }
        public bool LAxisInMotion
        {
            get
            {
                lock (_lAxisLocker)
                {
                    return _inMotionArray[1];
                }
            }
        }
        public bool LAxisInHome
        {
            get
            {
                lock (_lAxisLocker)
                {
                    return _lJointInHome;
                }
            }
        }
        public double MinLJointPosition
        {
            get
            {
                lock (_lAxisLocker)
                {
                    return _minLJointPosition;
                }
            }
            set
            {
                lock (_lAxisLocker)
                {
                    if (value >= -105.0 && value <= 155.0)
                    {
                        _limits[1][0] = _minLJointPosition = value;
                    }
                }
            }
        }
        public double MaxLJointPosition
        {
            get
            {
                lock (_lAxisLocker)
                {
                    return _maxLJointPosition;

                }
            }
            set
            {
                lock (_lAxisLocker)
                {
                    if (value >= -105.0 && value <= 155.0)
                    {
                        _limits[1][1] = _maxLJointPosition = value;
                    }
                }
            }
        }
        public double LJointHomePosition
        {
            get
            {
                lock (_lAxisLocker)
                {
                    return _lJointHomePosition;

                }
            }
            set
            {
                lock (_lAxisLocker)
                {
                    _homePositions[1] = _lJointHomePosition = value;
                }
            }
        }

        public double UAxis
        {
            get
            {
                return _actualPositions[2];
            }
            set
            {
                lock (_uAxisLocker)
                {
                    _inMotionArray[2] = (_actualPositions[2] != value);
                    _actualPositions[2] = value;
                    _pulsePositions[2] = _actualPositions[2] * UJointPulsesDegreeRatio;

                    _uJointInHome = (_actualUJointPosition == _homePositions[2]);
                }
            }
        }
        public bool UAxisInMotion
        {
            get
            {
                lock (_uAxisLocker)
                {
                    return _inMotionArray[2];
                }
            }
        }
        public bool UAxisInHome
        {
            get
            {
                lock (_uAxisLocker)
                {
                    return _uJointInHome;
                }
            }
        }
        public double MinUJointPosition
        {
            get
            {
                lock (_uAxisLocker)
                {
                    return _minUJointPosition;
                }
            }
            set
            {
                lock (_uAxisLocker)
                {
                    if (value >= -170.0 && value <= 240.0)
                    {
                        _limits[2][0] = _minUJointPosition = value;
                    }
                }
            }
        }
        public double MaxUJointPosition
        {
            get
            {
                lock (_uAxisLocker)
                {
                    return _maxUJointPosition;

                }
            }
            set
            {
                lock (_uAxisLocker)
                {
                    if (value >= -170.0 && value <= 240.0)
                    {
                        _limits[2][1] = _maxUJointPosition = value;
                    }
                }
            }
        }
        public double UJointHomePosition
        {
            get
            {
                lock (_uAxisLocker)
                {
                    return _uJointHomePosition;

                }
            }
            set
            {
                lock (_uAxisLocker)
                {
                    _homePositions[2] = _uJointHomePosition = value;
                }
            }
        }

        public double RAxis
        {
            get
            {
                return _actualPositions[3];
            }
            set
            {
                lock (_rAxisLocker)
                {
                    _inMotionArray[3] = (_actualPositions[3] != value);
                    _actualPositions[3] = value;
                    _pulsePositions[3] = _actualPositions[3] * RJointPulsesDegreeRatio;

                    _rJointInHome = (_actualRJointPosition == _homePositions[3]);
                }
            }
        }
        public bool RAxisInMotion
        {
            get
            {
                lock (_rAxisLocker)
                {
                    return _inMotionArray[3];
                }
            }
        }
        public bool RAxisInHome
        {
            get
            {
                lock (_rAxisLocker)
                {
                    return _rJointInHome;
                }
            }
        }
        public double MinRJointPosition
        {
            get
            {
                lock (_rAxisLocker)
                {
                    return _minRJointPosition;
                }
            }
            set
            {
                lock (_rAxisLocker)
                {
                    if (value >= -200.0 && value <= 200.0)
                    {
                        _limits[3][0] = _minRJointPosition = value;
                    }
                }
            }
        }
        public double MaxRJointPosition
        {
            get
            {
                lock (_rAxisLocker)
                {
                    return _maxRJointPosition;

                }
            }
            set
            {
                lock (_rAxisLocker)
                {
                    if (value >= -200.0 && value <= 200.0)
                    {
                        _limits[3][1] = _maxRJointPosition = value;
                    }
                }
            }
        }
        public double RJointHomePosition
        {
            get
            {
                lock (_rAxisLocker)
                {
                    return _rJointHomePosition;

                }
            }
            set
            {
                lock (_rAxisLocker)
                {
                    _homePositions[3] = _rJointHomePosition = value;
                }
            }
        }

        public double BAxis
        {
            get
            {
                return _actualPositions[4];
            }
            set
            {
                lock (_bAxisLocker)
                {
                    _inMotionArray[4] = (_actualPositions[4] != value);
                    _actualPositions[4] = value;
                    _pulsePositions[4] = _actualPositions[4] * BJointPulsesDegreeRatio;

                    _bJointInHome = (_actualBJointPosition == _homePositions[4]);
                }
            }
        }
        public bool BAxisInMotion
        {
            get
            {
                lock (_bAxisLocker)
                {
                    return _inMotionArray[4];
                }
            }
        }
        public bool BAxisInHome
        {
            get
            {
                lock (_bAxisLocker)
                {
                    return _bJointInHome;
                }
            }
        }
        public double MinBJointPosition
        {
            get
            {
                lock (_bAxisLocker)
                {
                    return _minBJointPosition;
                }
            }
            set
            {
                lock (_bAxisLocker)
                {
                    if (value >= -150.0 && value <= 150.0)
                    {
                        _limits[4][0] = _minBJointPosition = value;
                    }
                }
            }
        }
        public double MaxBJointPosition
        {
            get
            {
                lock (_bAxisLocker)
                {
                    return _maxBJointPosition;

                }
            }
            set
            {
                lock (_bAxisLocker)
                {
                    if (value >= -150.0 && value <= 150.0)
                    {
                        _limits[4][1] = _maxBJointPosition = value;
                    }
                }
            }
        }
        public double BJointHomePosition
        {
            get
            {
                lock (_bAxisLocker)
                {
                    return _bJointHomePosition;

                }
            }
            set
            {
                lock (_bAxisLocker)
                {
                    _homePositions[4] = _bJointHomePosition = value;
                }
            }
        }

        public double TAxis
        {
            get
            {
                return _actualPositions[5];
            }
            set
            {
                lock (_tAxisLocker)
                {
                    _inMotionArray[5] = (_actualPositions[5] != value);
                    _actualPositions[5] = value;
                    _pulsePositions[5] = _actualPositions[5] * TJointPulsesDegreeRatio;

                    _tJointInHome = (_actualTJointPosition == _homePositions[5]);
                }
            }
        }
        public bool TAxisInMotion
        {
            get
            {
                lock (_tAxisLocker)
                {
                    return _inMotionArray[5];
                }
            }
        }
        public bool TAxisInHome
        {
            get
            {
                lock (_tAxisLocker)
                {
                    return _tJointInHome;
                }
            }
        }
        public double MinTJointPosition
        {
            get
            {
                lock (_tAxisLocker)
                {
                    return _minTJointPosition;
                }
            }
            set
            {
                lock (_tAxisLocker)
                {
                    if (value >= -455.0 && value <= 455.0)
                    {
                        _limits[5][0] = _minTJointPosition = value;
                    }
                }
            }
        }
        public double MaxTJointPosition
        {
            get
            {
                lock (_tAxisLocker)
                {
                    return _maxTJointPosition;

                }
            }
            set
            {
                lock (_tAxisLocker)
                {
                    if (value >= -455.0 && value <= 455.0)
                    {
                        _limits[5][1] = _maxTJointPosition = value;
                    }
                }
            }
        }
        public double TJointHomePosition
        {
            get
            {
                lock (_tAxisLocker)
                {
                    return _tJointHomePosition;

                }
            }
            set
            {
                lock (_tAxisLocker)
                {
                    _homePositions[5] = _tJointHomePosition = value;
                }
            }
        }

        public double X
        {
            get
            {
                return _actualPositions[0];
            }
            set
            {
                lock (_xAxisLocker)
                {
                    _inMotionArray[0] = (_actualPositions[0] != value);
                    _actualPositions[0] = value;

                    _xAxisInHome = (_actualXAxisPosition == _homePositions[0]);
                }
            }
        }
        public bool XAxisInMotion
        {
            get
            {
                lock (_xAxisLocker)
                {
                    return _inMotionArray[0];
                }
            }
        }
        public bool XAxisInHome
        {
            get
            {
                lock (_xAxisLocker)
                {
                    return _xAxisInHome;
                }
            }
        }
        public double MinXAxisPosition
        {
            get
            {
                lock (_xAxisLocker)
                {
                    return _minXAxisPosition;
                }
            }
            set
            {
                lock (_xAxisLocker)
                {
                    if (value >= -10000.0 && value <= 10000.0)
                    {
                        _limits[0][0] = _minXAxisPosition = value;
                    }
                }
            }
        }
        public double MaxXAxisPosition
        {
            get
            {
                lock (_xAxisLocker)
                {
                    return _maxXAxisPosition;

                }
            }
            set
            {
                lock (_xAxisLocker)
                {
                    if (value >= -10000.0 && value <= 10000.0)
                    {
                        _limits[0][1] = _maxXAxisPosition = value;
                    }
                }
            }
        }
        public double XAxisHomePosition
        {
            get
            {
                lock (_xAxisLocker)
                {
                    return _xAxisHomePosition;

                }
            }
            set
            {
                lock (_xAxisLocker)
                {
                    _homePositions[0] = _xAxisHomePosition = value;
                }
            }
        }

        public double Y
        {
            get
            {
                return _actualPositions[1];
            }
            set
            {
                _inMotionArray[1] = (_actualPositions[1] != value);
                _actualPositions[1] = value;
            }
        }
        public double Z
        {
            get
            {
                return _actualPositions[2];
            }
            set
            {
                _inMotionArray[2] = (_actualPositions[2] != value);
                _actualPositions[2] = value;
            }
        }
        public double Rx
        {
            get
            {
                return _actualPositions[3];
            }
            set
            {
                _inMotionArray[3] = (_actualPositions[3] != value);
                _actualPositions[3] = value;
            }
        }
        public double Ry
        {
            get
            {
                return _actualPositions[4];
            }
            set
            {
                _inMotionArray[4] = (_actualPositions[4] != value);
                _actualPositions[4] = value;
            }
        }
        public double Rz
        {
            get
            {
                return _actualPositions[5];
            }
            set
            {
                _inMotionArray[5] = (_actualPositions[5] != value);
                _actualPositions[5] = value;
            }
        }
        public double E7Axis
        {
            get
            {
                return _actualPositions[6];
            }
            set
            {
                _inMotionArray[6] = (_actualPositions[6] != value);
                _actualPositions[6] = value;
                _pulsePositions[6] = _actualPositions[6] * TrackerPulsesMmRatio;
            }
        }
        public double E8Axis
        {
            get
            {
                return _actualPositions[7];
            }
            set
            {
                _actualPositions[7] = value;
            }
        }
        public double E9Axis
        {
            get
            {
                return _actualPositions[8];
            }
            set
            {
                _actualPositions[8] = value;
            }
        }
        public double E10Axis
        {
            get
            {
                return _actualPositions[9];
            }
            set
            {
                _actualPositions[9] = value;
            }
        }
        public double E11Axis
        {
            get
            {
                return _actualPositions[10];
            }
            set
            {
                _actualPositions[10] = value;
            }
        }
        public double E12Axis
        {
            get
            {
                return _actualPositions[11];
            }
            set
            {
                _actualPositions[11] = value;
            }
        }

        public double[][] Limits
        {
            get
            {
                return _limits;
            }
            set
            {
                _limits = value;

                _limitsPulse[0][0] = _limits[0][0] * SJointPulsesDegreeRatio;
                _limitsPulse[0][1] = _limits[0][1] * SJointPulsesDegreeRatio;

                _limitsPulse[1][0] = _limits[1][0] * LJointPulsesDegreeRatio;
                _limitsPulse[1][1] = _limits[1][1] * LJointPulsesDegreeRatio;

                _limitsPulse[2][0] = _limits[2][0] * UJointPulsesDegreeRatio;
                _limitsPulse[2][1] = _limits[2][1] * UJointPulsesDegreeRatio;

                _limitsPulse[3][0] = _limits[3][0] * RJointPulsesDegreeRatio;
                _limitsPulse[3][1] = _limits[3][1] * RJointPulsesDegreeRatio;

                _limitsPulse[4][0] = _limits[4][0] * BJointPulsesDegreeRatio;
                _limitsPulse[4][1] = _limits[4][1] * BJointPulsesDegreeRatio;

                _limitsPulse[5][0] = _limits[5][0] * TJointPulsesDegreeRatio;
                _limitsPulse[5][1] = _limits[5][1] * TJointPulsesDegreeRatio;

                _limitsPulse[6][0] = _limits[6][0] * TrackerPulsesMmRatio;
                _limitsPulse[6][1] = _limits[6][1] * TrackerPulsesMmRatio;
            }
        }
        public double[][] LimitsPulse
        {
            get
            {
                return _limitsPulse;
            }
            set
            {
                _limitsPulse = value;

                _limits[0][0] = _limitsPulse[0][0] / SJointPulsesDegreeRatio;
                _limits[0][1] = _limitsPulse[0][1] / SJointPulsesDegreeRatio;

                _limits[1][0] = _limitsPulse[1][0] / LJointPulsesDegreeRatio;
                _limits[1][1] = _limitsPulse[1][1] / LJointPulsesDegreeRatio;

                _limits[2][0] = _limitsPulse[2][0] / UJointPulsesDegreeRatio;
                _limits[2][1] = _limitsPulse[2][1] / UJointPulsesDegreeRatio;

                _limits[3][0] = _limitsPulse[3][0] / RJointPulsesDegreeRatio;
                _limits[3][1] = _limitsPulse[3][1] / RJointPulsesDegreeRatio;

                _limits[4][0] = _limitsPulse[4][0] / BJointPulsesDegreeRatio;
                _limits[4][1] = _limitsPulse[4][1] / BJointPulsesDegreeRatio;

                _limits[5][0] = _limitsPulse[5][0] / TJointPulsesDegreeRatio;
                _limits[5][1] = _limitsPulse[5][1] / TJointPulsesDegreeRatio;

                _limits[6][0] = _limitsPulse[5][0] / TrackerPulsesMmRatio;
                _limits[6][1] = _limitsPulse[5][1] / TrackerPulsesMmRatio;
            }
        }

        public double[] RobotPositions
        {
            get
            {
                lock (_positionsLocker)
                {
                    return _actualPositions;
                }
            }
            set
            {
                lock (_positionsLocker)
                {
                    _inMotionArray[0] = (_actualPositions[0] != value[0]);
                    _inMotionArray[1] = (_actualPositions[1] != value[1]);
                    _inMotionArray[2] = (_actualPositions[2] != value[2]);
                    _inMotionArray[3] = (_actualPositions[3] != value[3]);
                    _inMotionArray[4] = (_actualPositions[4] != value[4]);
                    _inMotionArray[5] = (_actualPositions[5] != value[5]);
                    _inMotionArray[6] = (_actualPositions[6] != value[6]);
                    _inMotionArray[7] = (_actualPositions[7] != value[7]);
                    _inMotionArray[8] = (_actualPositions[8] != value[8]);
                    _inMotionArray[9] = (_actualPositions[9] != value[9]);
                    _inMotionArray[10] = (_actualPositions[10] != value[10]);
                    _inMotionArray[11] = (_actualPositions[11] != value[11]);

                    _actualPositions = value;

                    _actualSJointPosition = _actualXAxisPosition = _actualPositions[0];
                    _actualLJointPosition = _actualYAxisPosition = _actualPositions[1];
                    _actualUJointPosition = _actualZAxisPosition = _actualPositions[2];
                    _actualRJointPosition = _actualRxAxisPosition = _actualPositions[3];
                    _actualBJointPosition = _actualRyAxisPosition = _actualPositions[4];
                    _actualTJointPosition = _actualRzAxisPosition = _actualPositions[5];

                    _sJointInHome = _xAxisInHome = (_actualPositions[0] == _homePositions[0]);
                    _lJointInHome = (_actualPositions[1] == _homePositions[1]);
                    _uJointInHome = (_actualPositions[2] == _homePositions[2]);
                    _rJointInHome = (_actualPositions[3] == _homePositions[3]);
                    _bJointInHome = (_actualPositions[4] == _homePositions[4]);
                    _tJointInHome = (_actualPositions[5] == _homePositions[5]);
                }
            }
        }
        public double[] RobotHomePositions
        {
            get
            {
                lock (_homePositionsLocker)
                {
                    return _homePositions;
                }
            }
            set
            {
                lock (_homePositionsLocker)
                {
                    _homePositions = value;

                    _pulseHomePositions[0] = _homePositions[0] * SJointPulsesDegreeRatio;
                    _pulseHomePositions[1] = _homePositions[1] * LJointPulsesDegreeRatio;
                    _pulseHomePositions[2] = _homePositions[2] * UJointPulsesDegreeRatio;
                    _pulseHomePositions[3] = _homePositions[3] * RJointPulsesDegreeRatio;
                    _pulseHomePositions[4] = _homePositions[4] * BJointPulsesDegreeRatio;
                    _pulseHomePositions[5] = _homePositions[5] * TJointPulsesDegreeRatio;
                }
            }
        }
        public double[] RobotParkPositions
        {
            get
            {
                lock (_parkPositionsLocker)
                {
                    return _parkPositions;
                }
            }
            set
            {
                lock (_parkPositionsLocker)
                {
                    _parkPositions = value;

                    _pulseParkPositions[0] = _parkPositions[0] * SJointPulsesDegreeRatio;
                    _pulseParkPositions[1] = _parkPositions[1] * LJointPulsesDegreeRatio;
                    _pulseParkPositions[2] = _parkPositions[2] * UJointPulsesDegreeRatio;
                    _pulseParkPositions[3] = _parkPositions[3] * RJointPulsesDegreeRatio;
                    _pulseParkPositions[4] = _parkPositions[4] * BJointPulsesDegreeRatio;
                    _pulseParkPositions[5] = _parkPositions[5] * TJointPulsesDegreeRatio;
                }
            }
        }
        public double[] RobotPulsePositions
        {
            get
            {
                lock (_pulsePositionsLocker)
                {
                    return _pulsePositions;
                }
            }
            set
            {
                lock (_pulsePositionsLocker)
                {
                    _inMotionArray[0] = (_pulsePositions[0] != value[0]);
                    _inMotionArray[1] = (_pulsePositions[1] != value[1]);
                    _inMotionArray[2] = (_pulsePositions[2] != value[2]);
                    _inMotionArray[3] = (_pulsePositions[3] != value[3]);
                    _inMotionArray[4] = (_pulsePositions[4] != value[4]);
                    _inMotionArray[5] = (_pulsePositions[5] != value[5]);
                    _inMotionArray[6] = (_pulsePositions[6] != value[6]);
                    _inMotionArray[7] = (_pulsePositions[7] != value[7]);
                    _inMotionArray[8] = (_pulsePositions[8] != value[8]);
                    _inMotionArray[9] = (_pulsePositions[9] != value[9]);
                    _inMotionArray[10] = (_pulsePositions[10] != value[10]);
                    _inMotionArray[11] = (_pulsePositions[11] != value[11]);

                    _pulsePositions = value;

                    _actualSJointPosition = _actualPositions[0] = _pulsePositions[0] / SJointPulsesDegreeRatio;
                    _actualLJointPosition = _actualPositions[1] = _pulsePositions[1] / LJointPulsesDegreeRatio;
                    _actualUJointPosition = _actualPositions[2] = _pulsePositions[2] / UJointPulsesDegreeRatio;
                    _actualRJointPosition = _actualPositions[3] = _pulsePositions[3] / RJointPulsesDegreeRatio;
                    _actualBJointPosition = _actualPositions[4] = _pulsePositions[4] / BJointPulsesDegreeRatio;
                    _actualTJointPosition = _actualPositions[5] = _pulsePositions[5] / TJointPulsesDegreeRatio;
                    _actualPositions[6] = _pulsePositions[6] / TrackerPulsesMmRatio;
                }
            }
        }
        public double[] RobotPulseHomePositions
        {
            get
            {
                lock (_pulseHomePositionsLocker)
                {
                    return _pulseHomePositions;
                }
            }
            set
            {
                lock (_pulseHomePositionsLocker)
                {
                    _pulseHomePositions = value;

                    _homePositions[0] = _pulseHomePositions[0] / SJointPulsesDegreeRatio;
                    _homePositions[1] = _pulseHomePositions[1] / LJointPulsesDegreeRatio;
                    _homePositions[2] = _pulseHomePositions[2] / UJointPulsesDegreeRatio;
                    _homePositions[3] = _pulseHomePositions[3] / RJointPulsesDegreeRatio;
                    _homePositions[4] = _pulseHomePositions[4] / BJointPulsesDegreeRatio;
                    _homePositions[5] = _pulseHomePositions[5] / TJointPulsesDegreeRatio;
                }
            }
        }
        public double[] RobotPulseParkPositions
        {
            get
            {
                lock (_pulseParkPositionsLocker)
                {
                    return _pulseParkPositions;
                }
            }
            set
            {
                lock (_pulseParkPositionsLocker)
                {
                    _pulseParkPositions = value;
                }
            }
        }

        public bool[] InMotionArray
        {
            get
            {
                return _inMotionArray;
            }
            set
            {
                _inMotionArray = value;
            }
        }

        #endregion
    }
}
