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

            return StatusCode(200, count/4); 
            //return  squareSet.ToList<Point>();
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

        /* Single Square Check
        static int distSq(Point p, Point q)
        {
            return (p.X - q.X) * (p.X - q.X) + (p.Y - q.Y) * (p.Y - q.Y);
        }

        static bool isSquare(Point p1, Point p2, Point p3, Point p4)
        {
            int d2 = distSq(p1, p2); // from p1 to p2
            int d3 = distSq(p1, p3); // from p1 to p3
            int d4 = distSq(p1, p4); // from p1 to p4
        
            if (d2 == 0 || d3 == 0 || d4 == 0)   
                return false;
        
            // If lengths if (p1, p2) and (p1, p3) are same, then
            // following conditions must met to form a square.
            // 1) Square of length of (p1, p4) is same as twice
            // the square of (p1, p2)
            // 2) Square of length of (p2, p3) is same
            // as twice the square of (p2, p4)
            if (d2 == d3 && 2 * d2 == d4
                && 2 * distSq(p2, p4) == distSq(p2, p3))
            {
                return true;
            }
        
            // The below two cases are similar to above case
            if (d3 == d4 && 2 * d3 == d2
                && 2 * distSq(p3, p2) == distSq(p3, p4))
            {
                return true;
            }
            if (d2 == d4 && 2 * d2 == d3
                && 2 * distSq(p2, p3) == distSq(p2, p4))
            {
                return true;
            }
            return false;
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

            
            return StatusCode(200, isSquare(points[0], points[1], points[2], points[3])); 

            //return  set.ToList<Point>();
        
        }  
        */
    }
}