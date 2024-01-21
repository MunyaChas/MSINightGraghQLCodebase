using CSharpFunctionalExtensions;

namespace Demo2.Data
{
    public sealed class SkillsMatrix
    {
        public SkillsMatrixId SkillsMatrixId { get; private set; }
        public Skill Skill { get; private set; }
        public SkillLevel? SkillLevel { get; private set; }
        public YearOfExperience YearsOfExperience { get; private set; }
        private SkillsMatrix() { }
        private SkillsMatrix(SkillsMatrixId skillsMatrixId,
                             Skill skill,
                             SkillLevel? skillLevel,
                             YearOfExperience yearOfExperience)
        {
            SkillsMatrixId = skillsMatrixId;
            Skill = skill;
            SkillLevel = skillLevel;
            YearsOfExperience = yearOfExperience;
        }
        public static SkillsMatrix Create(SkillsMatrixId skillsMatrixId,
                                          Skill skill,
                                          SkillLevel? skillLevel,
                                          YearOfExperience yearOfExperience)
        {
            return new SkillsMatrix(skillsMatrixId, skill, skillLevel, yearOfExperience);
        }
        public void Update(Skill skill,
                           SkillLevel? skillLevel,
                           YearOfExperience yearOfExperience)
        {
            Skill = skill;
            SkillLevel = skillLevel;
            YearsOfExperience = yearOfExperience;
        }
    }

    public sealed record SkillLevel(string? Value);

    public class SkillsMatrixId : ValueObject
    {
        public Guid Value { get; private set; }
        private SkillsMatrixId() { }
        private SkillsMatrixId(Guid value)
        {
            Value = value;
        }
        public static SkillsMatrixId FromGuid(Guid? value = null)
        {
            return new SkillsMatrixId(value ?? Guid.NewGuid());
        }
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }

    public sealed record YearOfExperience(int? Value);

    public class Skill : ValueObject
    {
        public string Value { get; private set; }
        private Skill() { }
        private Skill(string value)
        {
            Value = value;
        }
        public static CSharpFunctionalExtensions.Result<Skill> Create(string? skill = null)
        {
            if (string.IsNullOrWhiteSpace(skill))
            {
                return Result.Failure<Skill>("Skill should not be empty");
            }
            return new Skill(skill);
        }
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}