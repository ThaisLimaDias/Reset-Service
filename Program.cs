using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reset_Service
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            string nomeProcesso = Process.GetCurrentProcess().ProcessName;

            //Busca os processos com este nome que estão em execução
            Process[] processos = Process.GetProcessesByName(nomeProcesso);

            //Se já houver um aberto
            if (processos.Length > 1)
            {
                //Mata todos os processos diferentes do atual
                foreach (var item in processos.Where(p => p.Id != Process.GetCurrentProcess().Id))
                {
                    item.Kill();
                }
            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
}
