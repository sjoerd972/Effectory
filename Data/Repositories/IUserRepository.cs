using Data.Model.People;

namespace Data.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		User GetByEmailAddress(string emailAddress);
	}
}
