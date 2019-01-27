using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Components.Utility;

namespace Game.Components.Scheduling
{
	public abstract class SchedulingManager<T> : PersistentSingletonMonoBehaviour<T>
		where T : SchedulingManager<T>
	{
		public class TimerInstance
		{
			public bool Begun
			{
				get;
				private set;
			}

			public bool Completed
			{
				get;
				private set;
			}

			private float duration;

			private float time;

			public object ID
			{
				get;
				private set;
			}

			private Action<float> step;

			private Action onBegin;

			private Action onComplete;

			public TimerInstance(
				float duration,
				float delay = 0,
				object id = null)
			{
				this.duration = duration;
				time = -delay;
				ID = id;
			}

			internal void Update(float dt)
			{
				if(Completed)
				{
					return;
				}

				time += dt;

				if(!Begun && time >= 0)
				{
					Begun = true;
					if(onBegin != null)
					{
						onBegin();
					}
				}

				if(time >= duration)
				{
					time = duration;
					Completed = true;
				}

				if(step != null)
				{
					step(GetInterpolation());
				}

				if(Completed)
				{
					if(onComplete != null)
					{
						onComplete();
					}
				}
			}

			public TimerInstance OnStep(Action<float> step)
			{
				this.step += step;
				return this;
			}

			public TimerInstance OnBegin(Action onBegin)
			{
				this.onBegin += onBegin;
				return this;
			}

			public TimerInstance OnComplete(Action onComplete)
			{
				this.onComplete += onComplete;
				return this;
			}

			private float GetInterpolation()
			{
				return Mathf.Clamp(time, 0, duration) / duration;
			}
		}

		private List<TimerInstance> timers = new List<TimerInstance>();

		void Update()
		{
			TimerInstance[] toUpdate = new TimerInstance[timers.Count];
			timers.CopyTo(toUpdate);
			foreach(TimerInstance timer in toUpdate)
			{
				timer.Update(Time.deltaTime);
				if(timer.Completed)
				{
					timers.Remove(timer);
				}
			}
		}

		protected static TimerInstance Begin(float duration, float delay = 0, object id = null)
		{
			TimerInstance timer = new TimerInstance(duration, delay, id);
			Instance.timers.Add(timer);
			return timer;
		}

		public static void Cancel(object id)
		{
			TimerInstance[] toUpdate = new TimerInstance[Instance.timers.Count];
			Instance.timers.CopyTo(toUpdate);
			foreach(TimerInstance timer in toUpdate)
			{
				if(timer.ID != null && timer.ID.Equals(id))
				{
					Instance.timers.Remove(timer);
				}
			}
		}
	}
}