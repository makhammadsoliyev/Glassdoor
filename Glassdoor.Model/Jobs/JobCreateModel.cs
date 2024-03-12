using Glassdoor.Domain.Enums;

namespace Glassdoor.Model.Jobs;

public class JobCreateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string SalarRange { get; set; }
    public JobStatusEnum Status { get; set; }
    public long CompanyId { get; set; }
}
