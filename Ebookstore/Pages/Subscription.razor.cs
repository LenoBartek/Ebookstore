using Ebookstore.Models;
using Ebookstore.Repositories;
using Microsoft.AspNetCore.Components;

namespace Ebookstore.Pages
{
    public partial class Subscription
    {
        [Inject]
        public ShelfRepository? _shelfRepository { get; set; }
        [Inject]
        public EbooksRepository _ebooksRepository { get; set; }
        private List<ShelfModel> ShelfList { get; set; }
        private List<EbooksModel> EBooksStoreList { get; set; }
        private List<EbooksModel> _EBooksStoreList { get; set; }
        public EbooksFilter ebooksFilter { get; set; } = new EbooksFilter();
        public ShelfModel _model { get; set; }
        private ShelfModel SelectedModel { get; set; } = new ShelfModel();

        private EbooksModel SelectedEbookModel { get; set; } = new EbooksModel();
        public PageState State { get; set; } = PageState.None;
        private string? _filename { get; set; } = "";
        protected override Task OnInitializedAsync()
        {
            ReadShelf();
            return base.OnInitializedAsync();
        }
        private void ReadShelf()
        {
            ShelfList = _shelfRepository.ShelfList();
        }

        public void ViewPdf(ShelfModel item)
        {
            SelectedModel = item;
            _filename = SelectedModel.path + "\\" + SelectedModel.filename;
            State = PageState.Preview;
        }

        public void ViewEbookPdf(EbooksModel item)
        {
            SelectedEbookModel = item;
            _filename = SelectedEbookModel.path + "\\" + SelectedEbookModel.filename;
            State = PageState.Preview;
        }

        public void OnClosePdf()
        {
            State = PageState.None;
        }

        public void AddEbooks()
        {
            State = PageState.Add;
            ebooksFilter.ResetFilter();
            ReadEbooks();
            ebooksFilter.PropertyChanged += EbooksFilter_PropertyChanged;             
        }

        private void EbooksFilter_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            EBooksStoreList = _ebooksRepository.EbooksList(ebooksFilter);
            _EBooksStoreList = EBooksStoreList;
        }

        private void ReadEbooks()
        {
            EBooksStoreList = _ebooksRepository.EbooksList(ebooksFilter);
            _EBooksStoreList = EBooksStoreList;
        }

        public void OnCheckChanged(EbooksModel item)
        {
            if (item.IsChecked)
                item.IsChecked = false;
            else
                item.IsChecked = true;
        }

        public void AddToSubscr()
        {
            foreach (var item in EBooksStoreList)
            {
                if (item.IsChecked)
                {
                    ShelfModel newShelfItem = new ShelfModel();
                    newShelfItem.ebook = item.id;
                    newShelfItem.userid = 1;
                    newShelfItem.created_date = DateTime.Now;
                    ShelfList.Add(newShelfItem);
                    _shelfRepository.AddToShelf(newShelfItem);
                }
            }
            State = PageState.None;
            ReadShelf();
        }

        private void OnRowDelete(ShelfModel item)
        {

            SelectedModel = item;
            _shelfRepository.DeleteFromShelf(SelectedModel);
            ReadShelf();
        }

        private void BackToMySubscriptions()
        {
            State = PageState.None;
        }
    }
}
