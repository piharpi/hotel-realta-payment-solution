using System.Reflection;
using System.Text;
using HotelRealtaPayment.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace HotelRealtaPayment.Persistence.Repositories.RepositoryExtensions;

public static class RepositoryBankExtensions
{
    public static IQueryable<Bank> Search(this IQueryable<Bank> banks, string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return banks;

        var k = keyword.Trim().ToLower();

        return banks.Where(t => t.Name.ToLower().Trim().Contains(k));
    }

    public static IQueryable<Bank> Sort(this IQueryable<Bank> banks, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return banks.OrderBy(e => e.Name);

        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(Bank).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
        return string.IsNullOrWhiteSpace(orderQuery) ? banks.OrderBy(e => e.Name) : banks.OrderBy(orderQuery);
    }
}