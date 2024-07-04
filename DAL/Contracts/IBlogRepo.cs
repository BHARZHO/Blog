using BlogApp.Dto.Author;
using BlogApp.ViewModels.BlogViewModel;

namespace BlogApp.DAL.Contracts;

public interface IBlogRepo
{
Task<IEnumerable<BlogPostViewModel>>? GetBlog();
// get all blogs
Task<BlogPostViewModel?> GetBlog(Guid id);
}
