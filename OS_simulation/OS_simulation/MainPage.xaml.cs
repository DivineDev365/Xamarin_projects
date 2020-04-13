using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OS_simulation
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}

		async private void RRClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RRPage());
		}

		async private void LRUClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LRUPage());
		}
	
		async private void MEMFCFSClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new FIFOPage());
		}

		async private void BestFitClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new BestFitPage());
		}

		async private void RRIOClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RRIOPage());
		}
	}
}
