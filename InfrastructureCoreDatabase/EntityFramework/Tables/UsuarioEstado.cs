using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class UsuarioEstado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Isactive { get; set; }

    public int? Usercreatedid { get; set; }

    public int? Usermodifiedid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }
}
