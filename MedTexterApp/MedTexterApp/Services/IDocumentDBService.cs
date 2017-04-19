using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedTexterApp
{
	public interface IDocumentDBService
	{
		Task<bool> LoginAsync(Xamarin.Forms.Page page);

		Task SaveLoginItemAsync(LoginItem item, bool isNewItem);		
	}
}
