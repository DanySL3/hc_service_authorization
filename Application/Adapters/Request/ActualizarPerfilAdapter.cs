using Application.Adapters.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Adapters.Request
{
    public class ActualizarPerfilAdapter
    {
        public int perfil_id { get; set; }
        public string perfil { get; set; }
        public string descripcion { get; set; }
    }
}

public class ActualizarPerfilValidator : AbstractValidator<ActualizarPerfilAdapter>
{
    public ActualizarPerfilValidator()
    {
    }
}