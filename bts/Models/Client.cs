using System;

namespace bts.Models
{
    public class Client
    {
        public int ID_клиента { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Название_компании { get; set; }
        public string Телефон { get; set; }
        public string Email { get; set; }
        public string Город { get; set; }
        public DateTime Дата_регистрации { get; set; }
    }
}