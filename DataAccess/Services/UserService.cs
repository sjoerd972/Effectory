using Data.Model.People;
using Data.Services;
using DataAccess.Base;
using LiteDB;

namespace DataAccess.Services
{
	public class UserService : IUserService
	{
		public void Delete(User o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var users = db.GetCollection<User>("User");

				users.Delete(o.Id);
				users.EnsureIndex("Id");
			}
		}

		public void Insert(User o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var users = db.GetCollection<User>("User");

				users.Insert(o);
				users.EnsureIndex("Id");
			}
		}

		public void Update(User o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var users = db.GetCollection<User>("User");

				users.Update(o);
				users.EnsureIndex("Id");
			}
		}

		public void Upsert(User o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var users = db.GetCollection<User>("User");

				users.Upsert(o);
				users.EnsureIndex("Id");
			}
		}
	}
}
