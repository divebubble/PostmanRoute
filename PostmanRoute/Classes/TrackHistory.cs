using System;
using System.Collections.Generic;

namespace PostmanRoute.Classes
{
    class TrackHistory
    {
        private List<Route> Routes { get; }

        public TrackHistory()
        {
            Routes = new List<Route>();
        }

        public void AddRoute(Route route)
        {
            Routes.Add(route);
            Console.WriteLine(route.Wp1.Caption + " -> " + route.Wp2.Caption);
        }

        // Check if route is already done, optional check way back 
        public bool CheckIsNewRoute(Route route, bool checkWayBack)
        {
            if (route == null) return false;

            for (int i = 0; i < Routes.Count; i++)
            {
                if (Routes[i].Id == route.Id) return false;

                // other Id, same route
                if (Routes[i].Wp1.Id == route.Wp1.Id && Routes[i].Wp2.Id == route.Wp2.Id) return false;

                if (checkWayBack)
                {
                    if (Routes[i].Wp2.Id == route.Wp1.Id && Routes[i].Wp1.Id == route.Wp2.Id) return false;
                }
            }

            return true;
        }
    }
}
