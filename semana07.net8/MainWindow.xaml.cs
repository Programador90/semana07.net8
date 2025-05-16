// MainWindow.xaml.cs

using System.Windows;
using System.Windows.Controls;
using capa_de_entidades;
using capa_de_negocios;

namespace semana07.net8
{
    public partial class MainWindow : Window
    {
        ProductLogic logic = new ProductLogic();

        // 1. Variable para llevar el producto seleccionado
        private Product _seleccionado;

        public MainWindow()
        {
            InitializeComponent();
            dgProductos.ItemsSource = logic.ObtenerProductos();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();
            dgProductos.ItemsSource = logic.BuscarPorNombre(nombre);
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var producto = new Product
                {
                    Name = txtNombre.Text.Trim(),
                    Price = decimal.Parse(txtPrecio.Text),
                    Stock = int.Parse(txtStock.Text),
                    Active = true
                };

                logic.InsertarProducto(producto);
                MessageBox.Show("Producto registrado correctamente.");

                dgProductos.ItemsSource = logic.ObtenerProductos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void dgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProductos.SelectedItem is Product prod)
            {
                _seleccionado = prod;
                txtNombre.Text = prod.Name;
                txtPrecio.Text = prod.Price.ToString();
                txtStock.Text = prod.Stock.ToString();
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (_seleccionado == null)
            {
                MessageBox.Show("Seleccione primero un producto.");
                return;
            }

            try
            {
                _seleccionado.Name = txtNombre.Text.Trim();
                _seleccionado.Price = decimal.Parse(txtPrecio.Text);
                _seleccionado.Stock = int.Parse(txtStock.Text);

                logic.ActualizarProducto(_seleccionado);
                MessageBox.Show("Producto actualizado correctamente.");

                dgProductos.ItemsSource = logic.ObtenerProductos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (_seleccionado == null)
            {
                MessageBox.Show("Seleccione primero un producto.");
                return;
            }

            if (MessageBox.Show(
                    $"¿Dar de baja al producto “{_seleccionado.Name}”?",
                    "Confirmar",
                    MessageBoxButton.YesNo
                ) == MessageBoxResult.Yes)
            {
                logic.EliminarProducto(_seleccionado.ProductId);
                MessageBox.Show("Producto marcado como inactivo.");

                dgProductos.ItemsSource = logic.ObtenerProductos();
                LimpiarCampos();
            }
        }

        /// <summary>
        /// Limpia todos los TextBox y quita la selección del DataGrid.
        /// </summary>
        private void LimpiarCampos()
        {
            txtBuscar.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            dgProductos.UnselectAll();
            _seleccionado = null;
        }
    }
}
