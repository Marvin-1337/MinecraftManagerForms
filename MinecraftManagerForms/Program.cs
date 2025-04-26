using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MinecraftManagerForms
{

    

    
        static class Program
        {
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DatabaseManager db = new DatabaseManager();
                JugadorService jugadorService = new JugadorService(db);
                BloqueService bloqueService = new BloqueService(db);
                InventarioService inventarioService = new InventarioService(db, jugadorService, bloqueService);

                Application.Run(new MainForm(jugadorService, bloqueService, inventarioService));
            }
        }
    



}
