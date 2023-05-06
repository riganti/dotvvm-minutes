namespace FiltersDemo.Domain.Exceptions
{
    public class WarehouseNotOperationalException : DomainException
    {
        public WarehouseNotOperationalException()
            : base($"The warehouse is not operational right now!")
        {
        }
    }
}
