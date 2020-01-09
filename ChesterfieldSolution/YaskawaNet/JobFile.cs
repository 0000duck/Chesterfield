using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaskawaNet
{
    public class JobFile
    {
        #region Constants

        const double ROriginalX = 0.0;
        const double ROriginalY = 0.0;
        const double ROriginalZ = 0.0;
        const double ROriginalRx = 0.0;
        const double ROriginalRy = 0.0;
        const double ROriginalRz = 0.0;

        #endregion

        #region Fields

        /// <summary>
        /// The JBI fileName to write the commands to it.
        /// </summary>
        private string _fileName = string.Empty;
        /// <summary>
        /// The frequency the commands rely on (to make the velocity).
        /// </summary>
        private int _frequency = 0;

        #endregion

        #region Constructor
        public JobFile(int frequency = 50)
        {
            _frequency = frequency;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Set or get the frequency the JBIFileCreator rely on.
        /// </summary>
        public int Frequency
        {
            get
            {
                return _frequency;
            }
            set
            {
                _frequency = value;
            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }
        #endregion

        #region Methods
        public void SerializePosition(string jbiFileName, RobotPosition position, double velocity)
        {
            StreamWriter _fileStreamWriter = new StreamWriter(_fileName);
            StringBuilder lineStringBuilder = new StringBuilder();

            try
            {
                #region
                _fileStreamWriter.WriteLine("/JOB");
                _fileStreamWriter.WriteLine("//NAME " + jbiFileName);
                _fileStreamWriter.WriteLine("//POS");
                _fileStreamWriter.WriteLine("///NPOS 0,0,0,4,0,0");
                _fileStreamWriter.WriteLine("///TOOL 0");
                _fileStreamWriter.WriteLine("///POSTYPE ROBOT");
                _fileStreamWriter.WriteLine("///RECTAN");
                _fileStreamWriter.WriteLine("///RCONF 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
                _fileStreamWriter.WriteLine("P00000=10.000,0.000,0.000,0.0000,0.0000,0.0000");
                _fileStreamWriter.WriteLine("///POSTYPE BASE");

                lineStringBuilder.Append("P00001=");
                lineStringBuilder.Append(position.X.ToString("0000.0000000"));
                lineStringBuilder.Append(",");
                lineStringBuilder.Append(position.Y.ToString("0000.0000000"));
                lineStringBuilder.Append(",");
                lineStringBuilder.Append(position.Z.ToString("0000.0000000"));
                lineStringBuilder.Append(",");
                lineStringBuilder.Append(position.Rx.ToString("0000.0000000"));
                lineStringBuilder.Append(",");
                lineStringBuilder.Append(position.Ry.ToString("0000.0000000"));
                lineStringBuilder.Append(",");
                lineStringBuilder.Append(position.Rz.ToString("0000.0000000"));

                _fileStreamWriter.WriteLine(lineStringBuilder.ToString());
                lineStringBuilder.Clear();

                _fileStreamWriter.WriteLine("//INST");
                _fileStreamWriter.WriteLine("///DATE 2017/03/31 08:11");
                _fileStreamWriter.WriteLine("///COMM PLAYINGTWOROBOTS");
                _fileStreamWriter.WriteLine("///ATTR SC,RW");
                _fileStreamWriter.WriteLine("///GROUP1 RB1");
                _fileStreamWriter.WriteLine("///GROUP2 RB2");
                _fileStreamWriter.WriteLine("NOP");

                lineStringBuilder.Append("MOVL P00001 V=");
                lineStringBuilder.Append(velocity.ToString("0000.0000000"));

                _fileStreamWriter.WriteLine(lineStringBuilder);
                lineStringBuilder.Clear();

                _fileStreamWriter.WriteLine("END");

                _fileStreamWriter.Close();
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="traj"></param>
        public short CreateJobFileRobotBaseFromPulseTrajectory(Trajectory traj)
        {
            short returnValue = 0;
            StreamWriter _fileStreamWriter = new StreamWriter(_fileName);
            Trajectory clonedR1Traj = null;
            StringBuilder sb = new StringBuilder();
            int fileExtPos = 0;
            string _fileNameWithoutExtension = string.Empty;

            try
            {
                #region
                _fileNameWithoutExtension = Path.GetFileName(_fileName);
                fileExtPos = _fileNameWithoutExtension.LastIndexOf(".");
                if (fileExtPos >= 0)
                {
                    _fileNameWithoutExtension = _fileNameWithoutExtension.Substring(0, fileExtPos);
                }

                _fileStreamWriter.WriteLine("/JOB");
                _fileStreamWriter.Write("//NAME ");
                _fileStreamWriter.WriteLine(_fileNameWithoutExtension);
                _fileStreamWriter.WriteLine("//POS");

                _fileStreamWriter.Write("///NPOS ");
                _fileStreamWriter.Write(traj.Count); _fileStreamWriter.Write(",");
                _fileStreamWriter.Write(traj.Count); _fileStreamWriter.Write(",");
                _fileStreamWriter.WriteLine("0,0,0,0");

                _fileStreamWriter.WriteLine("///TOOL 0");
                _fileStreamWriter.WriteLine("///POSTYPE PULSE");
                _fileStreamWriter.WriteLine("///PULSE");
                //_fileStreamWriter.WriteLine("///RCONF 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
                //_fileStreamWriter.WriteLine("P00000=10.000,0.000,0.000,0.0000,0.0000,0.0000");
                //_fileStreamWriter.WriteLine("///POSTYPE BASE");

                clonedR1Traj = traj.Clone();
                //adding the zero point place for the trajectory (for the velocity calculaion behind) at the end if it is backward or at the beginning if it is forward movement.
                //also, for the backward movement it skip the last point (because the robot is already there from the forward movement) and added the 0 placed to the end of the trajectory.
                foreach (string lineString in JointsTrajectoryToLine(clonedR1Traj))
                {
                    _fileStreamWriter.WriteLine(lineString);
                }

                clonedR1Traj.InsertOriginPlace(true);

                _fileStreamWriter.WriteLine("//INST");
                _fileStreamWriter.WriteLine("///DATE 2019/01/31 09:00");

                _fileStreamWriter.WriteLine("///ATTR SC,RW");

                _fileStreamWriter.WriteLine("///GROUP1 RB1,BS1");

                _fileStreamWriter.WriteLine("NOP");

                //make the f * duration velocity points vector from the f * duratoin + 1 places points in the trajectory.
                for (int i = 0; i < clonedR1Traj.Count - 1; i++)
                {
                    #region
                    //decode the velocity for the selected robot (if only one of then) or the first robot (r1) if both of them.
                    sb.Append("MOVJ ");
                    sb.Append("C");
                    sb.Append((i).ToString("D" + 5));
                    sb.Append(" BC");
                    sb.Append((i).ToString("D" + 5));
                    double velocity = 10;// Velocity3D(clonedR1Traj[i + 1], clonedR1Traj[i]) * 10000.0 / (1000.0 / (double)(_frequency));
                    sb.Append(" VJ=");
                    sb.Append(velocity.ToString("00.00"));

                    _fileStreamWriter.WriteLine(sb.ToString());
                    sb.Clear();
                    #endregion
                }

                _fileStreamWriter.WriteLine("END");

                _fileStreamWriter.Close();
                #endregion
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="traj"></param>
        public short CreateJobFileRobotBaseFromTCPTrajectory(Trajectory traj, short referenceFrame)
        {
            short returnValue = 0;
            StreamWriter _fileStreamWriter = new StreamWriter(_fileName);
            Trajectory clonedR1Traj = null;
            StringBuilder sb = new StringBuilder();
            int fileExtPos = 0;
            string _fileNameWithoutExtension = string.Empty;

            try
            {
                #region
                if (traj.Count > 0)
                {
                    #region
                    _fileNameWithoutExtension = Path.GetFileName(_fileName);
                    fileExtPos = _fileNameWithoutExtension.LastIndexOf(".");
                    if (fileExtPos >= 0)
                    {
                        _fileNameWithoutExtension = _fileNameWithoutExtension.Substring(0, fileExtPos);
                    }

                    _fileStreamWriter.WriteLine("/JOB");
                    _fileStreamWriter.Write("//NAME ");
                    _fileStreamWriter.WriteLine(_fileNameWithoutExtension);
                    _fileStreamWriter.WriteLine("//POS");

                    _fileStreamWriter.Write("///NPOS ");
                    _fileStreamWriter.Write(traj.Count); _fileStreamWriter.Write(",");
                    _fileStreamWriter.Write(traj.Count); _fileStreamWriter.Write(",");
                    _fileStreamWriter.WriteLine("0,0,0,0");

                    if (referenceFrame == 1)
                    {
                        _fileStreamWriter.WriteLine("///USER 1");
                    }

                    _fileStreamWriter.WriteLine("///TOOL 0");
                    if (referenceFrame == 0)
                    {
                        _fileStreamWriter.WriteLine("///POSTYPE BASE");
                    }
                    if (referenceFrame == 1)
                    {
                        _fileStreamWriter.WriteLine("///POSTYPE USER");
                    }
                    _fileStreamWriter.WriteLine("///RECTAN");
                    _fileStreamWriter.WriteLine("///RCONF 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");

                    clonedR1Traj = traj.Clone();
                    //adding the zero point place for the trajectory (for the velocity calculaion behind) at the end if it is backward or at the beginning if it is forward movement.
                    //also, for the backward movement it skip the last point (because the robot is already there from the forward movement) and added the 0 placed to the end of the trajectory.
                    foreach (string lineString in TCPTrajectoryToLine(clonedR1Traj))
                    {
                        _fileStreamWriter.WriteLine(lineString);
                    }

                    //clonedR1Traj.InsertOriginPlace(true);

                    _fileStreamWriter.WriteLine("//INST");
                    _fileStreamWriter.WriteLine("///DATE 2019/01/31 09:00");

                    _fileStreamWriter.WriteLine("///ATTR SC,RW,RJ");

                    if (referenceFrame == 0)
                    {
                        _fileStreamWriter.WriteLine("////FRAME BASE");
                    }
                    if (referenceFrame == 1)
                    {
                        _fileStreamWriter.WriteLine("////FRAME USER 1");
                    }

                    _fileStreamWriter.WriteLine("///GROUP1 RB1,BS1");

                    _fileStreamWriter.WriteLine("NOP");

                    //make the f * duration velocity points vector from the f * duration + 1 places points in the trajectory.
                    for (int i = 0; i < clonedR1Traj.Count; i++)
                    {
                        #region
                        //decode the velocity for the selected robot (if only one of then) or the first robot (r1) if both of them.
                        sb.Append("MOVJ ");
                        sb.Append("C");
                        sb.Append((i).ToString("D" + 5));
                        sb.Append(" BC");
                        sb.Append((i).ToString("D" + 5));
                        double velocity = 10.0;// Velocity3D(clonedR1Traj[i + 1], clonedR1Traj[i]) * 10000.0 / (1000.0 / (double)(_frequency));
                        velocity = (clonedR1Traj.ActualSpeed[i] > 0) ? clonedR1Traj.ActualSpeed[i] : velocity;
                        sb.Append(" VJ=");
                        sb.Append(velocity.ToString("00.00"));

                        _fileStreamWriter.WriteLine(sb.ToString());
                        sb.Clear();
                        #endregion
                    }

                    _fileStreamWriter.WriteLine("END");

                    _fileStreamWriter.Close();
                    #endregion
                }
                else
                {
                    returnValue = -1;
                }
                #endregion
            }
            catch (Exception ex)
            {
                returnValue = -1;
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        #endregion

        #region Private
        /// <summary>
        /// Reset the Dout Pins 1-13 for the trial data number.
        /// </summary>
        /// <returns>The reset trial data number string.</returns>
        private string ResetDoutPins()
        {
            string returnValue = string.Empty;
            bool[] binValue = new bool[14];

            try
            {
                for (int i = 0; i < binValue.Length; i++)
                {
                    binValue[i] = false;
                }

                returnValue = MakeDoutsPins(binValue);
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// Making the string for the output pins (1-13) for the AlphaOmega.
        /// </summary>
        /// <param name="binValue">The binary value to be sent to the AlphaOmega.</param>
        /// <returns>The string represents the command for the AlphaOmega for the number sending.</returns>
        private string MakeDoutsPins(bool[] binValue)
        {
            string returnValue = string.Empty;
            int bitIndex = 0;
            StringBuilder sb = new StringBuilder();

            try
            {
                foreach (bool bitValue in binValue)
                {
                    #region
                    sb.Append("DOUT OT#(");
                    if (bitIndex + 1 < 9)
                        sb.Append((bitIndex + 1).ToString("0"));
                    else
                        sb.Append((bitIndex + 1).ToString("00"));

                    if (bitValue)
                        sb.Append(") ON");
                    else
                        sb.Append(") OFF");

                    sb.AppendLine();

                    bitIndex++;
                    #endregion
                }

                returnValue = sb.ToString();
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        /// <summary>
        /// Convert commands of one points in the trajectory to a commands lines in a JBI format.
        /// </summary>
        /// <param name="traj">The trajectories of robot r1 to be converted to commands line (if needed as the updateJobType).</param>
        /// <param name="trajR2">The trajectories of robot r2 to be converted to commands line (if needed as the updateJobType).</param>
        /// <returns>
        /// The list of commands strings.
        /// Every item in the list is a line command in the JBI file.
        /// </returns>
        private List<string> JointsTrajectoryToLine(Trajectory traj)
        {
            List<string> stringLinesList = new List<string>();
            StringBuilder currectStringValue = new StringBuilder();
            int i = 0;

            try
            {
                #region
                //if need to encode the r1 robot for movement.
                //setting the tool0 for the robot0.
                //currectStringValue.Append("///TOOL0");
                //stringLinesList.Add(currectStringValue.ToString());
                currectStringValue.Clear();

                //setting all the points for the robot.
                foreach (double point in traj.S)
                {
                    #region
                    currectStringValue.Append("C");
                    currectStringValue.Append(i.ToString("D" + 5));
                    currectStringValue.Append("=");
                    currectStringValue.Append(((double)(point + ROriginalX)).ToString("0"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(traj.L[i] + ROriginalY)).ToString("0"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(traj.U[i] + ROriginalZ)).ToString("0"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(traj.R[i] + ROriginalRx)).ToString("0"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(traj.B[i] + ROriginalRy)).ToString("0"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(traj.T[i] + ROriginalRz)).ToString("0"));
                    //currectStringValue.Append(",");
                    i++;
                    stringLinesList.Add(currectStringValue.ToString());
                    currectStringValue.Clear();
                    #endregion
                }

                i = 0;
                //setting all the points for the robot.
                foreach (double point in traj.EX7Pulse)
                {
                    #region
                    currectStringValue.Append("BC");
                    currectStringValue.Append(i.ToString("D" + 5));
                    currectStringValue.Append("=");
                    currectStringValue.Append(((double)(point)).ToString("0"));
                    i++;
                    stringLinesList.Add(currectStringValue.ToString());
                    currectStringValue.Clear();
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return stringLinesList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="traj"></param>
        /// <returns></returns>
        private List<string> TCPTrajectoryToLine(Trajectory traj)
        {
            List<string> stringLinesList = new List<string>();
            StringBuilder currectStringValue = new StringBuilder();
            int i = 0;

            try
            {
                #region
                //if need to encode the r1 robot for movement.
                //setting the tool0 for the robot0.
                //currectStringValue.Append("///TOOL0");
                //stringLinesList.Add(currectStringValue.ToString());
                currectStringValue.Clear();

                //setting all the points for the robot.
                foreach (double point in traj.X)
                {
                    #region
                    currectStringValue.Append("C");
                    currectStringValue.Append(i.ToString("D" + 5));
                    currectStringValue.Append("=");
                    currectStringValue.Append(((double)(point + ROriginalX)).ToString("0.0000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(traj.Y[i] + ROriginalY)).ToString("0.0000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(traj.Z[i] + ROriginalZ)).ToString("0.0000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(traj.Rx[i] + ROriginalRx)).ToString("0.0000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(traj.Ry[i] + ROriginalRy)).ToString("0.0000"));
                    currectStringValue.Append(",");
                    currectStringValue.Append(((double)(traj.Rz[i] + ROriginalRz)).ToString("0.0000"));
                    //currectStringValue.Append(",");
                    i++;
                    stringLinesList.Add(currectStringValue.ToString());
                    currectStringValue.Clear();
                    #endregion
                }

                stringLinesList.Add("///RCONF 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");

                i = 0;
                //setting all the points for the robot.
                foreach (double point in traj.EX7Mm)
                {
                    #region
                    currectStringValue.Append("BC");
                    currectStringValue.Append(i.ToString("D" + 5));
                    currectStringValue.Append("=");
                    currectStringValue.Append(((double)(point)).ToString("0.0000"));
                    i++;
                    stringLinesList.Add(currectStringValue.ToString());
                    currectStringValue.Clear();
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return stringLinesList;
        }
        /// <summary>
        /// Converts a 14 digits decimal number to a binary array. Throw exception if the number is bigger than 14 disits representation.
        /// </summary>
        /// <param name="num">The number top be converted to a binary representation.</param>
        /// <returns>The binary representation of the number.</returns>
        private bool[] DecToBin(int num)
        {
            int index = 0;
            bool[] binValue = new bool[14];

            try
            {
                #region
                if (num > Math.Pow(2, 14))
                {
                    throw new Exception("The number cannot be represented by 13 digits");
                }
                else
                {
                    while (num > 0)
                    {
                        #region
                        binValue[index] = !((num % 2) == 0);

                        index++;

                        num = num / 2;
                        #endregion
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return binValue;
        }
        /// <summary>
        /// Velocity between 3D points.
        /// </summary>
        /// <param name="xSource">The source x value.</param>
        /// <param name="xDesdination">The destination x value.</param>
        /// <param name="ySource">The source y value.</param>
        /// <param name="yDestination">The destination y value.</param>
        /// <param name="zSource">The z soure value.</param>
        /// <param name="zDestination">The z destination value.</param>
        /// <returns>The distance between the 2 3D points.</returns>
        private double Velocity3D(double xSource, double xDesdination, double ySource, double yDestination, double zSource, double zDestination)
        {
            double result = 0;

            try
            {
                result += Math.Pow((xDesdination - xSource), 2);
                result += Math.Pow((yDestination - ySource), 2);
                result += Math.Pow((zDestination - zSource), 2);

                result = Math.Sqrt(result);
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return result;
        }
        /// <summary>
        /// Calaculates the velocity between 3D Points.
        /// </summary>
        /// <param name="source">The source position.</param>
        /// <param name="destination">The destination poisition.</param>
        /// <returns>The 3D velocity for the points.</returns>
        private double Velocity3D(RobotPosition source, RobotPosition destination)
        {
            double returnValue = 0.0;
            //todo:chek what about the other 3 axes : rx , ry , rz.

            try
            {
                returnValue = Velocity3D(source.X, destination.X, source.Y, destination.Y, source.Z, destination.Z);
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return returnValue;
        }
        #endregion
    }
}
