using PostmanRoute.Classes;
using System;
using System.IO;

namespace PostmanRoute
{
    class Startup
    {
        private readonly MapOfRoutes myMap = new MapOfRoutes();
        private readonly TrackHistory myTrack = new TrackHistory();
        public Startup()
        {
            string exeName = Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            int Start = LoadMapData(Path.ChangeExtension(exeName, ".set"));

            Console.WriteLine("Start " + myMap.GetWaypoint(Start).Caption);
            SearchRoute(Start, Start);
            Console.WriteLine("Done");
        }

        public void SearchRoute(int lastWpId, int actWpId)
        {
            // Strecke mit Start actWpId in allen möglichen Strecken suchen die noch nicht (hin oder zurück) gefahren wurde
            foreach (var r in myMap.Routes)
            {
                if (r.Wp1.Id == actWpId && myTrack.CheckIsNewRoute(r, true))
                {
                    myTrack.AddRoute(r);
                    SearchRoute(r.Wp1.Id, r.Wp2.Id);
                    break;
                }
            }

            // Wenn keine neue Strecke gefunden dann Gegenrichtung suchen
            var wayBackRoute = myMap.GetRoute(actWpId, lastWpId);
            if (myTrack.CheckIsNewRoute(wayBackRoute, false))
            {
                myTrack.AddRoute(wayBackRoute);
                SearchRoute(wayBackRoute.Wp1.Id, wayBackRoute.Wp2.Id);
            }
        }
        public int LoadMapData(string configFile)
        {
            var myIni = new IniFile(configFile);

            int n = Int32.Parse(myIni.Read("count", "waypoint"));

            for (int i = 1; i <= n; i++) myMap.AddWaypoint(i, myIni.Read(i.ToString(), "waypoint"));

            n = Int32.Parse(myIni.Read("count", "route"));

            for (int i = 1; i <= n; i++)
            {
                string rDef = myIni.Read(i.ToString(), "route");
                int wpId1 = Int32.Parse(GetToken(rDef, 1, ","));
                int wpId2 = Int32.Parse(GetToken(rDef, 2, ","));

                myMap.AddRoute(wpId1, wpId2);
                myMap.AddRoute(wpId2, wpId1);
            }

            return myIni.ReadInteger("start", "system");
        }

        public string GetToken(string strValue, int tokenPos, string separator)
        {
            string[] tokens = strValue.Split(separator);

            if (tokens.Length >= tokenPos) return tokens[tokenPos - 1];

            return "";
        }

    }
}
