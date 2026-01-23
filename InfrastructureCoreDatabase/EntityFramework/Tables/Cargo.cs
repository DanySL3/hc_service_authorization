using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class Cargo
{
    public int Id { get; set; }

    public int? CargoPadreId { get; set; }

    public string Cargo1 { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Isactive { get; set; }

    public int? Usercreatedid { get; set; }

    public int? Usermodifiedid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }
}
