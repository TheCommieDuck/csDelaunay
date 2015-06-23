using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csDelaunay
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 3; ++i)
            {
                List<Vector2f> vertices = new List<Vector2f>();
                Random r = new Random();
                for (int f = 0; f < 100000; ++f)
                {
                    vertices.Add(new Vector2f(r.NextDouble() * 100, r.NextDouble() * 100));
                }
                Stopwatch s = new Stopwatch();
                s.Start();
                var n = new Voronoi(vertices, new Rectf(0, 0, 100, 100));
                s.Stop();
                Console.WriteLine(s.ElapsedMilliseconds);
            }
        }
    }
}
