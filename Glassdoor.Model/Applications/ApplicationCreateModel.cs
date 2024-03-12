namespace Glassdoor.Model.Applications;

public class ApplicationCreateModel
{
    public long UserId { get; set; }
    public long JobId { get; set; }
    public DateTime Time { get; set; } = DateTime.UtcNow;
}
