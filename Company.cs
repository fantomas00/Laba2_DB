using System;
using System.Collections.Generic;

namespace Persistance.Models;

public partial class Company
{
    public long CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Owner { get; set; } = null!;

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
