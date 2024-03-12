using Glassdoor.Domain.Enums;

namespace Glassdoor.Model.Applications;

public class ApplicationViewModel
{
    public UserViewModel User { get; set; }
    public JobViewModel Job { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime Time { get; set; }
}
