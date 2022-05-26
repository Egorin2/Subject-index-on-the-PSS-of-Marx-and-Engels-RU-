using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPU
{
    class WordFile
    {
        private char ch;
        public string Name;
        public string[] Str;

        public WordFile(string name, string[] str)
        {
            try
            {
                if (str[0].Contains(name))
                {
                    Name = name;
                    Str = str;
                    if (name[0] == '«')
                    {
                        ch = name[1];
                    }
                    else
                    {
                        ch = name[0];
                    }
                }
                else
                {
                    Console.WriteLine(name + " не прошёл");//Ошибка
                    Console.ReadLine();
                }
            }
            catch
            {
                Console.WriteLine("Хуйня вышла " + name);
            }
        }
        public bool Save(string dir)
        {
            if (Name != null)
            {
                string dirOUT = Path.Combine(dir, ch.ToString());
                var chek = new DirectoryInfo(dirOUT);
                if (!chek.Exists)
                {
                    chek.Create();
                }
                dirOUT = Path.Combine(dirOUT, Name + ".txt");
                File.WriteAllLines(dirOUT, Str);
                return true;
            }
            else
            {
                Console.WriteLine("Файл то пуст");
                return false;
            }
        }
    }
    class LogWork
    {
        private string dir = Path.Combine(Environment.CurrentDirectory, "log.txt");
        public int lastNumber;
        public string lastWord;
        public string breakWord;
        public List<string> words;

        public void Save()
        {
            var list = new List<string>();
            list.Add(lastNumber.ToString());
            list.Add(lastWord);
            list.Add("");
            list.Add("Всё сломалось на: " + breakWord);
            list.Add("");
            //list.AddRange(words);
            File.WriteAllLines(dir, list);
        }

    }
}
