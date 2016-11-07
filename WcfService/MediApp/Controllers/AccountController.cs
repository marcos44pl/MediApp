using System.Web.Mvc;
using MediApp.Models;
using System.Security.Cryptography;
using System.Text;
using MediApp.WcfControllers;

namespace MediApp.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        //
        // POST: /Account/Login
       [AllowAnonymous]
       [ValidateAntiForgeryToken]
       [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            SHA256Managed crypt = new SHA256Managed();
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(model.Password), 0,
                                              Encoding.ASCII.GetByteCount(model.Password));

            if (WcfController.authenticateUser(model.Email, crypto))
            {
                Security.SessionPersister.Username = model.Email;

                if(null != returnUrl)
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("IncorrectData", "Email i hasło nie pasują do siebie!");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = WcfController.checkIfExist(model.Email);

                if (result)
                {
                    ModelState.AddModelError("Email","Istnieje już taki użytkownik!");
                    return View(model);
                }

                SHA256Managed crypt = new SHA256Managed();
                byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(model.Password), 0,
                                                  Encoding.ASCII.GetByteCount(model.Password));

               
                WcfController.createPatient(new EntityModels.User{ Pass = crypto,
                    Email = model.Email, Surname = model.SurName,FstName = model.FirstName });

                return RedirectToAction("Index", "Home");
                
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }



        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Security.SessionPersister.Username = string.Empty;
            return RedirectToAction("Index", "Home");
        }

    }
}