namespace WebApplication71.DTOs
{
    public class BaseModel<T>
    {
        public T Object { get; set; } // pobiera obiekt o określonym typie np listy, klasy, wartości bool

        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
