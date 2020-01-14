using Data.Model.Questionnaire;
using Data.Services;
using DataAccess.Base;
using LiteDB;

namespace DataAccess.Services
{
	public class SurveyService : ISurveyService
	{
		public void Delete(Survey o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var surveys = db.GetCollection<Survey>("Survey");

				surveys.Delete(o.Id);
				surveys.EnsureIndex("Id");
			}
		}

		public void Insert(Survey o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var surveys = db.GetCollection<Survey>("Survey");

				surveys.Insert(o);
				surveys.EnsureIndex("Id");
			}
		}

		public void Update(Survey o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var surveys = db.GetCollection<Survey>("Survey");

				surveys.Update(o);
				surveys.EnsureIndex("Id");
			}
		}

		public void Upsert(Survey o)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				var surveys = db.GetCollection<Survey>("Survey");

				surveys.Upsert(o);
				surveys.EnsureIndex("Id");
			}
		}
	}
}
