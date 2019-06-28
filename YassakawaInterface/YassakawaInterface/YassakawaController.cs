using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotocomdotNetWrapper;

namespace YassakawaInterface
{
    public class YassakawaController
    {
        public CYasnac m_cYasnac { get; set; }

        #region CTOR
        /// <summary>
        /// Default construvtor for creating a Yassakawa Robot object.
        /// </summary>
        /// <param name="IPAddress">The robot IP address.</param>
        /// <param name="path">The path to the robot directory.</param>
        public YassakawaController(string IPAddress, string path)
        {
            m_cYasnac = new CYasnac(IPAddress, path);
        }
        #endregion CTOR

        #region SERVOS
        /// <summary>
        /// Setting the robot servo on.
        /// </summary>
        public void ServoOn()
        {
            m_cYasnac.SetServoOn();
        }

        /// <summary>
        /// Setting the robot servo off.
        /// </summary>
        public void ServoOff()
        {
            m_cYasnac.SetServoOff();
        }

        /// <summary>
        /// Gets te servo status.
        /// </summary>
        /// <returns>-1 for acquisition failure , 0 for servo off , 1 for servo on.</returns>
        public short ServoStatus()
        {
            return m_cYasnac.IsServo();
        }
        #endregion SERVOS

        #region
        /// <summary>
        /// Connect to the robot controller.
        /// </summary>
        /// <returns>0 for error , 1 for normal operation</returns>
        public short Connect()
        {
            return m_cYasnac.Connect();
        }

        /// <summary>
        /// Disconnect from the robot controller.
        /// </summary>
        /// <returns>0 for error , 1 for normal operation</returns>
        public short Disconnect()
        {
            return m_cYasnac.Disconnect();
        }
        #endregion

        #region MODES
        /// <summary>
        /// Set the robot to play mode.
        /// </summary>
        public void SetPlayMode()
        {
            m_cYasnac.SetPlayMode();
        }

        /// <summary>
        /// Set the robot to teach mode.
        /// </summary>
        public void SetTeachMode()
        {
            m_cYasnac.SetTeachMode();
        }
        #endregion MODES

        #region MOVING_OPERATIONS
        #region LINEAR_MOVING_OPERATION
        /// <summary>
        /// Moving the robot to a target point in linear motion in specified frame type.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public short MoveLinearCartesianTarget(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, ref double targetPosition)
        {
            StringBuilder moveSpeedSelectionSB = new StringBuilder(moveSpeedSelection);
            StringBuilder framNameSB = new StringBuilder(frameName);

            return m_cYasnac.Movl(moveSpeedSelectionSB, speed, framNameSB, rconf, toolNumber, ref targetPosition);
        }
        
        /// <summary>
        /// Moving the robot to a target point in linear motion with joint position target.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public short MoveLinearPulseTarget(string moveSpeedSelection, double speed, short toolNumber, ref double targetPosition)
        {
            StringBuilder moveSpeedSelectionStringBuilder = new StringBuilder(moveSpeedSelection);
            return m_cYasnac.MovlJoint(moveSpeedSelectionStringBuilder, speed, toolNumber, ref targetPosition);
        }
        
        /// <summary>
        /// Moving the robot to a target cartesian point in  joint motion.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public short MoveJointCartesianTarget(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, ref double targetPosition)
        {
            StringBuilder moveSpeedSelectionSB = new StringBuilder(moveSpeedSelection);
            StringBuilder framNameSB = new StringBuilder(frameName);

            return m_cYasnac.MovJ(speed, framNameSB, rconf, toolNumber, ref targetPosition);
        }

        /// <summary>
        /// Moving the robot to a target point in joint motion with joint position target.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public short MoveJointPulseTarget(string moveSpeedSelection, double speed, short toolNumber, ref double targetPosition)
        {
            StringBuilder moveSpeedSelectionStringBuilder = new StringBuilder(moveSpeedSelection);
            return m_cYasnac.MovjJoint(moveSpeedSelectionStringBuilder, speed , toolNumber , ref targetPosition);
        }

        /// <summary>
        /// Moving the robot with increamental position in X axis value in a linear motion in specified frame type.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short MoveLinearIncrementX(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, double increamentValue)
        {
            double[] increamentArray = new double[]
            {
                increamentValue ,
                0, 
                0,
                0,
                0,
                0
            };

            return MoveLinearIncrementCartesian(moveSpeedSelection, speed, frameName, rconf, toolNumber, ref increamentArray[0]);
        }
        
        /// <summary>
        /// Moving the robot with increamental position in Y axis value in a linear motion in specified frame type.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short MoveLinearIncrementY(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, double increamentValue)
        {
            double[] increamentArray = new double[]
            {
                0 ,
                increamentValue,
                0,
                0,
                0,
                0
            };

            return MoveLinearIncrementCartesian(moveSpeedSelection, speed, frameName, rconf, toolNumber, ref increamentArray[0]);
        }

