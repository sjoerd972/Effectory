using Data.Model.Questionnaire;
using Data.Services;
using DataAccess.Base;
using LiteDB;

namespace DataAccess.Services
{
	public class AnswerOptionService : IAnswerOptionService
	{
		public void Delete(AnswerOption o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var answerOptions = db.GetCollection<AnswerOption>("AnswerOption");

				answerOptions.Delete(o.Id);
				answerOptions.EnsureIndex("Id");
			}
		}

		public void Insert(AnswerOption o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var answerOptions = db.GetCollection<AnswerOption>("AnswerOption");

				answerOptions.Insert(o);
				answerOptions.EnsureIndex("Id");
			}
		}

		public void Update(AnswerOption o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var answerOptions = db.GetCollection<AnswerOption>("AnswerOption");

				answerOptions.Update(o);
				answerOptions.EnsureIndex("Id");
			}
		}

		public void Upsert(AnswerOption o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var answerOptions = db.GetCollection<AnswerOption>("AnswerOption");

				answerOptions.Upsert(o);
				answerOptions.EnsureIndex("Id");
			}
		}
	}
}
