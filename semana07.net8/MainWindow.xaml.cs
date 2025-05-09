using System.Windows;
using capa_de_entidades;
using capa_de_negocios;

namespace semana07.net8
{
    public partial class MainWindow : Window
    {
        ProductLogic logic = new ProductLogic();

        public MainWindow()
        {
            InitializeComponent();
            // Cargar todos al inicio (opcional)
            // dgProductos.ItemsSource = logic.ObtenerProductos();
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

                dgProductos.ItemsSource = logic.ObtenerProductos(); // actualizar tabla
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

    }
}
