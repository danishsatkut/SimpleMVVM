using System.Windows;
using System.Windows.Controls;
using SimpleMVVM.Models;

namespace SimpleMVVM.Views
{
    public partial class EmployeeDetails : ChildWindow
    {
        public EmployeeDetails()
        {
            InitializeComponent();
        }

        private Employee _employee;
        public Employee Employee
        {
            get { return _employee; }
            set 
            { 
                _employee = value;
                DataContext = _employee;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

