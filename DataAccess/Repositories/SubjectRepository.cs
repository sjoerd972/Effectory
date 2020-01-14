using Data.Model.Questionnaire;
using Data.Repositories;
using DataAccess.Base;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
	public class SubjectRepository : ISubjectRepository
	{
		public Subject Get(int id)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Subject> Subjects = db.GetCollection<Subject>("Subject").FindAll().ToList();
				return Subjects.FirstOrDefault(q => q.Id == id);
			}
		}

		public IList<Subject> GetAll()
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Subject> Subjects = db.GetCollection<Subject>("Subject").FindAll().ToList();
				return Subjects;
			}
		}

		public IList<Subject> GetBySurveyId(int surveyId)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Subject> subjects = db.GetCollection<Subject>("Subject").FindAll().Where(s => s.SurveyId == surveyId).ToList();
				return subjects;
			}
		}
	}
}
