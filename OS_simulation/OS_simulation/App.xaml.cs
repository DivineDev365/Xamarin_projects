using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OS_simulation
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var nav = new NavigationPage(new MainPage());
			nav.BarBackgroundColor = Color.Blue;
			nav.BarTextColor = Color.Yellow;
			MainPage = nav;
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