        /// <summary>
        /// Moving the robot with increamental position in Z axis value in a linear motion in specified frame type.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short MoveLinearIncrementZ(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, double increamentValue)
        {
            double[] increamentArray = new double[]
            {
                0 ,
                0,
                increamentValue,
                0,
                0,
                0
            };

            return MoveLinearIncrementCartesian(moveSpeedSelection, speed, frameName, rconf, toolNumber, ref increamentArray[0]);
        }

        /// <summary>
        /// Moving the robot with increamental position in RX axis value in a linear motion in specified frame type.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short MoveLinearIncrementRX(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, double increamentValue)
        {
            double[] increamentArray = new double[]
            {
                0 ,
                0,
                0,
                increamentValue,
                0,
                0
            };

            return MoveLinearIncrementCartesian(moveSpeedSelection, speed, frameName, rconf, toolNumber, ref increamentArray[0]);
        }

        /// <summary>
        /// Moving the robot with increamental position in RY axis value in a linear motion in specified frame type.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short MoveLinearIncrementRY(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, double increamentValue)
        {
            double[] increamentArray = new double[]
            {
                0 ,
                0,
                0,
                0,
                increamentValue,
                0
            };

            return MoveLinearIncrementCartesian(moveSpeedSelection, speed, frameName, rconf, toolNumber, ref increamentArray[0]);
        }

        /// <summary>
        /// Moving the robot with increamental position in RZ axis value in a linear motion in specified frame type.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short MoveLinearIncrementRZ(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, double increamentValue)
        {
            double[] increamentArray = new double[]
            {
                0 ,
                0,
                0,
                0,
                0,
                increamentValue,
            };

            return MoveLinearIncrementCartesian(moveSpeedSelection, speed, frameName, rconf, toolNumber, ref increamentArray[0]);
        }

        /// <summary>
        /// Moving the robot with increamental position value in a linear motion in specified frame type.
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

            return m_cYasnac.IMov(moveSpeedSelectionSB, speed, framNameSB, toolNumber, ref incrementValue);
        }
        #endregion LINEAR_MOVING_OPERATION

        #endregion MOVING_OPERATIONS

        #region ERROR_ALARMS
        /// <summary>
        /// Get the alarm list in the robot controller , including the error description.
        /// </summary>
        /// <param name="error">The error message with the error number.</The>/param>
        /// <param name="alarmList">The alarm list include alarm sub number and alarm description.</param>
        /// <returns>The number of alarms in the error data.</returns>
        public int GetAlarmsList(out CErrorData error, out ArrayList alarmList)
        {
            return m_cYasnac.GetAlarm(out error, out alarmList);
        }

        /// <summary>
        /// Resets the robot alarms list.
        /// </summary>
        /// <returns></returns>
        public short ResetAlarms()
        {
            return m_cYasnac.ResetAlarm();
        }
        #endregion ERROR_ALARMS

        #region FILES_OPERATION
        /// <summary>
        /// Download file to the controller.
        /// </summary>
        /// <param name="fileName"></param>
        public void WriteFile(string fileName)
        {
            m_cYasnac.WriteFile(fileName);
        }

        /// <summary>
        /// Upload file from the controller.
        /// </summary>
        /// <param name="fileTitle"></param>
        /// <param name="dirPath"></param>
        public void ReadFile(string fileTitle, string dirPath)
        {
            m_cYasnac.ReadFile(fileTitle, dirPath);
        }

        /// <summary>
        /// Delete file the controller.
        /// </summary>
        /// <param name="fileName"></param>
        public void Deletefile(string fileName)
        {
            m_cYasnac.DeleteJob(fileName);
        }
        #endregion FILES_OPERATION

        #region JOBS_OPERATION
        /// <summary>
        /// Starts a job with specified name (in the controller).
        /// </summary>
        /// <param name="jobName">The job name as downloaded to the controller including it's .JBI extension.</param>
        public void StartJob(string jobName)
        {
            m_cYasnac.StartJob(jobName);
        }

        /// <summary>
        /// Holds on the current execution.
        /// </summary>
        /// <returns>0 for complete operation , others , error codes.</returns>
        public short HoldOnExecution()
        {
            return m_cYasnac.SetHoldOn();
        }

        /// <summary>
        /// Holds off the current execution. Need to Call Countinue Job in order to start the job from the last executed line.
        /// </summary>
        /// <returns>0 for complete operation , others , error codes.</returns>
        public short HoldOffExecution()
        {
            return m_cYasnac.SetHoldOff();
        }

