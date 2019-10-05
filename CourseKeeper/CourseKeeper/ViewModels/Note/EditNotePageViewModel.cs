using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseKeeper.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CourseKeeper.ViewModels
{
    public class EditNotePageViewModel : BaseViewModel
    {
        private Course _course;
        public string Notes
        {
            get { return _course.Notes; }
            set { _course.Notes = value; OnPropertyChanged(); }
        }
        public Course Course
        {
            get
            {
                return _course;
            }
            set
            {
                _course = value;
                OnPropertyChanged();
            }
        }
        public string CourseName
        {
            get
            {
                return _course.Name;
            }
        }

        public Command SaveNoteCommand { get; set; }
        public Command CancelEditCommand { get; set; }
        public Command ShareNotesCommand { get; set; }

        public EditNotePageViewModel(Course course)
        {
            _course = course;
            SaveNoteCommand = new Command(async () => await ExecuteSaveNoteCommand());
            CancelEditCommand = new Command(async () => await ExecuteCancelEditCommand());
            ShareNotesCommand = new Command(async () => await ExecuteShareNotesCommand());
        }

        async Task ExecuteSaveNoteCommand()
        {
            await App.Database.SaveCourseAsync(Course);
            await App.Current.MainPage.Navigation.PopAsync();
            MessagingCenter.Send(this, "UpdateCourse", Course);
        }

        async Task ExecuteCancelEditCommand()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteShareNotesCommand()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = Notes,
                Title = $"Notes for: {CourseName}"
            });

        }

    }
}
