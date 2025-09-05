using Domain.Entities.Autenticacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IHelperCommon
    {
        public string hashPassword(string password);

        public bool checkPassword(string hashedPassword, string password);
    }
}