        /// <summary>
        /// Continue job execution after HoldOn and HoldOff calling.
        /// </summary>
        /// <returns>0 for complete operation , others , error codes.</returns>
        public short ContinueExecuting()
        {
            return m_cYasnac.Start();
        }
        #endregion JOBS_OPERATION

        #region I/O
        /// <summary>
        /// Reads a single I/O address.
        /// </summary>
        /// <param name="address"></param>
        /// <returns>True if '1' logic , false if '0' logic.</returns>
        public bool ReadIO(int address)
        {
            //todo:check if it returns a byte with 1/0 or a group of 8 buts of 0/1.
            return m_cYasnac.ReadSingleIO(address);
        }

        /// <summary>
        /// Read multiple single I/O in groups.
        /// </summary>
        /// <param name="startAddress">The start address to read from.</param>
        /// <param name="numOfGroups">The num og 8 bit I/O groups to read.</param>
        /// <param name="ioValues">The values of the 8 I/O groups/</param>
        /// <returns>0 for complete execution , otherwise , otherwise , error codes.</returns>
        public short ReadIO(int startAddress, short numOfGroups, out short[] ioValues)
        {
            return m_cYasnac.ReadIOGroups(startAddress, numOfGroups, out ioValues);
        }

        /// <summary>
        /// Write to a single I/O address.
        /// </summary>
        /// <param name="address">The address of the I/O to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        /// <returns>0 if complete . otherwise , error codes.</returns>
        public short WriteIO(int address, bool value)
        {
            return m_cYasnac.WriteSingleIO(address, value);
        }

        /// <summary>
        /// Write to a I/O address in groups of 8.
        /// </summary>
        /// <param name="startAddress">The start address to write the first value.</param>
        /// <param name="numOfGroups">The number of 8 groups pins to write to.</param>
        /// <param name="ioValues">The values to write.</param>
        /// <returns>0 if complete . otherwise , error codes.</returns>
        public short WriteIO(int startAddress , short numOfGroups , short[] ioValues)
        {
            return m_cYasnac.WriteIOGroups(startAddress , numOfGroups , ioValues);
        }
        #endregion I/O

        #region VARIABLES
        /// <summary>
        /// Write a position variable to the controller memory.
        /// </summary>
        /// <param name="index">The index of the variable.</param>
        /// <param name="posVar">The position variable description.</param>
        /// <returns>0 for complete operation , others , error codes.</returns>
        public short WritePositionVariable(short index , CRobPosVar posVar)
        {
            return m_cYasnac.WritePositionVariable(index, posVar);
        }

        /// <summary>
        /// Read a position variable from controller memory.
        /// </summary>
        /// <param name="index">The index of the variable.</param>
        /// <param name="posVar">The position variable description.</param>
        /// <returns>0 for complete operation , others , error codes.</returns>
        public short ReadPositionVariable(short index , out CRobPosVar posVar)
        {
            return m_cYasnac.ReadPositionVariable(index, out posVar);
        }
        #endregion VARIABLES

        #region FEEDBACKS_STATUSES
        /// <summary>
        /// Getting robot status flags.
        /// </summary>
        /// <param name="robotStatus">The robot status flags output.</param>
        /// <returns>0 for complete operation , others , error codes.</returns>
        public short GetStatus(out RobotStatus robotStatus)
        {
            return m_cYasnac.GetStatus(out robotStatus);
        }

        /// <summary>
        /// Get the current robot position in a choosem frame type.
        /// </summary>
        /// <param name="frameName">The frame Type (Base , Robot , UF1....... , UF2......)</param>
        /// <param name="isExternal">External axis flag.</param>
        /// <param name="rconf">Form storage</param>
        /// <param name="toolNumber">Tool number</param>
        /// <param name="position">Current Position</param>
        /// <returns>0 for complete operation , others , error codes.</returns>
        public short GetCurrentPosition(string frameName , bool isExternal , ref short rconf , ref short toolNumber , ref double position)
        {
            StringBuilder frameNameSB = new StringBuilder(frameName);
            return m_cYasnac.GetRobotPosition(frameNameSB, (short)(isExternal?1:0), ref rconf, ref toolNumber, ref position);
        }

        /// <summary>
        /// Get the current robot position in a pulse/robot frame type.
        /// </summary>
        /// <param name="isPulseOrXYZ">True for pulse frame type , false for XYZ frame type.</param>
        /// <param name="rconf">Form storage</param>
        /// <param name="position">Current Position</param>
        /// <returns>0 for complete operation , others , error codes.</returns>
        public short GetCurrentPosition(bool isPulseOrXYZ, ref short rconf, ref double position)
        {
            return m_cYasnac.GetRobotPosition((short)(isPulseOrXYZ ? 1 : 0), ref rconf, ref position);
        }
        #endregion FEEDBACKS_STATUSES
    }
}
