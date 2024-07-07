using System.ComponentModel.DataAnnotations;


namespace BlogApplication.Models;

public class Blog
{
    [Key]
    public Guid ID { get; set; }
    [StringLength(100)]
    [Required(ErrorMessage = "Name is a Required Field")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "Body is a Required Field")]
    public required string Body { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }
    public Guid AuthorId { get; set; }

    public Author Author { get; set; }
}
