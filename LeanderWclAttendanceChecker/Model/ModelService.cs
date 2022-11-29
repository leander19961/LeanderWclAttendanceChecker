using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using LeanderWclAttendanceChecker.API.Web;
using LeanderWclAttendanceChecker.IO;
using LeanderWclAttendanceChecker.View.LeanderWclAttendanceCheckerViewer;

namespace LeanderWclAttendanceChecker.Model
{
    public class ModelService
    {
        private LeanderWclAttendanceCheckerView _mainWindow;

        private WebAPI _webAPI;

        private bool _useStartTime;
        private bool _useEndTime;
        private int _maxAttendance;

        private List<Player> _players;
        private List<Character> _characters;

        public List<Player> Players
        {
            get { return _players; }
        }

        public List<Character> Characters
        {
            get { return _characters; }
        }

        private string _baseUrl;

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        public bool UseStartTime
        {
            get { return _useStartTime; }
            set { _useStartTime = value; }
        }

        public bool UseEndTime
        {
            get { return _useEndTime; }
            set { _useEndTime = value; }
        }

        public ModelService(LeanderWclAttendanceCheckerView mainWindow, string baseUrl)
        {
            _mainWindow = mainWindow;
            _baseUrl = baseUrl;
            _maxAttendance = 0;

            _webAPI = new WebAPI();

            _players = new List<Player>();
            _characters = new List<Character>();
        }

        public Character CreateNewCharacter(string name)
        {
            Character newCharacter = new Character(name);
            _characters.Add(newCharacter);
            return newCharacter;
        }

        public Player CreateNewPlayer(string name)
        {
            Player newPlayer = new Player(name);
            _players.Add(newPlayer);

            IOManager.SavePlayers(_players);

            return newPlayer;
        }

        public void DeletePlayer(Player? player)
        {
            player.RemoveYou();
            _players.Remove(player);

            IOManager.SavePlayers(_players);
        }

        public ObservableCollection<Player> GetPlayersObservable()
        {
            ObservableCollection<Player> result = new ObservableCollection<Player>();

            foreach (Player player in _players)
            {
                result.Add(player);
            }

            return result;
        }

        public ObservableCollection<Character> GetCharactersObservable()
        {
            ObservableCollection<Character> result = new ObservableCollection<Character>();

            foreach (Character character in _characters)
            {
                result.Add(character);
            }

            return result;
        }

        public void GetCharacterAttendance(DateTime? startDate, DateTime? endDate)
        {
            ResetCharacterAttendance();

            List<string> reports;
            if (UseStartTime && UseEndTime)
            {
                string startTime = UnixTime(startDate);
                string endTime = UnixTime(endDate);

                reports = JsonParser.GetReportsFromJson(_webAPI.GETLogList(startTime, endTime));
            }
            else if (UseStartTime)
            {
                string startTime = UnixTime(startDate);

                reports = JsonParser.GetReportsFromJson(_webAPI.GETLogList(startTime, true));
            }
            else if (UseEndTime)
            {
                string endTime = UnixTime(endDate);

                reports = JsonParser.GetReportsFromJson(_webAPI.GETLogList(endTime, false));
            }
            else
            {
                reports = JsonParser.GetReportsFromJson(_webAPI.GETLogList());
            }

            foreach (string report in reports)
            {
                List<string> characters = JsonParser.GetCharactersFromJson(_webAPI.GETReport(report));
                foreach (string character in characters)
                {
                    Character newCharacter = CheckCharacterExists(character);
                    newCharacter.AttendanceCount++;
                }
                _maxAttendance++;
            }

            _characters.OrderBy(entry => entry.AttendanceCount).ToList<Character>();
        }

        public void GetPlayerAttendance()
        {
            foreach (Player player in _players)
            {
                player.AttendanceCount = 0;
                foreach (Character character in player.Characters)
                {
                    player.AttendanceCount += character.AttendanceCount;
                }
                player.AttendancePercent = ((float )player.AttendanceCount / (float) _maxAttendance) * 100;
            }
        }

        private void ResetCharacterAttendance()
        {
            foreach (Character character in _characters)
            {
                character.AttendanceCount = 0;
            }

            _maxAttendance = 0;
        }

        private string UnixTime(DateTime? selectedDate)
        {
            double reference = new TimeSpan(new DateTime(1970, 1, 1).Ticks).TotalMilliseconds;
            double selectedDateMils = new TimeSpan(selectedDate.Value.Ticks).TotalMilliseconds;
            return (selectedDateMils - reference).ToString();
        }

        private Character CheckCharacterExists(string name)
        {
            foreach (Character character in _characters)
            {
                if (character.Name == name)
                {
                    return character;
                }
            }
            return CreateNewCharacter(name);
        }
    }
}
