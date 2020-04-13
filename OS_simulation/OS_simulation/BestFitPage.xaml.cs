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
            if (Device.RuntimePlatform == Device.UWP)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
        }

		async private void FindResultsClicked(object sender, EventArgs e)
		{
            try
            {
                int[] blockSize = (BlockSizeEntry.Text).Split(',').Select(y => Convert.ToInt32(y)).ToArray();
                int[] processSize = (ProcessSizeEntry.Text).Split(',').Select(y => Convert.ToInt32(y)).ToArray();
                int m = blockSize.Length;
                int n = processSize.Length;
           

                int[] allocation = new int[n];
			    for (int i = 0; i < allocation.Length; i++)// Initially no block is assigned to any process 
				    allocation[i] = -1;

                for (int i = 0; i < n; i++)
                {
                    // Find the best fit block for 
                    // current process 
                    int bestIdx = -1;
                    for (int j = 0; j < m; j++)
                    {
                        if (blockSize[j] >= processSize[i])
                        {
                            if (bestIdx == -1)
                                bestIdx = j;
                            else if (blockSize[bestIdx]
                                           > blockSize[j])
                                bestIdx = j;
                        }
                    }
                    // If we could find a block for 
                    // current process 
                    if (bestIdx != -1)
                    {

                        // allocate block j to p[i]  
                        // process 
                        allocation[i] = bestIdx;

                        // Reduce available memory in 
                        // this block. 
                        blockSize[bestIdx] -= processSize[i];
                    }
                }

                for(int i=0; i<n; i++)
                {
                    var c1 = new Label { Text = (i+1).ToString() };
                    var c2 = new Label { Text = processSize[i].ToString() };
                    var c3 = new Label { Text = (allocation[i]+1).ToString() };

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