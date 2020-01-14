using Data.Model.Questionnaire;
using System.Collections.Generic;

namespace Data.Repositories
{
	public interface IQuestionRepository : IRepository<Question>
	{
		IList<Question> GetBySubjectId(int id);
	}
}
