namespace Oxygen
{
	public class DataTransfer
	{
		public static DataTransfer Instance
		{
			get
			{
				return _instance;
			}
		}

		private static DataTransfer _instance;

		private readonly GameStorage _storage;

		public DataTransfer(string name)
		{
			_storage = new GameStorage(name);

			_instance = this;
		}

		public void Write<T>(string name, T value)
		{
			_storage.AddValue<T>(name, value);
		}

		public T Read<T>(string name, T valueByDefault)
		{
			return _storage.GetValue(name, valueByDefault);
		}

		public void Load()
		{
			_storage.Load();
		}

		public void Save()
		{
			_storage.Save();
		}
	}
}
