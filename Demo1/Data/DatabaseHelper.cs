using Demo1.Common;
using Demo1.Helpers;
using Microsoft.Extensions.Options;

namespace Demo1.Data
{
    public class DatabaseHelper
    {
        public static async Task SeedDatabaseAsync(WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<Demo1DbContext>();
            var bogusConfig = scope.ServiceProvider.GetRequiredService<IOptions<BogusGenerateData>>();
            if (await context.Database.EnsureCreatedAsync())
            {
                await AddEmployees(context, bogusConfig.Value.NumberOfEmployees);
                await AddOpenRequestBU(context, bogusConfig.Value.NumberOfOpenRequests);
                await context.SaveChangesAsync();
            }
        }

        private static async Task AddOpenRequestBU(Demo1DbContext context, int numberOfOpenRequests)
        {
            var sampleOpenRequestBU = new OpenRequestBUData().GetOpenRequestBUData(numberOfOpenRequests);
            var addOpenRequestBU = new List<OpenRequestBU>();
            foreach (var openRequestBU in sampleOpenRequestBU)
            {
                var openRequestBUEntity = OpenRequestBU.Create(
                                            OpenRequestId.FromGuid(openRequestBU.OpenRequestId),
                                            TeamRequestId.FromGuid(openRequestBU.TeamRequestId),
                                            new TeamRequestName(openRequestBU.TeamRequestName),
                                            new PositionName(openRequestBU.PositionName),
                                            new Department(openRequestBU.Cluster.Name),
                                            new PositionDescription(openRequestBU.PositionDescription),
                                            new Location(openRequestBU.Location),
                                            new NumberOfFTERequired(openRequestBU.NumberOfFTERequired),
                                            new AccountManager(openRequestBU.AccountManager),
                                            new SkillLevel(openRequestBU.SkillLevel.Name),
                                            new RoleStartDate(openRequestBU.RoleStartDate),
                                            new DeadLine(openRequestBU.DeadLine),
                                            openRequestBU.Competences?.ConvertAll(_ => new Competence(_.Value, _.YearsOfExperience)));

                addOpenRequestBU.Add(openRequestBUEntity);
            }
            await context.Set<OpenRequestBU>().AddRangeAsync(addOpenRequestBU);
        }

        private static async Task AddEmployees(Demo1DbContext context, int numberOfEmployees)
        {
            var sampleEmployeeData = new EmployeeData().GetEmployeeData(numberOfEmployees);
            var addEmployee = new List<Employee>();
            foreach (var employee in sampleEmployeeData)
            {
                var employeeNameResult = Name.Create(employee.FirstName, employee.LastName);
                var employeePhoneResult = Phone.Create(employee.Phone);
                var employeeEmployeeCodeResult = EmployeeCode.Create(employee.EmployeeCode);
                var employeeEmailResult = Email.Create(employee.Email);
                if (employeeNameResult.IsSuccess || employeePhoneResult.IsSuccess || employeeEmployeeCodeResult.IsSuccess || employeeEmailResult.IsSuccess)
                {
                    var employeeEntity = Employee.Create(
                    EmployeeId.FromGuid(Guid.NewGuid()),
                    employeeNameResult.Value,
                    employeeEmailResult.Value,
                    employeePhoneResult.Value,
                    employeeEmployeeCodeResult.Value,
                    employee.SkillsMatrices?.ConvertAll(_ => SkillsMatrix.Create(SkillsMatrixId.FromGuid(_.SkillsMatrixId),
                    Skill.Create(_.Skill).Value, new SkillLevel(_.SkillLevel.Name), new YearOfExperience(_.YearsOfExperience))),
                    employee.Qualifications?.ConvertAll(_ => Qualification.Create(QualificationId.FromGuid(_.QualificationId),
                    new NameOfQualification(_.NameOfQualification), new Institute(_.Institute), new Year(_.YearCompleted))));
                    addEmployee.Add(employeeEntity);
                }
            }
            await context.Set<Employee>().AddRangeAsync(addEmployee);
        }
    }
}
