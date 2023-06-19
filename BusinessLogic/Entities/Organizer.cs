namespace BusinessLogic.Entities;

public class Organizer : User
{
    public ICollection<Event> EventsCreated = new List<Event>();
}