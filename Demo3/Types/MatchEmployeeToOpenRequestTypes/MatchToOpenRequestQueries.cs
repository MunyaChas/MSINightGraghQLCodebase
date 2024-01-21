using Demo3.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo3.Types.MatchEmployeeToOpenRequestTypes
{
    [QueryType]
    public static class MatchToOpenRequestQueries
    {
        [NodeResolver]
        public static async Task<MatchToOpenRequest?> GetMatchToOpenByIdAsync(Guid matchToOpenRequestId,
                                                              MatchToOpenRequestsByMatchIdDataLoader matchIdDataLoader,
                                                              CancellationToken cancellationToken) => await matchIdDataLoader.LoadAsync(matchToOpenRequestId, cancellationToken);

        public static async Task<IEnumerable<MatchToOpenRequest>> GetMatchToOpenByOpenRequestIdAsync(Guid openRequestId,
                                                                                                      MatchToOpenRequestByOpenRequestIdDataLoader openRequestIdDataLoader,
                                                                                                      CancellationToken cancellationToken) => await openRequestIdDataLoader.LoadAsync(openRequestId, cancellationToken);

        public static async Task<IEnumerable<MatchToOpenRequest>> GetMatchToOpenByEmployeeIdAsync(Guid employeeId,
                                                                                                  MatchToOpenRequestByEmployeeIdDataLoader employeeIdDataLoader,
                                                                                                  CancellationToken cancellationToken) => await employeeIdDataLoader.LoadAsync(employeeId, cancellationToken);

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public static IQueryable<MatchToOpenRequest> GetMatchToOpenRequests(Demo3DbContext dbContext)
        {
            var data = dbContext.Set<MatchToOpenRequest>()
                .OrderBy(_ => _.ApplyForPosition.Value).AsNoTracking();
            return data;
        }
    }
}
