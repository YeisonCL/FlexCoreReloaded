using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCore
{
    static class Settings
    {
        private static readonly string OLD_DB_OLD_ACCESS = "BD sin optimizar, DTOs y DAOs";
        private static readonly string OLD_DB_OPT_ACCESS = "BD sin optimizar, ORM";
        private static readonly string OPT_DB_OLD_ACCESS = "BD optimizada, DTOs y DAOs";
        private static readonly string OPT_DB_OPT_ACCESS = "BD optimizada, ORM";

        private static string _curentVersion = OPT_DB_OPT_ACCESS;

        public static List<string> getDataBaseVersions()
        {
            List<string> list = new List<string>();
            list.Add(OLD_DB_OLD_ACCESS);
            list.Add(OLD_DB_OPT_ACCESS);
            list.Add(OPT_DB_OLD_ACCESS);
            list.Add(OPT_DB_OPT_ACCESS);
            return list;
        }

        public static void setCurrentDatabase(string pVersion)
        {
            _curentVersion = pVersion;
        }

        public static string getCurrentVersion()
        {
            return _curentVersion;
        }

        public static string getCurrentIPAndPort()
        {
            if (_curentVersion == OLD_DB_OLD_ACCESS)
            {
                return "http://192.168.1.124:6358";
            }
            else if (_curentVersion == OLD_DB_OPT_ACCESS)
            {
                return "http://192.168.1.124:6358";
            }
            else if (_curentVersion == OPT_DB_OLD_ACCESS)
            {
                return "http://192.168.1.124:6358";
            } 
            else if (_curentVersion == OPT_DB_OPT_ACCESS)
            {
                return "http://192.168.1.124:6358";
            }
            return "";
        }
    }
}
