using System.Collections.Generic;

namespace SuperZapatos.DTO
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Total_in_shelf { get; set; }
        public int Total_in_vault { get; set; }
        public int Store_Id { get; set; }
    }
}