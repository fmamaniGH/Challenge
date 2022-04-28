using Challenge.Ecommerce.Application.DTO;
using Challenge.Ecommerce.Application.Interface;
using Challenge.Ecommerce.Comun;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Application.Main
{
    public class PaisApplication : IPaisApplication
    {

        public async Task<Response<PaisDto>> GetPais(string pais)
        {
            var response = new Response<PaisDto>();
 
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
