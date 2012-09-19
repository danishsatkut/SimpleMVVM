using System;
using System.Collections.ObjectModel;
using SimpleMVVM.EmployeeServiceClient;
using SimpleMVVM.Services;

namespace SimpleMVVM.ViewModels
{
    public class EmployeeListViewModel : ViewModel
    {
        private EmployeeDataService _dataService = new EmployeeDataService();

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
                AddVacationBonusCommand.OnCanExecuteChanged();
            }
        }

        public void LoadEmployees()
        {
            if (_dataService.AreEmployeesLoaded)
            {
                Employees = _dataService.Employees;
                OnEmployeesLoaded();
            }
            else
            {
                _dataService.EmployeesLoaded += (s, ea) =>
                {
                    Employees = _dataService.Employees;
                    OnEmployeesLoaded();
                };
                _dataService.LoadEmployees();
            }
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

        #region Commands

        public bool CanAddBonus
        {
            get { return SelectedEmployee != null; }
        }

        private ViewModelCommand _addVacationBonusCommand = null;
        public ViewModelCommand AddVacationBonusCommand
        {
            get
            {
                if (_addVacationBonusCommand == null)
                {
                    _addVacationBonusCommand = new ViewModelCommand(
                        p => AddVacationBonusToSelectedEmployee(),
                        p => CanAddBonus
                    );
                }

                return _addVacationBonusCommand;
            }
        }

        #endregion
    }
}
