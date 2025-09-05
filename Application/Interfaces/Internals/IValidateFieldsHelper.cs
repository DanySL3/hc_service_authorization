using Application.Adapters.Internals;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Internals
{
    public interface IValidateFieldsHelper
    {
        public bool dataValidate<T>(T objModel, List<FieldResponse> lstError, AbstractValidator<T> objValidator);

    }
}
