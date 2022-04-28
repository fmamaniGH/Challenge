using System.Data;

namespace Challenge.Ecommerce.Comun
{
    public interface IConnectionFactory
    {
        public interface IConnectionactory
        {
            IDbConnection GetConnection { get; }
        }
    }
}
 