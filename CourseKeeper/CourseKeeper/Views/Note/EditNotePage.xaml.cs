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
    public partial class EditNotePage : ContentPage
    {

        EditNotePageViewModel viewModel;
        private string _note;
        public string Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public EditNotePage(Course course)
        {
            InitializeComponent();
            _note = course.Notes;
            BindingContext = viewModel = new EditNotePageViewModel(course);
        }
    }
}
