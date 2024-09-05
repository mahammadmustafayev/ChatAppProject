using ChatApp.Models;

namespace ChatApp.ViewModels.Home;

public class HomeVM
{
    public IEnumerable<AppUser> Users { get; set; }
    public AppUser CurrentUser { get; set; }
}
