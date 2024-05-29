using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Common.Response
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } //type, description
        public Object? Result { get; set; }
    }
}
