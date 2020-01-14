using Data.Model.Questionnaire;
using System.Collections.Generic;

namespace Data.Repositories
{
	public interface IAnswerOptionRepository : IRepository<AnswerOption>
	{
		IList<AnswerOption> GetByQuestionId(int id);
	}
}
