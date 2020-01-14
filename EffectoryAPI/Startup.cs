using Data.Repositories;
using Data.Services;
using DataAccess.Repositories;
using DataAccess.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EffectoryAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services
				.AddSingleton<ISurveyService, SurveyService>()
				.AddSingleton<ISubjectService, SubjectService>()
				.AddSingleton<IQuestionService, QuestionService>()
				.AddSingleton<IAnswerService, AnswerService>()
				.AddSingleton<IAnswerOptionService, AnswerOptionService>()
				.AddSingleton<IUserService, UserService>()
				.AddSingleton<ISurveyRepository, SurveyRepository>()
				.AddSingleton<ISubjectRepository, SubjectRepository>()
				.AddSingleton<IAnswerRepository, AnswerRepository>()
				.AddSingleton<IAnswerOptionRepository, AnswerOptionRepository>()
				.AddSingleton<IQuestionRepository>(q => new QuestionRepository(q.GetRequiredService<IAnswerOptionRepository>(), q.GetRequiredService<IAnswerRepository>(), q.GetRequiredService<IUserRepository>()))
				.AddSingleton<IUserRepository, UserRepository>();

			services.AddCors(options => options.AddPolicy("all", builder => builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader()));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors("all");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
