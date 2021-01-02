using Com.OneSignal;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mh.Ui.Xamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            //OneSignal.Current.StartInit("7ef09f6a-db22-4a86-8119-3af60f2eefbb").EndInit();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    public class StartLongRunningTaskMessage { }

    public class StopLongRunningTaskMessage { }
}
