using System.ComponentModel.DataAnnotations;

namespace TheKnife.API.Models.Authentication
{
    public class LoginRequest
    {
        public string UserName { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
