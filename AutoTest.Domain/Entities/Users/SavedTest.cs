using System.ComponentModel.DataAnnotations.Schema;

namespace AutoTest.Domain.Entities.Users;

public class SavedTest : BaseEntity
{
    [Column("user_id")]
    public required long UserId { get; set; }
    [Column("user")]
    public User User { get; set; }
    [Column("test_id")]
    public required long TestId { get; set; }
    [Column("test")]
    public long Test { get; set; }    
}
