using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PhoneMediApp.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace PhoneMediApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MeasurePage : Page
    {
        public MeasurePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var id = e.Parameter as int?;
            var data = await DataSources.GetItemAsync(id.GetValueOrDefault());
            pulse.Text = data.Pulse.ToString();
            lowPres.Text = data.LowPressure.ToString();
            highPres.Text = data.LowPressure.ToString();
            temp.Text = data.Temp.ToString();
            date.Text = data.Date.Date.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
