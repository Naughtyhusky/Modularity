using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Jwt
{
    public class ClaimsModel
    {
        static ClaimsModel() 
        {
            Id = "id";
        }

        /// <summary>
        /// Id
        /// </summary>
        public static string Id { get; set; }
    }
}
