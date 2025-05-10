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
        private int currentPage = 1;
        private int pageSize = 50;
        private bool isLoading = false;
        private string filtroActual = "";

        private List<EntidadProduct> productos = new List<EntidadProduct>();

        public MainWindow()
        {
            InitializeComponent();
            txtBuscar.Text = "Ingrese nombre";
            txtBuscar.Foreground = Brushes.Gray;
            CargarPagina(1);
        }

        private void CargarPagina(int page)
        {
            if (isLoading) return;
            isLoading = true;

            List<EntidadProduct> nuevos;
            if (string.IsNullOrWhiteSpace(filtroActual))
            {
                nuevos = negocio.ObtenerProductosPaginado(page, pageSize);
            }
            else
            {
                nuevos = negocio.ObtenerProductosPorNombre(filtroActual);
                productos.Clear(); // cuando es búsqueda, reiniciar
            }

            productos.AddRange(nuevos);
            dgProductos.ItemsSource = null;
            dgProductos.ItemsSource = productos;

            currentPage++;
            isLoading = false;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            filtroActual = txtBuscar.Text.Trim();
            currentPage = 1;
            productos.Clear();

            if (string.IsNullOrWhiteSpace(filtroActual) || filtroActual == "Ingrese nombre")
            {
                filtroActual = "";
                CargarPagina(currentPage);
            }
            else
            {
                CargarPagina(currentPage); // con filtro
            }
        }

        private void BtnRegistrarProducto_Click(object sender, RoutedEventArgs e)
        {
            var ventana = new ProductoFormularioWindow();
            if (ventana.ShowDialog() == true)
            {
                productos.Clear();
                currentPage = 1;
                filtroActual = "";
                CargarPagina(currentPage);
            }
        }

        private void dgProductos_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight - 10 && !isLoading && string.IsNullOrWhiteSpace(filtroActual))
            {
                CargarPagina(currentPage);
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

        private void BtnActualizarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem is EntidadProduct seleccionado)
            {
                var ventana = new ActualizarProductoWindow(seleccionado);
                if (ventana.ShowDialog() == true)
                {
                    productos.Clear();
                    currentPage = 1;
                    filtroActual = "";
                    CargarPagina(currentPage);
                }
            }
            else
            {
                MessageBox.Show("⚠️ Selecciona un producto para actualizar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedItem is EntidadProduct seleccionado)
            {
                var confirmacion = MessageBox.Show($"¿Seguro que deseas eliminar el producto '{seleccionado.Name}'?",
                                                   "Confirmar eliminación",
                                                   MessageBoxButton.YesNo,
                                                   MessageBoxImage.Warning);

                if (confirmacion == MessageBoxResult.Yes)
                {
                    negocio.EliminarProductoLogico(seleccionado.ProductId);
                    MessageBox.Show("✅ Producto eliminado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    productos.Clear();
                    currentPage = 1;
                    filtroActual = "";
                    CargarPagina(currentPage);
                }
            }
            else
            {
                MessageBox.Show("⚠️ Selecciona un producto para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}