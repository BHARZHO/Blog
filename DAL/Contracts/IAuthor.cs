using BlogApplication.Data.Dto.Author;

namespace BlogApplication.DAL.Contracts;
public interface IAuthor : IBase<AuthorDto, CreateAuthorDto, EditAuthorDto>
{

}
