using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CourseKeeper.Models;
using CourseKeeper.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseKeeper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class TermDetailPage : ContentPage
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

		public TermDetailPage(Term term)
        {
            InitializeComponent();
            _term = term;
            BindingContext = new TermDetailViewModel(term);
        }
        
		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Course;
			if (item == null)
				return;

			await Navigation.PushAsync(new CourseDetailPage(item));

            // Manually de-select item.
			CourseListView.SelectedItem = null;
		}

		//protected override void OnAppearing()
		//{
		//	base.OnAppearing();
		//	viewModel.LoadItemsCommand.Execute(null);
		//}

	}
}