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
        double DesiredSpeed
        {
            get;
            set;
        }

        #region Joints

        double[] JointsSpeedLimits
        {
            get;
            set;
        }

        #region Units

        double[] ActualJointsUnitsPositions
        {
            get;
            set;
        }
        double[] DesiredJointsUnitsPositions
        {
            get;
            set;
        }

        double[] JointsUnitsHomePositions
        {
            get;
            set;
        }
        double[] JointsUnitsParkPositions
        {
            get;
            set;
        }

        [ComVisible(false)]
        double[][] LimitsJointsUnits
        {
            get;
            set;
        }

        double ActualSJointUnitsPosition
        {
            get;
            set;
        }
        bool SJointUnitsInMotion
        {
            get;
        }
        bool SJointUnitsInHome
        {
            get;
        }
        double MinSJointUnitsPosition
        {
            get;
            set;
        }
        double MaxSJointUnitsPosition
        {
            get;
            set;
        }
        double SJointUnitsHomePosition
        {
            get;
            set;
        }

        double ActualLJointUnitsPosition
        {
            get;
            set;
        }
        bool LJointUnitsInMotion
        {
            get;
        }
        bool LJointUnitsInHome
        {
            get;
        }
        double MinLJointUnitsPosition
        {
            get;
            set;
        }
        double MaxLJointUnitsPosition
        {
            get;
            set;
        }
        double LJointUnitsHomePosition
        {
            get;
            set;
        }

        double ActualUJointUnitsPosition
        {
            get;
            set;
        }
        bool UJointUnitsInMotion
        {
            get;
        }
        bool UJointUnitsInHome
        {
            get;
        }
        double MinUJointUnitsPosition
        {
            get;
            set;
        }
        double MaxUJointUnitsPosition
        {
            get;
            set;
        }
        double UJointUnitsHomePosition
        {
            get;
            set;
        }

        double ActualRJointUnitsPosition
        {
            get;
            set;
        }
        bool RJointUnitsInMotion
        {
            get;
        }
        bool RJointUnitsInHome
        {
            get;
        }
        double MinRJointUnitsPosition
        {
            get;
            set;
        }
        double MaxRJointUnitsPosition
        {
            get;
            set;
        }
        double RJointUnitsHomePosition
        {
            get;
            set;
        }

        double ActualBJointUnitsPosition
        {
            get;
            set;
        }
        bool BJointUnitsInMotion
        {
            get;
        }
        bool BJointUnitsInHome
        {
            get;
        }
        double MinBJointUnitsPosition
        {
            get;
            set;
        }
        double MaxBJointUnitsPosition
        {
            get;
            set;
        }
        double BJointUnitsHomePosition
        {
            get;
            set;
        }

        double ActualTJointUnitsPosition
        {
            get;
            set;
        }
        bool TJointUnitsInMotion
        {
            get;
        }
        bool TJointUnitsInHome
        {
            get;
        }
        double MinTJointUnitsPosition
        {
            get;
            set;
        }
        double MaxTJointUnitsPosition
        {
            get;
            set;
        }
        double TJointUnitsHomePosition
        {
            get;
            set;
        }

        double ActualE7AxisUnitsPosition
        {
            get;
            set;
        }
        bool E7AxisUnitsInMotion
        {
            get;
        }
        bool E7AxisUnitsInHome
        {
            get;
        }
        double MinE7AxisUnitsPosition
        {
            get;
            set;
        }
        double MaxE7AxisUnitsPosition
        {
            get;
            set;
        }
        double E7AxisUnitsHomePosition
        {
            get;
            set;
        }

        double ActualE8AxisUnitsPosition
        {
            get;
            set;
        }
        bool E8AxisUnitsInMotion
        {
            get;
        }
        bool E8AxisUnitsInHome
        {
            get;
        }
        double MinE8AxisUnitsPosition
        {
            get;
            set;
        }
        double MaxE8AxisUnitsPosition
        {
            get;
            set;
        }
        double E8AxisUnitsHomePosition
        {
            get;
            set;
        }

        double ActualE9AxisUnitsPosition
        {
            get;
            set;
        }
        bool E9AxisUnitsInMotion
        {
            get;
        }
        bool E9AxisUnitsInHome
        {
            get;
        }
        double MinE9AxisUnitsPosition
        {
            get;
            set;
        }
        double MaxE9AxisUnitsPosition
        {
            get;
            set;
        }
        double E9AxisUnitsHomePosition
        {
            get;
            set;
        }

        double ActualE10AxisUnitsPosition
        {
            get;
            set;
        }
        bool E10AxisUnitsInMotion
        {
            get;
        }
        bool E10AxisUnitsInHome
        {
            get;
        }
        double MinE10AxisUnitsPosition
        {
            get;
            set;
        }
        double MaxE10AxisUnitsPosition
        {
            get;
            set;
        }
        double E10AxisUnitsHomePosition
        {
            get;
            set;
        }

        double ActualE11AxisUnitsPosition
        {
            get;
            set;
        }
        bool E11AxisUnitsInMotion
        {
            get;
        }
        bool E11AxisUnitsInHome
        {
            get;
        }
        double MinE11AxisUnitsPosition
        {
            get;
            set;
        }
        double MaxE11AxisUnitsPosition
        {
            get;
            set;
        }
        double E11AxisUnitsHomePosition
        {
            get;
            set;
        }

        double ActualE12AxisUnitsPosition
        {
            get;
            set;
        }
        bool E12AxisUnitsInMotion
        {
            get;
        }
        bool E12AxisUnitsInHome
        {
            get;
        }
        double MinE12AxisUnitsPosition
        {
            get;
            set;
        }
        double MaxE12AxisUnitsPosition
        {
            get;
            set;
        }
        double E12AxisUnitsHomePosition
        {
            get;
            set;
        }

        #endregion

        #region Pulse

        double[] ActualJointsPulsePositions
        {
            get;
            set;
        }
        double[] DesiredJointsPulsePositions
        {
            get;
            set;
        }

        double[] JointsPulseHomePositions
        {
            get;
            set;
        }
        double[] JointsPulseParkPositions
        {
            get;
            set;
        }

        [ComVisible(false)]
        double[][] LimitsJointsPulse
        {
            get;
            set;
        }

        double ActualSJointPulsePosition
        {
            get;
            set;
        }
        bool SJointPulseInMotion
        {
            get;
        }
        bool SJointPulseInHome
        {
            get;
        }
        double MinSJointPulsePosition
        {
            get;
            set;
        }
        double MaxSJointPulsePosition
        {
            get;
            set;
        }
        double SJointPulseHomePosition
        {
            get;
            set;
        }

        double ActualLJointPulsePosition
        {
            get;
            set;
        }
        bool LJointPulseInMotion
        {
            get;
        }
        bool LJointPulseInHome
        {
            get;
        }
        double MinLJointPulsePosition
        {
            get;
            set;
        }
        double MaxLJointPulsePosition
        {
            get;
            set;
        }
        double LJointPulseHomePosition
        {
            get;
            set;
        }

        double ActualUJointPulsePosition
        {
            get;
            set;
        }
        bool UJointPulseInMotion
        {
            get;
        }
        bool UJointPulseInHome
        {
            get;
        }
        double MinUJointPulsePosition
        {
            get;
            set;
        }
        double MaxUJointPulsePosition
        {
            get;
            set;
        }
        double UJointPulseHomePosition
        {
            get;
            set;
        }

        double ActualRJointPulsePosition
        {
            get;
            set;
        }
        bool RJointPulseInMotion
        {
            get;
        }
        bool RJointPulseInHome
        {
            get;
        }
        double MinRJointPulsePosition
        {
            get;
            set;
        }
        double MaxRJointPulsePosition
        {
            get;
            set;
        }
        double RJointPulseHomePosition
        {
            get;
            set;
        }

        double ActualBJointPulsePosition
        {
            get;
            set;
        }
        bool BJointPulseInMotion
        {
            get;
        }
        bool BJointPulseInHome
        {
            get;
        }
        double MinBJointPulsePosition
        {
            get;
            set;
        }
        double MaxBJointPulsePosition
        {
            get;
            set;
        }
        double BJointPulseHomePosition
        {
            get;
            set;
        }

        double ActualTJointPulsePosition
        {
            get;
            set;
        }
        bool TJointPulseInMotion
        {
            get;
        }
        bool TJointPulseInHome
        {
            get;
        }
        double MinTJointPulsePosition
        {
            get;
            set;
        }
        double MaxTJointPulsePosition
        {
            get;
            set;
        }
        double TJointPulseHomePosition
        {
            get;
            set;
        }

        double ActualE7AxisPulsePosition
        {
            get;
            set;
        }
        bool E7AxisPulseInMotion
        {
            get;
        }
        bool E7AxisPulseInHome
        {
            get;
        }
        double MinE7AxisPulsePosition
        {
            get;
            set;
        }
        double MaxE7AxisPulsePosition
        {
            get;
            set;
        }
        double E7AxisPulseHomePosition
        {
            get;
            set;
        }

        double ActualE8AxisPulsePosition
        {
            get;
            set;
        }
        bool E8AxisPulseInMotion
        {
            get;
        }
        bool E8AxisPulseInHome
        {
            get;
        }
        double MinE8AxisPulsePosition
        {
            get;
            set;
        }
        double MaxE8AxisPulsePosition
        {
            get;
            set;
        }
        double E8AxisPulseHomePosition
        {
            get;
            set;
        }

        double ActualE9AxisPulsePosition
        {
            get;
            set;
        }
        bool E9AxisPulseInMotion
        {
            get;
        }
        bool E9AxisPulseInHome
        {
            get;
        }
        double MinE9AxisPulsePosition
        {
            get;
            set;
        }
        double MaxE9AxisPulsePosition
        {
            get;
            set;
        }
        double E9AxisPulseHomePosition
        {
            get;
            set;
        }

        double ActualE10AxisPulsePosition
        {
            get;
            set;
        }
        bool E10AxisPulseInMotion
        {
            get;
        }
        bool E10AxisPulseInHome
        {
            get;
        }
        double MinE10AxisPulsePosition
        {
            get;
            set;
        }
        double MaxE10AxisPulsePosition
        {
            get;
            set;
        }
        double E10AxisPulseHomePosition
        {
            get;
            set;
        }

        double ActualE11AxisPulsePosition
        {
            get;
            set;
        }
        bool E11AxisPulseInMotion
        {
            get;
        }
        bool E11AxisPulseInHome
        {
            get;
        }
        double MinE11AxisPulsePosition
        {
            get;
            set;
        }
        double MaxE11AxisPulsePosition
        {
            get;
            set;
        }
        double E11AxisPulseHomePosition
        {
            get;
            set;
        }

        double ActualE12AxisPulsePosition
        {
            get;
            set;
        }
        bool E12AxisPulseInMotion
        {
            get;
        }
        bool E12AxisPulseInHome
        {
            get;
        }
        double MinE12AxisPulsePosition
        {
            get;
            set;
        }
        double MaxE12AxisPulsePosition
        {
            get;
            set;
        }
        double E12AxisPulseHomePosition
        {
            get;
            set;
        }

        #endregion

        #endregion

        #region TCP

        double[] ActualTCPPositions
        {
            get;
            set;
        }
        double[] TCPHomePositions
        {
            get;
            set;
        }
        double[] TCPParkPositions
        {
            get;
            set;
        }

        [ComVisible(false)]
        double[][] LimitsTCP
        {
            get;
            set;
        }
        bool[] TCPInMotionArray
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
        bool YAxisInMotion
        {
            get;
        }
        bool YAxisInHome
        {
            get;
        }
        double MinYAxisPosition
        {
            get;
        }
        double MaxYAxisPosition
        {
            get;
        }
        double YAxisHomePosition
        {
            get;
            set;
        }

        double Z
        {
            get;
            set;
        }
        bool ZAxisInMotion
        {
            get;
        }
        bool ZAxisInHome
        {
            get;
        }
        double MinZAxisPosition
        {
            get;
        }
        double MaxZAxisPosition
        {
            get;
        }
        double ZAxisHomePosition
        {
            get;
            set;
        }

        double Rx
        {
            get;
            set;
        }
        bool RxAxisInMotion
        {
            get;
        }
        bool RxAxisInHome
        {
            get;
        }
        double MinRxAxisPosition
        {
            get;
        }
        double MaxRxAxisPosition
        {
            get;
        }
        double RxAxisHomePosition
        {
            get;
            set;
        }

        double Ry
        {
            get;
            set;
        }
        bool RyAxisInMotion
        {
            get;
        }
        bool RyAxisInHome
        {
            get;
        }
        double MinRyAxisPosition
        {
            get;
        }
        double MaxRyAxisPosition
        {
            get;
        }
        double RyAxisHomePosition
        {
            get;
            set;
        }

        double Rz
        {
            get;
            set;
        }
        bool RzAxisInMotion
        {
            get;
        }
        bool RzAxisInHome
        {
            get;
        }
        double MinRzAxisPosition
        {
            get;
        }
        double MaxRzAxisPosition
        {
            get;
        }
        double RzAxisHomePosition
        {
            get;
            set;
        }

        double E7Axis
        {
            get;
            set;
        }
        bool E7AxisInMotion
        {
            get;
        }
        bool E7AxisInHome
        {
            get;
        }
        double MinE7AxisPosition
        {
            get;
        }
        double MaxE7AxisPosition
        {
            get;
        }
        double E7AxisHomePosition
        {
            get;
            set;
        }

        double E8Axis
        {
            get;
            set;
        }
        bool E8AxisInMotion
        {
            get;
        }
        bool E8AxisInHome
        {
            get;
        }
        double MinE8AxisPosition
        {
            get;
        }
        double MaxE8AxisPosition
        {
            get;
        }
        double E8AxisHomePosition
        {
            get;
            set;
        }

        double E9Axis
        {
            get;
            set;
        }
        bool E9AxisInMotion
        {
            get;
        }
        bool E9AxisInHome
        {
            get;
        }
        double MinE9AxisPosition
        {
            get;
        }
        double MaxE9AxisPosition
        {
            get;
        }
        double E9AxisHomePosition
        {
            get;
            set;
        }

        double E10Axis
        {
            get;
            set;
        }
        bool E10AxisInMotion
        {
            get;
        }
        bool E10AxisInHome
        {
            get;
        }
        double MinE10AxisPosition
        {
            get;
        }
        double MaxE10AxisPosition
        {
            get;
        }
        double E10AxisHomePosition
        {
            get;
            set;
        }

        double E11Axis
        {
            get;
            set;
        }
        bool E11AxisInMotion
        {
            get;
        }
        bool E11AxisInHome
        {
            get;
        }
        double MinE11AxisPosition
        {
            get;
        }
        double MaxE11AxisPosition
        {
            get;
        }
        double E11AxisHomePosition
        {
            get;
            set;
        }

        double E12Axis
        {
            get;
            set;
        }
        bool E12AxisInMotion
        {
            get;
        }
        bool E12AxisInHome
        {
            get;
        }
        double MinE12AxisPosition
        {
            get;
        }
        double MaxE12AxisPosition
        {
            get;
        }
        double E12AxisHomePosition
        {
            get;
            set;
        }
        #endregion

        #endregion

        #region Methods

        #endregion
    }
    [ComVisible(true)]
    public class RobotPosition : IRobotPosition
    {
        #region Constants

        const double SJointRatio = 1341.380117;
        const double LJointRatio = 1907.674074;//-
        const double UJointRatio = 1592.888889;
        const double RJointRatio = 1022.862222;//-
        const double BJointRatio = 986.074074;
        const double TJointRatio = 631.299346;//-
        const double E7AxisRatio = 159.2781191;//Linear tracker
        const double E8AxisRatio = 11.378;//Turn table
        const double E9AxisRatio = 11.378; //Turn table
        const double E10AxisRatio = 1.0;
        const double E11AxisRatio = 1.0;
        const double E12AxisRatio = 1.0;

        #endregion

        #region Fields

        private double _actualSpeed = 0.0;
        private double _desiredSpeed = 0.0;

        #region Joints

        private bool[] _jointsInMotionArray = new bool[12];
        private bool[] _jointsInHomeArray = new bool[12];

        private double[] _jointsSpeedLimits = new double[12] { 50.0, 197.0, 190.0, 210.0, 410.0, 410.0, 620.0, 0, 0, 0, 0, 0 }; //MH24 datasheet

        volatile double[] _actualJointsUnitsPositions = new double[12];
        volatile double[] _actualJointsPulsePositions = new double[12];

        volatile double[] _desiredJointsUnitsPositions = new double[12];
        volatile double[] _desiredJointsPulsePositions = new double[12];

        volatile double[] _homeJointsUnitsPositions = new double[12];
        volatile double[] _parkJointsUnitsPositions = new double[12];

        volatile double[] _homeJointsPulsePositions = new double[12];
        volatile double[] _parkJointsPulsePositions = new double[12];

        #region Units

        readonly object _sAxisUnitsLocker = new object();
        readonly object _lAxisUnitsLocker = new object();
        readonly object _uAxisUnitsLocker = new object();
        readonly object _rAxisUnitsLocker = new object();
        readonly object _bAxisUnitsLocker = new object();
        readonly object _tAxisUnitsLocker = new object();
        readonly object _e7AxisUnitsLocker = new object();
        readonly object _e8AxisUnitsLocker = new object();
        readonly object _e9AxisUnitsLocker = new object();
        readonly object _e10AxisUnitsLocker = new object();
        readonly object _e11AxisUnitsLocker = new object();
        readonly object _e12AxisUnitsLocker = new object();

        #endregion

        #region Pulses

        readonly object _sAxisPulseLocker = new object();
        readonly object _lAxisPulseLocker = new object();
        readonly object _uAxisPulseLocker = new object();
        readonly object _rAxisPulseLocker = new object();
        readonly object _bAxisPulseLocker = new object();
        readonly object _tAxisPulseLocker = new object();
        readonly object _e7AxisPulseLocker = new object();
        readonly object _e8AxisPulseLocker = new object();
        readonly object _e9AxisPulseLocker = new object();
        readonly object _e10AxisPulseLocker = new object();
        readonly object _e11AxisPulseLocker = new object();
        readonly object _e12AxisPulseLocker = new object();

        #endregion

        readonly object _actualJointsUnitsPositionsLocker = new object();
        readonly object _actualJointsPulsePositionsLocker = new object();

        readonly object _desiredJointsUnitsPositionsLocker = new object();
        readonly object _desiredJointsPulsePositionsLocker = new object();

        readonly object _parkJointsUnitsPositionsLocker = new object();
        readonly object _parkJointsPulsePositionsLocker = new object();

        readonly object _homeJointsUnitsPositionsLocker = new object();
        readonly object _homeJointsPulsePositionsLocker = new object();

        #region Units

        private bool _sJointUnitsInHome = false;
        private double _actualSJointUnitsPosition = 0.0;
        private double _minSJointUnitsPosition = -180.0;
        private double _maxSJointUnitsPosition = 180.0;
        private double _sJointUnitsHomePosition = 0.0;

        private bool _lJointUnitsInHome = false;
        private double _actualLJointUnitsPosition = 0.0;
        private double _minLJointUnitsPosition = -105.0;
        private double _maxLJointUnitsPosition = 155.0;
        private double _lJointUnitsHomePosition = 0.0;

        private bool _uJointUnitsInHome = false;
        private double _actualUJointUnitsPosition = 0.0;
        private double _minUJointUnitsPosition = -170.0;
        private double _maxUJointUnitsPosition = 240.0;
        private double _uJointUnitsHomePosition = 0.0;

        private bool _rJointUnitsInHome = false;
        private double _actualRJointUnitsPosition = 0.0;
        private double _minRJointUnitsPosition = -200.0;
        private double _maxRJointUnitsPosition = 200.0;
        private double _rJointUnitsHomePosition = 0.0;

        private bool _bJointUnitsInHome = false;
        private double _actualBJointUnitsPosition = 0.0;
        private double _minBJointUnitsPosition = -150.0;
        private double _maxBJointUnitsPosition = 150.0;
        private double _bJointUnitsHomePosition = 0.0;

        private bool _tJointUnitsInHome = false;
        private double _actualTJointUnitsPosition = 0.0;
        private double _minTJointUnitsPosition = -150.0;
        private double _maxTJointUnitsPosition = 150.0;
        private double _tJointUnitsHomePosition = 0.0;

        private bool _e7AxisUnitsInHome = false;
        private double _actualE7AxisUnitsPosition = 0.0;
        private double _minE7AxisUnitsPosition = 0.0;
        private double _maxE7AxisUnitsPosition = 4000.0;
        private double _e7AxisUnitsHomePosition = 0.0;

        private bool _e8AxisUnitsInHome = false;
        private double _actualE8AxisUnitsPosition = 0.0;
        private double _minE8AxisUnitsPosition = -360.0;
        private double _maxE8AxisUnitsPosition = 360.0;
        private double _e8AxisUnitsHomePosition = 0.0;

        private bool _e9AxisUnitsInHome = false;
        private double _actualE9AxisUnitsPosition = 0.0;
        private double _minE9AxisUnitsPosition = -360.0;
        private double _maxE9AxisUnitsPosition = 360.0;
        private double _e9AxisUnitsHomePosition = 0.0;

        private bool _e10AxisUnitsInHome = false;
        private double _actualE10AxisUnitsPosition = 0.0;
        private double _minE10AxisUnitsPosition = 0.0;
        private double _maxE10AxisUnitsPosition = 0.0;
        private double _e10AxisUnitsHomePosition = 0.0;

        private bool _e11AxisUnitsInHome = false;
        private double _actualE11AxisUnitsPosition = 0.0;
        private double _minE11AxisUnitsPosition = 0.0;
        private double _maxE11AxisUnitsPosition = 0.0;
        private double _e11AxisUnitsHomePosition = 0.0;

        private bool _e12AxisUnitsInHome = false;
        private double _actualE12AxisUnitsPosition = 0.0;
        private double _minE12AxisUnitsPosition = 0.0;
        private double _maxE12AxisUnitsPosition = 0.0;
        private double _e12AxisUnitsHomePosition = 0.0;

        #endregion

        #region Pulses

        private bool _sJointPulseInHome = false;
        private double _actualSJointPulsePosition = 0.0;
        private double _minSJointPulsePosition = 0.0;
        private double _maxSJointPulsePosition = 0.0;
        private double _sJointPulseHomePosition = 0.0;

        private bool _lJointPulseInHome = false;
        private double _actualLJointPulsePosition = 0.0;
        private double _minLJointPulsePosition = 0.0;
        private double _maxLJointPulsePosition = 0.0;
        private double _lJointPulseHomePosition = 0.0;

        private bool _uJointPulseInHome = false;
        private double _actualUJointPulsePosition = 0.0;
        private double _minUJointPulsePosition = 0.0;
        private double _maxUJointPulsePosition = 0.0;
        private double _uJointPulseHomePosition = 0.0;

        private bool _rJointPulseInHome = false;
        private double _actualRJointPulsePosition = 0.0;
        private double _minRJointPulsePosition = 0.0;
        private double _maxRJointPulsePosition = 0.0;
        private double _rJointPulseHomePosition = 0.0;

        private bool _bJointPulseInHome = false;
        private double _actualBJointPulsePosition = 0.0;
        private double _minBJointPulsePosition = 0.0;
        private double _maxBJointPulsePosition = 0.0;
        private double _bJointPulseHomePosition = 0.0;

        private bool _tJointPulseInHome = false;
        private double _actualTJointPulsePosition = 0.0;
        private double _minTJointPulsePosition = 0.0;
        private double _maxTJointPulsePosition = 0.0;
        private double _tJointPulseHomePosition = 0.0;

        private bool _e7AxisPulseInHome = false;
        private double _actualE7AxisPulsePosition = 0.0;
        private double _minE7AxisPulsePosition = 0.0;
        private double _maxE7AxisPulsePosition = 0.0;
        private double _e7AxisPulseHomePosition = 0.0;

        private bool _e8AxisPulseInHome = false;
        private double _actualE8AxisPulsePosition = 0.0;
        private double _minE8AxisPulsePosition = 0.0;
        private double _maxE8AxisPulsePosition = 0.0;
        private double _e8AxisPulseHomePosition = 0.0;

        private bool _e9AxisPulseInHome = false;
        private double _actualE9AxisPulsePosition = 0.0;
        private double _minE9AxisPulsePosition = 0.0;
        private double _maxE9AxisPulsePosition = 0.0;
        private double _e9AxisPulseHomePosition = 0.0;

        private bool _e10AxisPulseInHome = false;
        private double _actualE10AxisPulsePosition = 0.0;
        private double _minE10AxisPulsePosition = 0.0;
        private double _maxE10AxisPulsePosition = 0.0;
        private double _e10AxisPulseHomePosition = 0.0;

        private bool _e11AxisPulseInHome = false;
        private double _actualE11AxisPulsePosition = 0.0;
        private double _minE11AxisPulsePosition = 0.0;
        private double _maxE11AxisPulsePosition = 0.0;
        private double _e11AxisPulseHomePosition = 0.0;

        private bool _e12AxisPulseInHome = false;
        private double _actualE12AxisPulsePosition = 0.0;
        private double _minE12AxisPulsePosition = 0.0;
        private double _maxE12AxisPulsePosition = 0.0;
        private double _e12AxisPulseHomePosition = 0.0;

        #endregion

        private double[][] _limitsJointsUnits = new double[12][]
          {
               #region
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
	           #endregion
          };

        private double[][] _limitsJointsPulse = new double[12][]
          {
               #region
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
	            #endregion
          };

        #endregion

        #region TCP

        private bool[] _tcpInMotionArray = new bool[12];
        private double[] _tcpSpeedLimits = new double[12] { 1000.0, 1000.0, 1000.0, 1000.0, 1000.0, 1000.0, 1000.0, 1000.0, 1000.0, 1000.0, 1000.0, 1000.0 }; //MH24 datasheet

        volatile double[] _actualTCPPositions = new double[12];
        volatile double[] _desiredTCPPositions = new double[12];

        volatile double[] _homeTCPPositions = new double[12];
        volatile double[] _parkTCPPositions = new double[12];

        readonly object _xAxisLocker = new object();
        readonly object _yAxisLocker = new object();
        readonly object _zAxisLocker = new object();
        readonly object _rxAxisLocker = new object();
        readonly object _ryAxisLocker = new object();
        readonly object _rzAxisLocker = new object();
        readonly object _e7AxisLocker = new object();
        readonly object _e8AxisLocker = new object();
        readonly object _e9AxisLocker = new object();
        readonly object _e10AxisLocker = new object();
        readonly object _e11AxisLocker = new object();
        readonly object _e12AxisLocker = new object();

        readonly object _actualTCPPositionsLocker = new object();
        readonly object _desiredTCPPositionsLocker = new object();
        readonly object _parkTCPPositionsLocker = new object();
        readonly object _homeTCPPositionsLocker = new object();

        private bool _xAxisInHome = false;
        private double _actualXAxisPosition = 0.0;
        private double _minXAxisPosition = -1500.0;
        private double _maxXAxisPosition = 1500.0;
        private double _xAxisHomePosition = 0.0;

        private bool _yAxisInHome = false;
        private double _actualYAxisPosition = 0.0;
        private double _minYAxisPosition = -1500.0;
        private double _maxYAxisPosition = 1500.0;
        private double _yAxisHomePosition = 0.0;

        private bool _zAxisInHome = false;
        private double _actualZAxisPosition = 0.0;
        private double _minZAxisPosition = -1500.0;
        private double _maxZAxisPosition = 1500.0;
        private double _zAxisHomePosition = 0.0;

        private bool _rxAxisInHome = false;
        private double _actualRxAxisPosition = 0.0;
        private double _minRxAxisPosition = -180.0;
        private double _maxRxAxisPosition = 180.0;
        private double _rxAxisHomePosition = 0.0;

        private bool _ryAxisInHome = false;
        private double _actualRyAxisPosition = 0.0;
        private double _minRyAxisPosition = -180.0;
        private double _maxRyAxisPosition = 180.0;
        private double _ryAxisHomePosition = 0.0;

        private bool _rzAxisInHome = false;
        private double _actualRzAxisPosition = 0.0;
        private double _minRzAxisPosition = -180.0;
        private double _maxRzAxisPosition = 180.0;
        private double _rzAxisHomePosition = 0.0;

        private bool _e7AxisInHome = false;
        private double _actualE7AxisPosition = 0.0;
        private double _minE7AxisPosition = 0.0;
        private double _maxE7AxisPosition = 4000.0;
        private double _e7AxisHomePosition = 0.0;

        private bool _e8AxisInHome = false;
        private double _actualE8AxisPosition = 0.0;
        private double _minE8AxisPosition = -360.0;
        private double _maxE8AxisPosition = 360.0;
        private double _e8AxisHomePosition = 0.0;

        private bool _e9AxisInHome = false;
        private double _actualE9AxisPosition = 0.0;
        private double _minE9AxisPosition = -360.0;
        private double _maxE9AxisPosition = 360.0;
        private double _e9AxisHomePosition = 0.0;

        private bool _e10AxisInHome = false;
        private double _actualE10AxisPosition = 0.0;
        private double _minE10AxisPosition = 0.0;
        private double _maxE10AxisPosition = 0.0;
        private double _e10AxisHomePosition = 0.0;

        private bool _e11AxisInHome = false;
        private double _actualE11AxisPosition = 0.0;
        private double _minE11AxisPosition = 0.0;
        private double _maxE11AxisPosition = 0.0;
        private double _e11AxisHomePosition = 0.0;

        private bool _e12AxisInHome = false;
        private double _actualE12AxisPosition = 0.0;
        private double _minE12AxisPosition = 0.0;
        private double _maxE12AxisPosition = 0.0;
        private double _e12AxisHomePosition = 0.0;

        private double[][] _limitsTCP = new double[12][]
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

        #endregion

        #endregion

        #region Properties

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
        public double DesiredSpeed
        {
            get
            {
                return _desiredSpeed;
            }
            set
            {
                _desiredSpeed = value;
            }
        }

        #region Joints

        public double[] JointsSpeedLimits
        {
            get
            {
                return _jointsSpeedLimits;
            }
            set
            {
                _jointsSpeedLimits = value;
            }
        }
        public bool[] JointsInMotionArray
        {
            get
            {
                return _jointsInMotionArray;
            }
            set
            {
                _jointsInMotionArray = value;
            }
        }

        #region Units

        public double[] ActualJointsUnitsPositions
        {
            get
            {
                lock (_actualJointsUnitsPositionsLocker)
                {
                    return _actualJointsUnitsPositions;
                }
            }
            set
            {
                lock (_actualJointsUnitsPositionsLocker)
                {
                    #region
                    _jointsInMotionArray[0] = (_actualJointsUnitsPositions[0] != value[0]);
                    _jointsInMotionArray[1] = (_actualJointsUnitsPositions[1] != value[1]);
                    _jointsInMotionArray[2] = (_actualJointsUnitsPositions[2] != value[2]);
                    _jointsInMotionArray[3] = (_actualJointsUnitsPositions[3] != value[3]);
                    _jointsInMotionArray[4] = (_actualJointsUnitsPositions[4] != value[4]);
                    _jointsInMotionArray[5] = (_actualJointsUnitsPositions[5] != value[5]);
                    _jointsInMotionArray[6] = (_actualJointsUnitsPositions[6] != value[6]);
                    _jointsInMotionArray[7] = (_actualJointsUnitsPositions[7] != value[7]);
                    _jointsInMotionArray[8] = (_actualJointsUnitsPositions[8] != value[8]);
                    _jointsInMotionArray[9] = (_actualJointsUnitsPositions[9] != value[9]);
                    _jointsInMotionArray[10] = (_actualJointsUnitsPositions[10] != value[10]);
                    _jointsInMotionArray[11] = (_actualJointsUnitsPositions[11] != value[11]);

                    _actualJointsUnitsPositions = value;

                    _actualSJointUnitsPosition = _actualJointsUnitsPositions[0];
                    _actualLJointUnitsPosition = _actualJointsUnitsPositions[1];
                    _actualUJointUnitsPosition = _actualJointsUnitsPositions[2];
                    _actualRJointUnitsPosition = _actualJointsUnitsPositions[3];
                    _actualBJointUnitsPosition = _actualJointsUnitsPositions[4];
                    _actualTJointUnitsPosition = _actualJointsUnitsPositions[5];
                    _actualE7AxisUnitsPosition = _actualJointsUnitsPositions[6];
                    _actualE8AxisUnitsPosition = _actualJointsUnitsPositions[7];
                    _actualE9AxisUnitsPosition = _actualJointsUnitsPositions[8];
                    _actualE10AxisUnitsPosition = _actualJointsUnitsPositions[9];
                    _actualE11AxisUnitsPosition = _actualJointsUnitsPositions[10];
                    _actualE12AxisUnitsPosition = _actualJointsUnitsPositions[11];

                    _actualSJointPulsePosition = _actualJointsPulsePositions[0] = _actualJointsUnitsPositions[0] * SJointRatio;
                    _actualLJointPulsePosition = _actualJointsPulsePositions[1] = _actualJointsUnitsPositions[1] * LJointRatio;
                    _actualUJointPulsePosition = _actualJointsPulsePositions[2] = _actualJointsUnitsPositions[2] * UJointRatio;
                    _actualRJointPulsePosition = _actualJointsPulsePositions[3] = _actualJointsUnitsPositions[3] * RJointRatio;
                    _actualBJointPulsePosition = _actualJointsPulsePositions[4] = _actualJointsUnitsPositions[4] * BJointRatio;
                    _actualTJointPulsePosition = _actualJointsPulsePositions[5] = _actualJointsUnitsPositions[5] * TJointRatio;
                    _actualE7AxisPulsePosition = _actualJointsPulsePositions[6] = _actualJointsUnitsPositions[6] * E7AxisRatio;
                    _actualE8AxisPulsePosition = _actualJointsPulsePositions[7] = _actualJointsUnitsPositions[7] * E8AxisRatio;
                    _actualE9AxisPulsePosition = _actualJointsPulsePositions[8] = _actualJointsUnitsPositions[8] * E9AxisRatio;
                    _actualE10AxisPulsePosition = _actualJointsPulsePositions[9] = _actualJointsUnitsPositions[9] * E10AxisRatio;
                    _actualE11AxisPulsePosition = _actualJointsPulsePositions[10] = _actualJointsUnitsPositions[10] * E11AxisRatio;
                    _actualE12AxisPulsePosition = _actualJointsPulsePositions[11] = _actualJointsUnitsPositions[11] * E12AxisRatio;

                    _sJointUnitsInHome = (_actualJointsUnitsPositions[0] == _homeJointsUnitsPositions[0]);
                    _lJointUnitsInHome = (_actualJointsUnitsPositions[1] == _homeJointsUnitsPositions[1]);
                    _uJointUnitsInHome = (_actualJointsUnitsPositions[2] == _homeJointsUnitsPositions[2]);
                    _rJointUnitsInHome = (_actualJointsUnitsPositions[3] == _homeJointsUnitsPositions[3]);
                    _bJointUnitsInHome = (_actualJointsUnitsPositions[4] == _homeJointsUnitsPositions[4]);
                    _tJointUnitsInHome = (_actualJointsUnitsPositions[5] == _homeJointsUnitsPositions[5]);
                    _e7AxisUnitsInHome = (_actualJointsUnitsPositions[6] == _homeJointsUnitsPositions[6]);
                    _e8AxisUnitsInHome = (_actualJointsUnitsPositions[7] == _homeJointsUnitsPositions[7]);
                    _e9AxisUnitsInHome = (_actualJointsUnitsPositions[8] == _homeJointsUnitsPositions[8]);
                    _e10AxisUnitsInHome = (_actualJointsUnitsPositions[9] == _homeJointsUnitsPositions[9]);
                    _e11AxisUnitsInHome = (_actualJointsUnitsPositions[10] == _homeJointsUnitsPositions[10]);
                    _e12AxisUnitsInHome = (_actualJointsUnitsPositions[11] == _homeJointsUnitsPositions[11]);
                    #endregion
                }
            }
        }
        public double[] DesiredJointsUnitsPositions
        {
            get
            {
                lock (_desiredJointsUnitsPositionsLocker)
                {
                    return _desiredJointsUnitsPositions;
                }
            }
            set
            {
                lock (_desiredJointsUnitsPositionsLocker)
                {
                    _desiredJointsUnitsPositions = value;

                    _desiredJointsPulsePositions[0] = _desiredJointsUnitsPositions[0] * SJointRatio;
                    _desiredJointsPulsePositions[1] = _desiredJointsUnitsPositions[1] * LJointRatio;
                    _desiredJointsPulsePositions[2] = _desiredJointsUnitsPositions[2] * UJointRatio;
                    _desiredJointsPulsePositions[3] = _desiredJointsUnitsPositions[3] * RJointRatio;
                    _desiredJointsPulsePositions[4] = _desiredJointsUnitsPositions[4] * BJointRatio;
                    _desiredJointsPulsePositions[5] = _desiredJointsUnitsPositions[5] * TJointRatio;
                    _desiredJointsPulsePositions[6] = _desiredJointsUnitsPositions[6] * E7AxisRatio;
                    _desiredJointsPulsePositions[7] = _desiredJointsUnitsPositions[7] * E8AxisRatio;
                    _desiredJointsPulsePositions[8] = _desiredJointsUnitsPositions[8] * E9AxisRatio;
                    _desiredJointsPulsePositions[9] = _desiredJointsUnitsPositions[9] * E10AxisRatio;
                    _desiredJointsPulsePositions[10] = _desiredJointsUnitsPositions[10] * E11AxisRatio;
                    _desiredJointsPulsePositions[11] = _desiredJointsUnitsPositions[11] * E12AxisRatio;
                }
            }
        }

        public double[] JointsUnitsHomePositions
        {
            get
            {
                lock (_homeJointsUnitsPositionsLocker)
                {
                    return _homeJointsUnitsPositions;
                }
            }
            set
            {
                lock (_homeJointsUnitsPositionsLocker)
                {
                    #region
                    _homeJointsUnitsPositions = value;

                    _homeJointsPulsePositions[0] = _homeJointsUnitsPositions[0] * SJointRatio;
                    _homeJointsPulsePositions[1] = _homeJointsUnitsPositions[1] * LJointRatio;
                    _homeJointsPulsePositions[2] = _homeJointsUnitsPositions[2] * UJointRatio;
                    _homeJointsPulsePositions[3] = _homeJointsUnitsPositions[3] * RJointRatio;
                    _homeJointsPulsePositions[4] = _homeJointsUnitsPositions[4] * BJointRatio;
                    _homeJointsPulsePositions[5] = _homeJointsUnitsPositions[5] * TJointRatio;
                    _homeJointsPulsePositions[6] = _homeJointsUnitsPositions[6] * E7AxisRatio;
                    _homeJointsPulsePositions[7] = _homeJointsUnitsPositions[7] * E8AxisRatio;
                    _homeJointsPulsePositions[8] = _homeJointsUnitsPositions[8] * E9AxisRatio;
                    _homeJointsPulsePositions[9] = _homeJointsUnitsPositions[9] * E10AxisRatio;
                    _homeJointsPulsePositions[10] = _homeJointsUnitsPositions[10] * E11AxisRatio;
                    _homeJointsPulsePositions[11] = _homeJointsUnitsPositions[11] * E12AxisRatio;
                    #endregion
                }
            }
        }
        public double[] JointsUnitsParkPositions
        {
            get
            {
                lock (_parkJointsUnitsPositionsLocker)
                {
                    return _parkJointsUnitsPositions;
                }
            }
            set
            {
                lock (_parkJointsUnitsPositionsLocker)
                {
                    #region
                    _parkJointsUnitsPositions = value;

                    _parkJointsPulsePositions[0] = _parkJointsUnitsPositions[0] * SJointRatio;
                    _parkJointsPulsePositions[1] = _parkJointsUnitsPositions[1] * LJointRatio;
                    _parkJointsPulsePositions[2] = _parkJointsUnitsPositions[2] * UJointRatio;
                    _parkJointsPulsePositions[3] = _parkJointsUnitsPositions[3] * RJointRatio;
                    _parkJointsPulsePositions[4] = _parkJointsUnitsPositions[4] * BJointRatio;
                    _parkJointsPulsePositions[5] = _parkJointsUnitsPositions[5] * TJointRatio;
                    _parkJointsPulsePositions[6] = _parkJointsUnitsPositions[6] * E7AxisRatio;
                    _parkJointsPulsePositions[7] = _parkJointsUnitsPositions[7] * E8AxisRatio;
                    _parkJointsPulsePositions[8] = _parkJointsUnitsPositions[4] * E9AxisRatio;
                    _parkJointsPulsePositions[9] = _parkJointsUnitsPositions[5] * E10AxisRatio;
                    _parkJointsPulsePositions[10] = _parkJointsUnitsPositions[6] * E11AxisRatio;
                    _parkJointsPulsePositions[11] = _parkJointsUnitsPositions[7] * E12AxisRatio;
                    #endregion
                }
            }
        }

        public double[][] LimitsJointsUnits
        {
            get
            {
                return _limitsJointsUnits;
            }
            set
            {
                #region
                _limitsJointsUnits = value;

                _limitsJointsPulse[0][0] = _limitsJointsUnits[0][0] * SJointRatio;
                _limitsJointsPulse[0][1] = _limitsJointsUnits[0][1] * SJointRatio;

                _limitsJointsPulse[1][0] = _limitsJointsUnits[1][0] * LJointRatio;
                _limitsJointsPulse[1][1] = _limitsJointsUnits[1][1] * LJointRatio;

                _limitsJointsPulse[2][0] = _limitsJointsUnits[2][0] * UJointRatio;
                _limitsJointsPulse[2][1] = _limitsJointsUnits[2][1] * UJointRatio;

                _limitsJointsPulse[3][0] = _limitsJointsUnits[3][0] * RJointRatio;
                _limitsJointsPulse[3][1] = _limitsJointsUnits[3][1] * RJointRatio;

                _limitsJointsPulse[4][0] = _limitsJointsUnits[4][0] * BJointRatio;
                _limitsJointsPulse[4][1] = _limitsJointsUnits[4][1] * BJointRatio;

                _limitsJointsPulse[5][0] = _limitsJointsUnits[5][0] * TJointRatio;
                _limitsJointsPulse[5][1] = _limitsJointsUnits[5][1] * TJointRatio;

                _limitsJointsPulse[6][0] = _limitsJointsUnits[6][0] * E7AxisRatio;
                _limitsJointsPulse[6][1] = _limitsJointsUnits[6][1] * E7AxisRatio;

                _limitsJointsPulse[7][0] = _limitsJointsUnits[7][0] * E8AxisRatio;
                _limitsJointsPulse[7][1] = _limitsJointsUnits[7][1] * E8AxisRatio;

                _limitsJointsPulse[8][0] = _limitsJointsUnits[8][0] * E9AxisRatio;
                _limitsJointsPulse[8][1] = _limitsJointsUnits[8][1] * E9AxisRatio;

                _limitsJointsPulse[9][0] = _limitsJointsUnits[9][0] * E10AxisRatio;
                _limitsJointsPulse[9][1] = _limitsJointsUnits[9][1] * E10AxisRatio;

                _limitsJointsPulse[10][0] = _limitsJointsUnits[10][0] * E11AxisRatio;
                _limitsJointsPulse[10][1] = _limitsJointsUnits[10][1] * E11AxisRatio;

                _limitsJointsPulse[11][0] = _limitsJointsUnits[10][0] * E12AxisRatio;
                _limitsJointsPulse[11][1] = _limitsJointsUnits[10][1] * E12AxisRatio;
                #endregion
            }
        }

        public double ActualSJointUnitsPosition
        {
            get
            {
                lock (_sAxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[0];
                }
            }
            set
            {
                lock (_sAxisUnitsLocker)
                {
                    _jointsInMotionArray[0] = (_actualJointsUnitsPositions[0] != value);

                    _actualSJointUnitsPosition = _actualJointsUnitsPositions[0] = value;

                    _actualJointsPulsePositions[0] = _actualJointsUnitsPositions[0] * SJointRatio;

                    _sJointUnitsInHome = _jointsInHomeArray[0] = (_actualJointsUnitsPositions[0] == _homeJointsUnitsPositions[0]);
                }
            }
        }
        public bool SJointUnitsInMotion
        {
            get
            {
                lock (_sAxisUnitsLocker)
                {
                    return _jointsInMotionArray[0];
                }
            }
        }
        public bool SJointUnitsInHome
        {
            get
            {
                lock (_sAxisUnitsLocker)
                {
                    return _sJointUnitsInHome;
                }
            }
        }
        public double MinSJointUnitsPosition
        {
            get
            {
                lock (_sAxisUnitsLocker)
                {
                    return _minSJointUnitsPosition;
                }
            }
            set
            {
                lock (_sAxisUnitsLocker)
                {
                    if (value >= -180.0 && value <= 180.0)
                    {
                        _limitsJointsUnits[0][0] = _minSJointUnitsPosition = value;
                        _limitsJointsPulse[0][0] = _limitsJointsUnits[0][0] * SJointRatio;
                    }
                }
            }
        }
        public double MaxSJointUnitsPosition
        {
            get
            {
                lock (_sAxisUnitsLocker)
                {
                    return _maxSJointUnitsPosition;
                }
            }
            set
            {
                lock (_sAxisUnitsLocker)
                {
                    if (value >= -180.0 && value <= 180.0)
                    {
                        _limitsJointsUnits[0][1] = _maxSJointUnitsPosition = value;
                        _limitsJointsPulse[0][1] = _limitsJointsUnits[0][1] * SJointRatio;
                    }
                }
            }
        }
        public double SJointUnitsHomePosition
        {
            get
            {
                lock (_sAxisUnitsLocker)
                {
                    return _sJointUnitsHomePosition;

                }
            }
            set
            {
                lock (_sAxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[0] = _sJointUnitsHomePosition = value;
                    _homeJointsPulsePositions[0] = _homeJointsUnitsPositions[0] * SJointRatio;
                }
            }
        }

        public double ActualLJointUnitsPosition
        {
            get
            {
                lock (_lAxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[1];
                }
            }
            set
            {
                lock (_lAxisUnitsLocker)
                {
                    _jointsInMotionArray[1] = (_actualJointsUnitsPositions[1] != value);

                    _actualLJointUnitsPosition = _actualJointsUnitsPositions[1] = value;

                    _actualJointsPulsePositions[1] = _actualJointsUnitsPositions[1] * LJointRatio;

                    _lJointUnitsInHome = _jointsInHomeArray[1] = (_actualJointsUnitsPositions[1] == _homeJointsUnitsPositions[1]);
                }
            }
        }
        public bool LJointUnitsInMotion
        {
            get
            {
                lock (_lAxisUnitsLocker)
                {
                    return _jointsInMotionArray[1];
                }
            }
        }
        public bool LJointUnitsInHome
        {
            get
            {
                lock (_lAxisUnitsLocker)
                {
                    return _lJointUnitsInHome;
                }
            }
        }
        public double MinLJointUnitsPosition
        {
            get
            {
                lock (_lAxisUnitsLocker)
                {
                    return _minLJointUnitsPosition;
                }
            }
            set
            {
                lock (_lAxisUnitsLocker)
                {
                    if (value >= -105.0 && value <= 155.0)
                    {
                        _limitsJointsUnits[1][0] = _minLJointUnitsPosition = value;
                        _limitsJointsPulse[1][0] = _limitsJointsUnits[1][0] * LJointRatio;
                    }
                }
            }
        }
        public double MaxLJointUnitsPosition
        {
            get
            {
                lock (_lAxisUnitsLocker)
                {
                    return _maxLJointUnitsPosition;
                }
            }
            set
            {
                lock (_lAxisUnitsLocker)
                {
                    if (value >= -105.0 && value <= 155.0)
                    {
                        _limitsJointsUnits[1][1] = _maxLJointUnitsPosition = value;
                        _limitsJointsPulse[1][1] = _limitsJointsUnits[1][1] * LJointRatio;
                    }
                }
            }
        }
        public double LJointUnitsHomePosition
        {
            get
            {
                lock (_lAxisUnitsLocker)
                {
                    return _lJointUnitsHomePosition;

                }
            }
            set
            {
                lock (_lAxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[1] = _lJointUnitsHomePosition = value;
                    _homeJointsPulsePositions[1] = _homeJointsUnitsPositions[1] * LJointRatio;
                }
            }
        }

        public double ActualUJointUnitsPosition
        {
            get
            {
                lock (_uAxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[2];
                }
            }
            set
            {
                lock (_uAxisUnitsLocker)
                {
                    _jointsInMotionArray[2] = (_actualJointsUnitsPositions[2] != value);

                    _actualUJointUnitsPosition = _actualJointsUnitsPositions[2] = value;

                    _actualJointsPulsePositions[2] = _actualJointsUnitsPositions[2] * UJointRatio;

                    _uJointUnitsInHome = _jointsInHomeArray[2] = (_actualJointsUnitsPositions[2] == _homeJointsUnitsPositions[2]);
                }
            }
        }
        public bool UJointUnitsInMotion
        {
            get
            {
                lock (_uAxisUnitsLocker)
                {
                    return _jointsInMotionArray[2];
                }
            }
        }
        public bool UJointUnitsInHome
        {
            get
            {
                lock (_uAxisUnitsLocker)
                {
                    return _uJointUnitsInHome;
                }
            }
        }
        public double MinUJointUnitsPosition
        {
            get
            {
                lock (_uAxisUnitsLocker)
                {
                    return _minUJointUnitsPosition;
                }
            }
            set
            {
                lock (_uAxisUnitsLocker)
                {
                    if (value >= -170.0 && value <= 240.0)
                    {
                        _limitsJointsUnits[2][0] = _minUJointUnitsPosition = value;
                        _limitsJointsPulse[2][0] = _limitsJointsUnits[2][0] * UJointRatio;
                    }
                }
            }
        }
        public double MaxUJointUnitsPosition
        {
            get
            {
                lock (_uAxisUnitsLocker)
                {
                    return _maxUJointUnitsPosition;

                }
            }
            set
            {
                lock (_uAxisUnitsLocker)
                {
                    if (value >= -170.0 && value <= 240.0)
                    {
                        _limitsJointsUnits[2][1] = _maxUJointUnitsPosition = value;
                        _limitsJointsPulse[2][1] = _limitsJointsUnits[2][1] * UJointRatio;
                    }
                }
            }
        }
        public double UJointUnitsHomePosition
        {
            get
            {
                lock (_uAxisUnitsLocker)
                {
                    return _uJointUnitsHomePosition;

                }
            }
            set
            {
                lock (_uAxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[2] = _uJointUnitsHomePosition = value;
                    _homeJointsPulsePositions[2] = _homeJointsUnitsPositions[2] * LJointRatio;
                }
            }
        }

        public double ActualRJointUnitsPosition
        {
            get
            {
                lock (_rAxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[3];
                }
            }
            set
            {
                lock (_rAxisUnitsLocker)
                {
                    _jointsInMotionArray[3] = (_actualJointsUnitsPositions[3] != value);

                    _actualRJointUnitsPosition = _actualJointsUnitsPositions[3] = value;

                    _actualJointsPulsePositions[3] = _actualJointsUnitsPositions[3] * RJointRatio;

                    _rJointUnitsInHome = _jointsInHomeArray[3] = (_actualJointsUnitsPositions[3] == _homeJointsUnitsPositions[3]);
                }
            }
        }
        public bool RJointUnitsInMotion
        {
            get
            {
                lock (_rAxisUnitsLocker)
                {
                    return _jointsInMotionArray[3];
                }
            }
        }
        public bool RJointUnitsInHome
        {
            get
            {
                lock (_rAxisUnitsLocker)
                {
                    return _rJointUnitsInHome;
                }
            }
        }
        public double MinRJointUnitsPosition
        {
            get
            {
                lock (_rAxisUnitsLocker)
                {
                    return _minRJointUnitsPosition;
                }
            }
            set
            {
                lock (_rAxisUnitsLocker)
                {
                    if (value >= -200.0 && value <= 200.0)
                    {
                        _limitsJointsUnits[3][0] = _minRJointUnitsPosition = value;
                        _limitsJointsPulse[3][0] = _limitsJointsUnits[3][0] * RJointRatio;
                    }
                }
            }
        }
        public double MaxRJointUnitsPosition
        {
            get
            {
                lock (_rAxisUnitsLocker)
                {
                    return _maxRJointUnitsPosition;

                }
            }
            set
            {
                lock (_rAxisUnitsLocker)
                {
                    if (value >= -200.0 && value <= 200.0)
                    {
                        _limitsJointsUnits[3][1] = _maxRJointUnitsPosition = value;
                        _limitsJointsPulse[3][1] = _limitsJointsUnits[3][1] * RJointRatio;
                    }
                }
            }
        }
        public double RJointUnitsHomePosition
        {
            get
            {
                lock (_rAxisUnitsLocker)
                {
                    return _rJointUnitsHomePosition;

                }
            }
            set
            {
                lock (_rAxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[3] = _rJointUnitsHomePosition = value;
                    _homeJointsPulsePositions[3] = _homeJointsUnitsPositions[3] * RJointRatio;
                }
            }
        }

        public double ActualBJointUnitsPosition
        {
            get
            {
                lock (_bAxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[4];
                }
            }
            set
            {
                lock (_bAxisUnitsLocker)
                {
                    _jointsInMotionArray[4] = (_actualJointsUnitsPositions[4] != value);

                    _actualBJointUnitsPosition = _actualJointsUnitsPositions[4] = value;

                    _actualJointsPulsePositions[4] = _actualJointsUnitsPositions[4] * BJointRatio;

                    _bJointUnitsInHome = _jointsInHomeArray[4] = (_actualJointsUnitsPositions[4] == _homeJointsUnitsPositions[4]);
                }
            }
        }
        public bool BJointUnitsInMotion
        {
            get
            {
                lock (_bAxisUnitsLocker)
                {
                    return _jointsInMotionArray[4];
                }
            }
        }
        public bool BJointUnitsInHome
        {
            get
            {
                lock (_bAxisUnitsLocker)
                {
                    return _bJointUnitsInHome;
                }
            }
        }
        public double MinBJointUnitsPosition
        {
            get
            {
                lock (_bAxisUnitsLocker)
                {
                    return _minBJointUnitsPosition;
                }
            }
            set
            {
                lock (_bAxisUnitsLocker)
                {
                    if (value >= -150.0 && value <= 150.0)
                    {
                        _limitsJointsUnits[4][0] = _minBJointUnitsPosition = value;
                        _limitsJointsPulse[4][0] = _limitsJointsUnits[4][0] * BJointRatio;
                    }
                }
            }
        }
        public double MaxBJointUnitsPosition
        {
            get
            {
                lock (_bAxisUnitsLocker)
                {
                    return _maxBJointUnitsPosition;

                }
            }
            set
            {
                lock (_bAxisUnitsLocker)
                {
                    if (value >= -150.0 && value <= 150.0)
                    {
                        _limitsJointsUnits[4][0] = _minBJointUnitsPosition = value;
                        _limitsJointsPulse[4][0] = _limitsJointsUnits[4][0] * BJointRatio;
                    }
                }
            }
        }
        public double BJointUnitsHomePosition
        {
            get
            {
                lock (_bAxisUnitsLocker)
                {
                    return _bJointUnitsHomePosition;

                }
            }
            set
            {
                lock (_bAxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[4] = _bJointUnitsHomePosition = value;
                    _homeJointsPulsePositions[4] = _homeJointsUnitsPositions[4] * BJointRatio;
                }
            }
        }

        public double ActualTJointUnitsPosition
        {
            get
            {
                lock (_tAxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[5];
                }
            }
            set
            {
                lock (_tAxisUnitsLocker)
                {
                    _jointsInMotionArray[5] = (_actualJointsUnitsPositions[5] != value);

                    _actualTJointUnitsPosition = _actualJointsUnitsPositions[5] = value;

                    _actualJointsPulsePositions[5] = _actualJointsUnitsPositions[5] * TJointRatio;

                    _tJointUnitsInHome = _jointsInHomeArray[5] = (_actualJointsUnitsPositions[5] == _homeJointsUnitsPositions[5]);
                }
            }
        }
        public bool TJointUnitsInMotion
        {
            get
            {
                lock (_tAxisUnitsLocker)
                {
                    return _jointsInMotionArray[5];
                }
            }
        }
        public bool TJointUnitsInHome
        {
            get
            {
                lock (_tAxisUnitsLocker)
                {
                    return _tJointUnitsInHome;
                }
            }
        }
        public double MinTJointUnitsPosition
        {
            get
            {
                lock (_tAxisUnitsLocker)
                {
                    return _minTJointUnitsPosition;
                }
            }
            set
            {
                lock (_tAxisUnitsLocker)
                {
                    if (value >= -455.0 && value <= 455.0)
                    {
                        _limitsJointsUnits[5][0] = _minTJointUnitsPosition = value;
                        _limitsJointsPulse[5][0] = _limitsJointsUnits[5][0] * TJointRatio;
                    }
                }
            }
        }
        public double MaxTJointUnitsPosition
        {
            get
            {
                lock (_tAxisUnitsLocker)
                {
                    return _maxTJointUnitsPosition;

                }
            }
            set
            {
                lock (_tAxisUnitsLocker)
                {
                    if (value >= -455.0 && value <= 455.0)
                    {
                        _limitsJointsUnits[5][0] = _minTJointUnitsPosition = value;
                        _limitsJointsPulse[5][0] = _limitsJointsUnits[5][0] * TJointRatio;
                    }
                }
            }
        }
        public double TJointUnitsHomePosition
        {
            get
            {
                lock (_tAxisUnitsLocker)
                {
                    return _tJointUnitsHomePosition;

                }
            }
            set
            {
                lock (_tAxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[5] = _tJointUnitsHomePosition = value;
                    _homeJointsPulsePositions[5] = _homeJointsUnitsPositions[5] * TJointRatio;
                }
            }
        }

        public double ActualE7AxisUnitsPosition
        {
            get
            {
                lock (_e7AxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[6];
                }
            }
            set
            {
                lock (_e7AxisUnitsLocker)
                {
                    _jointsInMotionArray[6] = (_actualJointsUnitsPositions[6] != value);

                    _actualE7AxisUnitsPosition = _actualJointsUnitsPositions[6] = value;

                    _actualJointsPulsePositions[6] = _actualJointsUnitsPositions[6] * E7AxisRatio;

                    _e7AxisUnitsInHome = _jointsInHomeArray[6] = (_actualJointsUnitsPositions[6] == _homeJointsUnitsPositions[6]);
                }
            }
        }
        public bool E7AxisUnitsInMotion
        {
            get
            {
                lock (_e7AxisUnitsLocker)
                {
                    return _jointsInMotionArray[6];
                }
            }
        }
        public bool E7AxisUnitsInHome
        {
            get
            {
                lock (_e7AxisUnitsLocker)
                {
                    return _e7AxisUnitsInHome;
                }
            }
        }
        public double MinE7AxisUnitsPosition
        {
            get
            {
                lock (_e7AxisUnitsLocker)
                {
                    return _minE7AxisUnitsPosition;
                }
            }
            set
            {
                lock (_e7AxisUnitsLocker)
                {
                    if (value >= 0.5 && value <= 4500.0)
                    {
                        _limitsJointsUnits[6][0] = _minE7AxisUnitsPosition = value;
                        _limitsJointsPulse[6][0] = _limitsJointsUnits[6][0] * E7AxisRatio;
                    }
                }
            }
        }
        public double MaxE7AxisUnitsPosition
        {
            get
            {
                lock (_e7AxisUnitsLocker)
                {
                    return _maxE7AxisUnitsPosition;

                }
            }
            set
            {
                lock (_e7AxisUnitsLocker)
                {
                    if (value >= 0.5 && value <= 4500.0)
                    {
                        _limitsJointsUnits[6][0] = _minE7AxisUnitsPosition = value;
                        _limitsJointsPulse[6][0] = _limitsJointsUnits[6][0] * E7AxisRatio;
                    }
                }
            }
        }
        public double E7AxisUnitsHomePosition
        {
            get
            {
                lock (_e7AxisUnitsLocker)
                {
                    return _e7AxisUnitsHomePosition;

                }
            }
            set
            {
                lock (_e7AxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[6] = _e7AxisUnitsHomePosition = value;
                    _homeJointsPulsePositions[6] = _homeJointsUnitsPositions[6] * E7AxisRatio;
                }
            }
        }

        public double ActualE8AxisUnitsPosition
        {
            get
            {
                lock (_e8AxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[7];
                }
            }
            set
            {
                lock (_e8AxisUnitsLocker)
                {
                    _jointsInMotionArray[7] = (_actualJointsUnitsPositions[7] != value);

                    _actualE8AxisUnitsPosition = _actualJointsUnitsPositions[7] = value;

                    _actualJointsPulsePositions[7] = _actualJointsUnitsPositions[7] * E8AxisRatio;

                    _e8AxisUnitsInHome = _jointsInHomeArray[7] = (_actualJointsUnitsPositions[7] == _homeJointsUnitsPositions[7]);
                }
            }
        }
        public bool E8AxisUnitsInMotion
        {
            get
            {
                lock (_e8AxisUnitsLocker)
                {
                    return _jointsInMotionArray[7];
                }
            }
        }
        public bool E8AxisUnitsInHome
        {
            get
            {
                lock (_e8AxisUnitsLocker)
                {
                    return _e8AxisUnitsInHome;
                }
            }
        }
        public double MinE8AxisUnitsPosition
        {
            get
            {
                lock (_e8AxisUnitsLocker)
                {
                    return _minE8AxisUnitsPosition;
                }
            }
            set
            {
                lock (_e8AxisUnitsLocker)
                {
                    if (value >= -360.0 && value <= 360.0)
                    {
                        _limitsJointsUnits[7][0] = _minE8AxisUnitsPosition = value;
                        _limitsJointsPulse[7][0] = _limitsJointsUnits[7][0] * E8AxisRatio;
                    }
                }
            }
        }
        public double MaxE8AxisUnitsPosition
        {
            get
            {
                lock (_e8AxisUnitsLocker)
                {
                    return _maxE8AxisUnitsPosition;

                }
            }
            set
            {
                lock (_e8AxisUnitsLocker)
                {
                    if (value >= -360.0 && value <= 360.0)
                    {
                        _limitsJointsUnits[7][0] = _minE8AxisUnitsPosition = value;
                        _limitsJointsPulse[7][0] = _limitsJointsUnits[7][0] * E8AxisRatio;
                    }
                }
            }
        }
        public double E8AxisUnitsHomePosition
        {
            get
            {
                lock (_e8AxisUnitsLocker)
                {
                    return _e8AxisUnitsHomePosition;

                }
            }
            set
            {
                lock (_e8AxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[7] = _e8AxisUnitsHomePosition = value;
                    _homeJointsPulsePositions[7] = _homeJointsUnitsPositions[7] * E8AxisRatio;
                }
            }
        }

        public double ActualE9AxisUnitsPosition
        {
            get
            {
                lock (_e9AxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[8];
                }
            }
            set
            {
                lock (_e9AxisUnitsLocker)
                {
                    _jointsInMotionArray[8] = (_actualJointsUnitsPositions[8] != value);

                    _actualE9AxisUnitsPosition = _actualJointsUnitsPositions[8] = value;

                    _actualJointsPulsePositions[8] = _actualJointsUnitsPositions[8] * E9AxisRatio;

                    _e9AxisUnitsInHome = _jointsInHomeArray[8] = (_actualJointsUnitsPositions[8] == _homeJointsUnitsPositions[8]);
                }
            }
        }
        public bool E9AxisUnitsInMotion
        {
            get
            {
                lock (_e9AxisUnitsLocker)
                {
                    return _jointsInMotionArray[8];
                }
            }
        }
        public bool E9AxisUnitsInHome
        {
            get
            {
                lock (_e9AxisUnitsLocker)
                {
                    return _e9AxisUnitsInHome;
                }
            }
        }
        public double MinE9AxisUnitsPosition
        {
            get
            {
                lock (_e9AxisUnitsLocker)
                {
                    return _minE9AxisUnitsPosition;
                }
            }
            set
            {
                lock (_e9AxisUnitsLocker)
                {
                    if (value >= -360.0 && value <= 360.0)
                    {
                        _limitsJointsUnits[8][0] = _minE9AxisUnitsPosition = value;
                        _limitsJointsPulse[8][0] = _limitsJointsUnits[8][0] * E9AxisRatio;
                    }
                }
            }
        }
        public double MaxE9AxisUnitsPosition
        {
            get
            {
                lock (_e9AxisUnitsLocker)
                {
                    return _maxE9AxisUnitsPosition;

                }
            }
            set
            {
                lock (_e9AxisUnitsLocker)
                {
                    if (value >= -360.0 && value <= 360.0)
                    {
                        _limitsJointsUnits[8][0] = _minE9AxisUnitsPosition = value;
                        _limitsJointsPulse[8][0] = _limitsJointsUnits[8][0] * E9AxisRatio;
                    }
                }
            }
        }
        public double E9AxisUnitsHomePosition
        {
            get
            {
                lock (_e9AxisUnitsLocker)
                {
                    return _e9AxisUnitsHomePosition;

                }
            }
            set
            {
                lock (_e9AxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[8] = _e9AxisUnitsHomePosition = value;
                    _homeJointsPulsePositions[8] = _homeJointsUnitsPositions[8] * E9AxisRatio;
                }
            }
        }

        public double ActualE10AxisUnitsPosition
        {
            get
            {
                lock (_e10AxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[9];
                }
            }
            set
            {
                lock (_e10AxisUnitsLocker)
                {
                    _jointsInMotionArray[9] = (_actualJointsUnitsPositions[9] != value);

                    _actualE10AxisUnitsPosition = _actualJointsUnitsPositions[9] = value;

                    _actualJointsPulsePositions[9] = _actualJointsUnitsPositions[9] * E10AxisRatio;

                    _e10AxisUnitsInHome = _jointsInHomeArray[9] = (_actualJointsUnitsPositions[9] == _homeJointsUnitsPositions[9]);
                }
            }
        }
        public bool E10AxisUnitsInMotion
        {
            get
            {
                lock (_e10AxisUnitsLocker)
                {
                    return _jointsInMotionArray[9];
                }
            }
        }
        public bool E10AxisUnitsInHome
        {
            get
            {
                lock (_e10AxisUnitsLocker)
                {
                    return _e10AxisUnitsInHome;
                }
            }
        }
        public double MinE10AxisUnitsPosition
        {
            get
            {
                lock (_e10AxisUnitsLocker)
                {
                    return _minE10AxisUnitsPosition;
                }
            }
            set
            {
                lock (_e10AxisUnitsLocker)
                {
                    if (value >= -360.0 && value <= 360.0)
                    {
                        _limitsJointsUnits[9][0] = _minE10AxisUnitsPosition = value;
                        _limitsJointsPulse[9][0] = _limitsJointsUnits[9][0] * E10AxisRatio;
                    }
                }
            }
        }
        public double MaxE10AxisUnitsPosition
        {
            get
            {
                lock (_e10AxisUnitsLocker)
                {
                    return _maxE10AxisUnitsPosition;

                }
            }
            set
            {
                lock (_e10AxisUnitsLocker)
                {
                    if (value >= -360.0 && value <= 360.0)
                    {
                        _limitsJointsUnits[9][0] = _minE10AxisUnitsPosition = value;
                        _limitsJointsPulse[9][0] = _limitsJointsUnits[9][0] * E10AxisRatio;
                    }
                }
            }
        }
        public double E10AxisUnitsHomePosition
        {
            get
            {
                lock (_e10AxisUnitsLocker)
                {
                    return _e10AxisUnitsHomePosition;

                }
            }
            set
            {
                lock (_e10AxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[9] = _e10AxisUnitsHomePosition = value;
                    _homeJointsPulsePositions[9] = _homeJointsUnitsPositions[9] * E10AxisRatio;
                }
            }
        }

        public double ActualE11AxisUnitsPosition
        {
            get
            {
                lock (_e11AxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[10];
                }
            }
            set
            {
                lock (_e11AxisUnitsLocker)
                {
                    _jointsInMotionArray[10] = (_actualJointsUnitsPositions[10] != value);

                    _actualE11AxisUnitsPosition = _actualJointsUnitsPositions[10] = value;

                    _actualJointsPulsePositions[10] = _actualJointsUnitsPositions[10] * E11AxisRatio;

                    _e11AxisUnitsInHome = _jointsInHomeArray[10] = (_actualJointsUnitsPositions[10] == _homeJointsUnitsPositions[10]);
                }
            }
        }
        public bool E11AxisUnitsInMotion
        {
            get
            {
                lock (_e11AxisUnitsLocker)
                {
                    return _jointsInMotionArray[10];
                }
            }
        }
        public bool E11AxisUnitsInHome
        {
            get
            {
                lock (_e11AxisUnitsLocker)
                {
                    return _e11AxisUnitsInHome;
                }
            }
        }
        public double MinE11AxisUnitsPosition
        {
            get
            {
                lock (_e11AxisUnitsLocker)
                {
                    return _minE11AxisUnitsPosition;
                }
            }
            set
            {
                lock (_e11AxisUnitsLocker)
                {
                    if (value >= -360.0 && value <= 360.0)
                    {
                        _limitsJointsUnits[10][0] = _minE11AxisUnitsPosition = value;
                        _limitsJointsPulse[10][0] = _limitsJointsUnits[10][0] * E11AxisRatio;
                    }
                }
            }
        }
        public double MaxE11AxisUnitsPosition
        {
            get
            {
                lock (_e11AxisUnitsLocker)
                {
                    return _maxE11AxisUnitsPosition;

                }
            }
            set
            {
                lock (_e11AxisUnitsLocker)
                {
                    if (value >= -360.0 && value <= 360.0)
                    {
                        _limitsJointsUnits[10][0] = _minE11AxisUnitsPosition = value;
                        _limitsJointsPulse[10][0] = _limitsJointsUnits[10][0] * E11AxisRatio;
                    }
                }
            }
        }
        public double E11AxisUnitsHomePosition
        {
            get
            {
                lock (_e11AxisUnitsLocker)
                {
                    return _e11AxisUnitsHomePosition;

                }
            }
            set
            {
                lock (_e11AxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[10] = _e11AxisUnitsHomePosition = value;
                    _homeJointsPulsePositions[10] = _homeJointsUnitsPositions[10] * E11AxisRatio;
                }
            }
        }

        public double ActualE12AxisUnitsPosition
        {
            get
            {
                lock (_e12AxisUnitsLocker)
                {
                    return _actualJointsUnitsPositions[11];
                }
            }
            set
            {
                lock (_e12AxisUnitsLocker)
                {
                    _jointsInMotionArray[11] = (_actualJointsUnitsPositions[11] != value);

                    _actualE12AxisUnitsPosition = _actualJointsUnitsPositions[11] = value;

                    _actualJointsPulsePositions[11] = _actualJointsUnitsPositions[11] * E12AxisRatio;

                    _e12AxisUnitsInHome = _jointsInHomeArray[11] = (_actualJointsUnitsPositions[11] == _homeJointsUnitsPositions[11]);
                }
            }
        }
        public bool E12AxisUnitsInMotion
        {
            get
            {
                lock (_e12AxisUnitsLocker)
                {
                    return _jointsInMotionArray[11];
                }
            }
        }
        public bool E12AxisUnitsInHome
        {
            get
            {
                lock (_e12AxisUnitsLocker)
                {
                    return _e12AxisUnitsInHome;
                }
            }
        }
        public double MinE12AxisUnitsPosition
        {
            get
            {
                lock (_e12AxisUnitsLocker)
                {
                    return _minE12AxisUnitsPosition;
                }
            }
            set
            {
                lock (_e12AxisUnitsLocker)
                {
                    if (value >= -360.0 && value <= 360.0)
                    {
                        _limitsJointsUnits[11][0] = _minE12AxisUnitsPosition = value;
                        _limitsJointsPulse[11][0] = _limitsJointsUnits[11][0] * E12AxisRatio;
                    }
                }
            }
        }
        public double MaxE12AxisUnitsPosition
        {
            get
            {
                lock (_e12AxisUnitsLocker)
                {
                    return _maxE12AxisUnitsPosition;

                }
            }
            set
            {
                lock (_e12AxisUnitsLocker)
                {
                    if (value >= -360.0 && value <= 360.0)
                    {
                        _limitsJointsUnits[11][0] = _minE12AxisUnitsPosition = value;
                        _limitsJointsPulse[11][0] = _limitsJointsUnits[11][0] * E12AxisRatio;
                    }
                }
            }
        }
        public double E12AxisUnitsHomePosition
        {
            get
            {
                lock (_e12AxisUnitsLocker)
                {
                    return _e12AxisUnitsHomePosition;

                }
            }
            set
            {
                lock (_e12AxisUnitsLocker)
                {
                    _homeJointsUnitsPositions[11] = _e12AxisUnitsHomePosition = value;
                    _homeJointsPulsePositions[11] = _homeJointsUnitsPositions[11] * E12AxisRatio;
                }
            }
        }

        #endregion

        #region Pulses

        public double[] ActualJointsPulsePositions
        {
            get
            {
                return _actualJointsPulsePositions;
            }
            set
            {
                #region
                _jointsInMotionArray[0] = (_actualJointsPulsePositions[0] != value[0]);
                _jointsInMotionArray[1] = (_actualJointsPulsePositions[1] != value[1]);
                _jointsInMotionArray[2] = (_actualJointsPulsePositions[2] != value[2]);
                _jointsInMotionArray[3] = (_actualJointsPulsePositions[3] != value[3]);
                _jointsInMotionArray[4] = (_actualJointsPulsePositions[4] != value[4]);
                _jointsInMotionArray[5] = (_actualJointsPulsePositions[5] != value[5]);
                _jointsInMotionArray[6] = (_actualJointsPulsePositions[6] != value[6]);
                _jointsInMotionArray[7] = (_actualJointsPulsePositions[7] != value[7]);
                _jointsInMotionArray[8] = (_actualJointsPulsePositions[8] != value[8]);
                _jointsInMotionArray[9] = (_actualJointsPulsePositions[9] != value[9]);
                _jointsInMotionArray[10] = (_actualJointsPulsePositions[10] != value[10]);
                _jointsInMotionArray[11] = (_actualJointsPulsePositions[11] != value[11]);

                _actualJointsPulsePositions = value;
                //IMPORTANT:Vlad:turn table sync
                _actualJointsPulsePositions[8] = -(_actualJointsPulsePositions[7]);

                _actualSJointPulsePosition = _actualJointsPulsePositions[0];
                _actualLJointPulsePosition = _actualJointsPulsePositions[1];
                _actualUJointPulsePosition = _actualJointsPulsePositions[2];
                _actualRJointPulsePosition = _actualJointsPulsePositions[3];
                _actualBJointPulsePosition = _actualJointsPulsePositions[4];
                _actualTJointPulsePosition = _actualJointsPulsePositions[5];
                _actualE7AxisPulsePosition = _actualJointsPulsePositions[6];
                _actualE8AxisPulsePosition = _actualJointsPulsePositions[7];
                _actualE9AxisPulsePosition = _actualJointsPulsePositions[8];
                _actualE10AxisPulsePosition = _actualJointsPulsePositions[9];
                _actualE11AxisPulsePosition = _actualJointsPulsePositions[10];
                _actualE12AxisPulsePosition = _actualJointsPulsePositions[11];

                _actualSJointUnitsPosition = _actualJointsUnitsPositions[0] = _actualJointsPulsePositions[0] / SJointRatio;
                _actualLJointUnitsPosition = _actualJointsUnitsPositions[1] = _actualJointsPulsePositions[1] / LJointRatio;
                _actualUJointUnitsPosition = _actualJointsUnitsPositions[2] = _actualJointsPulsePositions[2] / UJointRatio;
                _actualRJointUnitsPosition = _actualJointsUnitsPositions[3] = _actualJointsPulsePositions[3] / RJointRatio;
                _actualBJointUnitsPosition = _actualJointsUnitsPositions[4] = _actualJointsPulsePositions[4] / BJointRatio;
                _actualTJointUnitsPosition = _actualJointsUnitsPositions[5] = _actualJointsPulsePositions[5] / TJointRatio;
                _actualE7AxisUnitsPosition = _actualJointsUnitsPositions[6] = _actualJointsPulsePositions[6] / E7AxisRatio;
                _actualE8AxisUnitsPosition = _actualJointsUnitsPositions[7] = _actualJointsPulsePositions[7] / E8AxisRatio;
                _actualE9AxisUnitsPosition = _actualJointsUnitsPositions[8] = _actualJointsPulsePositions[8] / E9AxisRatio;
                _actualE10AxisUnitsPosition = _actualJointsUnitsPositions[9] = _actualJointsPulsePositions[9] / E10AxisRatio;
                _actualE11AxisUnitsPosition = _actualJointsUnitsPositions[10] = _actualJointsPulsePositions[10] / E11AxisRatio;
                _actualE12AxisUnitsPosition = _actualJointsUnitsPositions[11] = _actualJointsPulsePositions[11] / E12AxisRatio;

                _sJointPulseInHome = (_actualJointsPulsePositions[0] == _homeJointsPulsePositions[0]);
                _lJointPulseInHome = (_actualJointsPulsePositions[1] == _homeJointsPulsePositions[1]);
                _uJointPulseInHome = (_actualJointsPulsePositions[2] == _homeJointsPulsePositions[2]);
                _rJointPulseInHome = (_actualJointsPulsePositions[3] == _homeJointsPulsePositions[3]);
                _bJointPulseInHome = (_actualJointsPulsePositions[4] == _homeJointsPulsePositions[4]);
                _tJointPulseInHome = (_actualJointsPulsePositions[5] == _homeJointsPulsePositions[5]);
                _e7AxisPulseInHome = (_actualJointsPulsePositions[6] == _homeJointsPulsePositions[6]);
                _e8AxisPulseInHome = (_actualJointsPulsePositions[7] == _homeJointsPulsePositions[7]);
                _e9AxisPulseInHome = (_actualJointsPulsePositions[8] == _homeJointsPulsePositions[8]);
                _e10AxisPulseInHome = (_actualJointsPulsePositions[9] == _homeJointsPulsePositions[9]);
                _e11AxisPulseInHome = (_actualJointsPulsePositions[10] == _homeJointsPulsePositions[10]);
                _e12AxisPulseInHome = (_actualJointsPulsePositions[11] == _homeJointsPulsePositions[11]);
                #endregion
            }
        }
        public double[] DesiredJointsPulsePositions
        {
            get
            {
                return _desiredJointsPulsePositions;
            }
            set
            {
                lock (_desiredJointsPulsePositionsLocker)
                {
                    _desiredJointsPulsePositions = value;

                    _desiredJointsUnitsPositions[0] = _desiredJointsPulsePositions[0] / SJointRatio;
                    _desiredJointsUnitsPositions[1] = _desiredJointsPulsePositions[1] / LJointRatio;
                    _desiredJointsUnitsPositions[2] = _desiredJointsPulsePositions[2] / UJointRatio;
                    _desiredJointsUnitsPositions[3] = _desiredJointsPulsePositions[3] / RJointRatio;
                    _desiredJointsUnitsPositions[4] = _desiredJointsPulsePositions[4] / BJointRatio;
                    _desiredJointsUnitsPositions[5] = _desiredJointsPulsePositions[5] / TJointRatio;
                    _desiredJointsUnitsPositions[6] = _desiredJointsPulsePositions[6] / E7AxisRatio;
                    _desiredJointsUnitsPositions[7] = _desiredJointsPulsePositions[7] / E8AxisRatio;
                    _desiredJointsUnitsPositions[8] = _desiredJointsPulsePositions[8] / E9AxisRatio;
                    _desiredJointsUnitsPositions[9] = _desiredJointsPulsePositions[9] / E10AxisRatio;
                    _desiredJointsUnitsPositions[10] = _desiredJointsPulsePositions[10] / E11AxisRatio;
                    _desiredJointsUnitsPositions[11] = _desiredJointsPulsePositions[11] / E12AxisRatio;
                }
            }
        }

        public double[] JointsPulseHomePositions
        {
            get
            {
                lock (_homeJointsPulsePositionsLocker)
                {
                    return _homeJointsPulsePositions;
                }
            }
            set
            {
                lock (_homeJointsPulsePositionsLocker)
                {
                    #region
                    _homeJointsPulsePositions = value;

                    _homeJointsUnitsPositions[0] = _homeJointsPulsePositions[0] / SJointRatio;
                    _homeJointsUnitsPositions[1] = _homeJointsPulsePositions[1] / LJointRatio;
                    _homeJointsUnitsPositions[2] = _homeJointsPulsePositions[2] / UJointRatio;
                    _homeJointsUnitsPositions[3] = _homeJointsPulsePositions[3] / RJointRatio;
                    _homeJointsUnitsPositions[4] = _homeJointsPulsePositions[4] / BJointRatio;
                    _homeJointsUnitsPositions[5] = _homeJointsPulsePositions[5] / TJointRatio;
                    _homeJointsUnitsPositions[6] = _homeJointsPulsePositions[6] / E7AxisRatio;
                    _homeJointsUnitsPositions[7] = _homeJointsPulsePositions[7] / E8AxisRatio;
                    _homeJointsUnitsPositions[8] = _homeJointsPulsePositions[8] / E9AxisRatio;
                    _homeJointsUnitsPositions[9] = _homeJointsPulsePositions[9] / E10AxisRatio;
                    _homeJointsUnitsPositions[10] = _homeJointsPulsePositions[10] / E11AxisRatio;
                    _homeJointsUnitsPositions[11] = _homeJointsPulsePositions[11] / E12AxisRatio;
                    #endregion
                }
            }
        }
        public double[] JointsPulseParkPositions
        {
            get
            {
                lock (_parkJointsPulsePositionsLocker)
                {
                    return _parkJointsPulsePositions;
                }
            }
            set
            {
                lock (_parkJointsPulsePositionsLocker)
                {
                    #region
                    _parkJointsPulsePositions = value;

                    _parkJointsUnitsPositions[0] = _parkJointsPulsePositions[0] / SJointRatio;
                    _parkJointsUnitsPositions[1] = _parkJointsPulsePositions[1] / LJointRatio;
                    _parkJointsUnitsPositions[2] = _parkJointsPulsePositions[2] / UJointRatio;
                    _parkJointsUnitsPositions[3] = _parkJointsPulsePositions[3] / RJointRatio;
                    _parkJointsUnitsPositions[4] = _parkJointsPulsePositions[4] / BJointRatio;
                    _parkJointsUnitsPositions[5] = _parkJointsPulsePositions[5] / TJointRatio;
                    _parkJointsUnitsPositions[6] = _parkJointsPulsePositions[6] / E7AxisRatio;
                    _parkJointsUnitsPositions[7] = _parkJointsPulsePositions[7] / E8AxisRatio;
                    _parkJointsUnitsPositions[8] = _parkJointsPulsePositions[8] / E9AxisRatio;
                    _parkJointsUnitsPositions[9] = _parkJointsPulsePositions[9] / E10AxisRatio;
                    _parkJointsUnitsPositions[10] = _parkJointsPulsePositions[10] / E11AxisRatio;
                    _parkJointsUnitsPositions[11] = _parkJointsPulsePositions[11] / E12AxisRatio;
                    #endregion
                }
            }
        }

        public double[][] LimitsJointsPulse
        {
            get
            {
                return _limitsJointsPulse;
            }
            set
            {
                #region
                _limitsJointsPulse = value;

                _limitsJointsUnits[0][0] = _limitsJointsPulse[0][0] / SJointRatio;
                _limitsJointsUnits[0][1] = _limitsJointsPulse[0][1] / SJointRatio;

                _limitsJointsUnits[1][0] = _limitsJointsPulse[1][0] / LJointRatio;
                _limitsJointsUnits[1][1] = _limitsJointsPulse[1][1] / LJointRatio;

                _limitsJointsUnits[2][0] = _limitsJointsPulse[2][0] / UJointRatio;
                _limitsJointsUnits[2][1] = _limitsJointsPulse[2][1] / UJointRatio;

                _limitsJointsUnits[3][0] = _limitsJointsPulse[3][0] / RJointRatio;
                _limitsJointsUnits[3][1] = _limitsJointsPulse[3][1] / RJointRatio;

                _limitsJointsUnits[4][0] = _limitsJointsPulse[4][0] / BJointRatio;
                _limitsJointsUnits[4][1] = _limitsJointsPulse[4][1] / BJointRatio;

                _limitsJointsUnits[5][0] = _limitsJointsPulse[5][0] / TJointRatio;
                _limitsJointsUnits[5][1] = _limitsJointsPulse[5][1] / TJointRatio;

                _limitsJointsUnits[6][0] = _limitsJointsPulse[6][0] / E7AxisRatio;
                _limitsJointsUnits[6][1] = _limitsJointsPulse[6][1] / E7AxisRatio;

                _limitsJointsUnits[7][0] = _limitsJointsPulse[7][0] / E8AxisRatio;
                _limitsJointsUnits[7][1] = _limitsJointsPulse[7][1] / E8AxisRatio;

                _limitsJointsUnits[8][0] = _limitsJointsPulse[8][0] / E9AxisRatio;
                _limitsJointsUnits[8][1] = _limitsJointsPulse[8][1] / E9AxisRatio;

                _limitsJointsUnits[9][0] = _limitsJointsPulse[9][0] / E10AxisRatio;
                _limitsJointsUnits[9][1] = _limitsJointsPulse[9][1] / E10AxisRatio;

                _limitsJointsUnits[10][0] = _limitsJointsPulse[10][0] / E11AxisRatio;
                _limitsJointsUnits[10][1] = _limitsJointsPulse[10][1] / E11AxisRatio;

                _limitsJointsUnits[11][0] = _limitsJointsPulse[11][0] / E12AxisRatio;
                _limitsJointsUnits[11][1] = _limitsJointsPulse[11][1] / E12AxisRatio;
                #endregion
            }
        }

        public double ActualSJointPulsePosition
        {
            get
            {
                lock (_sAxisPulseLocker)
                {
                    return _actualJointsPulsePositions[0];
                }
            }
            set
            {
                lock (_sAxisPulseLocker)
                {
                    _jointsInMotionArray[0] = (_actualJointsPulsePositions[0] != value);

                    _actualSJointPulsePosition = _actualJointsPulsePositions[0] = value;

                    _actualJointsUnitsPositions[0] = _actualJointsPulsePositions[0] / SJointRatio;

                    _sJointPulseInHome = _jointsInHomeArray[0] = (_actualJointsPulsePositions[0] == _homeJointsPulsePositions[0]);
                }
            }
        }
        public bool SJointPulseInMotion
        {
            get
            {
                lock (_sAxisPulseLocker)
                {
                    return _jointsInMotionArray[0];
                }
            }
        }
        public bool SJointPulseInHome
        {
            get
            {
                lock (_sAxisPulseLocker)
                {
                    return _sJointPulseInHome;
                }
            }
        }
        public double MinSJointPulsePosition
        {
            get
            {
                lock (_sAxisPulseLocker)
                {
                    return _minSJointPulsePosition;
                }
            }
            set
            {
                lock (_sAxisPulseLocker)
                {
                    if (value >= (-180.0 * SJointRatio) && value <= (180.0 * SJointRatio))
                    {
                        _limitsJointsPulse[0][0] = _minSJointPulsePosition = value;
                        _limitsJointsUnits[0][0] = _limitsJointsPulse[0][0] / SJointRatio;
                    }
                }
            }
        }
        public double MaxSJointPulsePosition
        {
            get
            {
                lock (_sAxisPulseLocker)
                {
                    return _maxSJointPulsePosition;
                }
            }
            set
            {
                lock (_sAxisPulseLocker)
                {
                    if (value >= (-180.0 * SJointRatio) && value <= (180.0 * SJointRatio))
                    {
                        _limitsJointsPulse[0][1] = _maxSJointPulsePosition = value;
                        _limitsJointsUnits[0][1] = _limitsJointsPulse[0][1] / SJointRatio;
                    }
                }
            }
        }
        public double SJointPulseHomePosition
        {
            get
            {
                lock (_sAxisPulseLocker)
                {
                    return _sJointPulseHomePosition;

                }
            }
            set
            {
                lock (_sAxisPulseLocker)
                {
                    _homeJointsPulsePositions[0] = _sJointPulseHomePosition = value;
                    _homeJointsUnitsPositions[0] = _homeJointsPulsePositions[0] / SJointRatio;
                }
            }
        }

        public double ActualLJointPulsePosition
        {
            get
            {
                lock (_lAxisPulseLocker)
                {
                    return _actualJointsPulsePositions[1];
                }
            }
            set
            {
                lock (_lAxisPulseLocker)
                {
                    _jointsInMotionArray[1] = (_actualJointsPulsePositions[1] != value);

                    _actualLJointPulsePosition = _actualJointsPulsePositions[1] = value;

                    _actualJointsUnitsPositions[1] = _actualJointsPulsePositions[1] / LJointRatio;

                    _lJointPulseInHome = _jointsInHomeArray[1] = (_actualJointsPulsePositions[1] == _homeJointsPulsePositions[1]);
                }
            }
        }
        public bool LJointPulseInMotion
        {
            get
            {
                lock (_lAxisPulseLocker)
                {
                    return _jointsInMotionArray[1];
                }
            }
        }
        public bool LJointPulseInHome
        {
            get
            {
                lock (_lAxisPulseLocker)
                {
                    return _lJointPulseInHome;
                }
            }
        }
        public double MinLJointPulsePosition
        {
            get
            {
                lock (_lAxisPulseLocker)
                {
                    return _minLJointPulsePosition;
                }
            }
            set
            {
                lock (_lAxisPulseLocker)
                {
                    if (value >= (-105.0 * LJointRatio) && value <= (155.0 * LJointRatio))
                    {
                        _limitsJointsPulse[1][0] = _minLJointPulsePosition = value;
                        _limitsJointsUnits[1][0] = _limitsJointsPulse[1][0] / LJointRatio;
                    }
                }
            }
        }
        public double MaxLJointPulsePosition
        {
            get
            {
                lock (_lAxisPulseLocker)
                {
                    return _maxLJointPulsePosition;
                }
            }
            set
            {
                lock (_lAxisPulseLocker)
                {
                    if (value >= (-105.0 * LJointRatio) && value <= (155.0 * LJointRatio))
                    {
                        _limitsJointsPulse[1][1] = _maxLJointPulsePosition = value;
                        _limitsJointsUnits[1][1] = _limitsJointsPulse[1][1] / LJointRatio;
                    }
                }
            }
        }
        public double LJointPulseHomePosition
        {
            get
            {
                lock (_lAxisPulseLocker)
                {
                    return _lJointPulseHomePosition;

                }
            }
            set
            {
                lock (_lAxisPulseLocker)
                {
                    _homeJointsPulsePositions[1] = _lJointPulseHomePosition = value;
                    _homeJointsUnitsPositions[1] = _homeJointsPulsePositions[1] / LJointRatio;
                }
            }
        }

        public double ActualUJointPulsePosition
        {
            get
            {
                lock (_uAxisPulseLocker)
                {
                    return _actualJointsPulsePositions[2];
                }
            }
            set
            {
                lock (_uAxisPulseLocker)
                {
                    _jointsInMotionArray[2] = (_actualJointsPulsePositions[2] != value);

                    _actualUJointPulsePosition = _actualJointsPulsePositions[2] = value;

                    _actualJointsUnitsPositions[2] = _actualJointsPulsePositions[2] / UJointRatio;

                    _uJointPulseInHome = _jointsInHomeArray[2] = (_actualJointsPulsePositions[2] == _homeJointsPulsePositions[2]);
                }
            }
        }
        public bool UJointPulseInMotion
        {
            get
            {
                lock (_uAxisPulseLocker)
                {
                    return _jointsInMotionArray[2];
                }
            }
        }
        public bool UJointPulseInHome
        {
            get
            {
                lock (_uAxisPulseLocker)
                {
                    return _uJointPulseInHome;
                }
            }
        }
        public double MinUJointPulsePosition
        {
            get
            {
                lock (_uAxisPulseLocker)
                {
                    return _minUJointPulsePosition;
                }
            }
            set
            {
                lock (_uAxisPulseLocker)
                {
                    if (value >= (-170.0 * UJointRatio) && value <= (240.0 * UJointRatio))
                    {
                        _limitsJointsPulse[2][0] = _minUJointPulsePosition = value;
                        _limitsJointsUnits[2][0] = _limitsJointsPulse[2][0] / UJointRatio;
                    }
                }
            }
        }
        public double MaxUJointPulsePosition
        {
            get
            {
                lock (_uAxisPulseLocker)
                {
                    return _maxUJointPulsePosition;

                }
            }
            set
            {
                lock (_uAxisPulseLocker)
                {
                    if (value >= (-170.0 * UJointRatio) && value <= (240.0 * UJointRatio))
                    {
                        _limitsJointsPulse[2][1] = _maxUJointPulsePosition = value;
                        _limitsJointsUnits[2][1] = _limitsJointsPulse[2][1] / UJointRatio;
                    }
                }
            }
        }
        public double UJointPulseHomePosition
        {
            get
            {
                lock (_uAxisPulseLocker)
                {
                    return _uJointPulseHomePosition;

                }
            }
            set
            {
                lock (_uAxisPulseLocker)
                {
                    _homeJointsPulsePositions[2] = _uJointPulseHomePosition = value;
                    _homeJointsUnitsPositions[2] = _homeJointsPulsePositions[2] / LJointRatio;
                }
            }
        }

        public double ActualRJointPulsePosition
        {
            get
            {
                lock (_rAxisPulseLocker)
                {
                    return _actualJointsPulsePositions[3];
                }
            }
            set
            {
                lock (_rAxisPulseLocker)
                {
                    _jointsInMotionArray[3] = (_actualJointsPulsePositions[3] != value);

                    _actualRJointPulsePosition = _actualJointsPulsePositions[3] = value;

                    _actualJointsUnitsPositions[3] = _actualJointsPulsePositions[3] / RJointRatio;

                    _rJointPulseInHome = _jointsInHomeArray[3] = (_actualJointsPulsePositions[3] == _homeJointsPulsePositions[3]);
                }
            }
        }
        public bool RJointPulseInMotion
        {
            get
            {
                lock (_rAxisPulseLocker)
                {
                    return _jointsInMotionArray[3];
                }
            }
        }
        public bool RJointPulseInHome
        {
            get
            {
                lock (_rAxisPulseLocker)
                {
                    return _rJointPulseInHome;
                }
            }
        }
        public double MinRJointPulsePosition
        {
            get
            {
                lock (_rAxisPulseLocker)
                {
                    return _minRJointPulsePosition;
                }
            }
            set
            {
                lock (_rAxisPulseLocker)
                {
                    if (value >= (-200.0 * RJointRatio) && value <= (200.0 * RJointRatio))
                    {
                        _limitsJointsPulse[3][0] = _minRJointPulsePosition = value;
                        _limitsJointsUnits[3][0] = _limitsJointsPulse[3][0] / RJointRatio;
                    }
                }
            }
        }
        public double MaxRJointPulsePosition
        {
            get
            {
                lock (_rAxisPulseLocker)
                {
                    return _maxRJointPulsePosition;

                }
            }
            set
            {
                lock (_rAxisPulseLocker)
                {
                    if (value >= (-200.0 * RJointRatio) && value <= (200.0 * RJointRatio))
                    {
                        _limitsJointsPulse[3][1] = _maxRJointPulsePosition = value;
                        _limitsJointsUnits[3][1] = _limitsJointsPulse[3][1] / RJointRatio;
                    }
                }
            }
        }
        public double RJointPulseHomePosition
        {
            get
            {
                lock (_rAxisPulseLocker)
                {
                    return _rJointPulseHomePosition;

                }
            }
            set
            {
                lock (_rAxisPulseLocker)
                {
                    _homeJointsPulsePositions[3] = _rJointPulseHomePosition = value;
                    _homeJointsUnitsPositions[3] = _homeJointsPulsePositions[3] / RJointRatio;
                }
            }
        }

        public double ActualBJointPulsePosition
        {
            get
            {
                lock (_bAxisPulseLocker)
                {
                    return _actualJointsPulsePositions[4];
                }
            }
            set
            {
                lock (_bAxisPulseLocker)
                {
                    _jointsInMotionArray[4] = (_actualJointsPulsePositions[4] != value);

                    _actualBJointPulsePosition = _actualJointsPulsePositions[4] = value;

                    _actualJointsUnitsPositions[4] = _actualJointsPulsePositions[4] / BJointRatio;

                    _bJointPulseInHome = _jointsInHomeArray[4] = (_actualJointsPulsePositions[4] == _homeJointsPulsePositions[4]);
                }
            }
        }
        public bool BJointPulseInMotion
        {
            get
            {
                lock (_bAxisPulseLocker)
                {
                    return _jointsInMotionArray[4];
                }
            }
        }
        public bool BJointPulseInHome
        {
            get
            {
                lock (_bAxisPulseLocker)
                {
                    return _bJointPulseInHome;
                }
            }
        }
        public double MinBJointPulsePosition
        {
            get
            {
                lock (_bAxisPulseLocker)
                {
                    return _minBJointPulsePosition;
                }
            }
            set
            {
                lock (_bAxisPulseLocker)
                {
                    if (value >= (-150.0 * BJointRatio) && value <= (150.0 * BJointRatio))
                    {
                        _limitsJointsPulse[4][0] = _minBJointPulsePosition = value;
                        _limitsJointsUnits[4][0] = _limitsJointsPulse[4][0] / BJointRatio;
                    }
                }
            }
        }
        public double MaxBJointPulsePosition
        {
            get
            {
                lock (_bAxisPulseLocker)
                {
                    return _maxBJointPulsePosition;

                }
            }
            set
            {
                lock (_bAxisPulseLocker)
                {
                    if (value >= (-150.0 * BJointRatio) && value <= (150.0 * BJointRatio))
                    {
                        _limitsJointsPulse[4][0] = _minBJointPulsePosition = value;
                        _limitsJointsUnits[4][0] = _limitsJointsPulse[4][0] / BJointRatio;
                    }
                }
            }
        }
        public double BJointPulseHomePosition
        {
            get
            {
                lock (_bAxisPulseLocker)
                {
                    return _bJointPulseHomePosition;

                }
            }
            set
            {
                lock (_bAxisPulseLocker)
                {
                    _homeJointsPulsePositions[4] = _bJointPulseHomePosition = value;
                    _homeJointsUnitsPositions[4] = _homeJointsPulsePositions[4] / BJointRatio;
                }
            }
        }

        public double ActualTJointPulsePosition
        {
            get
            {
                lock (_tAxisPulseLocker)
                {
                    return _actualJointsPulsePositions[5];
                }
            }
            set
            {
                lock (_tAxisPulseLocker)
                {
                    _jointsInMotionArray[5] = (_actualJointsPulsePositions[5] != value);

                    _actualTJointPulsePosition = _actualJointsPulsePositions[5] = value;

                    _actualJointsUnitsPositions[5] = _actualJointsPulsePositions[5] / TJointRatio;

                    _tJointPulseInHome = _jointsInHomeArray[5] = (_actualJointsPulsePositions[5] == _homeJointsPulsePositions[5]);
                }
            }
        }
        public bool TJointPulseInMotion
        {
            get
            {
                lock (_tAxisPulseLocker)
                {
                    return _jointsInMotionArray[5];
                }
            }
        }
        public bool TJointPulseInHome
        {
            get
            {
                lock (_tAxisPulseLocker)
                {
                    return _tJointPulseInHome;
                }
            }
        }
        public double MinTJointPulsePosition
        {
            get
            {
                lock (_tAxisPulseLocker)
                {
                    return _minTJointPulsePosition;
                }
            }
            set
            {
                lock (_tAxisPulseLocker)
                {
                    if (value >= (-455.0 * TJointRatio) && value <= (455.0 * TJointRatio))
                    {
                        _limitsJointsPulse[5][0] = _minTJointPulsePosition = value;
                        _limitsJointsUnits[5][0] = _limitsJointsPulse[5][0] / TJointRatio;
                    }
                }
            }
        }
        public double MaxTJointPulsePosition
        {
            get
            {
                lock (_tAxisPulseLocker)
                {
                    return _maxTJointPulsePosition;

                }
            }
            set
            {
                lock (_tAxisPulseLocker)
                {
                    if (value >= (-455.0 * TJointRatio) && value <= (455.0 * TJointRatio))
                    {
                        _limitsJointsPulse[5][0] = _minTJointPulsePosition = value;
                        _limitsJointsUnits[5][0] = _limitsJointsPulse[5][0] / TJointRatio;
                    }
                }
            }
        }
        public double TJointPulseHomePosition
        {
            get
            {
                lock (_tAxisPulseLocker)
                {
                    return _tJointPulseHomePosition;

                }
            }
            set
            {
                lock (_tAxisPulseLocker)
                {
                    _homeJointsPulsePositions[5] = _tJointPulseHomePosition = value;
                    _homeJointsUnitsPositions[5] = _homeJointsPulsePositions[5] / TJointRatio;
                }
            }
        }

        public double ActualE7AxisPulsePosition
        {
            get
            {
                lock (_e7AxisPulseLocker)
                {
                    return _actualJointsPulsePositions[6];
                }
            }
            set
            {
                lock (_e7AxisPulseLocker)
                {
                    _jointsInMotionArray[6] = (_actualJointsPulsePositions[6] != value);

                    _actualE7AxisPulsePosition = _actualJointsPulsePositions[6] = value;

                    _actualJointsUnitsPositions[6] = _actualJointsPulsePositions[6] / E7AxisRatio;

                    _e7AxisPulseInHome = _jointsInHomeArray[6] = (_actualJointsPulsePositions[6] == _homeJointsPulsePositions[6]);
                }
            }
        }
        public bool E7AxisPulseInMotion
        {
            get
            {
                lock (_e7AxisPulseLocker)
                {
                    return _jointsInMotionArray[6];
                }
            }
        }
        public bool E7AxisPulseInHome
        {
            get
            {
                lock (_e7AxisPulseLocker)
                {
                    return _e7AxisPulseInHome;
                }
            }
        }
        public double MinE7AxisPulsePosition
        {
            get
            {
                lock (_e7AxisPulseLocker)
                {
                    return _minE7AxisPulsePosition;
                }
            }
            set
            {
                lock (_e7AxisPulseLocker)
                {
                    if (value >= (0.5 * E7AxisRatio) && value <= (4500.0 * E7AxisRatio))
                    {
                        _limitsJointsPulse[6][0] = _minE7AxisPulsePosition = value;
                        _limitsJointsUnits[6][0] = _limitsJointsPulse[6][0] / E7AxisRatio;
                    }
                }
            }
        }
        public double MaxE7AxisPulsePosition
        {
            get
            {
                lock (_e7AxisPulseLocker)
                {
                    return _maxE7AxisPulsePosition;

                }
            }
            set
            {
                lock (_e7AxisPulseLocker)
                {
                    if (value >= (0.5 * E7AxisRatio) && value <= (4500.0 * E7AxisRatio))
                    {
                        _limitsJointsPulse[6][0] = _minE7AxisPulsePosition = value;
                        _limitsJointsUnits[6][0] = _limitsJointsPulse[6][0] / E7AxisRatio;
                    }
                }
            }
        }
        public double E7AxisPulseHomePosition
        {
            get
            {
                lock (_e7AxisPulseLocker)
                {
                    return _e7AxisPulseHomePosition;

                }
            }
            set
            {
                lock (_e7AxisPulseLocker)
                {
                    _homeJointsPulsePositions[6] = _e7AxisPulseHomePosition = value;
                    _homeJointsUnitsPositions[6] = _homeJointsPulsePositions[6] / E7AxisRatio;
                }
            }
        }

        public double ActualE8AxisPulsePosition
        {
            get
            {
                lock (_e8AxisPulseLocker)
                {
                    return _actualJointsPulsePositions[7];
                }
            }
            set
            {
                lock (_e8AxisPulseLocker)
                {
                    _jointsInMotionArray[7] = (_actualJointsPulsePositions[7] != value);

                    _actualE8AxisPulsePosition = _actualJointsPulsePositions[7] = value;

                    _actualJointsUnitsPositions[7] = _actualJointsPulsePositions[7] / E8AxisRatio;

                    _e8AxisPulseInHome = _jointsInHomeArray[7] = (_actualJointsPulsePositions[7] == _homeJointsPulsePositions[7]);
                }
            }
        }
        public bool E8AxisPulseInMotion
        {
            get
            {
                lock (_e8AxisPulseLocker)
                {
                    return _jointsInMotionArray[7];
                }
            }
        }
        public bool E8AxisPulseInHome
        {
            get
            {
                lock (_e8AxisPulseLocker)
                {
                    return _e8AxisPulseInHome;
                }
            }
        }
        public double MinE8AxisPulsePosition
        {
            get
            {
                lock (_e8AxisPulseLocker)
                {
                    return _minE8AxisPulsePosition;
                }
            }
            set
            {
                lock (_e8AxisPulseLocker)
                {
                    if (value >= (-360.0 * E8AxisRatio) && value <= (360.0 * E8AxisRatio))
                    {
                        _limitsJointsPulse[7][0] = _minE8AxisPulsePosition = value;
                        _limitsJointsUnits[7][0] = _limitsJointsPulse[7][0] / E8AxisRatio;
                    }
                }
            }
        }
        public double MaxE8AxisPulsePosition
        {
            get
            {
                lock (_e8AxisPulseLocker)
                {
                    return _maxE8AxisPulsePosition;

                }
            }
            set
            {
                lock (_e8AxisPulseLocker)
                {
                    if (value >= (-360.0 * E8AxisRatio) && value <= (360.0 * E8AxisRatio))
                    {
                        _limitsJointsPulse[7][0] = _minE8AxisPulsePosition = value;
                        _limitsJointsUnits[7][0] = _limitsJointsPulse[7][0] / E8AxisRatio;
                    }
                }
            }
        }
        public double E8AxisPulseHomePosition
        {
            get
            {
                lock (_e8AxisPulseLocker)
                {
                    return _e8AxisPulseHomePosition;

                }
            }
            set
            {
                lock (_e8AxisPulseLocker)
                {
                    _homeJointsPulsePositions[7] = _e8AxisPulseHomePosition = value;
                    _homeJointsUnitsPositions[7] = _homeJointsPulsePositions[7] / E8AxisRatio;
                }
            }
        }

        public double ActualE9AxisPulsePosition
        {
            get
            {
                lock (_e9AxisPulseLocker)
                {
                    return _actualJointsPulsePositions[8];
                }
            }
            set
            {
                lock (_e9AxisPulseLocker)
                {
                    _jointsInMotionArray[8] = (_actualJointsPulsePositions[8] != value);

                    _actualE9AxisPulsePosition = _actualJointsPulsePositions[8] = value;

                    _actualJointsUnitsPositions[8] = _actualJointsPulsePositions[8] / E9AxisRatio;

                    _e9AxisPulseInHome = _jointsInHomeArray[8] = (_actualJointsPulsePositions[8] == _homeJointsPulsePositions[8]);
                }
            }
        }
        public bool E9AxisPulseInMotion
        {
            get
            {
                lock (_e9AxisPulseLocker)
                {
                    return _jointsInMotionArray[8];
                }
            }
        }
        public bool E9AxisPulseInHome
        {
            get
            {
                lock (_e9AxisPulseLocker)
                {
                    return _e9AxisPulseInHome;
                }
            }
        }
        public double MinE9AxisPulsePosition
        {
            get
            {
                lock (_e9AxisPulseLocker)
                {
                    return _minE9AxisPulsePosition;
                }
            }
            set
            {
                lock (_e9AxisPulseLocker)
                {
                    if (value >= (-360.0 * E9AxisRatio) && value <= (360.0 * E9AxisRatio))
                    {
                        _limitsJointsPulse[8][0] = _minE9AxisPulsePosition = value;
                        _limitsJointsUnits[8][0] = _limitsJointsPulse[8][0] / E9AxisRatio;
                    }
                }
            }
        }
        public double MaxE9AxisPulsePosition
        {
            get
            {
                lock (_e9AxisPulseLocker)
                {
                    return _maxE9AxisPulsePosition;

                }
            }
            set
            {
                lock (_e9AxisPulseLocker)
                {
                    if (value >= (-360.0 * E9AxisRatio) && value <= (360.0 * E9AxisRatio))
                    {
                        _limitsJointsPulse[8][0] = _minE9AxisPulsePosition = value;
                        _limitsJointsUnits[8][0] = _limitsJointsPulse[8][0] / E9AxisRatio;
                    }
                }
            }
        }
        public double E9AxisPulseHomePosition
        {
            get
            {
                lock (_e9AxisPulseLocker)
                {
                    return _e9AxisPulseHomePosition;

                }
            }
            set
            {
                lock (_e9AxisPulseLocker)
                {
                    _homeJointsPulsePositions[8] = _e9AxisPulseHomePosition = value;
                    _homeJointsUnitsPositions[8] = _homeJointsPulsePositions[8] / E9AxisRatio;
                }
            }
        }

        public double ActualE10AxisPulsePosition
        {
            get
            {
                lock (_e10AxisPulseLocker)
                {
                    return _actualJointsPulsePositions[9];
                }
            }
            set
            {
                lock (_e10AxisPulseLocker)
                {
                    _jointsInMotionArray[9] = (_actualJointsPulsePositions[9] != value);

                    _actualE10AxisPulsePosition = _actualJointsPulsePositions[9] = value;

                    _actualJointsUnitsPositions[9] = _actualJointsPulsePositions[9] / E10AxisRatio;

                    _e10AxisPulseInHome = _jointsInHomeArray[9] = (_actualJointsPulsePositions[9] == _homeJointsPulsePositions[9]);
                }
            }
        }
        public bool E10AxisPulseInMotion
        {
            get
            {
                lock (_e10AxisPulseLocker)
                {
                    return _jointsInMotionArray[9];
                }
            }
        }
        public bool E10AxisPulseInHome
        {
            get
            {
                lock (_e10AxisPulseLocker)
                {
                    return _e10AxisPulseInHome;
                }
            }
        }
        public double MinE10AxisPulsePosition
        {
            get
            {
                lock (_e10AxisPulseLocker)
                {
                    return _minE10AxisPulsePosition;
                }
            }
            set
            {
                lock (_e10AxisPulseLocker)
                {
                    if (value >= (-360.0 * E10AxisRatio) && value <= (360.0 * E10AxisRatio))
                    {
                        _limitsJointsPulse[9][0] = _minE10AxisPulsePosition = value;
                        _limitsJointsUnits[9][0] = _limitsJointsPulse[9][0] / E10AxisRatio;
                    }
                }
            }
        }
        public double MaxE10AxisPulsePosition
        {
            get
            {
                lock (_e10AxisPulseLocker)
                {
                    return _maxE10AxisPulsePosition;

                }
            }
            set
            {
                lock (_e10AxisPulseLocker)
                {
                    if (value >= (-360.0 * E10AxisRatio) && value <= (360.0 * E10AxisRatio))
                    {
                        _limitsJointsPulse[9][0] = _minE10AxisPulsePosition = value;
                        _limitsJointsUnits[9][0] = _limitsJointsPulse[9][0] / E10AxisRatio;
                    }
                }
            }
        }
        public double E10AxisPulseHomePosition
        {
            get
            {
                lock (_e10AxisPulseLocker)
                {
                    return _e10AxisPulseHomePosition;

                }
            }
            set
            {
                lock (_e10AxisPulseLocker)
                {
                    _homeJointsPulsePositions[9] = _e10AxisPulseHomePosition = value;
                    _homeJointsUnitsPositions[9] = _homeJointsPulsePositions[9] / E10AxisRatio;
                }
            }
        }

        public double ActualE11AxisPulsePosition
        {
            get
            {
                lock (_e11AxisPulseLocker)
                {
                    return _actualJointsPulsePositions[10];
                }
            }
            set
            {
                lock (_e11AxisPulseLocker)
                {
                    _jointsInMotionArray[10] = (_actualJointsPulsePositions[10] != value);

                    _actualE11AxisPulsePosition = _actualJointsPulsePositions[10] = value;

                    _actualJointsUnitsPositions[10] = _actualJointsPulsePositions[10] / E11AxisRatio;

                    _e11AxisPulseInHome = _jointsInHomeArray[10] = (_actualJointsPulsePositions[10] == _homeJointsPulsePositions[10]);
                }
            }
        }
        public bool E11AxisPulseInMotion
        {
            get
            {
                lock (_e11AxisPulseLocker)
                {
                    return _jointsInMotionArray[10];
                }
            }
        }
        public bool E11AxisPulseInHome
        {
            get
            {
                lock (_e11AxisPulseLocker)
                {
                    return _e11AxisPulseInHome;
                }
            }
        }
        public double MinE11AxisPulsePosition
        {
            get
            {
                lock (_e11AxisPulseLocker)
                {
                    return _minE11AxisPulsePosition;
                }
            }
            set
            {
                lock (_e11AxisPulseLocker)
                {
                    if (value >= (-360.0 * E11AxisRatio) && value <= (360.0 * E11AxisRatio))
                    {
                        _limitsJointsPulse[10][0] = _minE11AxisPulsePosition = value;
                        _limitsJointsUnits[10][0] = _limitsJointsPulse[10][0] / E11AxisRatio;
                    }
                }
            }
        }
        public double MaxE11AxisPulsePosition
        {
            get
            {
                lock (_e11AxisPulseLocker)
                {
                    return _maxE11AxisPulsePosition;

                }
            }
            set
            {
                lock (_e11AxisPulseLocker)
                {
                    if (value >= (-360.0 * E11AxisRatio) && value <= (360.0 * E11AxisRatio))
                    {
                        _limitsJointsPulse[10][0] = _minE11AxisPulsePosition = value;
                        _limitsJointsUnits[10][0] = _limitsJointsPulse[10][0] / E11AxisRatio;
                    }
                }
            }
        }
        public double E11AxisPulseHomePosition
        {
            get
            {
                lock (_e11AxisPulseLocker)
                {
                    return _e11AxisPulseHomePosition;

                }
            }
            set
            {
                lock (_e11AxisPulseLocker)
                {
                    _homeJointsPulsePositions[10] = _e11AxisPulseHomePosition = value;
                    _homeJointsUnitsPositions[10] = _homeJointsPulsePositions[10] / E11AxisRatio;
                }
            }
        }

        public double ActualE12AxisPulsePosition
        {
            get
            {
                lock (_e12AxisPulseLocker)
                {
                    return _actualJointsPulsePositions[11];
                }
            }
            set
            {
                lock (_e12AxisPulseLocker)
                {
                    _jointsInMotionArray[11] = (_actualJointsPulsePositions[11] != value);

                    _actualE12AxisPulsePosition = _actualJointsPulsePositions[11] = value;

                    _actualJointsUnitsPositions[11] = _actualJointsPulsePositions[11] / E12AxisRatio;

                    _e12AxisPulseInHome = _jointsInHomeArray[11] = (_actualJointsPulsePositions[11] == _homeJointsPulsePositions[11]);
                }
            }
        }
        public bool E12AxisPulseInMotion
        {
            get
            {
                lock (_e12AxisPulseLocker)
                {
                    return _jointsInMotionArray[11];
                }
            }
        }
        public bool E12AxisPulseInHome
        {
            get
            {
                lock (_e12AxisPulseLocker)
                {
                    return _e12AxisPulseInHome;
                }
            }
        }
        public double MinE12AxisPulsePosition
        {
            get
            {
                lock (_e12AxisPulseLocker)
                {
                    return _minE12AxisPulsePosition;
                }
            }
            set
            {
                lock (_e12AxisPulseLocker)
                {
                    if (value >= (-360.0 * E12AxisRatio) && value <= (360.0 * E12AxisRatio))
                    {
                        _limitsJointsPulse[11][0] = _minE12AxisPulsePosition = value;
                        _limitsJointsUnits[11][0] = _limitsJointsPulse[11][0] / E12AxisRatio;
                    }
                }
            }
        }
        public double MaxE12AxisPulsePosition
        {
            get
            {
                lock (_e12AxisPulseLocker)
                {
                    return _maxE12AxisPulsePosition;

                }
            }
            set
            {
                lock (_e12AxisPulseLocker)
                {
                    if (value >= (-360.0 * E12AxisRatio) && value <= (360.0 * E12AxisRatio))
                    {
                        _limitsJointsPulse[11][0] = _minE12AxisPulsePosition = value;
                        _limitsJointsUnits[11][0] = _limitsJointsPulse[11][0] / E12AxisRatio;
                    }
                }
            }
        }
        public double E12AxisPulseHomePosition
        {
            get
            {
                lock (_e12AxisPulseLocker)
                {
                    return _e12AxisPulseHomePosition;

                }
            }
            set
            {
                lock (_e12AxisPulseLocker)
                {
                    _homeJointsPulsePositions[11] = _e12AxisPulseHomePosition = value;
                    _homeJointsUnitsPositions[11] = _homeJointsPulsePositions[11] / E12AxisRatio;
                }
            }
        }

        #endregion

        #endregion

        #region TCP

        public double[][] LimitsTCP
        {
            get
            {
                return _limitsTCP;
            }
            set
            {
                _limitsTCP = value;
            }
        }

        public bool[] TCPInMotionArray
        {
            get
            {
                return _tcpInMotionArray;
            }
            set
            {
                _tcpInMotionArray = value;
            }
        }

        public double[] ActualTCPPositions
        {
            get
            {
                return _actualTCPPositions;
            }
            set
            {
                #region
                //TODO:Vlad:Check why don't work
                _tcpInMotionArray[0] = (_actualTCPPositions[0] != value[0]);
                _tcpInMotionArray[1] = (_actualTCPPositions[1] != value[1]);
                _tcpInMotionArray[2] = (_actualTCPPositions[2] != value[2]);
                _tcpInMotionArray[3] = (_actualTCPPositions[3] != value[3]);
                _tcpInMotionArray[4] = (_actualTCPPositions[4] != value[4]);
                _tcpInMotionArray[5] = (_actualTCPPositions[5] != value[5]);
                _tcpInMotionArray[6] = (_actualTCPPositions[6] != value[6]);
                _tcpInMotionArray[7] = (_actualTCPPositions[7] != value[7]);
                _tcpInMotionArray[8] = (_actualTCPPositions[8] != value[8]);
                _tcpInMotionArray[9] = (_actualTCPPositions[9] != value[9]);
                _tcpInMotionArray[10] = (_actualTCPPositions[10] != value[10]);
                _tcpInMotionArray[11] = (_actualTCPPositions[11] != value[11]);

                _actualTCPPositions = value;

                _actualXAxisPosition = _actualTCPPositions[0];
                _actualYAxisPosition = _actualTCPPositions[1];
                _actualZAxisPosition = _actualTCPPositions[2];
                _actualRxAxisPosition = _actualTCPPositions[3];
                _actualRyAxisPosition = _actualTCPPositions[4];
                _actualRzAxisPosition = _actualTCPPositions[5];
                _actualE7AxisPosition = _actualTCPPositions[6];
                _actualE8AxisPosition = _actualTCPPositions[7];
                _actualE9AxisPosition = _actualTCPPositions[8];
                _actualE10AxisPosition = _actualTCPPositions[9];
                _actualE11AxisPosition = _actualTCPPositions[10];
                _actualE12AxisPosition = _actualTCPPositions[11];
                #endregion
            }
        }
        public double[] TCPHomePositions
        {
            get
            {
                lock (_homeTCPPositionsLocker)
                {
                    return _homeTCPPositions;
                }
            }
            set
            {
                lock (_homeTCPPositionsLocker)
                {
                    #region

                    _homeTCPPositions = value;

                    _xAxisHomePosition = _homeTCPPositions[0];
                    _yAxisHomePosition = _homeTCPPositions[1];
                    _zAxisHomePosition = _homeTCPPositions[2];
                    _rxAxisHomePosition = _homeTCPPositions[3];
                    _ryAxisHomePosition = _homeTCPPositions[4];
                    _rzAxisHomePosition = _homeTCPPositions[5];
                    _e7AxisHomePosition = _homeTCPPositions[6];
                    _e8AxisHomePosition = _homeTCPPositions[7];
                    _e9AxisHomePosition = _homeTCPPositions[8];
                    _e10AxisHomePosition = _homeTCPPositions[9];
                    _e11AxisHomePosition = _homeTCPPositions[10];
                    _e12AxisHomePosition = _homeTCPPositions[11];

                    #endregion
                }
            }
        }
        public double[] TCPParkPositions
        {
            get
            {
                lock (_parkTCPPositionsLocker)
                {
                    return _parkTCPPositions;
                }
            }
            set
            {
                lock (_parkTCPPositionsLocker)
                {
                    _parkTCPPositions = value;
                }
            }
        }

        public double X
        {
            get
            {
                return _actualTCPPositions[0];
            }
            set
            {
                lock (_xAxisLocker)
                {
                    _tcpInMotionArray[0] = (_actualTCPPositions[0] != value);

                    _actualXAxisPosition = _actualTCPPositions[0] = value;

                    _xAxisInHome = (_actualXAxisPosition == _homeTCPPositions[0]);
                }
            }
        }
        public bool XAxisInMotion
        {
            get
            {
                lock (_xAxisLocker)
                {
                    return _tcpInMotionArray[0];
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
                    if (value >= -100000.0 && value <= 100000.0)
                    {
                        _limitsTCP[0][0] = _minXAxisPosition = value;
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
                    if (value >= -100000.0 && value <= 100000.0)
                    {
                        _limitsTCP[0][1] = _maxXAxisPosition = value;
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
                    _homeTCPPositions[0] = _xAxisHomePosition = value;
                }
            }
        }

        public double Y
        {
            get
            {
                return _actualTCPPositions[1];
            }
            set
            {
                lock (_yAxisLocker)
                {
                    _tcpInMotionArray[1] = (_actualTCPPositions[1] != value);

                    _actualYAxisPosition = _actualTCPPositions[1] = value;

                    _yAxisInHome = (_actualYAxisPosition == _homeTCPPositions[1]);
                }
            }
        }
        public bool YAxisInMotion
        {
            get
            {
                lock (_yAxisLocker)
                {
                    return _tcpInMotionArray[1];
                }
            }
        }
        public bool YAxisInHome
        {
            get
            {
                lock (_yAxisLocker)
                {
                    return _yAxisInHome;
                }
            }
        }
        public double MinYAxisPosition
        {
            get
            {
                lock (_yAxisLocker)
                {
                    return _minYAxisPosition;
                }
            }
            set
            {
                lock (_yAxisLocker)
                {
                    if (value >= -100000.0 && value <= 100000.0)
                    {
                        _limitsTCP[1][0] = _minYAxisPosition = value;
                    }
                }
            }
        }
        public double MaxYAxisPosition
        {
            get
            {
                lock (_yAxisLocker)
                {
                    return _maxYAxisPosition;

                }
            }
            set
            {
                lock (_yAxisLocker)
                {
                    if (value >= -100000.0 && value <= 100000.0)
                    {
                        _limitsTCP[1][1] = _maxYAxisPosition = value;
                    }
                }
            }
        }
        public double YAxisHomePosition
        {
            get
            {
                lock (_yAxisLocker)
                {
                    return _yAxisHomePosition;

                }
            }
            set
            {
                lock (_yAxisLocker)
                {
                    _homeTCPPositions[1] = _yAxisHomePosition = value;
                }
            }
        }

        public double Z
        {
            get
            {
                return _actualTCPPositions[2];
            }
            set
            {
                lock (_zAxisLocker)
                {
                    _tcpInMotionArray[2] = (_actualTCPPositions[2] != value);

                    _actualZAxisPosition = _actualTCPPositions[2] = value;

                    _zAxisInHome = (_actualZAxisPosition == _homeTCPPositions[2]);
                }
            }
        }
        public bool ZAxisInMotion
        {
            get
            {
                lock (_zAxisLocker)
                {
                    return _tcpInMotionArray[2];
                }
            }
        }
        public bool ZAxisInHome
        {
            get
            {
                lock (_zAxisLocker)
                {
                    return _zAxisInHome;
                }
            }
        }
        public double MinZAxisPosition
        {
            get
            {
                lock (_zAxisLocker)
                {
                    return _minZAxisPosition;
                }
            }
            set
            {
                lock (_zAxisLocker)
                {
                    if (value >= -100000.0 && value <= 100000.0)
                    {
                        _limitsTCP[2][0] = _minZAxisPosition = value;
                    }
                }
            }
        }
        public double MaxZAxisPosition
        {
            get
            {
                lock (_zAxisLocker)
                {
                    return _maxZAxisPosition;

                }
            }
            set
            {
                lock (_zAxisLocker)
                {
                    if (value >= -100000.0 && value <= 100000.0)
                    {
                        _limitsTCP[2][1] = _maxZAxisPosition = value;
                    }
                }
            }
        }
        public double ZAxisHomePosition
        {
            get
            {
                lock (_zAxisLocker)
                {
                    return _zAxisHomePosition;
                }
            }
            set
            {
                lock (_zAxisLocker)
                {
                    _homeTCPPositions[2] = _zAxisHomePosition = value;
                }
            }
        }

        public double Rx
        {
            get
            {
                return _actualTCPPositions[3];
            }
            set
            {
                lock (_rxAxisLocker)
                {
                    _tcpInMotionArray[3] = (_actualTCPPositions[3] != value);

                    _actualRxAxisPosition = _actualTCPPositions[3] = value;

                    _rxAxisInHome = (_actualRxAxisPosition == _homeTCPPositions[3]);
                }
            }
        }
        public bool RxAxisInMotion
        {
            get
            {
                lock (_rxAxisLocker)
                {
                    return _tcpInMotionArray[3];
                }
            }
        }
        public bool RxAxisInHome
        {
            get
            {
                lock (_rxAxisLocker)
                {
                    return _rxAxisInHome;
                }
            }
        }
        public double MinRxAxisPosition
        {
            get
            {
                lock (_rxAxisLocker)
                {
                    return _minRxAxisPosition;
                }
            }
            set
            {
                lock (_rxAxisLocker)
                {
                    if (value >= -720.0 && value <= 720.0)
                    {
                        _limitsTCP[3][0] = _minRxAxisPosition = value;
                    }
                }
            }
        }
        public double MaxRxAxisPosition
        {
            get
            {
                lock (_rxAxisLocker)
                {
                    return _maxRxAxisPosition;

                }
            }
            set
            {
                lock (_rxAxisLocker)
                {
                    if (value >= -720.0 && value <= 720.0)
                    {
                        _limitsTCP[3][1] = _maxRxAxisPosition = value;
                    }
                }
            }
        }
        public double RxAxisHomePosition
        {
            get
            {
                lock (_rxAxisLocker)
                {
                    return _rxAxisHomePosition;
                }
            }
            set
            {
                lock (_rxAxisLocker)
                {
                    _homeTCPPositions[3] = _rxAxisHomePosition = value;
                }
            }
        }

        public double Ry
        {
            get
            {
                return _actualTCPPositions[4];
            }
            set
            {
                lock (_ryAxisLocker)
                {
                    _tcpInMotionArray[4] = (_actualTCPPositions[4] != value);

                    _actualRyAxisPosition = _actualTCPPositions[4] = value;

                    _ryAxisInHome = (_actualRyAxisPosition == _homeTCPPositions[4]);
                }
            }
        }
        public bool RyAxisInMotion
        {
            get
            {
                lock (_ryAxisLocker)
                {
                    return _tcpInMotionArray[4];
                }
            }
        }
        public bool RyAxisInHome
        {
            get
            {
                lock (_ryAxisLocker)
                {
                    return _ryAxisInHome;
                }
            }
        }
        public double MinRyAxisPosition
        {
            get
            {
                lock (_ryAxisLocker)
                {
                    return _minRyAxisPosition;
                }
            }
            set
            {
                lock (_ryAxisLocker)
                {
                    if (value >= -720.0 && value <= 720.0)
                    {
                        _limitsTCP[4][0] = _minRyAxisPosition = value;
                    }
                }
            }
        }
        public double MaxRyAxisPosition
        {
            get
            {
                lock (_ryAxisLocker)
                {
                    return _maxRyAxisPosition;

                }
            }
            set
            {
                lock (_ryAxisLocker)
                {
                    if (value >= -720.0 && value <= 720.0)
                    {
                        _limitsTCP[4][1] = _maxRyAxisPosition = value;
                    }
                }
            }
        }
        public double RyAxisHomePosition
        {
            get
            {
                lock (_ryAxisLocker)
                {
                    return _ryAxisHomePosition;
                }
            }
            set
            {
                lock (_ryAxisLocker)
                {
                    _homeTCPPositions[4] = _ryAxisHomePosition = value;
                }
            }
        }

        public double Rz
        {
            get
            {
                return _actualTCPPositions[5];
            }
            set
            {
                lock (_rzAxisLocker)
                {
                    _tcpInMotionArray[5] = (_actualTCPPositions[5] != value);

                    _actualRzAxisPosition = _actualTCPPositions[5] = value;

                    _rzAxisInHome = (_actualRzAxisPosition == _homeTCPPositions[5]);
                }
            }
        }
        public bool RzAxisInMotion
        {
            get
            {
                lock (_rzAxisLocker)
                {
                    return _tcpInMotionArray[5];
                }
            }
        }
        public bool RzAxisInHome
        {
            get
            {
                lock (_rzAxisLocker)
                {
                    return _rzAxisInHome;
                }
            }
        }
        public double MinRzAxisPosition
        {
            get
            {
                lock (_rzAxisLocker)
                {
                    return _minRzAxisPosition;
                }
            }
            set
            {
                lock (_rzAxisLocker)
                {
                    if (value >= -720.0 && value <= 720.0)
                    {
                        _limitsTCP[5][0] = _minRzAxisPosition = value;
                    }
                }
            }
        }
        public double MaxRzAxisPosition
        {
            get
            {
                lock (_rzAxisLocker)
                {
                    return _maxRzAxisPosition;

                }
            }
            set
            {
                lock (_rzAxisLocker)
                {
                    if (value >= -720.0 && value <= 720.0)
                    {
                        _limitsTCP[5][1] = _maxRzAxisPosition = value;
                    }
                }
            }
        }
        public double RzAxisHomePosition
        {
            get
            {
                lock (_rzAxisLocker)
                {
                    return _rzAxisHomePosition;
                }
            }
            set
            {
                lock (_rzAxisLocker)
                {
                    _homeTCPPositions[5] = _rzAxisHomePosition = value;
                }
            }
        }

        public double E7Axis
        {
            get
            {
                return _actualTCPPositions[6];
            }
            set
            {
                _tcpInMotionArray[6] = (_actualTCPPositions[6] != value);

                _actualE7AxisPosition = _actualTCPPositions[6] = value;

                _e7AxisInHome = (_actualE7AxisPosition == _homeTCPPositions[6]);
            }
        }
        public bool E7AxisInMotion
        {
            get
            {
                lock (_e7AxisLocker)
                {
                    return _tcpInMotionArray[6];
                }
            }
        }
        public bool E7AxisInHome
        {
            get
            {
                lock (_e7AxisLocker)
                {
                    return _e7AxisInHome;
                }
            }
        }
        public double MinE7AxisPosition
        {
            get
            {
                lock (_e7AxisLocker)
                {
                    return _minE7AxisPosition;
                }
            }
            set
            {
                lock (_e7AxisLocker)
                {
                    _limitsTCP[6][0] = _minE7AxisPosition = value;
                }
            }
        }
        public double MaxE7AxisPosition
        {
            get
            {
                lock (_e7AxisLocker)
                {
                    return _maxE7AxisPosition;

                }
            }
            set
            {
                lock (_e7AxisLocker)
                {
                    _limitsTCP[6][1] = _maxE7AxisPosition = value;
                }
            }
        }
        public double E7AxisHomePosition
        {
            get
            {
                lock (_e7AxisLocker)
                {
                    return _e7AxisHomePosition;
                }
            }
            set
            {
                lock (_e7AxisLocker)
                {
                    _homeTCPPositions[6] = _e7AxisHomePosition = value;
                }
            }
        }

        public double E8Axis
        {
            get
            {
                return _actualTCPPositions[7];
            }
            set
            {
                _tcpInMotionArray[7] = (_actualTCPPositions[7] != value);

                _actualE8AxisPosition = _actualTCPPositions[7] = value;

                _e8AxisInHome = (_actualE8AxisPosition == _homeTCPPositions[7]);
            }
        }
        public bool E8AxisInMotion
        {
            get
            {
                lock (_e8AxisLocker)
                {
                    return _tcpInMotionArray[7];
                }
            }
        }
        public bool E8AxisInHome
        {
            get
            {
                lock (_e8AxisLocker)
                {
                    return _e8AxisInHome;
                }
            }
        }
        public double MinE8AxisPosition
        {
            get
            {
                lock (_e8AxisLocker)
                {
                    return _minE8AxisPosition;
                }
            }
            set
            {
                lock (_e8AxisLocker)
                {
                    _limitsTCP[7][0] = _minE8AxisPosition = value;
                }
            }
        }
        public double MaxE8AxisPosition
        {
            get
            {
                lock (_e8AxisLocker)
                {
                    return _maxE8AxisPosition;
                }
            }
            set
            {
                lock (_e8AxisLocker)
                {
                    _limitsTCP[7][1] = _maxE8AxisPosition = value;
                }
            }
        }
        public double E8AxisHomePosition
        {
            get
            {
                lock (_e8AxisLocker)
                {
                    return _e8AxisHomePosition;
                }
            }
            set
            {
                lock (_e8AxisLocker)
                {
                    _homeTCPPositions[7] = _e8AxisHomePosition = value;
                }
            }
        }

        public double E9Axis
        {
            get
            {
                return _actualTCPPositions[8];
            }
            set
            {
                _tcpInMotionArray[8] = (_actualTCPPositions[8] != value);

                _actualE9AxisPosition = _actualTCPPositions[8] = value;

                _e9AxisInHome = (_actualE9AxisPosition == _homeTCPPositions[8]);
            }
        }
        public bool E9AxisInMotion
        {
            get
            {
                lock (_e9AxisLocker)
                {
                    return _tcpInMotionArray[8];
                }
            }
        }
        public bool E9AxisInHome
        {
            get
            {
                lock (_e9AxisLocker)
                {
                    return _e9AxisInHome;
                }
            }
        }
        public double MinE9AxisPosition
        {
            get
            {
                lock (_e9AxisLocker)
                {
                    return _minE9AxisPosition;
                }
            }
            set
            {
                lock (_e9AxisLocker)
                {
                    _limitsTCP[8][0] = _minE9AxisPosition = value;
                }
            }
        }
        public double MaxE9AxisPosition
        {
            get
            {
                lock (_e9AxisLocker)
                {
                    return _maxE9AxisPosition;
                }
            }
            set
            {
                lock (_e9AxisLocker)
                {
                    _limitsTCP[8][1] = _maxE9AxisPosition = value;
                }
            }
        }
        public double E9AxisHomePosition
        {
            get
            {
                lock (_e9AxisLocker)
                {
                    return _e9AxisHomePosition;
                }
            }
            set
            {
                lock (_e9AxisLocker)
                {
                    _homeTCPPositions[8] = _e9AxisHomePosition = value;
                }
            }
        }

        public double E10Axis
        {
            get
            {
                return _actualTCPPositions[9];
            }
            set
            {
                _tcpInMotionArray[9] = (_actualTCPPositions[9] != value);

                _actualE10AxisPosition = _actualTCPPositions[9] = value;

                _e10AxisInHome = (_actualE10AxisPosition == _homeTCPPositions[9]);
            }
        }
        public bool E10AxisInMotion
        {
            get
            {
                lock (_e10AxisLocker)
                {
                    return _tcpInMotionArray[9];
                }
            }
        }
        public bool E10AxisInHome
        {
            get
            {
                lock (_e10AxisLocker)
                {
                    return _e10AxisInHome;
                }
            }
        }
        public double MinE10AxisPosition
        {
            get
            {
                lock (_e10AxisLocker)
                {
                    return _minE10AxisPosition;
                }
            }
            set
            {
                lock (_e10AxisLocker)
                {
                    _limitsTCP[9][0] = _minE10AxisPosition = value;
                }
            }
        }
        public double MaxE10AxisPosition
        {
            get
            {
                lock (_e10AxisLocker)
                {
                    return _maxE10AxisPosition;
                }
            }
            set
            {
                lock (_e10AxisLocker)
                {
                    _limitsTCP[9][1] = _maxE10AxisPosition = value;
                }
            }
        }
        public double E10AxisHomePosition
        {
            get
            {
                lock (_e10AxisLocker)
                {
                    return _e10AxisHomePosition;
                }
            }
            set
            {
                lock (_e10AxisLocker)
                {
                    _homeTCPPositions[9] = _e10AxisHomePosition = value;
                }
            }
        }

        public double E11Axis
        {
            get
            {
                return _actualTCPPositions[10];
            }
            set
            {
                _tcpInMotionArray[10] = (_actualTCPPositions[10] != value);

                _actualE11AxisPosition = _actualTCPPositions[10] = value;

                _e11AxisInHome = (_actualE11AxisPosition == _homeTCPPositions[10]);
            }
        }
        public bool E11AxisInMotion
        {
            get
            {
                lock (_e11AxisLocker)
                {
                    return _tcpInMotionArray[10];
                }
            }
        }
        public bool E11AxisInHome
        {
            get
            {
                lock (_e11AxisLocker)
                {
                    return _e11AxisInHome;
                }
            }
        }
        public double MinE11AxisPosition
        {
            get
            {
                lock (_e11AxisLocker)
                {
                    return _minE11AxisPosition;
                }
            }
            set
            {
                lock (_e11AxisLocker)
                {
                    _limitsTCP[10][0] = _minE11AxisPosition = value;
                }
            }
        }
        public double MaxE11AxisPosition
        {
            get
            {
                lock (_e11AxisLocker)
                {
                    return _maxE11AxisPosition;
                }
            }
            set
            {
                lock (_e11AxisLocker)
                {
                    _limitsTCP[10][1] = _maxE11AxisPosition = value;
                }
            }
        }
        public double E11AxisHomePosition
        {
            get
            {
                lock (_e11AxisLocker)
                {
                    return _e11AxisHomePosition;
                }
            }
            set
            {
                lock (_e11AxisLocker)
                {
                    _homeTCPPositions[10] = _e11AxisHomePosition = value;
                }
            }
        }

        public double E12Axis
        {
            get
            {
                return _actualTCPPositions[11];
            }
            set
            {
                _tcpInMotionArray[11] = (_actualTCPPositions[11] != value);

                _actualE12AxisPosition = _actualTCPPositions[11] = value;

                _e12AxisInHome = (_actualE12AxisPosition == _homeTCPPositions[11]);
            }
        }
        public bool E12AxisInMotion
        {
            get
            {
                lock (_e12AxisLocker)
                {
                    return _tcpInMotionArray[11];
                }
            }
        }
        public bool E12AxisInHome
        {
            get
            {
                lock (_e12AxisLocker)
                {
                    return _e12AxisInHome;
                }
            }
        }
        public double MinE12AxisPosition
        {
            get
            {
                lock (_e12AxisLocker)
                {
                    return _minE12AxisPosition;
                }
            }
            set
            {
                lock (_e12AxisLocker)
                {
                    _limitsTCP[11][0] = _minE12AxisPosition = value;
                }
            }
        }
        public double MaxE12AxisPosition
        {
            get
            {
                lock (_e12AxisLocker)
                {
                    return _maxE12AxisPosition;
                }
            }
            set
            {
                lock (_e12AxisLocker)
                {
                    _limitsTCP[11][1] = _maxE12AxisPosition = value;
                }
            }
        }
        public double E12AxisHomePosition
        {
            get
            {
                lock (_e12AxisLocker)
                {
                    return _e12AxisHomePosition;
                }
            }
            set
            {
                lock (_e12AxisLocker)
                {
                    _homeTCPPositions[11] = _e12AxisHomePosition = value;
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        public RobotPosition()
        {
            ActualJointsUnitsPositions = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            ActualJointsPulsePositions = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            ActualTCPPositions = new double[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        #endregion
    }
}
