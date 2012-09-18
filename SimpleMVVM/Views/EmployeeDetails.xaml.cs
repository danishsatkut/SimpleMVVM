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
using SimpleMVVM.EmployeeServiceClient;

namespace SimpleMVVM
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

