using System;
using System.Threading.Tasks;
using CourseKeeper.Models;
using Xamarin.Forms;

namespace CourseKeeper.ViewModels
{
	public class NewTermPageViewModel : BaseViewModel
	{
        private Term _term;
        public Term Term
        {
            get
            {
                return _term;
            }
            set
            {
                _term = value;
                OnPropertyChanged();
            }
        }
		public string Name
        {
            get
            {
                return _term.Name;
            }
            set
            {
                _term.Name = value;
                OnPropertyChanged();
            }
        }
		public string StartDate
        {
            get {
                return _term.StartDate.ToShortDateString();
            }
            set
            {
                _term.StartDate = DateTime.Parse(value);
                OnPropertyChanged();
            }
        }
		public string EndDate
        {
            get
            {
                return _term.EndDate.ToShortDateString();
            }
            set
            {
                _term.EndDate = DateTime.Parse(value);
                OnPropertyChanged();
            }
        }
        public Command SaveCommand { get; set; }

        public NewTermPageViewModel()
        {
            _term = new Term();
            StartDate = DateTime.Now.ToShortDateString();
            EndDate = DateTime.Now.AddMonths(6).ToShortDateString();
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
        }

        async Task ExecuteSaveCommand()
        {
            await App.Database.SaveTermAsync(Term);
            MessagingCenter.Send<NewTermPageViewModel, Term>(this, "AddTerm", Term);
            await App.Current.MainPage.Navigation.PopAsync();
        }
	}
}
