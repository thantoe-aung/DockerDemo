using Microsoft.Data.SqlClient;


namespace DockerDemo
{
    public class ConnectionManager
    {
        private readonly string _connectionString;
       

        public ConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {           
             return new SqlConnection(_connectionString);
              
        }
      
    }
}
