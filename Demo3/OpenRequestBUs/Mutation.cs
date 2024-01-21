using Demo3.Common;
using Demo3.Data;

namespace Demo3.OpenRequestBUs
{
    [MutationType]
    public class Mutation
    {
        public async Task<MutationResult<OpenRequestBUPayload>> AddOpenRequestBU(AddOpenRequestBUInput input,
                                                                                 Demo3DbContext dbContext,
                                                                                 CancellationToken cancellationToken)
        {
            var openRequestBU = OpenRequestBU.Create(OpenRequestId.FromGuid(null),
                                                     TeamRequestId.FromGuid(null),
                                                     new TeamRequestName(input.TeamRequestName),
                                                     new PositionName(input.PositionName),
                                                     new Department(input.Cluster),
                                                     new PositionDescription(input.PositionDescription),
                                                     new Data.Location(input.Location),
                                                     new NumberOfFTERequired(input.NumberOfFTERequired),
                                                     new AccountManager(input.AccountManager),
                                                     new SkillLevel(input.SkillLevel),
                                                     new RoleStartDate(input.RoleStartDate),
                                                     new DeadLine(input.DeadLine),
                                                     input.Competences?.ConvertAll(_ => new Competence(_.SkillName, _.YearsOfExperience)));

            dbContext.Set<OpenRequestBU>().Add(openRequestBU);
            await dbContext.SaveChangesAsync(cancellationToken);

            // await eventSender.SendAsync(nameof(Subscription.OnOpenRequestBUAdded), openRequestBU, cancellationToken);

            return new OpenRequestBUPayload(openRequestBU);
        }

        public async Task<MutationResult<OpenRequestBUPayload>> UpdateOpenRequestBU(UpdateOpenRequestBUInput input,
                                                                                    Demo3DbContext dbContext,
                                                                                    CancellationToken cancellationToken)
        {
            var openRequestBU = await dbContext.Set<OpenRequestBU>().FindAsync(input.Id.Value);
            if (openRequestBU == null)
            {
                return new MutationResult<OpenRequestBUPayload>(new ErrorResult($"OpenRequestBU with id {input.Id} not found.", "Not found"));
            }

            openRequestBU.Update(new TeamRequestName(input.TeamRequestName),
                                 new PositionName(input.PositionName),
                                 new Department(input.Cluster),
                                 new PositionDescription(input.PositionDescription),
                                 new Data.Location(input.Location),
                                 new NumberOfFTERequired(input.NumberOfFTERequired),
                                 new AccountManager(input.AccountManager),
                                 new SkillLevel(input.SkillLevel),
                                 new RoleStartDate(input.RoleStartDate),
                                 new DeadLine(input.DeadLine),
                                 input.Competences?.ConvertAll(_ => new Competence(_.SkillName, _.YearsOfExperience)));

            await dbContext.SaveChangesAsync(cancellationToken);

            // await eventSender.SendAsync(nameof(Subscription.OnOpenRequestBUUpdated), openRequestBU, cancellationToken);

            return new OpenRequestBUPayload(openRequestBU);
        }
    }
}
