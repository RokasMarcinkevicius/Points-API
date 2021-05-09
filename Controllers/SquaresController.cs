using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdForm.Models;

namespace AdForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquaresController : ControllerBase
    {
        private readonly PointContext _context;

        public SquaresController(PointContext context)
        {
            _context = context;
        }

        
        private Point[] GetRestPints(Point a, Point c)
        {
            Point[] res = new Point[2];

            int midX = (a.X + c.Y) / 2;
            int midY = (a.Y + c.Y) / 2;

            int Ax = a.X - midX;
            int Ay = a.Y - midY;
            int bX = midX - Ay;
            int bY = midY + Ax;
            Point b = new Point(bX,bY);

            int cX =  (c.X - midX);
            int cY =  (c.Y - midY);
            int dX = midX - cY;
            int dY = midY + cX;
            Point d = new Point(dX,dY);

            res[0] = b;
            res[1] = d;
            return res;
        }

        // GET: api/Squares
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Point>>> GetSquares()
        {
            List<Point> points = await _context.Points.ToListAsync();
            
            int count = 0;
            HashSet<Point> set = new HashSet<Point>();

            foreach(var item in points)
            {
                set.Add(item);
            }

            HashSet<Point> squareSet = new HashSet<Point>();
            var comparer = new PointByXandYComparer();

            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    if (i == j)
                        continue;
                    //For each Point i, Point j, check if b&d exist in set.
                    Point[] DiagVertex = GetRestPints(points[i], points[j]);
                    if (set.Contains(DiagVertex[0], comparer) && set.Contains(DiagVertex[1], comparer))
                    {
                        // Ugliest Hashset non duplicate addition ever
                        if(squareSet.Contains(DiagVertex[0], comparer) != true)
                        {
                            count++;
                            squareSet.Add(DiagVertex[0]);
                        }
                        if(squareSet.Contains(DiagVertex[1], comparer) != true)
                        {
                            count++;
                            squareSet.Add(DiagVertex[1]);
                        }
                    }
                }
            }

            //return StatusCode(200, "Count of squares identificed - " + count/4); 
            
            return squareSet.ToList<Point>();
        }    
          
        public class PointByXandYComparer : IEqualityComparer<Point>
        {
            public bool Equals(Point p1, Point p2)
            {
                return (p1.X == p2.X && p1.Y == p2.Y);
            }

            public int GetHashCode(Point p)
            {
                return p.GetHashCode();
            }
        }
    }
}