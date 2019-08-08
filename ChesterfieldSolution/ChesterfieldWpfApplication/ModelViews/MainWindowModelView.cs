using System;
using System.ComponentModel.DataAnnotations;
using ChesterfieldWpfApplication.Models;
using YaskawaNet;

namespace ChesterfieldWpfApplication.ModelViews
{
    public class MainWindowModelView : BaseModel
    {
        #region --------------- Properties --------------

        private DX200 _robotController = new DX200("192.168.17.77", "");
        private string _controllerIPAddress = "";

        public DX200 RobotController
        {
            get
            {
                return _robotController;
            }
            set
            {
                _robotController = value;
                OnPropertyChanged();
            }
        }
        public string ControllerIPAddress
        {
            get
            {
                return _controllerIPAddress;
            }
            set
            {
                _controllerIPAddress = value;
                OnPropertyChanged();
            }
        }
        //[Range(0, 65535, ErrorMessage = "Wrong value")]
        //public int Port
        //{
        //    get
        //    {
        //        return _port;
        //    }
        //    set
        //    {
        //        _port = value;
        //        OnPropertyChanged();
        //    }
        //}

        #endregion

        #region --------------- ICommands ------------
        //public RelayCommand Connect
        //{
        //    get
        //    {
        //        return (new RelayCommand(
        //           x =>
        //           {
        //               _fileDownloader.Connect();
        //           }));
        //    }
        //}
        //public RelayCommand Browse
        //{
        //    get
        //    {
        //        return (new RelayCommand(
        //           x =>
        //           {
        //               _fileDownloader.BrowseFile();
        //           }));
        //    }
        //}
        //public RelayCommand Download
        //{
        //    get
        //    {
        //        return (new RelayCommand(
        //           x =>
        //           {
        //               _fileDownloader.DownloadFile();
        //           }));
        //    }
        //}
        //public RelayCommand Remove
        //{
        //    get
        //    {
        //        return (new RelayCommand(
        //           x =>
        //           {
        //               _fileDownloader.RemoveFile();
        //           }));
        //    }
        //}
        //public RelayCommand ChangeIP
        //{
        //    get
        //    {
        //        return (new RelayCommand(
        //           x =>
        //           {
        //               _fileDownloader.DeltaTauIPAddress = _ipAddressDT;
        //           }));
        //    }
        //}
        #endregion

        #region --------------- Methods ----------------
        public MainWindowModelView()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
