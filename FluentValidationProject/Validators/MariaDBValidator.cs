using FluentValidation;
using FluentValidationProject.Models;
using FluentValidationProject.ViewModels;

namespace FluentValidationProject.Validators
{
    public class MariaDBValidator : AbstractValidator<MainWindowViewModel>
    {
        public MariaDBValidator()
        {
            // A Dataset's name should not be null or empty.
            RuleFor(dataSet => dataSet.DSName)
               .NotNull()
               .Length(5,10)
               .NotEmpty()
               .WithMessage("Invalid Name");

            // A Dataset's description should not be null or empty.
            RuleFor(dataSet => dataSet.Description)
               .NotNull()
               .NotEmpty()
               .WithMessage("Invalid Description");

            // A Dataset's IP address should match the regex and not be null or empty
            // Regex matches valid IPv4 addresses or localhost
            RuleFor(dataSet => dataSet.IPAddress)
                .Matches(@"^(((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.|$)){4})$|^localhost$")
                .WithMessage("Invalid IP Address");

            // A Dataset's password should not be null or empty.
            RuleFor(dataSet => dataSet.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid Password");

            // A Dataset's username should not be null or empty.
            RuleFor(dataSet => dataSet.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid Username");
        }
    }
}
