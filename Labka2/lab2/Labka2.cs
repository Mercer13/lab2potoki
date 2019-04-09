using System;
using System.Threading;

namespace lab2
{

    class MatX
    {
        public int n = 4;
        public double K1 = 0.05;
        public double K2 = 0.05;

        public double[,] B2 = { { 2, 3, 3, 4 }, { 4, 3, 2, 3 }, { 1, 3, 3, 3 }, { 2, 2, 1, 2 } },
            A2 = { { 1, 1, 1, 1 }, { 3, 4, 1, 4 }, { 4, 4, 3, 2 }, { 1, 3, 2, 2 } },
            A1 = { { 2, 4, 4, 3 }, { 3, 4, 4, 1 }, { 2, 3, 4, 4 }, { 2, 2, 3, 4 } },
            A  = { { 2, 4, 4, 3 }, { 1, 3, 4, 1 }, { 3, 3, 1, 4 }, { 3, 3, 4, 4 } },
            b1 = { { 2, 0, 0, 0 }, { 2, 0, 0, 0 }, { 1, 0, 0, 0 }, { 4, 0, 0, 0 } },
            c1 = { { 1, 0, 0, 0 }, { 4, 0, 0, 0 }, { 3, 0, 0, 0 }, { 1, 0, 0, 0 } };

        public double[,] C2, Y3;
        public double[,] b, y1;
        public double[,] y2;
        public double[,] first;
        public double[,] second;
        public double[,] X;

        public MatX()
        {
            this.b = new double[n, n];
            this.C2 = new double[n, n];
            this.y1 = new double[n, n];
            this.y2 = new double[n, n];
            this.Y3 = new double[n, n];

            this.first = new double[n, n];
            this.second = new double[n, n];
            this.X = new double[n, n];
        }

        public void m_Y3()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C2[i, j] = 1 / (i + 1 + Math.Pow((j + 1), 2));  
                    Y3[i, j] = A2[i, j] * (B2[i, j] - C2[i, j]);
                }
            }
        }
        public void m_b()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i % 2 == 0)
                        b[i, j] = Math.Pow(i, 2) / 12;
                    else
                        b[i, j] = i;
                }
            }
        }
        public void m_y1()
        {
            for (int i = 0; i < n; i++)
            {
                    y1[i, 0] = A[i, 0] * b[i, 0];
            }
        }
        public void m_y2()
        {
            for (int i = 0; i < n; i++)
            {
                    y2[i, 0] = A1[i, 0] * (12 * b1[i, 0] - c1[i, 0]);
            }
        }

        public void f()
        {
            for (int i = 0; i < n; i++)
            {
                first[i, 0] = y2[i, 0];
            }
        }
        public void s()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    second[i, j] = K1 * ((y1[i,j] * K2 * (Math.Pow(Y3[i, j], 2) * y1[i, j] + y2[i, j]) * Y3[i, j] + y1[i, j] * y1[i, j])*y2[i,j]);
                }
            }
        }
        
        public void x()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    X[i, j] = first[i, j] + second[i, j];
                }
            }
        }
        public void Output()
        {
            Console.WriteLine("B2: ");
            Print(B2);
            Console.WriteLine("A2: ");
            Print(A2);
            Console.WriteLine("A1: ");
            Print(A1);
            Console.WriteLine("A: ");
            Print(A);
            Console.WriteLine("b1: ");
            Print(b1);
            Console.WriteLine("c1: ");
            Print(c1);

            Console.WriteLine("Result: ");

            Console.WriteLine("Y3: ");
            Print(Y3);
            Console.WriteLine("b: ");
            Print(b);
            Console.WriteLine("y1: ");
            Print(y1);
            Console.WriteLine("y2: ");
            Print(y2);
            Console.WriteLine("f: ");
            Print(first);
            Console.WriteLine("s: ");
            Print(second);
            
            
            Console.WriteLine("x: ");
            Print(X);
            void Print(double[,] arr)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(arr[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            MatX matrix = new MatX();

            Thread t_m_Y3 = new Thread(matrix.m_Y3);
            Thread t_m_b = new Thread(matrix.m_b);
            Thread t_m_y1 = new Thread(matrix.m_y1);
            Thread t_m_y2 = new Thread(matrix.m_y2);
            Thread t_m_f = new Thread(matrix.f);
            Thread t_m_s = new Thread(matrix.s);
            Thread t_m_x = new Thread(matrix.x);

            t_m_Y3.Start();
            t_m_b.Start();
            t_m_y1.Start();
            t_m_y2.Start();
            t_m_f.Start();
            t_m_s.Start();
            t_m_x.Start();            

            t_m_Y3.Join();
            t_m_b.Join();
            t_m_y1.Join();
            t_m_y2.Join();
            t_m_f.Join();
            t_m_s.Join();
            t_m_x.Join();


            matrix.Output();

            Console.ReadKey();
        }
    }
}
