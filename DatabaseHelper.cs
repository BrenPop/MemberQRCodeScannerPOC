using MemberQRCodeScannerPOC.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberQRCodeScannerPOC
{
    public static class DatabaseHelper
    {
        private static string dbLocation = @"..\..\..\Data\MemberQRCodeScannerPOC.db;";
        private static string connectionString = @"Data Source=..\..\..\Data\MemberQRCodeScannerPOC.db;Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbLocation))
            {
                SQLiteConnection.CreateFile(dbLocation);

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Create users table
                    string createUsersTable = @"
                        CREATE TABLE IF NOT EXISTS Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            Name VARCHAR(255), 
                            Email VARCHAR(255), 
                            QRCodeLocation VARCHAR(255)
                        );";
                    
                    using (SQLiteCommand command = new SQLiteCommand(createUsersTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
