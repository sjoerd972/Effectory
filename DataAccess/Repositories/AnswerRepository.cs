using Data.Model.Questionnaire;
using Data.Repositories;
using DataAccess.Base;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
	public class AnswerRepository : IAnswerRepository
	{
		public Answer Get(int id)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Answer> answers = db.GetCollection<Answer>("Answer").FindAll().ToList();
				return answers.FirstOrDefault(q => q.Id == id);
			}
		}

		public IList<Answer> GetAll()
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Answer> answers = db.GetCollection<Answer>("Answer").FindAll().ToList();
				return answers;
			}
		}

		public Answer GetByQuestionIdAndEmailAddress(int questionId, string emailAddress)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Answer> answers = db.GetCollection<Answer>("Answer").FindAll().ToList();
				return answers.FirstOrDefault(a => a.QuestionId == questionId && a.ParticipantEmail == emailAddress);
			}
		}

		public IList<Answer> GetBySurveyIdAndEmailAddress(int surveyId, string emailAddress)
		{
			throw new NotImplementedException();
		}
	}
}
