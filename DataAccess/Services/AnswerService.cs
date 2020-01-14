using Data.Model.Questionnaire;
using Data.Services;
using DataAccess.Base;
using LiteDB;

namespace DataAccess.Services
{
	public class AnswerService : IAnswerService
	{
		public void Delete(Answer o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var answers = db.GetCollection<Answer>("Answer");

				answers.Delete(o.Id);
				answers.EnsureIndex("Id");
			}
		}

		public void Insert(Answer o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var answers = db.GetCollection<Answer>("Answer");

				answers.Insert(o);
				answers.EnsureIndex("Id");
			}
		}

		public void Update(Answer o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var answers = db.GetCollection<Answer>("Answer");

				answers.Update(o);
				answers.EnsureIndex("Id");
			}
		}

		public void Upsert(Answer o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var answers = db.GetCollection<Answer>("Answer");

				answers.Upsert(o);
				answers.EnsureIndex("Id");
			}
		}
	}
}
