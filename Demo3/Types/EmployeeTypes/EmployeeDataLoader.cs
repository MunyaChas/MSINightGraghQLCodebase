using Demo3.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo3.Types.EmployeeTypes
{
    public static class EmployeeDataLoader
    {
        [DataLoader]
        public async static Task<ILookup<string, Employee>> GetEmployeesByIdAsync(
                       IReadOnlyList<Guid> ids,
                                  Demo3DbContext demo2DbContext,
                                             CancellationToken cancellationToken)
        {
            var employees = await demo2DbContext.Set<Employee>()
                                          .Where(x => ids.Select(_ => EmployeeId.FromGuid(_)).Contains(x.Id))
                                          .ToListAsync(cancellationToken);
            return employees.ToLookup(_ => _.Name.First);
        }

        [DataLoader]
        public async static Task<IReadOnlyDictionary<Guid, Employee>> GetEmployeeById(IReadOnlyList<Guid> ids,
            Demo3DbContext demo2DbContext, CancellationToken cancellationToken)
        {
            var employees = await demo2DbContext.Set<Employee>()
                                          .Where(x => ids.Select(_ => EmployeeId.FromGuid(_)).Contains(x.Id))
                                          .ToListAsync(cancellationToken);
            return employees.ToDictionary(_ => _.Id.Value);
        }
    }
}
