using Data.Model.Questionnaire;
using System.Collections.Generic;

namespace Data.Model.People
{
	public class Participant : User
	{
		public IList<Survey> Surveys { get; set; }
	}
}
