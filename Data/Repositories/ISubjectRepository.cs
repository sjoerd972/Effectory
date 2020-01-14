using Data.Model.Questionnaire;
using System.Collections.Generic;

namespace Data.Repositories
{
	public interface ISubjectRepository : IRepository<Subject>
	{
		IList<Subject> GetBySurveyId(int surveyId);
	}
}
