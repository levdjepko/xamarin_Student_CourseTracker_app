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
    public partial class NewAssessmentPage : ContentPage
    {
        public NewAssessmentPage(Course course, string assessmentType)
        {
            InitializeComponent();
            BindingContext = new AssessmentViewModel(course, true, assessmentType);
        }
    }
}