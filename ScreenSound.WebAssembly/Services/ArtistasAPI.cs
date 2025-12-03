using ScreenSound.Shared.Modelos.Requests;
using ScreenSound.Shared.Modelos.Response; // Utilizando o projeto compartilhado para acessar os modelos de resposta
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ScreenSound.WebAssembly.Services
{
    public class ArtistasAPI
    {
        // Classe responsável por fazer a comunicação com a API para obter dados relacionados aos artistas

        private readonly HttpClient _httpClient; // Objeto que permite criar um cliente que fará requisições HTTP

        public ArtistasAPI(IHttpClientFactory factory) // Interface responsável por criar instâncias de HttpClient
        {
            _httpClient = factory.CreateClient("API");
        }

        // Métodos:
        public async Task<ICollection<ArtistaResponse>> GetArtistasAsync()
        {
            // GetFromJsonAsyncFaz uma chamada utilizando HTTP GET na API e faz a desserialização do JSON para uma coleção de objetos.
            // Como é uma função genérica, é necessário informar o tipo de dados que será retornado, no caso, uma coleção de ArtistaResponse.
            // Recebe como parâmetro a rota do endpoint da API que retorna os dadoas requeridos.
            return await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("artistas"); // Subentende-se que a URL base já foi configurada no appsettings, então apenas a rota específica é necessária
        }

        public async Task AddArtistaAsync(ArtistaRequest artista) // Utilizando o modelo de request que delimita os dados necessários para criar um artista e enviar ele para a API
        {
            await _httpClient.PostAsJsonAsync("artistas", artista); // Faz uma chamada HTTP POST para a API enviando para a rota "artistas" o objeto "artista" serializado
        }
    }
}
