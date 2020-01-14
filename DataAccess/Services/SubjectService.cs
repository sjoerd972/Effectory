using Data.Model.Questionnaire;
using Data.Services;
using DataAccess.Base;
using LiteDB;

namespace DataAccess.Services
{
	public class SubjectService : ISubjectService
	{
		public void Delete(Subject o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var subjects = db.GetCollection<Subject>("Subject");

				subjects.Delete(o.Id);
				subjects.EnsureIndex("Id");
			}
		}

		public void Insert(Subject o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var subjects = db.GetCollection<Subject>("Subject");

				subjects.Insert(o);
				subjects.EnsureIndex("Id");
			}
		}

		public void Update(Subject o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var subjects = db.GetCollection<Subject>("Subject");

				subjects.Update(o);
				subjects.EnsureIndex("Id");
			}
		}

		public void Upsert(Subject o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var subjects = db.GetCollection<Subject>("Subject");

				subjects.Upsert(o);
				subjects.EnsureIndex("Id");
			}
		}
	}
}
