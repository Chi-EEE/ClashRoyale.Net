using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Logic
{
    public class Vector2_Helper
    {
        public static double GetDistanceBetweenTwoPoints(Vector2 point1, Vector2 point2)
        {
            return Math.Pow(point1.Y - point2.Y, 2) + Math.Pow(point1.X - point2.X, 2);
        }
        public static double GetTwoRadiusSize(int radius1, int radius2)
        {
            return (radius1 + radius2) * (radius1 + radius2);
        }
        public static bool CheckDistanceFromProjectileAndEntity(Vector2 currentPosition, Vector2 entityPosition, int radius, int entityRadius)
        {
            return GetDistanceBetweenTwoPoints(currentPosition, entityPosition) <= GetTwoRadiusSize(radius, entityRadius);
        }
    }
}
