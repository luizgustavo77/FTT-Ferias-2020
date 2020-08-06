using Apresentacao.Entities;

namespace Apresentacao.Helpers.Common
{
    public static class State
    {
        public static string LoginSession { get => "login"; }
        public static string SenderEmail { get => "lgfttweb@gmail.com"; }
        public static string SMTPServer { get => "smtp.gmail.com"; }
        public static int SMTPPort { get => 587; }
    }
}
