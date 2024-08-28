using FluentValidation;

namespace App_Api_Jwt_Training.Validation
{
	public class StudentDtoValidation : AbstractValidator<StudentDto>
	{
        public StudentDtoValidation()
        {
            RuleFor(std=>std.Name).NotEmpty().NotNull().Length(2,255).Must(isValidName).When(user => user.Name != null);
            RuleFor(std=>std.Age).NotEmpty().NotNull().Must(isValidAge);
			RuleFor(std => std.DeptId).NotEmpty().NotNull().Must(isValidId);
        }

		private bool isValidId(int id)
		{
			return id>0 ;
		}

		private bool isValidAge(int age)
		{
			return age>0 && age<=100;
		}

		private bool isValidName(string arg)
		{
			return arg.All(char.IsLetter);
		}
	}
}
