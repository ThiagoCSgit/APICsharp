using API.Services;
using API.Validator;
using Common;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		private readonly IPersonService _personService;

		public PersonController(IPersonService personService)
		{
			_personService = personService;
		}

		[HttpPost("create")]
		public IActionResult Create(PersonModel person)
		{
			PersonValidator validator = new PersonValidator();
			ValidationResult results = validator.Validate(person);

			if (!results.IsValid)
			{
				foreach (var failure in results.Errors)
				{
					Console.WriteLine("Property" + failure.PropertyName + "failure validation. Error was:" + failure.ErrorMessage);
				}
				return Ok(new { response = "error" });
			}

			var personId = _personService.AddPerson(new PersonModel()
			{
				Email = person.Email,
				Name = person.Name,
				Type = person.Type,
			});

			return Ok(new { response = "Ok" });
		}

		[HttpPatch("update")]
		public IActionResult Update(PersonModel person)
		{
			_personService.UpdatePerson(person);
			return Ok(new { response = "Ok" });
		}

		[HttpGet()]
		public IActionResult Get(int id)
		{
			var person = _personService.GetPerson(id);
			return Ok(person);
		}

		[HttpDelete()]
		public IActionResult Delete(int id)
		{
			_personService.DeletePerson(id);
			return Ok(new {response = "Ok"});
		}
	}
}
