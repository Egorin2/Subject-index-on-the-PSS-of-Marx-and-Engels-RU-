using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPU
{
    class StringStrong
    {
        public string str;
        public bool isLink
        {
            get
            {
                if (str.Contains(';'))
                {
                    return true;
                }
                if (str.Contains("см."))
                {
                    return true;
                }
                if (str.Contains("См."))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool isSM
        {
            get
            {
                if (str.Contains("см.") ^ str.Contains("См."))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public StringStrong()
        {
            str = "";
        }
        public void Push(char ch)
        {
            str = str + ch;
        }
        public void Push(StringStrong strong)
        {
            str = str + " — " + strong.str;
        }
        public override string ToString()
        {
            return str;
        }
    }
    class objectWork
    {
        private StringStrong Last;
        private string[] file;
        public string[] File
        {
            set
            {
                file = value;
            }
            get
            {
                return file;
            }
        }
        public int index;
        public bool IsBad;
        private bool isLink;
        public List<StringStrong> arr;
        public StringStrong work;
        public bool isNew;
        public string LastSplit
        {
            get
            {
                var content = String.Join("\r\n", file);
                int indus = content.LastIndexOf('—');
                content = content.Substring(indus + 1);
                return content;
            }
        }
        public string LastStr
        {
            get
            {
                var content = String.Join("\r\n", file);
                int indus = content.LastIndexOf("См.");
                if (indus == -1)
                {
                    return indus.ToString();
                }
                content = content.Substring(indus);
                return content;
            }
        }


        public objectWork()
        {
            Last = new StringStrong();
            index = 0;
            IsBad = false;
            arr = new List<StringStrong>();
            work = new StringStrong();
            isNew = true;
        }
        public void Push(char ch)
        {
            work.Push(ch);
        }
        public void App()
        {
            if (isNew)
            {
                isLink = work.isLink;
                arr.Add(work);
                Last = work;
                work = new StringStrong();
                isNew = false;
            }
            else
            {
                if (isLink != work.isLink)
                {
                    isLink = work.isLink;
                    arr.Add(work);
                    Last = work;
                    work = new StringStrong();
                }
                else if (!isLink)
                {
                    int ind;
                    if (file.Length <= index)
                    {
                        ind = index - 1;
                    }
                    else
                    {
                        ind = index;
                    }
                    if (file[ind].Contains("См."))
                    {
                        arr.Add(work);
                        Last = work;
                        work = new StringStrong();
                    }
                    else
                    {

                        if (LastSplit == work.str)
                        {
                            arr.Add(work);
                            Last = work;
                            work = new StringStrong();
                        }
                        else if (work.str.Contains(LastSplit))
                        {
                            arr.Add(work);
                            Last = work;
                            work = new StringStrong();
                        }
                        else if (LastStr.Contains(work.str))
                        {
                            arr.Add(work);
                            Last = work;
                            work = new StringStrong();
                        }
                        else
                        {
                            IsBad = true;
                            arr.Add(work);
                            Last = work;
                            work = new StringStrong();
                        }
                    }
                }
                else if (Last.isSM)
                {
                    isLink = true;
                    Last.Push(work);
                    work = new StringStrong();
                }
                else
                {
                    IsBad = true;
                    arr.Add(work);
                    Last = work;
                    work = new StringStrong();
                }
            }





        }
    }

}
