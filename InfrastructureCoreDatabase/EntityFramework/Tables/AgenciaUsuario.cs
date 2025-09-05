using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class AgenciaUsuario
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int AgenciaId { get; set; }

    public bool? Esprincipal { get; set; }

    public bool? Isactive { get; set; }

    public int? Usercreatedid { get; set; }

    public int? Usermodifiedid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }
}
