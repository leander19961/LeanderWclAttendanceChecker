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
    public static class ReadFromFile
    {
        public static void LoadPlayers(ModelService modelService)
        {
            XmlDocument playerXml = new XmlDocument();
            playerXml.Load(PlayerstoreXml);
            XmlElement documentElement = playerXml.DocumentElement;

            foreach (XmlNode playerNode in documentElement.ChildNodes)
            {
                Player newPlayer = modelService.CreateNewPlayer(playerNode.Name);
                foreach(XmlNode characterNode in playerNode.ChildNodes)
                {
                    newPlayer.WithCharacters(modelService.CreateNewCharacter(characterNode.Name));
                }
            }
        }
    }
}
