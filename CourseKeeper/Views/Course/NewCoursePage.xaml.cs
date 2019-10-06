using System;
using System.Collections.Generic;
using System.ComponentModel;
using CourseKeeper.Models;
using CourseKeeper.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourseKeeper.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[DesignTimeVisible(false)]
	public partial class NewCoursePage : ContentPage
	{

		NewCoursePageViewModel viewModel;
        private Term _term;
		public Term Term
        {
            get { return _term; }
            set
            {
                _term = value;
                OnPropertyChanged();
            }
        }

		public NewCoursePage(Term term)
		{
			InitializeComponent();
            _term = term;
			BindingContext = viewModel = new NewCoursePageViewModel(Term);
		}
	}
}
