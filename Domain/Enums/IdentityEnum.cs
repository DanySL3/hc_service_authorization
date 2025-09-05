using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{

    public enum perfilEnum : int
    {
        rendidor = 1,
        aprobador = 2,
        administrador = 3,

    }
    public enum usuarioEstadoEnum : int
    {
        vigente = 1,
        suspendido = 2,
    }

    public enum documentoTipoEnum : int
    {
        DNI = 1,
        RUC = 2,
    }




    public enum IdentityEnum : int
    {
        nContraseniaIncorrecta = 1,
        nUsuarioIncorrecto = 2,
        nCredencialesValidos = 3,
    }

    public enum sistemaEnum : int
    {
        nCitaciones = 1,
        nNormativa = 3,
        nFirmas = 5,
        nMonitor = 4,
        nChiqui = 2,
    }
}
