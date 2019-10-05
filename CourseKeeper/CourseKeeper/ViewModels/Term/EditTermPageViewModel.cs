using System;
using System.Threading.Tasks;
using CourseKeeper.Models;
using Xamarin.Forms;

namespace CourseKeeper.ViewModels
{
	public class EditTermPageViewModel : BaseViewModel
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
		public string TermName { 
			get
			{
				return _term.Name;
			} 
			set {
                _term.Name = value;
				OnPropertyChanged();
			} 
		}
        public string StartDate
        {
            get
            {
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
        public Command CancelCommand { get; set; }

        public TermDetailViewModel viewModel;

        public EditTermPageViewModel(Term term)
		{
            _term = term;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            CancelCommand = new Command(async () => await ExecuteTermEditCancelCommand());
        }

        async Task ExecuteSaveCommand()
        {
            await App.Database.SaveTermAsync(Term);
            MessagingCenter.Send<EditTermPageViewModel, Term>(this, "UpdateTerm", Term);
            await App.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteTermEditCancelCommand()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
