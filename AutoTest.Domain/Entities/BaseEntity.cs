using System.ComponentModel.DataAnnotations;

namespace AutoTest.Domain.Entities;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
