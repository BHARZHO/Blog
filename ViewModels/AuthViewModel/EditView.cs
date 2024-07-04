using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlogApp.ViewModels.AuthViewModel;

public class EditView
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Username is required!")]
    public required string Username { get; set; }
    [Required(ErrorMessage = "Email is required!")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required!")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    //[DataType(DataType.PhoneNumber)]
    //[DisplayName("Phone Number")]
    //public required string Phone { get; set; }
}
