using BlogApplication.Data.Dto.Author;
using BlogApplication.ViewModels.BlogViewModel;

namespace BlogApplication.DAL.Contracts;

public interface IBlogRepo
{
Task<IEnumerable<BlogPostViewModel>>? GetBlog();
// get all blogs
Task<BlogPostViewModel?> GetBlog(Guid id);
}
