using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend1.Services
{
    public class OperationsService : IOperationsService
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