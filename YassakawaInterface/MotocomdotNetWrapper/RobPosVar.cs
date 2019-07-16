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
    public interface IRobPosVar
    {
        #region Properties
        PosVarType DataType
        {
            get;
            set;
        }
        int SAxis
        {
            get;
            set;
        }
        FrameType Frame
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
        double[] HostGetVarDataArray
        {
            get;
            set;
        }

        #endregion

        #region Methods

        #endregion
    }
    [ComVisible(true)]
    public class RobPosVar
    {
        double[] m_HostGetVarDataArray = new double[12];
        public RobPosVar() : this(PosVarType.XYZ)
        {
        }
        public RobPosVar(int saxis, int laxis, int uaxis, int raxis, int baxis, int taxis, int e7axis, int e8axis, short toolno)
        {
            DataType = PosVarType.Pulse;
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
        public RobPosVar(FrameType frame, double x, double y, double z, double rx, double ry, double rz, short formcode, short toolno)
        {
            DataType = PosVarType.XYZ;
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
        public RobPosVar(PosVarType datatype)
        {
            DataType = datatype;
            if (datatype == PosVarType.Pulse)
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
                Frame = FrameType.Robot;
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
        public RobPosVar(double[] HostGetVarDataArray)
        {
            HostGetVarDataArray.CopyTo(m_HostGetVarDataArray, 0);
        }
        public PosVarType DataType
        {
            get { return (PosVarType)m_HostGetVarDataArray[0]; }
            set { m_HostGetVarDataArray[0] = (double)value; }
        }
        public int SAxis
        {
            get { return (int)m_HostGetVarDataArray[1]; }
            set { m_HostGetVarDataArray[1] = (double)value; }
        }
        public FrameType Frame
        {
            get { return (FrameType)m_HostGetVarDataArray[1]; }
            set { m_HostGetVarDataArray[1] = (double)value; }
        }
        public int LAxis
        {
            get { return (int)m_HostGetVarDataArray[2]; }
            set { m_HostGetVarDataArray[2] = (double)value; }
        }
        public double X
        {
            get { return m_HostGetVarDataArray[2]; }
            set { m_HostGetVarDataArray[2] = value; }
        }
        public int UAxis
        {
            get { return (int)m_HostGetVarDataArray[3]; }
            set { m_HostGetVarDataArray[3] = (double)value; }
        }
        public double Y
        {
            get { return m_HostGetVarDataArray[3]; }
            set { m_HostGetVarDataArray[3] = value; }
        }
        public int RAxis
        {
            get { return (int)m_HostGetVarDataArray[4]; }
            set { m_HostGetVarDataArray[4] = (double)value; }
        }
        public double Z
        {
            get { return m_HostGetVarDataArray[4]; }
            set { m_HostGetVarDataArray[4] = value; }
        }
        public int BAxis
        {
            get { return (int)m_HostGetVarDataArray[5]; }
            set { m_HostGetVarDataArray[5] = (double)value; }
        }
        public double Rx
        {
            get { return m_HostGetVarDataArray[5]; }
            set { m_HostGetVarDataArray[5] = value; }
        }
        public int TAxis
        {
            get { return (int)m_HostGetVarDataArray[6]; }
            set { m_HostGetVarDataArray[6] = (double)value; }
        }
        public double Ry
        {
            get { return m_HostGetVarDataArray[6]; }
            set { m_HostGetVarDataArray[6] = value; }
        }
        public int E7Axis
        {
            get { return (int)m_HostGetVarDataArray[7]; }
            set { m_HostGetVarDataArray[7] = (double)value; }
        }
        public double Rz
        {
            get { return m_HostGetVarDataArray[7]; }
            set { m_HostGetVarDataArray[7] = value; }
        }
        public int E8Axis
        {
            get { return (int)m_HostGetVarDataArray[8]; }
            set { m_HostGetVarDataArray[8] = (double)value; }
        }
        public short Formcode
        {
            get { return (short)m_HostGetVarDataArray[8]; }
            set { m_HostGetVarDataArray[8] = (double)value; }
        }
        public short ToolNo
        {
            get { return (short)m_HostGetVarDataArray[9]; }
            set { m_HostGetVarDataArray[9] = (double)value; }
        }
        public double[] HostGetVarDataArray
        {
            get
            {

                return m_HostGetVarDataArray;
            }

            set
            {
                m_HostGetVarDataArray = value;
            }
        }
    }
}
