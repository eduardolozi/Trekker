using Trekker.Domain.Enums;

namespace Trekker.Domain.Models;

public class ChatActivity
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public ChatActivityEnum Type { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int ChatId { get; set; }
    public Chat Chat { get; set; }
}