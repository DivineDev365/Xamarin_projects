using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OS_simulation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BestFitPage : ContentPage
	{
		public BestFitPage()
		{
			InitializeComponent();
            this.Title = "Best Fit";
            //if (Device.RuntimePlatform == Device.UWP)
            //    NavigationPage.SetHasNavigationBar(this, false);
           
        }

		async private void FindResultsClicked(object sender, EventArgs e)
		{
            try
            {
                int[] bsize = (BlockSizeEntry.Text).Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                int[] psize = (ProcessSizeEntry.Text).Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                int m = bsize.Length;
                int n = psize.Length;
           

                int[] alloted = new int[n];
			    for (int i = 0; i < alloted.Length; i++)
				    alloted[i] = -1;

                for (int i = 0; i < n; i++)
                {
                   
                    int bestIdx = -1;
                    for (int j = 0; j < m; j++)
                    {
                        if (bsize[j] >= psize[i])
                        {
                            if (bestIdx == -1)
                                bestIdx = j;
                            else if (bsize[bestIdx]
                                           > bsize[j])
                                bestIdx = j;
                        }
                    }
                    
                    if (bestIdx != -1)
                    {

                        
                        alloted[i] = bestIdx;

                        
                        bsize[bestIdx] -= psize[i];
                    }
                }

                for(int i=0; i<n; i++)
                {
                    var c1 = new Label { Text = (i+1).ToString() };
                    var c2 = new Label { Text = psize[i].ToString() };
                    var c3 = new Label { Text = (alloted[i]+1).ToString() };

                    ResultGrid.Children.Add(c1, 0, i);
                    ResultGrid.Children.Add(c2, 1, i);
                    ResultGrid.Children.Add(c3, 2, i);
                }
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("Error", "One or more field(s) is/are empty", "OK");
                return;
            }
            catch (FormatException)
            {
                await DisplayAlert("Error", "Please Provide Only Integer Values", "OK");
                return;
            }

        }
	}
}