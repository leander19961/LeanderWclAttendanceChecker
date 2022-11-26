using LeanderWclAttendanceChecker.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static LeanderWclAttendanceChecker.Resource.Constants;

namespace LeanderWclAttendanceChecker.IO
{
    public static class IOManager
    {
        public static void CheckDirectories()
        {
            if (!Directory.Exists(DatastorePath))
            {
                Directory.CreateDirectory(DatastorePath);
            }

            if (!Directory.Exists(PlayerstorePath))
            {
                Directory.CreateDirectory(PlayerstorePath);
            }

            if (!File.Exists(PlayerstoreXml))
            {
                XmlDocument emptyFilterList = new XmlDocument();
                XmlElement firstChild = emptyFilterList.CreateElement("Players");
                emptyFilterList.AppendChild(firstChild);
                emptyFilterList.Save(PlayerstoreXml);
            }
        }

        public static void SavePlayers(List<Player> players)
        {
            WriteToFile.SavePlayers(players);
        }

        public static void LoadPlayers(ModelService modelService)
        {
            ReadFromFile.LoadPlayers(modelService);
        }
    }
}
