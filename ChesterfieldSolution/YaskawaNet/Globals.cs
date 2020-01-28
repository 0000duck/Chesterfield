using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace YaskawaNet
{
    [ComVisible(true)]
    public enum RobotFrameType 
    {
        Base = 0,
        Robot,
        User1, User2, User3, User4, User5, User6, User7, User8,
        User9, User10, User11, User12, User13, User14, User15, User16,
        User17, User18, User19, User20, User21, User22, User23, User24,
        User25, User26, User27, User28, User29, User30, User31, User32,
        User33, User34, User35, User36, User37, User38, User39, User40,
        User41, User42, User43, User44, User45, User46, User47, User48,
        User49, User50, User51, User52, User53, User54, User55, User56,
        User57, User58, User59, User60, User61, User62, User63, User64,
        Tool,
        MasterTool
    }
    [ComVisible(true)]
    public enum RobotVariableType 
    {
        Byte = 0,
        Integer,
        Double,
        Real,
        RobotAxisPosition,
        BaseAxisPosition,
        StationAxisPosition,
        String
    }
    [ComVisible(true)]
    public enum RobotPositionVariableType 
    {
        Pulse = 0,
        XYZ
    }
    [ComVisible(true)]
    public enum RobotCommunicationType 
    {
        SERIAL = 0x01,
        ETHERNET = 0x10,
        ETHERNET_SERVER = 0x100
    }
    [ComVisible(true)]
    public enum RobotModeType 
    {
        TEACH = 1,
        PLAY = 2
    }
    [ComVisible(true)]
    public enum RobotMotionType
    {
        NONE = -1,
        Relative,
        Jog,
        Absolute,
        Continuous,
        RelativeArray,
        JogArray,
        AbsoluteArray,
        JointMotion,
        LinearMotion
    }
    [ComVisible(true)]
    public enum RobotMoveSpeedSelectionType 
    {
        ControlPoint = 1,
        PositionAngular = 2,
        JointSpeed = 3
    }
    [ComVisible(true)]
    public enum RobotFunctionReturnType_1
    {
        AcquisitionFailure = -1,
        NormalCompletion = 0
    }
    [ComVisible(true)]
    public enum RobotFunctionReturnType_2 
    {
        Other = -1,
        NormalCompletion = 0
    }
    [ComVisible(true)]
    public enum MotionDirection
    {
        None = 0,
        Positive = 1,
        Negative = 2
    }
    [ComVisible(true)]
    public enum ConnectionStatus
    {
        Unreachable = 0,
        Reachable = 1
    }
    [ComVisible(true)]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CommunicationStatus
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool connected;
        [MarshalAs(UnmanagedType.BStr)]
        public string ipAddress;
        [MarshalAs(UnmanagedType.I4)]
        public ConnectionStatus connectionStatus;
    }
    [ComVisible(true)]
    public enum RoboDKMode
    {
       StandAlone,
       FollowRealRobot,
       CSIControlled
    }
}
