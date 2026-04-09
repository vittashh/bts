namespace bts.Models
{
    public class Tachograph
    {
        public int ID_тахографа { get; set; }
        public string Модель { get; set; }
        public decimal Цена { get; set; }
        public int Гарантия_лет { get; set; }
        public bool Наличие { get; set; }
        public string Примечание { get; set; }
    }
}