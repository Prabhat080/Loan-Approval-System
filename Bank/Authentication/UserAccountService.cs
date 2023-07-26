using System.Data.SqlClient;
using System.Security.Principal;
using System.Xml.Linq;

namespace Bank.Authentication
{
	public class UserAccountService
	{
		private List<UserAccount> _users = new List<UserAccount>();
		private List<LoanUser> loanRequests = new List<LoanUser>();
		public string message = "null";
		public UserAccountService()
		{
			string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string sql = "SELECT * from Accounts";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								UserAccount client = new UserAccount();

								client.id = "" + reader.GetInt32(0);
								client.name = reader.GetString(1);
								client.email = reader.GetString(2);
								client.password = reader.GetString(3);
								client.role = reader.GetString(4);

								_users.Add(client);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: ", ex.ToString());
			}

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
                                loanRequests.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ", ex.ToString());
            }
        }

		public void RegisterNewUser(UserAccount newAccount)
		{
			string name = newAccount.name;
			string email = newAccount.email;
			string password = newAccount.password;

            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO Accounts "+
								 "(name, email, pass) VALUES "+
								 "(@name, @email, @password);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
						command.Parameters.AddWithValue("@name", name);
						command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);

						command.ExecuteNonQuery();
						_users.Add(newAccount);
					}
                }
            }
            catch (Exception ex)
            {
				Console.WriteLine("Error: ", ex.ToString());
            }
        }

		public int GetTotalRequests(string EmailId)
		{
			string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string sql = "SELECT * from loans WHERE email = @email";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@email", EmailId);
						command.ExecuteNonQuery();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							var num = 0;
							while (reader.Read())
							{
								num += 1;
							}
							return num;
						}
					}
				}
			}
			catch (Exception ex)
			{
				return 10;
			}
			return 4;
		}

		public void ProcessLoanRequest(string EmailId, float amount)
		{
			string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string sql = "INSERT INTO loans " +
								 "(email,loan) VALUES " +
								 "(@email,@loan);";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						string x = "" + amount;
						command.Parameters.AddWithValue("@email", EmailId);
						command.Parameters.AddWithValue("@loan", ""+amount);
						command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				message = ex.ToString();
				Console.WriteLine("Error: ", ex.ToString());
			}
		}

		public List<double> GetLoansRecord(string EmailId)
		{
			string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

			List<double> record = new();

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string sql = "SELECT * from loans WHERE email = @email";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@email", EmailId);
						command.ExecuteNonQuery();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								double num = reader.GetDouble(2);

								record.Add(num);
							}
						}
					}
				}
				return record;
			}
			catch (Exception ex)
			{
				message = ex.ToString();
				Console.WriteLine("Error", ex.ToString());
			}
			
			return new();
		}

		public List<DateTime> GetDateRecord(string EmailId)
		{
			string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mystore;Integrated Security=True";

			List<DateTime> record = new();

			try
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					string sql = "SELECT * from loans WHERE email = @email";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@email", EmailId);
						command.ExecuteNonQuery();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								DateTime request_date = reader.GetDateTime(3);

								record.Add(request_date);
							}
						}
					}
				}
				return record;
			}
			catch (Exception ex)
			{
				message = ex.ToString();
				Console.WriteLine("Error", ex.ToString());
			}

			return new();
		}
		public UserAccount GetByUserEmail(string userEmail)
		{
			return _users.FirstOrDefault(x => x.email == userEmail);
		}
	}
}
