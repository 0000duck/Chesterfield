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
        const double LJointPulsesDegreeRatio = 1907.674;
        const double UJointPulsesDegreeRatio = 1592.889;
        const double RJointPulsesDegreeRatio = 1022.862;
        const double BJointPulsesDegreeRatio = 986.074;
        const double TJointPulsesDegreeRatio = 631.299;
        #endregion

        #region Fields
        volatile double[] _actualRobotPositions = new double[12];
        volatile double[] _previousRobotPositions = new double[12];
        volatile double[] _robotHomePositions = new double[12];
        volatile double[] _robotParkPositions = new double[12];
        volatile double[] _robotPulsePositions = new double[12];

        readonly object _sAxisLocker = new object();
        readonly object _lAxisLocker = new object();
        readonly object _uAxisLocker = new object();
        readonly object _rAxisLocker = new object();
        readonly object _bAxisLocker = new object();
        readonly object _tAxisLocker = new object();

        readonly object _xAxisLocker = new object();

        readonly object _robotPositionsLocker = new object();
        readonly object _robotHomePositionsLocker = new object();
        readonly object _robotParkPositionsLocker = new object();
        readonly object _robotPulsePositionsLocker = new object();

        #endregion

        private bool _sJointInMotion = false;
        private bool _sJointInHome = false;
        private double _actualSJointPosition = 0.0;
        private double _previousSJointPosition = 0.0;
        private double _minSJointPosition = -180.0;
        private double _maxSJointPosition = 180.0;
        private double _sJointHomePosition = 0;

        private bool _lJointInMotion = false;
        private bool _lJointInHome = false;
        private double _actualLJointPosition = 0.0;
        private double _previousLJointPosition = 0.0;
        private double _minLJointPosition = -105.0;
        private double _maxLJointPosition = 155.0;
        private double _lJointHomePosition = 0;

        private bool _uJointInMotion = false;
        private bool _uJointInHome = false;
        private double _actualUJointPosition = 0.0;
        private double _previousUJointPosition = 0.0;
        private double _minUJointPosition = -170.0;
        private double _maxUJointPosition = 240.0;
        private double _uJointHomePosition = 0;

        private bool _rJointInMotion = false;
        private bool _rJointInHome = false;
        private double _actualRJointPosition = 0.0;
        private double _previousRJointPosition = 0.0;
        private double _minRJointPosition = -200.0;
        private double _maxRJointPosition = 200.0;
        private double _rJointHomePosition = 0;

        private bool _bJointInMotion = false;
        private bool _bJointInHome = false;
        private double _actualBJointPosition = 0.0;
        private double _previousBJointPosition = 0.0;
        private double _minBJointPosition = -150.0;
        private double _maxBJointPosition = 150.0;
        private double _bJointHomePosition = 0;

        private bool _tJointInMotion = false;
        private bool _tJointInHome = false;
        private double _actualTJointPosition = 0.0;
        private double _previousTJointPosition = 0.0;
        private double _minTJointPosition = -150.0;
        private double _maxTJointPosition = 150.0;
        private double _tJointHomePosition = 0;

        private bool _xAxisInMotion = false;
        private bool _xAxisInHome = false;
        private double _actualXAxisPosition = 0.0;
        private double _previousXAxisPosition = 0.0;
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

            _actualRobotPositions.Initialize();
        }
        public RobotPosition(double[] positions)
        {
            positions.CopyTo(_actualRobotPositions, 0);
        }

        public double SAxis
        {
            get
            {
                lock (_sAxisLocker)
                {
                    return _actualRobotPositions[0];
                }
            }
            set
            {
                lock (_sAxisLocker)
                {
                    _actualRobotPositions[0] = value;
                    _robotPulsePositions[0] = _actualRobotPositions[0] * SJointPulsesDegreeRatio;

                    _inMotionArray[0] = (_previousRobotPositions[0] != _actualRobotPositions[0]);
                    _sJointInHome = (_actualSJointPosition == _robotHomePositions[0]);

                    _previousRobotPositions[0] = _actualRobotPositions[0];
                }
            }
        }
        public bool SAxisInMotion
        {
            get
            {
                lock (_sAxisLocker)
                {
                    return _sJointInMotion;
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
                    _robotHomePositions[0] = _sJointHomePosition = value;
                }
            }
        }

        public double LAxis
        {
            get
            {
                lock (_lAxisLocker)
                {
                    return _actualRobotPositions[1];
                }
            }
            set
            {
                lock (_lAxisLocker)
                {
                    _previousLJointPosition = _actualRobotPositions[1];
                    _actualLJointPosition = _actualRobotPositions[1] = value;
                    _robotPulsePositions[1] = _actualRobotPositions[1] * LJointPulsesDegreeRatio;
                    _inMotionArray[1] = _lJointInMotion = (_previousLJointPosition != _actualLJointPosition);
                    _lJointInHome = (_actualLJointPosition == _robotHomePositions[1]);
                }
            }
        }
        public bool LAxisInMotion
        {
            get
            {
                lock (_lAxisLocker)
                {
                    return _lJointInMotion;
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
                    _robotHomePositions[1] = _lJointHomePosition = value;
                }
            }
        }

        public double UAxis
        {
            get
            {
                return _actualRobotPositions[2];
            }
            set
            {
                lock (_uAxisLocker)
                {
                    _previousUJointPosition = _actualRobotPositions[2];
                    _actualUJointPosition = _actualRobotPositions[2] = value;
                    _robotPulsePositions[2] = _actualRobotPositions[2] * UJointPulsesDegreeRatio;
                    _inMotionArray[2] = _uJointInMotion = (_previousUJointPosition != _actualUJointPosition);
                    _uJointInHome = (_actualUJointPosition == _robotHomePositions[2]);
                }
            }
        }
        public bool UAxisInMotion
        {
            get
            {
                lock (_uAxisLocker)
                {
                    return _uJointInMotion;
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
                    _robotHomePositions[2] = _uJointHomePosition = value;
                }
            }
        }

        public double RAxis
        {
            get
            {
                return _actualRobotPositions[3];
            }
            set
            {
                lock (_rAxisLocker)
                {
                    _previousRJointPosition = _actualRobotPositions[3];
                    _actualRJointPosition = _actualRobotPositions[3] = value;
                    _robotPulsePositions[3] = _actualRobotPositions[3] * RJointPulsesDegreeRatio;
                    _inMotionArray[3] = _rJointInMotion = (_previousRJointPosition != _actualRJointPosition);
                    _rJointInHome = (_actualRJointPosition == _robotHomePositions[3]);
                }
            }
        }
        public bool RAxisInMotion
        {
            get
            {
                lock (_rAxisLocker)
                {
                    return _rJointInMotion;
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
                    _robotHomePositions[3] = _rJointHomePosition = value;
                }
            }
        }

        public double BAxis
        {
            get
            {
                return _actualRobotPositions[4];
            }
            set
            {
                lock (_bAxisLocker)
                {
                    _previousBJointPosition = _actualRobotPositions[4];
                    _actualBJointPosition = _actualRobotPositions[4] = value;
                    _robotPulsePositions[4] = _actualRobotPositions[4] * BJointPulsesDegreeRatio;
                    _inMotionArray[4] = _bJointInMotion = (_previousBJointPosition != _actualBJointPosition);
                    _bJointInHome = (_actualBJointPosition == _robotHomePositions[4]);
                }
            }
        }
        public bool BAxisInMotion
        {
            get
            {
                lock (_bAxisLocker)
                {
                    return _bJointInMotion;
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
                    _robotHomePositions[4] = _bJointHomePosition = value;
                }
            }
        }

        public double TAxis
        {
            get
            {
                return _actualRobotPositions[5];
            }
            set
            {
                lock (_tAxisLocker)
                {
                    _previousTJointPosition = _actualRobotPositions[5];
                    _actualBJointPosition = _actualRobotPositions[5] = value;
                    _robotPulsePositions[5] = _actualRobotPositions[5] * TJointPulsesDegreeRatio;
                    _inMotionArray[5] = _tJointInMotion = (_previousTJointPosition != _actualTJointPosition);
                    _tJointInHome = (_actualTJointPosition == _robotHomePositions[5]);
                }
            }
        }
        public bool TAxisInMotion
        {
            get
            {
                lock (_tAxisLocker)
                {
                    return _tJointInMotion;
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
                    _robotHomePositions[5] = _tJointHomePosition = value;
                }
            }
        }

        public double X
        {
            get
            {
                return _actualRobotPositions[0];
            }
            set
            {
                lock (_xAxisLocker)
                {
                    _actualRobotPositions[0] = value;

                    _inMotionArray[0] = (_previousRobotPositions[0] != _actualRobotPositions[0]);
                    _xAxisInHome = (_actualXAxisPosition == _robotHomePositions[0]);

                    _previousRobotPositions[0] = _actualRobotPositions[0];
                }
            }
        }
        public bool XAxisInMotion
        {
            get
            {
                lock (_xAxisLocker)
                {
                    return _xAxisInMotion;
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
                    _robotHomePositions[0] = _xAxisHomePosition = value;
                }
            }
        }

        public double Y
        {
            get
            {
                return _actualRobotPositions[1];
            }
            set
            {
                _actualRobotPositions[1] = value;
            }
        }
        public double Z
        {
            get
            {
                return _actualRobotPositions[2];
            }
            set
            {
                _actualRobotPositions[2] = value;
            }
        }
        public double Rx
        {
            get
            {
                return _actualRobotPositions[3];
            }
            set
            {
                _actualRobotPositions[3] = value;
            }
        }
        public double Ry
        {
            get
            {
                return _actualRobotPositions[4];
            }
            set
            {
                _actualRobotPositions[4] = value;
            }
        }
        public double Rz
        {
            get
            {
                return _actualRobotPositions[5];
            }
            set
            {
                _actualRobotPositions[5] = value;
            }
        }
        public double E7Axis
        {
            get
            {
                return _actualRobotPositions[6];
            }
            set
            {
                _actualRobotPositions[6] = value;
            }
        }
        public double E8Axis
        {
            get
            {
                return _actualRobotPositions[7];
            }
            set
            {
                _actualRobotPositions[7] = value;
            }
        }
        public double E9Axis
        {
            get
            {
                return _actualRobotPositions[8];
            }
            set
            {
                _actualRobotPositions[8] = value;
            }
        }
        public double E10Axis
        {
            get
            {
                return _actualRobotPositions[9];
            }
            set
            {
                _actualRobotPositions[9] = value;
            }
        }
        public double E11Axis
        {
            get
            {
                return _actualRobotPositions[10];
            }
            set
            {
                _actualRobotPositions[10] = value;
            }
        }
        public double E12Axis
        {
            get
            {
                return _actualRobotPositions[11];
            }
            set
            {
                _actualRobotPositions[11] = value;
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
            }
        }

        public double[] RobotPositions
        {
            get
            {
                lock (_robotPositionsLocker)
                {
                    return _actualRobotPositions;
                }
            }
            set
            {
                lock (_robotPositionsLocker)
                {
                    _actualRobotPositions = value;

                    _actualSJointPosition = _actualXAxisPosition = _actualRobotPositions[0];
                    _actualLJointPosition = _actualYAxisPosition = _actualRobotPositions[1];
                    _actualUJointPosition = _actualZAxisPosition = _actualRobotPositions[2];
                    _actualRJointPosition = _actualRxAxisPosition = _actualRobotPositions[3];
                    _actualBJointPosition = _actualRyAxisPosition = _actualRobotPositions[4];
                    _actualTJointPosition = _actualRzAxisPosition = _actualRobotPositions[5];

                    _inMotionArray[0] = (_previousRobotPositions[0] != _actualRobotPositions[0]);
                    _inMotionArray[1] = (_previousRobotPositions[1] != _actualRobotPositions[1]);
                    _inMotionArray[2] = (_previousRobotPositions[2] != _actualRobotPositions[2]);
                    _inMotionArray[3] = (_previousRobotPositions[3] != _actualRobotPositions[3]);
                    _inMotionArray[4] = (_previousRobotPositions[4] != _actualRobotPositions[4]);
                    _inMotionArray[5] = (_previousRobotPositions[5] != _actualRobotPositions[5]);
                    _inMotionArray[6] = (_previousRobotPositions[6] != _actualRobotPositions[6]);
                    _inMotionArray[7] = (_previousRobotPositions[7] != _actualRobotPositions[7]);
                    _inMotionArray[8] = (_previousRobotPositions[8] != _actualRobotPositions[8]);
                    _inMotionArray[9] = (_previousRobotPositions[9] != _actualRobotPositions[9]);
                    _inMotionArray[10] = (_previousRobotPositions[10] != _actualRobotPositions[10]);
                    _inMotionArray[11] = (_previousRobotPositions[11] != _actualRobotPositions[11]);

                    _sJointInHome = _xAxisInHome = (_actualRobotPositions[0] == _robotHomePositions[0]);
                    _lJointInHome = (_actualRobotPositions[1] == _robotHomePositions[1]);
                    _uJointInHome = (_actualRobotPositions[2] == _robotHomePositions[2]);
                    _rJointInHome = (_actualRobotPositions[3] == _robotHomePositions[3]);
                    _bJointInHome = (_actualRobotPositions[4] == _robotHomePositions[4]);
                    _tJointInHome = (_actualRobotPositions[5] == _robotHomePositions[5]);

                    _previousRobotPositions = _actualRobotPositions;
                }
            }
        }
        public double[] RobotHomePositions
        {
            get
            {
                lock (_robotHomePositionsLocker)
                {
                    return _robotHomePositions;
                }
            }
            set
            {
                lock (_robotHomePositionsLocker)
                {
                    _robotHomePositions = value;
                }
            }
        }
        public double[] RobotParkPositions
        {
            get
            {
                lock (_robotParkPositionsLocker)
                {
                    return _robotParkPositions;
                }
            }
            set
            {
                lock (_robotParkPositionsLocker)
                {
                    _robotParkPositions = value;
                }
            }
        }
        public double[] RobotPulsePositions
        {
            get
            {
                lock (_robotPulsePositionsLocker)
                {
                    return _robotPulsePositions;
                }
            }
            set
            {
                lock (_robotPulsePositionsLocker)
                {
                    _previousSJointPosition = _robotPulsePositions[0];
                    _previousLJointPosition = _robotPulsePositions[1];
                    _previousUJointPosition = _robotPulsePositions[2];
                    _previousRJointPosition = _robotPulsePositions[3];
                    _previousBJointPosition = _robotPulsePositions[4];
                    _previousTJointPosition = _robotPulsePositions[5];

                    _robotPulsePositions = value;

                    _actualSJointPosition = _robotPulsePositions[0];
                    _actualLJointPosition = _robotPulsePositions[1];
                    _actualUJointPosition = _robotPulsePositions[2];
                    _actualRJointPosition = _robotPulsePositions[3];
                    _actualBJointPosition = _robotPulsePositions[4];
                    _actualTJointPosition = _robotPulsePositions[5];

                    _sJointInMotion = (_previousSJointPosition != _actualSJointPosition);
                    _lJointInMotion = (_previousLJointPosition != _actualLJointPosition);
                    _uJointInMotion = (_previousUJointPosition != _actualUJointPosition);
                    _rJointInMotion = (_previousRJointPosition != _actualRJointPosition);
                    _bJointInMotion = (_previousBJointPosition != _actualBJointPosition);
                    _tJointInMotion = (_previousTJointPosition != _actualTJointPosition);
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
