using System;

namespace GeometricFigures
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void PointMove(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }

        public void PointRotate(double angle)
        {
            double rad = angle * Math.PI / 180;
            double newX = X * Math.Cos(rad) - Y * Math.Sin(rad);
            double newY = X * Math.Sin(rad) + Y * Math.Cos(rad);
            newX = Math.Round(newX, 0);
            newY = Math.Round(newY, 0);
            X = newX;
            Y = newY;
        }
    }

    public class Line
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Line(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public void LineMove(double dx, double dy)
        {
            StartPoint.PointMove(dx, dy);
            EndPoint.PointMove(dx, dy);
        }

        public void LineRotate(double angle)
        {
            StartPoint.PointRotate(angle);
            EndPoint.PointRotate(angle);
        }
    }

    public class Circle
    {
        public Point Center { get; set; }
        public double Radius { get; set; }

        public Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public void CenterMove(double dx, double dy)
        {
            Center.PointMove(dx, dy);
        }

        public void CircleRotate(double angle)
        {
            Center.PointRotate(angle);
        }
    }

    public class Aggregation
    {
        private List<object> figures;
        public Aggregation()
        {
            figures = new List<object>();
        }
        public void AddFigure(object figure)
        {
            figures.Add(figure);
        }
        public void AggMove(double deltaX, double deltaY)
        {
            foreach (object figure in figures)
            {
                if (figure is Point point)
                {
                    point.PointMove(deltaX, deltaY);
                }
                else if (figure is Line line)
                {
                    line.LineMove(deltaX, deltaY);
                }
                else if (figure is Circle circle)
                {
                    circle.CenterMove(deltaX, deltaY);
                }
            }
        }
        public void AggRotate(double angle)
        {
            foreach (object figure in figures)
            {
                if (figure is Point point)
                {
                    point.PointRotate(angle);
                }
                else if (figure is Line line)
                {
                    line.LineRotate(angle);
                }
                else if (figure is Circle circle)
                {
                    circle.CircleRotate(angle);
                }
            }
        }
    }

    class GeometricFigures
    {
        static void Main(string[] args)
        {
            Point point1 = new Point(0, 1); // Create a point at (0, 1)
            Point point2 = new Point(2, 3); // Create a point at (2, 3)
            Console.WriteLine($"P1: ({point1.X}, {point1.Y})");
            Console.WriteLine($"P2: ({point2.X}, {point2.Y})");

            Line line1 = new Line(new Point(1, 2), new Point(3, 4)); // Create a line of P1(1, 1) and P2(2, 2)
            Console.WriteLine($"Line P1: ({line1.StartPoint.X}, {line1.StartPoint.Y})");
            Console.WriteLine($"Line P2: ({line1.EndPoint.X}, {line1.EndPoint.Y})");

            Circle circle = new Circle(new Point(1, 2), 3); // Create a circle with center (1, 2) and radius 3
            Console.WriteLine($"Circle center: ({circle.Center.X}, {circle.Center.Y})");
            Console.WriteLine($"Circle radius: {circle.Radius}");

            Aggregation agg = new Aggregation();
            agg.AddFigure(point1);
            agg.AddFigure(point2);
            agg.AddFigure(line1);
            agg.AddFigure(circle);

            agg.AggMove(2, 2);
            Console.WriteLine("\nMove (2,2)");
            Console.WriteLine($"Point1: ({point1.X}, {point1.Y})");
            Console.WriteLine($"Point2: ({point2.X}, {point2.Y})");
            Console.WriteLine($"Line P1: ({line1.StartPoint.X}, {line1.StartPoint.Y})");
            Console.WriteLine($"Line P2: ({line1.EndPoint.X}, {line1.EndPoint.Y})");
            Console.WriteLine($"Circle center: ({circle.Center.X}, {circle.Center.Y})");

            agg.AggRotate(90);
            Console.WriteLine("\nRotate 90 Degrees");
            Console.WriteLine($"Point1: ({point1.X}, {point1.Y})");
            Console.WriteLine($"Point2: ({point2.X}, {point2.Y})");
            Console.WriteLine($"Line P1: ({line1.StartPoint.X}, {line1.StartPoint.Y})");
            Console.WriteLine($"Line P2: ({line1.EndPoint.X}, {line1.EndPoint.Y})");
            Console.WriteLine($"Circle center: ({circle.Center.X}, {circle.Center.Y})");
        }
    }
}
