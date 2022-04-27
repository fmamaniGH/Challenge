﻿using System;
using Challenge.Ecommerce.Comun;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Challenge.Ecommerce.Infrastructure.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection {
            get
            {
                var sqlConnection = new SqlConnection();
                if (sqlConnection == null) return null;

                sqlConnection.ConnectionString = _configuration.GetConnectionString("ChallengeDB");
                sqlConnection.Open();
                return sqlConnection;
            }
        }
    }
}
