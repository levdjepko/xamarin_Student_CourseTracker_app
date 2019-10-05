using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseKeeper.Models;
using CourseKeeper.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourseKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentsDetailPage : ContentPage
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
        private AssessmentViewModel viewModel;
        public AssessmentsDetailPage(Course course)
        {
            InitializeComponent();
            Course = course;
            BindingContext = viewModel = new AssessmentViewModel(Course);
        }
    }
}
