namespace BlogApplication.Models;

public class Author
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public IEnumerable<Blog>? Blogs { get; set; }
}
