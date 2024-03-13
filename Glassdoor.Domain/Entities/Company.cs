using Glassdoor.Domain.Commons;

namespace Glassdoor.Domain.Entities;

public class Company : Auditable
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public IEnumerable<Job> Jobs { get; set; }
}
