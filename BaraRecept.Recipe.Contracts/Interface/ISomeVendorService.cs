using System.Collections.Generic;
using BaraRecept.Recipe.Contracts.Entities;

namespace BaraRecept.Recipe.Contracts.Interface
{
    public interface ISomeVendorService
    {
        IReadOnlyCollection<SomeVendorModel> GetValues(string someSetting);
    }
}
