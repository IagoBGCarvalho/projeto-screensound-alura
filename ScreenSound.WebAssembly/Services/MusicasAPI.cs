using ScreenSound.Shared.Modelos.Requests;
using ScreenSound.Shared.Modelos.Response;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ScreenSound.WebAssembly.Services
{
    public class MusicasAPI
    {
        // Classe responsável por fazer a comunicação com a API para obter dados relacionados as músicas

        private readonly HttpClient _httpClient;
        private readonly ILogger<MusicasAPI> _logger;

        public MusicasAPI(IHttpClientFactory factory, ILogger<MusicasAPI> logger)
        {
            _httpClient = factory.CreateClient("API");
            _logger = logger;
        }

        public async Task<ICollection<MusicaResponse>> GetMusicaAsync()
        {
            try
            {
                _logger.LogInformation("Tentando buscar músicas no endpoint de músicas.");
                var result = await _httpClient.GetFromJsonAsync<ICollection<MusicaResponse>>("musicas");
                _logger.LogInformation("Busca de músicas concluída.");
                return result;
            }
            catch (Exception e)
            {
                string mensagem = "Erro ao buscar músicas da API";
                _logger.LogError(e, mensagem); // Atribui a exceção completa e uma mensagem personalizada ao log
                throw new ApplicationException(mensagem, e); // Atribui a mensagem personalizada e a exceção original ao lançamento da nova exceção
            }
            
        }
    }
}
