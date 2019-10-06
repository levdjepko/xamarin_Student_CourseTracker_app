using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseKeeper.ViewModels;
using CourseKeeper.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourseKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class EditAssessmentPage : ContentPage
    {
        private Assessment _assessment;

        public EditAssessmentPage(Assessment assessment)
        {
            InitializeComponent();
            BindingContext = new AssessmentViewModel(assessment);
        }

        public EditAssessmentPage(Course course, string assessmentType)
        {
            InitializeComponent();
            BindingContext = new AssessmentViewModel(course, true, assessmentType);
        }

    }
}