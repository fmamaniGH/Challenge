using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Ecommerce.Currency.Dto
{
    public class CurrencyConversionDto
    {
        public string currency_base { get; set; }
        public string currency_quote { get; set; }
        public double ratio { get; set; }
        public double rate { get; set; }
        public double inv_rate { get; set; }
        public DateTime creation_date { get; set; }
        public DateTime valid_until { get; set; }
    }
}
