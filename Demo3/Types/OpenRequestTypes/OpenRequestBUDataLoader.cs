﻿using Demo3.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo3.Types.OpenRequestTypes
{
    public static class OpenRequestBUDataLoader
    {
        [DataLoader]
        public static async Task<IReadOnlyDictionary<Guid, OpenRequestBU>> GetOpenRequestBUsByIdsAsync(IReadOnlyList<Guid> ids,
                                                                                                       Demo3DbContext context,
                                                                                                       CancellationToken cancellationToken)
        {
            var openRequestBUs = await context.Set<OpenRequestBU>()
                                              .Where(openRequestBU => ids.Select(_ => OpenRequestId.FromGuid(_)).Contains(openRequestBU.Id))
                                              .ToListAsync(cancellationToken);
            return openRequestBUs.ToDictionary(openRequestBU => openRequestBU.Id.Value);
        }
        [DataLoader]
        public static async Task<ILookup<string, OpenRequestBU>> GetOpenRequestByPositionName(IReadOnlyList<string> openRequestNames,
                                                                                              Demo3DbContext context,
                                                                                              CancellationToken cancellationToken)
        {
            var openRequestBUs = await context.Set<OpenRequestBU>()
                .Where(openRequestBU => openRequestNames.Contains(openRequestBU.PositionName.Value))
                .ToListAsync(cancellationToken);
            return openRequestBUs.ToLookup(openRequestBU => openRequestBU.PositionName.Value);
        }
    }
}
