using Ebookstore.Models;

namespace Ebookstore.Interfaces
{
	public interface IEbooksRepository
	{	
		int AddEbooks(EbooksModel ebooksModel);
		int DeleteEbooks(EbooksModel ebooksModel);
		int EditEbooks(EbooksModel ebooksModel);
		public List<EbooksModel> EbooksList(EbooksFilter filter);

	}
}
