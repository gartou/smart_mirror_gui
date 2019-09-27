using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.ServiceModel.Syndication;
using System.ServiceModel;


namespace OkosTukorWPF
{

    public class User
    {
        private string _name;
        private Color _color;
        private readonly string _userId;
        private string _location;
        private string _rssFeed;

        public string rssFeed
        {
            get { return _rssFeed; }
            set { _rssFeed = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public string UID => _userId;

        public User(string name, Color color)
        {
            this._name = name;
            this._color = color;
            this._userId = UserIdGenerate();
        }


        public User(string name, Color color, string UID, string location, string rssFeed)
        {
            this._name = name;
            this._color = color;
            this._userId = UID;
            this._location = location;
            _rssFeed = rssFeed;
        }


        private string UserIdGenerate()
        {
            var rnd = new Random();
            return String.Concat(this._name[rnd.Next(rnd.Next(_name.Length))],
                this.Color.R,
                this.Color.A,
                rnd.Next(19050));
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string API_KEY = "77262972affbe3dbd760984413e8c791";

        private const string CurrentUrl =
            "http://api.openweathermap.org/data/2.5/weather?" +
            "q=@LOC@&mode=xml&units=metric&APPID=" + API_KEY;

        private const string ForecastUrl =
            "http://api.openweathermap.org/data/2.5/forecast?" +
            "q=@LOC@&mode=xml&units=metric&APPID=" + API_KEY;

        static InfoPanel infoPanel;
        List<User> users = new List<User>();
        private string WeatherData = "Budapest";

        public MainWindow()
        {
            InitializeComponent();
        }


        #region PythonRecog

        public String outDetect;

        public String DetectionResult
        {
            get { return outDetect; }
            set
            {
                outDetect = value;
                UserCheck();
            }
        }

        #endregion

        #region PatrikCuccosai

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string cmd =
                @"c:\face\recognize_video.py --detector c:\face\face_detection_model\ --embedding-model c:\face\openface_nn4.small2.v1.t7 --recognizer c:\face\output\recognizer.pickle --le c:\face\output\le.pickle";

            Process recognize = new Process {StartInfo = {FileName = "python.exe", Arguments = cmd}};
            recognize.StartInfo.UseShellExecute = false;
            recognize.StartInfo.RedirectStandardOutput = true;
            recognize.StartInfo.CreateNoWindow = true;

            recognize.Start();

            StreamReader reader = recognize.StandardOutput;
            Console.WriteLine(reader);
            while (!reader.EndOfStream)
            {
                this.DetectionResult = reader.ReadLine();
            }
        }

        private void Btn_TrainModel_Click(object sender, RoutedEventArgs e)
        {
            string cmd =
                @"c:\face\train_model.py --embeddings c:\face\output\embeddings.pickle --recognizer c:\face\output\recognizer.pickle --le c:\face\output\le.pickle";

            Process modeltrain = new Process {StartInfo = {FileName = "python.exe", Arguments = cmd}};
            modeltrain.StartInfo.UseShellExecute = true;

            modeltrain.Start();
        }

        private void Btn_Extract_Click(object sender, RoutedEventArgs e)
        {
            string cmd =
                @"c:\face\extract_embeddings.py --dataset c:\face\dataset --embeddings c:\face\output\embeddings.pickle --detector c:\face\face_detection_model\ --embedding-model c:\face\openface_nn4.small2.v1.t7";

            Process extract = new Process {StartInfo = {FileName = "python.exe", Arguments = cmd}};
            extract.StartInfo.UseShellExecute = true;

            extract.Start();
        }

        #endregion

        private void UserCheck()
        {
            if (users.Exists(x => x.UID == outDetect))
            {
                User temp = users.First(x => x.UID == outDetect);
                infoPanel.txtblock_name.Text = temp.Name;
                DetectedUser.Content = temp.Name;
                //WeatherData = temp.Location;
                string url = CurrentUrl.Replace("@LOC@", temp.Location);
                infoPanel.txtblock_weather.Text = GetFormattedXml_(url);
                RSS_Feed(temp.rssFeed);
                infoPanel.rec_name.Fill = new SolidColorBrush(temp.Color);
            }
            else
            {
                infoPanel.txtblock_name.Text = outDetect;
            }
        }

        private void Btn_RegisterU_Click(object sender, RoutedEventArgs e)
        {
            User temp = new User(txtbox_name.Text, cp_color.SelectedColor.Value);
            users.Add(temp);
            Directory.CreateDirectory("C:/face/dataset/" + temp.UID);
            Process.Start("C:/face/dataset/" + temp.UID);
        }

        private void Btn_OpenMirror_Click(object sender, RoutedEventArgs e)
        {
            infoPanel = new InfoPanel();
            infoPanel.Show();

            //RSS_Feed(users.Find(x => x.Name == "Szabi").rssFeed);
            //string url = CurrentUrl.Replace("@LOC@", users.Find(x => x.Name == "Szabi").Location);
            //infoPanel.txtblock_weather.Text = GetFormattedXml_(url);

        }

        #region savingloading
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Save();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Read();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Btn_Read_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        #endregion
        private void Save()
        {
            StreamWriter sw = new StreamWriter("users.txt");
            foreach (var item in users)
            {
                sw.WriteLine(item.Name + ";" + item.Color.A + ";" + item.Color.R + ";" + item.Color.G + ";" +
                             item.Color.B + ";" + item.UID + ";" + item.Location + ";" + item.rssFeed);
            }

            sw.Close();
        }

        private void Read()
        {
            //#FF8C2323
            list_u.Items.Clear();
            try
            {
                StreamReader sr = new StreamReader("users.txt");
                while (!sr.EndOfStream)
                {
                    string[] temp = sr.ReadLine().Split(';');
                    User tempU = new User(temp[0],
                        Color.FromArgb(byte.Parse(temp[1]), byte.Parse(temp[2]), byte.Parse(temp[3]),
                            byte.Parse(temp[4])), temp[5], temp[6], temp[7]);
                    users.Add(tempU);
                }

                foreach (var item in users)
                {
                    list_u.Items.Add(item.Name);
                }

                sr.Close();
            }
            catch (Exception)
            {
            }
        }

        private void List_u_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = list_u.SelectedItem.ToString();
            textbox_loc.Text = users.Find(x => x.Name == text).Location;
            textbox_rssfeed.Text = users.Find(x => x.Name == text).rssFeed;
        }

        private string GetFormattedXml_(string url)
        {
            // Create a web client.
            using (WebClient client = new WebClient())
            {
                // Get the response string from the URL.
                string xml = client.DownloadString(url);

                // Load the response into an XML document.
                XmlDocument xml_document = new XmlDocument();
                xml_document.LoadXml(xml);

                string weather = "";
                // Format the XML.
                foreach (XmlNode node in xml_document.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "temperature":
                            weather += "Temp:" + node.Attributes["value"].InnerText + " " +
                                       node.Attributes["unit"].InnerText;
                            break;
                        case "wind":
                            weather += "\nWind:" + node.FirstChild.Attributes["name"].InnerText;
                            break;
                    }
                }

                return weather;
            }
        }

