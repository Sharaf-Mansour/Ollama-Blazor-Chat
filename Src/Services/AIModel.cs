using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
namespace OllamaLocalChat.Services;
#pragma warning disable SKEXP0001, SKEXP0003, SKEXP0010, SKEXP0011, SKEXP0050, SKEXP0052
public class AIModel
{
    public List<string> ModelTypes { get; set; } = [ "codegeex4", "deepseek-coder-v2", "qwen2:0.5b", "codestral", "llama3"];
    public ChatHistory ChatHistory { get; set; } = new ("You are an AI Chat Bot");
    public string ModelID { get; set; } = "codegeex4";
    static Uri EndPoint => new("http://localhost:11434");
    static IKernelBuilder Builder => Kernel.CreateBuilder();
    public Kernel? ChatKernalModel { get; set; }
    public void ResetChatHistory() => ChatHistory = new("You are an AI Chat Bot");
    public IAsyncEnumerable<StreamingChatMessageContent> AiUserAgent(string message)
    {
        Kernel ChatKernal() => Builder.AddOpenAIChatCompletion(ModelID, EndPoint, "").Build();
        ChatHistory.AddUserMessage(message);
        return ChatKernal().GetRequiredService<IChatCompletionService>()
            .GetStreamingChatMessageContentsAsync(ChatHistory,null, ChatKernalModel = ChatKernal());
    }
    public IAsyncEnumerable<StreamingChatMessageContent> AiSystemAgent(string message)
    {
        ChatHistory.AddSystemMessage(message);
        Kernel ChatKernal() => Builder.AddOpenAIChatCompletion(ModelID, EndPoint, "").Build();
        return ChatKernal().GetRequiredService<IChatCompletionService>()
            .GetStreamingChatMessageContentsAsync(ChatHistory,null, ChatKernalModel = ChatKernal());
    }
    public IAsyncEnumerable<StreamingChatMessageContent> AiAssistantAgent(string message)
    {
        ChatHistory.AddAssistantMessage(message);
        Kernel ChatKernal() => Builder.AddOpenAIChatCompletion(ModelID, EndPoint, "").Build();
        return ChatKernal().GetRequiredService<IChatCompletionService>()
            .GetStreamingChatMessageContentsAsync(ChatHistory,null, ChatKernalModel = ChatKernal());
    }
}
 