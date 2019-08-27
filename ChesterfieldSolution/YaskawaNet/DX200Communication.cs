using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace YaskawaNet
{
    public static class DX200Communication
    {
        public static bool IsValidIp(string strIP)
        {
            //  Split string by ".", check that array length is 3
            char chrFullStop = '.';
            string[] arrOctets = strIP.Split(chrFullStop);

            try
            {
                if (arrOctets.Length != 4)
                {
                    return false;
                }
                //  Check each substring checking that the int value is less than 255 and that is char[] length is !> 2
                Int16 MAXVALUE = 255;
                Int32 temp; // Parse returns Int32
                foreach (String strOctet in arrOctets)
                {
                    if (strOctet.Length > 3)
                    {
                        return false;
                    }

                    if (strOctet.Length > 0)
                    {
                        temp = int.Parse(strOctet);
                        if (temp > MAXVALUE)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
                return false;
            }

            return true;
        }

        public static ConnectionStatus CheckPing(string IP)
        {
            Ping ping = new Ping();
            PingReply pingreply;
            ConnectionStatus returnValue = ConnectionStatus.Unreachable;

            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())//check network
                {
                    if (IsValidIp(IP))
                    {
                        #region

                        pingreply = ping.Send(IPAddress.Parse(IP), 25);

                        if (pingreply.RoundtripTime >= 0 && pingreply.Status.ToString() == "Success")
                        {
                            returnValue = ConnectionStatus.Reachable;
                        }
                        else
                        {
                            returnValue = ConnectionStatus.Unreachable;
                        }

                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {                  
                returnValue = ConnectionStatus.Unreachable;
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return returnValue;
        }
    }
}
