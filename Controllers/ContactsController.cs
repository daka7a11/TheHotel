using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.EmailSender;
using TheHotel.EmailSender.ViewRender;
using TheHotel.Mapping;
using TheHotel.Services.Contacts;
using TheHotel.ViewModels;
using TheHotel.ViewModels.Contacts;

namespace TheHotel.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class ContactsController : Controller
    {
        private readonly IContactsService contactsService;
        private readonly IViewRenderService renderService;
        private readonly IMailService mailService;

        public ContactsController(IContactsService contactsService,
           IViewRenderService renderService,
           IMailService mailService)
        {
            this.contactsService = contactsService;
            this.renderService = renderService;
            this.mailService = mailService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(QuestionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var question = AutoMapperConfig.MapperInstance.Map<Question>(model);
            question.CreatedOn = DateTime.UtcNow;

            await contactsService.AddQuestionAsync(question);

            TempData.Add("QuestionSuccessfully",GlobalConstants.SuccessfullySubmittedQuestion);

            return Redirect("/");
        }

        public IActionResult AllQuestions()
        {
            var questions = contactsService.GetAllQuestions<AllQuestionsViewModel>();

            return View(questions);
        }

        public IActionResult QuestionDetails(int questionId)
        {
            Question question = contactsService.GetById(questionId);

            if (question == null)
            {
                return Redirect("/Contacts/AllQuestions");
            }

            var model = AutoMapperConfig.MapperInstance.Map<QuestionDetailsViewModel>(question);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> QuestionDetails(int questionId, string message)
        {
            Question question = contactsService.GetById(questionId);

            if (question == null)
            {
                return Redirect("/Contacts/AllQuestions");
            }

            var responseModel = AutoMapperConfig.MapperInstance.Map<MailQuestionResponseViewModel>(question);
            responseModel.Response = message;

            var mailBody = await renderService.RenderToStringAsync("MailQuestionResponse", responseModel);

            MailRequest mailRequest = new MailRequest()
            {
                ToEmail = question.Email,
                Subject = $"RE: {question.Title}",
                Body = mailBody,
            };

            await mailService.SendEmailAsync(mailRequest);

            await contactsService.DeleteAsync(questionId);

            return Redirect("/Contacts/AllQuestions");
        }

        public async Task<IActionResult> Delete(int questionId)
        {
            await contactsService.DeleteAsync(questionId);

            return Redirect("/Contacts/AllQuestions");
        }

        public IActionResult AnsweredQuestions()
        {
            var answeredQuestions = contactsService.GetAnsweredQuestions<AnsweredQuestionViewModel>();

            return View(answeredQuestions);
        }
    }
}
