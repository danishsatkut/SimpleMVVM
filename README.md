Simple MVVM Implementation
==========================

This is a simple MVVM architectural pattern implementation
in Silverlight. It does not use any MVVM framework.

This small application displays employee list in a DataGrid.
On selecting any employee it allows the functionality to add 
vacation hours or edit the selected employee.

This example is taken from Silverlight 4 in Action by Pete Brown.
Although there are some changes in the architecture of this example.

Views
-----

### Employee List
	Displays the list of all the employees. Its view-model is EmployeeListViewModel

### Employee Details
	A window to edit the current employee. Current design decision is not to use a 
	view-model for this view.

ViewModels
----------

### EmployeeListViewModel
	The view-model for EmployeeList view. It loads its data using service classes.

Models
------

### Employee
	Represents a single employee.

Notes
-----

1. The example in the book uses AdventureWorks as the database, but here I am using
AdventureWorks2008R2. The difference between them is that Contact entity is now represented as
Person entity in AdventureWorks2008R2.

2. In the book, Employee model is represented as EmployeeViewModel, although there is 
virtually no difference between them. I just like the term model better.

3. Currently no unit tests are defined for any classes.


