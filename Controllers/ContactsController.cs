using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(QuestionViewModel model)
        {
            //Add question to db.

            return View();
        }
    }
}
