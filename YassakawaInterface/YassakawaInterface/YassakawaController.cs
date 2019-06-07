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
        #endregion SERVOS

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
        /// <summary>
        /// Moving the robot to a target point in linear motion.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        public short MoveLinear(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, double targetPosition)
        {
            StringBuilder moveSpeedSelectionSB = new StringBuilder(moveSpeedSelection);
            StringBuilder framNameSB = new StringBuilder(frameName);

            return m_cYasnac.Movl(moveSpeedSelectionSB, speed, framNameSB, rconf, toolNumber, ref targetPosition);
        }

        /// <summary>
        /// Moving the robot with increamental position value in a linear motion.
        /// </summary>
        /// <param name="moveSpeedSelection"></param>
        /// <param name="speed"></param>
        /// <param name="frameName"></param>
        /// <param name="rconf"></param>
        /// <param name="toolNumber"></param>
        /// <param name="incrementValue"></param>
        /// <returns></returns>
        public short MoveLinearIncrement(string moveSpeedSelection, double speed, string frameName, short rconf, short toolNumber, double incrementValue)
        {
            StringBuilder moveSpeedSelectionSB = new StringBuilder(moveSpeedSelection);
            StringBuilder framNameSB = new StringBuilder(frameName);

            return m_cYasnac.IMov(moveSpeedSelectionSB, speed, framNameSB, toolNumber, ref incrementValue);
        }

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
        /// <returns></returns>
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
        /// Write a position variable o the controller memory.
        /// </summary>
        /// <param name="index">The index of the variable.</param>
        /// <param name="posVar">The position variable description.</param>
        /// <returns></returns>
        public short WritePositionVariable(short index , CRobPosVar posVar)
        {
            return m_cYasnac.WritePositionVariable(index, posVar);
        }

        public short ReadPositionVariable(short index , out CRobPosVar posVar)
        {
            return m_cYasnac.ReadPositionVariable(index, out posVar);
        }
        #endregion VARIABLES
    }
}
