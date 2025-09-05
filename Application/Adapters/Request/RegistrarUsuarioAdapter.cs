using Application.Adapters.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Adapters.Request
{
    public class RegistrarUsuarioAdapter
    {
        public int cargo_id {  get; set; }

        public string nombre {  get; set; }

        public string documento_numero {  get; set; }

        public string correo {  get; set; }

        public string usuario { get; set; }

        public string? contrasenia { get; set; }
    }
}

public class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioAdapter>
{
    public RegistrarUsuarioValidator()
    {
    }
}

