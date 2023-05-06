using System;

namespace FiltersDemo.Domain.Exceptions
{
    public class DomainException : Exception
    {

        public DomainException(string domainMesage) : base(domainMesage)
        {
        }
    }
}