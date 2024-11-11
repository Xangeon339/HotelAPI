using System.Timers;

namespace HotelAPI.Tasks
{
    public interface IRabbitMQScheduleTask
    {
        void OnTimedEvent(object source, ElapsedEventArgs e);
        void StartTimer();
    }
}
