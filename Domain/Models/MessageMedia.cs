namespace Domain.Models;

public class MessageMedia
{
    public int Id { get; set; }
    public string Path { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    
    public int MessageId { get; set; }
    public Message Message { get; set; }
}