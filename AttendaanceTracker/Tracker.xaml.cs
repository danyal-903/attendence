using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SystemUtilities;
using RestSharp;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AttendaanceTracker
{
    /// <summary>
    /// Interaction logic for Tracker.xaml
    /// </summary>
    public partial class Tracker : Window
    {

        DispatcherTimer t;
        DateTime start;

        bool inProgressBreak;

        DateTime lastInteraction;
        int idleTime;
        dynamic m_events = new TrackMouseEvents();

        bool breakAdded;

        int auto_break_threshold;
        public Tracker()
        {

            InitializeComponent();
            idleTime = 0;
            breakAdded = false;
            inProgressBreak = false;
            dynamic user_shift = Application.Current.Properties["user_shift"];
            auto_break_threshold = user_shift.auto_break_threshold;
            t = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 50), DispatcherPriority.Background,
                t_Tick, Dispatcher.CurrentDispatcher); 
            t.IsEnabled = true;

            start = DateTime.Now;
            dynamic data = Application.Current.Properties["user"];
            
            this.userDetails.Text = data.first_name + ' ' + data.last_name;
            
            lastInteraction = UnixTimeStampToDateTime(m_events.GetLastInputTime());
            LastInteraction.Text = lastInteraction.ToString();


        }

        private async void checkBreak()
        {
            
                dynamic _break = this.AddBreak();
                if (_break.success == true)
                {
                    inProgressBreak = false;
                    breakAdded = true;
                    Application.Current.Properties.Add("time_off", _break.data);
                }
                if (_break.success == false)
                {
                    inProgressBreak = true;
                }
            
        }

        private async void t_Tick(object sender, EventArgs e)
        {

            if (Application.Current.Properties.Contains("time_off"))
            {
                breakAdded = true;
            }
            if (Application.Current.Properties.Contains("user"))
            {
                loggedInTimer.Text = Convert.ToString(DateTime.Now - start);

                lastInteraction = UnixTimeStampToDateTime(m_events.GetLastInputTime());
                LastInteraction.Text = lastInteraction.ToString();
                idleTime = (int)(m_events.GetIdleTime() / 1000);
                InteractionDifference.Text = idleTime.ToString() + " second(s)";



                //breaks logic here.
                if (idleTime >= auto_break_threshold * 60 && !breakAdded && !inProgressBreak)
                {
                    try
                    {
                        dynamic _break = this.AddBreak();
                        if (_break.success == true)
                        {
                            breakAdded = true;
                            Application.Current.Properties.Add("time_off", _break.data);
                        }
                        if (_break.success == false)
                        {
                            inProgressBreak = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.LogOut(true);
                    }
                }
                if (idleTime < 60 && breakAdded)
                {
                    try
                    {
                        dynamic b = this.EndBreak("end-break");
                        if (b.success == true)
                        {
                            breakAdded = false;
                            Application.Current.Properties.Remove("time_off");
                        }
                    }
                    catch (Exception ex)
                    {
                        this.LogOut(true);
                    }
                }
                if (breakAdded && idleTime>60)
                {
                    this.checkBreak();
                }

            }
            else
            {
                t.Stop();
            }
        }

        private void LogOut(bool check=false)
        {

            if (check)
            {
                MessageBox.Show("API not working. Server down. Contact system admin.", "Error: Server Down", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Application.Current.Properties.Remove("user");
            Application.Current.Properties.Remove("token");
            Application.Current.Properties.Remove("user_shift");
            t.Stop();
            t = null;
            var main = new MainWindow();
            this.Close();
            main.Closed += (s, args) => this.Close();
            main.ShowDialog();
        }
        private object EndBreak(string subUri)
        {
            var token = Application.Current.Properties["token"];
            var uri = Application.Current.Properties["uri"];

            var client = new RestClient(uri + subUri);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);
            var res = JsonConvert.DeserializeObject(response.Content);
            return res;
        }


        private object AddBreak()
        {

            var uri = Application.Current.Properties["uri"];
            var client = new RestClient(uri+"add-break");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            var token = Application.Current.Properties["token"];
            dynamic shift = Application.Current.Properties["shift"];


            request.AddHeader("Authorization", "Bearer "+token);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("user_id", shift.user_id);
            request.AddParameter("attendance_id", shift.id);
            request.AddParameter("reason", "in-activity");
            
                IRestResponse response = client.Execute(request);
            var res = JsonConvert.DeserializeObject(response.Content);
            return res;

        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime now = DateTime.Now;
            DateTime boot = now - TimeSpan.FromMilliseconds(Environment.TickCount);
            return boot + TimeSpan.FromMilliseconds(unixTimeStamp);
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            this.LogOut();
        }
    }
}
