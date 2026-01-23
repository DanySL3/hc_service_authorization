using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class Usuario
{
    public int Id { get; set; }

    public int? DocumentoTipoId { get; set; }

    public int UsuarioEstadoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? DocumentoNumero { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Foto { get; set; }

    public bool? Esbloqueado { get; set; }

    public bool? Isactive { get; set; }

    public int? Usercreatedid { get; set; }

    public int? Usermodifiedid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }
}
