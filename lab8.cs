using System;
using System.Runtime.InteropServices;
interface IBildirimServisi
{
    void Gonder(string mesaj);
}
interface ILoglanabilir
{
    void LogYaz(string mesaj);
}

class EmailServisi : IBildirimServisi, ILoglanabilir
{
    public void Gonder(string mesaj)
    {
        Console.WriteLine("Email gönderildi: + mesaj");
        LogYaz(mesaj);
    }

    public void LogYaz(string mesaj)
    {
        Console.WriteLine("Email logu: + mesaj");
    }
}