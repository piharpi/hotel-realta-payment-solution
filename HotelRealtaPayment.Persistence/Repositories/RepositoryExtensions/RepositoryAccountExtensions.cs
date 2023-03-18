using System.Reflection;
using System.Text;
using HotelRealtaPayment.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace HotelRealtaPayment.Persistence.Repositories.RepositoryExtensions;

public static class RepositoryAccountExtensions
{
    public static IQueryable<Account> Search(this IQueryable<Account> accounts, string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return accounts;

        var k = keyword.Trim().ToLower();

        return accounts.Where(t => t.AccountNumber.ToLower().Trim().Contains(k));
    }

    public static IQueryable<Account> Sort(this IQueryable<Account> accounts, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return accounts.OrderBy(e => e.AccountNumber);

        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(Account).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
        return string.IsNullOrWhiteSpace(orderQuery) ? accounts.OrderBy(e => e.AccountNumber) : accounts.OrderBy(orderQuery);
    }
}