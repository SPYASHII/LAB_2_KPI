using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace LAB_2_KPI
{
    public class DinArr
    {
        private Random rand = new Random();
        public int[] arr { get; }
        public int length { get; }
        public DinArr(int length)
        {
            if (length < 0)
                length = 0;
            arr = new int[length];
            this.length = length;
        }

        [JsonConstructor]
        public DinArr(int[] arr)
        {
            if (arr != null)
                this.arr = arr;
            else
                this.arr = new int[0];
            length = this.arr.Length;
        }
        ~DinArr()
        {
            Console.WriteLine("What am i exist for?");
        }
        public void FillRandom()
        {
            for (int i = 0; i < length; i++)
            {
                arr[i] = rand.Next(1, 10);
            }
        }
        public void Randomize()
        {
            for (int i = 0; i < length; i++)
            {
                int randNum = rand.Next(0, length - 1);
                int num = arr[randNum];
                arr[randNum] = arr[i];
                arr[i] = num;
            }
        }
        public int FindNumOf(int value)
        {
            int num = arr.Count(a => a == value);
            return num;
        }
        public void ShowArr()
        {
            foreach(var i in arr)
            {
                Console.Write(i + "|");
            }
        }
    }
    class Program
    {
        static void Main()
        {
            var a = new DinArr(10);

            a.FillRandom();
            a.ShowArr();
            Console.WriteLine("\n" + a.FindNumOf(5));
            a.Randomize();
            a.ShowArr();
            Console.WriteLine("\n" + a.FindNumOf(5));
            
            SaveJson(a);

            try
            {
                var b = LoadJson();

                Console.WriteLine($"Arr b length: \n{b.length}\n");
                b.ShowArr();
            }
            catch(Exception)
            {
                Console.WriteLine("Some error with Json file");
            }
            Console.ReadKey();
        }
        static void SaveJson(DinArr a)
        {
            string json = JsonSerializer.Serialize(a);
            File.WriteAllText("json.json", json);
        }
        static DinArr LoadJson()
        {
            string json = File.ReadAllText("json.json");
            return JsonSerializer.Deserialize<DinArr>(json);
        }
    }
}
