using System;
using System.Collections.Generic;
using System.ComponentModel;
using CourseKeeper.Models;
using CourseKeeper.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CourseKeeper.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[DesignTimeVisible(false)]
	public partial class EditCoursePage : ContentPage
	{

		EditCoursePageViewModel viewModel;
		public Course Course { get; set; }

		public EditCoursePage(CourseDetailViewModel vm)
		{
			InitializeComponent();
			Course = vm.Course;
			BindingContext = viewModel = new EditCoursePageViewModel(Course);
		}
	}
}
