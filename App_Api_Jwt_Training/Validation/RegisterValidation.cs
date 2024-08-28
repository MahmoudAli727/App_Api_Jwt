using FluentValidation;

namespace App_Api_Jwt_Training.Validation
{
	public class RegisterValidation : AbstractValidator<NewRegister>
	{
		public RegisterValidation() 
		{ 
			RuleFor(user=>user.Name).NotNull().NotEmpty().Length(2,255).Must(isValidName).When(user=>user.Name!=null);
			RuleFor(user=>user.Email).NotNull().NotEmpty().EmailAddress();
			RuleFor(user => user.Password).NotNull().NotEmpty().MinimumLength(5).MaximumLength(30);
			//	.Must((user, pass) => user.PasswoedConfirmed==pass);
			RuleFor(user=>user.PasswoedConfirmed).NotNull().NotEmpty().MinimumLength(5).MaximumLength(30)
				.Must((user, ConPass) => ConPass==user.Password).WithMessage("ConfirmedPassword is not equal Password");
			RuleFor(user => user.PhoneNumber).NotNull().NotEmpty().Must(isValidPhone).When(uPhone => uPhone != null);

		}

		private bool isValidPhone(string arg)
		{
			return arg.All(char.IsDigit);
		}

		private bool isValidName(string arg)
		{
			return arg.All(char.IsLetter);
        }
	}
}
