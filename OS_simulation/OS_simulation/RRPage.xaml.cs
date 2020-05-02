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
	public partial class RRPage : ContentPage
	{
		public RRPage()
		{
			InitializeComponent();
			this.Title = "Round Robin";
			//switch (Device.RuntimePlatform)
			//{
			//	case Device.UWP:
			//		NavigationPage.SetHasNavigationBar(this, false);
			//		break;

			//}
		}


		public void RoundRobin(String[] p, int[] a, int[] b,  int n)
		{
			 
			int res = 0;
			int resc = 0;

			 
			String seq = "0";
			String[] seqlist = new String[a.Length];
			
			int[] res_b = new int[b.Length];
			int[] res_a = new int[a.Length];

			for (int i = 0; i < res_b.Length; i++)
			{
				res_b[i] = b[i];
				res_a[i] = a[i];
			}

			 
			int t = 0;

			 
			int[] w = new int[p.Length];

			
			int[] comp = new int[p.Length];

			while (true)
			{
				Boolean flag = true;
				for (int i = 0; i < p.Length; i++)
				{

					 
					if (res_a[i] <= t)
					{
						if (res_a[i] <= n)
						{
							if (res_b[i] > 0)
							{
								flag = false;
								if (res_b[i] > n)
								{

									
									t = t + n;
									res_b[i] = res_b[i] - n;
									res_a[i] = res_a[i] + n;
									seq += "] " + p[i] + " [" + t;
									seqlist[i] +=  p[i];
								}
								else
								{

									// for last time 
									t = t + res_b[i];

									
									comp[i] = t - a[i];

									
									w[i] = t - b[i] - a[i];
									res_b[i] = 0;

									
									seq += "] " + p[i] + " [" + t;
									seqlist[i] += p[i];
								}
							}
						}
						else if (res_a[i] > n)
						{

							
							for (int j = 0; j < p.Length; j++)
							{

								
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
											seqlist[i] +=  p[j];
										}
										else
										{
											t = t + res_b[j];
											comp[j] = t - a[j];
											w[j] = t - b[j] - a[j];
											res_b[j] = 0;
											seq += "] " + p[j] + " [" + t;
											seqlist[i] +=  p[j];
										}
									}
								}
							}

							
							if (res_b[i] > 0)
							{
								flag = false;

								
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

					
					else if (res_a[i] > t)
					{
						t++;
						i--;
					}
				}

				
				if (flag)
				{
					break;
				}
			}


			
			for (int i = 0; i < p.Length; i++)
			{
				

				res = res + w[i];
				resc = resc + comp[i];
			}

			

			AWTDisplay.Text = ((double)res / p.Length).ToString();
			ATTDisplay.Text = ((float)resc / p.Length).ToString();
			SeqDisplay.Text = seq;
			
		}



		async private void FindResultsClicked(object sender, EventArgs e)
		{
			String[] processes = { "P1", "P2", "P3", "P4", "P5" };
			
			try
			{ 
				int[] arr_time = { Int32.Parse(P1ATEntry.Text), Int32.Parse(P2ATEntry.Text), Int32.Parse(P3ATEntry.Text), Int32.Parse(P4ATEntry.Text), Int32.Parse(P5ATEntry.Text) };
				int[] cpu_time = { Int32.Parse(P1CTEntry.Text), Int32.Parse(P2CTEntry.Text), Int32.Parse(P3CTEntry.Text), Int32.Parse(P4CTEntry.Text), Int32.Parse(P5CTEntry.Text) };
				
				int quantum = Int32.Parse(TQEntry.Text);
				RoundRobin(processes, arr_time, cpu_time, quantum);
			}
			catch (NullReferenceException)
			{
				await DisplayAlert("Error", "One or more field(s) is/are empty", "OK");
				return;
			}
			catch(ArgumentNullException)
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