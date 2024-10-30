using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common;
using API.Validator;
using FluentValidation.Results;
using API.Services;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IPersonService _personService;
		private readonly IUserService _userService;
		public UserController(IPersonService personService, IUserService userService) {
			_personService = personService;
			_userService = userService;
		}
		/// <summary>
		/// Autentica o usuário
		/// </summary>
		/// <param name="user">Nome e senha do usuário</param>
		/// <returns>Ok</returns>
		[HttpPost]
		public IActionResult Login(UserModel user)
		{
			var result = _userService.Login(user);
			if(result != null)
			{
				return Ok(new 
				{
					UserId = result.Id,
					PersonId = result.PersonId,
					Email = result.Person.Email,
					Username = result.Username
				});
			}
			else
			{
				return Ok(new {response = "Error"});
			}
		}

		/// <summary>
		/// API para criar o usuário
		/// </summary>
		/// <param name="user">Nome e senha do usuário</param>
		/// <returns>Ok</returns>
		[HttpPost("create")]
		public IActionResult Create(UserModel user)
		{
			UserValidator validator = new UserValidator();
			ValidationResult results = validator.Validate(user);
			if (!results.IsValid)
			{
				foreach (var failure in results.Errors)
				{
					Console.WriteLine("Property" + failure.PropertyName + "failure validation. Error was:" + failure.ErrorMessage);
				}
				return Ok(new {response = "error"});
			}
			
			
			var personId = _personService.AddPerson(new PersonModel()
			{
				Email = user.Person.Email,
				Name = user.Person.Name
			});
			_userService.AddUser(new UserModel()
			{
				PersonId = personId,
				Password = user.Password,
				Username = user.Username
			});
			return Ok(new {response = "Ok"});
			
		}

		/// <summary>
		/// API para edição de usuário
		/// </summary>
		/// <param name="user">Modelo de usuário</param>
		/// <returns>Ok se tudo certo</returns>
		[HttpPatch("update")]
		public IActionResult Update(UserModel user) {
			_userService.UpdateUser(user);
			_personService.UpdatePerson(user.Person);
			return Ok(new { response = "Ok" });
		}

		/// <summary>
		/// API para resetar a senha
		/// </summary>
		/// <param name="email">Email do usuário</param>
		/// <returns>Ok</returns>
		[HttpPost("forgot")]

		//[FromBody] é pra quando o parâmetro não for um objeto, como uma string por exemplo
		public IActionResult Forgot([FromBody] string email)
		{
			return Ok(new { response = "Ok"});	
		}
		
		/// <summary>
		/// API para resetar a senha
		/// </summary>
		/// <param name="password">Senha do usuário</param>
		/// <param name="confirmpassword">Confirmar senha do usuário</param>
		/// <returns>Ok</returns>
		[HttpPost("reset")]
		public IActionResult Reset(UserModel user)
		{
			return Ok(new { response = "Ok"});	
		}
	}
}
