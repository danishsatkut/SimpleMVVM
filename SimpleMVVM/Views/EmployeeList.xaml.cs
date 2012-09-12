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
using SimpleMVVM.EmployeeServiceClient;

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
            if (EmployeesGrid.ItemsSource == null)
            {
                LoadingProgress.Visibility = Visibility.Visible;

                var client = new EmployeeServiceClient.EmployeeServiceClient();
                client.GetEmployeesCompleted += new EventHandler<GetEmployeesCompletedEventArgs>(client_GetEmployeesCompleted);
                client.GetEmployeesAsync();
            }
        }

        void client_GetEmployeesCompleted(object sender, GetEmployeesCompletedEventArgs e)
        {
            LoadingProgress.Visibility = Visibility.Collapsed;
            EmployeesGrid.ItemsSource = e.Result;
        }
    }
}
