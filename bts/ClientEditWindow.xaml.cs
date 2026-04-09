using System.Windows;
using bts.Models;

namespace bts
{
    public partial class ClientEditWindow : Window
    {
        public Client Client { get; set; }
        private bool isEditMode = false;

        public ClientEditWindow(Client client = null)
        {
            InitializeComponent();

            if (client != null && client.ID_клиента > 0)
            {
                // Режим редактирования
                isEditMode = true;
                Client = client;
                LoadClientData();
                this.Title = "Редактирование клиента";
            }
            else
            {
                // Режим добавления
                isEditMode = false;
                Client = new Client();
                this.Title = "Добавление нового клиента";
            }
        }

        private void LoadClientData()
        {
            tbLastName.Text = Client.Фамилия;
            tbFirstName.Text = Client.Имя;
            tbCompany.Text = Client.Название_компании;
            tbPhone.Text = Client.Телефон;
            tbEmail.Text = Client.Email;

            // Устанавливаем город в комбобоксе
            if (!string.IsNullOrEmpty(Client.Город))
            {
                cbCity.Text = Client.Город;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                MessageBox.Show("Телефон обязателен для заполнения!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbPhone.Focus();
                return;
            }

            // Заполняем данные клиента
            Client.Фамилия = tbLastName.Text.Trim();
            Client.Имя = tbFirstName.Text.Trim();
            Client.Название_компании = string.IsNullOrWhiteSpace(tbCompany.Text) ? null : tbCompany.Text.Trim();
            Client.Телефон = tbPhone.Text.Trim();
            Client.Email = string.IsNullOrWhiteSpace(tbEmail.Text) ? null : tbEmail.Text.Trim();
            Client.Город = string.IsNullOrWhiteSpace(cbCity.Text) ? "Уфа" : cbCity.Text.Trim();

            if (!isEditMode)
            {
                Client.Дата_регистрации = System.DateTime.Now;
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}