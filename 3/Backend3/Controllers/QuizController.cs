using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend1.Services;
using Backend3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend3.Controllers
{
    public class QuizController : Controller
    {
        private readonly IRandomService _randomService;
        private readonly IOperationsService _operationsService;

        public QuizController(IRandomService randomService, IOperationsService operationsService)
        {
            this._randomService = randomService;
            this._operationsService = operationsService;
        }

        public IActionResult QuizQuestion()
        {
            var model = new QuizModel
            {
                FirstInt = this._randomService.RandInt(),
                ArithmeticOperator = this._randomService.RandOperator(),
                SecondInt = this._randomService.RandInt()
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuizQuestion(QuizModel model, QuizAction action)
        {
            var newExpression = new Expression()
            {
                FirstInt = model.FirstInt,
                ArithmeticOperator = model.ArithmeticOperator,
                SecondInt = model.SecondInt,
                Result = model.Result,
                Correct = this.CheckCorrectness(model)
            };    

            model.Expressions.Add(newExpression);

            switch (action)
            {
                case QuizAction.Submit:
                    model.FirstInt = this._randomService.RandInt();
                    model.ArithmeticOperator = this._randomService.RandOperator();
                    model.SecondInt = this._randomService.RandInt();
                    model.Result = 0;

                    this.ModelState.Remove("FirstInt");
                    this.ModelState.Remove("ArithmeticOperator");
                    this.ModelState.Remove("SecondInt");
                    this.ModelState.Remove("Result");

                    return this.View(model);
                case QuizAction.Finish:
                    return this.View("QuizResult", model);
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        private Boolean CheckCorrectness(QuizModel model)
        {
            if ((model.ArithmeticOperator == "+" &&
                model.Result == this._operationsService.Addition(model.FirstInt, model.SecondInt))
                ||
                (model.ArithmeticOperator == "-" &&
                model.Result == this._operationsService.Subtraction(model.FirstInt, model.SecondInt))
                ||
                (model.ArithmeticOperator == "*" &&
                model.Result == this._operationsService.Multiplication(model.FirstInt, model.SecondInt))
                ||
                (model.ArithmeticOperator == "/" &&
                model.Result == this._operationsService.Division(model.FirstInt, model.SecondInt)))
            {
                return true;
            }

            return false;
        }
    }
}