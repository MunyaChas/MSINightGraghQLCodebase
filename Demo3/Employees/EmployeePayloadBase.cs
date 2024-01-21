using Demo3.Common;
using Demo3.Data;

namespace Demo3.Employees
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