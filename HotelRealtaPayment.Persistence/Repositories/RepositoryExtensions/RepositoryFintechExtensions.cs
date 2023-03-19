using System.Reflection;
using System.Text;
using HotelRealtaPayment.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace HotelRealtaPayment.Persistence.Repositories.RepositoryExtensions;

public static class RepositoryFintechExtensions
{
    public static IQueryable<Fintech> Search(this IQueryable<Fintech> fintechs, string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return fintechs;

        var k = keyword.Trim().ToLower();

        return fintechs.Where(t => t.Code.ToLower().Trim().Contains(k) || t.Name.ToLower().Trim().Contains(k));
    }

    public static IQueryable<Fintech> Sort(this IQueryable<Fintech> fintechs, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return fintechs.OrderBy(e => e.Name);

        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(Fintech).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
        return string.IsNullOrWhiteSpace(orderQuery) ? fintechs.OrderBy(e => e.Name) : fintechs.OrderBy(orderQuery);
    }
}