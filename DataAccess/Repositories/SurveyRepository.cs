using Data.Model.Questionnaire;
using Data.Repositories;
using DataAccess.Base;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
	public class SurveyRepository : ISurveyRepository
	{
		public Survey Get(int id)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Survey> surveys = db.GetCollection<Survey>("Survey").FindAll().ToList();
				return surveys.FirstOrDefault(q => q.Id == id);
			}
		}

		public IList<Survey> GetAll()
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Survey> surveys = db.GetCollection<Survey>("Survey").FindAll().ToList();
				return surveys;
			}
		}
	}
}
