using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class Usuario
{
    public Guid Uuid { get; set; }

    public int Id { get; set; }

    public int? DocumentoTipoId { get; set; }

    public int UsuarioEstadoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? DocumentoNumero { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public string? Correo { get; set; }

    public string? Foto { get; set; }

    public bool? IsActive { get; set; }

    public int UserCreatedId { get; set; }

    public int? UserModifiedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
