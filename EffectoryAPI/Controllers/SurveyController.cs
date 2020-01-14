using Data.Model.Questionnaire;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EffectoryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurveyController : ControllerBase
    {
        private ISurveyRepository _surveyRepository;
        private IQuestionRepository _questionRepository;
        private IAnswerOptionRepository _answerOptionRepository;
        private IAnswerRepository _answerRepository;
        private IUserRepository _userRepository;
        private ISubjectRepository _subjectRepository;

        public SurveyController(ISurveyRepository surveyRepository, ISubjectRepository subjectRepository, IQuestionRepository questionRepository, IAnswerOptionRepository answerOptionRepository, IAnswerRepository answerRepository, IUserRepository userRepository)
        {
            _surveyRepository = surveyRepository;
            _subjectRepository = subjectRepository;
            _questionRepository = questionRepository;
            _answerOptionRepository = answerOptionRepository;
            _answerRepository = answerRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            //get all surveys
            IList<Survey> surveys = _surveyRepository.GetAll();

            //parse to JSON
            return Ok(surveys);
        }

        [HttpGet("{surveyId}")]
        public ActionResult Survey(int surveyId)
        {
            //get the survey, the questions
            Survey survey = _surveyRepository.Get(surveyId);
            //get the answers (if an email address has been given)
            IList<Subject> subjects = _subjectRepository.GetBySurveyId(surveyId);
            survey.Subjects = subjects;

            foreach(Subject subject in subjects)
            {
                IList<Question> questions = _questionRepository.GetBySubjectId(subject.Id);
                subject.Questions = questions;

                foreach(Question question in questions)
                {
                    IList<AnswerOption> options = _answerOptionRepository.GetByQuestionId(question.Id);
                    question.AnswerOptions = options;
                }
            }

            return Ok(survey);
        }
    }
}