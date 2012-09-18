using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Collections.Generic;

namespace SimpleMVVM.Web
{
    [ServiceContract(Namespace = "services.web.simplemvvm")]
    [SilverlightFaultBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EmployeeService
    {
        [OperationContract]
        public IList<Employee> GetEmployees()
        {
            var context = new AdventureWorksEntities();
            return context.Employees.ToList();
        }
    }
}
