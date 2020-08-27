using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
    

    public static class SegmentExt
    {
        public static Dictionary<Segment, Color> Dic = new Dictionary<Segment, Color>();
        public static void SetColor(this Segment sector, Color color)
        {
            if (Dic.ContainsKey(sector))
            {
                Dic[sector] = color;
            }
            else
            {
                Dic[sector] = new Color();
                Dic[sector] = color;
			}
		}
        public static Color GetColor(this Segment sector)
        {
            if (Dic.ContainsKey(sector))
                return Dic[sector];

            return Color.Black;
		}
	}
}
