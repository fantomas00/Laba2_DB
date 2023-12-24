using System;
using System.Collections.Generic;

namespace Persistance.Models;

public partial class Pharmacy
{
    public long PharmacyId { get; set; }

    public string PharmacyName { get; set; } = null!;

    public string City { get; set; } = null!;

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
