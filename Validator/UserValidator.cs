using Common;
using FluentValidation;

namespace API.Validator
{
	public class UserValidator : AbstractValidator<UserModel>
	{
		public UserValidator()
		{
			RuleFor(user => user.Username).NotNull().WithMessage("Nome de usuário não pode ser vazio");
			RuleFor(user => user.Person.Email).NotNull().WithMessage("O email não pode ser vazio").EmailAddress().WithMessage("Email inválido");
			RuleFor(user => user.Password).NotNull().WithMessage("A senha não pode ser vazia");
			RuleFor(user => user.ConfirmPassword).NotNull().WithMessage("A senha não pode ser vazia");
			RuleFor(user => user.Password).Equal(o => o.ConfirmPassword).WithMessage("As senhas são diferentes");
		}
	}
}
