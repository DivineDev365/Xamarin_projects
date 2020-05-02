using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OS_simulation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage()
		{
			InitializeComponent();
			this.Title = "About";
		}

		

		async private void OpenLinkedInUrl(object sender, EventArgs e)
		{
			try
			{
				string url = "https://www.linkedin.com/in/shashwat-b-5871a3175";
				var link = new Uri(url);
				await Browser.OpenAsync(link, BrowserLaunchMode.SystemPreferred);
			}
			catch
			{
				await DisplayAlert("Error", "Invlid Url", "OK");
			}
		}

		async private void OpenGithubUrl(object sender, EventArgs e)
		{
			try
			{
				string url = "https://github.com/Shashwat422/Xamarin_projects/tree/master/OS_simulation";
				var link = new Uri(url);
				await Browser.OpenAsync(link, BrowserLaunchMode.SystemPreferred);
			}
			catch
			{
				await DisplayAlert("Error", "Invlid Url", "OK");
			}
		}
	}
}