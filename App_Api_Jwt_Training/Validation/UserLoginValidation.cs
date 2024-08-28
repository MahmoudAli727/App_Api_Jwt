using FluentValidation;

namespace App_Api_Jwt_Training.Validation
{
	public class UserLoginValidation:AbstractValidator<NewLogin>
	{
        public UserLoginValidation()
        {
            RuleFor(user => user.Email).NotEmpty().NotNull().EmailAddress().When(user=>user.Email!=null);
            RuleFor(user => user.Password).NotNull().NotEmpty().MinimumLength(5);
        }
    }
}
