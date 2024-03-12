using Glassdoor.Domain.Commons;

namespace Glassdoor.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public IEnumerable<Application> Applications { get; set; }
}
