using Datas.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApp.Common;

namespace WebApp.Controllers
{
    [Route("lien-he")]
    [Route("contact")]
    public class FEContactController : BaseController
    {
        private ContactService _contactService;
        public FEContactController(PeopleCategoryService peopleCategoryService, SettingUtils settingUtils, ContactService contactService) : base(peopleCategoryService, settingUtils)
        {
            _contactService = contactService;
        }

        #region 2.Create
        [HttpGet]
        [Route("")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Route("")]
        [AutoValidateAntiforgeryToken]
        public ActionResult Create(Contact model)
        {
            if (ModelState.IsValid)
            {
                _contactService.Add(model);
                if(string.IsNullOrEmpty(Settings.SubDomain))
                    base.SuccessNotification("Gửi liên hệ thành công!");
                else
                    base.SuccessNotification("Contact sent successfully!");
                return RedirectToAction("Create");
            }
            return View(model);
        }
        #endregion
    }
}
