using Glassdoor.Domain.Commons;

namespace Glassdoor.Model.UserModels;

public class UserUpdateModel
{
    public long Id {  get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
}
