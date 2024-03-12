using Glassdoor.Domain.Commons;
using Glassdoor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Domain.Entities;

public class Job : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string SalaryRange { get; set; }
    public JobStatusEnum Status { get; set; }
    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public IEnumerable<Application> Applications { get; set; }
}
