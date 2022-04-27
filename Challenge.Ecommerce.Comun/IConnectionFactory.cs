using System.Data;

namespace Challenge.Ecommerce.Comun
{
    public interface IConnectionFactory
    {
        public interface Iconnectionactory
        {
            IDbConnection GetConnection { get; }
        }
    }
}
 