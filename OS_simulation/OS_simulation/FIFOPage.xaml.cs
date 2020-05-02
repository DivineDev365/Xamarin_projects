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
            this.Title = "FIFO Page Replacement";
			//if (Device.RuntimePlatform == Device.UWP)
			//	NavigationPage.SetHasNavigationBar(this, false);
			
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
                    
                    if (s.Count < capacity)
                    {
                        
                        if (!s.Contains(pages[i]))
                        {
                            s.Add(pages[i]);

                           
                            page_faults++;

                            
                            indexes.Enqueue(pages[i]);
                        }
                    }

                   
                    else
                    {
                        
                        if (!s.Contains(pages[i]))
                        {
                            
                            int val = (int)indexes.Peek();

                            indexes.Dequeue();

                           
                            s.Remove(val);

                           
                            s.Add(pages[i]);

                         
                            indexes.Enqueue(pages[i]);

                           
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