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
        myselectedindex = LbxMission.SelectedIndex;
        LbxMission.SelectedItem = 0;
            var missionnames = missionname.ToList();
            var mission = allmissions.ElementAt(myselectedindex + 1);
            var descriptionquery = from m in db.Missions
                                     where m.MissionID == myselectedindex
                                     select m.MissionDescription;
            LbxMission.ItemsSource = missionnames;
            tbxMissionInfo.Text = string.Format($"Mission Name:\t{mission.MissionName} \nDescription:\t{mission.MissionDescription}  \nLaunch Date:\t{mission.LaunchDate}  \nLaunch Site:\t{mission.LaunchSite}  \nLaunch Vehicle:\t{mission.Rocket.RocketName}");
           // lbxMissionInfo.ItemsSource = allmissions.ElementAt(myselectedindex);
            var payloadlist = from p in db.Payloads
                              where p.MissionID == myselectedindex + 1
                              group p by p.PayloadName into payloadname
                              select payloadname;

            lbxPayload.ItemsSource = payloadlist.ToList();


      
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
                tbxMissionInfo.Text = string.Format($"Mission Name: {mission.MissionName} \nMission Description: {mission.MissionDescription}  \nLaunch Date: {mission.LaunchDate}  \nLaunch Site: {mission.LaunchSite}  \nLaunch Vehicle: {mission.Rocket.RocketName}");

                var payloadname = from p in db.Payloads
                               where p.MissionID == m.MissionID
                               select p.PayloadName;

                var payloaddescription = from p in db.Payloads
                                  where p.MissionID == m.MissionID
                                  select p.Description;

                lbxPayload.ItemsSource = payloadname.ToList();
                var payloadlist = from p in db.Payloads
                                  where p.MissionID == myselectedindex
                                  select p.PayloadName;
                lbxPayload.ItemsSource = payloadlist.ToList();
                tbxPayloadInfo.Text = string.Format($"Payload Name: {payloadname} \nDescription: {payloaddescription}"); 
            }
        }

        private void radioUpcoming_Checked(object sender, RoutedEventArgs e)
        {
            LbxMission.ItemsSource = null;
            LbxMission.ItemsSource = UpcomingMissions;
        }

        public void radioPast_Checked(object sender, RoutedEventArgs e)
        {
            LbxMission.ItemsSource = null;
            LbxMission.ItemsSource = CompletedMissions;
        }

        private void radioAll_Checked(object sender, RoutedEventArgs e)
        {
            LbxMission.ItemsSource = null;
            LbxMission.ItemsSource = allmissions;
        }
    }
}
