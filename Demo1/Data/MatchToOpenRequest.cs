using CSharpFunctionalExtensions;

namespace Demo1.Data
{
    public sealed class MatchToOpenRequest
    {
        public MatchToOpenRequestId Id { get; private set; }
        public MatchScore MatchScore { get; private set; }
        public ApplyForPosition ApplyForPosition { get; private set; }
        public IsMatch IsMatch { get; private set; }
        public IsOpen IsOpen { get; private set; }
        public IsClosed IsClosed { get; private set; }
        public IsHired IsHired { get; private set; }
        public InterviewDate? InterviewDate { get; private set; }
        public IsWithdrawn IsWithdrawn { get; private set; }
        public EmployeeId EmployeeId { get; private set; }
        public OpenRequestId OpenRequestId { get; private set; }

        public Employee Employee { get; private set; }
        public OpenRequestBU OpenRequest { get; private set; }

        private MatchToOpenRequest() { }
        private MatchToOpenRequest(MatchToOpenRequestId id,
                                   MatchScore matchScore,
                                   ApplyForPosition applyForPosition,
                                   IsMatch isMatch,
                                   IsOpen isOpen,
                                   IsClosed isClosed,
                                   IsHired isHired,
                                   InterviewDate? interviewDate,
                                   IsWithdrawn isWithdrawn,
                                   EmployeeId employeeId,
                                   OpenRequestId openRequestId)
        {
            Id = id;
            MatchScore = matchScore;
            ApplyForPosition = applyForPosition;
            IsMatch = isMatch;
            IsOpen = isOpen;
            IsClosed = isClosed;
            IsHired = isHired;
            InterviewDate = interviewDate;
            IsWithdrawn = isWithdrawn;
            EmployeeId = employeeId;
            OpenRequestId = openRequestId;
        }
        public static MatchToOpenRequest Create(MatchToOpenRequestId id,
                                                MatchScore matchScore,
                                                ApplyForPosition applyForPosition,
                                                IsMatch isMatch,
                                                IsOpen isOpen,
                                                IsClosed isClosed,
                                                IsHired isHired,
                                                InterviewDate? interviewDate,
                                                IsWithdrawn isWithdrawn,
                                                EmployeeId employeeId,
                                                OpenRequestId openRequestId)
        {
            return new MatchToOpenRequest(id,
                                          matchScore,
                                          applyForPosition,
                                          isMatch,
                                          isOpen,
                                          isClosed,
                                          isHired,
                                          interviewDate,
                                          isWithdrawn,
                                          employeeId,
                                          openRequestId);
        }

        public void Update(MatchScore matchScore,
                           ApplyForPosition applyForPosition,
                           IsMatch isMatch,
                           IsOpen isOpen,
                           IsClosed isClosed,
                           IsHired isHired,
                           InterviewDate? interviewDate,
                           IsWithdrawn isWithdrawn,
                           EmployeeId employeeId,
                           OpenRequestId openRequestId)
        {
            MatchScore = matchScore;
            ApplyForPosition = applyForPosition;
            IsMatch = isMatch;
            IsOpen = isOpen;
            IsClosed = isClosed;
            IsHired = isHired;
            InterviewDate = interviewDate;
            IsWithdrawn = isWithdrawn;
            EmployeeId = employeeId;
            OpenRequestId = openRequestId;
        }
    }

    public sealed record MatchScore(int Value);
    public sealed record ApplyForPosition(bool Value);
    public sealed record IsMatch(bool Value);
    public sealed record IsOpen(bool Value);
    public sealed record IsClosed(bool Value);
    public sealed record IsHired(bool Value);
    public sealed record InterviewDate(DateTime? Value);
    public sealed record IsWithdrawn(bool Value);


    public sealed class MatchToOpenRequestId : ValueObject
    {
        public Guid Value { get; private set; }
        private MatchToOpenRequestId() { }
        private MatchToOpenRequestId(Guid value)
        {
            Value = value;
        }
        public static MatchToOpenRequestId FromGuid(Guid? value = null)
        {
            return new MatchToOpenRequestId(value ?? Guid.NewGuid());
        }
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
