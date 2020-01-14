using System.Collections.Generic;

namespace Data.Model.Questionnaire
{
	public class Subject
	{
		public int Id { get; set; }
		public int SurveyId { get; set; }
		public Dictionary<string, string> Text { get; set; }
		public int DisplayOrder { get; set; }
		public IList<Question> Questions { get; set; } = new List<Question>();
	}
}
