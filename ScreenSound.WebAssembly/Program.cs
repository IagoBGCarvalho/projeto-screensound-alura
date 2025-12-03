using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ScreenSound.WebAssembly;
using ScreenSound.WebAssembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args); // Objeto utilizado para configurar toda a aplicação Blazor WebAssembly

// CONFIG DE CONTEXTO DE INJEÇÃO DE DEPENDÊNCIAS DOS SERVIÇOS

// AddTransient cria um serviço com ciclo de vida temporário, o objeto vai existir apenas no contexto que for chamado e depois será descartado no Garbage Collector
builder.Services.AddTransient<ArtistasAPI>();
builder.Services.AddTransient<MusicasAPI>();

// É necessário especificar as configurações do HttpClient que será injetado em cada serviço. Deve receber o nome do cliente HTTP especificado no serviço
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["APIServer"]!); // Configura a URL base da API a partir do appsettings.json que é mapeado pela coleção Configuration do builder

    // É necessário especificar que tipo de dado será aceito na resposta das requisições HTTP
    client.DefaultRequestHeaders.Add("Accept", "application/json"); // Aceita respostas no formato JSON apenas
}); 

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
