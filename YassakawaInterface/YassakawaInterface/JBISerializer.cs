using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakawaInterface
{
    public class JBISerializer
    {
        #region MEMBERS
        /// <summary>
        /// The JBI fileName to write the commands to it.
        /// </summary>
        private string _fileName;

        /// <summary>
        /// The frequency the commands rely on (to make the velocity).
        /// </summary>
        private int _frequency;
        #endregion MEMBERS

        #region CTOR
        public JBISerializer(int frequency = 50)
        {
            _frequency = frequency;
        }
        #endregion CTOR

        #region SETTERS_GETTERS
        /// <summary>
        /// Set or get the frequency the JBIFileCreator rely on.
        /// </summary>
        public int Frequency { get { return _frequency; } set { _frequency = value; } }
        /// <summary>
        /// Get or set the R1 robot trajectory position to be written to the controller JBI file.
        /// </summary>
        public Trajectory RTrajectory { get; set; }
        #endregion SETTERS_GETTERS

        #region FUNCTIONS
        public void DecodePosition(string jbiFileName, Position position, double velocity)
        {
            StreamWriter _fileStreamWriter = new StreamWriter(_fileName);

            StringBuilder lineStringBuilder = new StringBuilder();

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
            lineStringBuilder.Append(position.RX.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(position.RY.ToString("0000.0000000"));
            lineStringBuilder.Append(",");
            lineStringBuilder.Append(position.RZ.ToString("0000.0000000"));

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
        }

        /// <summary>
        /// Decode the trajectory commands to a JBI file.
        /// <param name="clonedR1Traj">The r1 robot traj to be written to the file as the protocol format.</param>
        /// <param name="clonedR2Traj">The r2 robot traj to be written to the file as the protocol format.</param>
        /// <param name="updateJobType">The robots type to update the job trajectory with.</param>
        /// <param name="returnBackMotion">Indicate if the motion is backword.</param>
        /// </summary>
        private void DecodeTrajectory(Trajectory traj)
        {
            StreamWriter _fileStreamWriter = new StreamWriter(_fileName);

            _fileStreamWriter.WriteLine("/JOB");
            _fileStreamWriter.WriteLine("//NAME GAUSSIANMOVING2");
            _fileStreamWriter.WriteLine("//POS");

            _fileStreamWriter.Write("///NPOS 0,0,0,"); _fileStreamWriter.Write(traj.Count + 1); _fileStreamWriter.WriteLine(",0,0");

            _fileStreamWriter.WriteLine("///TOOL 0");
            _fileStreamWriter.WriteLine("///POSTYPE ROBOT");
            _fileStreamWriter.WriteLine("///RECTAN");
            _fileStreamWriter.WriteLine("///RCONF 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
            _fileStreamWriter.WriteLine("P00000=10.000,0.000,0.000,0.0000,0.0000,0.0000");
            _fileStreamWriter.WriteLine("///POSTYPE BASE");



            Trajectory clonedR1Traj = traj.Clone();
            //adding the zero point place for the trajectory (for the velocity calculaion behind) at the end if it is backward or at the beginning if it is forward movement.
            //also, for the backward movement it skip the last point (because the robot is already there from the forward movement) and added the 0 placed to the end of the trajectory.

            foreach (string lineString in TrajectoryToLine(clonedR1Traj))
            {
                _fileStreamWriter.WriteLine(lineString);
            }

            clonedR1Traj.InsertOriginPlace(true);

            _fileStreamWriter.WriteLine("//INST");
            _fileStreamWriter.WriteLine("///DATE 2017/03/31 08:11");

            _fileStreamWriter.WriteLine("///ATTR SC,RW");

            _fileStreamWriter.WriteLine("///GROUP1 RB1");

            _fileStreamWriter.WriteLine("NOP");

            StringBuilder sb = new StringBuilder();

            //make the f * duration velocity points vector from the f * duratoin + 1 places points in the trajectory.
            for (int i = 0; i < clonedR1Traj.Count - 1; i++)
            {
                //decode the velocity for the selected robot (if only one of then) or the first robot (r1) if both of them.
                sb.Append("MOVL ");
                sb.Append("P");
                sb.Append((i + 1).ToString("D" + 5));
                double velocity = Velocity3D(clonedR1Traj[i + 1], clonedR1Traj[i]) * 10000.0 / (1000.0 / (double)(_frequency));
                sb.Append(" V=");
                sb.Append(velocity.ToString("0000.00000000"));

                _fileStreamWriter.WriteLine(sb.ToString());
                sb.Clear();
            }

            _fileStreamWriter.WriteLine("END");

            _fileStreamWriter.Close();
        }

        /// <summary>
        /// Reset the Dout Pins 1-13 for the trial data number.
        /// </summary>
        /// <returns>The reset trial data number string.</returns>
        public string ResetDoutPins()
        {
            bool[] binValue = new bool[14];

            for (int i = 0; i < binValue.Length; i++)
            {
                binValue[i] = false;
            }

            return MakeDoutsPins(binValue);
        }

        /// <summary>
        /// Making the string for the output pins (1-13) for the AlphaOmega.
        /// </summary>
        /// <param name="binValue">The binary value to be sent to the AlphaOmega.</param>
        /// <returns>The string represents the command for the AlphaOmega for the number sending.</returns>
        public string MakeDoutsPins(bool[] binValue)
        {
            int bitIndex = 0;
            StringBuilder sb = new StringBuilder();

            foreach (bool bitValue in binValue)
            {
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
            }

            return sb.ToString();
        }
        #endregion FUNCTIONS

        #region PRIVATE_FUNCTIONS
        /// <summary>
        /// Convert commands of one points in the trajectory to a commands lines in a JBI format.
        /// </summary>
        /// <param name="traj">The trajectories of robot r1 to be converted to commands line (if needed as the updateJobType).</param>
        /// <param name="trajR2">The trajectories of robot r2 to be converted to commands line (if needed as the updateJobType).</param>
        /// <returns>
        /// The list of commands strings.
        /// Every item in the list is a line command in the JBI file.
        /// </returns>
        private List<string> TrajectoryToLine(Trajectory traj)
        {
            List<string> stringLinesList = new List<string>();

            StringBuilder currectStringValue = new StringBuilder();

            //if need to encode the r1 robot for movement.
            int i = 1;

            //setting the tool0 for the robot0.
            currectStringValue.Append("///TOOL0");
            stringLinesList.Add(currectStringValue.ToString());
            currectStringValue.Clear();

            //setting all the points for the robot.
            foreach (double point in traj.X)
            {
                currectStringValue.Append("P");
                currectStringValue.Append(i.ToString("D" + 5));
                currectStringValue.Append("=");
                currectStringValue.Append(((double)(point * 10 + YASSAKAWA_SETTINGS.Default.ROriginalX)).ToString("0000.00000000"));
                currectStringValue.Append(",");
                currectStringValue.Append(((double)(traj.Y[i - 1] * 10 + YASSAKAWA_SETTINGS.Default.ROriginalY)).ToString("0000.00000000"));
                currectStringValue.Append(",");
                currectStringValue.Append(((double)(traj.Z[i - 1] * 10 + YASSAKAWA_SETTINGS.Default.ROriginalZ)).ToString("0000.00000000"));
                currectStringValue.Append(",");
                currectStringValue.Append(((double)(traj.RX[i - 1] * 10 + YASSAKAWA_SETTINGS.Default.ROriginalRX)).ToString("0000.00000000"));
                currectStringValue.Append(",");
                currectStringValue.Append(((double)(traj.RY[i - 1] * 10 + YASSAKAWA_SETTINGS.Default.ROriginalRY)).ToString("0000.00000000"));
                currectStringValue.Append(",");
                currectStringValue.Append(((double)(traj.RZ[i - 1] * 10 + YASSAKAWA_SETTINGS.Default.ROriginalRZ)).ToString("0000.00000000"));
                currectStringValue.Append(",");
                i++;
                stringLinesList.Add(currectStringValue.ToString());
                currectStringValue.Clear();
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
            if (num > Math.Pow(2, 14))
            {
                throw new Exception("The number cannot be represented by 13 digits");
            }

            else
            {
                int index = 0;
                bool[] binValue = new bool[14];

                while (num > 0)
                {
                    binValue[index] = !((num % 2) == 0);

                    index++;

                    num = num / 2;
                }

                return binValue;
            }
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

            result += Math.Pow((xDesdination - xSource), 2);
            result += Math.Pow((yDestination - ySource), 2);
            result += Math.Pow((zDestination - zSource), 2);

            result = Math.Sqrt(result);

            return result;
        }

        /// <summary>
        /// Calaculates the velocity between 3D Points.
        /// </summary>
        /// <param name="source">The source position.</param>
        /// <param name="destination">The destination poisition.</param>
        /// <returns>The 3D velocity for the points.</returns>
        private double Velocity3D(Position source, Position destination)
        {
            //todo:chek what about the other 3 axes : rx , ry , rz.
            return Velocity3D(source.X, destination.X,
                              source.Y, destination.Y,
                              source.Z, destination.Z);
        }
        #endregion PRIVATE_FUNCTIONS
    }
}
  