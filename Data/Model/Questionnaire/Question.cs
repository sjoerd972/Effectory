using Data.Model.Enums;
using System;
using System.Collections.Generic;

namespace Data.Model.Questionnaire
{
	public class Question
	{
		public int Id { get; set; }
		public Dictionary<string, string> Text { get; set; }
		public Subject Subject { get; set; }
		public int SubjectId { get; set; }
		public AnswerType AnswerType { get; set; }
		public int DisplayOrder { get; set; }
		public IList<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
	}
}
