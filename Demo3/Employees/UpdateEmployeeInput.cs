using Demo3.Data;

namespace Demo3.Employees
{
    public sealed record UpdateEmployeeInput(
            [ID(nameof(Employee))] EmployeeId EmployeeId,
            string FirstName,
            string LastName,
            string Email,
            string Phone,
            List<SkillsMatrixInput>? SkillsMatrices = null,
            List<QualificationInput>? Qualifications = null
        );
}