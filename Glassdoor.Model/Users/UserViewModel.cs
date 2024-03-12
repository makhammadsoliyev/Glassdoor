using Glassdoor.Model.Applications;

namespace Glassdoor.Model.UserModels;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public IEnumerable<ApplicationViewModel> Applications { get; set; }
}
