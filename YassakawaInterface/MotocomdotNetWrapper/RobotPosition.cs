using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MotoCom32Net
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
        double LAxis
        {
            get;
            set;
        }
        double UAxis
        {
            get;
            set;
        }
        double RAxis
        {
            get;
            set;
        }
        double BAxis
        {
            get;
            set;
        }
        double TAxis
        {
            get;
            set;
        }
        double X
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

        double[] RobotPositions
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
        volatile double[] _robotPositions = new double[12];

        readonly object _sAxisLocker = new object();
        readonly object _robotPositionsLocker = new object();

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

            _robotPositions.Initialize();
        }
        public RobotPosition(double[] positions)
        {
            positions.CopyTo(_robotPositions, 0);
        }

        public double SAxis
        {
            get
            {
                lock (_sAxisLocker)
                {
                    return _robotPositions[0];
                }
            }
            set
            {
                lock (_sAxisLocker)
                {
                    _robotPositions[0] = value;
                }
            }
        }
        public double LAxis
        {
            get
            {
                return _robotPositions[1];
            }
            set
            {
                _robotPositions[1] = value;
            }
        }
        public double UAxis
        {
            get
            {
                return _robotPositions[2];
            }
            set
            {
                _robotPositions[2] = value;
            }
        }
        public double RAxis
        {
            get
            {
                return _robotPositions[3];
            }
            set
            {
                _robotPositions[3] = value;
            }
        }
        public double BAxis
        {
            get
            {
                return _robotPositions[4];
            }
            set
            {
                _robotPositions[4] = value;
            }
        }
        public double TAxis
        {
            get
            {
                return _robotPositions[5];
            }
            set
            {
                _robotPositions[5] = value;
            }
        }
        public double X
        {
            get
            {
                return _robotPositions[0];
            }
            set
            {
                _robotPositions[0] = value;
            }
        }
        public double Y
        {
            get
            {
                return _robotPositions[1];
            }
            set
            {
                _robotPositions[1] = value;
            }
        }
        public double Z
        {
            get
            {
                return _robotPositions[2];
            }
            set
            {
                _robotPositions[2] = value;
            }
        }
        public double Rx
        {
            get
            {
                return _robotPositions[3];
            }
            set
            {
                _robotPositions[3] = value;
            }
        }
        public double Ry
        {
            get
            {
                return _robotPositions[4];
            }
            set
            {
                _robotPositions[4] = value;
            }
        }
        public double Rz
        {
            get
            {
                return _robotPositions[5];
            }
            set
            {
                _robotPositions[5] = value;
            }
        }
        public double E7Axis
        {
            get
            {
                return _robotPositions[6];
            }
            set
            {
                _robotPositions[6] = value;
            }
        }
        public double E8Axis
        {
            get
            {
                return _robotPositions[7];
            }
            set
            {
                _robotPositions[7] = value;
            }
        }
        public double E9Axis
        {
            get
            {
                return _robotPositions[8];
            }
            set
            {
                _robotPositions[8] = value;
            }
        }
        public double E10Axis
        {
            get
            {
                return _robotPositions[9];
            }
            set
            {
                _robotPositions[9] = value;
            }
        }
        public double E11Axis
        {
            get
            {
                return _robotPositions[10];
            }
            set
            {
                _robotPositions[10] = value;
            }
        }
        public double E12Axis
        {
            get
            {
                return _robotPositions[11];
            }
            set
            {
                _robotPositions[11] = value;
            }
        }

        public double[] RobotPositions
        {
            get
            {
                lock (_robotPositionsLocker)
                {
                    return _robotPositions;
                }
            }
            set
            {
                lock (_robotPositionsLocker)
                {
                    _robotPositions = value;
                }
            }
        }
    }
}
