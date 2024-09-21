using Maui.Common;
using Maui.Pages;

namespace Maui
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            Services = serviceProvider;

            var user = SecureStorage.Default.GetUser();
            Utility.IsRegistration = false;

            if (user != null)
            {
                MainPage = new NavigationPage(new TabPage());
            }
            else
            {
                MainPage = new LoginPage();
            }

            //MainPage = new LoginPage();

        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 400;
            const int newHeight = 850;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }
    }
}
