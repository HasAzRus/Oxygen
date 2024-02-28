namespace Oxygen
{
	public class Coroutines : Behaviour
	{
		public static Coroutines Instance
		{
			get
			{
				return s_instance;
			}
		}

		private static Coroutines s_instance;

		protected override void Awake()
		{
			base.Awake();

			if(s_instance != null)
			{
				Destroy(s_instance);
			}

			s_instance = this;
		}
	}
}