using Glassdoor.Model.Jobs;

namespace Glassdoor.Model.Companies;

public class CompanyViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public IEnumerable<JobViewModel> Jobs { get; set; }
}
