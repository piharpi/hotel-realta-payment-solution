using System.Reflection;
using System.Text;
using HotelRealtaPayment.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace HotelRealtaPayment.Persistence.Repositories.RepositoryExtensions;

public static class RepositoryTransactionExtensions
{
    public static IQueryable<Transaction> Search(this IQueryable<Transaction> transactions, string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return transactions;

        var k = keyword.Trim().ToLower();

        return transactions.Where(t => t.PatrTrxNumber.ToLower().Trim().Contains(k));
    }
    
    public static IQueryable<Transaction> Filter(this IQueryable<Transaction> transactions, string filter)
    {
        
        if (string.IsNullOrWhiteSpace(filter))
            return transactions;
        
        var k = filter.Trim().ToLower();

        return transactions.Where(t => t.PatrType.ToLower().Trim().Equals(k));
    }


    public static IQueryable<Transaction> Sort(this IQueryable<Transaction> transactions, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return transactions.OrderBy(e => e.PatrType);

        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(Transaction).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
        return string.IsNullOrWhiteSpace(orderQuery) ? transactions.OrderBy(e => e.PatrType) : transactions.OrderBy(orderQuery);
    }
}