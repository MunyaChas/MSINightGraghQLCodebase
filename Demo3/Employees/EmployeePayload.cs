using Demo3.Common;
using Demo3.Data;

namespace Demo3.Employees
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
