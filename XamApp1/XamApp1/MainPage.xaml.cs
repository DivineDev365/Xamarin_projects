using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace XamApp1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        int count = 0;
       public async Task Speak(String s)
        {
            await TextToSpeech.SpeakAsync(s);
        }
       // public  void Inter(String s)
        //{
         //   await Speak(s);
        //}
        async void TextButton_Clicked(object sender, System.EventArgs e)
        {   
            if(!String.IsNullOrEmpty(Text))
              await Speak(Text);
        }
        public String Text;
        void saveText(object sender, System.EventArgs e)
        {
            Text = ((Editor)sender).Text;
        }
        void Button_Clicked(object sender, System.EventArgs e)
        {
            count++;
            ((Button)sender).Text = $"You clicked {count} times.";
        }
    }
}
