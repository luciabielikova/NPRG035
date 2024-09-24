using System;

namespace sorter
{
    class Program
    {
        static void sortWords(string filename)
        {
            //StreamWriter sw = new StreamWriter(filename);
            using (StreamReader sr = new StreamReader(filename))
            {
                while (sr.Peek() >= 0)
                {
                    string riadok = sr.ReadLine();
                    if (riadok.Length <= 15 && riadok.Length >= 3)
                    {
                        string writeToFilename = riadok.Length.ToString() + ".txt";
                        StreamWriter sw = new StreamWriter(writeToFilename, append: true);
                        sw.WriteLine(riadok);
                        sw.Close();
                    }

                }
            }
        }

        static void Main(string[] args)
        {
            string filename = "words.txt";
            sortWords(filename);
        }
    }
}
