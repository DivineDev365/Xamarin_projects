using  System;
using  System.Collections.Generic;
using  System.Linq;
using  System.Text;
using  System.Threading.Tasks;

using  Xamarin.Forms;
using  Xamarin.Forms.Xaml;


namespace  OS_simulation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public  partial  class  RRPage  :  ContentPage
	{
		public  RRPage()
		{
			InitializeComponent();
			this.Title  =  "Round  Robin";
			//switch  (Device.RuntimePlatform)
			//{
			//	case  Device.UWP:
			//		NavigationPage.SetHasNavigationBar(this,  false);
			//		break;

			//}
		}


		public  void  RoundRobin(String[]  proce,  int[]  x,  int[]  y,    int  n)
		{
			  
			int  res  =  0;
			int  resc  =  0;

			  
			String  seq  =  "0";
			String[]  seqlist  =  new  String[x.Length];
			
			int[]  res_y  =  new  int[y.Length];
			int[]  res_x  =  new  int[x.Length];

			for  (int  i  =  0;  i  <  res_y.Length;  i++)
			{
				res_y[i]  =  y[i];
				res_x[i]  =  x[i];
			}

			  
			int  t  =  0;

			  
			int[]  w  =  new  int[proce.Length];

			
			int[]  comp  =  new  int[proce.Length];

			while  (true)
			{
				Boolean  flag  =  true;
				for  (int  i  =  0;  i  <  proce.Length;  i++)
				{

					  
					if  (res_x[i]  <=  t)
					{
						if  (res_x[i]  <=  n)
						{
							if  (res_y[i]  >  0)
							{
								flag  =  false;
								if  (res_y[i]  >  n)
								{

									
									t  =  t  +  n;
									res_y[i]  =  res_y[i]  -  n;
									res_x[i]  =  res_x[i]  +  n;
									seq  +=  "]  "  +  proce[i]  +  "  ["  +  t;
									seqlist[i]  +=    proce[i];
								}
								else
								{

									//last  iter
									t  =  t  +  res_y[i];

									
									comp[i]  =  t  -  x[i];

									
									w[i]  =  t  -  y[i]  -  x[i];
									res_y[i]  =  0;

									
									seq  +=  "]  "  +  proce[i]  +  "  ["  +  t;
									seqlist[i]  +=  proce[i];
								}
							}
						}
						else  if  (res_x[i]  >  n)
						{

							
							for  (int  j  =  0;  j  <  proce.Length;  j++)
							{

								
								if  (res_x[j]  <  res_x[i])
								{
									if  (res_y[j]  >  0)
									{
										flag  =  false;
										if  (res_y[j]  >  n)
										{
											t  =  t  +  n;
											res_y[j]  =  res_y[j]  -  n;
											res_x[j]  =  res_x[j]  +  n;
											seq  +=  "]  "  +  proce[j]  +  "[  "  +  t;
											seqlist[i]  +=    proce[j];
										}
										else
										{
											t  =  t  +  res_y[j];
											comp[j]  =  t  -  x[j];
											w[j]  =  t  -  y[j]  -  x[j];
											res_y[j]  =  0;
											seq  +=  "]  "  +  proce[j]  +  "  ["  +  t;
											seqlist[i]  +=    proce[j];
										}
									}
								}
							}

							
							if  (res_y[i]  >  0)
							{
								flag  =  false;

								
								if  (res_y[i]  >  n)
								{
									t  =  t  +  n;
									res_y[i]  =  res_y[i]  -  n;
									res_x[i]  =  res_x[i]  +  n;
									seq  +=  "]  "  +  proce[i]  +  "  ["  +  t;
									seqlist[i]  +=  proce[i];
								}
								else
								{
									t  =  t  +  res_y[i];
									comp[i]  =  t  -  x[i];
									w[i]  =  t  -  y[i]  -  x[i];
									res_y[i]  =  0;
									seq  +=  "]  "  +  proce[i]  +  "  ["  +  t;
									seqlist[i]  +=  proce[i];
								}
							}
						}
					}

					
					else  if  (res_x[i]  >  t)
					{
						t++;
						i--;
					}
				}

				
				if  (flag)
				{
					break;
				}
			}


			
			for  (int  i  =  0;  i  <  proce.Length;  i++)
			{
				

				res  =  res  +  w[i];
				resc  =  resc  +  comp[i];
			}

			

			AWTDisplay.Text  =  ((double)res  /  proce.Length).ToString();
			ATTDisplay.Text  =  ((float)resc  /  proce.Length).ToString();
			SeqDisplay.Text  =  seq;
			
		}



		async  private  void  FindResultsClicked(object  sender,  EventArgs  e)
		{
			String[]  processes  =  {  "P1",  "P2",  "P3",  "P4",  "P5"  };
			
			try
			{  
				int[]  arr_time  =  {  Int32.Parse(P1ATEntry.Text),  Int32.Parse(P2ATEntry.Text),  Int32.Parse(P3ATEntry.Text),  Int32.Parse(P4ATEntry.Text),  Int32.Parse(P5ATEntry.Text)  };
				int[]  cpu_time  =  {  Int32.Parse(P1CTEntry.Text),  Int32.Parse(P2CTEntry.Text),  Int32.Parse(P3CTEntry.Text),  Int32.Parse(P4CTEntry.Text),  Int32.Parse(P5CTEntry.Text)  };
				
				int  quantum  =  Int32.Parse(TQEntry.Text);
				RoundRobin(processes,  arr_time,  cpu_time,  quantum);
			}
			catch  (NullReferenceException)
			{
				// doc at https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/pop-ups
				await DisplayAlert("Error",  "One  or  more  field(s)  is/are  empty",  "OK");
				return;
			}
			catch(ArgumentNullException)
			{
				await  DisplayAlert("Error",  "One  or  more  field(s)  is/are  empty",  "OK");
				return;
			}
			catch  (FormatException)
			{
				await  DisplayAlert("Error",  "Please  Provide  Only  Integer  Values",  "OK");
				return;
			}
			
		}
	}
}