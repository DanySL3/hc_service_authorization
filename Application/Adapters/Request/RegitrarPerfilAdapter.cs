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
    public class RegitrarPerfilAdapter
    {
        public string perfil {  get; set; }
        public int? sistema_id {  get; set; }
        public string descripcion {  get; set; }
    }
}


public class RegitrarPerfilValidator : AbstractValidator<RegitrarPerfilAdapter>
{
    public RegitrarPerfilValidator()
    {
    }
}
