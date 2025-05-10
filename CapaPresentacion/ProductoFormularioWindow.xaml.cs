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
    /// Lógica de interacción para InsertarProductoWindow.xaml
    /// </summary>
    public partial class ProductoFormularioWindow : Window
    {
        private readonly NegocioProduct negocio = new NegocioProduct();

        public ProductoFormularioWindow()
        {
            InitializeComponent();
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                    string.IsNullOrWhiteSpace(txtStock.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Crear producto
                var producto = new EntidadProduct
                {
                    Name = txtNombre.Text,
                    Price = decimal.Parse(txtPrecio.Text),
                    Stock = int.Parse(txtStock.Text)
                };

                // Registrar
                negocio.RegistrarProducto(producto);

                MessageBox.Show("✅ Producto registrado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor ingrese valores numéricos válidos en Precio y Stock.", "Error de formato", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Error al registrar producto: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
