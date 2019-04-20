using System.Text.RegularExpressions;

namespace EP.CursoMvc.Domain.Value_Objects
{
    public partial class Email
    {
        public static bool Validar(string email)
        {
            // Descobre se o email está no formato válido
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
    }
}
