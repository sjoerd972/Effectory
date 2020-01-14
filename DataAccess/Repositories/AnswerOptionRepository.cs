using Data.Model.Questionnaire;
using Data.Repositories;
using DataAccess.Base;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
	public class AnswerOptionRepository : IAnswerOptionRepository
	{
		public AnswerOption Get(int id)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<AnswerOption> AnswerOptions = db.GetCollection<AnswerOption>("AnswerOption").FindAll().ToList();
				return AnswerOptions.FirstOrDefault(q => q.Id == id);
			}
		}

		public IList<AnswerOption> GetAll()
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<AnswerOption> answerOptions = db.GetCollection<AnswerOption>("AnswerOption").FindAll().ToList();
				return answerOptions;
			}
		}

		public IList<AnswerOption> GetByQuestionId(int questionId)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<AnswerOption> answerOptions = db.GetCollection<AnswerOption>("AnswerOption").FindAll().Where(ao => ao.QuestionId == questionId).ToList();
				return answerOptions;
			}
		}
	}
}
