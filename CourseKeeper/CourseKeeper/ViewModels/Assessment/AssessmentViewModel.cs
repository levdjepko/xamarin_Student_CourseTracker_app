using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseKeeper.Models;
using CourseKeeper.Views;
using Xamarin.Forms;

namespace CourseKeeper.ViewModels
{
    class AssessmentViewModel : BaseViewModel
    {
        private Assessment _assessment;
        private Course _course;
        private string _performanceAssessmentName;
        private string _objectiveAssessmentName;
        public Assessment Assessment
        {
            get
            {
                return _assessment;
            }
            set
            {
                _assessment = value;
                OnPropertyChanged();
            }
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
        public string Name
        {
            get
            {
                return _assessment.Name;
            }
            set
            {
                _assessment.Name = value;
                OnPropertyChanged();
            }
        }
        public string StartDate
        {
            get
            {
                return _assessment.StartDate.ToShortDateString();
            }
            set
            {
                _assessment.StartDate = DateTime.Parse(value);
                OnPropertyChanged();
            }
        }
        public string EndDate
        {
            get
            {
                return _assessment.EndDate.ToShortDateString();
            }
            set
            {
                _assessment.EndDate = DateTime.Parse(value);
                OnPropertyChanged();
            }
        }
        public string AssessmentType
        {
            get
            {
                return _assessment.AssessmentType;
            }
            set
            {
                _assessment.AssessmentType = value;
                OnPropertyChanged();
            }
        }
        public bool Notifications
        {
            get
            {
                return _assessment.Notifications;
            }
            set
            {
                _assessment.Notifications = value;
                OnPropertyChanged();
            }
        }
        public int CourseID
        {
            get
            {
                return _course.ID;
            }
            set
            {
                _course.ID = value;
                OnPropertyChanged();
            }
        }
        public Assessment ObjectiveAssessment { get; set; }
        public string ObjectiveAssessmentName
        {
            get
            {
                if (ObjectiveAssessment == null)
                {
                    return "Add Objective Assessment";
                }
                else { return ObjectiveAssessment.Name; }
            }
        }
        public Assessment PerformanceAssessment { get; set; }
        public string PerformanceAssessmentName
        {
            get
            {
                if (PerformanceAssessment == null)
                {
                    return "Add Performance Assessment";
                }
                else { return PerformanceAssessment.Name; }
            }
        }
        public Command SaveAssessmentCommand { get; set; }
        public Command CancelCommand { get; set; }
        public Command EditAssessmentCommand { get; set; }
        public Command DeleteAssessmentCommand { get; set; }
        public Command OAButtonCommand { get; set; }
        public Command PAButtonCommand { get; set; }

        // Base for creating commands
        public AssessmentViewModel()
        {
            SaveAssessmentCommand = new Command(async () => await ExecuteSaveAssessmentCommand());
            CancelCommand = new Command(async () => await App.Current.MainPage.Navigation.PopAsync());
            EditAssessmentCommand = new Command(async () => await ExecuteEditAssessmentCommand());
            DeleteAssessmentCommand = new Command(async () => await ExecuteDeleteAssessmentCommand());
            //Command for the Objective Assessment Button
            OAButtonCommand = new Command(async () => await ExecuteOAButtonCommand());
            // Command for the Performance Assessment Button
            PAButtonCommand = new Command(async () => await ExecutePAButtonCommand());

            MessagingCenter.Subscribe<AssessmentViewModel, Assessment>(this, "UpdateAssessment", (sender, obj) =>
            {
                if (obj.AssessmentType == "Performance")
                {
                    PerformanceAssessment = obj;
                }
                else
                {
                    ObjectiveAssessment = obj;
                }
                RaiseAllProperties();
            });
            MessagingCenter.Subscribe<AssessmentViewModel, Assessment>(this, "DeleteAssessment", (sender, obj) =>
            {
                if (obj.AssessmentType == "Performance")
                {
                    PerformanceAssessment = null;
                }
                else
                {
                    ObjectiveAssessment = null;
                }
                RaiseAllProperties();
            });

        }
        // For modifying or displaying EditAssessementPage
        public AssessmentViewModel(Assessment assessment)
            : this()
        {
            Assessment = assessment;
        }

        // For creating new NewAssessmentPage
        public AssessmentViewModel(Course course, bool isNew, string assessmentType)
            : this()
        {
            Assessment = new Assessment();
            Course = course;
            Assessment.CourseID = Course.ID;
            Assessment.AssessmentType = assessmentType;
            StartDate = DateTime.Now.ToShortDateString();
            EndDate = DateTime.Now.AddMonths(6).ToShortDateString();
        }

        // For displaying list of both assessments: AssessmentDetailPage
        public AssessmentViewModel(Course course)
            : this()
        {
            Course = course;


            GetAssessmentObjective(course.ID);
            GetAssessmentPerformance(course.ID);
        }

        public async Task GetAssessmentObjective(int courseID)
        {
            ObjectiveAssessment = await App.Database.GetAssessmentObjective(courseID) ?? MakeNewAssessment("Objective");
            //ObjectiveAssessmentName = ObjectiveAssessment.Name;
            RaiseAllProperties();
        }

        public async Task GetAssessmentPerformance(int courseID)
        {
            PerformanceAssessment = await App.Database.GetAssessmentPerformance(courseID) ?? MakeNewAssessment("Performance");
            //PerformanceAssessmentName = PerformanceAssessment.Name;
            RaiseAllProperties();
        }

        private Assessment MakeNewAssessment(string assessmentType)
        {
            Assessment assessment = new Assessment();
            assessment.CourseID = CourseID;
            assessment.AssessmentType = assessmentType;
            return assessment;
        }

        async Task ExecuteOAButtonCommand()
        {
            if (ObjectiveAssessment is null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new EditAssessmentPage(Course, "Objective"));
            } else
            {
                await App.Current.MainPage.Navigation.PushAsync(new EditAssessmentPage(ObjectiveAssessment));
            }
        }
        
        async Task ExecutePAButtonCommand()
        {
            if (PerformanceAssessment is null)
            {
                await App.Current.MainPage.Navigation.PushAsync(new EditAssessmentPage(Course, "Performance"));
            }
            else
            {
                await App.Current.MainPage.Navigation.PushAsync(new EditAssessmentPage(PerformanceAssessment));
            }
        }

        async Task ExecuteSaveAssessmentCommand()
        {
            await App.Database.SaveAssessmentAsync(Assessment);
            SetNotify(Notifications, "CourseKeeper", $"{Name} is ending at {EndDate}", "Course", Course.ID, DateTime.Parse(EndDate).AddHours(-36));
            MessagingCenter.Send<AssessmentViewModel, Assessment>(this, "UpdateAssessment", Assessment);
            await App.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteEditAssessmentCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditAssessmentPage(Assessment));
        }
        async Task ExecuteDeleteAssessmentCommand()
        {
            await App.Database.DeleteAssessmentAsync(Assessment);
            MessagingCenter.Send<AssessmentViewModel, Assessment>(this, "DeleteAssessment", Assessment);
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
