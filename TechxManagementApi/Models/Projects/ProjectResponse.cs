using TechxManagementApi.Models.AccountTasks;

namespace TechxManagementApi.Models.Projects;

public class ProjectResponse
{
    public int Id { get; set; }
    public string ProjectName { get; set; }
    public string ProjectDescription { get; set; }
    // public List<AccountTaskResponse> AccountTasks { get; set; }
}