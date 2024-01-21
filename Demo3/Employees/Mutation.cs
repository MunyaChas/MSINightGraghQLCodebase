using Demo3.Common;
using Demo3.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo3.Employees
{
    [MutationType]
    public class Mutation
    {
        public async Task<MutationResult<EmployeePayload>> AddEmployeeAsync(AddEmployeeInput employee,
                                                                            Demo3DbContext dbContext,
                                                                            CancellationToken cancellationToken)
        {
            var employeeNameResult = Name.Create(employee.FirstName, employee.LastName);
            var employeePhoneResult = Phone.Create(employee.Phone);
            var employeeEmailResult = Email.Create(employee.Email);
            if (employeeNameResult.IsSuccess && employeePhoneResult.IsSuccess && employeeEmailResult.IsSuccess)
            {
                try
                {
                    var employeeEntity = Employee.Create(
                    EmployeeId.FromGuid(Guid.NewGuid()),
                    employeeNameResult.Value,
                    employeeEmailResult.Value,
                    employeePhoneResult.Value,
                    EmployeeCode.Create(null).Value,
                    employee.SkillsMatrices?.ConvertAll(_ => SkillsMatrix.Create(SkillsMatrixId.FromGuid(_.SkillsMatrixId),
                    Skill.Create(_.SkillName).Value, new SkillLevel(_.SkillLevel), new YearOfExperience(_.YearsOfExperience))),
                    employee.Qualifications?.ConvertAll(_ => Qualification.Create(QualificationId.FromGuid(_.QualificationId),
                    new NameOfQualification(_.NameOfQualification), new Institute(_.Institute), new Year(_.YearCompleted))));
                    dbContext.Set<Employee>().Add(employeeEntity);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    return new EmployeePayload(employeeEntity);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Add logging
                    return new EmployeePayload(new ErrorResult("A concurrency error occurred.", "ConcurrencyError"));
                }
                catch (DbUpdateException ex)
                {
                    // Add logging
                    return new EmployeePayload(new ErrorResult("An error occurred while updating the database.", "UpdateError"));
                }
                catch (Exception ex)
                {
                    return new EmployeePayload(new ErrorResult(ex.Message, "AddEmployee"));
                }
            }
            else
            {
                return new EmployeePayload(ValidationErrors(employeeNameResult, employeePhoneResult, employeeEmailResult));
            }
        }

        public static class EmployeeQueries
        {
            public static readonly Func<Demo3DbContext, EmployeeId, Task<Employee?>> GetEmployeeById =
                EF.CompileAsyncQuery((Demo3DbContext db, EmployeeId id) =>
                    db.Set<Employee>().FirstOrDefault(e => e.Id == id));
        }

        public async Task<MutationResult<EmployeePayload>> UpdateEmployeeAsync(UpdateEmployeeInput input,
                                                                               Demo3DbContext dbContext,
                                                                               CancellationToken cancellationToken)
        {
            var employee = await dbContext.Set<Employee>().FindAsync(EmployeeId.FromGuid(input.EmployeeId.Value), cancellationToken);
            var employeeNameResult = Name.Create(input.FirstName, input.LastName);
            var employeePhoneResult = Phone.Create(input.Phone);
            var employeeEmailResult = Email.Create(input.Email);
            if ((employeeNameResult.IsSuccess && employeePhoneResult.IsSuccess && employeeEmailResult.IsSuccess) && employee is not null)
            {
                try
                {
                    employee.Update(employeeNameResult.Value, employeeEmailResult.Value, employeePhoneResult.Value,
                        input.SkillsMatrices?.ConvertAll(_ => SkillsMatrix.Create(SkillsMatrixId.FromGuid(_.SkillsMatrixId),
                        Skill.Create(_.SkillName).Value, new SkillLevel(_.SkillLevel), new YearOfExperience(_.YearsOfExperience))),
                        input.Qualifications?.ConvertAll(_ => Qualification.Create(QualificationId.FromGuid(_.QualificationId),
                        new NameOfQualification(_.NameOfQualification), new Institute(_.Institute), new Year(_.YearCompleted))));
                    await dbContext.SaveChangesAsync(cancellationToken);
                    return new EmployeePayload(employee);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Add logging
                    return new EmployeePayload(new ErrorResult("A concurrency error occurred.", "ConcurrencyError"));
                }
                catch (DbUpdateException ex)
                {
                    // Add logging
                    return new EmployeePayload(new ErrorResult("An error occurred while updating the database.", "UpdateError"));
                }
                catch (Exception ex)
                {
                    return new EmployeePayload(new ErrorResult(ex.Message, "AddEmployee"));
                }
            }
            else
            {
                return new EmployeePayload(ValidationErrors(employeeNameResult, employeePhoneResult, employeeEmailResult));
            }
        }

        public async Task<Employee?> DeleteEmployeeAsync(DeleteEmployeeInput input,
                                                         Demo3DbContext dbContext,
                                                         CancellationToken cancellationToken)
        {
            var employee = await dbContext.Set<Employee>().FindAsync(input.EmployeeId);
            if (employee is null)
            {
                return null;
            }
            dbContext.Set<Employee>().Remove(employee);
            await dbContext.SaveChangesAsync(cancellationToken);
            return employee;
        }

        private List<ErrorResult> ValidationErrors(CSharpFunctionalExtensions.Result<Name> employeeNameResult,
                                                   CSharpFunctionalExtensions.Result<Phone> employeePhoneResult,
                                                   CSharpFunctionalExtensions.Result<Email> employeeEmailResult)
        {
            var errors = new List<ErrorResult>();
            if (employeeNameResult.IsFailure)
            {
                errors.Add(new ErrorResult(employeeNameResult.Error, "Name"));
            }
            if (employeePhoneResult.IsFailure)
            {
                errors.Add(new ErrorResult(employeePhoneResult.Error, "Phone"));
            }
            if (employeeEmailResult.IsFailure)
            {
                errors.Add(new ErrorResult(employeeEmailResult.Error, "Email"));
            }
            return errors;
        }
    }
}
