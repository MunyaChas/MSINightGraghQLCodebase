namespace Demo3.Employees
{
    public sealed record AddEmployeeInput(
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        List<SkillsMatrixInput>? SkillsMatrices = null,
        List<QualificationInput>? Qualifications = null
    );

    public sealed record SkillsMatrixInput(
        Guid? SkillsMatrixId,
        string SkillName,
        string SkillLevel,
        int YearsOfExperience
    );

    public sealed record QualificationInput(
        Guid? QualificationId,
        string NameOfQualification,
        string Institute,
        int YearCompleted
    );
}
