using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanderWclAttendanceChecker.Model
{
    public class Player
    {
        private string _name;
        private int _attendanceCount;
        private float _attendancePercent;
        private List<Character> _characters;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int AttendanceCount
        {
            get { return _attendanceCount; }
            set { _attendanceCount = value; }
        }

        public float AttendancePercent
        {
            get { return _attendancePercent; }
            set { _attendancePercent = value; }
        }

        public string AttendancePercent_String
        {
            get { return (_attendancePercent.ToString("0.00") + '%'); }
        }

        public Player(string name)
        {
            _name = name;
            _attendanceCount = 0;
            _attendancePercent = 0.0f;

            _characters = new List<Character>();
        }

        public List<Character> Characters
        {
            get { return _characters; }
        }

        public Player WithCharacters(Character value)
        {
            if (this._characters == null) { this._characters = new List<Character>(); }
            if (!this._characters.Contains(value))
            {
                this._characters.Add(value);
                value.SetPlayer(this);
            }

            return this;
        }
        public Player WithCharacters(List<Character> Characters)
        {
            foreach (Character Character in Characters) { WithCharacters(Character); }

            return this;
        }

        public Player WithoutCharacters(Character value)
        {
            if (this._characters != null && this._characters.Remove(value))
            {
                value.SetPlayer(null);
            }

            return this;
        }

        public void RemoveYou()
        {
            foreach (Character character in _characters)
            {
                character.SetPlayer(null);
            }
        }

        override
        public string ToString()
        {
            return _name;
        }
    }
}
