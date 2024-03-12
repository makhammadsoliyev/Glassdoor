using Glassdoor.Model.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Model.Companies;

public class CompanyUpdateModel
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public JobCreateModel Job { get; set; }
}
