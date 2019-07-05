using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YassakawaInterface;

namespace YassakawaInterfaceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            YassakawaController yassakawaController = new YassakawaController("10.0.0.2", "");

            Servo_ON_Test(yassakawaController);

            //Moving_Linear_Z_Test(yassakawaController);

            //Moving_Linear_Increament_Cartesian_Test(yassakawaController);

            //Get_Current_Posision_Test(yassakawaController);

            Move_Joint_Cartesian_Target_Test(yassakawaController);

            Thread.Sleep(2000);

            Servo_OFF_Test(yassakawaController);

            Disconnect_Test(yassakawaController);

            Console.ReadLine();
        }

        public static void Connect_Test(YassakawaController yassakawaController)
        {
            yassakawaController.Connect();
        }

        public static void Disconnect_Test(YassakawaController yassakawaController)
        {
            yassakawaController.Disconnect();
        }

        public static void Servo_ON_Test(YassakawaController yassakawaController)
        {
            yassakawaController.ServoOn();
        }

        public static void Servo_OFF_Test(YassakawaController yassakawaController)
        {
            yassakawaController.ServoOff();
        }

        public static void Moving_Linear_X_Test(YassakawaController yassakawaController)
        {
            short result = yassakawaController.MoveLinearIncrementX("V" , 50 , "ROBOT" , 0 , 0 , -40); ;

            Console.WriteLine($"{result}");
        }

        #region MOVING_LINEAR_OPERATIONS_TESTS
        public static void Moving_Linear_Y_Test(YassakawaController yassakawaController)
        {
            short result = yassakawaController.MoveLinearIncrementY("V", 10, "ROBOT", 0, 0, 5); ;

            Console.WriteLine($"{result}");
        }
        public static void Moving_Linear_Z_Test(YassakawaController yassakawaController)
        {
            short result = yassakawaController.MoveLinearIncrementZ("V", 10, "ROBOT", 0, 0, 50); ;

            Console.WriteLine($"{result}");
        }
        public static void Moving_Linear_RX_Test(YassakawaController yassakawaController)
        {
            short result = yassakawaController.MoveLinearIncrementRX("V", 10, "ROBOT", 0, 0, 5); ;

            Console.WriteLine($"{result}");
        }
        public static void Moving_Linear_RY_Test(YassakawaController yassakawaController)
        {
            short result = yassakawaController.MoveLinearIncrementRY("V", 10, "ROBOT", 0, 0, 5); ;

            Console.WriteLine($"{result}");
        }
        public static void Moving_Linear_RZ_Test(YassakawaController yassakawaController)
        {
            short result = yassakawaController.MoveLinearIncrementRZ("V", 10, "ROBOT", 0, 0, 5); ;

            Console.WriteLine($"{result}");
        }
        public static void Moving_Linear_Increament_Cartesian_Test(YassakawaController yassakawaController)
        {
            double[] increamentValue = new double[]
            {
                1,
                1,
                1,
                1,
                1,
                1,
                0,
                0,
                0,
                0,
                0,
                0
            };
            yassakawaController.MoveLinearIncrementCartesian("V", 5, "ROBOT", 0, 0, ref increamentValue[0]);
        }
        #endregion MOVING_LINEAR_OPERATIONS_TESTS

        #region PULSE_JOINT_MOVING_TESTS
        public static void Move_Joint_Cartesian_Target_Test(YassakawaController yassakawaController)
        {
            double[] targetPosition = new double[12];
            short rconf = 0;
            yassakawaController.GetCurrentPosition(false, ref rconf, ref targetPosition[0]);

            targetPosition[0] += 50;
            targetPosition[0] += 50;
            targetPosition[0] += 50;

            short result = yassakawaController.MoveJointCartesianTarget("V", 1, "ROBOT", 0, 0, ref targetPosition[0]);

            Console.WriteLine($"{result}");
        }
        #endregion PULSE_JOINT_MOVING_TESTS

        #region FEEDBACK_STATUSES_TESTS
        public static void Get_Current_Posision_Test(YassakawaController yassakawaController)
        {
            double[] position = new double[12];
            short rconf = 0;
            yassakawaController.GetCurrentPosition(false, ref rconf, ref position[0]);
            Console.WriteLine($"Current position: {position[0]} , {position[1]} , {position[2]} , {position[3]} , {position[4]} , {position[5]}");
            yassakawaController.MoveLinearIncrementX("V", 10, "ROBOT", 1, 1, 1);
            //wait for the movement to end.
            Thread.Sleep(5000);
            yassakawaController.GetCurrentPosition(false, ref rconf, ref position[0]);
            Console.WriteLine($"Current position: {position[0]} , {position[1]} , {position[2]} , {position[3]} , {position[4]} , {position[5]}");
        }
        #endregion FEEDBACK_STATUSES_TESTS
    }
}
