using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinopolis
{
    class Serie
    {
        private KeyValuePair<int, int> _currentEpisode = new KeyValuePair<int, int>(1, 1);
        public string Name { get; set; }      
        public string Url { get; set; }
      

        public string CurrentEpisode
        {
            get { return ("S" + _currentEpisode.Key.ToString() + "E" + _currentEpisode.Value.ToString()); }
            set { string temp = value;
                temp = temp.Remove(0,1);
                  string[] tempi = temp.Split('E');
                    _currentEpisode = new KeyValuePair<int,int>(Int32.Parse(tempi[0]), Int32.Parse(tempi[1]));
                    }
        }

        public IList<string> AllEpisodes
        {
            
            get {
                List<string> returnList = new List<string>();
                foreach(KeyValuePair<int,int> episode in Episodes.Keys)
                {
                    returnList.Add("S" + episode.Key.ToString() + "E" + episode.Value.ToString());
                }
                return returnList;
            }
        }

        public void SetCurrtentEpisode(int season, int episode)
        {
            this._currentEpisode = new KeyValuePair<int, int>(season, episode);
        }
        public IDictionary<KeyValuePair<int, int>, String> Episodes;


        public Serie(string name1, string url1)
        {
            Name = name1;
            Url = url1;
            Episodes = new Dictionary<KeyValuePair<int, int>, String>();

        }

        public void AddEpisode(int season, int episode)
        {

            if (!Episodes.ContainsKey(new KeyValuePair<int, int>(season, episode)))
            {
                Episodes.Add(new KeyValuePair<int, int>(season, episode), "dummy");
            }


        }
    }

}
