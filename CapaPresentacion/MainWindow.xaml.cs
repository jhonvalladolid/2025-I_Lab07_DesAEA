using CapaEntidad;
using CapaNegocio;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CapaPresentacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NegocioProduct negocio = new NegocioProduct();

        public MainWindow()
        {
            InitializeComponent();
            txtBuscar.Text = "Ingrese nombre";
            txtBuscar.Foreground = Brushes.Gray;
            CargarTodos();
        }

        private void CargarTodos()
        {
            List<EntidadProduct> productos = negocio.ObtenerProductos();
            dgProductos.ItemsSource = productos;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || nombre == "Ingrese nombre")
            {
                CargarTodos();
            }
            else
            {
                List<EntidadProduct> productosFiltrados = negocio.ObtenerProductosPorNombre(nombre);
                dgProductos.ItemsSource = productosFiltrados;
            }
        }

        private void BtnRegistrarProducto_Click(object sender, RoutedEventArgs e)
        {
            var ventana = new InsertarProductoWindow();
            if (ventana.ShowDialog() == true)
            {
                CargarTodos();
            }
        }

        private void txtBuscar_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBuscar.Text == "Ingrese nombre")
            {
                txtBuscar.Text = "";
                txtBuscar.Foreground = Brushes.Black;
            }
        }

        private void txtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = "Ingrese nombre";
                txtBuscar.Foreground = Brushes.Gray;
            }
        }
    }
}