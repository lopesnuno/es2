namespace Front.Models.bModels;

public class Organizer : User
{
    public ICollection<Event> EventsCreated = new List<Event>();
}