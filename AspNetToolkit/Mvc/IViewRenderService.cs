using System.Threading.Tasks;

namespace AspNetToolkit.Mvc {
	public interface IViewRenderService {
		Task<string> RenderToStringAsync(string viewName, object model);
	}
}
