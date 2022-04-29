using Challenge.Ecommerce.Currency.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Currency
{
    public class Helper
    {

        /// <summary>
        /// Obtiene Currencies
        /// </summary>
        /// <returns>Lista de currency </returns>
        public static async Task<List<CurrencyDto>> GetCurrencies()
        {
            List<CurrencyDto> currencies = new List<CurrencyDto>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage data = await httpClient.GetAsync("https://api.mercadolibre.com/currencies/");
                if (data.IsSuccessStatusCode)
                {
                    var currenciesAsync = await data.Content.ReadAsStringAsync();
                    currencies = JsonConvert.DeserializeObject<List<CurrencyDto>>(currenciesAsync);
                }   
            }
            return currencies;
        }

        /// <summary>
        /// Convierte a dolar
        /// </summary>
        /// <param name="from"></param>
        /// <returns>ratio</returns>
        public static async Task<double> GetCurrencyConversionUSD(string from)
        {
            double ratio = 0;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage data = await httpClient.GetAsync("https://api.mercadolibre.com/currency_conversions/search?from=" +from+"&to=USD");
                if (data.IsSuccessStatusCode)
                {
                    var currencyConversionAsync = await data.Content.ReadAsStringAsync();
                    CurrencyConversionDto currencyConversion = JsonConvert.DeserializeObject<CurrencyConversionDto>(currencyConversionAsync);          
                    ratio = currencyConversion.ratio;
                }
            }
            return ratio;
        }

        /// <summary>
        /// Graba a disco el json
        /// </summary>
        /// <param name="currencies"></param>
        internal static void ExportCurrenciesToJson(List<CurrencyDto> currencies)
        {
            string PathExportRatiosToCSV = GetAppValue("PathExportCurrenciesToJson");
            var path = Path.Combine(PathExportRatiosToCSV, "Currencies.json");
            if (File.Exists(path))
            {
                string json = JsonConvert.SerializeObject(currencies);

                File.WriteAllText(path, json);
            }
            else
            {
                throw new Exception($"No existe el path destino {path}");
            }
        }

        /// <summary>
        /// Graba a disco el CSV
        /// </summary>
        /// <param name="currencies"></param>
        internal static void ExportRatiosToCSV(List<CurrencyDto> currencies)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            string PathExportRatiosToCSV = GetAppValue("PathExportRatiosToCSV");
            var path = Path.Combine(PathExportRatiosToCSV, "ratio.csv");

            if (File.Exists(path))
            {
                //Es necesario utiliar separador de miles ya que se transforma al concatenar con comas
                string csv = string.Join(",", currencies.Select(x => x.toDolar.ToString(nfi)));
                File.WriteAllText(path, csv);
            }
            else
            {
                throw new Exception($"No existe el path destino {path}");
            }
           
        }

        /// <summary>
        /// Devuelve el valor de la key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetAppValue(string key)
        {
            string value = "";
            if (ConfigurationManager.AppSettings[key] == null && ConfigurationManager.AppSettings[key] == "")
            {
                throw new Exception($"No existe valor para la key {key} en el App.config");
            }
            value = ConfigurationManager.AppSettings[key];

            return value;
        }

    }
}
