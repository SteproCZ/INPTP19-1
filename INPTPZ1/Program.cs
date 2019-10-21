using System;
using System.Collections.Generic;
using System.Drawing;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private static int width, height;
        private static double xMax, xMin, yMin, yMax, xStep, yStep;
        private static Bitmap bitmapPicture;
        private static List<ComplexNumber> roots;
        private static Polynome polynome, derivatedPolynom;
        private static Color[] colors;
        private const int BRIGHTNESS = 30;
        private const double TOLERANCE = 0.5;
        private const double MINIMUM = 0.01;

        private static void Initialize(int widthParameter, int heightParameter)
        {
            width = widthParameter;
            height = heightParameter;

            Bitmap bitmap = new Bitmap(width, height);

            xMax = 1.5;
            xMin = -1.5;

            yMin = xMin;
            yMax = xMax;

            xStep = (xMax - xMin) / width;
            yStep = (yMax - yMin) / height;

            bitmapPicture = new Bitmap(width, height);

            roots = new List<ComplexNumber>();
            polynome = CreatePolynome();
            derivatedPolynom = polynome.Derive();

            colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            Console.WriteLine(polynome);
            Console.WriteLine(derivatedPolynom);
        }

        private static Polynome CreatePolynome()
        {
            Polynome polynome = new Polynome();
            polynome.Coeficients.Add(new ComplexNumber() { Re = 1 });
            polynome.Coeficients.Add(ComplexNumber.Zero);
            polynome.Coeficients.Add(ComplexNumber.Zero);
            polynome.Coeficients.Add(new ComplexNumber() { Re = 1 });
            return polynome;
        }

        private static void CheckZeroComplexNumber(ComplexNumber complexNumber)
        {
            if (complexNumber.Re == 0)
                complexNumber.Re = 0.0001;
            if (complexNumber.Im == 0)
                complexNumber.Im = 0.0001;
        }

        private static void MakePixelOfImage(int numberOfRoots, int iteration, int xCoordinate, int yCoordinate)
        {
            Color color = colors[numberOfRoots % colors.Length];
            int red = Math.Min(Math.Max(0, color.R - iteration * 2), 255);
            int blue = Math.Min(Math.Max(0, color.B - iteration * 2), 255);
            int green = Math.Min(Math.Max(0, color.G - iteration * 2), 255);
            color = Color.FromArgb(red, green,blue);
            bitmapPicture.SetPixel(yCoordinate, xCoordinate, color);
        }

        static void Main(string[] args)
        {
            try
            {
                int inputNumber = int.Parse(args[0]);
                if (inputNumber<=0)
                {
                    Console.WriteLine("The input argument is the image size. Must be greater than zero.");
                    return;
                }

                Initialize(inputNumber, inputNumber);

                for (int xCoordinate = 0; xCoordinate < width; xCoordinate++)
                {
                    for (int yCoordinate = 0; yCoordinate < height; yCoordinate++)
                    {
                        ComplexNumber complexNumber = new ComplexNumber()
                        {
                            Re = xMin + yCoordinate * xStep,
                            Im = yMin + xCoordinate * yStep
                        };

                        CheckZeroComplexNumber(complexNumber);

                        int iteration = 0;
                        FinderSolutionByNewtonIteration(ref iteration, ref complexNumber);

                        var numberOfRoots = 0;
                        FinderSolutionRoot(complexNumber, ref numberOfRoots);

                        MakePixelOfImage(numberOfRoots, iteration, xCoordinate, yCoordinate);
                    }
                }
                bitmapPicture.Save("../../../out.png");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("ArgumentNullException \n"+ex);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ArgumentException \n" + ex);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("FormatException \n" + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception \n" + ex);
            }            
        }

        private static void FinderSolutionRoot(ComplexNumber complexNumber, ref int numberOfRoots)
        {
            bool knownRoot = false;

            for (int i = 0; i < roots.Count; i++)
            {
                double realParth = Math.Pow(complexNumber.Re - roots[i].Re, 2);
                double imaginaryParth = Math.Pow(complexNumber.Im - roots[i].Im, 2);
                
                if (realParth + imaginaryParth <= MINIMUM)
                {
                    knownRoot = true;
                    numberOfRoots = i;
                }
            }
            if (!knownRoot)
            {
                roots.Add(complexNumber);
                numberOfRoots =  roots.Count;
            }
        }

        private static void FinderSolutionByNewtonIteration(ref int iteration, ref ComplexNumber complexNumber)
        {
            
            for (int i = 0; i < BRIGHTNESS; i++)
            {
                var diff = polynome.Eval(complexNumber).Divide(derivatedPolynom.Eval(complexNumber));
                complexNumber = complexNumber.Subtract(diff);

                if (Math.Pow(diff.Re, 2) + Math.Pow(diff.Im, 2) >= TOLERANCE)
                {
                    i--;
                }
                iteration++;
            }
        }
    }
}
