using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OS_simulation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LRUPage : ContentPage
	{
		public LRUPage()
		{
			InitializeComponent();
            this.Title = "LRU Page Replacement";
			//switch (Device.RuntimePlatform)
			//{
			//	case Device.UWP:
			//		NavigationPage.SetHasNavigationBar(this, false);
			//		break;

			//}
		}

		async private void FindResultsClicked(object sender, EventArgs e)
		{
            try
            {
                int[] pageEntries = (PageEntries.Text).Split(',').Select(y => Convert.ToInt32(y)).ToArray();
                int n = pageEntries.Length;
                //await DisplayAlert(pageEntries[0].ToString(), pageEntries[n-1].ToString(), "OK");
                int capacity = Convert.ToInt32(FSizeEntry.Text);
                
                List<int> s = new List<int>(capacity);
                int count = 0;
                int page_faults = 0;

                Stopwatch stopwatch = new Stopwatch(); //creates and start the instance of Stopwatch           

                foreach (int i in pageEntries)
                {
                    // Insert it into set if not present already which represents page fault  
                    if (!s.Contains(i))
                    {
                        stopwatch.Start();
                        // Check if the set can hold equal pages  
                        if (s.Count == capacity)
                        {
                            s.RemoveAt(0);
                            s.Insert(capacity - 1, i);
                        }
                        else
                            s.Insert(count, i);

                        // Increment page faults  
                        page_faults++;
                        ++count;
                        stopwatch.Stop();
                    }
                    else
                    {
                        // Remove the indexes page  
                        s.Remove(i);

                        // insert the current page  
                        s.Insert(s.Count, i);
                    }
                }
                PFLabel.Text = page_faults.ToString();
                PFSTLabel.Text = stopwatch.Elapsed.ToString() + " seconds";
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("Error", "Please Provide Both Entries", "OK");
                return;
            }
            catch(FormatException)
            {
                await DisplayAlert("Error", "Please Provide Only Integer Values", "OK");
                return;
            }

            
        }
	}
}