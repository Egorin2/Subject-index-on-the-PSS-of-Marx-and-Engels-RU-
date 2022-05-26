using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestPU
{
    class JobVoid
    {
        static public char[] AlfaBeta =
        {
            'а','б','в','г','д','е','ё','ж','з','и','й','к','л','м','н','о','п','р','с','т','у','ф','х','ц','ч','ш','щ','ъ','ы','ь','э','ю','я', ' ','«','»','(',')'
        };
        static public string Replase(string str)
        {
            var result = str.Replace("\r\n", " ");
            result = result.Replace('	', ' ');
            return result;
        }
        static public bool isPU(string str)
        {
            if (str.Contains("См."))
            {
                return true;
            }else if (str.Contains("см."))
            {
                return true;
            }
            else if (str.Contains("Cм."))
            {
                return true;
            }
            {
                return false;
            }
        }
        static public string Trim(string str)
        {
            char[] ch = { '\r', '\n', ' ','.' };
            var result = str.Trim(ch);
            return result;
        }
        static public bool inArray(string name, string[] arr)
        {
            
            foreach (var i in arr)
            {
                if (name == i)
                {
                    return true;
                }
            }
            return false;
        }
        static public bool inArray(char name, char[] arr)
        {

            foreach (var i in arr)
            {
                if (name == i)
                {
                    return true;
                }
            }
            return false;
        }

        static public string[] FirstDefed (string[] arr)
        {
            string[] result = new string[arr.Length - 1];
            for(int i = 1; i<arr.Length; i++)
            {
                result[i - 1] = arr[i];
            }
            return result;
        }
        static public string[] FirstDefed (string []arr, int i)
        {
            string[] result = new string[arr.Length - i];
            for (int index = i; index < arr.Length; index++)
            {
                result[index - i] = arr[index];
            }
            return result;
        }
        static public void Folder(string path)
        {
            var folder = new DirectoryInfo(path);
            if (!folder.Exists)
            {
                folder.Create();
            }
        }
        static public bool Contains(string str)
        {
            if (str.Contains("\r\n"))
            {
                return true;
            }else if (str.Contains('	'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    static class WorkExtencion
    {
        static public List<string> error = new List<string>();
        static public bool isNull
        {
            get
            {
                if (error.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        static public void Clear()
        {
            error = new List<string>();
        }
        static public void Push(string str)
        {
            error.Add("-----------");
            error.Add(str);
        }
    }
    
    class Page
    {
        private int page;
        private int startPage;
        private int endPage;
        private bool isUnit;
        public Page(string str)
        {
            char[] ch = { ' ', '.', '\r', '\n', '-' };
            if (String.IsNullOrEmpty(str))
            {
                throw new NullReferenceException("Буду я из пустоты страницу делать. Угу.");
            }
            str = str.Trim(ch);
            if (str.Contains('-'))
            {
                isUnit = false;
                var arr = str.Split('-');
                arr[0] = arr[0].Trim(ch);
                arr[1] = arr[1].Trim(ch);
                if (arr[0].Contains(' ')^arr[1].Contains(' '))
                {
                    throw new ArgumentException("Потеряна запятая или ;");
                }
                var oneInt = int.Parse(arr[0], System.Globalization.NumberStyles.None);
                var twoInt = int.Parse(arr[1], System.Globalization.NumberStyles.None);
                if (oneInt < twoInt)
                {
                    startPage = oneInt;
                    endPage = twoInt;
                } else if (oneInt > twoInt)
                {
                    WorkExtencion.Push("ПОРЯДОК НАПУТАН!\n" + str);
                }
                else
                {
                    WorkExtencion.Push("Две одинаковые страницы\n" + str);
                }
            }
            else
            {
                isUnit = true;
                str = str.Trim(ch);
                if(str.Contains(' '))
                {
                    throw new ArgumentException("Потеряна запятая или ;");
                }
                page = int.Parse(str, System.Globalization.NumberStyles.None);
            }
        }
        public override string ToString()
        {
            string str = "";
            if (isUnit)
            {
                str = page.ToString();
            }
            else
            {
                str = startPage.ToString() + "-" + endPage.ToString();
            }
            return str;
        }
    }
    class Tom
    {
        static public string[] NumberTom = {
        "1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25 I","25 II","26 I","26 II","26 III","27","28","29","30","31","32","33","34","35","36","37","38","39",
        "I","II","III", "40"
        };
        static List<char> Char = new List<char>();
        static List<char> CharInTom = new List<char>();
        static public List<string> AllTom = new List<string>();
        static public List<string> AllLink = new List<string>();
        private string link;
        public bool isLink;
        private string tom;
        public string Book
        {
            get { return tom; }
            set
            {
                string[] chek = {"111", "86", "422", "505" };
                if (value.Contains('-'))
                {
                    throw new ArgumentException("Загнали что-то в номер тома\nТипа: "+value);
                }
                if (value == "")
                {
                    throw new NullReferenceException("Пустота была ему ответом");
                }
                var str = value.Trim(' ');                
                if (!JobVoid.inArray(str, AllTom.ToArray()))
                {
                    AllTom.Add(str);
                }
                tom = str;
                foreach(var i in str)
                {
                    if (JobVoid.inArray(i, CharInTom.ToArray()))
                    {
                        CharInTom.Add(i);
                    }
                }
            }
        }
        public Page[] pages;
        static private string[] chek = { "III", "II", "I" };

        public Tom(string str)
        {
            if (!JobVoid.isPU(str)) 
            {
                isLink = false;
                str = JobVoid.Trim(str);
                if (str.Contains(','))
                { 
                var arr = str.Split(',');
                var chekTom = JobVoid.Trim(arr[1]);
                if (JobVoid.inArray(chekTom, chek))
                {
                    Book = JobVoid.Trim(arr[0]) + " " + chekTom;
                    arr = JobVoid.FirstDefed(arr, 2);
                }
                else
                {
                    Book = arr[0];
                    arr = JobVoid.FirstDefed(arr);
                }
                pages = new Page[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                {
                    try
                    {
                        pages[i] = new Page(arr[i]);
                    }
                    catch (Exception e)
                    {
                        string error = "\n----------------------\nНеудачка при преобразовании в числовую страницу. Пишет:\n"+e.Message+"\nЗагоняли: "+ arr[i] +"\nВ "+str;
                        WorkExtencion.Push(error);
                    }
                }
                var job = this.ListLink();                
                foreach (var i in job)
                {
                    if (!JobVoid.inArray(i, AllLink.ToArray()))
                    {
                        AllLink.Add(i);
                    }
                }
            }
            else
            {
                    WorkExtencion.Push("Обозначенная ссылка не может быть обработана в числовую ссылку \n" + str);
            }
            }
            else
            {
                isLink = true;
                link = str;
            }
        }

        public override string ToString()
        {
            if (isLink)
            {
                var result = "\r\n[PU]";
                char[] ch = { ' ', ',', '.' };
                if (JobVoid.Contains(link))
                {
                    result = result + JobVoid.Replase(link);
                    result = result.TrimEnd(ch);
                    return result;
                }
                else
                {
                    result = result + link.TrimEnd(ch);                    
                }
                result = result + "\r\n";
                return result;
            }
            else
            {
                var str = tom + ": ";
                foreach (var i in pages)
                {
                    str = str + i + ", ";
                }
                char[] ch = { ' ', ',' };
                str = str.TrimEnd(ch);
                return str;
            }
        }
        public int Count
        {
            get
            {
                if (isLink) { return 1; } else {
                    return pages.Length; }
            }
        }
        public string[] ListLink()
        {
            if (isLink)
            {
                string[] result = new string[1];
                result[0] = link;
                return result;
            }
            else
            {
                string[] arr = new string[pages.Length];
                int indx = 0;
                foreach (var i in pages)
                {
                    arr[indx] = tom + ": " + i;
                }
                return arr;
            }
        }
       static public Tom[] Toms(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new NullReferenceException(String.Format("Строка, которую переделываем в ссылку - пустая!"));
            }
            char[] ch = { '\r', '\n', ';' , ',', '.', ' '};
            str = str.Trim(ch);
            var arr = str.Split(';');
            var result = new Tom[arr.Length];
            int i = 0;
            foreach(var item in arr)
            {
                result[i] = new Tom(item);
                i++;
            }
            return result;
        }
    }

    class UnderPage
    {
        private string name;
        public string Name
        {
            get {
                if (JobVoid.Contains(name))
                {
                    return JobVoid.Replase(name);
                }
                else
                {
                    return name;
                } 
            }
            set
            {
                var str = value.Trim(' ');
                if (!str.Contains(';'))
                {
                    name = str;
                }
                else
                {
                    throw new ArgumentException(String.Format("Предположительно в наших названиях нет ;\nГлянь на {0}", value));
                }
            }
        }
        public Tom[] Links;

        public int LinkCount
        {
            get
            {
                int i = 0;
                foreach (var item in Links)
                {
                    i = i + item.Count;
                }
                return i;
            }
        }
        public override string ToString()
        {
            var str = "[Categori] ";
            if (!String.IsNullOrEmpty(name))
            {
                str =str+ Name;
            }
            str = str + "\r\n[Link] ";
            foreach (var i in Links)
            {
                if (i.isLink)
                {
                    str = str + i.ToString() + "[Link] ";
                }
                else
                {
                    str = str + i.ToString() + "; ";
                }
            }
            char[] ch = { ' ', ';' };
            str = str.TrimEnd(ch);
            return str;
        }
        public int PULink
        {
            get
            {
                int i = 0;
                foreach (var item in Links)
                {
                    if (item.isLink)
                    {
                        i++;
                    }
                }
                return i;
            }
        }

        
    }
    static class JobString
    {
        static public bool isRubric = false;
        static public int step = 0;
        static public string path;
        static public PredmetWork General;
        static public string[] OpenFile;
        static public List<string> FailFile = new List<string>();
        static public UnderPage categorie;
        static private PredmetWork link;

        static public void Join(PredmetWork obj)
        {
            link = obj;
            categorie = null;
            WorkExtencion.Clear();
            isRubric = false;
            General = null;
            step = 0;
        }
        static public void Push(string str)
        {
            
            if (!str.Contains(';'))
            {
                if (categorie == null)
                {
                    categorie = new UnderPage();
                    categorie.Name = str;
                }
                else
                {
                    try
                    {
                        categorie.Links = Tom.Toms(str);
                        link.Children.Add(categorie);
                        categorie = null;
                    }
                    catch (ArgumentException e)
                    {
                        WorkExtencion.Push("\nПоломался на строке:\n" + str + "\nВ файле: " + link.Name);
                    }
                }
            }
            else
            {
                if (categorie == null)
                {
                    categorie = new UnderPage();
                    JobString.go(str);
                }
                else
                {
                    JobString.go(str);
                }
            }
        }

        static public void Clear()
        {
            if (categorie != null)
            {
                    var cat = new UnderPage();
                    cat.Links = Tom.Toms(categorie.Name);
                    link.Children.Add(cat);
                    categorie = null;
                
            }
            if (isRubric)
            {
                if ((step + 1) != General.Rubric.Length)
                {
                    step++;
                    link = General.Rubric[step];
                }
                else if(WorkExtencion.isNull)
                {
                    throw new Exception(String.Join('\n', WorkExtencion.error));
                }
                else
                {
                    General.Save(path);
                }
            }else if (WorkExtencion.isNull)
            {
                throw new Exception(String.Join('\n', WorkExtencion.error));
            }
            else
            {
                link.Save(path);
            }
        }
        static void go(string str)
        {
            categorie.Links = Tom.Toms(str);
            link.Children.Add(categorie);
            categorie = null;
        }

        static public void Rubric(List<string> names)
        {
            isRubric = true;
            General = link;
            General.Rubric = new PredmetWork[names.Count];
            General.isRubric = true;
            for (int i = 0; i < names.Count; i++)
            {
                General.Rubric[i] = new PredmetWork(names[i]);
            }
            link = General.Rubric[step];
        }


    }
    class PredmetWork
    {
        public bool isRubric;
        public PredmetWork[] Rubric;
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Contains(".txt"))
                {
                    int index = value.LastIndexOf('.');
                    name = value.Substring(0, index);
                }
                else
                {
                    name = value;
                }
            }
        }

        public List<UnderPage> Children;
        public int LinkCount
        {
            get
            {
                int index = 0;
                foreach(var i in Children)
                {
                    index = index + i.LinkCount;
                }
                return index;
            }
        }
        public PredmetWork (string str)
        {
            Name = str;
            Children = new List<UnderPage>();
        }
        public override string ToString()
        {
            string str = Name+"\r\n";
            if (!isRubric)
            {
                foreach (var i in Children)
                {
                    str = str + i.ToString() + "\r\n";
                }
            }
            else
            {

                foreach (var i in Rubric)
                {
                    str =str + "[Rubric] "  + i.ToString() + "\r\n";
                }
            }

            return str;
        }
        public void Save(string path)
        {

            Char ch = new char();
            if (name[0] == '«')
            {
                ch = name[1];
            }
            else
            {
                ch = name[0];
            }
            var file = Path.Combine(path, "Вывод");
            file = Path.Combine(file, ch.ToString());
            JobVoid.Folder(file);
            file = Path.Combine(file, Name + ".txt");
            File.WriteAllText(file, this.ToString());
        }

        public int PULink
        {
            get
            {
                int i = 0;
                foreach(var item in Children)
                {
                    i = i + item.PULink;
                }
                return i;
            }
        }
    }
    
    
    
    
    class Program
    {
        static List<string> GoodJob = new List<string>();
        static void Step(string openFile, FileInfo file)
        {
            string pull = "";
            var arr = openFile.Split('—');
            for (int index = 1; index < arr.Length; index++)
            {
                char[] ch = { ' ' };
                var operStr = arr[index].Trim(ch);
                if (String.IsNullOrEmpty(operStr))
                {
                    continue;
                }
                if (JobVoid.isPU(operStr))
                {
                    pull = pull + operStr;
                    while (true)
                    {
                        int last = pull.Length - 2;
                        string lastChar = pull.Substring(last);
                        if (lastChar == "\r\n")
                        {
                            operStr = pull;
                            pull = "";
                            break;
                        }
                        if (index == arr.Length - 1)
                        {
                            operStr = pull;
                            pull = "";
                            break;
                        }
                        index++;
                        pull = pull + " " + arr[index];
                    }
                }
                try { JobString.Push(operStr); }
                catch (Exception e)
                {
                    GoodJob.Add(e.Message);
                    GoodJob.Add(file.Name);
                    GoodJob.Add("Не прошёл\n");
                    GoodJob.Add("===================");
                    JobString.categorie = null;
                    JobString.FailFile.Add(file.Name);

                }
            }
            try
            {
                JobString.Clear();
            }
            catch (Exception e)
            {
                GoodJob.Add(e.Message);
                GoodJob.Add(file.Name);
                GoodJob.Add("Не прошёл\n");
                GoodJob.Add("===================");
                JobString.FailFile.Add(file.Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var dir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "IN"));
            var dirOUT = Path.Combine(Environment.CurrentDirectory, "OUT");
            var log = new LogWork();
            while (true) {
               var key = Console.ReadLine();
                if (key == "q")
                {
                    break;
                }
                if (key == "t")
                {
                    log.lastNumber = 0;
                    var listPU = File.ReadAllLines(Path.Combine(dir.FullName, "List.txt"));
                    var arrPU = File.ReadAllLines(Path.Combine(dir.FullName, "PU.txt"));
                    log.lastWord = listPU[0];
                    int i = 0;
                    int wordNumber = 0;
                    int next = i;
                    int j = 0;
                    List<string> newFile = new List<string>();
                    List<string> pred = new List<string>();
                    List<string> words = new List<string>();
                    string nextWord = "";
                    string wordWork;
                    string str;
                    while (true)
                    {
                        if (i == arrPU.Length)
                        {
                            break;
                        }
                        if (wordNumber == listPU.Length - 1)
                        {
                            break;
                        }
                        wordWork = listPU[wordNumber];
                        str = arrPU[i];
                        if (str == "")
                        {
                            i++;
                            continue;
                        }
                        if (!str.Contains(wordWork))
                        {
                            log.breakWord = wordWork;
                            log.lastNumber = i;
                            break;
                        }
                        else
                        {

                            log.lastNumber = i;
                            log.lastWord = wordWork;
                            nextWord = listPU[wordNumber + 1];
                            while (true)
                            {
                                if (i == arrPU.Length)
                                {
                                    break;
                                }
                                str = arrPU[i];
                                if (str == "")
                                {
                                    break;
                                }
                                if (str.Contains(nextWord))
                                {
                                    if (!str.Contains("См. также"))
                                    {
                                        int index = str.IndexOf(nextWord);
                                        if (index == 0)
                                        {
                                            break;
                                        }
                                        else
                                        {

                                            pred.Add(str.Split(nextWord)[0]);
                                            arrPU[i] = str.Substring(index);
                                            Console.WriteLine("Было разделение");
                                            break;
                                        }

                                    }
                                }
                                pred.Add(str);
                                i++;
                            }
                            Console.WriteLine(log.lastWord + " Запушили");
                            newFile.AddRange(pred);
                            newFile.Add("");
                            pred.Clear();
                            wordNumber++;
                        }
                    }
                    log.Save();
                    File.WriteAllLines(Path.Combine(dirOUT, "text.txt"), newFile);
                }
                if (key == "w")
                {
                    List<WordFile> listing = new List<WordFile>();
                    for (int i = 0; i<dir.GetDirectories().Length; i++)
                    {
                        var listDir = Path.Combine(dir.GetDirectories()[i].FullName, "list.txt");
                        var textDir = Path.Combine(dir.GetDirectories()[i].FullName, "text.txt");
                        var openFile = File.ReadAllLines(textDir);
                        var listFile = File.ReadAllLines(listDir);
                        int indexStr = 0;
                        for (int j = 0; j < listFile.Length; j++)
                        {
                            List<string> oneFile = new List<string>();
                            if (openFile.Length == indexStr)
                            {
                                Console.WriteLine("Опять переполнение на " + i);
                                continue;
                            }
                            while (true)
                            {
                                if (indexStr == openFile.Length)
                                {
                                    break;
                                }
                                if (openFile[indexStr] == "")
                                {
                                    indexStr++;
                                    if(oneFile.Count != 0)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    oneFile.Add(openFile[indexStr]);
                                    indexStr++;
                                }
                            }
                            listing.Add(new WordFile(listFile[j], oneFile.ToArray()));
                            oneFile.Clear();                            
                        }
                        Console.WriteLine("Обработали папку: " + i);

                    }
                    for (int i = 0; i < listing.Count; i++)
                    {
                        Console.WriteLine("Записываем " + i + " из " + listing.Count);
                        if (!listing[i].Save(dirOUT))
                        {
                            Console.WriteLine("Ошибка с файлом. Порядковый в листе " + i);
                            Console.WriteLine("Предыдущий по имени: " + listing[i - 1].Name);
                        }
                    }
                    Console.WriteLine("ЗАкончили нахуй");
                }
                if (key == "i")
                {
                    var statistic = new Dictionary<int, int>();
                    var charing = new Dictionary<char, int>();
                    int[] chetNeChet = { 0, 0, 0 };
                    foreach(var folder in dir.GetDirectories())
                    {
                        foreach(var file in folder.GetFiles())
                        {
                            var filename = file.Name;
                            var open = File.ReadAllText(file.FullName);
                            int count = open.Length;                            
                            if (!statistic.ContainsKey(open.Length))
                            {
                                statistic[open.Length] = 1;
                            } else
                            {
                                statistic[open.Length]++;
                            }
                            var stack = new Stack<char>();
                            foreach (var ch in open)
                            {
                                if (ch == '«')
                                {
                                    stack.Push(ch);
                                }
                                if (ch == '»')
                                {
                                    if (stack.Contains('«'))
                                    {
                                        stack.Pop();
                                    } else
                                    {
                                        Console.WriteLine(filename);
                                    }
                                }
                            }
                            if (stack.Count != 0)
                            {
                                Console.WriteLine(filename);
                            }
                            /*foreach (var str in open)
                            {
                                if (str.IndexOf("см.") !=-1& str.IndexOf("см.") < str.LastIndexOf('—')&str[str.Length-1]!=';'&open.Length != 1)
                                {
                                    Console.WriteLine(filename);
                                    Console.WriteLine("Последний символ: " + str[str.Length - 1]);
                                    chetNeChet[2]++;

                                }
                                for (int i=0; i < str.Length; i++)
                                {
                                    if (str[i]== '—')
                                    {
                                        cherta++;
                                    }
                                    if (!charing.ContainsKey(str[i]))
                                    {
                                        charing[str[i]] = 1;
                                    } else
                                    {
                                        charing[str[i]]++;
                                    }
                                }
                            }*/                            
                        }
                    }
                    var logFile = new List<string>();
                    logFile.Add("Статистика по строкам:");
                    /*int ip = 0;
                    int[] keys = new int[statistic.Count];
                    foreach (var par in statistic)
                    {
                        keys[ip] = par.Key;
                        ip++;
                    }
                    logFile.Add("");
                    Array.Sort(keys);
                    foreach(var item in keys)
                    {
                        var str = item + ": " + statistic[item];
                        logFile.Add(str);
                    }
                    var logName = Path.Combine(dirOUT, "log.txt");
                    Console.WriteLine("Чётныйх: " + chetNeChet[0]);
                    Console.WriteLine("Не Чётных: "+chetNeChet[1]);
                    Console.WriteLine("С одной строкой: " + chetNeChet[2]);*/
                    //File.WriteAllLines(logName, logFile);
                }
                if (key == "x")
                {
                    List<PredmetWork> json = new List<PredmetWork>();
                    List<string> goodJob = new List<string>();
                    var ignoreList = File.ReadAllLines(Path.Combine(dir.FullName, "IgnoreList.txt"));
                    var rubric = File.ReadAllLines(Path.Combine(dir.FullName, "Rubric.txt"));
                    int indexFail = 0;
                    int allFile = 0;
                    int allAllFile = 0;
                    foreach (var folder in dir.GetDirectories())
                    {
                        foreach(var file in folder.GetFiles())
                        {
                            bool isCrash = false;
                            allAllFile++;
                            /*if(JobVoid.inArray(file.Name, rubric))
                            {
                                continue;
                            }*/
                            /*if (JobVoid.inArray(file.Name, ignoreList))
                            {
                                continue;
                            }*/

                            allFile++;
                            
                            var pred = new PredmetWork(file.Name);
                            JobString.Join(pred);
                            JobString.path = dirOUT;
                            var openFileInText = File.ReadAllText(file.FullName);
                            var openList = File.ReadAllLines(file.FullName);
                            JobString.OpenFile = openList;
                            if (openFileInText.Contains("Схема рубрики"))
                            {
                                List<string> RubricName = new List<string>();
                                RubricName.Add(openList[2]);
                                int numStr = 3;
                                while (true)
                                {
                                    if (openList[numStr] == RubricName[0])
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        RubricName.Add(openList[numStr]);
                                        numStr++;
                                    }
                                }
                                for (int i = 0; i < RubricName.Count; i++)
                                {
                                    var u = RubricName[i].Split('.', 2);
                                    RubricName[i] = u[1].Trim(' ');
                                }
                                string[] rubricStr = new string[RubricName.Count];
                                int j = 0;
                                for (int i = j; i < RubricName.Count; i++)
                                {
                                    List<string> content = new List<string>();
                                    int start = numStr;
                                    while (true)
                                    {
                                        if (i + 1 != RubricName.Count)
                                        {
                                            if (numStr == openList.Length)
                                            {
                                                Console.WriteLine(RubricName[i + 1]);
                                                isCrash = true;
                                                break;
                                            }
                                            if (openList[numStr].Contains(RubricName[i + 1]))
                                            {
                                                break;
                                            }
                                        }
                                        
                                        if(numStr == openList.Length)
                                        {
                                            break;
                                        }
                                        content.Add(openList[numStr]);
                                        numStr++;
                                    }
                                    if (isCrash)
                                    {
                                        break;
                                    }
                                    rubricStr[i] = String.Join("\r\n", content);
                                    j = i;
                                }
                                if (j != RubricName.Count-1)
                                {
                                    Console.WriteLine("Глянем");
                                }
                                if (isCrash)
                                {
                                    Console.WriteLine("Переполнили строку " + file.Name);
                                    continue;
                                }
                                JobString.Rubric(RubricName);
                                foreach(var i in rubricStr)
                                {
                                    Step(i, file);
                                }

                            }
                            else
                            {
                                Step(openFileInText, file);
                            }
                            
                        }
                    }
                    File.WriteAllLines(Path.Combine(dirOUT, "FailFile.txt"), JobString.FailFile);
                    List<PredmetWork> FailLinc = new List<PredmetWork>();
                    int count = 0;
                    foreach (var i in json)
                    {
                        if (i.LinkCount == 0)
                        {
                            FailLinc.Add(i);
                        }
                        count = count + i.PULink;
                        i.Save(dirOUT);
                    }
                    Console.WriteLine("Окончили");
                    Console.WriteLine(indexFail);
                    Console.WriteLine(allFile - indexFail);
                    Console.WriteLine(allAllFile);
                    File.WriteAllLines(Path.Combine(dirOUT, "РАБОТАЙ НЕГР.txt"), GoodJob);
                }
                if (key == "e")
                {
                    var GoodListPU = new List<string>();
                    var BadPageList = new List<string>();
                    var BadCategoriList = new List<string>();
                    var BadPUList = new List<string>();
                    var WTFList = new List<string>();
                    foreach(var folder in dir.GetDirectories())
                    {
                        foreach(var file in folder.GetFiles())
                        {
                            var open = File.ReadAllLines(file.FullName);
                            open = JobVoid.FirstDefed(open);
                            foreach (var lines in open)
                            {
                                string str = lines.Trim(' ');
                                if (String.IsNullOrEmpty(str))
                                {
                                    continue;
                                }
                                if (str.Contains("[Rubric]"))
                                {
                                    continue;
                                }
                                if (str.Contains("[Categori]")) {
                                   str =  str.Replace("[Categori]", "");
                                    str = str.Trim(' ');
                                    str = str.ToLower();
                                    if (String.IsNullOrEmpty(str))
                                    {
                                        continue;
                                    }
                                    string sup = "";
                                    foreach (var i in str)
                                    {
                                        if (!JobVoid.inArray(i, JobVoid.AlfaBeta))
                                        {
                                            sup = sup + i;
                                        }
                                    }
                                    if (String.IsNullOrEmpty(sup))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        BadCategoriList.Add(file.Name);
                                        BadCategoriList.Add(sup);
                                        BadCategoriList.Add("");
                                    }
                                    continue;
                                }
                                if (str.Contains("[PU]")) {
                                    str = str.Replace("[PU]", "");
                                    str = str.Trim(' ');                                    
                                    if ((str.IndexOf("См.") > 0) ^(str.IndexOf("Cм.") > 0)^(str.IndexOf("см.") > 0))
                                    {
                                        BadPUList.Add(file.Name);
                                        BadPUList.Add(str);
                                        BadPUList.Add("");
                                        continue;
                                    }
                                    GoodListPU.Add(str);
                                    continue;
                                }
                                if (str.Contains("[Link]")) {
                                    char[] ch = { ' ', ';' };
                                    str = str.Replace("[Link]", "");
                                    str = str.Trim(ch);
                                    if (String.IsNullOrEmpty(str))
                                    {
                                        continue;
                                    }
                                    var array = str.Split(';');
                                    foreach(var tom in array)
                                    {
                                        var sub = tom.Split(':')[0];
                                        sub = sub.Trim(' ');
                                        if (!JobVoid.inArray(sub, Tom.NumberTom))
                                        {
                                            BadPageList.Add(file.Name);
                                            BadPageList.Add(sub);
                                            BadPageList.Add("");
                                        }
                                        continue;
                                    }
                                    continue;
                                }
                                WTFList.Add(file.Name);
                                WTFList.Add(str);
                                WTFList.Add("");
                            }
                            

                            
                        }
                    }
                    var filename = Path.Combine(dirOUT, "GoodListPU.txt");
                            File.WriteAllLines(filename, GoodListPU);
                            filename = Path.Combine(dirOUT, "BadListPU.txt");
                            File.WriteAllLines(filename, BadPUList);
                            filename = Path.Combine(dirOUT, "BadPageList.txt");
                            File.WriteAllLines(filename, BadPageList);
                            filename = Path.Combine(dirOUT, "BadCategoryList.txt");
                            File.WriteAllLines(filename, BadCategoriList);
                            filename = Path.Combine(dirOUT, "WTFList.txt");
                    File.WriteAllLines(filename, WTFList);
                    Console.WriteLine("Закончили");
                }
            }



        }
    }
}
