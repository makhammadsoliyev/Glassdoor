using Glassdoor.Domain.Commons;
using Glassdoor.Domain.Enums;

namespace Glassdoor.Domain.Entities;

public class Application : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long JobId { get; set; }
    public Job Job { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime Time { get; set; }
}
