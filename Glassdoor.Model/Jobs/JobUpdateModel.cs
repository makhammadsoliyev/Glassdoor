using Glassdoor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Model.Jobs;

public class JobUpdateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string SalarRange { get; set; }
    public JobStatusEnum Status { get; set; }
}
