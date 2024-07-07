namespace BlogApplication.ViewModels.BlogViewModel;
class BlogIndexList
{
    public IEnumerable<BlogPostViewModel> Blogs { get; set; } = default!;
}
