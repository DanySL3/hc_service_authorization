using Application.Adapters.Request;
using Domain.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Adapters.Request
{
    public class ActualizarUsuarioAdapter
    {
        public int usuario_id { get; set; }

        public int cargo_id { get; set; }

        public List<int> agencia_ids { get; set; }

        public string nombre { get; set; }

        public string documento_numero { get; set; }

        public string correo { get; set; }

        public string usuario { get; set; }

    }
}

public class ActualizarUsuarioValidator : AbstractValidator<ActualizarUsuarioAdapter>
{
    public ActualizarUsuarioValidator()
    {
    }
}
