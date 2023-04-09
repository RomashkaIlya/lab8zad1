using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8zad1
{





    internal class Program
    {


        static int[] ReadInfo(string path )
        {
            var numbers = String.Empty;
            using (var sr = new StreamReader(path))//@"D:\файлы\sorted.dat"
            {
                numbers = sr.ReadLine();

            }
            var numbersArray = numbers.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var numberTosearch = new int[numbersArray.Length];
            int cnt = 0;
            foreach (string number in numbersArray)
            {
                numberTosearch[cnt++] = Convert.ToInt32(number);

            }
            return numberTosearch;
        }
        static void ShowResult(int position, long comparisions, TimeSpan interval)
        {
            var posText = string.Empty;
            if (position != -1)
            {
                posText = $"{position}";
            }
            else
            {
                posText = "Не найдено";
            }
            Console.WriteLine($"Позиция элемента - {posText,12}\n"
                            + $"Время работы - {interval.ToString(),16}\n"
                            + $"Кол-во сравнений:{comparisions,10}\n");

        }

        static int SetElement()
        {
            Console.WriteLine("Введите число для поиска:");
            int select = 0;
            var isNotSelected = true;
            while (isNotSelected)
            {
                try
                {
                    select = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("попробуйте снова");

                }
            }
            Console.WriteLine();
            return select;
        }

        static int BinarySearch(int elem, int[] array)
        {
            long comparisons = 0;
            var stopWach = new Stopwatch();
            stopWach.Start();
            int left = 0;
            int right = array.Length - 1;
            while (right >= left)
            {
                int mid = (left + right) / 2;
                if (array[mid] == elem && comparisons++ >= 0)
                {
                    stopWach.Stop();
                    ShowResult(mid, comparisons, stopWach.Elapsed);
                    return mid;
                }
                if (array[mid] > elem && comparisons++ >= 0)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            stopWach.Stop();
            ShowResult(-1, comparisons, stopWach.Elapsed);
            return -1;
        }
        static int InterpolationSrearch(int elem, int[] array)
        {
            long comparisons = 0;
            var stopWach = new Stopwatch();
            stopWach.Start();
            int left = 0;
            int right = array.Length - 1;
            while (array[right] != array[left] && elem >= array[left] && elem <= array[right])
            {
                int mid = left + (elem - array[left]) * ((right - left) / (array[right] - array[left]));
                if (array[mid] < elem & comparisons++ >= 0)
                {
                    left = mid + 1;
                }
                else if (elem < array[mid] & comparisons++ >= 0)
                {
                    right = mid - 1;
                }
                else
                {
                    stopWach.Stop();
                    ShowResult(mid, comparisons, stopWach.Elapsed);
                    return mid;
                }

            }
            if (elem == array[left] & comparisons++ >= 0)
            {
                stopWach.Stop();
                ShowResult(left, comparisons, stopWach.Elapsed);
                return left;
            }
            else
            {
                stopWach.Stop();
                ShowResult(-1, comparisons, stopWach.Elapsed);
                return -1;
            }



        }
        static int LinearSearch(int elem, int[] array)
        {
            long comparasons = 0;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == elem && comparasons++ >= 0)
                {
                    stopWatch.Stop();
                    ShowResult(i, comparasons, stopWatch.Elapsed);
                    return i;

                }
            }
            stopWatch.Stop();
            ShowResult(-1, comparasons, stopWatch.Elapsed);
            return -1;

        }


        static void Main(string[] args)
        {
            var numbersToSearch = new int[1];
            var isFileRead = true;
            try
            {
                numbersToSearch = ReadInfo(Directory.GetCurrentDirectory().ToString() + "\\sorted.dat");
            }
            catch
            {
                isFileRead = false;
                Console.WriteLine("файл не найден");

            }
            if (isFileRead)
            {
                var element = SetElement();
                Console.WriteLine("binary");
                BinarySearch(element, numbersToSearch);

                Console.WriteLine("line");
                BinarySearch(element, numbersToSearch);

                Console.WriteLine("interpol");
                BinarySearch(element, numbersToSearch);

            }
            //Console.ReadLine();
            return;


        }
    }
}