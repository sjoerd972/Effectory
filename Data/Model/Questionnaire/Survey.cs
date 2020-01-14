using System;
using System.Collections.Generic;

namespace Data.Model.Questionnaire
{
	public class Survey
	{
		public int Id { get; set; }
		public Dictionary<string, string> Title { get; set; }
		public Dictionary<string, string> Introduction { get; set; }
		public DateTime DateCreated { get; set; }
		public IList<Subject> Subjects { get; set; } = new List<Subject>();
	}
}
