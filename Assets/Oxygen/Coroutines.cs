namespace Oxygen
{
	public class Coroutines : Behaviour
	{
		public static Coroutines Instance
		{
			get
			{
				return _instance;
			}
		}

		private static Coroutines _instance;

		protected override void Awake()
		{
			base.Awake();

			if(_instance != null)
			{
				Destroy(_instance);
			}

			_instance = this;
		}
	}
}