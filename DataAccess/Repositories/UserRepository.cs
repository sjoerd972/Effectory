using Data.Model.People;
using Data.Repositories;
using DataAccess.Base;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
	public class UserRepository : IUserRepository
	{
		public User Get(int id)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<User> users = db.GetCollection<User>("User").FindAll().ToList();
				return users.FirstOrDefault(q => q.Id == id);
			}
		}

		public IList<User> GetAll()
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<User> users = db.GetCollection<User>("User").FindAll().ToList();
				return users;
			}
		}

		public User GetByEmailAddress(string emailAddress)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<User> users = db.GetCollection<User>("User").FindAll().ToList();
				return users.FirstOrDefault(q => q.Email == emailAddress);
			}
		}
	}
}
