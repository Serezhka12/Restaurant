namespace Api.Endpoints;

public static class Routes
{
    public static class Products
    {
        public const string Base = "products";
        public const string ById = "products/{id}";
        public const string Storage = "products/{id}/storage";
        public const string Use = "products/{id}/use";
        public const string LowOnStock = "products/low-on-stock";
    }

    public static class Staff
    {
        public const string Base = "staff";
        public const string ById = "staff/{id}";
        public const string ByDay = "staff/by-day/{day}";
    }
    
    public static class Tables
    {
        public const string Base = "tables";
        public const string ById = "tables/{id}";
        public const string Status = "tables/{id}/status";
    }
    
    public static class Reservations
    {
        public const string Base = "reservations";
        public const string ById = "reservations/{id}";
        public const string ByDate = "reservations/by-date/{date}";
        public const string Reserve = "reservations/reserve";
        public const string Cancel = "reservations/{id}/cancel";
    }
} 