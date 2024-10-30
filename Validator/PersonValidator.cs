using Common;
using FluentValidation;

namespace API.Validator
{
	public class PersonValidator : AbstractValidator<PersonModel>
	{
		public PersonValidator() 
		{
			RuleFor(person => person.Name).NotNull().WithMessage("O nome não pode ser vazio");
			RuleFor(person => person.Email).NotNull().WithMessage("O email não pode ser vazio").EmailAddress().WithMessage("Email inválido");
		}
	}
}
