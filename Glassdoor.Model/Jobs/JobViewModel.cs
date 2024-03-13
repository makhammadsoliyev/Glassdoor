using Glassdoor.Domain.Entities;
using Glassdoor.Domain.Enums;
using Glassdoor.Model.Companies;

namespace Glassdoor.Model.Jobs;

public class JobViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SalaryRange { get; set; }
    public JobStatusEnum Status { get; set; }
    public CompanyViewModel Company { get; set; }
    public IEnumerable<Application> Applications { get; set; }
}
