using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AnotherNewsPlatform.Services.NewsService.Models
{
    public class OllamaChatRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("messages")]
        public List<OllamaChatMessage> Messages { get; set; } = new();

        // Опциональные параметры, которые поддерживает API
        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        [JsonPropertyName("max_tokens")]
        public int? MaxTokens { get; set; }

        [JsonPropertyName("stop")]
        public string[]? Stop { get; set; }

        [JsonPropertyName("stream")]
        public bool? Stream { get; set; }

        [JsonPropertyName("options")]
        public object? Options { get; set; } // оставил object для гибкости (map / dictionary)
    }

    public class OllamaChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = null!; // "system" | "user" | "assistant"

        [JsonPropertyName("content")]
        public string Content { get; set; } = null!;
    }

    public class OllamaChatResponse
    {
        // Поля, которые возвращает /api/chat в нестримовом режиме
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        // Новый формат API с сообщением
        [JsonPropertyName("message")]
        public OllamaChatMessage? Message { get; set; }

        // Старый формат API (для обратной совместимости)
        [JsonPropertyName("response")]
        public string? Response { get; set; }

        [JsonPropertyName("done")]
        public bool Done { get; set; }

        [JsonPropertyName("done_reason")]
        public string? DoneReason { get; set; }

        [JsonPropertyName("total_duration")]
        public long? TotalDuration { get; set; }

        [JsonPropertyName("load_duration")]
        public long? LoadDuration { get; set; }

        [JsonPropertyName("prompt_eval_count")]
        public int? PromptEvalCount { get; set; }

        [JsonPropertyName("eval_count")]
        public int? EvalCount { get; set; }

        // Дополнительные метаданные, если понадобятся
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalData { get; set; }
    }
}