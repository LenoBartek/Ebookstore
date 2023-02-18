using Ebookstore.Models;

namespace Ebookstore.Interfaces
{
    public interface IShelfRepository
    {
        int AddToShelf(ShelfModel shelfModel);
        int DeleteFromShelf(ShelfModel shelfModel);
        public List<ShelfModel> ShelfList();
    }
}
