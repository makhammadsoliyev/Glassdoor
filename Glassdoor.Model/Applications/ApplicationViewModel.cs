using Glassdoor.Domain.Enums;
using Glassdoor.Model.Jobs;
using Glassdoor.Model.UserModels;

namespace Glassdoor.Model.Applications;

public class ApplicationViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public JobViewModel Job { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime Time { get; set; }
}
