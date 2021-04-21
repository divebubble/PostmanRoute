using System.Collections.Generic;

namespace PostmanRoute.Classes
{
    class MapOfRoutes
    {
        private List<Waypoint> Waypoints { get; set; }
        public List<Route> Routes { get; }
        public MapOfRoutes()
        {
            Waypoints = new List<Waypoint>();
            Routes = new List<Route>();
        }

        public void AddWaypoint(int Id, string Caption)
        {
            Waypoints.Add(new Waypoint { Id = Id, Caption = Caption });
        }
        public void AddRoute(int WpId1, int WpId2)
        {
            Routes.Add(new Route { Id = Routes.Count + 1, Wp1 = GetWaypoint(WpId1), Wp2 = GetWaypoint(WpId2) });
        }
        public Route GetRoute(int WpId1, int WpId2)
        {
            for (int i = 0; i < Routes.Count; i++)
            {
                if (Routes[i].Wp1.Id == WpId1 && Routes[i].Wp2.Id == WpId2) return Routes[i];
            }
            return null;
        }
        public Waypoint GetWaypoint(int Id)
        {
            for (int i = 0; i < Waypoints.Count; i++)
            {
                if (Waypoints[i].Id == Id) return Waypoints[i];
            }
            return null;
        }
    }
}
