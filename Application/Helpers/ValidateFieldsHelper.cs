using Application.Adapters.Internals;
using Application.Interfaces.Internals;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class ValidateFieldsHelper : IValidateFieldsHelper
    {
        public bool dataValidate<T>(T objModel, List<FieldResponse> lstError, AbstractValidator<T> objValidator)
        {

            ValidationResult objResult = objValidator.Validate(objModel);

            if (!objResult.IsValid)
            {
                foreach (var item in objResult.Errors)
                {
                    lstError.Add(new FieldResponse { Code = item.ErrorCode, Message = item.ErrorMessage, Field = item.PropertyName });
                }
                return false;
            }
            return true;
        }
    }
}
