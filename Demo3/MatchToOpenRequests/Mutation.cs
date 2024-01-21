using Demo3.Data;

namespace Demo3.MatchToOpenRequests
{
    [MutationType]
    public class Mutation
    {
        public async Task<MutationResult<MatchToOpenRequestPayload>> AddMatchToOpenRequest(AddMatchToOpenRequestInput input, Demo3DbContext dbContext, CancellationToken cancellationToken)
        {
            var matchToOpenRequest = MatchToOpenRequest.Create(MatchToOpenRequestId.FromGuid(null),
                                                               new MatchScore(input.MatchScore),
                                                               new ApplyForPosition(input.ApplyForPosition),
                                                               new IsMatch(input.IsMatch),
                                                               new IsOpen(input.IsOpen),
                                                               new IsClosed(input.IsClosed),
                                                               new IsHired(input.IsHired),
                                                               new InterviewDate(input.InterviewDate),
                                                               new IsWithdrawn(input.IsWithdrawn),
                                                               EmployeeId.FromGuid(input.EmployeeId.Value),
                                                               OpenRequestId.FromGuid(input.OpenRequestId.Value));

            dbContext.Set<MatchToOpenRequest>().Add(matchToOpenRequest);
            await dbContext.SaveChangesAsync(cancellationToken);

            // await eventSender.SendAsync(nameof(Subscription.OnMatchToOpenRequestAdded), matchToOpenRequest, cancellationToken);

            return new MatchToOpenRequestPayload(matchToOpenRequest);
        }
    }
}
