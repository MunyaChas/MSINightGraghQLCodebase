﻿using Demo3.Data;

namespace Demo3.Types.EmployeeTypes
{
    [QueryType]
    public static class EmployeeQueries
    {
        [NodeResolver]
        public static async Task<Employee?> GetEmployeeById(Guid id, EmployeeByIdDataLoader dataLoader, CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync(id, cancellationToken);
        }
        [UsePaging]
        [UseFiltering]
        public static IQueryable<Employee> GetEmployees(Demo3DbContext demo2DbContext)
        {
            return demo2DbContext.Set<Employee>().OrderBy(_ => _.Name.First);
        }
    }
}
