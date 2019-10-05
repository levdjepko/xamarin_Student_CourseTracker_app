using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CourseKeeper.Models;
using CourseKeeper.ViewModels;

namespace CourseKeeper.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class NewTermPage : ContentPage
    {
        public NewTermPage()
        {
            InitializeComponent();
            NewTermPageViewModel viewModel;
            BindingContext = viewModel = new NewTermPageViewModel(); ;
        }
    }
}