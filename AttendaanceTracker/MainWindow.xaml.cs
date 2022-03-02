using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using RestSharp;
using Newtonsoft.Json;

namespace AttendaanceTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            show_tracker();
            if (!Application.Current.Properties.Contains("uri"))
                Application.Current.Properties.Add("uri", "http://localhost/support-portal/api/");
        }


        private void show_tracker()
        {
            if (Application.Current.Properties.Contains("user"))
            {
                var tracker = new Tracker();
                this.Hide();
                tracker.Closed += (s, args) => this.Close();
                tracker.ShowDialog();
            }
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            
            var un = username.Text;
            var ps = password.Password;
            var uri = Application.Current.Properties["uri"];

            var client = new RestClient(uri+"login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("email", un);
            request.AddParameter("password", ps);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                try
                {
                    string data = response.Content;
                    dynamic d = JsonConvert.DeserializeObject(data);

                    MessageBox.Show(Convert.ToString(d.data), Convert.ToString(d.message), MessageBoxButton.OK, MessageBoxImage.Error);
                }catch(Exception ex)
                {
                    MessageBox.Show("API not working. Server down. Contact system admin.", "Error: Server Down", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    string data = response.Content;
                    dynamic d = JsonConvert.DeserializeObject(data);
                    Application.Current.Properties.Add("user", d.data.user);
                    Application.Current.Properties.Add("token", d.data.token);

                    dynamic shift = this.GetApi("get-shift");
                    if (shift.data.todays_attendance == null)
                    {
                        label_error.Content = "First check-in to system from Portal then login here.";

                        Application.Current.Properties.Remove("user");
                        Application.Current.Properties.Remove("token");
                    }
                    else if (shift.data.todays_attendance?.end_time != null)
                    {
                        label_error.Content = "Already Checked out for the day. Contact System admin.";

                        Application.Current.Properties.Remove("user");
                        Application.Current.Properties.Remove("token");
                    }
                    else
                    {
                        if (Application.Current.Properties.Contains("shift"))
                            Application.Current.Properties.Remove("shift");
                        Application.Current.Properties.Add("shift", shift.data.todays_attendance);
                        Application.Current.Properties.Add("user_shift", shift.data.shift_details);
                        dynamic off_time = this.GetApi("in-active-break");
                        Console.WriteLine(off_time);
                        if (off_time.data != null)
                        {
                            Application.Current.Properties.Add("time_off", off_time.data);
                        }

                        show_tracker();
                    }
                }catch(Exception ex)
                {

                    label_error.Content = "Contact System admin. Operation not allowed.";
                }
            }
           
        }

        private object GetApi(string subUri)
        {
            var token = Application.Current.Properties["token"];
            var uri = Application.Current.Properties["uri"];

            var client = new RestClient(uri + subUri);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer "+token);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject(response.Content);
        }
        private bool check(string t)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(t);
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {            
            if (!this.check(username.Text))
            {
                label_error.Content = " Enter a valid email.";
                username.Focus();
                login.IsEnabled = false;
            }
            else if(this.check(username.Text) && (password.Password.Length < 5)) 
            {
                label_error.Content = " Password Cannot be empty.";
                username.Focus();
                login.IsEnabled = false;
            }
            else
            {
                login.IsEnabled = true;
                label_error.Content = "";
            }

        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password.Password.Length < 5)
            {
                label_error.Content = " Password must be more then 5 characters.";
                password.Focus();
                login.IsEnabled = false;
            }
            else if (!this.check(username.Text) && (password.Password.Length > 4))
            {
                label_error.Content = " Enter a valid email.";
                password.Focus();
                login.IsEnabled = false;
            }
            else
            {
                login.IsEnabled = true;
                label_error.Content = "";
            }

        }
    }
}
