using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using LeanderWclAttendanceChecker.Model;
using static LeanderWclAttendanceChecker.Resource.Constants;

namespace LeanderWclAttendanceChecker.IO
{
    public static class WriteToFile
    {
        public static void SavePlayers(List<Player> players)
        {
            XmlDocument playersXml = new XmlDocument();
            XmlElement documentElement = playersXml.CreateElement("Players");
            playersXml.AppendChild(documentElement);

            foreach (Player player in players)
            {
                XmlElement playerXml = playersXml.CreateElement(player.Name);
                foreach (Character character in player.Characters)
                {
                    XmlElement characterXml = playersXml.CreateElement(character.Name);
                    playersXml.AppendChild(characterXml);
                }
                documentElement.AppendChild(playerXml);
            }

            playersXml.Save(PlayerstoreXml);
        }
    }
}
