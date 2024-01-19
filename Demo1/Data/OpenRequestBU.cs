using CSharpFunctionalExtensions;

namespace Demo1.Data
{
    public sealed class OpenRequestBU
    {
        private readonly List<Competence> _competences = [];
        public OpenRequestId Id { get; private set; }
        public TeamRequestId TeamRequestId { get; private set; }
        public TeamRequestName TeamRequestName { get; private set; }
        public PositionName PositionName { get; private set; }
        public Department Cluster { get; private set; }
        public PositionDescription PositionDescription { get; private set; }
        public Location Location { get; private set; }
        public NumberOfFTERequired NumberOfFTERequired { get; private set; }
        public AccountManager AccountManager { get; private set; }
        public SkillLevel SkillLevel { get; private set; }
        public RoleStartDate RoleStartDate { get; private set; }
        public DeadLine DeadLine { get; private set; }
        public IReadOnlyList<Competence> Competences => _competences.AsReadOnly();
        public OpenRequestBU()
        {
        }
        private OpenRequestBU(OpenRequestId openRequestId, TeamRequestId teamRequestId, TeamRequestName teamRequestName, PositionName positionName, Department cluster, PositionDescription positionDescription, Location location, NumberOfFTERequired numberOfFTERequired, AccountManager accountManager, SkillLevel skillLevel, RoleStartDate roleStartDate, DeadLine deadLine, List<Competence>? competences = null)
        {
            Id = openRequestId;
            TeamRequestId = teamRequestId;
            TeamRequestName = teamRequestName;
            PositionName = positionName;
            Cluster = cluster;
            PositionDescription = positionDescription;
            Location = location;
            NumberOfFTERequired = numberOfFTERequired;
            AccountManager = accountManager;
            SkillLevel = skillLevel;
            RoleStartDate = roleStartDate;
            DeadLine = deadLine;
            _competences = competences ?? [];
        }

        public static OpenRequestBU Create(OpenRequestId openRequestId,
                                           TeamRequestId teamRequestId,
                                           TeamRequestName teamRequestName,
                                           PositionName positionName,
                                           Department cluster,
                                           PositionDescription positionDescription,
                                           Location location,
                                           NumberOfFTERequired numberOfFTERequired,
                                           AccountManager accountManager,
                                           SkillLevel skillLevel,
                                           RoleStartDate roleStartDate,
                                           DeadLine deadLine,
                                           List<Competence>? competences = null)
        {
            return new OpenRequestBU(openRequestId,
                                     teamRequestId,
                                     teamRequestName,
                                     positionName,
                                     cluster,
                                     positionDescription,
                                     location,
                                     numberOfFTERequired,
                                     accountManager,
                                     skillLevel,
                                     roleStartDate,
                                     deadLine,
                                     competences ?? []);
        }
        public void Update(TeamRequestName teamRequestName,
                           PositionName positionName,
                           Department cluster,
                           PositionDescription positionDescription,
                           Location location,
                           NumberOfFTERequired numberOfFTERequired,
                           AccountManager accountManager,
                           SkillLevel skillLevel,
                           RoleStartDate roleStartDate,
                           DeadLine deadLine,
                           List<Competence>? competences = null)
        {
            TeamRequestName = teamRequestName;
            PositionName = positionName;
            Cluster = cluster;
            PositionDescription = positionDescription;
            Location = location;
            NumberOfFTERequired = numberOfFTERequired;
            AccountManager = accountManager;
            SkillLevel = skillLevel;
            RoleStartDate = roleStartDate;
            DeadLine = deadLine;
            UpdateCollection(_competences, competences ?? []);
        }

        private void UpdateCollection<T>(List<T> existingItems, IEnumerable<T> newItems)
        {
            var existingItemsHashSet = existingItems.ToHashSet();
            var newItemsHashSet = newItems?.ToHashSet() ?? [];

            // Remove items that are in existingItems but not in newItems
            existingItems.RemoveAll(item => !newItemsHashSet.Contains(item));

            // Add items that are in newItems but not in existingItems
            existingItems.AddRange(newItemsHashSet.Where(item => !existingItemsHashSet.Contains(item)));
        }
    }
    public sealed record Department(string Value);
    //public sealed record SkillLevel(string Value);
    public sealed record Competence(string Value, int YearsOfExperience);
    public sealed record PositionName(string Value);
    public sealed record TeamRequestName(string Value);
    public sealed record PositionDescription(string Value);
    public sealed record Location(string Value);
    public sealed record NumberOfFTERequired(int Value);
    public sealed record AccountManager(string Value);
    public sealed record RoleStartDate(DateTime Value);
    public sealed record DeadLine(DateTime Value);
    public sealed class TeamRequestId : ValueObject
    {
        public Guid Value { get; private set; }
        private TeamRequestId() { }
        private TeamRequestId(Guid value)
        {
            Value = value;
        }
        public static TeamRequestId FromGuid(Guid? value = null) => new(value ?? Guid.NewGuid());
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
    public sealed class OpenRequestId : ValueObject
    {
        public Guid Value { get; private set; }
        private OpenRequestId() { }
        private OpenRequestId(Guid value)
        {
            Value = value;
        }
        public static OpenRequestId FromGuid(Guid? value = null) => new(value ?? Guid.NewGuid());
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
