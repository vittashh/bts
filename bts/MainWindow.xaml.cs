using System;
using System.Data;
using System.Windows;
using bts.Models;

namespace bts
{
    public partial class MainWindow : Window
    {
        private DatabaseHelper db = new DatabaseHelper();
        private string currentTable = "Clients"; // Clients, Tachographs, Orders

        public MainWindow()
        {
            InitializeComponent();
            LoadClients();
        }

        // Загрузка клиентов
        private void LoadClients()
        {
            try
            {
                DataTable dt = db.GetClients();
                dgData.ItemsSource = dt.DefaultView;
                currentTable = "Clients";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки клиентов: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Загрузка тахографов
        private void LoadTachographs()
        {
            try
            {
                DataTable dt = db.GetTachographs();
                dgData.ItemsSource = dt.DefaultView;
                currentTable = "Tachographs";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки тахографов: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Загрузка заказов
        private void LoadOrders()
        {
            try
            {
                DataTable dt = db.GetOrders();
                dgData.ItemsSource = dt.DefaultView;
                currentTable = "Orders";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки заказов: " + ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Кнопки навигации
        private void BtnClients_Click(object sender, RoutedEventArgs e)
        {
            LoadClients();
            btnAddClient.Visibility = Visibility.Visible;
            btnEditClient.Visibility = Visibility.Visible;
            btnDeleteClient.Visibility = Visibility.Visible;
        }

        private void BtnTachographs_Click(object sender, RoutedEventArgs e)
        {
            LoadTachographs();
            btnAddClient.Visibility = Visibility.Collapsed;
            btnEditClient.Visibility = Visibility.Collapsed;
            btnDeleteClient.Visibility = Visibility.Collapsed;
            MessageBox.Show("Редактирование тахографов будет добавлено позже", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            LoadOrders();
            btnAddClient.Visibility = Visibility.Collapsed;
            btnEditClient.Visibility = Visibility.Collapsed;
            btnDeleteClient.Visibility = Visibility.Collapsed;
            MessageBox.Show("Редактирование заказов будет добавлено позже", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Добавление клиента
        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ClientEditWindow();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    db.AddClient(dialog.Client);
                    MessageBox.Show("Клиент успешно добавлен!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadClients();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при добавлении: " + ex.Message, "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Редактирование клиента
        private void BtnEditClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента для редактирования", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView selectedRow = (DataRowView)dgData.SelectedItem;
            var client = new Client
            {
                ID_клиента = Convert.ToInt32(selectedRow["ID_клиента"]),
                Фамилия = selectedRow["Фамилия"].ToString(),
                Имя = selectedRow["Имя"].ToString(),
                Название_компании = selectedRow["Название_компании"].ToString(),
                Телефон = selectedRow["Телефон"].ToString(),
                Email = selectedRow["Email"].ToString(),
                Город = selectedRow["Город"].ToString()
            };

            var dialog = new ClientEditWindow(client);
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    db.UpdateClient(dialog.Client);
                    MessageBox.Show("Клиент успешно обновлен!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadClients();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении: " + ex.Message, "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Удаление клиента
        private void BtnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                MessageBox.Show("Выберите клиента для удаления", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView selectedRow = (DataRowView)dgData.SelectedItem;
            string clientName = $"{selectedRow["Фамилия"]} {selectedRow["Имя"]}".Trim();

            var result = MessageBox.Show($"Удалить клиента '{clientName}'?\n\nЭто действие нельзя отменить.",
                "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(selectedRow["ID_клиента"]);
                    db.DeleteClient(id);
                    MessageBox.Show("Клиент успешно удален!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadClients();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении: " + ex.Message, "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Обновление данных
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            switch (currentTable)
            {
                case "Clients":
                    LoadClients();
                    break;
                case "Tachographs":
                    LoadTachographs();
                    break;
                case "Orders":
                    LoadOrders();
                    break;
            }
        }

        // При выборе строки в DataGrid
        private void DgData_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (currentTable == "Clients")
            {
                btnEditClient.IsEnabled = dgData.SelectedItem != null;
                btnDeleteClient.IsEnabled = dgData.SelectedItem != null;
            }
        }
    }
}