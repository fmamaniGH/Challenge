using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Ecommerce.Currency.Dto
{
    public class CurrencyDto
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string description { get; set; }
        public int decimal_places { get; set; }
        public double toDolar { get; set; }

    }
}
