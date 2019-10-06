using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourseKeeper.Models;
using CourseKeeper.Views;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace CourseKeeper.ViewModels
{
	public class CourseDetailViewModel : BaseViewModel
	{
        private Course _course;
		public Course Course {
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
		public string Name {
            get
            {
                return _course.Name;
            }
            set
            {
                _course.Name = value;
                OnPropertyChanged();
            }
        }
		public string StartDate
        {
            get
            {
                return _course.StartDate.ToShortDateString();
            }
            set
            {
                _course.StartDate = DateTime.Parse(value);
                OnPropertyChanged();
            }
        }
		public string EndDate
        {
            get
            {
                return _course.EndDate.ToShortDateString();
            }
            set
            {
                _course.EndDate = DateTime.Parse(value);
                OnPropertyChanged();
            }
        }
		public string CurrentStatus
        {
            get
            {
                return _course.Status;
            }
            set
            {
                _course.Status = value;
                OnPropertyChanged();
            }
        }
        public string InstructorName
        {
            get
            {
                return _course.InstructorName;
            }
            set
            {
                _course.InstructorName = value;
                OnPropertyChanged();
            }
        }
		public string InstructorPhone
        {
            get
            {
                return _course.InstructorPhone;
            }
            set
            {
                _course.InstructorPhone = value;
                OnPropertyChanged();
            }
        }
        public string InstructorEmail
        {
            get
            {
                return _course.InstructorEmail;
            }
            set
            {
                _course.InstructorEmail = value;
                OnPropertyChanged();
            }
        }
        public string Notes
        {
            get
            {
                return _course.Notes;
            }
            set
            {
                _course.Notes = value;
                OnPropertyChanged();
            }
        }
		public bool Notifications
        {
            get
            {
                return _course.Notifications;
            }
            set
            {
                _course.Notifications = value;
                OnPropertyChanged();
            }
        }

        public Command EditCourseCommand { get; set; }
        public Command DeleteCourseCommand { get; set; }
        public Command EditNotesCommand { get; set; }
        public Command ShareNotesCommand { get; set; }
        public Command ManageAssessmentsCommand { get; set; }


        public CourseDetailViewModel(Course course)
		{
            _course = course;
            EditCourseCommand = new Command(async () => await ExecuteEditCourseCommand());
            DeleteCourseCommand = new Command(async () => await ExecuteDeleteCourseCommand());
            EditNotesCommand = new Command(async () => await ExecuteEditNotesCommand());
            ManageAssessmentsCommand = new Command(async () => await ExecuteManageAssessmentsCommand());

            MessagingCenter.Subscribe<EditCoursePageViewModel, Course>(this, "UpdateCourse", (sender, obj) =>
            {
                Course = obj;
                RaiseAllProperties();
            });
        }

        async Task ExecuteEditNotesCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditNotePage(Course));
        }

        async Task ExecuteDeleteCourseCommand()
        {
            await App.Database.DeleteCourseAsync(Course);
            MessagingCenter.Send<CourseDetailViewModel, Course>(this, "DeleteCourse", Course);
            await App.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteManageAssessmentsCommand()
        {
            try
            {
                await App.Current.MainPage.Navigation.PushAsync(new AssessmentsDetailPage(Course));
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        async Task ExecuteEditCourseCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditCoursePage(this));
        }
    }
}
