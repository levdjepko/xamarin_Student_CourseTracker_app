using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseKeeper.Models;
using Xamarin.Forms;

namespace CourseKeeper.ViewModels
{
	public class NewCoursePageViewModel : BaseViewModel
	{
        private Course _course;
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
        public string Name
        {
            get { return _course.Name; }
            set { _course.Name = value; OnPropertyChanged(); }
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
            get { return _course.Status; }
            set { _course.Status = value; OnPropertyChanged(); }
        }
        public List<string> StatusList { get; } = new List<string> { "Enrolled", "Not Enrolled" };
        public string InstructorName
        {
            get { return _course.InstructorName; }
            set { _course.InstructorName = value; OnPropertyChanged(); }
        }
        public string InstructorPhone
        {
            get { return _course.InstructorPhone; }
            set { _course.InstructorPhone = value; OnPropertyChanged(); }
        }
        public string InstructorEmail
        {
            get { return _course.InstructorEmail; }
            set { _course.InstructorEmail = value; OnPropertyChanged(); }
        }
        public bool Notifications
        {
            get { return _course.Notifications; }
            set { _course.Notifications = value; OnPropertyChanged(); }
        }
        public int TermID
        {
            get
            {
                return _course.TermID;
            }
            set
            {
                _course.TermID = value;
                OnPropertyChanged();
            }
        }
        public Command SaveCourseCommand { get; set; }
        public Command CancelEditCommand { get; set; }

        public NewCoursePageViewModel(Term term)
		{
            _course = new Course();
			TermID = term.ID;
            StartDate = DateTime.Now.ToShortDateString();
            EndDate = DateTime.Now.AddMonths(6).ToShortDateString();
            //addition of assessments, 10-05-2019
            //following here

            /* var course = new Course()
            {
                Name = "Mathematics",
                StartDate = DateTime.Parse("09/11/2019"),
                EndDate = DateTime.Parse("12/31/2019"),
                Status = "Enrolled",
                InstructorName = "Lev Dzhepko",
                InstructorPhone = "402-301-3167",
                InstructorEmail = "ldzhepk@wgu.edu",
                Notifications = true,
                Notes = "This project is tough",
                TermID = 1
            }; */
            var assessment1 = new Assessment()
            {
                AssessmentType = "Performance",
                StartDate = DateTime.Parse("09/01/2019"),
                EndDate = DateTime.Parse("12/31/2019"),
                Name = "Type performance assessment name",
                CourseID = _course.ID,
                Notifications = true
            };
            var assessment2 = new Assessment()
            {
                AssessmentType = "Objective",
                StartDate = DateTime.Parse("09/13/2019"),
                EndDate = DateTime.Parse("12/31/2019"),
                Name = "Type objective assessment name",
                CourseID = _course.ID,
                Notifications = true
            };
            //App.Database.SaveCourseAsync(course);
            App.Database.SaveAssessmentAsync(assessment1);
            App.Database.SaveAssessmentAsync(assessment2);

            //up to here. TEST IT
            SaveCourseCommand = new Command(async () => await ExecuteSaveCourseCommand());
            CancelEditCommand = new Command(async () => await App.Current.MainPage.Navigation.PopAsync());
        }

        public bool checkValues(Course course)
        {
            return course.Name != null && 
                course.InstructorName != null &&
                course.InstructorPhone != null &&
                course.InstructorEmail != null &&
                course.Status != null;
        }
        async Task ExecuteSaveCourseCommand()
        {
            if (checkValues(Course))
            {
                await App.Database.SaveCourseAsync(Course);
                SetNotify(Notifications, "CourseKeeper", $"{Name} is ending at {EndDate}", "Course", Course.ID, DateTime.Parse(EndDate).AddHours(-36));
                await App.Current.MainPage.Navigation.PopAsync();
                MessagingCenter.Send<NewCoursePageViewModel, Course>(this, "AddCourse", Course);
            }
            else {
                App.Current.MainPage.DisplayAlert("Alert", "Fields cannot be left blank, please supply all values", "OK");
                return;
            }
        }
    }
}
