using System;

namespace Challenge.Ecommerce.Currency
{
    class Program
    {
        static void Main(string[] args)
        {
            //Obtiene Currencies
            var currencies = Helper.GetCurrencies().Result;

            foreach (var currency in currencies)
            {
                currency.toDolar = Helper.GetCurrencyConversionUSD(currency.id).Result;
            }

            //Exporta a archivo Json
            Helper.ExportCurrenciesToJson(currencies);

            //Exporta a archivo csv
            Helper.ExportRatiosToCSV(currencies);
        }       
    }
}
