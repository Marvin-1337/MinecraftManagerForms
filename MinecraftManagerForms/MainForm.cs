using System.Windows.Forms;

namespace MinecraftManagerForms
{
    internal class MainForm : Form
    {
        private JugadorService jugadorService;
        private BloqueService bloqueService;
        private InventarioService inventarioService;

        public MainForm(JugadorService jugadorService, BloqueService bloqueService, InventarioService inventarioService)
        {
            this.jugadorService = jugadorService;
            this.bloqueService = bloqueService;
            this.inventarioService = inventarioService;
        }
    }
}