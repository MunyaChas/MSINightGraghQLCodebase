using Bogus;
using Demo1.Enums;

namespace Demo1.Helpers
{
    public class EmployeeData
    {
        Faker<EmployeeInputData> Faker { get; }
        public EmployeeData()
        {
            Faker = new Faker<EmployeeInputData>("nl").CustomInstantiator(f =>
            {
                var predefinedLineOfWork = Enum.GetNames(typeof(LineOfWork));
                var pickLineOfWork = f.PickRandom(predefinedLineOfWork);
                var predefinedSkillsCount = PredefinedSkillsAndQualification.Skills.ToList().Count(_ => _.LineOfWork.ToString() == pickLineOfWork);
                var firstName = f.Person.FirstName;
                var lastName = f.Person.LastName;
                return new EmployeeInputData
                {
                    Id = f.Random.Guid(),
                    FirstName = firstName,
                    LastName = lastName,
                    Email = f.Internet.Email(firstName, lastName, "capgemini.com"),
                    Phone = f.Phone.PhoneNumber("+##-####-######"),
                    EmployeeCode = f.Random.AlphaNumeric(10),
                    Qualifications = new QualificationBogusData(f.PickRandom(pickLineOfWork)).GetQualificationData(1),
                    SkillsMatrices = new SkillsMatrixData(f.PickRandom(pickLineOfWork)).GetSkillsMatrixData(f.Random.Int(1, predefinedSkillsCount))
                };
            });
        }

        public EmployeeInputData GetEmployeeData()
        {
            return Faker.Generate();
        }

        public List<EmployeeInputData> GetEmployeeData(int count)
        {
            return Faker.Generate(count);
        }
    }

    public class SkillsMatrixData
    {
        private readonly List<string> _availableSkills;
        Faker<SkillsMatrixInputData> Faker { get; }

        public SkillsMatrixData(string lineOfWork)
        {
            _availableSkills = PredefinedSkillsAndQualification.Skills
                                .Where(_ => _.LineOfWork.ToString() == lineOfWork)
                                .Select(_ => _.Name)
                                .ToList();
            var predefinedSkills = PredefinedSkillsAndQualification.Skills.Where(_ => _.LineOfWork.ToString() == lineOfWork);
            var pickSkillLevel = new Func<Level>(() =>
            {
                var skillLevel = Enumeration.GetAll<Level>().ToList();
                return skillLevel[new Random().Next(0, skillLevel.Count)];
            });
            Faker = new Faker<SkillsMatrixInputData>()
                .RuleFor(_ => _.SkillsMatrixId, f => f.Random.Guid())
                .RuleFor(_ => _.Skill, f => PickUniqueSkill(f))
                .RuleFor(_ => _.SkillLevel, f => f.PickRandom(Enumeration.GetAll<Level>()))
                .RuleFor(_ => _.YearsOfExperience, f => f.Random.Int(1, 40));
        }
        public SkillsMatrixInputData GetSkillsMatrixData()
        {
            return Faker.Generate();
        }
        public List<SkillsMatrixInputData> GetSkillsMatrixData(int count)
        {
            return Faker.Generate(count);
        }
        private string PickUniqueSkill(Faker f)
        {
            if (!_availableSkills.Any())
            {
                _availableSkills.AddRange(PredefinedSkillsAndQualification.Skills.Where(_ => _.LineOfWork == LineOfWork.ProjectManagement).Select(_ => _.Name).ToList());
            }

            var skill = f.PickRandom(_availableSkills);
            _availableSkills.Remove(skill);
            return skill;
        }
    }

    public class QualificationBogusData
    {
        Faker<QualificationInputData> Faker { get; }
        public QualificationBogusData(string lineOfWork)
        {
            var predefinedQualifications = PredefinedSkillsAndQualification.Qualifications.Where(_ => _.LineOfWork.ToString() == lineOfWork);
            Faker = new Faker<QualificationInputData>()
                    .CustomInstantiator(f =>
                    {
                        var predefinedQualification = f.PickRandom(predefinedQualifications);
                        return new QualificationInputData
                        {
                            QualificationId = f.Random.Guid(),
                            NameOfQualification = predefinedQualification?.Name,
                            Institute = predefinedQualification?.Institute,
                            YearCompleted = f.Random.Int(1996, 2023)
                        };
                    });
        }

