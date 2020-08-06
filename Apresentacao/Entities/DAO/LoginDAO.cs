using Apresentacao.Entities.Data;
using Apresentacao.Helpers;
using Apresentacao.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Apresentacao.Entities.DAO
{
    public class LoginDAO : ILoginDAO, IDisposable
    {
        private DBContext _contexto;

        public LoginDAO()
        {
            this._contexto = new DBContext();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }

        public RetornaAcao Adicionar(Login item)
        {
            RetornaAcao retorno = new RetornaAcao();
            try
            {
                Cripto criptografia = new Cripto();
                item.PasswordCrypto = criptografia.CodificarMensagem(item.Password);

                _contexto.Logins.Add(item);
                _contexto.SaveChanges();
                retorno.Retorno = true;
                retorno.Mensagem = "Salvo com sucesso!";
            }
            catch (Exception ex)
            {
                retorno.Retorno = false;
                retorno.Mensagem = "Erro! " + ex.Message;
            }
            return retorno;
        }

        public RetornaAcao Atualizar(Login item)
        {
            RetornaAcao retorno = new RetornaAcao();
            try
            {
                _contexto.Logins.Update(item);
                _contexto.SaveChanges();
                retorno.Retorno = true;
                retorno.Mensagem = "Atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                retorno.Retorno = false;
                retorno.Mensagem = "Erro! " + ex.Message;
            }
            return retorno;
        }

        public RetornaAcao Remover(Login item)
        {
            RetornaAcao retorno = new RetornaAcao();
            try
            {
                _contexto.Logins.Remove(item);
                _contexto.SaveChanges();
                retorno.Retorno = true;
                retorno.Mensagem = "Excluido com sucesso!";
            }
            catch (Exception ex)
            {
                retorno.Retorno = false;
                retorno.Mensagem = "Erro! " + ex.Message;
            }
            return retorno;
        }

        public Login Localizar(int? Id)
        {
            Login retorno = null;
            try
            {
                retorno = _contexto.Logins.FirstOrDefault(x => x.Id == Id);
                retorno.Password = new Cripto().DecodificarMensagem(retorno.PasswordCrypto);
            }
            catch (Exception ex)
            {

            }
            return retorno;
        }

        public bool Existe(int? Id)
        {
            bool retorno = false;
            try
            {
                retorno = _contexto.Logins.Any(x => x.Id == Id);
            }
            catch (Exception ex)
            {

            }
            return retorno;
        }

        public IList<Login> ListLogins()
        {
            IList<Login> retorno = null;
            try
            {
                retorno = _contexto.Logins.ToList();
                if (retorno != null)
                {
                    foreach (Login login in retorno)
                    {
                        login.Password = new Cripto().DecodificarMensagem(login.PasswordCrypto);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return retorno;
        }

        public Login Logar(Login item)
        {
            Login retorno = null;

            try
            {
                IList<Login> logins = _contexto.Logins.Where(x => x.Username.ToUpper() == item.Username.ToUpper())
                                                        .Select(x => x).ToList();
                if (logins != null)
                {
                    Cripto criptografia = new Cripto();
                    foreach (Login entidade in logins)
                        if (criptografia.DecodificarMensagem(entidade.PasswordCrypto) == item.Password)
                        {
                            retorno = entidade;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
            }
            return retorno;
        }
    }
}
