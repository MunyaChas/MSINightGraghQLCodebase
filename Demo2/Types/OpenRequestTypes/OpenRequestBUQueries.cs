using Demo2.Data;

namespace Demo2.Types.OpenRequestTypes
{
    [QueryType]
    public static class OpenRequestBUQueries
    {
        [NodeResolver]
        public static async Task<OpenRequestBU?> GetOpenRequestById(Guid id,
                                                         OpenRequestBUsByIdsDataLoader openRequestBUDataLoader,
                                                         CancellationToken cancellationToken) => await openRequestBUDataLoader.LoadAsync(id, cancellationToken);

        public static async Task<IEnumerable<OpenRequestBU?>> GetOpenRequestByPositionNameAsync(string openPositionName,
                                                                                                OpenRequestByPositionNameDataLoader openRequestByPosition,
                                                                                                CancellationToken cancellationToken) => await openRequestByPosition.LoadAsync(openPositionName, cancellationToken);

        [UsePaging]
        // [UseProjection]
        [UseFiltering]
        //[UseSorting]
        public static IQueryable<OpenRequestBU> GetOpenRequestBUs(Demo2DbContext dbContext)
        {
            var data = dbContext.Set<OpenRequestBU>()
                .OrderBy(_ => _.DeadLine.Value);
            return data;
        }
    }
}
