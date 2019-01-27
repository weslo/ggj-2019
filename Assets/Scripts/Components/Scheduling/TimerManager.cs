namespace Game.Components.Scheduling
{
	public class TimerManager : SchedulingManager<TimerManager>
	{
		public static TimerInstance Schedule(
			float time,
			string id = null)
		{
			return Begin(time, 0, id);
		}
	}
}