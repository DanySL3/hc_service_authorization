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
    public class ActualizarContraseniaAdapter
    {        
        public string contrasenia_anterior {  get; set; }

        public string contrasenia_actual {  get; set; }
    }
}

public class ActualizarContraseniaValidator : AbstractValidator<ActualizarContraseniaAdapter>
{
    public ActualizarContraseniaValidator()
    {
        RuleFor(x => x.contrasenia_anterior)
                .NotEmpty().WithMessage(MessageException.GetErrorByCode(10002, "Contraseña anterior")).WithErrorCode("10002")
                .NotNull().WithMessage(MessageException.GetErrorByCode(10001, "Contraseña anterior")).WithErrorCode("10001");

        RuleFor(x => x.contrasenia_actual)
                .NotEmpty().WithMessage(MessageException.GetErrorByCode(10002, "Contraseña Actual")).WithErrorCode("10002")
                .NotNull().WithMessage(MessageException.GetErrorByCode(10001, "Contraseña Actual")).WithErrorCode("10001");
    }
}
