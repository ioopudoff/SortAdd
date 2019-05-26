using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace SortAdd
{
    public class Sort<T>
    {
        //Создание потока для чтения файла 
        public string[] readerString(string file)
        {
            string line;

            List<string> mass = new List<string>();
            StreamReader read = new StreamReader(file, Encoding.Default);
            while (!read.EndOfStream)
            {
                line = read.ReadLine();
                mass.Add(line);
            }
            string[] array = mass.ToArray();
            read.Close();
            return array;
        }

        public int[] readerInt(string file)
        {
            int line;
            string str;
            List<int> mass = new List<int>();
            StreamReader read = new StreamReader(file, Encoding.Default);
            while (!read.EndOfStream)
            {
                str = read.ReadLine();
                if(IsNumber(str))
                {
                    line = Convert.ToInt32(str);
                    mass.Add(line);
                }
            }
            int[] array = mass.ToArray();
            read.Close();
            return array;
        }

        //Проверка на число
        public bool IsNumber(string x)
        {
            foreach(char z in x)
            {
                if(z < '0' || z > '9')
                {
                    return false;
                }
            }
            return true;
        }

        //Слияние массивов
        public T[] Join(T[] arr1, T[] arr2)
        {
            T[] array = new T[arr1.Length + arr2.Length];
            arr1.CopyTo(array, 0);
            arr2.CopyTo(array, arr1.Length);
            return array;
        }

        //Сортировка чисел
        public int[] MergeSortInt(int[] arr)
        {
            int[] left = new int[arr.Length / 2];
            int[] right = new int[arr.Length - left.Length];

            if (arr.Length > 1)
            {
                for (int i = 0; i < left.Length; i++)
                {
                    left[i] = arr[i];
                }
                for (int i = 0; i < right.Length; i++)
                {
                    right[i] = arr[left.Length + i];
                }
                if (left.Length > 1)
                {
                    left = MergeSortInt(left);
                }
                if (right.Length > 1)
                {
                    right = MergeSortInt(right);
                }
                arr = MergeInt(left, right);
            }
            return arr;
        }

        public int[] MergeInt(int[] left, int[] right)
        {
            int l = 0, r = 0;
            int[] merg = new int[left.Length + right.Length];

            for (int i = 0; i < merg.Length; i++)
            {
                if (r >= right.Length)
                {
                    merg[i] = left[l];
                    l++;
                }
                else if (l < left.Length && left[l] < right[r])
                {
                    merg[i] = left[l];
                    l++;
                }
                else
                {
                    merg[i] = right[r];
                    r++;
                }
            }
            return merg;
        }
        //Сортировка строк
        public string[] MergeSortString(string[] arr)
        {
            string[] left = new string[arr.Length / 2];
            string[] right = new string[arr.Length - left.Length];

            if (arr.Length > 1)
            {
                for (int i = 0; i < left.Length; i++)
                {
                    left[i] = arr[i];
                }
                for (int i = 0; i < right.Length; i++)
                {
                    right[i] = arr[left.Length + i];
                }
                if (left.Length > 1)
                {
                    left = MergeSortString(left);
                }
                if (right.Length > 1)
                {
                    right = MergeSortString(right);
                }
                arr = MergeString(left, right);
            }
            return arr;
        }

        public string[] MergeString(string[] left, string[] right)
        {
            int l = 0, r = 0;
            string[] merg = new string[left.Length + right.Length];

            for (int i = 0; i < merg.Length; i++)
            {
                if (r >= right.Length)
                {
                    merg[i] = left[l];
                    l++;
                }
                else if (l < left.Length && CompareStr(left[l], right[r])) 
                {
                    merg[i] = left[l];
                    l++;
                }
                else
                {
                    merg[i] = right[r];
                    r++;
                }
            }
            return merg;
        }
        //Сравнение строк 
        public bool CompareStr(string x, string y)
        {
            if(x.Length<y.Length)
            {
                return true;
            }
            else if(x.Length == y.Length)
            {
                if(string.Compare(x, y)>=0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void Display(T[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string save, type, filename;

            try
            {
                Console.WriteLine("Программа для сортировки слиянием файлов");
                Console.WriteLine("Какого типа данные будут сортироваться? Выберите (int/string)");
                type = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Введите имя выходного файла. Пример: 3.txt");
                save = Convert.ToString(Console.ReadLine());

                string[] mass;

                if (type == "string")
                {
                    Console.WriteLine("Введите имена файлов.");
                    string[] m, res = { "" };
                    filename = Console.ReadLine();
                    while (filename.Length != 0)
                    {
                        Sort<string> M = new Sort<string>();
                        m = M.MergeSortString(M.readerString(filename));
                        res = M.MergeSortString(M.Join(res, m));

                        filename = Console.ReadLine();
                    }
                    mass = new string[res.Length];
                    for (int i = 1; i < res.Length; i++)
                    {
                        mass[i - 1] = res[i].ToString();
                    }

                }
                else if (type == "int")
                {
                    Console.WriteLine("Введите имена файлов.");
                    int[] m, res= {Int32.MinValue};

                    filename = Console.ReadLine();
                    while (filename.Length != 0)
                    {
                        Sort<int> M = new Sort<int>();
                        m = M.MergeSortInt(M.readerInt(filename));
                        res = M.MergeSortInt(M.Join(res, m));

                        filename = Console.ReadLine();
                    }

                    mass = new string[res.Length];
                    for (int i = 1; i < res.Length; i++)
                    {
                       mass[i - 1] = res[i].ToString();
                    }

                    Console.ReadLine();
                }
                else
                {
                    throw new IOException();
                }
                File.WriteAllLines(save, mass, Encoding.Default);

            }
            catch (FormatException)
            {
                Console.WriteLine("Неверный формат данных!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (IndexOutOfRangeException r)
            {
                TextWriter Error = Console.Error;
                Error.WriteLine(r.Message);
            }
            catch (NullReferenceException w)
            {
                TextWriter Err = Console.Error;
                Err.WriteLine(w.Message);
            }
            catch (IOException)
            {
                Console.WriteLine("Файл не найден или ошибка ввода!!!");
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("Ошибка преобразования \n"+e.Message);
            }
            catch (OutOfMemoryException t)
            {
                Console.WriteLine("Память переполнена \n"+t.Message);
            }
            Console.ReadLine();
        }
        
    }
}
