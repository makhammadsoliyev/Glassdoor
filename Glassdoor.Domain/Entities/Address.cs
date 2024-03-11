using Glassdoor.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glassdoor.Domain.Entities;

public class Address : Auditable
{
    public string Name { get; set; }
}
