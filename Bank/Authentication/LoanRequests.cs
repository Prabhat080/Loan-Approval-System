using System.Data.SqlClient;

namespace Bank.Authentication
{
    public class LoanRequests
    {
        public List<LoanUser> pending = new List<LoanUser>();
        public List<LoanUser> completed = new List<LoanUser>();
        public string message = "null";
        public LoanRequests()
        {
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * from loans";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LoanUser client = new LoanUser();

                                client.id = reader.GetInt32(0);
                                client.email = reader.GetString(1);
                                client.loan = reader.GetDouble(2);
								pending.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                Console.WriteLine("Error: ", ex.ToString());
            }

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string sql = "SELECT * from processedLoans";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								LoanUser client = new LoanUser();

								client.id = reader.GetInt32(0);
								client.email = reader.GetString(1);
								client.loan = reader.GetDouble(2);
                                client.status = reader.GetInt32(3);
                                client.processed_date = reader.GetDateTime(4);
								completed.Add(client);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
                message = ex.ToString();
				Console.WriteLine("Error: ", ex.ToString());
			}
		}

        public void ReInitialize()
        {
            pending = new List<LoanUser>();
            completed = new List<LoanUser>();

            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * from loans";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LoanUser client = new LoanUser();

                                client.id = reader.GetInt32(0);
                                client.email = reader.GetString(1);
                                client.loan = reader.GetDouble(2);
                                pending.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                Console.WriteLine("Error: ", ex.ToString());
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * from processedLoans";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LoanUser client = new LoanUser();

                                client.id = reader.GetInt32(0);
                                client.email = reader.GetString(1);
                                client.loan = reader.GetDouble(2);
                                client.status = reader.GetInt32(3);
                                client.processed_date = reader.GetDateTime(4);
                                completed.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                Console.WriteLine("Error: ", ex.ToString());
            }
        }
    }
}
