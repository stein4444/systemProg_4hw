using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace systemProg_hw4
{
    public class SortingItem : INotifyPropertyChanged
    {
        public int words { get; set; }
        public int lines { get; set; }
        public int punctuation { get; set; }

        public int Words
        {
            get => words;
            set
            {
                words = value;
                OnPropertyChanged();
            }
        }
        public int Lines
        {
            get => lines;
            set
            {
                lines = value;
                OnPropertyChanged();
            }
        }
        public int Punctuation
        {
            get => punctuation;
            set
            {
                punctuation = value;
                OnPropertyChanged();
            }
        }

        public void Sorting(object obj)
        {
            string el = (string)obj;
            string line;
            int count = 0;
            using (StreamReader reader = File.OpenText(el))
            {
                lock (this)
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        count++;
                        Lines = count;
                    }
                    CountingWords(reader, el);
                    CountingSymbols(reader, el);
                }

            }

        }
        public void CountingWords(StreamReader st, string el)
        {
            int count = 0;
            using (st = File.OpenText(el))
            {

                string line;
                
                    while ((line = st.ReadLine()) != null)
                    {
                    lock (this)
                    {
                        String[] words = line.Split('.', '?', '!', ' ', ';', ':', ',', '+', '–', '…', '"', '\'', '«', '»', '(', ')', '{', '}', '[', ']', '<', '>', '/');

                        count = count + words.Length;
                        Words = count;
                    }
                    }
            }
        }
        public void CountingSymbols(StreamReader st, string el)
        {
            int count = 0;
            var text = new char[] { '.', '?', '!', ' ', ';', ':', ',', '+', '–', '…','"', '\'', '«','»','(', ')', '{', '}', '[', ']',  '<', '>',  '/' };
            using (st = File.OpenText(el))
            {

                string line;

                while ((line = st.ReadLine()) != null)
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (line.Contains(text[i]))

                            lock (this)
                            {
                                count++;
                                Punctuation = count;
                            }

                    }
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
