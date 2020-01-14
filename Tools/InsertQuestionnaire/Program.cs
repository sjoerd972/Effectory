using Data.Model.Enums;
using Data.Model.Questionnaire;
using Data.Repositories;
using Data.Services;
using DataAccess.Repositories;
using DataAccess.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace InsertQuestionnaire
{
	class Program
	{
		private static ServiceProvider _serviceCollection;

		static void Main(string[] args)
		{
			//load dependencies
			_serviceCollection = new ServiceCollection()
				.AddSingleton<ISurveyService, SurveyService>()
				.AddSingleton<ISubjectService, SubjectService>()
				.AddSingleton<IQuestionService, QuestionService>()
				.AddSingleton<IAnswerOptionService, AnswerOptionService>()
				.AddSingleton<ISurveyRepository, SurveyRepository>()
				.AddSingleton<ISubjectRepository, SubjectRepository>()
				.AddSingleton<IQuestionRepository, QuestionRepository>()
				.AddSingleton<IAnswerOptionRepository, AnswerOptionRepository>()
				.BuildServiceProvider();

			//var a = _serviceCollection.GetService<ISurveyRepository>().GetAll();
			var b = _serviceCollection.GetService<ISurveyRepository>().Get(1000);

			//var c = _serviceCollection.GetService<ISubjectRepository>().GetAll();
			var d = _serviceCollection.GetService<ISubjectRepository>().Get(2605515);

			//start importing the json file
			string path = Directory.GetCurrentDirectory();

			if (!File.Exists(path + "\\questionnaire.json"))
				return;

			using (StreamReader file = File.OpenText(path + "\\questionnaire.json"))
			using (JsonTextReader reader = new JsonTextReader(file))
			{
				JObject questionnaireJson = (JObject)JToken.ReadFrom(reader);

				CreateSurvey(questionnaireJson);
			}
		}

		private static void CreateSurvey(JObject questionnaireJson)
		{
			if (!int.TryParse(questionnaireJson["questionnaireId"].ToString(), out int id))
				return;

			Survey survey = new Survey
			{
				Id = id
			};

			_serviceCollection.GetService<ISurveyService>().Upsert(survey);

			if (questionnaireJson["questionnaireItems"] != null)
			{
				foreach (JObject item in questionnaireJson["questionnaireItems"])
				{
					survey.Subjects.Add(CreateSubject(item, survey));
				}
			}
		}

		private static Subject CreateSubject(JObject subjectJson, Survey survey)
		{
			if(!int.TryParse(subjectJson["subjectId"].ToString(), out int id))
				return null;

			int.TryParse(subjectJson["orderNumber"].ToString(), out int order);

			Subject subject = new Subject
			{
				Id = id,
				SurveyId = survey.Id,
				Text = Util.Json.GetDictionaryFromText((JObject)subjectJson["texts"]),
				DisplayOrder = order
			};

			_serviceCollection.GetService<ISubjectService>().Upsert(subject);

			if (subjectJson["questionnaireItems"] != null)
			{
				foreach (JObject item in subjectJson["questionnaireItems"])
				{
					subject.Questions.Add(CreateQuestion(item, subject));
				}
			}

			return subject;
		}

		private static Question CreateQuestion(JObject questionJson, Subject subject)
		{
			if (!int.TryParse(questionJson["questionId"].ToString(), out int id))
				return null;

			int.TryParse(questionJson["orderNumber"].ToString(), out int order);
			int.TryParse(questionJson["answerCategoryType"].ToString(), out int answerType);

			Question question = new Question
			{
				Id = id,
				SubjectId = subject.Id,
				Text = Util.Json.GetDictionaryFromText((JObject)questionJson["texts"]),
				DisplayOrder = order,
				AnswerType = (AnswerType)answerType
			};
			
			_serviceCollection.GetService<IQuestionService>().Upsert(question);

			if (questionJson["questionnaireItems"] != null && (AnswerType)answerType == AnswerType.MultipleChoice)
			{
				foreach (JObject item in questionJson["questionnaireItems"])
				{
					question.AnswerOptions.Add(CreateAnswerOptions(item, question));
				}
			}

			return question;
		}

		private static AnswerOption CreateAnswerOptions(JObject answerJson, Question question)
		{
			if (!int.TryParse(answerJson["answerId"].ToString(), out int id))
				return null;

			int.TryParse(answerJson["orderNumber"].ToString(), out int order);

			AnswerOption option = new AnswerOption
			{
				Id = id,
				Text = Util.Json.GetDictionaryFromText((JObject)answerJson["texts"]),
				DisplayOrder = order
			};

			_serviceCollection.GetService<IAnswerOptionService>().Upsert(option); 
			
			return option;
		}

		/*{
"questionnaireId": 1000,
"questionnaireItems": [
{
	"subjectId": 2605515,
	"orderNumber": 0,
	"texts": {
		"nl-NL": "Mijn werk",
		"en-US": "My work"
	},
	"itemType": 0,
	"questionnaireItems": [
		{
			"questionId": 3807638,
			"subjectId": 2605515,
			"answerCategoryType": 0,
			"orderNumber": 0,
			"texts": {
				"nl-NL": "Ik ben blij met mijn werk",
				"en-US": "I am happy with my work"
			},
			"itemType": 1,
			"questionnaireItems": [
				{
					"answerId": 17969124,
					"questionId": 3807638,
					"answerType": 1,
					"orderNumber": 0,
					"texts": {
						"nl-NL": "Helemaal mee oneens",
						"en-US": "Strongly disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969123,
					"questionId": 3807638,
					"answerType": 1,
					"orderNumber": 1,
					"texts": {
						"nl-NL": "Mee oneens",
						"en-US": "Disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969122,
					"questionId": 3807638,
					"answerType": 1,
					"orderNumber": 2,
					"texts": {
						"nl-NL": "Niet mee eens/ niet mee oneens",
						"en-US": "Neither agree nor disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969121,
					"questionId": 3807638,
					"answerType": 1,
					"orderNumber": 3,
					"texts": {
						"nl-NL": "Mee eens",
						"en-US": "Agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969120,
					"questionId": 3807638,
					"answerType": 1,
					"orderNumber": 4,
					"texts": {
						"nl-NL": "Helemaal mee eens",
						"en-US": "Strongly agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				}
			]
		},
		{
			"questionId": 3807643,
			"subjectId": 2605515,
			"answerCategoryType": 0,
			"orderNumber": 1,
			"texts": {
				"en-US": "I enjoy doing my work",
				"nl-NL": "Ik doe mijn werk met plezier"
			},
			"itemType": 1,
			"questionnaireItems": [
				{
					"answerId": 17969149,
					"questionId": 3807643,
					"answerType": 1,
					"orderNumber": 0,
					"texts": {
						"nl-NL": "Helemaal mee oneens",
						"en-US": "Strongly disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969148,
					"questionId": 3807643,
					"answerType": 1,
					"orderNumber": 1,
					"texts": {
						"nl-NL": "Mee oneens",
						"en-US": "Disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969147,
					"questionId": 3807643,
					"answerType": 1,
					"orderNumber": 2,
					"texts": {
						"nl-NL": "Niet mee eens/ niet mee oneens",
						"en-US": "Neither agree nor disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969146,
					"questionId": 3807643,
					"answerType": 1,
					"orderNumber": 3,
					"texts": {
						"nl-NL": "Mee eens",
						"en-US": "Agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969145,
					"questionId": 3807643,
					"answerType": 1,
					"orderNumber": 4,
					"texts": {
						"nl-NL": "Helemaal mee eens",
						"en-US": "Strongly agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				}
			]
		},
		{
			"questionId": 3851855,
			"subjectId": 2605515,
			"answerCategoryType": 0,
			"orderNumber": 2,
			"texts": {
				"nl-NL": "Ik vind mijn werk uitdagend",
				"en-US": "My work is enjoyably challenging"
			},
			"itemType": 1,
			"questionnaireItems": [
				{
					"answerId": 18166389,
					"questionId": 3851855,
					"answerType": 1,
					"orderNumber": 0,
					"texts": {
						"nl-NL": "Helemaal mee oneens",
						"en-US": "Strongly disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166388,
					"questionId": 3851855,
					"answerType": 1,
					"orderNumber": 1,
					"texts": {
						"nl-NL": "Mee oneens",
						"en-US": "Disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166387,
					"questionId": 3851855,
					"answerType": 1,
					"orderNumber": 2,
					"texts": {
						"nl-NL": "Niet mee eens/ niet mee oneens",
						"en-US": "Neither agree nor disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166386,
					"questionId": 3851855,
					"answerType": 1,
					"orderNumber": 3,
					"texts": {
						"nl-NL": "Mee eens",
						"en-US": "Agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166385,
					"questionId": 3851855,
					"answerType": 1,
					"orderNumber": 4,
					"texts": {
						"nl-NL": "Helemaal mee eens",
						"en-US": "Strongly agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				}
			]
		},
		{
			"questionId": 3807701,
			"subjectId": 2605515,
			"answerCategoryType": 0,
			"orderNumber": 3,
			"texts": {
				"nl-NL": "Ik heb voldoende verantwoordelijkheid in mijn werk",
				"en-US": "I have sufficient responsibilities in my work"
			},
			"itemType": 1,
			"questionnaireItems": [
				{
					"answerId": 17969415,
					"questionId": 3807701,
					"answerType": 1,
					"orderNumber": 0,
					"texts": {
						"nl-NL": "Helemaal mee oneens",
						"en-US": "Strongly disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969414,
					"questionId": 3807701,
					"answerType": 1,
					"orderNumber": 1,
					"texts": {
						"nl-NL": "Mee oneens",
						"en-US": "Disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969413,
					"questionId": 3807701,
					"answerType": 1,
					"orderNumber": 2,
					"texts": {
						"nl-NL": "Niet mee eens/ niet mee oneens",
						"en-US": "Neither agree nor disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969412,
					"questionId": 3807701,
					"answerType": 1,
					"orderNumber": 3,
					"texts": {
						"nl-NL": "Mee eens",
						"en-US": "Agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969411,
					"questionId": 3807701,
					"answerType": 1,
					"orderNumber": 4,
					"texts": {
						"nl-NL": "Helemaal mee eens",
						"en-US": "Strongly agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				}
			]
		},
		{
			"questionId": 3807644,
			"subjectId": 2605515,
			"answerCategoryType": 0,
			"orderNumber": 4,
			"texts": {
				"nl-NL": "Ik heb het gevoel dat ik met mijn werk een bijdrage lever",
				"en-US": "I feel I contribute meaningfully through my work"
			},
			"itemType": 1,
			"questionnaireItems": [
				{
					"answerId": 17969154,
					"questionId": 3807644,
					"answerType": 1,
					"orderNumber": 0,
					"texts": {
						"nl-NL": "Helemaal mee oneens",
						"en-US": "Strongly disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969153,
					"questionId": 3807644,
					"answerType": 1,
					"orderNumber": 1,
					"texts": {
						"nl-NL": "Mee oneens",
						"en-US": "Disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969152,
					"questionId": 3807644,
					"answerType": 1,
					"orderNumber": 2,
					"texts": {
						"nl-NL": "Niet mee eens/ niet mee oneens",
						"en-US": "Neither agree nor disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969151,
					"questionId": 3807644,
					"answerType": 1,
					"orderNumber": 3,
					"texts": {
						"nl-NL": "Mee eens",
						"en-US": "Agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 17969150,
					"questionId": 3807644,
					"answerType": 1,
					"orderNumber": 4,
					"texts": {
						"nl-NL": "Helemaal mee eens",
						"en-US": "Strongly agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				}
			]
		}
	]
},
{
	"subjectId": 2605516,
	"orderNumber": 1,
	"texts": {
		"nl-NL": "Mijn rol",
		"en-US": "My role"
	},
	"itemType": 0,
	"questionnaireItems": [
		{
			"questionId": 3851843,
			"subjectId": 2605516,
			"answerCategoryType": 0,
			"orderNumber": 0,
			"texts": {
				"nl-NL": "Ik weet hoe mijn werk bijdraagt aan de visie en doelen van Organisatie",
				"en-US": "It is clear to me how my work contributes to the organisation's strategy"
			},
			"itemType": 1,
			"questionnaireItems": [
				{
					"answerId": 18166291,
					"questionId": 3851843,
					"answerType": 1,
					"orderNumber": 0,
					"texts": {
						"nl-NL": "Helemaal mee oneens",
						"en-US": "Strongly disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166290,
					"questionId": 3851843,
					"answerType": 1,
					"orderNumber": 1,
					"texts": {
						"nl-NL": "Mee oneens",
						"en-US": "Disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166289,
					"questionId": 3851843,
					"answerType": 1,
					"orderNumber": 2,
					"texts": {
						"nl-NL": "Niet mee eens/ niet mee oneens",
						"en-US": "Neither agree nor disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166288,
					"questionId": 3851843,
					"answerType": 1,
					"orderNumber": 3,
					"texts": {
						"nl-NL": "Mee eens",
						"en-US": "Agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166287,
					"questionId": 3851843,
					"answerType": 1,
					"orderNumber": 4,
					"texts": {
						"nl-NL": "Helemaal mee eens",
						"en-US": "Strongly agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				}
			]
		},
		{
			"questionId": 3851856,
			"subjectId": 2605516,
			"answerCategoryType": 0,
			"orderNumber": 1,
			"texts": {
				"nl-NL": "Ik geef in de dagelijkse praktijk feedback aan de mensen met wie ik samenwerk",
				"en-US": "I give feedback to the people I work with in the daily practice of work"
			},
			"itemType": 1,
			"questionnaireItems": [
				{
					"answerId": 18166394,
					"questionId": 3851856,
					"answerType": 1,
					"orderNumber": 0,
					"texts": {
						"nl-NL": "Helemaal mee oneens",
						"en-US": "Strongly disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166393,
					"questionId": 3851856,
					"answerType": 1,
					"orderNumber": 1,
					"texts": {
						"nl-NL": "Mee oneens",
						"en-US": "Disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166392,
					"questionId": 3851856,
					"answerType": 1,
					"orderNumber": 2,
					"texts": {
						"nl-NL": "Niet mee eens/ niet mee oneens",
						"en-US": "Neither agree nor disagree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166391,
					"questionId": 3851856,
					"answerType": 1,
					"orderNumber": 3,
					"texts": {
						"nl-NL": "Mee eens",
						"en-US": "Agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				},
				{
					"answerId": 18166390,
					"questionId": 3851856,
					"answerType": 1,
					"orderNumber": 4,
					"texts": {
						"nl-NL": "Helemaal mee eens",
						"en-US": "Strongly agree"
					},
					"itemType": 2,
					"questionnaireItems": null
				}
			]
		},
		{
			"questionId": 3810105,
			"subjectId": 2605521,
			"answerCategoryType": 2,
			"orderNumber": 2,
			"texts": {
				"nl-NL": "Als je deze organisatie één tip mag geven, wat zou dat dan zijn?",
				"en-US": "If you could give this organisation one tip, what would it be?"
			},
			"itemType": 1,
			"questionnaireItems": [
				{
					"answerId": null,
					"questionId": 3810105,
					"answerType": 2,
					"orderNumber": 0,
					"texts": null,
					"itemType": 2,
					"questionnaireItems": null
				}
			]
		}
	]
}
]
}*/
	}
}
