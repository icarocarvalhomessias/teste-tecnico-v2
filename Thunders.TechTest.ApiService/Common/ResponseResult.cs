namespace Thunders.TechTest.ApiService.Common;

/// <summary>
/// Classe base para respostas de requisições.
/// </summary>
public class ResponseResult
{
    public ResponseResult()
    {
        Errors = new ResponseErrorMessages();
    }

    public string Title { get; set; }
    public int Status { get; set; }
    public ResponseErrorMessages Errors { get; set; }
}

public class ResponseErrorMessages
{
    public ResponseErrorMessages()
    {
        Mensagens = new List<string>();
    }

    public List<string> Mensagens { get; set; }
}