using Front.Models.bModels;

namespace Front.Services;

public interface IActivityService
{
    List<Activity> Activities { get; set; }
    Task GetActivities();
    Task GetEventActivities(string id);
    Task<Activity?> GetActivityById(string id);
    Task CreateActivity(Activity activity, string eventId);
    Task UpdateActivity(Activity activity);
    Task DeleteActivity(string id);
    Task BookParticipant(string activityId);
}