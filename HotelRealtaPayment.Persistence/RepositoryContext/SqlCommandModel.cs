using System.Data;

namespace HotelRealtaPayment.Persistence.RepositoryContext
{
    public class SqlCommandModel
    {
        public string CommandText { get; set; }
        public CommandType CommandType { get; set; }
        public SqlCommandParameterModel[] CommandParameters { get; set; }
    }
}