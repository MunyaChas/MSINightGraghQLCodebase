using Microsoft.EntityFrameworkCore;

namespace Demo1.Data
{
    public sealed class Demo1DbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly bool _consoleLogger;

        public Demo1DbContext(DbContextOptions<Demo1DbContext> options) : base(options)
        {
        }
        //public MSNightDbContext(string connectionString, bool consoleLogger)
        //{
        //    _connectionString = connectionString;
        //    _consoleLogger = consoleLogger;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //    if (_consoleLogger)
        //    {
        //        optionsBuilder.UseLoggerFactory(GetLoggerFactory()).EnableSensitiveDataLogging();
        //    }
        //    else
        //    {
        //        optionsBuilder.UseLoggerFactory(CreateEmptyLoggerFactory());
        //    }
        //}
        //private static ILoggerFactory CreateEmptyLoggerFactory() => LoggerFactory.Create(builder => builder.AddFilter((_, _) => false));
        //private static ILoggerFactory GetLoggerFactory() =>
        //    LoggerFactory.Create(builder =>
        //    {
        //        builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole();
        //    });
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Id).ValueGeneratedNever().HasConversion(_ => _.Value, _ => EmployeeId.FromGuid(_)).HasColumnName("EmployeeId");
                entity.OwnsOne(_ => _.Name, name =>
                {
                    name.Property(_ => _.First).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
                    name.Property(_ => _.Last).HasColumnName("LastName").HasMaxLength(50).IsRequired();
                });
                entity.OwnsOne(_ => _.Email, email =>
                {
                    email.Property(_ => _.Value).HasColumnName("Email").HasMaxLength(50).IsRequired();
                });
                entity.OwnsOne(_ => _.Phone, phone =>
                {
                    phone.Property(__ => __.Value).HasColumnName("Phone").HasMaxLength(50).IsRequired();
                });
                entity.OwnsOne(_ => _.EmployeeCode, employeeCode =>
                {
                    employeeCode.Property(_ => _.Value).HasColumnName("EmployeeCode").HasMaxLength(50).IsRequired();
                });
                entity.Metadata.FindNavigation(nameof(Employee.Qualifications))!.SetPropertyAccessMode(PropertyAccessMode.Field);
                entity.Metadata.FindNavigation(nameof(Employee.SkillsMatrices))!.SetPropertyAccessMode(PropertyAccessMode.Field);
                entity.OwnsMany(entity => entity.Qualifications, qualification =>
                {
                    qualification.ToTable("Qualifications");
                    qualification.HasKey(_ => _.QualificationId);
                    qualification.Property(_ => _.QualificationId).ValueGeneratedNever().HasConversion(__ => __.Value, __ => QualificationId.FromGuid(__)).HasColumnName("QualificationId");
                    qualification.OwnsOne(_ => _.NameOfQualification, nameOfQualification =>
                    {
                        nameOfQualification.Property(_ => _.Value).HasColumnName("NameOfQualification").HasMaxLength(50).IsRequired();
                    });
                    qualification.OwnsOne(_ => _.Institute, institute =>
                    {
                        institute.Property(__ => __.Value).HasColumnName("Institute").HasMaxLength(50).IsRequired();
                    });
                    qualification.OwnsOne(_ => _.YearCompleted, yearCompleted =>
                    {
                        yearCompleted.Property(__ => __.Value).HasColumnName("YearCompleted").IsRequired();
                    });
                });
                entity.OwnsMany(entity => entity.SkillsMatrices, matrix =>
                {
                    matrix.ToTable("SkillsMatrices");
                    matrix.HasKey(_ => _.SkillsMatrixId);
                    matrix.Property(_ => _.SkillsMatrixId).ValueGeneratedNever().HasConversion(__ => __.Value, __ => SkillsMatrixId.FromGuid(__)).HasColumnName("SkillsMatrixId");
                    matrix.OwnsOne(_ => _.Skill, skill =>
                    {
                        skill.Property(_ => _.Value).HasColumnName("Skill").HasMaxLength(50).IsRequired();
                    });

                    matrix.OwnsOne(_ => _.SkillLevel, skill =>
                    {
                        skill.Property(_ => _.Value).HasColumnName("SkillLevel").IsRequired();
                    });
                    matrix.OwnsOne(_ => _.YearsOfExperience, yearsOfExperience =>
                    {
                        yearsOfExperience.Property(__ => __.Value).HasColumnName("YearsOfExperience").IsRequired();
                    });
                });
            });

            modelBuilder.Entity<OpenRequestBU>(entity =>
            {
                entity.ToTable("OpenRequestBUs");
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Id).ValueGeneratedNever().HasConversion(
                    __ => __.Value, __ => OpenRequestId.FromGuid(__)).HasColumnName("OpenRequestId");
                entity.OwnsOne(_ => _.TeamRequestId, teamRequestId =>
                {
                    teamRequestId.Property(_ => _.Value).HasColumnName("TeamRequestId").IsRequired();
                });
                entity.OwnsOne(_ => _.TeamRequestName, teamRequestName =>
                {
                    teamRequestName.Property(__ => __.Value).HasColumnName("TeamRequestName").HasMaxLength(50).IsRequired();
                });
                entity.OwnsOne(_ => _.PositionName, positionName =>
                {
                    positionName.Property(__ => __.Value).HasColumnName("PositionName").HasMaxLength(50).IsRequired();
                });
                entity.OwnsOne(_ => _.Cluster, cluster =>
                {
                    cluster.Property(__ => __.Value).HasColumnName("Cluster").HasMaxLength(50).IsRequired();
                });
                entity.OwnsOne(_ => _.PositionDescription, positionDescription =>
                {
                    positionDescription.Property(__ => __.Value).HasColumnName("PositionDescription").HasMaxLength(500).IsRequired();
                });
                entity.OwnsOne(_ => _.Location, location =>
                {
                    location.Property(__ => __.Value).HasColumnName("Location").HasMaxLength(50).IsRequired();
                });
                entity.OwnsOne(_ => _.NumberOfFTERequired, numberOfFTERequired =>
                {
                    numberOfFTERequired.Property(__ => __.Value).HasColumnName("NumberOfFTERequired").IsRequired();
                });
                entity.OwnsOne(_ => _.AccountManager, accountManager =>
                {
                    accountManager.Property(__ => __.Value).HasColumnName("AccountManager").HasMaxLength(50).IsRequired();
                });
                entity.OwnsOne(_ => _.SkillLevel, skillLevel =>
                {
                    skillLevel.Property(__ => __.Value).HasColumnName("SkillLevel").HasMaxLength(50).IsRequired();
                });
                entity.OwnsOne(_ => _.RoleStartDate, roleStartDate =>
                {
                    roleStartDate.Property(__ => __.Value).HasColumnName("RoleStartDate").IsRequired();
                });
                entity.OwnsOne(_ => _.DeadLine, deadLine =>
                {
                    deadLine.Property(__ => __.Value).HasColumnName("DeadLine").IsRequired();
                });
                entity.OwnsMany(_ => _.Competences, competences =>
                {
                    competences.Property(__ => __.Value).HasColumnName("Competence").IsRequired();
                    competences.Property(__ => __.YearsOfExperience).HasColumnName("YearsOfExperience").IsRequired();
                });
                entity.Metadata.FindNavigation(nameof(OpenRequestBU.Competences))!.SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            modelBuilder.Entity<MatchToOpenRequest>(entity =>
            {
                entity.ToTable("MatchToOpenRequests");
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Id).ValueGeneratedNever().HasConversion(
                                       __ => __.Value, __ => MatchToOpenRequestId.FromGuid(__)).HasColumnName("MatchToOpenRequestId");
                entity.OwnsOne(_ => _.IsOpen, isOpen =>
                {
                    isOpen.Property(__ => __.Value).HasColumnName("IsOpen").IsRequired();
                });
                entity.OwnsOne(_ => _.IsMatch, isMatched =>
                {
                    isMatched.Property(__ => __.Value).HasColumnName("IsMatch").IsRequired();
                });
                entity.OwnsOne(_ => _.IsHired, isHired =>
                {
                    isHired.Property(__ => __.Value).HasColumnName("IsHired").IsRequired();
                });
                entity.OwnsOne(_ => _.MatchScore, matchScore =>
                {
                    matchScore.Property(__ => __.Value).HasColumnName("MatchScore").IsRequired();
                });
                entity.OwnsOne(_ => _.IsWithdrawn, isWithdrawn =>
                {
                    isWithdrawn.Property(__ => __.Value).HasColumnName("IsWithdrawn").IsRequired();
                });
                entity.OwnsOne(_ => _.IsClosed, isClosed =>
                {
                    isClosed.Property(__ => __.Value).HasColumnName("IsClosed").IsRequired();
                });
                entity.OwnsOne(_ => _.InterviewDate, interviewDate =>
                {
                    interviewDate.Property(__ => __.Value).HasColumnName("InterviewDate");
                });
                entity.OwnsOne(_ => _.ApplyForPosition, applyForPosition =>
                {
                    applyForPosition.Property(__ => __.Value).HasColumnName("ApplyForPosition");
                });

                entity.Property(_ => _.EmployeeId)
                      .HasConversion(
                          employeeId => employeeId.Value,
                          value => EmployeeId.FromGuid(value)
                      ).IsRequired();

                entity.Property(_ => _.OpenRequestId)
                      .HasConversion(
                          openRequestId => openRequestId.Value,
                          value => OpenRequestId.FromGuid(value)
                      ).IsRequired();
            });
        }
    }
}
