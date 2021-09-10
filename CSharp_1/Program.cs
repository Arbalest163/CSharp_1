using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_1
{
    
    public delegate void EventComlete();
    public delegate void EventCorrect();
    public delegate void EventCountSpace(int val);
    public delegate void EventLuckyTicket(bool val);

    public static class Massage
    {
        public static void PrintMassageComlete()
        {
            Console.WriteLine("Задание выполнено!");
            Console.WriteLine("====================================================\n");
        }
        public static void PrintSpaceCount(int val)
        {
            Console.WriteLine($"Количетво введённных пробелов: {val} ;");
        }
        public static void PrintCorrect()
        {
            Console.WriteLine("Некорректный ввод! Повторите.");
        }
        public static void PrintLuckyTicket(bool value)
        {
            if (value)
                Console.WriteLine("Поздравляем! Билет является счастливым!");
            else Console.WriteLine("К сожалению, билет не счасливый!");
        }

    }

    public class CountSpace
    {
        private event EventComlete _complete;
        private event EventCountSpace _countSpaceMassage;
        private int _countSpace;
        public CountSpace()
        {
            _countSpaceMassage += Massage.PrintSpaceCount;
            _complete += Massage.PrintMassageComlete;
        }
        ~CountSpace()
        {
            _complete -= Massage.PrintMassageComlete;
            _countSpaceMassage -= Massage.PrintSpaceCount;
        }
        public void Run()
        {
            Console.WriteLine("Программа для подсчёта пробелов до первого символа '.'");
            Console.WriteLine("Введите предложение: ");
            Enter();
            Print();
        }
        private void Enter()
        {
            string tmp = "";
            do
            {
                tmp += Console.ReadLine();
            }
            while (!checkEnter(tmp));
           
        }
        private void Print()
        {
            _countSpaceMassage?.Invoke(_countSpace);
            _complete?.Invoke();

        }
        private bool checkEnter(string str)
        {
            _countSpace = 0;
            foreach (char s in str)
            {
                if (s == ' ') _countSpace++;
                if (s == '.') return true;
            }
            return false;
        }
    }

    public class LuckyTicket
    {
        private event EventComlete _complete;
        private event EventCorrect _notCorrect;
        private event EventLuckyTicket _luckyTicket;
        private int[] _luckyNum;
        public LuckyTicket()
        {
            _luckyNum = new int[6];
            _notCorrect += Massage.PrintCorrect;
            _luckyTicket += Massage.PrintLuckyTicket;
            _complete += Massage.PrintMassageComlete;
        }
        ~LuckyTicket()
        {
            
            _notCorrect -= Massage.PrintCorrect;
            _luckyTicket -= Massage.PrintLuckyTicket;
            _complete -= Massage.PrintMassageComlete;
        }

        public void Run()
        {
            Console.WriteLine("Программа для определения счастливого билета.");
            Console.WriteLine("Введите номер билета(6 цифр): ");
            Enter();
            _luckyTicket?.Invoke(isLuckyTicket());
            _complete?.Invoke();
        }
        private void Enter()
        {
            string tmp = "";
            do
            {
                tmp = Console.ReadLine();
            }
            while (!checkEnter(tmp));

        }
        private bool checkEnter(string val)
        {
            if (val.Length != 6)
            {
                _notCorrect?.Invoke();
                return false;
            }
            for (int i = 0; i < 6; i++)
            {
                _luckyNum[i] = Convert.ToInt32(val[i]) - 48;
                if (_luckyNum[i] < 0 || _luckyNum[i] > 10)
                {
                    return false;
                }
            }

            return true;
        }

        private bool isLuckyTicket()
        {
            int resLeft =0, resRight = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i < 3) resLeft += _luckyNum[i];
                else resRight += _luckyNum[i];
            }
            return resLeft == resRight;
        }

    }

    public class FirTree
    {
        private event EventComlete _complete;
        private event EventCorrect _notCorrect;

        int a, b;
        public FirTree()
        {
            _notCorrect += Massage.PrintCorrect;
            _complete += Massage.PrintMassageComlete;
        }
        ~FirTree()
        {
            _notCorrect -= Massage.PrintCorrect;
            _complete -= Massage.PrintMassageComlete;
        }
        public void Run()
        {
            Console.WriteLine("Программа для построения ёлочки.");
            Console.WriteLine("Введите первую цифру(1-9): ");
            Enter(ref a);
            Console.WriteLine("Введите вторую цифру(1-9): ");
            Enter(ref b);
            if (a < b) Print(a, b);
            else Print(b,a);
            _complete?.Invoke();

        }
        private void Enter(ref int item)
        {
            string tmp = "";
            do
            {
                tmp = Console.ReadLine();
            }
            while (!checkEnter(tmp, ref item));

        }
        private bool checkEnter(string str, ref int item)
        {
            if (str.Length != 1 || (Convert.ToInt32(str[0]) - 48) < 0 || (Convert.ToInt32(str[0]) - 48) > 9)
            {
                _notCorrect?.Invoke();
                return false;
            }
            item = Convert.ToInt32(str[0]) - 48;
            return true;
        }
        private void Print(int x, int y)
        {
            for (int i = x; i <= y; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }
        }
    
    }

    public class NumReverse
    {
        private event EventComlete _complete;
        private event EventCorrect _notCorrect;
        private int[] _numArray;
       
        public NumReverse()
        {
            _notCorrect += Massage.PrintCorrect;
            _complete += Massage.PrintMassageComlete;
        }
        ~NumReverse()
        {
            _notCorrect -= Massage.PrintCorrect;
            _complete -= Massage.PrintMassageComlete;
        }

        public void Run()
        {
            Console.WriteLine("Программа для отражения цифр.");
            Console.WriteLine("Введите любые положительные цифры: ");
            Enter();
            Print();
            _complete?.Invoke();
        }
        private void Enter()
        {
            string tmp = "";
            do
            {
                tmp = Console.ReadLine();
            }
            while (!checkEnter(tmp));

        }
        private bool checkEnter(string str)
        {
            if (str == "")
            {
                _notCorrect?.Invoke();
                return false;
            }
            _numArray = new int[str.Length];
            for(int i = 0, j = str.Length - 1; i < str.Length; i++, j--)
            {
                _numArray[j] = Convert.ToInt32(str[i]) - 48;
                if (_numArray[j] < 0 || _numArray[j] > 9)
                {
                    _notCorrect?.Invoke();
                    return false;
                }
            }
            return true;
        }
        
        private void Print()
        {
            Console.WriteLine("Отраженное число: ");
            foreach (int t in _numArray)
            {
                Console.Write(t);
            }
            Console.WriteLine();
        }

    }

    public class UpDownCase
    {
        private event EventComlete _complete;
        private event EventCorrect _notCorrect;
        private string _string = "";

        public UpDownCase()
        {
            _notCorrect += Massage.PrintCorrect;
            _complete += Massage.PrintMassageComlete;
        }
        ~UpDownCase()
        {
            _notCorrect -= Massage.PrintCorrect;
            _complete -= Massage.PrintMassageComlete;
        }

        public void Run()
        {
            Console.WriteLine("Программа для изменения регистра букв.");
            Console.WriteLine("Введите любой текст: ");
            Enter();
            Print();
            _complete?.Invoke();
        }
        private void Enter()
        {
            string tmp = "";
            do
            {
                tmp = Console.ReadLine();
            }
            while (!checkEnter(tmp));

        }
        private bool checkEnter(string str)
        {
            if (str == "")
            {
                _notCorrect?.Invoke();
                return false;
            }
            
            for(int i = 0; i < str.Length; i++)
            {
                int tmpInt = Convert.ToInt32(str[i]);
               
                if (tmpInt == 1025)
                {
                    _string += (char)(tmpInt + 80);
                }
                else if (tmpInt == 1105)
                {
                    _string += (char)(tmpInt - 80);
                }
                else if (tmpInt > 64 && tmpInt < 97 || tmpInt > 1039 && tmpInt < 1072)
                {
                    _string += (char)(tmpInt + 32);
                }
                else if (tmpInt > 96 && tmpInt < 123 || tmpInt > 1071 && tmpInt < 1104)
                {
                    _string += (char)(tmpInt - 32);
                }
                else
                {
                    _string += (char)tmpInt;
                }
            }

           
            return true;
        }

        private void Print()
        {
            Console.WriteLine("Изменённый текст: ");
            foreach (char t in _string)
            {
                Console.Write(t);
            }
            Console.WriteLine();
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            CountSpace csp = new CountSpace();
            csp.Run();
            LuckyTicket ticket = new LuckyTicket();
            ticket.Run();
            FirTree fil = new FirTree();
            fil.Run();
            NumReverse nrev = new NumReverse();
            nrev.Run();
            UpDownCase udc = new UpDownCase(); ;
            udc.Run();
           
        }
    }

}
