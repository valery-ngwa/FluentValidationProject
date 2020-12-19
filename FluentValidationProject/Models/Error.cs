using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidationProject.Models
{
    public class Error
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }

        public Error(string property, string message)
        {
            this.PropertyName = property;
            this.ErrorMessage = message;
        }
    }
}
