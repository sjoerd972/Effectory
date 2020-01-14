namespace Data.Services
{
	public interface IService<T> : IService
	{
		void Insert(T o);
		void Update(T o);
		void Delete(T o);
		void Upsert(T o);
	}

	public interface IService { }
}
