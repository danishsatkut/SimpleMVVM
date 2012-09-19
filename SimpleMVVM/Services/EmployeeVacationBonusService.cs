using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SimpleMVVM.Services
{
    public class EmployeeVacationBonusService
    {
        /// <summary>
        /// Calculates the vacation bonus for a person based on the hire date.
        /// </summary>
        /// <param name="hireDate">Date of hiring</param>
        /// <returns>Bonus vacation hours</returns>
        public short AddVacationBonus(DateTime hireDate)
        {
            short vacationBonus;
            DateTime today = DateTime.Today;

            int yearsInService = today.Year - hireDate.Year;
            if (hireDate.AddYears(yearsInService) > today)
                yearsInService--;

            // Vacation bonus criteria
            if (yearsInService < 5)
                vacationBonus = 10;
            else if (yearsInService < 10)
                vacationBonus = 22;
            else if (yearsInService < 15)
                vacationBonus = 80;
            else
                vacationBonus = 100;

            return vacationBonus;
        }
    }
}
