namespace Game.Components.Scheduling
{
	public class TweenManager : SchedulingManager<TweenManager>
	{
		public static TimerInstance Tween(
			float duration,
			float delay = 0,
			string id = null)
		{
			return Begin(duration, delay, id);
		}
	}
}