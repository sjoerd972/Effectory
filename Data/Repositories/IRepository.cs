using System.Collections.Generic;

namespace Data.Repositories
{
	public interface IRepository<T>:IRepository
	{
		T Get(int id);
		IList<T> GetAll();
	}

	public interface IRepository { }
}
