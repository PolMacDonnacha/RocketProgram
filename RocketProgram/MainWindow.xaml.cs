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
        public List<Mission> allmissions = new List<Mission>(); //= new List<Rocket>;

        public List<Payload> selectedpayloads; //= new List<Payload>;
        public int myselectedindex;
        public MainWindow()
        {
            InitializeComponent();
            
        }

   

        public void Window_Loaded(object sender, RoutedEventArgs e)
        {

            foreach (Mission m in db.Missions)
            {
                allmissions.Add(m);
            }
            foreach (Payload payload in db.Payloads)
            {
                allpayloads.Add(payload);
            }
            


            CheckDates();
            var missionname = from m in db.Missions
                              select m.MissionName;
        myselectedindex = LbxMission.SelectedIndex +1;
        LbxMission.SelectedItem = 0;
            Mission mis = LbxMission.SelectedItem as Mission;
   

            var missionnames = missionname.ToList();
            var mission = allmissions.ElementAt(myselectedindex );
            var descriptionquery = from m in db.Missions
                                     where m.MissionID -1 == myselectedindex
                                     select m.MissionDescription;
            LbxMission.ItemsSource = missionnames;
            tbxMissionInfo.Text = string.Format($"Mission Name:\t{mission.MissionName} \nDescription:\t{mission.MissionDescription}  \nLaunch Date:\t{mission.LaunchDate}  \nLaunch Site:\t{mission.LaunchSite}  \nLaunch Vehicle:\t{mission.Rocket.RocketName}");
            var payloadlist = from p in db.Payloads
                              where p.MissionID - 1 == myselectedindex
                              select p;

            foreach (var item in payloadlist)
            {
                matchingpayload.Add(item);
            }

            lbxPayload.ItemsSource = matchingpayload.ElementAt(myselectedindex).PayloadName;


      
        }
        public void CheckDates()
        {
            foreach (Mission date in db.Missions)
            {
                int combineddate = DateTime.Today.CompareTo(date.LaunchDate);
                if(combineddate >= 0)
                {
                    CompletedMissions.Add(date);
                }
                else
                {
                    UpcomingMissions.Add(date);
                }
            }
        }
        public void LbxMission_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mission m = LbxMission.SelectedItem as Mission;
            if (m != null)
            {

                myselectedindex = LbxMission.SelectedIndex;
                var mission = allmissions.ElementAt(myselectedindex);
                tbxMissionInfo.Text = null;
                tbxMissionInfo.Text = string.Format($"Mission Name: {mission.MissionName} \n Description: {mission.MissionDescription}  \nLaunch Date: {mission.LaunchDate}  \nLaunch Site: {mission.LaunchSite}  \nLaunch Vehicle: {mission.Rocket.RocketName}");

                var payloadname = from p in db.Payloads
                               where p.MissionID  == m.MissionID
                               select p.PayloadName;

                var payloaddescription = from p in db.Payloads
                                  where p.MissionID == m.MissionID
                                  select p.Description;

                //lbxPayload.ItemsSource = payloadname.ToList();
                var payloadlist = from p in db.Payloads
                                  where p.MissionID -1 == myselectedindex 
                                  select p.PayloadName;
                lbxPayload.ItemsSource = payloadname.ToList();
                tbxPayloadInfo.Text = string.Format($"Payload Name: {payloadname} \nDescription: {payloaddescription}"); 
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
    }
}
