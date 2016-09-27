using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge3
{
    public class Point
    {
        public Point(int x, int y)
        {
            Xcoordinate = x;
            Ycoordinate = y;
        }

        public int Xcoordinate { get; set; }

        public int Ycoordinate { get; set; }
    }

    public class Program
    {
        public static int count;

        public static bool[,] vistedArray;

        public static void Main(string[] args)
        {
            var rows = 0;
            var columns = 0;

            Console.WriteLine("Please enter number of rows");

            var rowsinput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(rowsinput))
            {
                rows = Convert.ToInt32(rowsinput);
            }

            if (rows <= 0 && rows > 100)
            {
                Console.WriteLine("rows should be with in the range of 1 to  100");
                return;
            }

            Console.WriteLine("Please enter number of columns");
            var columnsinput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(columnsinput))
            {
                columns = Convert.ToInt32(columnsinput);
            }

            if (columns <= 0 && columns > 100)
            {
                Console.WriteLine("columns should be with in the range of 1 to  100");
                return;
            }

            var inputArray = new int[rows, columns];

            vistedArray = new bool[rows, columns];

            Console.WriteLine("Please enter input string with only 1's and 0's");

            var inputString = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(inputString))
            {
                var elementPosition = 0;

                for (var j = 0; j < rows; j++)
                {
                    for (var k = 0; k < columns; k++)
                    {
                        if (inputString[elementPosition].ToString() != String.Empty)
                        {
                            var element = Convert.ToInt32(inputString[elementPosition].ToString());

                            if (element == 1 || element == 0)
                            {
                                inputArray[j, k] = element;
                                vistedArray[j, k] = false;
                                elementPosition++;
                            }
                            else
                            {
                                Console.WriteLine("please provide appropriate input");
                            }
                        }
                        else
                        {
                            Console.WriteLine("please provide appropriate input");
                        }
                    }
                }
            }

            var sourcePointXconrdinate = -1;
            var sourcePointYcoordinate = -1;
            var destinationPointXcoordinate = -1;
            var destinationPointYcoordinate = -1;

            Console.WriteLine("Please enter sourcePointXconrdinate");
            var sourcexPoint = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(sourcexPoint))
            {
                sourcePointXconrdinate = Convert.ToInt32(sourcexPoint);
            }

            Console.WriteLine("Please enter sourcePointYcoordinate");
            var sourceYPoint = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(sourceYPoint))
            {
                sourcePointYcoordinate = Convert.ToInt32(sourceYPoint);
            }

            Console.WriteLine("Please enter destinationPointXcoordinate");
            var destinationxPoint = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(destinationxPoint))
            {
                destinationPointXcoordinate = Convert.ToInt32(destinationxPoint);
            }

            Console.WriteLine("Please enter destinationPointYcoordinate");
            var destinationyPoint = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(destinationyPoint))
            {
                destinationPointYcoordinate = Convert.ToInt32(destinationyPoint);
            }

            var sourcePoint = new Point(sourcePointXconrdinate, sourcePointYcoordinate);

            //by default start point is visted point.
            vistedArray[sourcePointXconrdinate, sourcePointYcoordinate] = true;

            var destinationPoint = new Point(destinationPointXcoordinate, destinationPointYcoordinate);

            var sourcePoints = new List<Point> {sourcePoint};

            Recursion(inputArray, sourcePoints, destinationPoint, rows, columns);



        }

        // function to find the number of recursions taken to find destination
        private static void Recursion(int[,] inputArray, List<Point> sourcePoints, Point destinationPoint, int rows,
            int columns)
        {
            var neighbourPoints = GetNeighbourPoints(inputArray, sourcePoints, rows, columns);

            if (!neighbourPoints.Any())
            {
                Console.WriteLine("No");
                Console.Read();
                return;
            }

            var isDestination = CheckIsdestinationAvailable(neighbourPoints, destinationPoint);

            if (!isDestination)
            {
                sourcePoints = new List<Point>(neighbourPoints);
                Recursion(inputArray, sourcePoints, destinationPoint, rows, columns);
            }
            else
            {
                Console.WriteLine("Yes {0}", count);
                Console.Read();
            }
        }

        // checking whether destination point is available in given neighbour points or not
        private static bool CheckIsdestinationAvailable(List<Point> neighbourPoints, Point destinationPoint)
        {
            return
                neighbourPoints.Any(
                    t => t.Xcoordinate == destinationPoint.Xcoordinate && t.Ycoordinate == destinationPoint.Ycoordinate);
        }

        // fecthing the non visted neighbour points of source elements
        private static List<Point> GetNeighbourPoints(int[,] inputArray, List<Point> sourcePoints, int rows, int columns)
        {
            count++;
            var neighbourPoints = new List<Point>();

            foreach (var sourcePoint in sourcePoints)
            {

                if (sourcePoint.Xcoordinate - 1 >= 0 &&
                    (inputArray[sourcePoint.Xcoordinate - 1, sourcePoint.Ycoordinate]) != 0 &&
                    !vistedArray[sourcePoint.Xcoordinate - 1, sourcePoint.Ycoordinate])
                {
                    neighbourPoints.Add(new Point(sourcePoint.Xcoordinate - 1, sourcePoint.Ycoordinate));
                    vistedArray[sourcePoint.Xcoordinate - 1, sourcePoint.Ycoordinate] = true;
                }
                if (sourcePoint.Ycoordinate - 1 >= 0 &&
                    (inputArray[sourcePoint.Xcoordinate, sourcePoint.Ycoordinate - 1]) != 0 &&
                    !vistedArray[sourcePoint.Xcoordinate, sourcePoint.Ycoordinate - 1])
                {
                    neighbourPoints.Add(new Point(sourcePoint.Xcoordinate, sourcePoint.Ycoordinate - 1));
                    vistedArray[sourcePoint.Xcoordinate, sourcePoint.Ycoordinate - 1] = true;
                }
                if (sourcePoint.Xcoordinate + 1 < rows &&
                    (inputArray[sourcePoint.Xcoordinate + 1, sourcePoint.Ycoordinate]) != 0 &&
                    !vistedArray[sourcePoint.Xcoordinate + 1, sourcePoint.Ycoordinate])
                {
                    neighbourPoints.Add(new Point(sourcePoint.Xcoordinate + 1, sourcePoint.Ycoordinate));
                    vistedArray[sourcePoint.Xcoordinate + 1, sourcePoint.Ycoordinate] = true;
                }
                if (sourcePoint.Ycoordinate + 1 < columns &&
                    (inputArray[sourcePoint.Xcoordinate, sourcePoint.Ycoordinate + 1]) != 0 &&
                    !vistedArray[sourcePoint.Xcoordinate, sourcePoint.Ycoordinate + 1])
                {
                    neighbourPoints.Add(new Point(sourcePoint.Xcoordinate, sourcePoint.Ycoordinate + 1));
                    vistedArray[sourcePoint.Xcoordinate, sourcePoint.Ycoordinate + 1] = true;
                }
            }
            return neighbourPoints;
        }
    }
}
