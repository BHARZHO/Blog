using BlogApplication.Models;

namespace BlogApplication.Data.Dto.Author;
public record class AuthorDto(
         string Name,
         IEnumerable<Blog>? Blogs
);
