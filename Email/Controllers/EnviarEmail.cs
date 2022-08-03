using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Email.Models;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace Email.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnviarEmailController : ControllerBase
    {
        [HttpPost]
        public ActionResult<IEnumerable<Models.Email>> Post(Models.Email email)
        {
            try
            {
                if (ValidarEnderecoEmail(email.Remetente) == false)
                    throw new Exception("Email inválido:" + email.Remetente);

                MailMessage mensagemEmail = new MailMessage(email.Remetente, email.Destinatario, email.Assunto, email.EnviaMensagem);

                SmtpClient smtp = new SmtpClient("link do seu servidor", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("seu e-mail", "senha do seu e-mail")
                };

                // envia a mensagem
                smtp.Send(mensagemEmail);

                return Ok("Mensagem enviada com sucesso às " + DateTime.Now.ToString() + ".");
            }
            catch (Exception ex)
            {
                return NotFound(new MensagemRetorno { Status = "Erro", Message = "Não foi possível enviar e-mail." });
            }
        }

        private bool ValidarEnderecoEmail(string enderecoEmail)
        {
            try
            {
                //define a expressão regular para validar o email
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                // testa o email com a expressão
                if (expressaoRegex.IsMatch(enderecoEmail))
                    return true;                

                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }


}
