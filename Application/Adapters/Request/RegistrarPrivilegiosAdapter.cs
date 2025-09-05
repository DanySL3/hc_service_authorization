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
    public class RegistrarPrivilegiosAdapter
    {
        public int sistema_id {  get; set; }
        public int perfil_id { get; set; }
        public int usuario_id {  get; set; }
        public int agencia_id { get; set; }
        public string? fecha_inicio_perfil { get; set; }
        public string? fecha_fin_perfil { get; set; } 
    }
}

public class RegistrarPrivilegiosValidator : AbstractValidator<RegistrarPrivilegiosAdapter>
{
    public RegistrarPrivilegiosValidator()
    {
        RuleFor(x => x.sistema_id)
            .GreaterThan(0)
            .WithMessage(MessageException.GetErrorByCode(10010, "id de Sistema"))
            .WithErrorCode("10010");

        RuleFor(x => x.usuario_id)
            .GreaterThan(0)
            .WithMessage(MessageException.GetErrorByCode(10010, "id de Usuario"))
            .WithErrorCode("10010");
    }
}
