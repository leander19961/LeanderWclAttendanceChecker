using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using LeanderWclAttendanceChecker.Model;
using static LeanderWclAttendanceChecker.Resource.Constants;

namespace LeanderWclAttendanceChecker.IO
{
    public static  class TxtExport
    {
        public static void WriteOutPutFile(List<Player> players)
        {
            StreamWriter streamWriter = File.CreateText(OutputTxt);

            foreach (Player player in players)
            {
                string player_line = "";
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        player_line += '\'' + player.Name + '\'' + ";";
                    }
                    else if (i == 1)
                    {
                        player_line += '\'' + player.AttendanceCount.ToString().Replace(",", ".") + '\'' + ";";
                    }
                    else
                    {
                        player_line += '\'' + player.AttendancePercent.ToString("0.00").Replace(",", ".") + '\'';
                    }
                }
                streamWriter.WriteLine(player_line);
            }
            streamWriter.Close();
        }
    }
}
