using System.Collections.Generic;

namespace Data.Model.Questionnaire
{
	public class AnswerOption
	{
		public int Id { get; set; }
		public Dictionary<string, string> Text { get; set; }
		public int DisplayOrder { get; set; }
		public int QuestionId { get; set; }
	}
}
