using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using SimpleMVVM.ViewModels;

namespace SimpleMVVM
{
    public partial class EmployeeList : Page
    {
        public EmployeeList()
        {
            InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (_viewModel == null)
            {
                _viewModel = new EmployeeListViewModel();
                _viewModel.EmployeesLoaded += (s, ea) =>
                {
                    LoadingProgress.Visibility = Visibility.Collapsed;
                };

                LoadingProgress.Visibility = Visibility.Visible;

                DataContext = _viewModel;
                _viewModel.LoadEmployees();
            }
        }

        private EmployeeDetails _employeeDetails = new EmployeeDetails();
        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            _employeeDetails.Employee = _viewModel.SelectedEmployee;
            _employeeDetails.Show();
        }

        private EmployeeListViewModel _viewModel = null;

        private void AddMoreVacation_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AddVacationBonusToSelectedEmployee();
        }
    }
}
