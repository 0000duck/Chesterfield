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
    [Guid("EFA73ABE-5B71-4900-8CA5-FD5120A0F8B0")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IConfiguration
    {
        #region Properties
        byte Formcode
        {
            get;
            set;
        }
        bool Flip
        {
            get;
            set;
        }
        bool ElbowAbove
        {
            get;
            set;
        }
        bool FrontSide
        {
            get;
            set;
        }
        bool Rbelow180
        {
            get;
            set;
        }
        bool Tbelow180
        {
            get;
            set;
        }
        bool Sbelow180
        {
            get;
            set;
        }

        #endregion

        #region Methods

        #endregion
    }
    [ComVisible(true)]
    public class Configuration : IConfiguration
    {
        byte _formCode = 0;
        public Configuration(byte formcode)
        {
            _formCode = formcode;
        }
        public Configuration(bool flip, bool elbowabove, bool frontside, bool rbelow180, bool tbelow180, bool sbelow180)
        {
            Flip = flip;
            ElbowAbove = elbowabove;
            FrontSide = frontside;
            Rbelow180 = rbelow180;
            Tbelow180 = tbelow180;
            Sbelow180 = sbelow180;
        }

        public byte Formcode
        {
            get
            {
                return _formCode;
            }
            set
            {
                _formCode = value;
            }
        }
        public bool Flip
        {
            get
            {
                return (_formCode & (1 << 0)) > 0 ? true : false;
            }
            set
            {
                _formCode = value ?(byte)( _formCode |(1 << 0)) :(byte)( _formCode & ~(1 << 0));
            }
        }
        public bool ElbowAbove
        {
            get
            {
                return (_formCode & (1 << 1)) > 0 ? true : false;
            }
            set
            {
                _formCode = value ?(byte)( _formCode | (1 << 1)) :(byte)( _formCode & ~(1 << 1));
            }
        }
        public bool FrontSide
        {
            get
            {
                return (_formCode & (1 << 2)) > 0 ? true : false;
            }
            set
            {
                _formCode = value ?(byte)( _formCode | (1 << 2)) :(byte)( _formCode & ~(1 << 2));
            }
        }
        public bool Rbelow180
        {
            get
            {
                return (_formCode & (1 << 3)) > 0 ? true : false;
            }
            set
            {
                _formCode = value ?(byte)( _formCode | (1 << 3)) :(byte)( _formCode & ~(1 << 3));
            }
        }
        public bool Tbelow180
        {
            get
            {
                return (_formCode & (1 << 4)) > 0 ? true : false;
            }
            set
            {
                _formCode = value ?(byte)( _formCode | (1 << 4)) : (byte)(_formCode & ~(1 << 4));
            }
        }
        public bool Sbelow180
        {
            get
            {
                return (_formCode & (1 << 5)) > 0 ? true : false;
            }
            set
            {
                _formCode = value ?(byte)( _formCode | (1 << 5)) :(byte)( _formCode & ~(1 << 5));
            }
        }
    }
}
