using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTemplate.Domain.Core.DTO
{
    public class PagingParametersRequest
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 50;
        public bool CountTotal { get; set; } = false;
    }
}
