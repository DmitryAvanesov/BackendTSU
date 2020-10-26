using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend3.Models
{
    public class QuizModel
    {
        public Int32 FirstInt { get; set; }
        public String ArithmeticOperator { get; set; }
        public Int32 SecondInt { get; set; }
        public Int32 Result { get; set; }
        public IList<Expression> Expressions { get; set; } = new List<Expression>();
    }
}
