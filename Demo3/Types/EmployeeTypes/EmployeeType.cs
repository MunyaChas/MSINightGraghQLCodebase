using Demo3.Data;

namespace Demo3.Types.EmployeeTypes
{
    public class EmployeeType : ObjectType<Employee>
    {
        protected override void Configure(IObjectTypeDescriptor<Employee> descriptor)
        {
            descriptor.Field(_ => _.Id).Type<NonNullType<IdType>>().Resolve(ctx =>
            {
                var employee = ctx.Parent<Employee>();
                return employee.Id.Value;
            });
            descriptor.Field(e => e.Name).Type<StringType>()
                .Resolve(context =>
                {
                    var employee = context.Parent<Employee>();
                    return employee.Name.First + " " + employee.Name.Last;
                }).UseFiltering();
            descriptor.Field(e => e.Email).Type<StringType>().Resolve(context => context.Parent<Employee>().Email.Value);
            descriptor.Field(e => e.Phone).Type<StringType>().Resolve(context => context.Parent<Employee>().Phone.Value);
            descriptor.Field(e => e.EmployeeCode).Type<StringType>().Resolve(context => context.Parent<Employee>().EmployeeCode.Value);
            descriptor.Field(e => e.Qualifications).Type<ListType<QualificationType>>().UseFiltering();
            descriptor.Field(e => e.SkillsMatrices).Type<ListType<SkillsMatrixType>>().UseFiltering();
            descriptor.Field("EmpId").Type<UuidType>().Resolve(ctx =>
            {
                var employee = ctx.Parent<Employee>();
                return employee.Id.Value;
            });
        }
    }


    internal class QualificationType : ObjectType<Qualification>
    {
        protected override void Configure(IObjectTypeDescriptor<Qualification> descriptor)
        {
            descriptor.Field(_ => _.QualificationId).Type<NonNullType<IdType>>().Resolve(context =>
            {
                var qualification = context.Parent<Qualification>();
                return qualification.QualificationId.Value;
            });
            descriptor.Field(_ => _.NameOfQualification).Type<StringType>().Resolve(context =>
            {
                var qualification = context.Parent<Qualification>();
                return qualification.NameOfQualification.Value;
            });
            descriptor.Field(_ => _.Institute).Type<StringType>().Resolve(context =>
            {
                var qualification = context.Parent<Qualification>();
                return qualification.Institute.Value;
            });
            descriptor.Field(_ => _.YearCompleted).Type<IntType>().Resolve(context =>
            {
                var qualification = context.Parent<Qualification>();
                return qualification.YearCompleted.Value;
            });
        }
    }
    internal class SkillsMatrixType : ObjectType<SkillsMatrix>
    {
        protected override void Configure(IObjectTypeDescriptor<SkillsMatrix> descriptor)
        {
            descriptor.Field(_ => _.SkillsMatrixId).Type<NonNullType<IdType>>().Resolve(context =>
            {
                var skillsMatrix = context.Parent<SkillsMatrix>();
                return skillsMatrix.SkillsMatrixId.Value;
            });
            descriptor.Field(_ => _.Skill).Type<StringType>().Resolve(context =>
            {
                var skillsMatrix = context.Parent<SkillsMatrix>();
                return skillsMatrix.Skill.Value;
            });
            descriptor.Field(_ => _.SkillLevel).Type<StringType>().Resolve(context =>
            {
                var skillsMatrix = context.Parent<SkillsMatrix>();
                return skillsMatrix.SkillLevel.Value;
            });
            descriptor.Field(_ => _.YearsOfExperience).Type<IntType>().Resolve(context =>
            {
                var skillsMatrix = context.Parent<SkillsMatrix>();
                return skillsMatrix.YearsOfExperience.Value;
            });
        }
    }
}
