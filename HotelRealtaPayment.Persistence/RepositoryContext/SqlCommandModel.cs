using System.Data;

namespace HotelRealtaPayment.Persistence.RepositoryContext
{
    internal class SqlCommandModel
    {
        public string CommandText { get; set; }
        public CommandType CommandType { get; set; }
        public SqlCommandParameterModel[] CommandParameters { get; set; }
    }
}
