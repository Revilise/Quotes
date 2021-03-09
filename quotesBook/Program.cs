using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace qoutesBook
{
    // Класс, предназначенный для хранения данных о каждой цитате 
    public class Qcontrol
    {
        public int views = 0; // просмотры цитаты, по умолчанию = 0
        public string stroke { get; set; } // текст цитаты
        public int position { get; set; } // позиция каждой цитаты в списке
    }
    class Program
    {
        static void Main(string[] args)
        {
            // лист, элменты которого - объекты класса Qcontrol. 
            List<Qcontrol> quoteList = new List<Qcontrol>();

            int index;
            string act; // переменная для команд пользователя
            int pos;
            string[] path = { @"quotes.txt", @"views.txt" };
            string buffer;
            // инфо для использования
            Console.WriteLine("\n добавить цитату напишите 'добавить', " +
                "\n посмотреть существующие - 'посмотреть'. " +
                "\n покинуть просмотр, напишите 'выйти'.\n");

            try
            {
                StreamReader sr = new StreamReader(path[0]);
                int j = quoteList.Count;
                while ((buffer = sr.ReadLine()) != null)
                {
                    quoteList.Add(new Qcontrol());
                    quoteList[j].stroke = buffer;
                    quoteList[j].position = j;
                    j++;
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // главная функция, которая дает возможность использовать команды добавить, посмотреть и выйти из просмотра
            void addAndWatch()
            {
                pos = quoteList.Count;
                act = Console.ReadLine();
                if (act == "добавить" || act == "добавить ")
                {
                    quoteList.Add(new Qcontrol());                      // добавить в лист новую цитату
                    Console.WriteLine("Новая цитата " + pos + ": ");    // вывести приглашение для написания цитаты с обозначением её будущей позиции
                    quoteList[pos].position = pos;                      // установка позиции для цитаты с помощью счетчика позиций pos
                    quoteList[pos].stroke = Console.ReadLine();                          // написание цитаты
                    Console.WriteLine("\n");

                    StreamWriter swr = new StreamWriter(path[0], true);
                    swr.WriteLine(quoteList[pos].views + quoteList[pos].stroke);
                    swr.Close();

                    pos++;                                              // прирощение счетчика позиции для новых цитат
                    addAndWatch();                                      // вернуться к возможности добавлять и просматривать цитаты

                }
                else if (act == "посмотреть" || act == "посмотреть ")
                {
                    // если лист пуст, выдать сообщение и вернуться к возможности добавлять и просматривать цитаты
                    // // // если в списке есть хотя бы одна запись, то это условие просто не выполнится и код пойдет дальше.
                    if (quoteList.Count == 0) { Console.WriteLine("Список цитат ещё пуст :(\n"); addAndWatch(); }

                    // вывести весь список
                    foreach (Qcontrol ele in quoteList)
                    {
                        Console.WriteLine(ele.position + ". " + ele.stroke.Substring(0, 10) + "...");
                    }

                    // функция просмотра цитат
                    void req()
                    {
                        Console.Write("\nДля предпросмотра цитаты введите её номер: ");
                        act = Console.ReadLine();

                        // вернуться к возможности добавлять и просматривать цитаты
                        if (act == "выход" || act == "выйти") { addAndWatch(); }
                        else
                        {
                            index = int.Parse(act); // попытка захвата целого числа из полученной строки

                            if (quoteList.Count() - 1 >= index)                 // если список длиннее или равен полученному числу
                            {
                                Console.WriteLine(quoteList[index].stroke); // вывод строки по полученному индексу
                                quoteList[index].views++;                   // увеличение числа просмотров и из вывод в следующей строке
                                Console.WriteLine("Кол-во просмотров: " + quoteList[index].views);

                                StreamWriter swr = new StreamWriter(path[1], false);

                                swr.
                            }
                            req();
                        }
                    }
                    req();
                }
                addAndWatch();

            }
            addAndWatch();

            Console.ReadLine();
        }
    }
}
