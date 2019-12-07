using System;

namespace LibidoMusic.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime Expiration { get; set; }
        public string Role { get; set; }
        public int User_Id { get; set; }
    }
}
