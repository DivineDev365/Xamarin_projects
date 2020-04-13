using System;
using System.Collections;
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
	public partial class FIFOPage : ContentPage
	{
		public FIFOPage()
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
                int[] pages = (PageEntries.Text).Split(',').Select(y => Convert.ToInt32(y)).ToArray();
                int n = pages.Length;
                //await DisplayAlert(pageEntries[0].ToString(), pageEntries[n-1].ToString(), "OK");
                int capacity = Convert.ToInt32(FSizeEntry.Text);

                List<int> s = new List<int>(capacity);
                Queue indexes = new Queue();
                //int count = 0;
                int page_faults = 0;

                Stopwatch stopwatch = new Stopwatch(); //creates and start the instance of Stopwatch 
                for (int i = 0; i < n; i++)
                {
                    stopwatch.Start();
                    // Check if the set can hold more pages  
                    if (s.Count < capacity)
                    {
                        // Insert it into set if not present  
                        // already which represents page fault  
                        if (!s.Contains(pages[i]))
                        {
                            s.Add(pages[i]);

                            // increment page fault  
                            page_faults++;

                            // Push the current page into the queue  
                            indexes.Enqueue(pages[i]);
                        }
                    }

                    // If the set is full then need to perform FIFO  
                    // i.e. Remove the first page of the queue from  
                    // set and queue both and insert the current page  
                    else
                    {
                        // Check if current page is not already  
                        // present in the set  
                        if (!s.Contains(pages[i]))
                        {
                            //Pop the first page from the queue  
                            int val = (int)indexes.Peek();

                            indexes.Dequeue();

                            // Remove the indexes page  
                            s.Remove(val);

                            // insert the current page  
                            s.Add(pages[i]);

                            // push the current page into  
                            // the queue  
                            indexes.Enqueue(pages[i]);

                            // Increment page faults  
                            page_faults++;
                        }
                    }
                    stopwatch.Stop();
                }
                PFLabel.Text = page_faults.ToString();
                PFSTLabel.Text = stopwatch.Elapsed.ToString() + " seconds";

            }
            catch (NullReferenceException)
            {
                await DisplayAlert("Error", "Please Provide Both Entries", "OK");
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