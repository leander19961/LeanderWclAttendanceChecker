using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using LeanderWclAttendanceChecker.API.Web;
using LeanderWclAttendanceChecker.View.LeanderWclAttendanceCheckerViewer;

namespace LeanderWclAttendanceChecker.Model
{
    public class ModelService
    {
        private LeanderWclAttendanceCheckerView _mainWindow;

        private WebAPI _webAPI;

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

        ModelService(LeanderWclAttendanceCheckerView mainWindow, string baseUrl)
        {
            _mainWindow = mainWindow;
            _baseUrl = baseUrl;
            _maxAttendance = 0;

            _webAPI = new WebAPI();

            _players = new List<Player>();
            _characters = new List<Character>();
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

        public void GetCharacterAttendance(string startTime, string endTime)
        {
            List<string> reports = JsonParser.GetReportsFromJson(_webAPI.GETLogList(startTime, endTime));
            foreach (string report in reports)
            {
                List<string> characters = JsonParser.GetCharactersFromJson(_webAPI.GETReport(report));
                foreach (string character in characters)
                {
                    Character newCharacter = CheckCharacterExists(character);
                    if (newCharacter == null)
                    {
                        newCharacter = new Character(character)
                        {
                            AttendanceCount = 1,
                        };

                        _characters.Add(newCharacter);
                    }
                    else
                    {
                        newCharacter.AttendanceCount++;
                    }
                }
                _maxAttendance++;
            }
        }

        public void GetPlayerAttendance()
        {
            foreach (Player player in _players)
            {
                foreach (Character character in player.Characters)
                {
                    player.AttendanceCount += character.AttendanceCount;
                }
                player.AttendancePercent = player.AttendanceCount / _maxAttendance;
            }
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
            return null;
        }
    }
}
