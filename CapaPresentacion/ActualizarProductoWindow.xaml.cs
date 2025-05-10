using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para ActualizarProductoWindow.xaml
    /// </summary>
    public partial class ActualizarProductoWindow : Window
    {
        private readonly NegocioProduct negocio = new NegocioProduct();
        private EntidadProduct producto;

        public ActualizarProductoWindow(EntidadProduct productoSeleccionado)
        {
            InitializeComponent();
            producto = productoSeleccionado;

            // Pre-cargar datos en los campos
            txtId.Text = producto.ProductId.ToString();
            txtNombre.Text = producto.Name;
            txtPrecio.Text = producto.Price.ToString("0.00");
            txtStock.Text = producto.Stock.ToString();
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                producto.Name = txtNombre.Text;
                producto.Price = decimal.Parse(txtPrecio.Text);
                producto.Stock = int.Parse(txtStock.Text);

                negocio.ActualizarProducto(producto);

                MessageBox.Show("✅ Producto actualizado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("❌ Error de formato en los valores ingresados.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error al actualizar producto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
