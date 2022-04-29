using Challenge.Ecommerce.Application.DTO;
using Challenge.Ecommerce.Application.Interface;
using Challenge.Ecommerce.Comun;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Application.Main
{
    public class PaisApplication : IPaisApplication
    {
        /// <summary>
        /// Se busca paises en API de ML
        /// </summary>
        /// <param name="pais"></param>
        /// <returns>Paises</returns>
        public async Task<Response<PaisDto>> GetPais(string pais)
        {
            var response = new Response<PaisDto>();

            if (string.IsNullOrEmpty(pais))
            {
                response.IsSuccess = false;
                response.Message = "Errores de validación";
                return response;
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    HttpResponseMessage data = await httpClient.GetAsync("https://api.mercadolibre.com/classified_locations/countries/" + pais);
                    if (data.IsSuccessStatusCode)
                    {
                        var countriesAsync = await data.Content.ReadAsStringAsync();
                        response.Data = JsonConvert.DeserializeObject<PaisDto>(countriesAsync);
                        if (response.Data != null)
                        {                           
                            response.IsSuccess = true;
                            response.Message = "Exito";
                            return response;                          
                        }
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "No se encuentra el pais";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
