using Glassdoor.Domain.Commons;
using Glassdoor.Domain.Entities.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Domain.Entities.Companies;

public class Company : Auditable
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public ICollection<Job> Jobs { get; set; }
}
