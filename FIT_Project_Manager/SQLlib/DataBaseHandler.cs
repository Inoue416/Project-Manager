
using FIT_Project_Manager.Sessionlib;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic;
using Npgsql;
using Microsoft.AspNetCore.Http;
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
                response.Message = "Error Insert.";
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
            response.Message = "Error Insert.";
        }
        CloseDatabaseAsync();
        return response;
    }

    public class RecordData
    {
        public int Id { get; set; }
        public string Title { get; set;}
        public string Content { get; set;}
    }

    public class ResponseRecordData : Response
    {
        public List<RecordData>? RecordData { get; set; }
    }

    public async Task<ResponseRecordData> GetTodayRecordDataAsync(int user_id)
    {
        ResponseRecordData response = new ResponseRecordData();
        List<RecordData>? recordData = new List<RecordData>();
        string cmd = """
            SELECT * id, title, content
            FROM record
            WHERE id == 1;
        """;
        // string cmd = """
        //     SELECT 
        //         id, title, content
        //     FROM record
        //     WHERE record.id IN (
        //         SELECT * FROM record_has
        //         WHERE (
        //             record_has.user_id == ($1)
        //             AND
        //             DATE_FORMAT(record_has.created_at, '%Y-%m-%d')
        //             ==
        //             DATE_FORMAT(now(), '%Y-%m-%d');
        //         )
        //     );
        // """;
        if (!await ConnectionDatabaseAsync())
        {
            response.Flag = false;
            response.Message = "Cannot connected DB.";
            response.RecordData = null;
            CloseDatabaseAsync();
            return response;
        }
        try
        {
            await using var command = new NpgsqlCommand(cmd, this.connection);
            // {
            //     Parameters = 
            //     {
            //         new() { Value = user_id },
            //     }
            // };
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.Depth == 0)
                {
                    recordData = null;
                }
                else
                {
                    while(await reader.ReadAsync())
                    {
                        RecordData today_data = new RecordData();
                        today_data.Id = reader.GetInt32(0);
                        today_data.Title = reader.GetString(1);
                        today_data.Content = reader.GetString(2);
                        recordData.Add(today_data);
                    }
                }
            }
            response.Message = "Success Select.";
            response.Flag = true;
            response.RecordData = recordData;
        }
        catch
        {
            response.Message = "Error Select.";
            response.Flag = false;
            response.RecordData = null;
        }
        return response;
    }
}
