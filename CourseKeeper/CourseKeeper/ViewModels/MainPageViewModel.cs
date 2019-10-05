using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using CourseKeeper.Models;
using CourseKeeper.Views;
using System.Collections.Generic;

namespace CourseKeeper.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<Term> _terms;
        public ObservableCollection<Term> Terms
        {
            get
            {
                return _terms;
            }
            set
            {
                _terms = value;
                OnPropertyChanged();
            }
        }
        public Command LoadItemsCommand { get; set; }
        public Command AddTermCommand { get; set; }

        public MainPageViewModel()
        {
            Title = "CourseKeeper";
            _terms = new ObservableCollection<Term>();
           	LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddTermCommand = new Command(async () => await ExecuteAddTermCommand());
            if (App.Database.GetTermsAsync().Result.Count == 0)
            {
                DoSetup();
            }
            PopulateTerms();

            MessagingCenter.Subscribe<NewTermPageViewModel, Term>(this, "AddTerm", (sender, obj) =>
            {
				Terms.Add(obj);
            });
			MessagingCenter.Subscribe<TermDetailViewModel, Term>(this, "TermDelete", (sender, obj) =>
			{
				Terms.Remove(obj);
			});
            MessagingCenter.Subscribe<NewTermPageViewModel>(this, "TermUpdate", async (obj) =>
            {
                await ExecuteLoadItemsCommand();
            });

        }

        private async void PopulateTerms()
		{
			List<Term> terms = await App.Database.GetTermsAsync();
            Terms.Clear();
			foreach (Term term in terms)
			{
				Terms.Add(term);
			}
		}

        async Task ExecuteAddTermCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewTermPage());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Terms.Clear();
                var items = await App.Database.GetTermsAsync();
                foreach (var item in items)
                {
                    Terms.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void DoSetup()
        {
            var term = new Term()
            {
                //ID = 1,
                Name = "Term 1",
                StartDate = DateTime.Parse("09/01/2019"),
                EndDate = DateTime.Parse("12/31/2019")
            };
            var course = new Course()
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
            };
            var assessment1 = new Assessment()
            {
                AssessmentType = "Performance",
                StartDate = DateTime.Parse("09/01/2019"),
                EndDate = DateTime.Parse("12/31/2019"),
                Name = "Math test",
                CourseID = 1,
                Notifications = true
            };
            var assessment2 = new Assessment()
            {
                AssessmentType = "Objective",
                StartDate = DateTime.Parse("09/13/2019"),
                EndDate = DateTime.Parse("12/31/2019"),
                Name = "Objective test",
                CourseID = 1,
                Notifications = true
            };
            App.Database.SaveTermAsync(term);
            App.Database.SaveCourseAsync(course);
            App.Database.SaveAssessmentAsync(assessment1);
            App.Database.SaveAssessmentAsync(assessment2);
            SetNotify(course.Notifications, "CourseKeeper", $"{course.Name} is ending at {course.EndDate}", "Course", course.ID, course.EndDate.AddHours(-36));
            SetNotify(assessment1.Notifications, "CourseKeeper", $"{assessment1.Name} is ending at {assessment1.EndDate}", "Course", assessment1.ID, assessment1.EndDate.AddHours(-36));
            SetNotify(assessment2.Notifications, "CourseKeeper", $"{assessment2.Name} is ending at {assessment2.EndDate}", "Course", assessment2.ID, assessment2.EndDate.AddHours(-36));
        }
    }
}