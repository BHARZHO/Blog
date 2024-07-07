using System.ComponentModel;

namespace BlogApplication.ViewModels.BlogViewModel;

public class BlogPostViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Body { get; set; }
    [DisplayName("Date Published")]
    public DateTime dateTime { get; set; }
    [DisplayName("Written By")]
    public required string AuthorName { get; set; }
}
