using System;

namespace bts.Models
{
    public class Order
    {
        public int ID_заказа { get; set; }
        public int ID_клиента { get; set; }
        public int ID_тахографа { get; set; }
        public DateTime Дата_заказа { get; set; }
        public string Адрес_установки { get; set; }
        public string Статус { get; set; }
        public decimal Итоговая_цена { get; set; }
        public bool Рассрочка { get; set; }
        public int? Срок_рассрочки_мес { get; set; }
        public string Комментарий { get; set; }
    }
}