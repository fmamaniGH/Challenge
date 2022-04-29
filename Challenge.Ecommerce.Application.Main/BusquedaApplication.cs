using Challenge.Ecommerce.Application.DTO;
using Challenge.Ecommerce.Application.Interface;
using Challenge.Ecommerce.Comun;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Challenge.Ecommerce.Application.Main
{
    public class BusquedaApplication : IBusquedaApplication
    {

        /// <summary>
        /// Se busca items en api de ML
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Items</returns>
        public async Task<Response<BusquedaDto>> GetItems(string item)
        {
            var response = new Response<BusquedaDto>();
            if (string.IsNullOrEmpty(item))
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
                    HttpResponseMessage data = await httpClient.GetAsync("https://api.mercadolibre.com/sites/MLA/search?q=" + item);
                    if (data.IsSuccessStatusCode)
                    {
                        var itemsAsync = await data.Content.ReadAsStringAsync();
                        response.Data = JsonConvert.DeserializeObject<BusquedaDto>(itemsAsync);
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
                        response.Message = "No se encuentra el item";
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


