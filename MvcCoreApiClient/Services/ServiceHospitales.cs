using MvcCoreApiClient.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MvcCoreApiClient.Services
{
    public class ServiceHospitales
    {
        private string ApiUrl;
        //NECESITAMOS INDICAR EL TIPO DE DATOS QUE VAMOS A LEER
        private MediaTypeWithQualityHeaderValue header;

        public ServiceHospitales()
        {
            this.ApiUrl = "https://apicorehospitales-bbabf2gfdpcua3ck.francecentral-01.azurewebsites.net/";
            this.header = new MediaTypeWithQualityHeaderValue
                ("application/json");
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            //SE UTILIZA LA CLASE HttpClient PARA LAS PETICIONES
            using (HttpClient client = new HttpClient())
            {
                string request = "api/hospitales";
                //INDICAMOS EL HOST
                client.BaseAddress = new Uri(this.ApiUrl);
                //INDICAMOS LOS DATOS QUE VAMOS A CONSUMIR
                //LIMPIAMOS LAS CABECERAS POR NORMA
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //REALIZAMOS LA PETICION Y CAPTURAMOS UNA RESPUESTA
                HttpResponseMessage response =
                    await client.GetAsync(request);
                //EN LA RESPUESTA TENEMOS LA CLAVE SI DESEAMOS 
                //PERSONALIZAR ERRORES
                if (response.IsSuccessStatusCode == true)
                {
                    //RECUPERAMOS EL CONTENIDO EN json
                    string json =
                        await response.Content.ReadAsStringAsync();
                    //MEDIANTE NEWTON DESERIALIZAMOS JSON A LIST
                    List<Hospital> hospitales =
                        JsonConvert.DeserializeObject<List<Hospital>>
                        (json);
                    return hospitales;
                }else
                {
                    return null;
                }
            }
        }
    }
}
