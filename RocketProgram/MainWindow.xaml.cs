using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RocketProgram;

namespace RocketProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MissionData db = new MissionData();
        public List<Payload> allpayloads = new List<Payload>(); 
        public List<Mission> UpcomingMissions = new List<Mission>(); 
        public List<Mission> CompletedMissions = new List<Mission>();
        public List<Mission> allmissions = new List<Mission>(); 

        public int missionselectedindex;
        public int payloadselectedindex;
        
        public MainWindow()
        {
            InitializeComponent();
            
        }

   

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {

            PopulateLists();

            LbxMission.ItemsSource = allmissions;
            LbxMission.SelectedIndex = 0;    //set the selected item to the first item in the listbox
            Mission m = LbxMission.SelectedItem as Mission;
            missionselectedindex = m.MissionID - 1;
            var mission = allmissions.ElementAt(missionselectedindex);
           
            lbxPayload.ItemsSource = mission.Payloads;// set listbox to display the list of satellites for the selected mission;

            lbxPayload.SelectedIndex = 0;

            Payload p = lbxPayload.SelectedItem as Payload;
            payloadselectedindex = lbxPayload.SelectedIndex;
            
            tbxMissionInfo.Text = string.Format($"Name\n{m.MissionName}\n\nLaunch Date\n{m.LaunchDate.ToShortDateString()}  \n\nLaunch Site\n{m.LaunchSite}");
            var payloadquery = from pay in db.Payloads
                               where pay.MissionID == m.MissionID
                               select pay;
            var payloaddescription = from pay in payloadquery
                                     where pay.PayloadID == p.PayloadID
                                     select pay.Description;
            var payload = payloadquery.ToList().ElementAt(payloadselectedindex);
            
            TimeSpan timeLeft = mission.Countdown();
            if (timeLeft.ToString().First() == '-')//if the time to launch begins with minus change to plus and show as time since launch
            {
                lblCountdown.Content = "Time Since Launch";
                timeLeft = timeLeft.Negate();
                tbxCountDown.Text = string.Format($"{timeLeft.Days} days  {timeLeft.Hours} hours  {timeLeft.Minutes} minutes  {timeLeft.Seconds} seconds");
            }
            else
            {
                lblCountdown.Content = "Countdown to Launch";
                tbxCountDown.Text = string.Format($"{timeLeft.Days} days  {timeLeft.Hours} hours  {timeLeft.Minutes} minutes  {timeLeft.Seconds} seconds");
            }
            tbxPayloadInfo.Text = string.Format($"Name\n{p.PayloadName} \n\nManufacturer\n{p.Manufacturer} \n\nDestination Orbit\n{p.DestinationOrbit}  \n\nNumber Of Satellites:\t{p.NumberOfSatellites}");
            tbxPayloadDesc.Text = payloaddescription.ToList().ElementAt(payloadselectedindex);
            tbxRocketInfo.Text = m.Rocket.ToString();
            tbxRocketDesc.Text = m.Rocket.Description;

            BitmapImage rocketimage = new BitmapImage(new Uri($"RocketImages\\{m.Rocket.Image}", UriKind.RelativeOrAbsolute));
            imgRocket.Source = rocketimage;
            BitmapImage payloadimage = new BitmapImage(new Uri($"PayloadImages\\{payload.Image}", UriKind.RelativeOrAbsolute));
            imgPayload.Source = payloadimage;
        }
        
        public void PopulateLists()
        {
            foreach (Mission m in db.Missions)
            {
                m.Payloads.Clear();
                foreach (Payload p in db.Payloads)
                {
                    if (m.MissionID == p.MissionID)
                    {
                        m.Payloads.Add(p);
                    }
                }

                
                int combineddate = DateTime.Today.CompareTo(m.LaunchDate);
                if(combineddate >= 0)
                {
                    CompletedMissions.Add(m);
                }
                else
                {
                    UpcomingMissions.Add(m);
                }
                    allmissions.Add(m);
            }
        }
        public void LbxMission_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mission m = LbxMission.SelectedItem as Mission;

            Payload p = lbxPayload.SelectedItem as Payload;


            if (m != null)
            {
                missionselectedindex = m.MissionID -1;
                var mission = allmissions.ElementAt(missionselectedindex);
                tbxMissionInfo.Text = null;
                tbxMissionInfo.Text = string.Format($"Mission Name \n{mission.MissionName}\n\n Launch Date\n{mission.LaunchDate.ToShortDateString()}  \n\nLaunch Site\n{mission.LaunchSite}");
                tbxMissionDesc.Text = mission.MissionDescription;
                   
                lbxPayload.ItemsSource = m.Payloads;
                payloadselectedindex = lbxPayload.SelectedIndex;
                if(payloadselectedindex == -1)
                {
                    payloadselectedindex = 0;
                }

                var payload = m.Payloads.ElementAt(payloadselectedindex);
                var payloadquery = from pay in db.Payloads
                                   where pay.MissionID == m.MissionID
                                   select pay;
                var payloaddescription = from pay in payloadquery
                                         where pay.PayloadID == payload.PayloadID
                                         select pay.Description;
                TimeSpan timeLeft = mission.Countdown();
                //checking if the launch has already occurred
                if(timeLeft.ToString().First() == '-')
                {
                    lblCountdown.Content = "Time Since Launch";
                    timeLeft = timeLeft.Negate();
                    tbxCountDown.Text = string.Format($"{timeLeft.Days} days  {timeLeft.Hours} hours  {timeLeft.Minutes} minutes  {timeLeft.Seconds} seconds");
                }
                else
                {
                    lblCountdown.Content = "Countdown to Launch";
                    tbxCountDown.Text = string.Format($"{timeLeft.Days} days  {timeLeft.Hours} hours  {timeLeft.Minutes} minutes  {timeLeft.Seconds} seconds");

                }

                //Filling textboxes and images for rockets and payloads
                tbxPayloadInfo.Text = null;
                tbxPayloadDesc.Text = payloaddescription.ToList().ElementAt(payloadselectedindex);
                tbxPayloadInfo.Text = string.Format($"Name\n{payload.PayloadName} \n\nManufacturer\n{payload.Manufacturer} \n\nDestination Orbit\n{payload.DestinationOrbit}  \n\nNumber Of Satellites:\t{payload.NumberOfSatellites}");
                tbxRocketInfo.Text = mission.Rocket.ToString();
                tbxRocketDesc.Text = mission.Rocket.Description;
                BitmapImage rocketimage = new BitmapImage(new Uri($"RocketImages\\{m.Rocket.Image}", UriKind.RelativeOrAbsolute));
                imgRocket.Source = rocketimage;
                BitmapImage payloadimage = new BitmapImage(new Uri($"PayloadImages\\{payload.Image}", UriKind.RelativeOrAbsolute));
                imgPayload.Source = payloadimage;
            }
        }

        private void radioUpcoming_Checked(object sender, RoutedEventArgs e)
        {
            UpcomingMissions.Clear();
            foreach (Mission date in db.Missions)
            {
                int combineddate = DateTime.Today.CompareTo(date.LaunchDate);
                if (combineddate < 0)
                {
                    UpcomingMissions.Add(date);
                }
            }

            LbxMission.ItemsSource = null;
            LbxMission.ItemsSource = UpcomingMissions;
            LbxMission.SelectedIndex = 0;
        }

        public void radioPast_Checked(object sender, RoutedEventArgs e)
        {
            CompletedMissions.Clear();
            foreach (Mission date in db.Missions)
            {
                int combineddate = DateTime.Today.CompareTo(date.LaunchDate);
                if (combineddate >= 0)
                {
                    CompletedMissions.Add(date);
                }
            }

            LbxMission.ItemsSource = null;
            LbxMission.ItemsSource = CompletedMissions;
            LbxMission.SelectedIndex = 0;
        }

        private void radioAll_Checked(object sender, RoutedEventArgs e)
        {
            allmissions.Clear();

            foreach (Mission date in db.Missions)
            {
                allmissions.Add(date);
            }
            LbxMission.ItemsSource = null;
            LbxMission.ItemsSource = allmissions;
            LbxMission.SelectedIndex = 0;
        }

        private void lbxPayload_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mission m = LbxMission.SelectedItem as Mission;
            /*Payload p = lbxPayload.SelectedItem as Payload;
            if(p != null)
            {*/  //p was returning null every time so I decided to leave it out of my query and use the "payload" variable below 
                payloadselectedindex = lbxPayload.SelectedIndex;
                if (payloadselectedindex == -1)
                {
                    payloadselectedindex = 0;
                }
                if (m != null)
                {
                    var payload = m.Payloads.ElementAt(payloadselectedindex);
                var payloadquery = from p in db.Payloads
                                  where p.MissionID == m.MissionID
                                  select p;

                var payloaddescription = from p in payloadquery
                                         where p.PayloadID == payload.PayloadID
                                         select p.Description;

                tbxPayloadDesc.Text = payloaddescription.ToList().ElementAt(0);


                tbxPayloadInfo.Text = null;
                    tbxPayloadInfo.Text = string.Format($"Name\n{payload.PayloadName} \n\nManufacturer\n{payload.Manufacturer}\n\nDestination Orbit\n{payload.DestinationOrbit}  \n\nNumber Of Satellites:\t{payload.NumberOfSatellites}");
                    BitmapImage payloadimage = new BitmapImage(new Uri($"PayloadImages\\{payload.Image}", UriKind.RelativeOrAbsolute));
                    imgPayload.Source = payloadimage;
                }
           // }
           
        }
    }
}
