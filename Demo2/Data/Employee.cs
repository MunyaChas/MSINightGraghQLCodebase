using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace Demo2.Data
{
    public sealed class Employee
    {
        private readonly List<SkillsMatrix> _skillsMatrices = [];
        private readonly List<Qualification> _qualifications = [];
        public EmployeeId Id { get; private set; }
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public EmployeeCode EmployeeCode { get; private set; }
        public IReadOnlyList<SkillsMatrix> SkillsMatrices => _skillsMatrices.AsReadOnly();
        public IReadOnlyList<Qualification> Qualifications => _qualifications.AsReadOnly();
        private Employee() { }
        private Employee(EmployeeId id, Name name, Email email, Phone phone, EmployeeCode employeeCode, List<SkillsMatrix>? skillsMatrices = null, List<Qualification>? qualifications = null)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            EmployeeCode = employeeCode;
            _skillsMatrices = skillsMatrices ?? [];
            _qualifications = qualifications ?? [];
        }
        public static Employee Create(EmployeeId id, Name name, Email email, Phone phone, EmployeeCode employeeCode, List<SkillsMatrix>? skillsMatrices = null, List<Qualification>? qualifications = null)
        {
            return new Employee(id, name, email, phone, employeeCode, skillsMatrices ?? [], qualifications ?? []);
        }
        public void Update(Name name, Email email, Phone phone, List<SkillsMatrix>? skillsMatrices = null, List<Qualification>? qualifications = null)
        {
            Name = name;
            Email = email;
            Phone = phone;
            UpdateCollection(_skillsMatrices, skillsMatrices ?? []);
            UpdateCollection(_qualifications, qualifications ?? []);
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

    public sealed class EmployeeCode : ValueObject
    {
        public string Value { get; }
        private EmployeeCode(string value)
        {
            Value = value;
        }
        public static CSharpFunctionalExtensions.Result<EmployeeCode> Create(string? employeeCode = null)
        {
            if (string.IsNullOrWhiteSpace(employeeCode))
            {
                employeeCode = GenerateUniqueCode().Value;
            }
            return Result.Success(new EmployeeCode(employeeCode));
        }
        public static EmployeeCode GenerateUniqueCode()
        {
            var random = new Random();
            var prefix = new char[3];
            for (int i = 0; i < prefix.Length; i++)
            {
                prefix[i] = (char)('A' + random.Next(26)); // Random letter from A to Z
            }

            var uniquePart = Guid.NewGuid().ToString("N").Substring(0, 7); // 7-digit unique identifier

            return new EmployeeCode(new string(prefix) + uniquePart);
        }
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
        public static implicit operator string(EmployeeCode employeeCode)
        {
            return employeeCode.Value;
        }
    }

    public sealed class Phone : ValueObject
    {
        public string Value { get; }
        private Phone(string value)
        {
            Value = value;
        }
        public static CSharpFunctionalExtensions.Result<Phone> Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Result.Failure<Phone>("Phone should not be empty");
            phone = phone.Trim();

            if (phone.Length > 15)
                return Result.Failure<Phone>("Phone is too long");

            if (!Regex.IsMatch(phone, @"^\+?\d{0,2}\-?\d{4,5}\-?\d{5,6}$"))
                return Result.Failure<Phone>("Phone is invalid");

            return Result.Success(new Phone(phone));

        }
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
        public static implicit operator string(Phone phone)
        {
            return phone.Value;
        }
    }

    public sealed class EmployeeId : ValueObject
    {
        public Guid Value { get; }
        private EmployeeId(Guid value)
        {
            Value = value;
        }
        public static EmployeeId FromGuid(Guid value)
        {
            return new(value);
        }
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }

    public class Email : ValueObject
    {
        public string Value { get; }
        private Email(string value)
        {
            Value = value;
        }
        public static CSharpFunctionalExtensions.Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<Email>("Email should not be empty");
            email = email.Trim();

            if (email.Length > 200)
                return Result.Failure<Email>("Email is too long");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Result.Failure<Email>("Email is invalid");

            return Result.Success(new Email(email));

        }
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
    public class Name : ValueObject
    {
        public string First { get; }
        public string Last { get; }

        protected Name()
        {
        }

        private Name(string first, string last)
            : this()
        {
            First = first;
            Last = last;
        }

        public static CSharpFunctionalExtensions.Result<Name> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<Name>("First name should not be empty");
            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<Name>("Last name should not be empty");

            firstName = firstName.Trim();
            lastName = lastName.Trim();

            if (firstName.Length > 50)
                return Result.Failure<Name>("First name is too long");
            if (lastName.Length > 50)
                return Result.Failure<Name>("Last name is too long");

            return Result.Success(new Name(firstName, lastName));
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return First;
            yield return Last;
        }
        override public string ToString()
        {
            return $"{First} {Last}";
        }
    }
}
