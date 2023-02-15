using System.Data;

namespace HotelRealtaPayment.Persistence.RepositoryContext
{
    public class SqlCommandParameterModel
    {
        public string ParameterName { get; set; }
        public DbType DataType { get; set; }
        public dynamic Value { get; set; }
        public bool IsNullable { get; set; }
    }
}
