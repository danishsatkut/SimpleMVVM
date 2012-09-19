using System;
using System.Collections.ObjectModel;
using SimpleMVVM.EmployeeServiceClient;
using SimpleMVVM.Services;

namespace SimpleMVVM.ViewModels
{
    public class EmployeeListViewModel : ViewModel
    {
        /// <summary>
        ///     Collection containing all the employees to be displayed.
        /// </summary>
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set 
            { 
                _employees = value;
                OnPropertyChanged("Employees");
            }
        }

        /// <summary>
        ///     Represents the current selected employee
        /// </summary>
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set 
            { 
                _selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        public void LoadEmployees()
        {
            var client = new EmployeeServiceClient.EmployeeServiceClient();
            client.GetEmployeesCompleted += (s, ea) =>
            {
                Employees = ea.Result;
                OnEmployeesLoaded();
            };
            client.GetEmployeesAsync();
        }

        public void AddVacationBonusToSelectedEmployee()
        {
            if (SelectedEmployee != null) 
            {
                var vacationBonusService = new EmployeeVacationBonusService();
                SelectedEmployee.VacationHours += vacationBonusService.AddVacationBonus(SelectedEmployee.HireDate);
            }
        }

        public event EventHandler EmployeesLoaded;
        protected void OnEmployeesLoaded()
        {
            if (EmployeesLoaded != null)
                EmployeesLoaded(this, EventArgs.Empty);
        }
    }
}
