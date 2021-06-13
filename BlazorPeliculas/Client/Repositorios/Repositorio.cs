using BlazorPeliculas.Shared.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorPeliculas.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        private readonly HttpClient httpClient;
        public Repositorio(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        private JsonSerializerOptions OpcionesPorDefectoJSON =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHTTP = await httpClient.GetAsync(url);
            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<T>(responseHTTP, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<T>(response, false, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, true, responseHTTP);
            }
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }


        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<TResponse>(responseHttp, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }

            private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseString, jsonSerializerOptions);
            }

            //public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
            //{
            //    var enviarJSON = JsonSerializer.Serialize(enviar);
            //    var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            //    var responseHttp = await httpClient.PostAsync(url, enviarContent);
            //    return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
            //}
            //public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
            //{
            //    var enviarJSON = JsonSerializer.Serialize(enviar);
            //    var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            //    var responseHttp = await httpClient.PostAsync(url, enviarContent);
            //    return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);

            //    //var enviarJSON = JsonSerializer.Serialize(enviar);
            //    //var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            //    //var responseHttp = await httpClient.PostAsync(url, enviarContent);
            //    //return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
            //}
            public List<Pelicula> ObtenerPeliculas()
        {
           return  new List<Pelicula>()
        {
                new Pelicula(){Titulo = "Spider-Man: Far From Home", 
                    Lanzamiento  = new DateTime(2019, 7, 2), 
                    Poster= "https://pics.filmaffinity.com/spider_man_spiderman-945131720-mmed.jpg"},
                new Pelicula(){Titulo = "Moana", Lanzamiento  = new DateTime(2016, 11, 23), 
                    Poster = "https://pics.filmaffinity.com/moana-802273424-mmed.jpg"},
                new Pelicula(){Titulo = "Inception", Lanzamiento  = new DateTime(2010, 7, 16),
                    Poster = "https://pics.filmaffinity.com/inception-652954101-mmed.jpg"}
            };
        }
    }
}
