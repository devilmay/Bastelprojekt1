using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Kinopolis
{
    class HtmlHandler
    {
        public string SourceCode { get; set; }
        public int Seasons { get; set; }
        public Dictionary<int, int> MaxEpisodesInSeason { get; set; }
        public string[] HtmlLines { get; set; }

        public HtmlHandler( string url){

            this.MaxEpisodesInSeason = new Dictionary<int, int>() ;
            HttpClient HttpClient = new HttpClient();
            HttpResponseMessage ResponseMessage = HttpClient.GetAsync(url).Result;
            Stream receiveStream = ResponseMessage.Content.ReadAsStreamAsync().Result;
          
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
           
            string Answer = readStream.ReadToEnd();
          
            ReadHtml(Answer);
            SourceCode = Answer;
        }

        public void ReadHtml(string html)
        {
            HtmlLines = html.Split('\n');
            string[] temp;
            List<string> tempi;
            int season;
            int episode;
          
            for ( int i = 0; i <= HtmlLines.Length; i++)
            {
                if(HtmlLines[i].StartsWith("<option value=\"1"))
                {
                    while (!HtmlLines[i].StartsWith("</select>")) { 
                    temp = HtmlLines[i].Split('"');
                    season = Int32.Parse(temp[1]);
                    this.Seasons = season;
                    tempi = temp[3].Split(',').ToList();
                    episode = Int32.Parse(tempi.Last<string>()); 
                    if(episode== 0)
                    {
                        break;
                    }                  
                    this.MaxEpisodesInSeason.Add(season, episode);
                    i++;
                    }
                    break;
                }

            }
            
        }
      
    }
}
