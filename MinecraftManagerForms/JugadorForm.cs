using System;
using System.Windows.Forms;

namespace MinecraftManager
{
    public partial class JugadorForm : Form
    {
        private readonly JugadorService _jugadorService;

        public JugadorForm(JugadorService jugadorService)
        {
            _jugadorService = jugadorService;
            InitializeComponent();
            CargarJugadores();


        }

        private void CargarJugadores()
        {
            listBoxJugadores.DataSource = null;
            listBoxJugadores.DataSource = _jugadorService.ObtenerTodos();
            listBoxJugadores.DisplayMember = "Nombre";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            if (!string.IsNullOrEmpty(nombre))
            {
                _jugadorService.Agregar(nombre);
                CargarJugadores();
                txtNombre.Clear();
            }
            else
            {
                MessageBox.Show("Por favor ingrese un nombre válido.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var jugadorSeleccionado = listBoxJugadores.SelectedItem as Jugador;
            if (jugadorSeleccionado != null)
            {
                _jugadorService.Eliminar(jugadorSeleccionado.Id);
                CargarJugadores();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un jugador para eliminar.");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var jugadorSeleccionado = listBoxJugadores.SelectedItem as Jugador;
            if (jugadorSeleccionado != null)
            {
                string nuevoNombre = txtNombre.Text;
                if (!string.IsNullOrEmpty(nuevoNombre))
                {
                    jugadorSeleccionado.Nombre = nuevoNombre;
                    CargarJugadores();
                    txtNombre.Clear();
                }
                else
                {
                    MessageBox.Show("Por favor ingrese un nuevo nombre válido.");
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un jugador para modificar.");
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // JugadorForm
            // 
            this.ClientSize = new System.Drawing.Size(349, 261);
            this.Name = "JugadorForm";
            this.ResumeLayout(false);

        }
    }
}