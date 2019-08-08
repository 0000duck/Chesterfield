using System;
using System.IO;
using System.Net.NetworkInformation;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Windows;
using Translations = ChesterfieldWpfApplication.Languages.LanguageTranslation;

namespace ChesterfieldWpfApplication.Models
{
    public class SingleFileControllerDownloader : BaseModel
    {
        #region ---------------- Constans ----------------
        const string _globalIncludes = @"/var/ftp/usrflash/Project/PMAC Script Language/Global Includes/";
        const string _libraries = @"/var/ftp/usrflash/Project/PMAC Script Language/Libraries/";
        const string _plcPrograms = @"/var/ftp/usrflash/Project/PMAC Script Language/PLC Programs/";
        #endregion

        #region ---------------- Fields --------------------

        ObservableCollection<string> _destinationFolders = new ObservableCollection<string>
        {
            "PMAC Script Language/Global Includes/",
            "PMAC Script Language/Libraries/",
            "PMAC Script Language/PLC Programs/"
        };

        private System.Net.IPAddress _deltaTauIPAddress = null;

        private bool _connected = false;
        private bool _download = false;
        private bool _remove = false;
        private bool _saveProject = false;
        private bool _reset = false;

        private string _connectMessgeText;
        private string _loginFailedText;
        private string _successText;
        private string _downloadText;
        private string _resetText;
        private string _disconnectText;
        private string _connectedToText;
        private string _connectText;
        private string _notConnectText;
        private string _errorText;
        private string _pleaseEnterAValidFileNameText;
        private string _cpuType = string.Empty;
        private string _version = string.Empty;
        private string _connectState = string.Empty;
        private string _saveProjectStatus = string.Empty;

        private string _filePath = string.Empty;
        private string _connection = "Connect";
        private string _errorList = string.Empty;
        private string _errorFilePath = string.Empty;

        private string _userName = "root";
        private string _password = "deltatau";
        private int _port = 22;
        private string _logfilePath = string.Empty;

        private string _downloadDestination = _plcPrograms;

        #endregion

        #region ---------------- Properties ---------------
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        [Range(0, 65535, ErrorMessage = "Wrong value")]
        public int Port
        {
            get
            {
                return _port;
            }
            set
            {
                if (value > 0 && value < 65535)
                {
                    Disconnect();
                    _port = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ErrorList
        {
            get
            {
                return _errorList;
            }
            set
            {
                _errorList = value;
                OnPropertyChanged();
            }
        }
        public string LogfilePath
        {
            get
            {
                return _logfilePath;
            }
            set
            {
                _logfilePath = value;
                OnPropertyChanged();
            }
        }
        public string Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
                OnPropertyChanged();
            }
        }
        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
                OnPropertyChanged();
            }
        }
        public string CPUType
        {
            get
            {
                return _cpuType;
            }
            set
            {
                _cpuType = value;
                OnPropertyChanged();
            }
        }
        public string ConnectState
        {
            get
            {
                return _connectState;
            }
            set
            {
                _connectState = value;
                OnPropertyChanged();
            }
        }
        public string DeltaTauIPAddress
        {
            get
            {
                return _deltaTauIPAddress.ToString();
            }
            set
            {
                System.Net.IPAddress _hostAddress = null;
                System.Net.IPAddress.TryParse(value, out _hostAddress);
                if (_hostAddress != null)
                {
                    Disconnect();
                    _deltaTauIPAddress = _hostAddress;
                    OnPropertyChanged();
                }
            }
        }
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }
        public string ErrorFilePath
        {
            get
            {
                return _errorFilePath;
            }
            set
            {
                _errorFilePath = value;
                OnPropertyChanged();
            }
        }
        public string SaveProjectStatus
        {
            get
            {
                return _saveProjectStatus;
            }
            set
            {
                _saveProjectStatus = value;
                OnPropertyChanged();
            }
        }
        public string DownloadDestination
        {
            get
            {
                return _downloadDestination;
            }
            set
            {
                if (value.Contains("Global Includes"))
                {
                    _downloadDestination = _globalIncludes;
                }
                else if (value.Contains("Libraries"))
                {
                    _downloadDestination = _libraries;
                }
                else if (value.Contains("PLC Programs"))
                {
                    _downloadDestination = _plcPrograms;
                }
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> DestinationFolders
        {
            get
            {
                return _destinationFolders;
            }
            set
            {
                _destinationFolders = value;
            }
        }
        #endregion

        #region ---------------- Methods -----------------
        /// <summary>
        /// 
        /// </summary>
        public SingleFileControllerDownloader()
        {
            try
            {
                SetLanguageTranslations();

                DeltaTauIPAddress = "192.168.0.200";
                Connection = _connectText;

                //this.ErrorFilePathTextBox.Visibility = Visibility.Visible;
                //this.ErrorFilePathLabel.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void SetLanguageTranslations()
        {
            try
            {
                                
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
    
        /// <summary>
        /// 
        /// </summary>
        private void DisconnectFailed()
        {
            Disconnect();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Disconnect()
        {
            try
            {
                _connected = false;
                Connection = _connectText;
                ConnectState = _notConnectText;
                CPUType = string.Empty;
                Version = string.Empty;
                FilePath = string.Empty;
                ErrorList = string.Empty;
                LogfilePath = string.Empty;
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void BrowseFile()
        {
            try
            {
                if (_connected)
                {
                    #region
                    System.Windows.Forms.OpenFileDialog ppmacScriptFile = new System.Windows.Forms.OpenFileDialog
                    {
                        Filter = "global includes (*.pmh)|*.pmh|libraries files (*.pmc)|*.pmc|script files (*.plc)|*.plc",
                        FilterIndex = 2,
                        RestoreDirectory = true
                    };

                    if (ppmacScriptFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        FilePath = ppmacScriptFile.FileName;
                        ErrorList = string.Empty;
                        LogfilePath = string.Empty;
                    }
                    #endregion
                }
                else
                {
                    ExpressionDark.DarkMessageBox.Show(_connectMessgeText, "Warning");
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        private bool IsValidIp(string strIP)
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
            }

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        private bool CheckPing(string IP)
        {
            Ping ping = new Ping();
            PingReply pingreply;
            bool result = false;

            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    if (IsValidIp(IP))
                    {
                        #region

                        pingreply = ping.Send(System.Net.IPAddress.Parse(IP), 15);

                        if (pingreply.RoundtripTime >= 0 && pingreply.Status.ToString() == "Success")
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }

                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex.Message);
            }

            return result;
        }
        #endregion
    }
}
