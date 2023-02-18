using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ebookstore.Models
{
    public class EbooksModel
    {
        public int id { get; set; }
        public string? authors { get; set; }
        public string? title { get; set; }
        public int pages { get; set; }
        public string? path { get; set; }
        public string? filename { get; set; }
        public int publication_year { get; set; }
        public DateTime added_date { get; set; }
        public bool IsChecked { get; set; } = false;
    }
    public class ObservableProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged(object sender, [CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
    }

    public class EbooksFilter : ObservableProperty
    {

        private string? _authors;
        public string? authors
        {
            get
            {
                return _authors;
            }
            set
            {
                _authors = value;
                NotifyPropertyChanged(this);
            }
        }

        private string? _title;
        public string? title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged(this);
            }
        }

        private int _pages;
        public int pages
        {
            get
            {
                return _pages;
            }
            set
            {
                _pages = value;
                NotifyPropertyChanged(this);
            }
        }

        private string? _path;
        public string? path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                NotifyPropertyChanged(this);
            }
        }

        private string? _filename;
        public string? filename
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
                NotifyPropertyChanged(this);
            }
        }

        private int _publication_year;
        public int publication_year
        {
            get
            {
                return _publication_year;
            }
            set
            {
                _publication_year = value;
                NotifyPropertyChanged(this);
            }
        }

        private DateTime? _added_date;
        public DateTime? added_date
        {
            get
            {
                return _added_date;
            }
            set
            {
                _added_date = value;
                NotifyPropertyChanged(this);
            }
        }

        private int _RowsPerPage = 10;
        public int RowsPerPage
        {
            get
            {
                return _RowsPerPage;
            }
            set
            {
                _RowsPerPage = value;
                NotifyPropertyChanged(this);
            }
        }

        public void ResetFilter()
        {
            authors = "";
            title = "";
            pages = 100;
            path = "";
            publication_year = 0;
            added_date = DateTime.Now.Date;
        }
    }

}
