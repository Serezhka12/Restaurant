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
} 