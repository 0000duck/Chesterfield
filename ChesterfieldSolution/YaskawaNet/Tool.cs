using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace YaskawaNet
{
    /// <summary>
    /// 
    /// </summary>
    [ComVisible(true)]
    [Guid("19C30823-BF59-42B4-BB5D-8ACD7B7A2196")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ITool
    {
        #region Properties

        #endregion

        #region Methods

        #endregion
    }
    [ComVisible(true)]
    public class Tool : ITool
    {
        #region Fields

        double _x = 0.0;
        double _y = 0.0;
        double _z = 0.0;
        double _rx = 0.0;
        double _ry = 0.0;
        double _rz = 0.0;
        double _w = 0.0;
        double _xg = 0.0;
        double _yg = 0.0;
        double _zg = 0.0;
        double _ix = 0.0;
        double _iy = 0.0;
        double _iz = 0.0;

        #endregion

        #region Properties

        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public double Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
            }
        }

        public double Rx
        {
            get
            {
                return _rx;
            }
            set
            {
                _rx = value;
            }
        }

        public double Ry
        {
            get
            {
                return _ry;
            }
            set
            {
                _ry = value;
            }
        }

        public double W
        {
            get
            {
                return _w;
            }
            set
            {
                _w = value;
            }
        }

        public double Xg
        {
            get
            {
                return _xg;
            }
            set
            {
                _xg = value;
            }
        }

        public double Yg
        {
            get
            {
                return _yg;
            }
            set
            {
                _yg = value;
            }
        }

        public double Zg
        {
            get
            {
                return _zg;
            }
            set
            {
                _zg = value;
            }
        }

        public double Ix
        {
            get
            {
                return _ix;
            }
            set
            {
                _ix = value;
            }
        }

        public double Iy
        {
            get
            {
                return _iy;
            }
            set
            {
                _iy = value;
            }
        }

        public double Iz
        {
            get
            {
                return _iz;
            }
            set
            {
                _iz = value;
            }
        }

        #endregion

        #region Methods
                  
        #endregion
    }
}
