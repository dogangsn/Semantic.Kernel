using Codeblaze.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.ChatCompletion;
using Semantic.Kernel.API.Models;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddKernel();

builder.Services.AddKernel()
                .AddOllamaChatCompletion("deepseek-r1", "http://localhost:11434");
builder.Services.AddRequestTimeouts();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseRequestTimeouts();

app.MapPost("/chat", async (IChatCompletionService chatCompletionService, ChatModel chatModel) =>
{
    var response = await chatCompletionService.GetChatMessageContentAsync(chatModel.Input);
    return response.ToString();
}).WithRequestTimeout(TimeSpan.FromMinutes(10));

app.MapGet("/", () => "Hello World!");

app.Run();
