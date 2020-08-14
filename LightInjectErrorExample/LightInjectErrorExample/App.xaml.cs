using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

using LightInject;

using Xamarin.Forms;

namespace LightInjectErrorExample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var container = new ServiceContainer();

            // This form changes the ToolbarItems from an ObservableCollection<> to a List<>
            // which then causes Xamarin.Forms to throw an InvalidCastException
            container.RegisterSingleton<MainPage>();
            // This form doesn't fail 
            container.RegisterSingleton(_ => new SecondPage());

            var mainPage = container.GetInstance<MainPage>();
            var secondPage = container.GetInstance<SecondPage>();

            var isSecondPageToolbarItemsAnObservableCollection = secondPage.ToolbarItems is ObservableCollection<ToolbarItem>;
            var isMainPageToolbarItemsAnObservableCollection = mainPage.ToolbarItems is ObservableCollection<ToolbarItem>;

            if (Debugger.IsAttached)
                Debugger.Break();

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