        public QualificationInputData GetQualificationData()
        {
            return Faker.Generate();
        }

        public List<QualificationInputData> GetQualificationData(int count)
        {
            return Faker.Generate(count);
        }
    }

    public class EmployeeInputData
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? EmployeeCode { get; set; }
        public List<SkillsMatrixInputData> SkillsMatrices { get; set; } = [];
        public List<QualificationInputData> Qualifications { get; set; } = [];
    }

    public class QualificationInputData
    {
        public Guid? QualificationId { get; set; }
        public string? NameOfQualification { get; set; }
        public string? Institute { get; set; }
        public int YearCompleted { get; set; }
    }

    public class SkillsMatrixInputData
    {
        public Guid? SkillsMatrixId { get; set; }
        public string? Skill { get; set; }
        public Level SkillLevel { get; set; }
        public int YearsOfExperience { get; set; }
    }

    public class PredefinedSkillsAndQualification
    {
        private static readonly List<QualificationData> _qualifications =
        [
            new QualificationData
            {
                Name = "Master of Business Administration",
                Institute = "University of the East",
                LineOfWork = LineOfWork.ProjectManagement
            },
            new QualificationData
            {
                Name = "Master of Science in Information Technology",
                Institute = "University of the South",
                LineOfWork = LineOfWork.BusinessAnalyst
            },
            new QualificationData
            {
                Name = "Master of Science in Data Science",
                Institute = "University of the West",
                LineOfWork = LineOfWork.BusinessAnalyst
            },
            new QualificationData
            {
                Name = "Ph.D. in Artificial Intelligence",
                Institute = "University of the North",
                LineOfWork = LineOfWork.ArtificialIntelligence
            },
            new QualificationData
            {
                Name = "Business Analyst",
                Institute = "University of the SouthEast",
                LineOfWork = LineOfWork.BusinessAnalyst
            },
            new QualificationData
            {
                Name = "Bachelor of Science in Computer Science",
                Institute = "University of the NothEast",
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new QualificationData
            {
                Name = "Master of Engineering",
                Institute = "University of the East",
                LineOfWork = LineOfWork.Engineering
            },
            new QualificationData
            {
                Name = "Master of Science in Information Security",
                Institute = "University of the East",
                LineOfWork = LineOfWork.ITSecurity
            },
            new QualificationData
            {
                Name = "Science in Cloud Computing",
                Institute = "University of the East",
                LineOfWork = LineOfWork.Cloud
            },
            new QualificationData
            {
                Name = "Microsoft Azure DevOps Engineer",
                Institute = "Microsoft",
                LineOfWork = LineOfWork.DevOps
            },
            new QualificationData
            {
                Name = "AWS Certified DevOps Engineer Professional",
                Institute = "Aws",
                LineOfWork = LineOfWork.DevOps
            },
            new QualificationData
            {
                Name = "AWS Certified Solutions Architect Professional",
                Institute = "Aws",
                LineOfWork = LineOfWork.Cloud
            },
            new QualificationData
            {
                Name = "Data Science BSc",
                Institute = "University of the East",
                LineOfWork = LineOfWork.DataScience
            },
        ];

        private static readonly List<SkillData> _skills =
        [
            new SkillData
            {
                Name = "C#",
                Cluster = Cluster.CCADotNet,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "ASP.NET Core",
                Cluster = Cluster.CCADotNet,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "ASP.NET MVC",
                Cluster = Cluster.CCADotNet,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "ASP.NET Web API",
                Cluster = Cluster.CCADotNet,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Entity Framework Core",
                Cluster = Cluster.CCADotNet,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "SQL Server",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Azure",
                Cluster = Cluster.Cloud,
                LineOfWork = LineOfWork.Cloud
            },
            new SkillData
            {
                Name = "Aws",
                Cluster = Cluster.Cloud,
                LineOfWork = LineOfWork.Cloud
            },
            new SkillData
            {
                Name = "Azure DevOps",
                Cluster = Cluster.DevOps,
                LineOfWork = LineOfWork.DevOps
            },
            new SkillData
            {
                Name = "Git",
                Cluster = Cluster.DevOps,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Docker",
                Cluster = Cluster.DevOps,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Kubernetes",
                Cluster = Cluster.DevOps,
                LineOfWork = LineOfWork.DevOps
            },
            new SkillData
            {
                Name = "React",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Angular",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Vue",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "JavaScript",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "TypeScript",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "HTML",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "CSS",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "SASS",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "LESS",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Bootstrap",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Material UI",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "JQuery",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Node.js",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Webpack",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "Gulp",
                Cluster = Cluster.Frontend,
                LineOfWork = LineOfWork.SoftwareDevelopment
            },
            new SkillData
            {
                Name = "AI",
                Cluster = Cluster.AI,
                LineOfWork = LineOfWork.ArtificialIntelligence
            },
            new SkillData
            {
                Name = "Machine Learning",
                Cluster = Cluster.AI,
                LineOfWork = LineOfWork.ArtificialIntelligence
            },
            new SkillData
            {
                Name = "Python",
                Cluster = Cluster.AI,
                LineOfWork = LineOfWork.ArtificialIntelligence
            },
            new SkillData
            {
                Name = "Power BI",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Tableau",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Data Science",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Data Engineering",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Data Analytics",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Data Warehousing",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Data Modelling",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Data Mining",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Data Visualization",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "ETL",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Big Data",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Hadoop",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Spark",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Scala",
                Cluster = Cluster.Data,
                LineOfWork = LineOfWork.DataScience
            },
            new SkillData
            {
                Name = "Project Manager",
                Cluster = Cluster.ProjectManagement,
                LineOfWork = LineOfWork.ProjectManagement
            },
            new SkillData
            {
                Name = "Industrial Engineering",
                Cluster = Cluster.Engineering,
                LineOfWork = LineOfWork.Engineering
            },
            new SkillData
            {
                Name = "Mechanical Engineering",
                Cluster = Cluster.Engineering,
                LineOfWork = LineOfWork.Engineering
            },
            new SkillData
            {
                Name = "Electrical Engineering",
                Cluster = Cluster.Engineering,
                LineOfWork = LineOfWork.Engineering
            },
            new SkillData
            {
                Name = "Civil Engineering",
                Cluster = Cluster.Engineering,
                LineOfWork = LineOfWork.Engineering
            },
            new SkillData
            {
                Name = "Chemical Engineering",
                Cluster = Cluster.Engineering,
                LineOfWork = LineOfWork.Engineering
            },
            new SkillData
            {
                Name = "Cloud security",
                Cluster = Cluster.Security,
                LineOfWork = LineOfWork.ITSecurity
            },
            new SkillData
            {
                Name = "Network security",
                Cluster = Cluster.Security,
                LineOfWork = LineOfWork.ITSecurity
            },
            new SkillData
            {
                Name = "Application security",
                Cluster = Cluster.Security,
                LineOfWork = LineOfWork.ITSecurity
            },
            new SkillData
            {
                Name = "Information security",
                Cluster = Cluster.Security,
                LineOfWork = LineOfWork.ITSecurity
            },
            new SkillData
            {
                Name = "Security architecture",
                Cluster = Cluster.Security,
                LineOfWork = LineOfWork.ITSecurity
            },
            new SkillData
            {
                Name = "Security operations",
                Cluster = Cluster.Security,
                LineOfWork = LineOfWork.ITSecurity
            },
            new SkillData
            {
                Name = "Security engineering",
                Cluster = Cluster.Security,
                LineOfWork = LineOfWork.ITSecurity
            },
            new SkillData
            {
                Name = "Business Analyst",
                Cluster = Cluster.ProjectManagement,
                LineOfWork = LineOfWork.BusinessAnalyst
            },
            new SkillData
            {
                Name = "Associate Business Analyst",
                Cluster = Cluster.ProjectManagement,
                LineOfWork = LineOfWork.BusinessAnalyst
            },

        ];

        public static IReadOnlyList<SkillData> Skills => _skills.ToList();

        public static IReadOnlyList<QualificationData> Qualifications => _qualifications.ToList();
    }

    public class SkillData
    {
        public string Name { get; set; }
        public Cluster Cluster { get; set; }
        public Level SkillLevel { get; set; }
        public LineOfWork LineOfWork { get; set; }
    }

    public class QualificationData
    {
        public string? Name { get; set; }
        public string? Institute { get; set; }
        public LineOfWork LineOfWork { get; set; }
    }

    public enum LineOfWork
    {
        SoftwareDevelopment,
        DataScience,
        BusinessAnalyst,
        ProjectManagement,
        ArtificialIntelligence,
        Cloud,
        Engineering,
        DevOps,
        ITSecurity
    }

    public sealed record JobSpec(LineOfWork lineOfWork, List<string> jobTitle);

    public class OpenRequestDetails
    {
        public static List<JobSpec> GenerateJobDescription()
        {
            var jobDescriptions = new List<JobSpec>
            {
                new(LineOfWork.SoftwareDevelopment,
                [
                    "Develop and maintain software applications.",
                    "Collaborate with cross-functional teams.",
                    "Write clean, scalable code using .NET programming languages.",
                    "Test and deploy applications and systems.",
                    "Revise, update, refactor and debug code.",
                    "Improve existing software.",
                    "Develop documentation throughout the software development life cycle (SDLC).",
                ]),
                new(LineOfWork.BusinessAnalyst,
                [
                    "Analyze business processes and requirements.",
                    "Facilitate communication between stakeholders and development teams.",
                    "Create and validate business cases for technological improvements.",
                    "Develop and implement solutions according to business needs.",
                    "Manage and monitor projects, ensuring they are delivered on time and within budget.",
                    "Identify and resolve issues and risks.",
                    "Provide support and training to end users."
                ]),
               new(LineOfWork.DataScience,
                [
                    "Analyze large, complex data sets to derive actionable insights.",
                    "Develop predictive models and machine-learning algorithms.",
                    "Present findings to stakeholders in a clear and effective manner.",
                    "Collaborate with data engineers to develop data pipelines and data products.",
                    "Identify and implement process improvements.",
                    "Develop and implement data governance policies.",
                ]),
                new(LineOfWork.ProjectManagement,
                [
                    "Plan, execute, and close projects successfully.",
                    "Lead and manage project teams.",
                    "Ensure projects are delivered on time, within scope, and budget.",
                    "Identify and manage risks and issues.",
                    "Develop and implement project management processes and best practices.",
                    "Provide project status reports to stakeholders.",
                    "Manage and resolve conflicts within project teams.",
                ]),
                new(LineOfWork.ArtificialIntelligence,
                [
                    "Design and develop AI models and algorithms.",
                    "Collaborate with data scientists and engineers.",
                    "Implement machine learning applications according to requirements.",
                    "Perform statistical analysis and fine-tune models.",
                    "Train and retrain systems when necessary.",
                    "Extend existing ML libraries and frameworks.",
                    "Research and implement appropriate ML algorithms and tools."
                ]),
                new(LineOfWork.Cloud,
                [
                    "Design and deploy cloud-based solutions and infrastructure.",
                    "Ensure cloud environments are secure and compliant.",
                    "Manage and optimize cloud resources and services.",
                    "Collaborate with software engineers to deploy and operate cloud-based systems.",
                    "Automate and streamline cloud operations and processes.",
                    "Monitor and optimize cloud performance.",
                    "Troubleshoot and resolve issues in cloud environments."
                ]),
                new(LineOfWork.DevOps,
                [
                    "Implement and manage continuous delivery systems.",
                    "Collaborate with software developers for seamless code deployment.",
                    "Automate and optimize operations and processes.",
                    "Build and maintain tools for deployment, monitoring, and operations.",
                    "Troubleshoot and resolve issues in development, test, and production environments.",
                    "Ensure security and compliance requirements are met.",
                    "Manage and monitor infrastructure and applications."
                ]),
                new(LineOfWork.ITSecurity,
                [
                    "Develop and implement security frameworks for IT systems.",
                    "Monitor for security breaches and respond to incidents.",
                    "Ensure compliance with security regulations and policies.",
                    "Perform vulnerability testing and risk analyses.",
                    "Develop and implement security policies and procedures.",
                    "Install and configure security systems and tools.",
                    "Train and educate staff on security best practices."
                ]),
                new(LineOfWork.Engineering,
                [
                    "Manage and lead engineering teams.",
                    "Develop and implement engineering strategies and processes.",
                    "Ensure engineering projects are delivered on time and within budget.",
                    "Identify and resolve issues and risks.",
                    "Provide technical guidance and coaching to engineers.",
                    "Develop and implement engineering policies and best practices.",
                    "Manage and resolve conflicts within engineering teams."
                ])
            };

            return jobDescriptions;
        }
    }
}
