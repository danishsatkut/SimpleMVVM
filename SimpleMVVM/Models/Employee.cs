using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleMVVM.Models
{
    // TODO: Create the properties for the model employee that we require
    public class Employee : Model
    {
        private string _firstName;
        private string _lastName;
        private string _title;
        private string _gender;
        private bool _salaried;
        private DateTime _hireDate;
        private short _vacationHours;

        [Display(Name = "First Name")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("FullName");
            }
        }

        [Display(Name = "Last Name")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        [Display(Name = "Full Name")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [Display(Name = "Title")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        [Display(Name = "Gender")]
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged("Gender");
            }
        }

        [Display(Name = "Salaried")]
        public bool Salaried
        {
            get { return _salaried; }
            set
            {
                _salaried = value;
                OnPropertyChanged("Salaried");
            }
        }

        [Display(Name = "Hire Date")]
        public DateTime HireDate
        {
            get { return _hireDate; }
            set
            {
                _hireDate = value;
                OnPropertyChanged("HireDate");
            }
        }

        [Display(Name = "Vacation Hours")]
        public short VacationHours
        {
            get { return _vacationHours; }
            set
            {
                _vacationHours = value;
                OnPropertyChanged("VacationHours");
            }
        }
    }
}
