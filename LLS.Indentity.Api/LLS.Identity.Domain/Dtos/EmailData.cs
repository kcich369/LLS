namespace LLS.Identity.Domain.Dtos;

public class EmailData
{
    public string EmailTo { get; set; }
    public string Subject { get; set; }
    public string HtmlMessage { get; set; }
    public IEnumerable<string> Bccs { get; set; } = new List<string>();
}