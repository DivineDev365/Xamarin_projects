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

		public void RoundRobin(String[] p, int[] a, int[] b, int[] iot, int n)
		{
			// result of average times 
			int res = 0;
			int resc = 0;

			// for sequence storage 
			String seq = "0";
			String[] seqlist = new String[a.Length];
			// copy the burst array and arrival array 
			// for not effecting the actual array 
			int[] res_b = new int[b.Length];
			int[] res_a = new int[a.Length];
			int[] iostart_t = new int[iot.Length];

			for (int i = 0; i < res_b.Length; i++)
			{
				res_b[i] = b[i];
				res_a[i] = a[i];
			}

			// critical time of system 
			int t = 0;

			// for store the waiting time 
			int[] w = new int[p.Length];

			// for store the Completion time 
			int[] comp = new int[p.Length];

			while (true)
			{
				Boolean flag = true;
				for (int i = 0; i < p.Length; i++)
				{
					// these condition for if 
					// arrival is not on zero 

					// check that if there come before qtime 
					if (res_a[i] <= t)
					{
						if (res_a[i] <= n)
						{
							if (res_b[i] > 0)
							{
								flag = false;
								if (res_b[i] > n)
								{

									// make decrease the b time 
									t = t + n;
									res_b[i] = res_b[i] - n;
									res_a[i] = res_a[i] + n;
									seq += "] " + p[i] + " [" + t;
									seqlist[i] += p[i];
								}
								else
								{

									// for last time 
									t = t + res_b[i];

									// store comp time 
									comp[i] = t - a[i];

									// store wait time 
									w[i] = t - b[i] - a[i];
									res_b[i] = 0;

									// add sequence 
									seq += "] " + p[i] + " [" + t;
									seqlist[i] += p[i];
								}
							}
						}
						else if (res_a[i] > n)
						{
							// is any have less arrival time 
							// the coming process then execute them 
							for (int j = 0; j < p.Length; j++)
							{
								// compare 
								if (res_a[j] < res_a[i])
								{
									if (res_b[j] > 0)
									{
										flag = false;
										if (res_b[j] > n)
										{
											t = t + n;
											res_b[j] = res_b[j] - n;
											res_a[j] = res_a[j] + n;
											seq += "] " + p[j] + "[ " + t;
											seqlist[i] += p[j];
										}
										else
										{
											t = t + res_b[j];
											comp[j] = t - a[j];
											w[j] = t - b[j] - a[j];
											res_b[j] = 0;
											seq += "] " + p[j] + " [" + t;
											seqlist[i] += p[j];
										}
									}
								}
							}
							// now the previous porcess according to 
							// ith is process 
							if (res_b[i] > 0)
							{
								flag = false;

								// Check for greaters 
								if (res_b[i] > n)
								{
									t = t + n;
									res_b[i] = res_b[i] - n;
									res_a[i] = res_a[i] + n;
									seq += "] " + p[i] + " [" + t;
									seqlist[i] += p[i];
								}
								else
								{
									t = t + res_b[i];
									comp[i] = t - a[i];
									w[i] = t - b[i] - a[i];
									res_b[i] = 0;
									seq += "] " + p[i] + " [" + t;
									seqlist[i] += p[i];
								}
							}
						}
					}
					// if no process is come on thse critical 
					else if (res_a[i] > t)
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
			//for(int i=0; i<p.Length; i++)
			//{	iostart_t[i] = w[i] + comp[i];		}

			for (int i = 0; i < p.Length; i++)
			{
				//Console.WriteLine(" " + p[i] + "\t" + comp[i] + "\t" + w[i]);z
				w[i] += iot[i];
				res = res + w[i];
				resc = resc + comp[i];
			}

			AWTDisplay.Text = ((double)res / p.Length).ToString();
			ATTDisplay.Text = ((float)resc / p.Length).ToString();
			SeqDisplay.Text = seq;
			for(int j=1 ;j<p.Length+1; j++)
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