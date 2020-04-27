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
        public List<Rocket> selectedrocket; //= new List<Rocket>;
        public List<Mission> filteredmission; 
        public List<Payload> allpayloads = new List<Payload>(); 
        public List<Payload> matchingpayload = new List<Payload>(); 
        public List<Mission> UpcomingMissions = new List<Mission>(); 
        public List<Mission> CompletedMissions = new List<Mission>();
        public List<Mission> allmissions = new List<Mission>(); 

        public List<Payload> selectedpayloads; //= new List<Payload>;
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
            foreach (Payload pl in mission.Payloads)
            {
                matchingpayload.Add(pl); //add every payload item thats in the 
            }
            lbxPayload.ItemsSource = mission.Payloads;// matchingpayload;

            lbxPayload.SelectedIndex = 0;

            Payload p = lbxPayload.SelectedItem as Payload;
            payloadselectedindex = lbxPayload.SelectedIndex;

            
            
            tbxMissionInfo.Text = string.Format($"Mission Name:\t{m.MissionName} \n\nDescription:{m.MissionDescription} \n\nLaunch Date:\t{m.LaunchDate.ToShortDateString()}  \n\nLaunch Site:\t{m.LaunchSite}");
            



            var payload = matchingpayload.ElementAt(payloadselectedindex);
            tbxPayloadInfo.Text = string.Format($"Name:\t{p.PayloadName} \n\nDescription:\t{p.Description} \n\nManufacturer:\t{p.Manufacturer} \nDestination Orbit:\t{p.DestinationOrbit}  \nNumber Of Satellites:\t{p.NumberOfSatellites}");
            TimeSpan timeLeft = mission.Countdown();
            if (timeLeft.ToString().First() == '-')//if the time to launch begins with minus change to plus and show as time since launch
            {
                timeLeft = timeLeft.Negate();
                tbxCountDown.Text = string.Format($"\tTime since launch\nDays\tHours\tMinutes\tSeconds\n{timeLeft.Days}\t{timeLeft.Hours}\t{timeLeft.Minutes}\t{timeLeft.Seconds}");
            }
            else
            {
                tbxCountDown.Text = string.Format($"\tCountdown To Launch\nDays\tHours\tMinutes\tSeconds\n{timeLeft.Days}\t{timeLeft.Hours}\t{timeLeft.Minutes}\t{timeLeft.Seconds}");

            }
            tbxRocketInfo.Text = m.Rocket.ToString();
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
                tbxMissionInfo.Text = string.Format($"Mission Name: \t{mission.MissionName}\n\nDescription: {mission.MissionDescription}  \n\nLaunch Date: {mission.LaunchDate.ToShortDateString()}  \n\nLaunch Site: {mission.LaunchSite}");

                var payloadname = from payld in db.Payloads
                               where payld.MissionID  == m.MissionID
                               select payld.PayloadName;
                matchingpayload.Clear();
                foreach (Payload pld in mission.Payloads)
                {
                    matchingpayload.Add(pld);
                }
                lbxPayload.ItemsSource = m.Payloads;
                payloadselectedindex = lbxPayload.SelectedIndex;
                if(payloadselectedindex == -1)
                {
                    payloadselectedindex = 0;
                }

                var payload = m.Payloads.ElementAt(payloadselectedindex);
                TimeSpan timeLeft = mission.Countdown();
                if(timeLeft.ToString().First() == '-')
                {
                    timeLeft = timeLeft.Negate();
                    tbxCountDown.Text = string.Format($"\tTime since launch\nDays\tHours\tMinutes\tSeconds\n{timeLeft.Days}\t{timeLeft.Hours}\t{timeLeft.Minutes}\t{timeLeft.Seconds} ");
                }
                else
                {
                    tbxCountDown.Text = string.Format($"\tCountdown To Launch\nDays\tHours\tMinutes\tSeconds\n{timeLeft.Days}\t{timeLeft.Hours}\t{timeLeft.Minutes}\t{timeLeft.Seconds}");

                }


                tbxPayloadInfo.Text = null;
                tbxPayloadInfo.Text = string.Format($"Name:\t{payload.PayloadName} \nDescription:{payload.Description} \n\nManufacturer:\t{payload.Manufacturer} \nDestination Orbit:\t{payload.DestinationOrbit:-15}  \nNumber Of Satellites:\t{payload.NumberOfSatellites}");
                tbxRocketInfo.Text = mission.Rocket.ToString();
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
        }

        private void lbxPayload_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mission m = LbxMission.SelectedItem as Mission;
            Payload p = lbxPayload.SelectedItem as Payload;

            payloadselectedindex = lbxPayload.SelectedIndex;
            if (payloadselectedindex == -1)
            {
                payloadselectedindex = 0;
            }

            var payload = m.Payloads.ElementAt(payloadselectedindex);

            tbxPayloadInfo.Text = null;
            tbxPayloadInfo.Text = string.Format($"Name:\t{payload.PayloadName} \nDescription:{payload.Description} \n\nManufacturer:\t{payload.Manufacturer} \nDestination Orbit:\t{payload.DestinationOrbit:-15}  \nNumber Of Satellites:\t{payload.NumberOfSatellites}");
            BitmapImage payloadimage = new BitmapImage(new Uri($"PayloadImages\\{p.Image}", UriKind.RelativeOrAbsolute));
            imgPayload.Source = payloadimage;
        }
    }
}
