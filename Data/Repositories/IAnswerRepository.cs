using Data.Model.Questionnaire;
using System.Collections.Generic;

namespace Data.Repositories
{
	public interface IAnswerRepository : IRepository<Answer>
	{
		IList<Answer> GetBySurveyIdAndEmailAddress(int surveyId, string emailAddress);
		Answer GetByQuestionIdAndEmailAddress(int questionId, string emailAddress);
	}
}
