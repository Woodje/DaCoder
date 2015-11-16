namespace DaCoder.Data
{
    public class Keyword
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
    }
}