using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
using System.Net.Http;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace Kinopolis
{


    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private IList<Serie> _serien;
        public MainPage()
        {
            this.InitializeComponent();

            SetUrl.Click += new RoutedEventHandler(SetUrlClick);

           _serien = new List<Serie>();
            Serie serie1 = new Serie("name", "url");
            serie1.Command.ButtonClicked += ButtonClickedEvent;
            for (int i = 1; i< 4; i++)
            {
                serie1.AddEpisode(1, i);
                serie1.SetCurrtentEpisode(1, i);
            };
            _serien.Add(serie1);
            _serien.Add(serie1);
            _serien.Add(serie1);

            episodes.ItemsSource = _serien;
        }

        private void ButtonClickedEvent(object sender, EventArgs e)
        {
            var command = sender as PlayNextCommand;
            var serie = command.Serie;

            try
            {
                string nextEpisode = serie.NextEpisode;
                if (nextEpisode == "none")
                    output.Text = "keine neue Folge da";
                else
                {
                    //aufruf an kinox seite schicken mit: 
                    // - staffel + episode zum selecten
                    // li anklicken -- event auslösen
                    // gleich alle mirrors testen?
                    // script ausführen? 
                    // link schnappen -> seite öffnen

                    HttpClient client = new HttpClient();
                    var postData = new List<KeyValuePair<string, string>>();
                    postData.Add(new KeyValuePair<string, string>("param1", "value"));
                    postData.Add(new KeyValuePair<string, string>("param2", "value"));

                    HttpContent content = new FormUrlEncodedContent(postData);

                    var response =  client.PostAsync("Post URL", content).Result;



                }

            }
            catch (Exception ex)
            {
                var noooo = ex.Message;
            }
        }

        private void SetUrlClick(object sender, RoutedEventArgs e)
        {
           // String url = requiredUrl.Text;
            string url = "http://kinox.to/Stream/Game_of_Thrones-Das_Lied_von_Eis_und_Feuer.html";

            try
            {
                HtmlHandler HtmlHandler = new HtmlHandler(url);
                int _maxEpisodes = 0;
                Serie serie1 = new Serie("name1", url);
                serie1.Command.ButtonClicked += ButtonClickedEvent;
                for (int i = 1; i <= HtmlHandler.Seasons; i++)
                {
                    HtmlHandler.MaxEpisodesInSeason.TryGetValue(i, out _maxEpisodes);
                    for (int j = 1; j <= _maxEpisodes; j++)
                    {
                        serie1.AddEpisode(i, j);

                    }
                    serie1.SetCurrtentEpisode(1, 1);
                    serie1.SetNextEpisode(1, 2);
                    
                  
                };
                IList<Serie> newseries = new List<Serie>();
                foreach (Serie series in _serien)
                {
                    newseries.Add(series);
                }
                newseries.Add(serie1);

                episodes.ItemsSource = newseries;
                _serien = newseries;
            }
            catch (Exception ex)
            {
                var haha = ex.Message;
            }
            
        }
      


       
    }
}
