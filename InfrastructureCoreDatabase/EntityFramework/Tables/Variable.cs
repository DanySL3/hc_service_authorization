using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class Variable
{
    public int Id { get; set; }

    public string Variable1 { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string VariableTipo { get; set; } = null!;

    public string Valor { get; set; } = null!;

    public bool? Isactive { get; set; }

    public int? Usercreatedid { get; set; }

    public int? Usermodifiedid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }
}
