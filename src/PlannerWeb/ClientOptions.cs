using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class ClientOptions
    {
        public string Uri { get; set; }

        // TODO Eventually move to authentication cookie or so.
        public string Token { get; set; }
    }
}
