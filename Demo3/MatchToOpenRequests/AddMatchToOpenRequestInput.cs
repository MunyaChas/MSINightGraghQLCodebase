using Demo3.Data;

namespace Demo3.MatchToOpenRequests
{
    public sealed record AddMatchToOpenRequestInput(
            [ID(nameof(OpenRequestBU))] OpenRequestId OpenRequestId,
            [ID(nameof(Employee))] EmployeeId EmployeeId,
            int MatchScore,
            bool ApplyForPosition,
            bool IsMatch,
            bool IsOpen,
            bool IsClosed,
            bool IsHired,
            DateTime? InterviewDate,
            bool IsWithdrawn
            );
}
