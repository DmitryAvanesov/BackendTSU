using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend2.Services
{
    public class Operations : IOperations
    {
        public Int32 Addition(Int32 firstInt, Int32 secondInt)
        {
            return firstInt + secondInt;
        }

        public Int32 Subtraction(Int32 firstInt, Int32 secondInt)
        {
            return firstInt - secondInt;
        }

        public Int32 Multiplication(Int32 firstInt, Int32 secondInt)
        {
            return firstInt * secondInt;
        }

        public Int32 Division(Int32 firstInt, Int32 secondInt)
        {
            return firstInt / secondInt;
        }

    }
}
