
using Microsoft.VisualBasic;
using Npgsql;
namespace FIT_Project_Manager.SQLlib;

public class DataBaseHandler
{
    private NpgsqlDataSource? dataSource;
    private NpgsqlConnection? connection;
    
    public DataBaseHandler() 
    {
    }
    public static string GetDBConnectInfo()
    {
        string db_contname = Environment.GetEnvironmentVariable("DB_CONTNAME");
        string db_name = Environment.GetEnvironmentVariable("POSTGRES_DB");
        string db_user = Environment.GetEnvironmentVariable("POSTGRES_USER");
        string db_password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
        string db_port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
        var connectionString = $"Host={db_contname};Port={db_port};Username={db_user};Password={db_password};Database={db_name}";
        // var connectionString = "Host=db;Port=5432;Username=fit_staff;Password=password;Database=fit_db";
        // string connect_query = $"Server={db_contname}; Database={db_name}; Port={db_port}; User ID={db_user}; Password={db_password};";
        // Console.WriteLine(connectionString2);
        // Console.WriteLine(connectionString);
        return connectionString;
    }

    public async Task<bool> ConnectionDatabaseAsync()
    {
        string connectString = GetDBConnectInfo();
        try
        {
            this.dataSource = NpgsqlDataSource.Create(connectString);
            this.connection = await dataSource.OpenConnectionAsync();
            return true;
        }
        catch
        {
            CloseDatabaseAsync();
            return false;
        }
    }

    public async void CloseDatabaseAsync()
    {
        try
        {
            this.connection.CloseAsync().Dispose();
            this.dataSource.Dispose();
        }
        catch
        { }
        this.connection = null;
        this.dataSource = null;
    }

    public class Response
    {
        public bool Flag { get; set; }
        public string Message { get; set; }
    }
    public async Task<Response> InsertRecord(string cmd, List<string> values) 
    {
        Response response = new Response();
        if (!await ConnectionDatabaseAsync())
        {
            response.Flag = false;
            response.Message = "Cannot connected DB.";
            CloseDatabaseAsync();
            return response;
        }
        try
        {
            await using var command = new NpgsqlCommand(cmd, this.connection)
            {
                Parameters = 
                {
                    new() { Value = values[0] },
                    new() { Value = values[1] },
                    new() { Value = Int32.Parse(values[2]) }
                }
            };
            if (await command.ExecuteNonQueryAsync() == -1)
            {
                response.Flag = false;
                response.Message = "Insert Error.";
            }
            else
            {
                response.Flag = true;
                response.Message = "Success Insert.";
            }
        }
        catch
        {
            response.Flag = false;
            response.Message = "Insert Error.";
        }
        CloseDatabaseAsync();
        return response;
    }
    // public async Task<List<UserData>> GetAsyncUserData()
    // {
    //     string connectionString = GetDBConnectInfo();
    //     await using var dataSource = NpgsqlDataSource.Create(connectionString);
    //     await using var connection = await dataSource.OpenConnectionAsync();
    //     await using var command = new NpgsqlCommand("SELECT id, name FROM users WHERE id = ($1)", connection)
    //     {
    //         Parameters = 
    //         {
    //             new() { Value = 1 }
    //         }
    //     };

    //     List<UserData> user_data = new List<UserData>();

    //     await using (var reader = await command.ExecuteReaderAsync())
    //     {
    //         while(await reader.ReadAsync())
    //         {
    //             string uname = reader.GetString("name");
    //             int uid = int.Parse(reader.GetValue("id").ToString());
    //             user_data.Add(new UserData() {UserName=uname, UserId=uid});
    //         }
    //     }
    //     // for (int i=0; i < reader.FieldCount; i++)
    //     // {
    //     //     Console.WriteLine($"{reader[i]}");
    //     // }
    //     Console.WriteLine("Success...");
    //     return user_data;
    // }
}
