using MinecraftManagerForms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MinecraftManager
{
    public partial class MainForm : Form
    {
        private readonly JugadorService _jugadorService;
        private readonly BloqueService _bloqueService;
        private readonly InventarioService _inventarioService;

        private DataGridView dataGrid;
        private ComboBox comboRareza;
        private PictureBox pictureBloque;
        private Chart chartStats;

        public MainForm(JugadorService jugadorService, BloqueService bloqueService, InventarioService inventarioService)
        {
            _jugadorService = jugadorService;
            _bloqueService = bloqueService;
            _inventarioService = inventarioService;

            InitializeComponent();
            CargarInterfaz();
            MostrarInventario();
            MostrarEstadisticas();
        }

        private void CargarInterfaz()
        {
            this.Text = "Minecraft Manager";
            this.Size = new Size(900, 600);

            // DataGrid
            dataGrid = new DataGridView
            {
                Location = new Point(20, 50),
                Size = new Size(400, 200),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dataGrid);

            // ComboBox
            comboRareza = new ComboBox
            {
                Location = new Point(20, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboRareza.Items.AddRange(new[] { "Todos", "Común", "Épico" });
            comboRareza.SelectedIndexChanged += ComboRareza_SelectedIndexChanged;
            comboRareza.SelectedIndex = 0;
            this.Controls.Add(comboRareza);

            // PictureBox
            pictureBloque = new PictureBox
            {
                Location = new Point(440, 50),
                Size = new Size(128, 128),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(pictureBloque);

            // Chart
            chartStats = new Chart
            {
                Location = new Point(20, 270),
                Size = new Size(500, 250)
            };
            chartStats.ChartAreas.Add(new ChartArea("MainArea"));
            this.Controls.Add(chartStats);

            // Botón para gestionar jugadores
            Button btnJugadores = new Button
            {
                Text = "Gestionar Jugadores",
                Location = new Point(600, 50),
                Size = new Size(200, 40)
            };
            btnJugadores.Click += (s, e) => new JugadorForm(_jugadorService).ShowDialog();
            this.Controls.Add(btnJugadores);
        }

        private void MostrarInventario(string rareza = "Todos")
        {
            var inventario = _inventarioService.ObtenerTodo();
            if (rareza != "Todos")
            {
                var bloquesFiltrados = _bloqueService.ObtenerPorRareza(rareza).Select(b => b.Nombre);
                inventario = inventario.Where(i => bloquesFiltrados.Contains(i.Bloque)).ToList();
            }

            dataGrid.DataSource = null;
            dataGrid.DataSource = inventario;

            if (inventario.Any())
                CargarImagenBloque(inventario.First().Bloque);
        }

        private void ComboRareza_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboRareza.SelectedItem.ToString();
            MostrarInventario(selected);
        }

        private void CargarImagenBloque(string nombreBloque)
        {
            try
            {
                string ruta = $"Resources/{nombreBloque.ToLower()}.png";
                pictureBloque.Image = Image.FromFile(ruta);
            }
            catch
            {
                pictureBloque.Image = null;
            }
        }

        private void MostrarEstadisticas()
        {
            chartStats.Series.Clear();
            var serie = new Series("Bloques por jugador")
            {
                ChartType = SeriesChartType.Column
            };

            foreach (var jugador in _jugadorService.ObtenerTodos())
            {
                int cantidad = _inventarioService.ContarBloques(jugador.Id);
                serie.Points.AddXY(jugador.Nombre, cantidad);
            }

            chartStats.Series.Add(serie);
        }
    }
}
