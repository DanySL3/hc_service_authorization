using Application.Adapters.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Adapters.Request
{
    public class RegistrarPrivilegiosPerfilAdapter
    {
        public int perfil_id {  get; set; }
        public List<RegistrarMenuAdapter>? lstMenu {  get; set; }
    }
}

public class RegistrarPrivilegiosPerfilValidator : AbstractValidator<RegistrarPrivilegiosPerfilAdapter>
{
    public RegistrarPrivilegiosPerfilValidator()
    {
    }
}

