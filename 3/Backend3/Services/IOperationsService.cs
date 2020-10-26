using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend1.Services
{
    public interface IOperationsService
    {
        Int32 Addition(Int32 firstInt, Int32 secondInt);
        Int32 Subtraction(Int32 firstInt, Int32 secondInt);
        Int32 Multiplication(Int32 firstInt, Int32 secondInt);
        Int32 Division(Int32 firstInt, Int32 secondInt);
    }
}
