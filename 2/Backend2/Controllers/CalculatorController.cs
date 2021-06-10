using System;
using Backend2.Models;
using Backend2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend2.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IOperations operationsService;

        public CalculatorController(IOperations operationsService)
        {
            this.operationsService = operationsService;
        }

        public ActionResult Directly()
        {
            if (this.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                var arithmeticOperator = Int32.Parse(this.Request.Form["ArithmeticOperator"]);
                var firstInt = this.Request.Form["FirstInt"];
                var secondInt = this.Request.Form["SecondInt"];

                // Validation

                if (String.IsNullOrEmpty(firstInt))
                {
                    this.ViewBag.ErrorFirst = "First int is required";
                }
                else
                {
                    this.ViewBag.FirstInt = Int32.Parse(firstInt);
                }

                if (String.IsNullOrEmpty(secondInt))
                {
                    this.ViewBag.ErrorSecond = "Second int is required";
                }
                else
                {
                    this.ViewBag.SecondInt = Int32.Parse(secondInt);
                }

                if (arithmeticOperator == 3 && secondInt == "0")
                {
                    this.ViewBag.ErrorSecond = "Division by zero is forbidden";
                }

                // Calculating

                if (this.ViewBag.ErrorFirst == null && this.ViewBag.ErrorSecond == null)
                {
                    if (arithmeticOperator == 0)
                    {
                        this.ViewBag.Result = this.operationsService.Addition(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                    }
                    else if (arithmeticOperator == 1)
                    {
                        this.ViewBag.Result = this.operationsService.Subtraction(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                    }
                    else if (arithmeticOperator == 2)
                    {
                        this.ViewBag.Result = this.operationsService.Multiplication(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                    }
                    else
                    {
                        this.ViewBag.Result = this.operationsService.Division(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                    }
                }
            }

            return this.View();
        }

        public ActionResult WithSeparateHandlers()
        {
            return this.View();
        }

        [HttpPost, ActionName("WithSeparateHandlers")]
        [ValidateAntiForgeryToken]
        public ActionResult WithSeparateHandlersConfirm()
        {
            var arithmeticOperator = Int32.Parse(this.Request.Form["ArithmeticOperator"]);
            var firstInt = this.Request.Form["FirstInt"];
            var secondInt = this.Request.Form["SecondInt"];

            // Validation

            if (String.IsNullOrEmpty(firstInt))
            {
                this.ViewBag.ErrorFirst = "First int is required";
            }
            else
            {
                this.ViewBag.FirstInt = Int32.Parse(firstInt);
            }

            if (String.IsNullOrEmpty(secondInt))
            {
                this.ViewBag.ErrorSecond = "Second int is required";
            }
            else
            {
                this.ViewBag.SecondInt = Int32.Parse(secondInt);
            }

            if (arithmeticOperator == 3 && secondInt == "0")
            {
                this.ViewBag.ErrorSecond = "Division by zero is forbidden";
            }

            // Calculating

            if (this.ViewBag.ErrorFirst == null && this.ViewBag.ErrorSecond == null)
            {
                if (arithmeticOperator == 0)
                {
                    this.ViewBag.Result = this.operationsService.Addition(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                }
                else if (arithmeticOperator == 1)
                {
                    this.ViewBag.Result = this.operationsService.Subtraction(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                }
                else if (arithmeticOperator == 2)
                {
                    this.ViewBag.Result = this.operationsService.Multiplication(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                }
                else
                {
                    this.ViewBag.Result = this.operationsService.Division(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                }
            }

            return this.View();
        }

        public ActionResult ModelBindingInParameters()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModelBindingInParameters(String arithmeticOperator, String firstInt, String secondInt)
        {
            // Validation

            if (String.IsNullOrEmpty(firstInt))
            {
                this.ViewBag.ErrorFirst = "First int is required";
            }
            else
            {
                this.ViewBag.FirstInt = Int32.Parse(firstInt);
            }

            if (String.IsNullOrEmpty(secondInt))
            {
                this.ViewBag.ErrorSecond = "Second int is required";
            }
            else
            {
                this.ViewBag.SecondInt = Int32.Parse(secondInt);
            }

            if (arithmeticOperator == "3" && secondInt == "0")
            {
                this.ViewBag.ErrorSecond = "Division by zero is forbidden";
            }

            // Calculating

            if (this.ViewBag.ErrorFirst == null && this.ViewBag.ErrorSecond == null)
            {
                if (arithmeticOperator == "0")
                {
                    this.ViewBag.Result = this.operationsService.Addition(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                }
                else if (arithmeticOperator == "1")
                {
                    this.ViewBag.Result = this.operationsService.Subtraction(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                }
                else if (arithmeticOperator == "2")
                {
                    this.ViewBag.Result = this.operationsService.Multiplication(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                }
                else
                {
                    this.ViewBag.Result = this.operationsService.Division(this.ViewBag.FirstInt, this.ViewBag.SecondInt);
                }
            }

            return this.View();
        }

        public ActionResult ModelBindingInSeparateModel()
        {
            return this.View(new CalculatorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModelBindingInSeparateModel(CalculatorViewModel model)
        {
            // Validation

            if (this.ModelState.IsValid && !(model.ArithmeticOperator == 3 && model.SecondInt == 0))
            {
                // Calculating

                if (model.ArithmeticOperator == 0)
                {
                    model.Result = this.operationsService.Addition(model.FirstInt, model.SecondInt);
                }
                else if (model.ArithmeticOperator == 1)
                {
                    model.Result = this.operationsService.Subtraction(model.FirstInt, model.SecondInt);
                }
                else if (model.ArithmeticOperator == 2)
                {
                    model.Result = this.operationsService.Multiplication(model.FirstInt, model.SecondInt);
                }
                else
                {
                    model.Result = this.operationsService.Division(model.FirstInt, model.SecondInt);
                }
            }
            else
            {
                model.Result = null;
            }

            return this.View(model);
        }
    }
}
