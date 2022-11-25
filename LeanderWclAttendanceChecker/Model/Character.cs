using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanderWclAttendanceChecker.Model
{
    public class Character
    {
        private string _name;
        private int _attendanceCount;
        private Player _player;

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

        public Character(string name)
        {
            _name = name;
            _attendanceCount = 0;
        }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public Character SetPlayer(Player value)
        {
            if (this._player == value)
            {
                return this;
            }

            Player oldValue = this._player;
            if (this._player != null)
            {
                this._player = null;
                oldValue.WithoutCharacters(this);
            }
            this._player = value;
            if (value != null)
            {
                value.WithCharacters(this);
            }

            return this;
        }

        override
        public string ToString()
        {
            return _name;
        }
    }
}
