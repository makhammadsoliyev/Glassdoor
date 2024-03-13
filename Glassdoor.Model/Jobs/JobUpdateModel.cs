using Glassdoor.Domain.Enums;

namespace Glassdoor.Model.Jobs;

public class JobUpdateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string SalaryRange { get; set; }
    public JobStatusEnum Status { get; set; }
}
