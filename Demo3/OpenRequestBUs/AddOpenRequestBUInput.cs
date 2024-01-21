using Demo3.Data;

namespace Demo3.OpenRequestBUs
{
    public sealed record AddOpenRequestBUInput(
        Guid TeamRequestId,
        string TeamRequestName,
        string TeamRequestDescription,
        string PositionName,
        string Cluster,
        string PositionDescription,
        string Location,
        int NumberOfFTERequired,
        string AccountManager,
        string SkillLevel,
        DateTime RoleStartDate,
        DateTime DeadLine,
        List<CompetenceInput>? Competences = null
        );

    public sealed record CompetenceInput(string SkillName,
                                         int YearsOfExperience);
    public sealed record UpdateOpenRequestBUInput([ID(nameof(OpenRequestBU))] OpenRequestId Id,
                                                  string TeamRequestName,
                                                  string TeamRequestDescription,
                                                  string PositionName,
                                                  string Cluster,
                                                  string PositionDescription,
                                                  string Location,
                                                  int NumberOfFTERequired,
                                                  string AccountManager,
                                                  string SkillLevel,
                                                  DateTime RoleStartDate,
                                                  DateTime DeadLine,
                                                  List<CompetenceInput>? Competences = null);
}