        private void Button_change_data(object sender, RoutedEventArgs e)
        {
            string text = list_u.SelectedItem.ToString();
            users.Find(x => x.Name == text).Location = textbox_loc.Text;
            users.Find(x => x.Name == text).rssFeed = textbox_rssfeed.Text;
        }

        private static void RSS_Feed(string rssUri)
        {
            if (rssUri != "")
            {

                int i = 0;
                const int MaxLength = 32;
                string rss_text = "";
                try
                {
                    XmlReader _xmlReader;
                    SyndicationFeed _syndicationFeed;

                    _xmlReader = XmlReader.Create(rssUri);
                    _syndicationFeed = SyndicationFeed.Load(_xmlReader);

                    Console.WriteLine(_syndicationFeed.Title.Text);
                    foreach (SyndicationItem item in _syndicationFeed.Items)
                    {
                        if (i >= 3 && i < 6)
                        {
                            if (item.Title.Text.Length > MaxLength)
                            {
                                rss_text = item.Title.Text.Substring(0, MaxLength);
                            }

                            infoPanel.txtblock_rss.Text += rss_text;
                        }
                        else if (i > 6)
                        {
                            break;
                        }

                        i++;
                        //Console.WriteLine("Title: {0}", item.Title.Text);
                    }
                }
                catch (CommunicationException ce)
                {
                    Console.WriteLine("An exception occurred: {0}", ce.Message);
                }
            }
        }
    }
}
