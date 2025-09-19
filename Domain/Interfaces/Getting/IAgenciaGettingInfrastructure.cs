﻿using Domain.Entities.Agencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Getting
{
    public interface IAgenciaGettingInfrastructure
    {
        public Task<List<DatosAgenciaEntity>> listarAgenciasUsuario(int usuario_id, int sistema_codigo);

        public Task<List<DatosAgenciaEntity>> listarAgencia();
    }
}
