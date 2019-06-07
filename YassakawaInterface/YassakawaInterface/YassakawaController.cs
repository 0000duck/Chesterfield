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
        public YassakawaController(string IPAddress , string path)
        {
            m_cYasnac = new CYasnac(IPAddress , path);
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
        public int GetAlarmsList(out CErrorData error , out ArrayList alarmList)
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
    }
}
