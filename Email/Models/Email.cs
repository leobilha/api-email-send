namespace Email.Models
{
    public class Email
    {
        public string Destinatario { get; private set; } = "seu e-mail aqui";
        public string Remetente { get; set; }
        public string Assunto { get; set; }
        public string EnviaMensagem { get; set; }

    }
}
