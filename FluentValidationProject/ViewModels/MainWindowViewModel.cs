using FluentValidation.Results;
using FluentValidationProject.Models;
using FluentValidationProject.Validators;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows;
using static FluentValidationProject.Models.ValidatableBindableBase<FluentValidationProject.Models.MariaDBModel, FluentValidationProject.Validators.MariaDBValidator>;

namespace FluentValidationProject.ViewModels
{
    public class MainWindowViewModel : ValidatableBindableBase<MainWindowViewModel, MariaDBValidator>
    {
        
        private string _title = "Fluent Validation with Prism";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

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


        private bool _isModelValid;
        public bool IsModelValid
        {
            get { return _isModelValid; }
            private set { SetProperty(ref _isModelValid, value); }
        }
        private bool _isPropertyValid = false;
        public bool IsPropertyValid
        {
            get { return _isPropertyValid; }
            set { SetProperty(ref _isPropertyValid, value); }
        }
        public MainWindowViewModel()
        {
            ViewModelToValidate = this;
            this.OnModelPropertyValidationChanged += new PropertyErrorChangeEventHandler<bool,string>(ModelPropertyErrorChanged);
            this.OnModelValidationChanged += new ModelValidationChangedEventHandler<bool>(ModelErrorChangee);
        }

        private void ModelErrorChangee(object sender, bool e)
        {
            
           IsModelValid=e;
            SaveOnModelValidCommand.RaiseCanExecuteChanged();
        }

        
        private void ModelPropertyErrorChanged(object sender, bool e, string propertyName)
        {
            
            if (propertyName=="DSName") IsPropertyValid = e;
            SaveOnPropertyValidCommand.RaiseCanExecuteChanged();
        }



        private DelegateCommand _saveOnPropertyValidCommand;
        public DelegateCommand SaveOnPropertyValidCommand =>
            _saveOnPropertyValidCommand ?? (_saveOnPropertyValidCommand = new DelegateCommand(ExecuteSaveOnPropertyValidCommand, CanExecuteSaveOnPropertyValidCommand));

        void ExecuteSaveOnPropertyValidCommand()
        {
            MessageBox.Show("Yes");
        }

        bool CanExecuteSaveOnPropertyValidCommand()
        {
            return IsPropertyValid;
        }

        private DelegateCommand _saveOnModelValidCommand;
        public DelegateCommand SaveOnModelValidCommand =>
            _saveOnModelValidCommand ?? (_saveOnModelValidCommand = new DelegateCommand(ExecuteSaveOnModelValidCommand, CanExecuteSaveOnModelValidCommand));

        void ExecuteSaveOnModelValidCommand()
        {

        }

        bool CanExecuteSaveOnModelValidCommand()
        {
            return IsModelValid;
        }
 
    }
}
