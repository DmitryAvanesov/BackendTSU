using System;
using System.ComponentModel.DataAnnotations;

namespace Backend2.Models
{
    public class CalculatorViewModel
    {
        public Int32 ArithmeticOperator { get; set; }

        [Required(ErrorMessage = "First int is required")]
        public Int32 FirstInt { get; set; }

        [Required(ErrorMessage = "Second int is required")]
        public Int32 SecondInt { get; set; }

        public Int32? Result { get; set; }
    }
}
