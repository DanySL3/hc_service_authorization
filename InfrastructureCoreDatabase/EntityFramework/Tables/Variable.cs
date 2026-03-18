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

    public bool? IsActive { get; set; }

    public int UserCreatedId { get; set; }

    public int? UserModifiedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
