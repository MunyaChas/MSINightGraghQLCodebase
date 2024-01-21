using Demo2.Common;
using Demo2.Data;

namespace Demo2.Employees
{
    public class EmployeePayload : EmployeePayloadBase
    {
        public EmployeePayload(Employee employee)
            : base(employee)
        {
        }

        public EmployeePayload(ErrorResult error)
            : base(new[] { error })
        {
        }
        public EmployeePayload(IReadOnlyList<ErrorResult> errors)
            : base(errors)
        {
        }
    }
}
