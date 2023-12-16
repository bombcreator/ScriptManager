using System;
using System.Data.SqlClient;
using System.IO;

class Program
{
    static void Main()
    {
        // Replace with your actual connection details
        string connectionString = "Data Source=YourServer;Initial Catalog=YourDatabase;Integrated Security=True";

        // Replace with your actual table name
        string tableName = "YourTableName";

        // Replace with the path where you want to save the script file
        string scriptFilePath = @"C:\Path\To\Your\InsertScript.sql";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Replace with your actual column names
            string selectQuery = $"SELECT * FROM {tableName}";

            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Generate INSERT statements and write to a file
                    GenerateInsertScript(reader, scriptFilePath);
                }
            }
        }

        Console.WriteLine("Script generation complete.");
    }

    static void GenerateInsertScript(SqlDataReader reader, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            while (reader.Read())
            {
                string values = string.Join(", ", GetValues(reader));
                string insertStatement = $"INSERT INTO YourTableName VALUES ({values});"; // Replace with your actual table name

                writer.WriteLine(insertStatement);
            }
        }
    }

    static object[] GetValues(SqlDataReader reader)
    {
        object[] values = new object[reader.FieldCount];
        reader.GetValues(values);
        return values;
    }
}
