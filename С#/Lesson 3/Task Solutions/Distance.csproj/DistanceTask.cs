using System;
using System.Xml.Linq;

namespace DistanceTask
{
    public static class DistanceTask
    {
        // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            Func<double, double, double> sumin2 = (xx, yy) => Math.Sqrt(Math.Pow(xx, 2) + Math.Pow(yy, 2));

            double a = sumin2(ax - x, ay - y);
            double b = sumin2(bx - x, by - y);
            double c = sumin2(ax - bx, by - ay);

            double xa = (a <= b) ? ax : bx;
            double ya = (a <= b) ? ay : by;
            double xb = (a <= b) ? bx : ax;
            double yb = (a <= b) ? by : ay;

            double x1 = x - xa;
            double y1 = y - ya;
            double x2 = xb - xa;
            double y2 = yb - ya;

            double angle = (x1 * x2 + y1 * y2) / ((sumin2(x1, y1) * sumin2(x2, y2)));
            angle = Math.Acos(angle) * (180 / Math.PI);

            if (double.IsNaN(angle))
                angle = 0;
            else
                angle %= 180;


            double ans = Math.Sin(angle * Math.PI / 180) * sumin2(xa - x, ya - y);

            bool outline = y == (-(ax * by) + bx * ay - (ay - by) * x) / (bx - ax) && ((y > Math.Max(ay, by)) || (y < Math.Min(ay, by)) || (x > Math.Max(ax, bx)) || (x < Math.Min(ax, bx)));

            if ((angle >= 90) || outline || ((ax == bx) && (ay == by)))
                return Math.Min(a, b);
            else if ((ans == 0) && (ax == bx) && (bx == x) && ((y > Math.Max(ay, by)) || (y < Math.Min(ay, by))))
                return Math.Min(a, b);
            else
                return ans;
        }
    }
}