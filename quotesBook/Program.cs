using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace qoutesBook
{
    class Program
    {
        class QuoteControl
        {
            private string stroke;
            private int views = 0;
            private int index;

            public void SetFull(string str, int vws, int indx)
            {
                stroke = str;
                views = vws;
                index = indx;
            }
            public void ShortGet()
            {
                Console.WriteLine(index + ".) " + stroke.Substring(0, 10) + "...");
            }
            public void FullGet()
            {
                views++;
                Console.WriteLine(index + ".) " + stroke + "\nПросмотров: " + views);
            }
            public string save()
            {
                return stroke + "   " + views;
            }
        }
        static void Main(string[] args)
        {
            List<QuoteControl> Quotes = new List<QuoteControl>();

            string path = @"quotes.txt";
            string buffer;
            string act;
            int index;

            Boolean exist = false;
            Boolean fun = false;

            try // проверяю файл на существование и читаю его
            {
                StreamReader sr = new StreamReader(path);
                while ((buffer = sr.ReadLine()) != null)
                {
                    Quotes.Add(new QuoteControl());
                    Quotes[Quotes.Count - 1].SetFull(
                        // считывать значение строки до последних трех символов
                        buffer.Substring(0, buffer.Length - 3),
                        // перевести последние три символа в число
                        int.Parse(buffer.Substring(buffer.Length - 3)),
                        // позиция цитаты
                        Quotes.Count - 1
                        );
                }
                sr.Close();
                exist = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            void checking() // фрагмент выхода
            {
                act = Console.ReadLine();
                if (act == "выйти" || act == "выход")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n добавить цитату напишите 'добавить', " +
                    "\n посмотреть существующие - 'посмотреть'. " +
                    "\n Cохранить изменения - 'сохранить'\n");
                    Console.ResetColor();
                    addAndWatch();
                }
                if (act == "сохранить")
                {
                    saveF(fun = true);
                }
                try
                {
                    index = int.Parse(act);
                    if (Quotes.Count - 1 >= index)
                    {
                        Quotes[index].FullGet();
                    }
                }
                catch { }
                checking();
            }

            void saveF(Boolean fun) // сохранене файла
            {
                StreamWriter swr = new StreamWriter(path);
                for (int i = 0; i < Quotes.Count; i++)
                {
                    swr.WriteLine(Quotes[i].save());
                }
                swr.Close();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nИзменения сохранены\n");
                Console.ResetColor();

                if (fun == false) { addAndWatch(); } else { checking(); }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n добавить цитату напишите 'добавить', " +
            "\n посмотреть существующие - 'посмотреть'. " +
            "\n Cохранить изменения - 'сохранить'\n");
            Console.ResetColor();

            void addAndWatch()
            {
                act = Console.ReadLine();

                if (act == "добавить")
                {
                    Quotes.Add(new QuoteControl());
                    Console.WriteLine("Новая цитата " + (Quotes.Count - 1) + ":");
                    buffer = Console.ReadLine();
                    Quotes[Quotes.Count - 1].SetFull(buffer, 0, Quotes.Count - 1);
                    exist = true;
                    Console.WriteLine();
                    addAndWatch();
                }
                if (act == "посмотреть")
                {
                    if (exist == false)
                    {
                        Console.WriteLine("Список ещё пуст :<\n");
                        addAndWatch();
                    }
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\nРежим просмотра");
                    Console.ResetColor();
                    Console.WriteLine(" Чтобы посмотреть цитату введите число от 0 до " +
                        (Quotes.Count - 1) +
                        "\n Для выхода напишите \"выйти\"\n");

                    for (int i = 0; i < Quotes.Count; i++)
                    {
                        Quotes[i].ShortGet();
                    }
                    Console.WriteLine();

                    checking();
                }
                if (act == "сохранить")
                {
                    saveF(fun = false);
                }
                addAndWatch();
            }
            addAndWatch();
            Console.ReadLine();
        }
    }
}