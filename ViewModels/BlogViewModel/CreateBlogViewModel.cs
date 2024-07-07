using System.ComponentModel.DataAnnotations;

namespace BlogApplication.ViewModels.BlogViewModel;

public class CreateBlogViewModel
{
    [Required(ErrorMessage = "Name must not exceed 100 characters")]
    [StringLength(100)]
    public required string BlogName { get; set; }
    [Required(ErrorMessage = "you did not provide a body")]

    public required string Body { get; set; }

}