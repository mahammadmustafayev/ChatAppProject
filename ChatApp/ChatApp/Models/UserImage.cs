using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Models;

public class UserImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    [NotMapped]
    public IFormFile Image { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
