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
    [Guid("BE5A1B4B-3E12-4806-9E1D-1A7ACA14EFBE")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IRobotPositionVariable
    {
        #region Properties
        RobotPositionVariableType DataType
        {
            get;
            set;
        }
        int SAxis
        {
            get;
            set;
        }
        RobotFrameType Frame
        {
            get;
            set;
        }
        int LAxis
        {
            get;
            set;
        }
        double X
        {
            get;
            set;
        }
        int UAxis
        {
            get;
            set;
        }
        double Y
        {
            get;
            set;
        }
        int RAxis
        {
            get;
            set;
        }
        double Z
        {
            get;
            set;
        }
        int BAxis
        {
            get;
            set;
        }
        double Rx
        {
            get;
            set;
        }
        int TAxis
        {
            get;
            set;
        }
        double Ry
        {
            get;
            set;
        }
        int E7Axis
        {
            get;
            set;
        }
        double Rz
        {
            get;
            set;
        }
        int E8Axis
        {
            get;
            set;
        }
        short Formcode
        {
            get;
            set;
        }
        short ToolNo
        {
            get;
            set;
        }
        double[] NumVarStorArea
        {
            get;
            set;
        }

        #endregion

        #region Methods

        #endregion
    }
    [ComVisible(true)]
    public class RobotPositionVariable : IRobotPositionVariable
    {
        double[] _numericVariableStorageArea = new double[12];
        public RobotPositionVariable() : this(RobotPositionVariableType.XYZ)
        {
        }
        public RobotPositionVariable(int saxis, int laxis, int uaxis, int raxis, int baxis, int taxis, int e7axis, int e8axis, short toolno)
        {
            DataType = RobotPositionVariableType.Pulse;
            SAxis = saxis;
            LAxis = laxis;
            UAxis = uaxis;
            RAxis = raxis;
            BAxis = baxis;
            TAxis = taxis;
            E7Axis = e7axis;
            E8Axis = e8axis;
            ToolNo = toolno;
        }
        public RobotPositionVariable(RobotFrameType frame, double x, double y, double z, double rx, double ry, double rz, short formcode, short toolno)
        {
            DataType = RobotPositionVariableType.XYZ;
            Frame = frame;
            X = x;
            Y = y;
            Z = z;
            Rx = rx;
            Ry = ry;
            Rz = rz;
            Formcode = formcode;
            ToolNo = toolno;
        }
        public RobotPositionVariable(RobotPositionVariableType datatype)
        {
            DataType = datatype;
            if (datatype == RobotPositionVariableType.Pulse)
            {
                SAxis = 0;
                LAxis = 0;
                UAxis = 0;
                RAxis = 0;
                BAxis = 0;
                TAxis = 0;
                E7Axis = 0;
                E8Axis = 0;
                ToolNo = 0;
            }
            else
            {
                Frame = RobotFrameType.Robot;
                X = 0.0;
                Y = 0.0;
                Z = 0.0;
                Rx = 0.0;
                Ry = 0.0;
                Rz = 0.0;
                Formcode = 0;
                ToolNo = 0;
            }
        }
        public RobotPositionVariable(double[] HostGetVarDataArray)
        {
            HostGetVarDataArray.CopyTo(_numericVariableStorageArea, 0);
        }
        public RobotPositionVariableType DataType
        {
            get { return (RobotPositionVariableType)_numericVariableStorageArea[0]; }
            set { _numericVariableStorageArea[0] = (double)value; }
        }
        public int SAxis
        {
            get { return (int)_numericVariableStorageArea[1]; }
            set { _numericVariableStorageArea[1] = (double)value; }
        }
        public RobotFrameType Frame
        {
            get { return (RobotFrameType)_numericVariableStorageArea[1]; }
            set { _numericVariableStorageArea[1] = (double)value; }
        }
        public int LAxis
        {
            get { return (int)_numericVariableStorageArea[2]; }
            set { _numericVariableStorageArea[2] = (double)value; }
        }
        public double X
        {
            get { return _numericVariableStorageArea[2]; }
            set { _numericVariableStorageArea[2] = value; }
        }
        public int UAxis
        {
            get { return (int)_numericVariableStorageArea[3]; }
            set { _numericVariableStorageArea[3] = (double)value; }
        }
        public double Y
        {
            get { return _numericVariableStorageArea[3]; }
            set { _numericVariableStorageArea[3] = value; }
        }
        public int RAxis
        {
            get { return (int)_numericVariableStorageArea[4]; }
            set { _numericVariableStorageArea[4] = (double)value; }
        }
        public double Z
        {
            get { return _numericVariableStorageArea[4]; }
            set { _numericVariableStorageArea[4] = value; }
        }
        public int BAxis
        {
            get { return (int)_numericVariableStorageArea[5]; }
            set { _numericVariableStorageArea[5] = (double)value; }
        }
        public double Rx
        {
            get { return _numericVariableStorageArea[5]; }
            set { _numericVariableStorageArea[5] = value; }
        }
        public int TAxis
        {
            get { return (int)_numericVariableStorageArea[6]; }
            set { _numericVariableStorageArea[6] = (double)value; }
        }
        public double Ry
        {
            get { return _numericVariableStorageArea[6]; }
            set { _numericVariableStorageArea[6] = value; }
        }
        public int E7Axis
        {
            get { return (int)_numericVariableStorageArea[7]; }
            set { _numericVariableStorageArea[7] = (double)value; }
        }
        public double Rz
        {
            get { return _numericVariableStorageArea[7]; }
            set { _numericVariableStorageArea[7] = value; }
        }
        public int E8Axis
        {
            get { return (int)_numericVariableStorageArea[8]; }
            set { _numericVariableStorageArea[8] = (double)value; }
        }
        public short Formcode
        {
            get { return (short)_numericVariableStorageArea[8]; }
            set { _numericVariableStorageArea[8] = (double)value; }
        }
        public short ToolNo
        {
            get { return (short)_numericVariableStorageArea[9]; }
            set { _numericVariableStorageArea[9] = (double)value; }
        }
        public double[] NumVarStorArea
        {
            get
            {
                return _numericVariableStorageArea;
            }
            set
            {
                _numericVariableStorageArea = value;
            }
        }
    }
}
