using System;
using System.Collections.Generic;

namespace Persistance.Models;

public partial class Medicine
{
    public long MedicineId { get; set; }

    public string MedicineName { get; set; } = null!;

    public long CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Pharmacy> Pharmacies { get; set; } = new List<Pharmacy>();
}
