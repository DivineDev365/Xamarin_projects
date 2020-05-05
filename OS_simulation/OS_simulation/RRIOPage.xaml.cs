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
	public partial class RRIOPage : ContentPage
	{
		public RRIOPage()
		{
			InitializeComponent();
			this.Title = "Round Robin With I/O";
			//if (Device.RuntimePlatform == Device.UWP)
			//	NavigationPage.SetHasNavigationBar(this, false);
		}

		public void RoundRobin(String[] proce, int[] a, int[] b, int[] iot, int n)
		{
			
			int res = 0;
			int resc = 0;

			
			String seq = "0";
			String[] seqlist = new String[a.Length];
			
			int[] res_y = new int[b.Length];
			int[] res_x = new int[a.Length];
			int[] iostart_t = new int[iot.Length];

			for (int i = 0; i < res_y.Length; i++)
			{
				res_y[i] = b[i];
				res_x[i] = a[i];
			}

			 
			int t = 0;

			
			int[] w = new int[proce.Length];

			
			int[] comp = new int[proce.Length];

			while (true)
			{
				Boolean flag = true;
				for (int i = 0; i < proce.Length; i++)
				{
				

					 
					if (res_x[i] <= t)
					{
						if (res_x[i] <= n)
						{
							if (res_y[i] > 0)
							{
								flag = false;
								if (res_y[i] > n)
								{

									
									t = t + n;
									res_y[i] = res_y[i] - n;
									res_x[i] = res_x[i] + n;
									seq += "] " + proce[i] + " [" + t;
									seqlist[i] += proce[i];
								}
								else
								{

									// for last time 
									t = t + res_y[i];

									
									comp[i] = t - a[i];

									
									w[i] = t - b[i] - a[i];
									res_y[i] = 0;

									
									seq += "] " + proce[i] + " [" + t;
									seqlist[i] += proce[i];
								}
							}
						}
						else if (res_x[i] > n)
						{
							 
							for (int j = 0; j < proce.Length; j++)
							{
								// compare 
								if (res_x[j] < res_x[i])
								{
									if (res_y[j] > 0)
									{
										flag = false;
										if (res_y[j] > n)
										{
											t = t + n;
											res_y[j] = res_y[j] - n;
											res_x[j] = res_x[j] + n;
											seq += "] " + proce[j] + "[ " + t;
											seqlist[i] += proce[j];
										}
										else
										{
											t = t + res_y[j];
											comp[j] = t - a[j];
											w[j] = t - b[j] - a[j];
											res_y[j] = 0;
											seq += "] " + proce[j] + " [" + t;
											seqlist[i] += proce[j];
										}
									}
								}
							}
							
							if (res_y[i] > 0)
							{
								flag = false;

								// Check for greaters 
								if (res_y[i] > n)
								{
									t = t + n;
									res_y[i] = res_y[i] - n;
									res_x[i] = res_x[i] + n;
									seq += "] " + proce[i] + " [" + t;
									seqlist[i] += proce[i];
								}
								else
								{
									t = t + res_y[i];
									comp[i] = t - a[i];
									w[i] = t - b[i] - a[i];
									res_y[i] = 0;
									seq += "] " + proce[i] + " [" + t;
									seqlist[i] += proce[i];
								}
							}
						}
					}
					 
					else if (res_x[i] > t)
					{
						t++;
						i--;
					}
				}
				// for exit the while loop 
				if (flag)
				{
					break;
				}
			}
			

			for (int i = 0; i < proce.Length; i++)
			{
				
				w[i] += iot[i];
				res = res + w[i];
				resc = resc + comp[i];
			}

			AWTDisplay.Text = ((double)res / proce.Length).ToString();
			ATTDisplay.Text = ((float)resc / proce.Length).ToString();
			SeqDisplay.Text = seq;
			for(int j=1 ;j<proce.Length+1; j++)
			{
				var c5 = new Label { Text = w[j - 1].ToString() };
				//var c6 = new Label { Text = comp[j-1].ToString() };

				ResultGrid.Children.Add(c5, 4, j);
				//ResultGrid.Children.Add(c6, 5, j);
			}

		}

		async private void FindResultsClicked(object sender, EventArgs e)
		{
			//await DisplayAlert("test1", "Working", "OK");
			try
			{
				int tq = Int32.Parse(TQEntry.Text);
				int[] at = (ATEntry.Text).Split(',').Select(y => Convert.ToInt32(y)).ToArray();
				int[] ct = (CTEntry.Text).Split(',').Select(y => Convert.ToInt32(y)).ToArray();
				int[] iot = (IOTEntry.Text).Split(',').Select(y => Convert.ToInt32(y)).ToArray();
				int m = at.Length;
				int n = ct.Length;
				int o = iot.Length;
				//await DisplayAlert(m.ToString(), n.ToString(), o.ToString());
				if (m != n || n != o || m != o)
				{
					await DisplayAlert("Error", "Number of entries count do not match: "+m.ToString()+", " + n.ToString()+ ", "+ o.ToString(), "OK");
					return;
				}

				string []processes = new string[m];
				for(int i=0; i<m; i++)
				{
					processes[i] = string.Concat("P", (i+1).ToString());
				}

				var r1 = new Label { Text = "PId" };
				var r2 = new Label { Text = "AT" };
				var r3 = new Label { Text = "CPU Time" };
				var r4 = new Label { Text = "I/O Time" };
				var r5 = new Label { Text = "Wait time" };
				var r6 = new Label { Text = "Turn Around Time" };
				ResultGrid.Children.Add(r1, 0, 0);
				ResultGrid.Children.Add(r2, 1, 0);
				ResultGrid.Children.Add(r3, 2, 0);
				ResultGrid.Children.Add(r4, 3, 0);
				ResultGrid.Children.Add(r5, 4, 0);
				//ResultGrid.Children.Add(r6, 5, 0);

				for (int i = 1; i < n+1; i++)
				{
					var c1 = new Label { Text = i.ToString() };
					var c2 = new Label { Text = at[i-1].ToString() };
					var c3 = new Label { Text = ct[i-1].ToString() };
					var c4 = new Label { Text = iot[i-1].ToString() };

					ResultGrid.Children.Add(c1, 0, i);
					ResultGrid.Children.Add(c2, 1, i);
					ResultGrid.Children.Add(c3, 2, i);
					ResultGrid.Children.Add(c4, 3, i);
				}

				RoundRobin(processes, at, ct, iot, tq);
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
			catch(ArgumentNullException)
			{
				await DisplayAlert("Error", "One or more field(s) is/are empty", "OK");
				return;
			}
			
		}
	}
}