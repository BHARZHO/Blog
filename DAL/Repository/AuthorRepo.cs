using AspNetCoreHero.ToastNotification.Abstractions;
using BlogApplication.DAL.Contracts;
using BlogApplication.Data;
using BlogApplication.Data.Dto.Author;
using BlogApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.DAL.Repository;
public class AuthorRepo : IAuthor
{
    private readonly ApplicationDbContext _appDb;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly INotyfService _notyfService;
    private readonly UserManager<User> _userManager;

    public AuthorRepo(ApplicationDbContext _appDb, IHttpContextAccessor httpContextAccessor, INotyfService _notyfService, UserManager<User> userManager)
    {
        this._appDb = _appDb;
        _httpContextAccessor = httpContextAccessor;
        this._notyfService = _notyfService;
        _userManager = userManager;
    }
    public async Task Create(CreateAuthorDto createEntity)
    {
        var currentUser = await Utility.Helper.GetCurrentUserIdAsync(_httpContextAccessor, _userManager);
        var isExistingUser = await GetById(Guid.Parse(currentUser.userId));
        Console.WriteLine(currentUser.userId);

        if (currentUser.userName != null && isExistingUser == null)
        {
            var author = new Author
            {
                Id = Guid.Parse(currentUser.userId),
                Name = currentUser.userName,
            };
            await _appDb.Authors.AddAsync(author);
            await _appDb.SaveChangesAsync();

        }
        else if (isExistingUser != null)
        {
            _notyfService.Warning("Author Exists");

        }
        else
        {
            _notyfService.Warning("You must be authenticated");
        }

    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AuthorDto>?> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<AuthorDto?> GetById(Guid Id)
    {
        var isExistingUser = await _appDb.Authors.FirstOrDefaultAsync(e => e.Id == Id);
        if (isExistingUser != null)
        {
            var author = new AuthorDto(
         Name: isExistingUser!.Name,
         Blogs: isExistingUser.Blogs
        );
            return author;
        }
        return null;

    }

    public async Task<bool> Update(EditAuthorDto editEntity)
    {
        var isExistingUser = await GetById(editEntity.Id);
        var author = new AuthorDto(
         Name: isExistingUser!.Name,
         Blogs: isExistingUser.Blogs
        );
        await _appDb.SaveChangesAsync();
        return true;
    }
}
