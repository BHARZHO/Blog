using BlogApplication.ViewModels.BlogViewModel;

namespace BlogApplication.DAL.Contracts;

public interface IAdmin : IBase<BlogPostViewModel, CreateBlogViewModel, EditBlogViewModel>
{

}
