using Common;
using Repository;
using Repository.Entity;

namespace API.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository) { 
			_userRepository = userRepository;
		}

		public void AddUser(UserModel model)
		{
			UserEntity entity = new UserEntity()
			{
				PersonId = (int)model.PersonId,
				Password = model.Password,
				UserName = model.Username
			};
			_userRepository.Add(entity);
		}

		public void UpdateUser(UserModel model)
		{
			UserEntity entity = new UserEntity()
			{
				Id = (int)model.Id,
				PersonId = (int)model.PersonId,
				Password = model.Password,
				UserName = model.Username
			};
			_userRepository.Update(entity);
		}

		public UserModel Login(UserModel model)
		{
			UserEntity entity = new UserEntity()
			{
				Person = new PersonEntity()
				{
					Email = model.Person.Email,
				},
				UserName = model.Username,
				Password = model.Password,
			};
			
			entity = _userRepository.Login(entity);
			if(entity != null)
			{
				model.Id = entity.Id;
				model.PersonId = entity.PersonId;
				model.Person.Email = entity.Person.Email;
				model.Username = entity.UserName;
				return model;
			}
			return null;
		}
	}
}
