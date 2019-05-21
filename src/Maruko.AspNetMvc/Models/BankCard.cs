using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.AspNetMvc.Models
{
    public class BankCard
    {
        public string bankName { get; set; }

        public string bankCode { get; set; }

        public List<Pattern> patterns { get; set; }
    }
}
