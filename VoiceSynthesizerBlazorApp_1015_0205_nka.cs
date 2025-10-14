// 代码生成时间: 2025-10-15 02:05:25
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System;

// VoiceSynthesizerService.cs - 语音合成服务类
public class VoiceSynthesizerService
{
    private readonly HttpClient _httpClient;

    public VoiceSynthesizerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // 语音合成方法
    public async Task<string> SynthesizeTextAsync(string text, string language = "en-US", string voiceName = "Microsoft Server Speech Text to Speech Voice (en-US, ZiraRUS)")
    {
        try
        {
            var payload = new {
                text,
                lang = language,
                voice = voiceName
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://eastus.tts.speech.microsoft.com/cognitiveservices/v1", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStreamAsync();

            return ConvertStreamToString(result);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error with the request: {ex.Message}");
            throw;
        }
    }

    // 用于将流转换为字符串的方法
    private string ConvertStreamToString(Stream stream)
    {
        using (var reader = new StreamReader(stream, Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
    }
}

// VoiceSynthesizer.razor - Blazor组件
@inject VoiceSynthesizerService VoiceService
@code {
    private string textToSynthesize = "Hello, this is a text-to-speech example.";
    private string synthesizedVoice = "";
    private bool isLoading = false;

    // 合成文本方法
    private async Task SynthesizeText()
    {
        isLoading = true;
        synthesizedVoice = "";

        try
        {
            synthesizedVoice = await VoiceService.SynthesizeTextAsync(textToSynthesize);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error synthesizing text: {ex.Message}");
            synthesizedVoice = "Error synthesizing voice";
        }
        finally
        {
            isLoading = false;
        }
    }
}

<!-- VoiceSynthesizer.razor -->
<div>
    <input type="text" @bind="textToSynthesize" placeholder="Enter text to synthesize..." />
    <button @onclick="SynthesizeText">Synthesize</button>
    <p>@isLoading ? "Synthesizing... " : synthesizedVoice</p>
</div>
