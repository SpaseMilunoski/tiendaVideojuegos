﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tiendaVideojuegos
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
<<<<<<< HEAD
            Login login = new Login();
            login.Show();
            Inicio inicio = new Inicio();
=======
            AdminProduct inicio = new AdminProduct();
>>>>>>> d6b4f1f842672bddb6e147cb2e1d64e6e73f043b
            inicio.FormClosed +=MainForm_Closed;
            inicio.Show();
            Application.Run();
            
        }
        private static void MainForm_Closed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= MainForm_Closed;

            if (Application.OpenForms.Count == 0)
            {
                Application.ExitThread();
            }
            else
            {
                Application.OpenForms[0].FormClosed += MainForm_Closed;
            }
        }
    }
}
