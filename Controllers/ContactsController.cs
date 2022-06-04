using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;
using TheHotel.Services.Contacts;
using TheHotel.ViewModels.Contacts;

namespace TheHotel.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class ContactsController : Controller
    {
        private readonly IContactsService contactsService;

        public ContactsController(IContactsService contactsService)
        {
            this.contactsService = contactsService;
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
        public IActionResult QuestionDetails(int questionId,string title, string message)
        {
            Question question = contactsService.GetById(questionId);

            if (question == null)
            {
                return Redirect("/Contacts/AllQuestions");
            }

            var model = AutoMapperConfig.MapperInstance.Map<QuestionDetailsViewModel>(question);

            return View(model);
        }
    }
}
