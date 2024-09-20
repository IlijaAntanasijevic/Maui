using Maui.Common;
using Maui.ViewModels;

namespace Maui.Pages;

public partial class TabPage : TabbedPage
{
	public TabPage()
	{
		InitializeComponent();

        var user = SecureStorage.Default.GetUser();


        if (!user.IsAdmin)
        {
            var adminPage = this.Children.FirstOrDefault(page => page is Admin);
            if (adminPage != null)
            {
                this.Children.Remove(adminPage);
            }
        }


    }

    private void Logout_Loaded(object sender, EventArgs e)
    {
        SecureStorage.Default.Remove("token");
        App.Current.MainPage = new LoginPage();
    }

}