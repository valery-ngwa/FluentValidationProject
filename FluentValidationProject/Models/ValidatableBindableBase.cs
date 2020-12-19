using FluentValidation;
using FluentValidation.Results;
using Microsoft.Collections.Extensions;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FluentValidationProject.Models
{
    public class ValidatableBindableBase<TViewModel,TValidator> : BindableBase, INotifyDataErrorInfo where TViewModel:class,new() where TValidator:class,new()
    {
        protected MultiValueDictionary<string, string> errors = new MultiValueDictionary<string, string>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { return; };

        public delegate void PropertyErrorChangeEventHandler<TEventArgs, TString>(Object? sender, TEventArgs e, TString propertyname);
        public event PropertyErrorChangeEventHandler<bool, string> OnModelPropertyValidationChanged = delegate { return; };

        public delegate void ModelValidationChangedEventHandler<TEventArgs>(Object? sender, TEventArgs e);
        public event ModelValidationChangedEventHandler<bool> OnModelValidationChanged = delegate { return; };

        protected IValidator<TViewModel> validator;
        protected TViewModel ViewModelToValidate { get; set; }

        public ValidatableBindableBase()
        {
            validator = (IValidator<TViewModel>)new TValidator();
            this.PropertyChanged += new PropertyChangedEventHandler(UpdateProperty);
        }
        private void UpdateProperty(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                ValidationResult result = validator.Validate(ViewModelToValidate);
                var errors = convert(result);
                SetValidationErrors(errors,e.PropertyName);
                foreach (var err in errors)
                {
                    if (err.PropertyName == e.PropertyName)
                    {
                        OnPropertyErrorChange(false, e.PropertyName);
                        return;
                    }
                    else OnPropertyErrorChange(true, e.PropertyName);
                }
                if (!result.IsValid) OnModelErrorChanged(true);
                else OnModelErrorChanged(false);
            }
            finally
            {

            }
        }
        /// <summary>
        /// Gets the validation errors for a property of the entire model.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) ||
               !errors.ContainsKey(propertyName)) return null;
            return errors[propertyName];
        }

        /// <summary>
        /// Gets a value that indicates the model has errors.
        /// </summary>
        public virtual bool HasErrors
        {
            get { return errors.Any((propertyErrors) => propertyErrors.Value.Count > 0); }
        }
        public void SetValidationErrors(IEnumerable<Error> errors, string pname)
        {
           
            
            this.errors.Clear();
            foreach (var error in errors)
            {
                
                this.errors.Add(error.PropertyName, error.ErrorMessage);
            }

            //signal error change for all properties
            var properties = getProperties();
            foreach (var property in properties)
            {

                if (property==pname)
                {
                    signalErrorChange(property);
                }
            }
            
        }

        private void signalErrorChange(string property)
        {
            if (property == null) throw new NullReferenceException("The property must not be null");
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(property));
        }

        private IEnumerable<string> getProperties()
        {
            var properties = this.GetType().GetProperties().Select(p => p.Name).ToList();
            properties.Remove("HasErrors"); //remove the HasErrors property, because it is part of the interface INotifyDataErrorInfo and not of the actual model
            return properties;
        }
        /// <summary>
        /// utility method to convert the FluentValidation ValidationResult to a collection of Error objects
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        protected IEnumerable<Error> convert(ValidationResult validationResult)
        {
            return validationResult.Errors.Select(e => new Error(e.PropertyName, e.ErrorMessage));
        }

        public virtual void OnModelErrorChanged(bool IsValid)
        {
            OnModelValidationChanged?.Invoke(this, IsValid);
        }
        public virtual void OnPropertyErrorChange(bool IsValid, string propertyName)
        {
            OnModelPropertyValidationChanged?.Invoke(this, IsValid, propertyName);
        }

    }
}
