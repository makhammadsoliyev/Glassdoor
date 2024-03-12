using Glassdoor.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Model.Companies;

public class CompanyCreateModel
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}
