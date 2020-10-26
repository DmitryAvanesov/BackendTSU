using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend4.Models;
using Backend4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend4.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public IActionResult SignUp()
        {
            return View(new SignUpModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                if (_registrationService.UserExists(model.FirstName, model.LastName,
                    DateTime.Parse(model.Day + model.Month + model.Year), model.Gender))
                {
                    model.Existed = true;
                    return View("SignUpAlreadyExists", model);
                }
                else
                {
                    model.Existed = false;

                    return View("SignUpCredentials", new SignUpCredentialsModel
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Day = model.Day,
                        Month = model.Month,
                        Year = model.Year,
                        Gender = model.Gender,
                        Existed = model.Existed
                    });
                }
            }

            return View(model);
        }

        [ValidateAntiForgeryToken]
        public IActionResult SignUpAlreadyExists(SignUpModel model)
        {
            return View("SignUpCredentials", new SignUpCredentialsModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Day = model.Day,
                Month = model.Month,
                Year = model.Year,
                Gender = model.Gender,
                Existed = model.Existed
            });
        }

        [ValidateAntiForgeryToken]
        public IActionResult SignUpCredentials(SignUpCredentialsModel model)
        {
            if (ModelState.IsValid)
            {
                _registrationService.AddUser(model.FirstName, model.LastName,
                    DateTime.Parse(model.Day + model.Month + model.Year), model.Gender,
                    model.Email, model.Password, model.Existed, model.Remembered);

                return View("SignUpResult", model);
            }

            return View();
        }
    }
}