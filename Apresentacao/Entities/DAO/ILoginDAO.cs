using Apresentacao.Helpers.Common;
using System.Collections.Generic;

namespace Apresentacao.Entities.DAO
{
    public interface ILoginDAO
    {
        RetornaAcao Adicionar(Login item);
        Login Logar(Login item);
        RetornaAcao Atualizar(Login item);
        RetornaAcao Remover(Login item);
        bool Existe(int? Id);
        Login Localizar(int? Id);
        IList<Login> ListLogins();
    }
}
