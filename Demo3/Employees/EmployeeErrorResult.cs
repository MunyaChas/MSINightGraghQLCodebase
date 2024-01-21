namespace Demo3.Employees
{
    public sealed record EmployeeErrorResult(string Message)
    {
        public static EmployeeErrorResult Create(string message) => new(message);
    }
}
