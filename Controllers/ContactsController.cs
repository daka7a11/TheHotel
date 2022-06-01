using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data;
using TheHotel.Data.Models;
using TheHotel.Mapping;
using TheHotel.Services.Contacts;
using TheHotel.ViewModels.Contacts;

namespace TheHotel.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsService contactsService;

        public ContactsController(IContactsService contactsService)
        {
            this.contactsService = contactsService;
        }

        public IActionResult Index()
        {
            return View();
        }

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
    }
}
