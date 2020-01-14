using Data.Model.Questionnaire;
using Data.Services;
using DataAccess.Base;
using LiteDB;

namespace DataAccess.Services
{
	public class QuestionService : IQuestionService
	{
		public void Delete(Question o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var questions = db.GetCollection<Question>("Question");

				questions.Delete(o.Id);
				questions.EnsureIndex("Id");
			}
		}

		public void Insert(Question o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var questions = db.GetCollection<Question>("Question");

				questions.Insert(o);
				questions.EnsureIndex("Id");
			}
		}

		public void Update(Question o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var questions = db.GetCollection<Question>("Question");

				questions.Update(o);
				questions.EnsureIndex("Id");
			}
		}

		public void Upsert(Question o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var questions = db.GetCollection<Question>("Question");
				questions.Upsert(o);
				questions.EnsureIndex("Id");
			}
		}
	}
}
