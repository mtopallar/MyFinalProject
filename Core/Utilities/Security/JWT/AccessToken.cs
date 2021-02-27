using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken //client tarafından istekle apiye gönderilen token
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
