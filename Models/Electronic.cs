namespace PTLab2_Final.Models
{
    public class Electronic
    {
        public int Id { get; set; }
        public string? Name { get; set; } // имя товара
        public string? Category { get; set; } // категория товара
        public float? Price { get; set; } // цена товара
        public int ForSale { get; set; } // кол-во для продажи
        public int Sold { get; set; } // кол-во проданного товара
    }
}
