using Domain.Entities.Autenticacion;
using Domain.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Getting
{
    public interface IUsuarioGettingInfrastructure
    {
        public Task<ListarUsuarioEntity> buscarUsuario(int usuario_id, string documento_numero);

        public Task<List<ListarCargosEntity>> listarCargos();

        public Task<List<ListaPrivilegiosEntity>> listarPrivilegios(int sistema_id, int usuario_id, string documento_numero);

        public Task<List<ListarUsuarioEntity>> listarUsuarios(int sistema_id);
    }
}
