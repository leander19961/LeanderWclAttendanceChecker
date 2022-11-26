using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanderWclAttendanceChecker.Resource
{
    public static class Constants
    {
        public static readonly string DatastorePath = @"Datastore\LeanderWclAttendanceChecker";
        public static readonly string PlayerstorePath = DatastorePath + @"\Playerstore";

        public static readonly string PlayerstoreXml = PlayerstorePath + @"\Playerstore.xml";
    }
}
