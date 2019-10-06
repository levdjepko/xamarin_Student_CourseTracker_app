using System;
using System.ComponentModel;
using System.Collections.Generic;
using CourseKeeper.Models;
using System.Threading.Tasks;
using SQLite;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using CourseKeeper.Views;
using System.Diagnostics;

namespace CourseKeeper.ViewModels
{
    public class TermDetailViewModel : BaseViewModel
    {
        private Term _term;
        private bool _showCourseLabel = false;
        private bool _addButtonEnabled = true;
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

        #region Props
        public string TermName
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
        public ObservableCollection<Course> CourseList { get; set; }
        public bool ShowCourseLabel
        {
            get
            {
                return _showCourseLabel;
            }
            set
            {
                _showCourseLabel = value;
                OnPropertyChanged();
            }
        }
        public bool AddButtonEnabled
        {
            get
            {
                return _addButtonEnabled;
            }
            set
            {
                _addButtonEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public Command LoadItemsCommand { get; set; }
        public Command EditTermCommand { get; set; }
        public Command DeleteTermCommand { get; set; }
        public Command AddCourseCommand { get; set; }
        #endregion

        public TermDetailViewModel(Term term)

        {
            _term = term;
            CourseList = new ObservableCollection<Course>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            EditTermCommand = new Command(async () => await ExecuteEditTermCommand());
            DeleteTermCommand = new Command(async () => await ExecuteDeleteTermCommand());
            AddCourseCommand = new Command(async () => await ExecuteAddCourseCommand());
            GetCourses();

            MessagingCenter.Subscribe<NewCoursePageViewModel, Course>(this, "AddCourse", (sender, obj) =>
            {
                CourseList.Add(obj);
                AddButtonEnabled = CourseList.Count >= 6 ? false : true;
                RaiseAllProperties();
            });
            MessagingCenter.Subscribe<EditTermPageViewModel, Term>(this, "UpdateTerm", (sender, obj) =>
            {
                Term = obj;
                RaiseAllProperties();
            });
            MessagingCenter.Subscribe<CourseDetailViewModel, Course>(this, "DeleteCourse", (sender, obj) =>
            {
                CourseList.Remove(obj);
                AddButtonEnabled = CourseList.Count >= 6 ? false : true;
                RaiseAllProperties();
            });
        }

        private async void GetCourses()
        {
            List<Course> courses = await App.Database.GetCoursesAsync(_term);
            foreach (Course course in courses)
            {
                CourseList.Add(course);
            }
            if (CourseList.Count > 0)
            {
                ShowCourseLabel = true;
            }
            AddButtonEnabled = CourseList.Count >= 6 ? false : true;
            RaiseAllProperties();

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                CourseList.Clear();
                var items = await App.Database.GetCoursesAsync(Term);
                foreach (var item in items)
                {
                    CourseList.Add(item);
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
        async Task ExecuteEditTermCommand()
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditTermPage(Term));
        }
        async Task ExecuteDeleteTermCommand()
        {
            var answer = await App.Current.MainPage.DisplayAlert("Delete?", "Are you sure you want to delete this item?", "Yes", "No");
            if (answer)
            {
                await App.Database.DeleteTermAsync(_term);
                MessagingCenter.Send<TermDetailViewModel, Term>(this, "TermDelete", _term);
                await App.Current.MainPage.Navigation.PopToRootAsync();
            }
        }
        async Task ExecuteAddCourseCommand()
        {

            await App.Current.MainPage.Navigation.PushAsync(new NewCoursePage(Term));
        }

    }
}
