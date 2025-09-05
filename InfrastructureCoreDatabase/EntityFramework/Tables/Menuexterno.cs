using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class MenuExterno
{
    public int Id { get; set; }

    public int? MenuPadreId { get; set; }

    public string Menu { get; set; } = null!;

    public string? Descripcion { get; set; }

    public double Orden { get; set; }

    public string Url { get; set; } = null!;

    public bool? Isactive { get; set; }

    public int? Usercreatedid { get; set; }

    public int? Usermodifiedid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }
}
