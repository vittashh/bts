using System;
using System.Data;
using System.Data.SqlClient;
using bts.Models;

namespace bts
{
    public class DatabaseHelper
    {
        // Строка подключения к базе данных (измените под ваш сервер)
        private string connectionString = @"Server=localhost\SQLEXPRESS;Database=bts;Integrated Security=True";

        #region Клиенты

        // Получить всех клиентов
        public DataTable GetClients()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [Клиенты] ORDER BY [ID_клиента]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка GetClients: " + ex.Message);
            }
            return dt;
        }

        // Добавить нового клиента
        public void AddClient(Client client)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO [Клиенты] 
                                ([Фамилия], [Имя], [Название_компании], [Телефон], [Email], [Город], [Дата_регистрации])
                                VALUES 
                                (@Фамилия, @Имя, @Название_компании, @Телефон, @Email, @Город, @Дата_регистрации)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Фамилия", (object)client.Фамилия ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Имя", (object)client.Имя ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Название_компании", (object)client.Название_компании ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Телефон", client.Телефон);
                cmd.Parameters.AddWithValue("@Email", (object)client.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Город", (object)client.Город ?? "Уфа");
                cmd.Parameters.AddWithValue("@Дата_регистрации", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Обновить данные клиента
        public void UpdateClient(Client client)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE [Клиенты] SET 
                                [Фамилия] = @Фамилия, 
                                [Имя] = @Имя, 
                                [Название_компании] = @Название_компании,
                                [Телефон] = @Телефон, 
                                [Email] = @Email, 
                                [Город] = @Город 
                                WHERE [ID_клиента] = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", client.ID_клиента);
                cmd.Parameters.AddWithValue("@Фамилия", (object)client.Фамилия ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Имя", (object)client.Имя ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Название_компании", (object)client.Название_компании ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Телефон", client.Телефон);
                cmd.Parameters.AddWithValue("@Email", (object)client.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Город", (object)client.Город ?? "Уфа");

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Удалить клиента
        public void DeleteClient(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM [Клиенты] WHERE [ID_клиента] = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Тахографы

        // Получить все тахографы
        public DataTable GetTachographs()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [Тахографы] ORDER BY [ID_тахографа]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка GetTachographs: " + ex.Message);
            }
            return dt;
        }

        // Добавить новый тахограф
        public void AddTachograph(Tachograph tacho)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO [Тахографы] 
                                ([Модель], [Цена], [Гарантия_лет], [Наличие], [Примечание])
                                VALUES 
                                (@Модель, @Цена, @Гарантия_лет, @Наличие, @Примечание)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Модель", tacho.Модель);
                cmd.Parameters.AddWithValue("@Цена", tacho.Цена);
                cmd.Parameters.AddWithValue("@Гарантия_лет", tacho.Гарантия_лет);
                cmd.Parameters.AddWithValue("@Наличие", tacho.Наличие);
                cmd.Parameters.AddWithValue("@Примечание", (object)tacho.Примечание ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Обновить данные тахографа
        public void UpdateTachograph(Tachograph tacho)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE [Тахографы] SET 
                                [Модель] = @Модель, 
                                [Цена] = @Цена, 
                                [Гарантия_лет] = @Гарантия_лет,
                                [Наличие] = @Наличие, 
                                [Примечание] = @Примечание 
                                WHERE [ID_тахографа] = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", tacho.ID_тахографа);
                cmd.Parameters.AddWithValue("@Модель", tacho.Модель);
                cmd.Parameters.AddWithValue("@Цена", tacho.Цена);
                cmd.Parameters.AddWithValue("@Гарантия_лет", tacho.Гарантия_лет);
                cmd.Parameters.AddWithValue("@Наличие", tacho.Наличие);
                cmd.Parameters.AddWithValue("@Примечание", (object)tacho.Примечание ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Удалить тахограф
        public void DeleteTachograph(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM [Тахографы] WHERE [ID_тахографа] = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Заказы

        // Получить все заказы (с расширенной информацией)
        public DataTable GetOrders()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    з.[ID_заказа],
                                    з.[Дата_заказа],
                                    з.[Адрес_установки],
                                    з.[Статус],
                                    з.[Итоговая_цена],
                                    з.[Рассрочка],
                                    з.[Срок_рассрочки_мес],
                                    з.[Комментарий],
                                    к.[Фамилия] + ' ' + ISNULL(к.[Имя], '') AS Клиент,
                                    к.[Телефон] AS Телефон_клиента,
                                    т.[Модель] AS Тахограф,
                                    т.[Цена] AS Цена_тахографа
                                FROM [Заказы] з
                                LEFT JOIN [Клиенты] к ON з.[ID_клиента] = к.[ID_клиента]
                                LEFT JOIN [Тахографы] т ON з.[ID_тахографа] = т.[ID_тахографа]
                                ORDER BY з.[ID_заказа] DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка GetOrders: " + ex.Message);
            }
            return dt;
        }

        // Получить заказы по ID клиента
        public DataTable GetOrdersByClientId(int clientId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    з.[ID_заказа],
                                    з.[Дата_заказа],
                                    з.[Статус],
                                    з.[Итоговая_цена],
                                    т.[Модель] AS Тахограф
                                FROM [Заказы] з
                                LEFT JOIN [Тахографы] т ON з.[ID_тахографа] = т.[ID_тахографа]
                                WHERE з.[ID_клиента] = @ClientId
                                ORDER BY з.[ID_заказа] DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ClientId", clientId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка GetOrdersByClientId: " + ex.Message);
            }
            return dt;
        }

        // Добавить новый заказ
        public void AddOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO [Заказы] 
                                ([ID_клиента], [ID_тахографа], [Дата_заказа], [Адрес_установки], 
                                 [Статус], [Итоговая_цена], [Рассрочка], [Срок_рассрочки_мес], [Комментарий])
                                VALUES 
                                (@ID_клиента, @ID_тахографа, @Дата_заказа, @Адрес_установки,
                                 @Статус, @Итоговая_цена, @Рассрочка, @Срок_рассрочки_мес, @Комментарий)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_клиента", order.ID_клиента);
                cmd.Parameters.AddWithValue("@ID_тахографа", order.ID_тахографа);
                cmd.Parameters.AddWithValue("@Дата_заказа", order.Дата_заказа);
                cmd.Parameters.AddWithValue("@Адрес_установки", (object)order.Адрес_установки ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Статус", (object)order.Статус ?? "Новый");
                cmd.Parameters.AddWithValue("@Итоговая_цена", order.Итоговая_цена);
                cmd.Parameters.AddWithValue("@Рассрочка", order.Рассрочка);
                cmd.Parameters.AddWithValue("@Срок_рассрочки_мес", (object)order.Срок_рассрочки_мес ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Комментарий", (object)order.Комментарий ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Обновить данные заказа
        public void UpdateOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE [Заказы] SET 
                                [ID_клиента] = @ID_клиента,
                                [ID_тахографа] = @ID_тахографа,
                                [Дата_заказа] = @Дата_заказа,
                                [Адрес_установки] = @Адрес_установки,
                                [Статус] = @Статус,
                                [Итоговая_цена] = @Итоговая_цена,
                                [Рассрочка] = @Рассрочка,
                                [Срок_рассрочки_мес] = @Срок_рассрочки_мес,
                                [Комментарий] = @Комментарий
                                WHERE [ID_заказа] = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", order.ID_заказа);
                cmd.Parameters.AddWithValue("@ID_клиента", order.ID_клиента);
                cmd.Parameters.AddWithValue("@ID_тахографа", order.ID_тахографа);
                cmd.Parameters.AddWithValue("@Дата_заказа", order.Дата_заказа);
                cmd.Parameters.AddWithValue("@Адрес_установки", (object)order.Адрес_установки ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Статус", (object)order.Статус ?? "Новый");
                cmd.Parameters.AddWithValue("@Итоговая_цена", order.Итоговая_цена);
                cmd.Parameters.AddWithValue("@Рассрочка", order.Рассрочка);
                cmd.Parameters.AddWithValue("@Срок_рассрочки_мес", (object)order.Срок_рассрочки_мес ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Комментарий", (object)order.Комментарий ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Удалить заказ
        public void DeleteOrder(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM [Заказы] WHERE [ID_заказа] = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Обновить статус заказа
        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE [Заказы] SET [Статус] = @Статус WHERE [ID_заказа] = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", orderId);
                cmd.Parameters.AddWithValue("@Статус", newStatus);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Вспомогательные методы для ComboBox

        // Получить список клиентов для выпадающего списка
        public DataTable GetClientsForCombo()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    [ID_клиента], 
                                    [Фамилия] + ' ' + ISNULL([Имя], '') + ' (' + [Телефон] + ')' AS DisplayName 
                                    FROM [Клиенты] 
                                    ORDER BY [Фамилия]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка GetClientsForCombo: " + ex.Message);
            }
            return dt;
        }

        // Получить список тахографов для выпадающего списка
        public DataTable GetTachographsForCombo()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    [ID_тахографа], 
                                    [Модель] + ' - ' + CAST([Цена] AS NVARCHAR) + ' ₽' AS DisplayName 
                                    FROM [Тахографы] 
                                    WHERE [Наличие] = 1 
                                    ORDER BY [Модель]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка GetTachographsForCombo: " + ex.Message);
            }
            return dt;
        }

        #endregion

        #region Статистика и отчеты

        // Получить общую сумму продаж
        public decimal GetTotalSales()
        {
            decimal total = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT SUM([Итоговая_цена]) FROM [Заказы]";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                        total = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка GetTotalSales: " + ex.Message);
            }
            return total;
        }

        // Получить количество заказов по статусам
        public DataTable GetOrdersCountByStatus()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                    [Статус], 
                                    COUNT(*) AS Количество 
                                    FROM [Заказы] 
                                    GROUP BY [Статус] 
                                    ORDER BY Количество DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Ошибка GetOrdersCountByStatus: " + ex.Message);
            }
            return dt;
        }

        #endregion
    }
}