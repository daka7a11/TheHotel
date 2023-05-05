using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Mapping;
using TheHotel.Services.Offers;
using TheHotel.ViewModels.Offers;

namespace TheHotel.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class OffersController : Controller
    {
        private readonly IOffersService offersService;

        public OffersController(IOffersService offersService)
        {
            this.offersService = offersService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var offers = offersService.GetAll<AllOffersViewModel>();

            return View(offers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await offersService.AddAsync(model);

            return Redirect($"/Offers");
        }

        public IActionResult Details(int offerId)
        {
            var offer = offersService.GetById(offerId);

            if (offer == null)
            {
                return Redirect("/Offers");
            }

            var model = AutoMapperConfig.MapperInstance.Map<AllOffersViewModel>(offer);

            return View(model);
        }

        public IActionResult Edit(int offerId)
        {
            var offer = offersService.GetById(offerId);

            if (offer == null)
            {
                return Redirect("/Offers");
            }

            var model = AutoMapperConfig.MapperInstance.Map<AllOffersViewModel>(offer);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int offerId, AllOffersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            offersService.Edit(offerId, model);

            return Redirect($"/Offers/Details?offerId={offerId}");
        }

        public IActionResult Delete(int offerId)
        {
            offersService.Delete(offerId);

            return Redirect("/Offers");
        }

        public IActionResult Undelete()
        {
            var deletedOffers = offersService.GetDeleted<UndeleteViewModel>();

            return View(deletedOffers);
        }

        [HttpPost]
        public IActionResult Undelete(int offerId)
        {
            offersService.Undelete(offerId);

            return Redirect($"/Offers/Details?offerId={offerId}");
        }
    }
}
