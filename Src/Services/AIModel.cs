using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
namespace ChatDeepSeekCoderV2.Services;
#pragma warning disable SKEXP0001, SKEXP0003, SKEXP0010, SKEXP0011, SKEXP0050, SKEXP0052
public class AIModel
{
    public ChatHistory ChatHistory { get; set; } = new ("You are an AI Chat Bot");
    static string ModelID => "deepseek-coder-v2";
    static Uri EndPoint => new("https://localhost:11434");
    static IKernelBuilder Builder => Kernel.CreateBuilder();
    public Kernel ChatKernal => Builder.AddOpenAIChatCompletion(ModelID, EndPoint,"").Build();
    public IAsyncEnumerable<StreamingChatMessageContent> AiUserAgent(string message)
    {
        ChatHistory.AddUserMessage(message);
        return ChatKernal.GetRequiredService<IChatCompletionService>()
            .GetStreamingChatMessageContentsAsync(ChatHistory,null, ChatKernal);
    }
    public IAsyncEnumerable<StreamingChatMessageContent> AiSystemAgent(string message)
    {
        ChatHistory.AddSystemMessage(message);
        return ChatKernal.GetRequiredService<IChatCompletionService>()
            .GetStreamingChatMessageContentsAsync(ChatHistory,null, ChatKernal);
    }
    public IAsyncEnumerable<StreamingChatMessageContent> AiAssistantAgent(string message)
    {
        ChatHistory.AddAssistantMessage(message);
        return ChatKernal.GetRequiredService<IChatCompletionService>()
            .GetStreamingChatMessageContentsAsync(ChatHistory,null, ChatKernal);
    }
}
