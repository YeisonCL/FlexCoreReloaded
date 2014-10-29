using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlexCore.persons;

namespace FlexCore
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new NameEdit(23, NameEdit.JURIDICAL, "Pedro", "Mata", "Mora"));
            Application.Run(new MainWindow());
        }
    }
}
