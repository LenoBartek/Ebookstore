using Ebookstore.Repositories;
using Ebookstore.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Ebookstore.Pages
{
    public partial class Ebooks
    {
        [Inject]
        public EbooksRepository? _ebooksRepository { get; set; }
        private List<EbooksModel> EBooksStoreList { get; set; }
        private List<EbooksModel> _EBooksStoreList { get; set; }
        public EbooksFilter ebooksFilter { get; set; } = new EbooksFilter();
        public EbooksModel _model { get; set; }
        private EbooksModel SelectedModel { get; set; } = new EbooksModel();
        public PageState State { get; set; } = PageState.None;
        private string? _filename { get; set; } = "";

        public static string networkPath = "ebooks";
        static readonly DirectoryInfo TempDir = new DirectoryInfo(networkPath);

        private List<IBrowserFile> loadedFiles = new();
        private long maxFileSize = 1024 * 10000;
        private int maxAllowedFiles = 100;
        private bool isLoading;
        private decimal progressPercent;
        protected override Task OnInitializedAsync()
        {
            ebooksFilter.ResetFilter();
            ReadEbooks();
            ebooksFilter.PropertyChanged += EbooksFilter_PropertyChanged;
            return base.OnInitializedAsync();
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

        private async Task LoadFiles(InputFileChangeEventArgs e)
        {
            try
            {

                isLoading = true;
                loadedFiles.Clear();
                progressPercent = 0;
                var pa = Path.GetTempFileName();
                var di = TempDir.FullName;


                foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
                {
                    try
                    {
                        var trustedFileName = file.Name;
                        string filename = di + "\\" + trustedFileName;
                        var path = filename;

                        await using FileStream writeStream = new(path, FileMode.Create);
                        using var readStream = file.OpenReadStream(maxFileSize);
                        var bytesRead = 0;
                        var totalRead = 0;
                        var buffer = new byte[1024 * 10000];

                        while ((bytesRead = await readStream.ReadAsync(buffer)) != 0)
                        {
                            totalRead += bytesRead;

                            await writeStream.WriteAsync(buffer, 0, bytesRead);

                            progressPercent = Decimal.Divide(totalRead, file.Size);
                        }

                        loadedFiles.Add(file);
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }


                for (int i = 0; i < loadedFiles.Count; i++)
                {
                    EbooksModel ebooksModel = new EbooksModel();

                    ebooksModel.title = loadedFiles[i].Name.Replace(".pdf", "");
                    ebooksModel.filename = loadedFiles[i].Name;
                    ebooksModel.path = di;
                    ebooksModel.added_date = DateTime.Now;

                    EBooksStoreList.Add(ebooksModel);
                    _ebooksRepository.AddEbooks(ebooksModel);
                }
                ReadEbooks();

                isLoading = false;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void OnEditRow(EbooksModel item)
        {
            SelectedModel = item;
            State = PageState.Edit;
        }
        private void OnCloseEdit()
        {
            State = PageState.None;
        }
        private void OnSaveEdit()
        {
            _ebooksRepository.EditEbooks(SelectedModel);
            State = PageState.None;
        }

        private void OnRowDelete(EbooksModel item)
        {
            SelectedModel = item;
            _ebooksRepository.DeleteEbooks(SelectedModel);
            ReadEbooks();
        }

        public void ViewPdf(EbooksModel item)
        {
            SelectedModel = item;
            _filename = SelectedModel.path + "\\" + SelectedModel.filename;
            State = PageState.Preview;
        }

        public void OnClosePdf()
        {
            State = PageState.None;
        }


    }
}
