using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using SimpleMVVM.ViewModels;

namespace SimpleMVVM.Views
{
    public partial class EmployeeList : Page
    {
        private EmployeeListViewModel _viewModel = null;
        private EmployeeDetails _employeeDetails = new EmployeeDetails();

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

                // Attach handler for showing EmployeeDetails dialog
                _viewModel.ShowEditEmployeeDialog += (s, ea) =>
                {
                    _employeeDetails.Employee = ea.SelectedEmployee;
                    _employeeDetails.Show();
                };

                DataContext = _viewModel;
                _viewModel.LoadEmployees();
            }
        }
    }
}
