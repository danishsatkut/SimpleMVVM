﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Employee = SimpleMVVM.Models.Employee;
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
                EditSelectedeEmployeeCommand.OnCanExecuteChanged();
            }
        }

        /// <summary>
        /// Loads the employees collection for the view.
        /// </summary>
        public void LoadEmployees()
        {
            if (_dataService.AreEmployeesLoaded)
            {
                Employees = ShapeEmployees(_dataService.Employees);
                OnEmployeesLoaded();
            }
            else
            {
                _dataService.EmployeesLoaded += (s, ea) =>
                {
                    Employees = ShapeEmployees(_dataService.Employees);
                    OnEmployeesLoaded();
                };
                _dataService.LoadEmployees();
            }
        }

        private ObservableCollection<Employee> ShapeEmployees(IEnumerable<EmployeeServiceClient.Employee> employees)
        {
            // Create the collection
            var shapedEmployees = new ObservableCollection<Employee>();

            // Shape each employee and add it to the collection
            foreach (EmployeeServiceClient.Employee employee in employees)
            {
                var employeeModel = new Employee
                                        {
                                            FirstName = employee.Person.FirstName,
                                            LastName = employee.Person.LastName,
                                            Gender = employee.Gender,
                                            HireDate = employee.HireDate,
                                            Salaried = employee.SalariedFlag,
                                            Title = employee.JobTitle,
                                            VacationHours = employee.VacationHours
                                        };
                shapedEmployees.Add(employeeModel);
            }

            // Return the collection
            return shapedEmployees;
        }

        public event EventHandler EmployeesLoaded;
        protected void OnEmployeesLoaded()
        {
            if (EmployeesLoaded != null)
                EmployeesLoaded(this, EventArgs.Empty);
        }

        public event EventHandler<ShowEditEmployeeDialogEventArgs> ShowEditEmployeeDialog;

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

        // Command calls this method as the action
        private void AddVacationBonusToSelectedEmployee()
        {
            if (SelectedEmployee != null)
            {
                var vacationBonusService = new EmployeeVacationBonusService();
                SelectedEmployee.VacationHours += vacationBonusService.AddVacationBonus(SelectedEmployee.HireDate);
            }
        }

        public bool CanEditEmployee
        {
            get { return SelectedEmployee != null; }
        }

        private ViewModelCommand _editSelectedEmployeeCommand = null;
        public ViewModelCommand EditSelectedeEmployeeCommand
        {
            get
            {
                if (_editSelectedEmployeeCommand == null)
                {
                    _editSelectedEmployeeCommand = new ViewModelCommand(
                            p => EditSelectedEmployee(),
                            p => CanEditEmployee
                        );
                }

                return _editSelectedEmployeeCommand;
            }
        }

        // Command calls this method as action
        private void EditSelectedEmployee()
        {
            if (SelectedEmployee != null)
            {
                ShowEditEmployeeDialog(this, new ShowEditEmployeeDialogEventArgs(SelectedEmployee));
            }
        }

        #endregion
    }

    public class ShowEditEmployeeDialogEventArgs : EventArgs
    {
        public Employee SelectedEmployee { get; set; }

        public ShowEditEmployeeDialogEventArgs(Employee selectedEmployee)
        {
            SelectedEmployee = selectedEmployee;
        }
    }
}
