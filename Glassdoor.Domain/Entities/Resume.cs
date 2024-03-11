using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Domain.Entities;

public class Resume
{
    public long Id { get; set; }
    public long AddressId { get; set; }
    public Address Address { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}
