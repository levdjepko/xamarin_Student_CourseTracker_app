using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using CourseKeeper.Models;
using CourseKeeper.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseKeeper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
	public partial class CourseDetailPage : ContentPage
	{
        CourseDetailViewModel viewModel;
		public Course Course { get; set; }

        public CourseDetailPage(Course course)
        {
            InitializeComponent();
            Course = course;
            BindingContext = viewModel = new CourseDetailViewModel(Course);
        }
	}
}