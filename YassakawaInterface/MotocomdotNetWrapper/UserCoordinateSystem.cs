using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MotoCom32Net
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(true)]
    [Guid("2AB49FAF-96FF-4690-B80D-DC489AD858CC")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IUserCoordinateSystem
    {
        #region Properties
        double Org_X
        {
            get;
            set;
        }
        double Org_Y
        {
            get;
            set;
        }
        double Org_Z
        {
            get;
            set;
        }
        double Org_Rx
        {
            get;
            set;
        }
        double Org_Ry
        {
            get;
            set;
        }
        double Org_Rz
        {
            get;
            set;
        }
        double Org_Form
        {
            get;
            set;
        }
        double XX_X
        {
            get;
            set;
        }
        double XX_Y
        {
            get;
            set;
        }
        double XX_Z
        {
            get;
            set;
        }
        double XX_Rx
        {
            get;
            set;
        }
        double XX_Ry
        {
            get;
            set;
        }
        double XX_Rz
        {
            get;
            set;
        }
        double XX_Form
        {
            get;
            set;
        }
        double XY_X
        {
            get;
            set;
        }
        double XY_Y
        {
            get;
            set;
        }
        double XY_Z
        {
            get;
            set;
        }
        double XY_Rx
        {
            get;
            set;
        }
        double XY_Ry
        {
            get;
            set;
        }
        double XY_Rz
        {
            get;
            set;
        }
        double XY_Form
        {
            get;
            set;
        }
        double ToolNumber
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

        double[] UserCoordinateData
        {
            get;
            set;
        }

        #endregion

        #region Methods

        #endregion
    }
    [ComVisible(true)]
    public class UserCoordinateSystem : IUserCoordinateSystem
    {
        double[] _userCoordinateData = new double[28];

        public UserCoordinateSystem
            (
            double org_x, double org_y, double org_z, double org_rx, double org_ry, double org_rz, double org_form,
            double xx_x, double xx_y, double xx_z, double xx_rx, double xx_ry, double xx_rz, double xx_form,
            double xy_x, double xy_y, double xy_z, double xy_rx, double xy_ry, double xy_rz, double xy_form, double toolNumber,
            double e7axis, double e8axis, double e9axis, double e10axis, double e11axis, double e12axis
            )
        {
            Org_X = org_x;
            Org_Y = org_y;
            Org_Z = org_z;
            Org_Rx = org_rx;
            Org_Ry = org_ry;
            Org_Rz = org_rz;
            Org_Form = org_form;

            XX_X = xx_x;
            XX_Y = xx_y;
            XX_Z = xx_z;
            XX_Rx = xx_rx;
            XX_Ry = xx_ry;
            XX_Rz = xx_rz;
            XX_Form = xx_form;

            XY_X = xy_x;
            XY_Y = xy_y;
            XY_Z = xy_z;
            XY_Rx = xy_rx;
            XY_Ry = xy_ry;
            XY_Rz = xy_rz;
            XY_Form = xy_form;

            ToolNumber = toolNumber;

            E7Axis = e7axis;
            E8Axis = e8axis;
            E9Axis = e9axis;
            E10Axis = e10axis;
            E11Axis = e11axis;
            E12Axis = e12axis;
        }
        public UserCoordinateSystem()
        {
            Org_X = 0;
            Org_Y = 0;
            Org_Z = 0;
            Org_Rx = 0;
            Org_Ry = 0;
            Org_Rz = 0;
            Org_Form = 0.0;

            XX_X = 0.0;
            XX_Y = 0.0;
            XX_Z = 0.0;
            XX_Rx = 0.0;
            XX_Ry = 0.0;
            XX_Rz = 0.0;
            XX_Form = 0.0;

            XY_X = 0;
            XY_Y = 0;
            XY_Z = 0;
            XY_Rx = 0;
            XY_Ry = 0;
            XY_Rz = 0;
            XY_Form = 0;

            ToolNumber = 0;

            E7Axis = 0;
            E8Axis = 0;
            E9Axis = 0;
            E10Axis = 0;
            E11Axis = 0;
            E12Axis = 0;

            _userCoordinateData.Initialize();
        }
        public UserCoordinateSystem(double[] userCoordinateData)
        {
            userCoordinateData.CopyTo(_userCoordinateData, 0);
        }

        public double Org_X
        {
            get
            {
                return _userCoordinateData[0];
            }
            set
            {
                _userCoordinateData[0] = value;
            }
        }
        public double Org_Y
        {
            get
            {
                return _userCoordinateData[1];
            }
            set
            {
                _userCoordinateData[1] = value;
            }
        }
        public double Org_Z
        {
            get
            {
                return _userCoordinateData[2];
            }
            set
            {
                _userCoordinateData[2] = value;
            }
        }
        public double Org_Rx
        {
            get
            {
                return _userCoordinateData[3];
            }
            set
            {
                _userCoordinateData[3] = value;
            }
        }
        public double Org_Ry
        {
            get
            {
                return _userCoordinateData[4];
            }
            set
            {
                _userCoordinateData[4] = value;
            }
        }
        public double Org_Rz
        {
            get
            {
                return _userCoordinateData[5];
            }
            set
            {
                _userCoordinateData[5] = value;
            }
        }
        public double Org_Form
        {
            get
            {
                return _userCoordinateData[6];
            }
            set
            {
                _userCoordinateData[6] = value;
            }
        }
        public double XX_X
        {
            get
            {
                return _userCoordinateData[7];
            }
            set
            {
                _userCoordinateData[7] = value;
            }
        }
        public double XX_Y
        {
            get
            {
                return _userCoordinateData[8];
            }
            set
            {
                _userCoordinateData[8] = value;
            }
        }
        public double XX_Z
        {
            get
            {
                return _userCoordinateData[9];
            }
            set
            {
                _userCoordinateData[9] = value;
            }
        }
        public double XX_Rx
        {
            get
            {
                return _userCoordinateData[10];
            }
            set
            {
                _userCoordinateData[10] = value;
            }
        }
        public double XX_Ry
        {
            get
            {
                return _userCoordinateData[11];
            }
            set
            {
                _userCoordinateData[11] = value;
            }
        }
        public double XX_Rz
        {
            get
            {
                return _userCoordinateData[12];
            }
            set
            {
                _userCoordinateData[12] = value;
            }
        }
        public double XX_Form
        {
            get
            {
                return _userCoordinateData[13];
            }
            set
            {
                _userCoordinateData[13] = value;
            }
        }
        public double XY_X
        {
            get
            {
                return _userCoordinateData[14];
            }
            set
            {
                _userCoordinateData[14] = value;
            }
        }
        public double XY_Y
        {
            get
            {
                return _userCoordinateData[15];
            }
            set
            {
                _userCoordinateData[15] = value;
            }
        }
        public double XY_Z
        {
            get
            {
                return _userCoordinateData[16];
            }
            set
            {
                _userCoordinateData[16] = value;
            }
        }
        public double XY_Rx
        {
            get
            {
                return _userCoordinateData[17];
            }
            set
            {
                _userCoordinateData[17] = value;
            }
        }
        public double XY_Ry
        {
            get
            {
                return _userCoordinateData[18];
            }
            set
            {
                _userCoordinateData[18] = value;
            }
        }
        public double XY_Rz
        {
            get
            {
                return _userCoordinateData[19];
            }
            set
            {
                _userCoordinateData[19] = value;
            }
        }
        public double XY_Form
        {
            get
            {
                return _userCoordinateData[20];
            }
            set
            {
                _userCoordinateData[20] = value;
            }
        }
        public double ToolNumber
        {
            get
            {
                return _userCoordinateData[21];
            }
            set
            {
                _userCoordinateData[21] = value;
            }
        }
        public double E7Axis
        {
            get
            {
                return _userCoordinateData[22];
            }
            set
            {
                _userCoordinateData[22] = value;
            }
        }
        public double E8Axis
        {
            get
            {
                return _userCoordinateData[23];
            }
            set
            {
                _userCoordinateData[23] = value;
            }
        }
        public double E9Axis
        {
            get
            {
                return _userCoordinateData[24];
            }
            set
            {
                _userCoordinateData[24] = value;
            }
        }
        public double E10Axis
        {
            get
            {
                return _userCoordinateData[25];
            }
            set
            {
                _userCoordinateData[25] = value;
            }
        }
        public double E11Axis
        {
            get
            {
                return _userCoordinateData[26];
            }
            set
            {
                _userCoordinateData[26] = value;
            }
        }
        public double E12Axis
        {
            get
            {
                return _userCoordinateData[27];
            }
            set
            {
                _userCoordinateData[27] = value;
            }
        }

        public double[] UserCoordinateData
        {
            get
            {
                return _userCoordinateData;
            }
            set
            {
                _userCoordinateData = value;
            }
        }
    }
}
