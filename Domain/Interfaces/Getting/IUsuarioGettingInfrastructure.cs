using Domain.Entities;
using Domain.Entities.Autenticacion;
using Domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Getting
{
    public interface IUsuarioGettingInfrastructure
    {
        public Task<ConsultarDetalleUsuarioEntity> buscarUsuario(int usuario_id, string documento_numero);

        public Task<List<ListarCargosEntity>> listarCargos();

        public Task<List<ListaPrivilegiosEntity>> listarAccesos(int sistema_id, int usuario_id, string documento_numero);

        public Task<paginationEntity<ListarUsuarioEntity>> listarUsuarios(int index, int cantidad);
    }
}
