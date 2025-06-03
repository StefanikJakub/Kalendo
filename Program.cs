using System;
using System.Windows.Forms;

namespace kalendar
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Prihlasenie()); // Spustí prihlasovací formulár
        }
    }
}
