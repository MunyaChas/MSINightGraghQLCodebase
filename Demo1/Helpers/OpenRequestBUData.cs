using Bogus;
using Demo1.Data;
using Demo1.Enums;

namespace Demo1.Helpers
{
    public class OpenRequestBUData
    {
        public Faker<OpenRequestBUInputData> Faker { get; }
        public OpenRequestBUData()
        {
            Faker = new Faker<OpenRequestBUInputData>("nl").CustomInstantiator(f =>
            {
                var predefinedLineOfWork = Enum.GetNames(typeof(LineOfWork));
                var pickLineOfWork = f.PickRandom(predefinedLineOfWork);
                var predefinedSkills = PredefinedSkillsAndQualification.Skills.Where(_ => _.LineOfWork.ToString() == pickLineOfWork).ToList();
                var predefinedJobPosition = OpenRequestDetails.GenerateJobDescription().FirstOrDefault(_ => _.lineOfWork.ToString() == pickLineOfWork);
                var pickJobPosition = f.PickRandom(predefinedJobPosition?.jobTitle, 3);
                var jobSpec = string.Join(" ", pickJobPosition);
                var cluster = predefinedSkills.FirstOrDefault()?.Cluster.Id ?? Cluster.Other.Id;
                var competences = f.Random.ListItems(predefinedSkills).ToList();
                var roleStartDate = f.Date.Between(DateTime.Now.AddDays(14), DateTime.Now.AddMonths(2));

                return new OpenRequestBUInputData
                {
                    OpenRequestId = f.Random.Guid(),
                    TeamRequestId = f.Random.Guid(),
                    TeamRequestName = f.Company.CompanyName(),
                    PositionName = pickLineOfWork,
                    Cluster = Enumeration.FromValue<Cluster>(cluster),
                    PositionDescription = jobSpec,
                    Location = f.PickRandom(CityLists.NetherlandsCities),
                    NumberOfFTERequired = f.Random.Int(1, 10),
                    AccountManager = f.Person.FullName,
                    SkillLevel = f.PickRandom(Enumeration.GetAll<Level>()),
                    RoleStartDate = roleStartDate,
                    DeadLine = f.Date.Between(DateTime.UtcNow, roleStartDate.AddDays(-7)),
                    Competences = competences.ConvertAll(_ => new Competence(_.Name, f.Random.Int(1, 20)))
                };
            });
        }

        public OpenRequestBUInputData GetOpenRequestBUData()
        {
            return Faker.Generate();
        }

        public List<OpenRequestBUInputData> GetOpenRequestBUData(int count)
        {
            return Faker.Generate(count);
        }
    }

    internal class CompetenceData
    {
        public string SkillName { get; set; }
        public int YearsOfExperience { get; set; }
    }

    public class OpenRequestBUInputData
    {
        public Guid OpenRequestId { get; set; }
        public Guid TeamRequestId { get; set; }
        public string TeamRequestName { get; set; }
        public string PositionName { get; set; }
        public Cluster Cluster { get; set; }
        public string PositionDescription { get; set; }
        public string Location { get; set; }
        public int NumberOfFTERequired { get; set; }
        public string AccountManager { get; set; }
        public Level SkillLevel { get; set; }
        public DateTime RoleStartDate { get; set; }
        public DateTime DeadLine { get; set; }
        public List<Competence>? Competences { get; set; }
    }
    public class CityLists
    {
        public static List<string> NetherlandsCities =
        [
            "Amsterdam",
            "Rotterdam",
            "The Hague",
            "Utrecht",
            "Eindhoven",
            "Tilburg",
            "Groningen"
        ];

        public static List<string> BelgiumCities =
        [
            "Brussels",
            "Antwerp",
            "Ghent",
            "Bruges",
            "Leuven",
            "Liege",
            "Namur"
        ];

        public static List<string> GermanyCities =
        [
            "Berlin",
            "Munich",
            "Frankfurt",
            "Hamburg",
            "Cologne",
            "Stuttgart"
        ];
    }
}
