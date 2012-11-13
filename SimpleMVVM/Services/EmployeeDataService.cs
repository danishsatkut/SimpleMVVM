using System;
using System.Collections.ObjectModel;
using SimpleMVVM.EmployeeServiceClient;

namespace SimpleMVVM.Services
{
    public class EmployeeDataService
    {
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            private set { _employees = value; }
        }

        private bool _areEmployeesLoaded = false;
        public bool AreEmployeesLoaded
        {
            get { return _areEmployeesLoaded; }
            private set { _areEmployeesLoaded = value; }

        }

        /// <summary>
        /// Gets the employee data from service client.
        /// </summary>
        public void LoadEmployees()
        {
            var client = new EmployeeServiceClient.EmployeeServiceClient();
            client.GetEmployeesCompleted += (sender, args) =>
                                                {
                                                    Employees = args.Result;
                                                    AreEmployeesLoaded = true;
                                                    OnEmployeesLoaded();
                                                };
            client.GetEmployeesAsync();
        }

        public event EventHandler EmployeesLoaded;
        private void OnEmployeesLoaded()
        {
            if (EmployeesLoaded != null)
            {
                EmployeesLoaded(this, EventArgs.Empty);
            }
        }
    }
}
