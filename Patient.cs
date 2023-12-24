using System;
using System.Collections.Generic;

namespace Persistance.Models;

public partial class Patient
{
    public long PatientId { get; set; }

    public string Fullname { get; set; } = null!;

    public int Age { get; set; }

    public virtual ICollection<Pharmacy> Pharmacies { get; set; } = new List<Pharmacy>();
}
