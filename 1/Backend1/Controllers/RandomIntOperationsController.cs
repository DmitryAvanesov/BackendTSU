using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend1.Models;
using Backend1.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend1.Controllers
{
    public class RandomIntOperationsController : Controller
    {
        private readonly IRandomIntService _randomIntService;
        private readonly IArithmeticOperationsService _arithmeticOperationsService;

        public RandomIntOperationsController(IRandomIntService randomIntService, IArithmeticOperationsService arithmeticOperationsService)
        {
            this._randomIntService = randomIntService;
            this._arithmeticOperationsService = arithmeticOperationsService;
        }

        public ActionResult PassUsingViewData()
        {
            var firstInt = this._randomIntService.RandInt();
            var secondInt = this._randomIntService.RandInt();

            this.ViewData["FirstInt"] = this._randomIntService.RandInt();
            this.ViewData["SecondInt"] = this._randomIntService.RandInt();

            this.ViewData["AdditionResult"] = this._arithmeticOperationsService.Addition(firstInt, secondInt);
            this.ViewData["SubtractionResult"] = this._arithmeticOperationsService.Subtraction(firstInt, secondInt);
            this.ViewData["MultiplicationResult"] = this._arithmeticOperationsService.Multiplication(firstInt, secondInt);
            this.ViewData["DivisionResult"] = this._arithmeticOperationsService.Division(firstInt, secondInt);

            return this.View();
        }

        public ActionResult PassUsingViewBag()
        {
            var firstInt = this._randomIntService.RandInt();
            var secondInt = this._randomIntService.RandInt();

            this.ViewBag.firstInt = firstInt;
            this.ViewBag.secondInt = secondInt;

            this.ViewBag.additionResult = this._arithmeticOperationsService.Addition(firstInt, secondInt);
            this.ViewBag.subtractionResult = this._arithmeticOperationsService.Subtraction(firstInt, secondInt);
            this.ViewBag.multiplicationResult = this._arithmeticOperationsService.Multiplication(firstInt, secondInt);
            this.ViewBag.divisionResult = this._arithmeticOperationsService.Division(firstInt, secondInt);

            return this.View();
        }

        public ActionResult PassUsingModel()
        {
            var firstInt = this._randomIntService.RandInt();
            var secondInt = this._randomIntService.RandInt();

            var model = new RandomIntOperationsViewModel
            {
                FirstInt = firstInt,
                SecondInt = secondInt,

                AdditionResult = this._arithmeticOperationsService.Addition(firstInt, secondInt),
                SubtractionResult = this._arithmeticOperationsService.Subtraction(firstInt, secondInt),
                MultiplicationResult = this._arithmeticOperationsService.Multiplication(firstInt, secondInt),
                DivisionResult = this._arithmeticOperationsService.Division(firstInt, secondInt)
            };

            return this.View(model);
        }

        public ActionResult AccessServiceDirectly()
        {
            return this.View();
        }
    }
}
