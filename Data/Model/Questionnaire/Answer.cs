using Data.Model.People;
using System;
using System.Collections.Generic;

namespace Data.Model.Questionnaire
{
	public class Answer
	{
		public int Id { get; set; }
		public string ParticipantEmail { get; set; }
		public Participant Participant { get; set; }
		public int QuestionId { get; set; }
		public Question Question { get; set; }
		public dynamic Value { get; set; }
		public DateTime DateAnswered { get; set; }
	}
}
