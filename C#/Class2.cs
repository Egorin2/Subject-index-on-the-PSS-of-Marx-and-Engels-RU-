using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPU
{
    class Link
    {
        public bool isPU;
        private string link;
        public string LinkStr
        {
            get { return link; }
            set
            {
                link = value;
            }

        }
        public Link(string str)
        {
            if (Link.isPUx(str))
            {
                isPU = true;
                link = str;
            }
            else
            {
                isPU = false;
                link = str;
            }
        }
        static public bool isPUx(string str)
        {
            if (str.Contains("См."))
            {
                return true;
            }
            else if (str.Contains("см."))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool isLink(string str)
        {
            if (Link.isPUx(str))
            {
                return true;
            }
            else if (str.Contains(";"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return link;
        }
    }
    class UnderForm
    {
        private string name = "";
        public Link[] links;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Link.isLink(value))
                {
                    Console.WriteLine("А хули ссылка?");
                    Console.WriteLine(name);
                }
                else if (value.Contains('—'))
                {
                    Console.WriteLine("Чё за нах?");
                }
                else
                {
                    name = value;
                }
            }
        }
        public int LinkCount
        {
            get
            {
                return links.Length;
            }
        }

        public void ToLink(string str)
        {
            var arr = str.Split(';');
            Link[] li = new Link[arr.Length];
            int i = 0;
            foreach (var item in arr)
            {
                li[i] = new Link(item);
                i++;
            }
            links = li;
        }
        public UnderForm(string str)
        {
            if (Link.isLink(str))
            {
                name = "";
                ToLink(str);
            }
            else
            {
                name = str;
            }
        }
        public UnderForm(string toName, string toLink)
        {
            Name = toName;
            ToLink(toLink);
        }
        public string[] ListLink()
        {
            string[] arr = new string[LinkCount];
            int i = 0;
            foreach(var item in links)
            {
                arr[i] = item.LinkStr;
                i++;
            }
            return arr;
        }

        public override string ToString()
        {
            string str = name + ": ";
            string val = "";
            foreach (var i in links)
            {
                val = val + i.ToString() + "; ";
            }
            str = str + val;

            return str;
        }


    }
    class FormFile
    {
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
        public List<UnderForm> children = new List<UnderForm>();

        public int LinkCount
        {
            get
            {
                int i = 0;
                foreach(var item in children)
                {
                    i = i + item.LinkCount;
                }
                return i;
            }
        }
        public string[] ListLink()
        {
            string[] arr = new string[LinkCount];
            int i = 0;
            foreach(var item in children)
            {
                var x = item.ListLink();
                foreach (var d in x)
                {
                    arr[i] = d;
                    i++;
                }
            }
            return arr;
        }
    }
    
}
