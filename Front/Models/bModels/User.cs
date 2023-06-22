namespace Front.Models.bModels;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int PhoneNumber { get; set; }
    public ICollection<Activity>? Activities { get; set; } = new List<Activity>();
    public ICollection<Event>? EventsCreated { get; set; } = new List<Event>();
    
    // this can either be Regular/UserManager/Admin
    public string Role = null!; 
}