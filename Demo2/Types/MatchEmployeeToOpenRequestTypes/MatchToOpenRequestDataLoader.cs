using Demo2.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo2.Types.MatchEmployeeToOpenRequestTypes
{
    public static class MatchToOpenRequestDataLoader
    {
        [DataLoader]
        public static async Task<IReadOnlyDictionary<Guid, MatchToOpenRequest>> GetMatchToOpenRequestsByMatchIdAsync(IReadOnlyList<Guid> guids,
                                                                                                         Demo2DbContext mSNightDbContext,
                                                                                                         CancellationToken cancellationToken)
        {
            var data = await mSNightDbContext.Set<MatchToOpenRequest>()
                .Where(_ => guids.Select(_ => MatchToOpenRequestId.FromGuid(_)).Contains(_.Id)).Include(_ => _.OpenRequest).Include(_ => _.Employee)
                .ToListAsync(cancellationToken);
            return data.ToDictionary(_ => _.Id.Value);

        }
        [DataLoader]
        public static async Task<ILookup<Guid, MatchToOpenRequest>> GetMatchToOpenRequestByOpenRequestId(IReadOnlyList<Guid> guids,
                                                                                                         Demo2DbContext mSNightDbContext,
                                                                                                         CancellationToken cancellationToken)
        {
            var data = await mSNightDbContext.Set<MatchToOpenRequest>()
                .Where(_ => guids.Select(_ => OpenRequestId.FromGuid(_)).Contains(_.OpenRequestId)).Include(_ => _.OpenRequest).Include(_ => _.Employee)
                .ToListAsync(cancellationToken);
            return data.ToLookup(_ => _.OpenRequestId.Value);

        }
        [DataLoader]
        public static async Task<ILookup<Guid, MatchToOpenRequest>> GetMatchToOpenRequestByEmployeeId(IReadOnlyList<Guid> guids,
                                                                                                          Demo2DbContext mSNightDbContext,
                                                                                                          CancellationToken cancellationToken)
        {
            var data = await mSNightDbContext.Set<MatchToOpenRequest>()
                .Where(_ => guids.Select(_ => EmployeeId.FromGuid(_)).Contains(_.EmployeeId)).Include(_ => _.OpenRequest).Include(_ => _.Employee)
                .ToListAsync(cancellationToken);
            return data.ToLookup(_ => _.EmployeeId.Value);

        }
    }
}
