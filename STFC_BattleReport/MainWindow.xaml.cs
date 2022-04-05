using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace STFC_BattleReport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel m = new MainViewModel();

        public class MainViewModel : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private ObservableCollection<AuswertungPlayer> auswertungplayerliste = new ObservableCollection<AuswertungPlayer>();

            public ObservableCollection<AuswertungPlayer> Auswertungplayerliste
            {
                get { return auswertungplayerliste; }
                set
                {
                    auswertungplayerliste = value;
                    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Firmenliste"));
                }
            }
            
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = m;
            damagegrid.ItemsSource = m.Auswertungplayerliste;
            playergrid.ItemsSource = m.Auswertungplayerliste;
        }

        private BattleReport breport;
        private void ParseCSV(string[] array)
        {
            //keywords: 
            //1. Player Name
            //2. Reward Name
            //3. Fleet Type
            //4. Round

            List<Enemy> enemies = new List<Enemy>();

            //EnemyListe
            int index = -1;
            for (int k = 0; k < array.Length; k++)
            {
                string s = array[k];
                index++;

                if (s.StartsWith("Player Name"))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(s))
                {
                    index++;
                    break;
                }

                string[] inhalt = s.Split(',');

                enemies.Add(new Enemy(inhalt));
            }

            //RewardListe
            List<Reward> rewards = new List<Reward>();

            for (int k = index; k < array.Length; k++)
            {
                string s = array[k];
                index++;

                if (s.StartsWith("Reward Name"))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(s))
                {
                    index++;
                    break;
                }

                string[] inhalt = s.Split(',');

                rewards.Add(new Reward(inhalt));
            }

            //PlayListe
            List<Player> players = new List<Player>();

            for (int k = index; k < array.Length; k++)
            {
                string s = array[k];
                index++;

                if (s.StartsWith("Fleet Type"))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(s))
                {
                    index++;
                    break;
                }

                string[] inhalt = s.Split(',');

                players.Add(new Player(inhalt));
            }

            //ReportListe
            List<Report> reports = new List<Report>();

            for (int k = index; k < array.Length; k++)
            {
                string s = array[k];
                index++;

                if (s.StartsWith("Round"))
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(s))
                {
                    index++;
                    break;
                }

                string[] inhalt = s.Split(',');

                reports.Add(new Report(inhalt));
            }

            breport = new BattleReport();
            breport.enemies = enemies.ToArray();
            breport.players = players.ToArray();
            breport.rewards = rewards.ToArray();
            breport.reports = reports.ToArray();
        }

        private class BattleReport
        {
            public Enemy[] enemies { get; set; }
            public Reward[] rewards { get; set; }
            public Player[] players { get; set; }
            public Report[] reports { get; set; }
        }

        private class Enemy
        {
            public Enemy(string[] s)
            {
                PlayerName = s[0];
                PlayerLevel = s[1];
                Outcome = s[2];
                ShipName = s[3];
                ShipLevel = s[4];
                ShipStrength = s[5];
                ShipXP = s[6];
                OfficerOne = s[7];
                OfficerTwo = s[8];
                OfficerThree = s[9];
                HullHealth = s[10];
                HullHealthRemaining = s[11];
                ShieldHealth = s[12];
                ShieldHealthRemaining = s[13];
                Location = s[14];
                Timestamp = s[15];
            }

            public string PlayerName { get; set; }
            public string PlayerLevel { get; set; }
            public string Outcome { get; set; }
            public string ShipName { get; set; }
            public string ShipLevel { get; set; }
            public string ShipStrength { get; set; }
            public string ShipXP { get; set; }
            public string OfficerOne { get; set; }
            public string OfficerTwo { get; set; }
            public string OfficerThree { get; set; }
            public string HullHealth { get; set; }
            public string HullHealthRemaining { get; set; }
            public string ShieldHealth { get; set; }
            public string ShieldHealthRemaining { get; set; }
            public string Location { get; set; }
            public string Timestamp { get; set; }
        }

        private class Reward
        {
            public Reward(string[] s)
            {
                RewardName = s[0];
                Count = s[1];
            }

            string RewardName { get; set; }
            string Count { get; set; }
        }

        private class Player
        {
            public Player(string[] s)
            {
                FleetType = s[0];
                Attack = s[1];
                Defense = s[2];
                Health = s[3];
                ShipAbility = s[4];
                CaptainManeuver = s[5];
                OfficerOneAbility = s[6];
                OfficerTwoAbility = s[7];
                OfficerThreeAbility = s[8];
                OfficerAttackBonus = s[9];
                DamagePerRound = s[10];
                ArmourPierce = s[11];
                ShieldPierce = s[12];
                Accuracy = s[13];
                CriticalChance = s[14];
                CriticalDamage = s[15];
                OfficerDefenseBonus = s[16];
                Armour = s[17];
                ShieldDeflection = s[18];
                Dodge = s[19];
                OfficerHealthBonus = s[20];
                ShieldHealth = s[21];
                HullHealth = s[22];
                ImpulseSpeed = s[23];
                WarpRange = s[24];
                WarpSpeed = s[25];
                CargoCapacity = s[26];
                ProtectedCargo = s[27];
                MiningBonus = s[28];

            }
            string FleetType { get; set; }
            string Attack { get; set; }
            string Defense { get; set; }
            string Health { get; set; }
            string ShipAbility { get; set; }
            string CaptainManeuver { get; set; }
            string OfficerOneAbility { get; set; }
            string OfficerTwoAbility { get; set; }
            string OfficerThreeAbility { get; set; }
            string OfficerAttackBonus { get; set; }
            string DamagePerRound { get; set; }
            string ArmourPierce { get; set; }
            string ShieldPierce { get; set; }
            string Accuracy { get; set; }
            string CriticalChance { get; set; }
            string CriticalDamage { get; set; }
            string OfficerDefenseBonus { get; set; }
            string Armour { get; set; }
            string ShieldDeflection { get; set; }
            string Dodge { get; set; }
            string OfficerHealthBonus { get; set; }
            string ShieldHealth { get; set; }
            string HullHealth { get; set; }
            string ImpulseSpeed { get; set; }
            string WarpRange { get; set; }
            string WarpSpeed { get; set; }
            string CargoCapacity { get; set; }
            string ProtectedCargo { get; set; }
            string MiningBonus { get; set; }
        }

        public class Report
        {
            public Report(string[] s)
            {
                Round = s[0];
                BattleEvent = s[1];
                TypeEvent = s[2];
                AttackerName = s[3];
                AttackerAlliance = s[4];
                AttackerShip = s[5];
                AttackerIsArmada = s[6];
                TargetName = s[7];
                TargetAlliance = s[8];
                TargetShip = s[9];
                TargetIsArmada = s[10];
                CriticalHit = s[11];
                HullDamage = s[12];
                ShieldDamage = s[13];
                MitigatedDamage = s[14];
                TotalDamage = s[15];
                AbilityType = s[16];
                AbilityValue = s[17];
                AbilityName = s[18];
                AbilityOwnerName = s[19];
                TargetDefeated = s[20];
                TargetDestroyed = s[21];
                ChargingWeaponsPercent = s[22];
            }

            public string Round { get; set; }
            public string BattleEvent { get; set; }
            public string TypeEvent { get; set; }
            public string AttackerName { get; set; }
            public string AttackerAlliance { get; set; }
            public string AttackerShip { get; set; }
            public string AttackerIsArmada { get; set; }
            public string TargetName { get; set; }
            public string TargetAlliance { get; set; }
            public string TargetShip { get; set; }
            public string TargetIsArmada { get; set; }
            public string CriticalHit { get; set; }
            public string HullDamage { get; set; }
            public string ShieldDamage { get; set; }
            public string MitigatedDamage { get; set; }
            public string TotalDamage { get; set; }
            public string AbilityType { get; set; }
            public string AbilityValue { get; set; }
            public string AbilityName { get; set; }
            public string AbilityOwnerName { get; set; }
            public string TargetDefeated { get; set; }
            public string TargetDestroyed { get; set; }
            public string ChargingWeaponsPercent { get; set; }
        }

        public class AuswertungPlayer
        {
            public AuswertungPlayer(string name)
            {
                PlayerName = name;
            }
            public string PlayerName { get; set; }
            public double HullDamage { get; set; }
            public double ShieldDamage { get; set; }
            public double MitigatedDamage { get; set; }
            public double TotalDamage { get; set; }
            public double HitsTaken { get; set; }
            public double DamageTaken { get; set; }
            public double HitsDone { get; set; }
            public double CriticalHitsDone { get; set; }
            public double CriticalHitsPercentage { get; set; }


            public double Mitigation { get; set; }
            public double Piercing { get; set; }


            public List<double> MitigationList { get; set; }
            public List<double> PiercingList { get; set; }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = Utility.getpfadtxt();
            m.Auswertungplayerliste.Clear();

            if (File.Exists(path))
            {
                try
                {
                    string data = File.ReadAllText(path);

                    string[] dataarray = data.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                    ParseCSV(dataarray);

                    Dictionary<string, AuswertungPlayer> playerdict = new Dictionary<string, AuswertungPlayer>();

                    foreach (Report rep in breport.reports)
                    {
                        if (rep.TypeEvent == "Attack")
                        {
                            if (!playerdict.ContainsKey(rep.AttackerName))
                            {
                                playerdict.Add(rep.AttackerName, new AuswertungPlayer(rep.AttackerName));
                            }

                            if (breport.enemies.Where(x => x.PlayerName == rep.AttackerName).Count() > 0)
                            {
                                if (!playerdict.ContainsKey(rep.TargetName))
                                {
                                    playerdict.Add(rep.TargetName, new AuswertungPlayer(rep.TargetName));
                                }

                                //Gegner attackiert
                                playerdict[rep.TargetName].HitsTaken += 1;
                                playerdict[rep.TargetName].DamageTaken += Convert.ToDouble(rep.TotalDamage);

                                double mitigation = Convert.ToDouble(rep.MitigatedDamage) / Convert.ToDouble(rep.TotalDamage);

                                playerdict[rep.TargetName].Mitigation = mitigation;
                            }
                            else
                            {
                                //Spieler attackiert
                                playerdict[rep.AttackerName].HullDamage += Convert.ToDouble(rep.HullDamage);
                                playerdict[rep.AttackerName].ShieldDamage += Convert.ToDouble(rep.ShieldDamage);
                                playerdict[rep.AttackerName].TotalDamage += Convert.ToDouble(rep.TotalDamage);
                                playerdict[rep.AttackerName].MitigatedDamage += Convert.ToDouble(rep.MitigatedDamage);
                                playerdict[rep.AttackerName].HitsDone += 1;

                                if (rep.CriticalHit != "NO")
                                {
                                    playerdict[rep.AttackerName].CriticalHitsDone += 1;
                                }

                                playerdict[rep.AttackerName].CriticalHitsPercentage = playerdict[rep.AttackerName].CriticalHitsDone / playerdict[rep.AttackerName].HitsDone;

                                double piercing = 1 - Convert.ToDouble(rep.MitigatedDamage) / Convert.ToDouble(rep.TotalDamage);

                                playerdict[rep.AttackerName].Piercing = piercing;
                            }


                        }
                    }

                    foreach (KeyValuePair<string, AuswertungPlayer> ap in playerdict)
                    {
                        if (breport.enemies.Where(x => x.PlayerName == ap.Key).Count() == 0)
                        {
                            m.Auswertungplayerliste.Add(ap.Value);
                        }
                    }
                }
                catch
                {
                    m.Auswertungplayerliste.Clear();
                    MessageBox.Show("Could not analyze report!");
                }
            }
            else
            {

            }
        }

        public static class Utility
        {
            //Öffnet den FileDialog und legt den Datenbankpfad fest
            public static string getpfad()
            {
                Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();
                file.InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                file.Filter = "Access Datenbankdateien (*.accdb)|*.accdb|All files (*.*)|*.*";

                if (file.ShowDialog() == true)
                {
                    return file.FileName;
                }
                return "";
            }

            //Öffnet den FileDialog
            public static string getpfadbeliebig()
            {
                Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();
                file.InitialDirectory = System.IO.Path.GetDirectoryName(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                //file.Filter = "Access Datenbankdateien (*.accdb)|*.accdb|All files (*.*)|*.*";

                if (file.ShowDialog() == true)
                {
                    return file.FileName;
                }
                return "";
            }

            //Öffnet den FileDialog
            public static string getpfadtxt()
            {
                Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();
                file.InitialDirectory = System.IO.Path.GetDirectoryName(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                file.Filter = "Text Files "
                    + "(*.txt;*.csv)|*.txt;*.csv";

                if (file.ShowDialog() == true)
                {
                    return file.FileName;
                }
                return "";
            }          

            

        }

    }
}
