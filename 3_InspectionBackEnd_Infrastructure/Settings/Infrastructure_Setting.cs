using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_InspectionBackEnd_Infrastructure.Settings
{
    public class Infrastructure_Setting
    {
        public List<ConnectionString> ConnectionStrings { get; set; } = new List<ConnectionString>();
        public Jwt Jwt { get; set; } = new Jwt();
    }
    public class Jwt
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;

    }
    public class ConnectionString
    {
        public string ConStringName { get; set; } = string.Empty;
        public string ConStringValue { get; set; } = string.Empty;
    }
}
