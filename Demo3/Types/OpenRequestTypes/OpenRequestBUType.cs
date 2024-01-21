using Demo3.Data;

namespace Demo3.Types.OpenRequestTypes
{
    public class OpenRequestBUType : ObjectType<OpenRequestBU>
    {
        protected override void Configure(IObjectTypeDescriptor<OpenRequestBU> descriptor)
        {
            descriptor.Field(_ => _.Id).Type<NonNullType<IdType>>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.Id.Value;
            });
            descriptor.Field(_ => _.PositionName).Type<StringType>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.PositionName.Value;
            });
            descriptor.Field(_ => _.SkillLevel).Type<StringType>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.SkillLevel.Value;
            });
            descriptor.Field(_ => _.RoleStartDate).Type<StringType>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.RoleStartDate.Value;
            });
            descriptor.Field(_ => _.DeadLine).Type<StringType>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.DeadLine.Value;
            });
            descriptor.Field(_ => _.TeamRequestName).Type<StringType>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.TeamRequestName.Value;
            });
            descriptor.Field(_ => _.Cluster).Type<StringType>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.Cluster.Value;
            });
            descriptor.Field(_ => _.PositionDescription).Type<StringType>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.PositionDescription.Value;
            });
            descriptor.Field(_ => _.Location).Type<StringType>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.Location.Value;
            });
            descriptor.Field(_ => _.NumberOfFTERequired).Type<IntType>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.NumberOfFTERequired.Value;
            });
            descriptor.Field(_ => _.AccountManager).Type<StringType>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.AccountManager.Value;
            });
            descriptor.Field(_ => _.Competences).Type<ListType<CompetenceType>>().Resolve(context =>
            {
                var openRequestBU = context.Parent<OpenRequestBU>();
                return openRequestBU.Competences;
            });

        }
    }
    internal class CompetenceType : ObjectType<Competence>
    {
        protected override void Configure(IObjectTypeDescriptor<Competence> descriptor)
        {
            descriptor.Field(_ => _.Value).Type<StringType>().Resolve(context =>
            {
                var competence = context.Parent<Competence>();
                return competence.Value;
            }).Name("Competence");
            descriptor.Field(_ => _.YearsOfExperience).Type<IntType>().Resolve(context =>
            {
                var competence = context.Parent<Competence>();
                return competence.YearsOfExperience;
            });

        }
    }
}
