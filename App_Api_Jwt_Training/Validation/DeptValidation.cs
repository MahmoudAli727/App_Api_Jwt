using FluentValidation;

namespace App_Api_Jwt_Training.Validation
{
	public class DeptValidation : AbstractValidator<DepartmentDto> 
	{
		public DeptValidation() 
		{
			RuleFor(dept=>dept.Name).NotEmpty().NotNull().MinimumLength(2).MaximumLength(20);
		}
	}
}
