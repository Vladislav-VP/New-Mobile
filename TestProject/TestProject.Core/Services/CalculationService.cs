using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Core.Services
{
    public class CalculationService : ICalculationService
    {
        public double TipAmount(double subTotal, int generosity)
        {
            return subTotal * generosity / 100.0;
        }
    }
}
