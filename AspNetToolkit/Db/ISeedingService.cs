using System.Threading.Tasks;

namespace AspNetToolkit.Db {
	public interface ISeedingService {
		Task InitialiseDatabase(bool runMigrations);
	}
}