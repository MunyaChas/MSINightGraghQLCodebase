using CSharpFunctionalExtensions;

namespace Demo3.Data
{
    public sealed record Qualification
    {
        public QualificationId QualificationId { get; set; }
        public NameOfQualification NameOfQualification { get; private set; }
        public Institute Institute { get; private set; }
        public Year YearCompleted { get; private set; }
        public Qualification() { }
        public static Qualification Create(QualificationId qualificationId, NameOfQualification nameOfQualification, Institute institute, Year yearCompleted)
        {
            return new Qualification
            {
                QualificationId = qualificationId,
                NameOfQualification = nameOfQualification,
                Institute = institute,
                YearCompleted = yearCompleted
            };
        }
        public void Update(NameOfQualification nameOfQualification, Institute institute, Year yearCompleted)
        {
            NameOfQualification = nameOfQualification;
            Institute = institute;
            YearCompleted = yearCompleted;
        }
    }

    public sealed record Institute(string Value);
    public sealed record NameOfQualification(string Value);
    public sealed record Year(int? Value);

    public sealed class QualificationId : ValueObject
    {
        public Guid Value { get; private set; }
        private QualificationId() { }
        private QualificationId(Guid value)
        {
            Value = value;
        }
        public static QualificationId FromGuid(Guid? value = null) => new(value ?? Guid.NewGuid());
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}