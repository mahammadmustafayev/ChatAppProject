using Microsoft.AspNetCore.Identity;

namespace ChatApp.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public UserImage Image { get; set; }
    public string ConnectionId { get; set; } = string.Empty;
}
