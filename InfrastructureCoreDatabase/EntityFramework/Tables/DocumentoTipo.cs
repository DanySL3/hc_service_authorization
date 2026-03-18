using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class DocumentoTipo
{
    public int Id { get; set; }

    public int PersonaTipoId { get; set; }

    public string? Nombre { get; set; }

    public string? NombreCorto { get; set; }

    public string? CodigoSunat { get; set; }

    public bool? IsActive { get; set; }

    public int UserCreatedId { get; set; }

    public int? UserModifiedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
