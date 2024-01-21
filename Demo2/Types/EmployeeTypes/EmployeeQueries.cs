using Demo2.Data;

namespace Demo2.Types.EmployeeTypes
{
    [QueryType]
    public static class EmployeeQueries
    {
        public static async Task<Employee?> GetEmployeeById(Guid id, EmployeeByIdDataLoader dataLoader, CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync(id, cancellationToken);
        }
        [UsePaging]
        [UseFiltering]
        public static IQueryable<Employee> GetEmployees(Demo2DbContext demo2DbContext)
        {
            return demo2DbContext.Set<Employee>().OrderBy(_ => _.Name.First);
        }
    }
}
