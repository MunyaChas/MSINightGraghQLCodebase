using Demo2.Common;
using Demo2.Data;

namespace Demo2.Employees
{
    public class EmployeePayloadBase : Payload
    {
        protected EmployeePayloadBase(Employee employee)
        {
            Employee = employee;
        }

        protected EmployeePayloadBase(IReadOnlyList<ErrorResult> errors)
            : base(errors)
        {
        }

        public Employee? Employee { get; }
    }
}
