using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Logic
{
    void SumFolder()
    {
        string[] files = Directory.GetFiles(@"C:", "*.*", SearchOption.AllDirectories); //вводим нам нужную папку
        int count = 0;
        double sum = 0;
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo fi = new FileInfo(files[i]);
            count++;
            sum += fi.Length;
            Console.WriteLine(fi);
            Console.WriteLine(fi.Length + " байт");
            Console.WriteLine("_________________________________");
        }
        double kiloByte;
        kiloByte = sum / (1024 * 1024);
        var Text = kiloByte.ToString("0.000") + " Мбайтов";
        Console.WriteLine(Text);
        Console.WriteLine(count);
    }
}
