using FluentValidationProject.Validators;
using Prism.Mvvm;
using System.ComponentModel;
using FluentValidation.Results;
namespace FluentValidationProject.Models
{
    public class MariaDBModel : BindableBase
    {
      
        private string _dsName;
        public string DSName
        {
            get { return _dsName; }
            set { SetProperty(ref _dsName, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _dbType;
        public string DBType
        {
            get { return _dbType; }
            set { SetProperty(ref _dbType, value); }
        }

        private string _ipAddress;
        public string IPAddress
        {
            get { return _ipAddress; }
            set { SetProperty(ref _ipAddress, value); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private bool _ivalid;
        public bool IValid
        {
            get { return _ivalid; }
           private  set { SetProperty(ref _ivalid, value); }
        }
      
       
    }
}
