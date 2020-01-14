using Data.Model.Questionnaire;
using Data.Repositories;
using DataAccess.Base;
using LiteDB;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
	public class QuestionRepository : IQuestionRepository
	{
		public IAnswerOptionRepository _answerOptionRepository;
		public IAnswerRepository _answerRepository;
		public IUserRepository _userRepository;

		public QuestionRepository(IAnswerOptionRepository answerOptionRepository, IAnswerRepository answerRepository, IUserRepository userRepository)
		{
			_answerOptionRepository = answerOptionRepository;
			_answerRepository = answerRepository;
			_userRepository = userRepository;
		}

		public Question Get(int id)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Question> questions = db.GetCollection<Question>("Question").FindAll().ToList();
				return questions.FirstOrDefault(q => q.Id == id);
			}
		}

		public IList<Question> GetAll()
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Question> questions = db.GetCollection<Question>("Question").FindAll().ToList();
				return questions;
			}
		}

		public IList<Question> GetBySubjectId(int subjectId)
		{
			using (var db = new LiteDatabase(Constants.DB_NAME))
			{
				IList<Question> questions = db.GetCollection<Question>("Question").FindAll().Where(q => q.SubjectId == subjectId).ToList();
				return questions;
			}
		}
	}
}
