using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Toolkit.Infrastructure.Models;

namespace Toolkit.Infrastructure.Services
{
    public static class DatabaseService
    {
        private static readonly string ConnectionString = "Data Source=toolkit.db";

        static DatabaseService()
        {
            InitializeDatabase();
        }

        private static SqliteConnection CreateConnection()
        {
            var conn = new SqliteConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        private static void InitializeDatabase()
        {
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Sites (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SiteId TEXT NOT NULL,
                    Name TEXT NOT NULL,
                    Latitude REAL NOT NULL,
                    Longitude REAL NOT NULL,
                    TowerHeight REAL NOT NULL,
                    SiteType TEXT,
                    Technology TEXT,
                    Notes TEXT,
                    CreatedAt TEXT NOT NULL,
                    UpdatedAt TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS EquipmentItems (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Category TEXT NOT NULL,
                    Manufacturer TEXT NOT NULL,
                    Model TEXT NOT NULL,
                    Type TEXT,
                    FrequencyMHz REAL,
                    PowerWatts REAL,
                    Voltage REAL,
                    Gain REAL,
                    Specifications TEXT,
                    Notes TEXT,
                    CreatedAt TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS SavedCalculations (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProjectName TEXT NOT NULL,
                    ToolName TEXT NOT NULL,
                    InputJson TEXT,
                    ResultJson TEXT,
                    Notes TEXT,
                    CreatedAt TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Projects (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    CreatedAt TEXT NOT NULL
                );
            ";
            cmd.ExecuteNonQuery();
        }

        // ==================== Sites CRUD ====================
        public static void AddSite(Site site)
        {
            site.CreatedAt = DateTime.Now;
            site.UpdatedAt = DateTime.Now;

            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Sites (SiteId, Name, Latitude, Longitude, TowerHeight, SiteType, Technology, Notes, CreatedAt, UpdatedAt)
                VALUES (@SiteId, @Name, @Latitude, @Longitude, @TowerHeight, @SiteType, @Technology, @Notes, @CreatedAt, @UpdatedAt)";

            cmd.Parameters.AddWithValue("@SiteId", site.SiteId);
            cmd.Parameters.AddWithValue("@Name", site.Name);
            cmd.Parameters.AddWithValue("@Latitude", site.Latitude);
            cmd.Parameters.AddWithValue("@Longitude", site.Longitude);
            cmd.Parameters.AddWithValue("@TowerHeight", site.TowerHeight);
            cmd.Parameters.AddWithValue("@SiteType", site.SiteType);
            cmd.Parameters.AddWithValue("@Technology", site.Technology);
            cmd.Parameters.AddWithValue("@Notes", site.Notes);
            cmd.Parameters.AddWithValue("@CreatedAt", site.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@UpdatedAt", site.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
        }

        public static List<Site> GetAllSites()
        {
            var sites = new List<Site>();
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Sites ORDER BY Id DESC";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sites.Add(new Site
                {
                    Id = reader.GetInt32(0),
                    SiteId = reader.GetString(1),
                    Name = reader.GetString(2),
                    Latitude = reader.GetDouble(3),
                    Longitude = reader.GetDouble(4),
                    TowerHeight = reader.GetDouble(5),
                    SiteType = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    Technology = reader.IsDBNull(7) ? "" : reader.GetString(7),
                    Notes = reader.IsDBNull(8) ? "" : reader.GetString(8),
                    CreatedAt = DateTime.Parse(reader.GetString(9)),
                    UpdatedAt = DateTime.Parse(reader.GetString(10))
                });
            }
            return sites;
        }

        public static void UpdateSite(Site site)
        {
            site.UpdatedAt = DateTime.Now;
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE Sites SET
                    SiteId = @SiteId,
                    Name = @Name,
                    Latitude = @Latitude,
                    Longitude = @Longitude,
                    TowerHeight = @TowerHeight,
                    SiteType = @SiteType,
                    Technology = @Technology,
                    Notes = @Notes,
                    UpdatedAt = @UpdatedAt
                WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@SiteId", site.SiteId);
            cmd.Parameters.AddWithValue("@Name", site.Name);
            cmd.Parameters.AddWithValue("@Latitude", site.Latitude);
            cmd.Parameters.AddWithValue("@Longitude", site.Longitude);
            cmd.Parameters.AddWithValue("@TowerHeight", site.TowerHeight);
            cmd.Parameters.AddWithValue("@SiteType", site.SiteType);
            cmd.Parameters.AddWithValue("@Technology", site.Technology);
            cmd.Parameters.AddWithValue("@Notes", site.Notes);
            cmd.Parameters.AddWithValue("@UpdatedAt", site.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@Id", site.Id);
            cmd.ExecuteNonQuery();
        }

        public static void DeleteSite(int id)
        {
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Sites WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        // ==================== Equipment CRUD ====================
        public static void AddEquipment(EquipmentItem equipment)
        {
            equipment.CreatedAt = DateTime.Now;
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO EquipmentItems 
                (Category, Manufacturer, Model, Type, FrequencyMHz, PowerWatts, Voltage, Gain, Specifications, Notes, CreatedAt)
                VALUES 
                (@Category, @Manufacturer, @Model, @Type, @FrequencyMHz, @PowerWatts, @Voltage, @Gain, @Specifications, @Notes, @CreatedAt)";

            cmd.Parameters.AddWithValue("@Category", equipment.Category);
            cmd.Parameters.AddWithValue("@Manufacturer", equipment.Manufacturer);
            cmd.Parameters.AddWithValue("@Model", equipment.Model);
            cmd.Parameters.AddWithValue("@Type", equipment.Type);
            cmd.Parameters.AddWithValue("@FrequencyMHz", (object?)equipment.FrequencyMHz ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PowerWatts", (object?)equipment.PowerWatts ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Voltage", (object?)equipment.Voltage ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Gain", (object?)equipment.Gain ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Specifications", equipment.Specifications);
            cmd.Parameters.AddWithValue("@Notes", equipment.Notes);
            cmd.Parameters.AddWithValue("@CreatedAt", equipment.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
        }

        public static List<EquipmentItem> GetAllEquipment()
        {
            var items = new List<EquipmentItem>();
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM EquipmentItems ORDER BY Id DESC";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new EquipmentItem
                {
                    Id = reader.GetInt32(0),
                    Category = reader.GetString(1),
                    Manufacturer = reader.GetString(2),
                    Model = reader.GetString(3),
                    Type = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    FrequencyMHz = reader.IsDBNull(5) ? null : reader.GetDouble(5),
                    PowerWatts = reader.IsDBNull(6) ? null : reader.GetDouble(6),
                    Voltage = reader.IsDBNull(7) ? null : reader.GetDouble(7),
                    Gain = reader.IsDBNull(8) ? null : reader.GetDouble(8),
                    Specifications = reader.IsDBNull(9) ? "" : reader.GetString(9),
                    Notes = reader.IsDBNull(10) ? "" : reader.GetString(10),
                    CreatedAt = DateTime.Parse(reader.GetString(11))
                });
            }
            return items;
        }

        public static void UpdateEquipment(EquipmentItem equipment)
        {
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE EquipmentItems SET
                    Category = @Category,
                    Manufacturer = @Manufacturer,
                    Model = @Model,
                    Type = @Type,
                    FrequencyMHz = @FrequencyMHz,
                    PowerWatts = @PowerWatts,
                    Voltage = @Voltage,
                    Gain = @Gain,
                    Specifications = @Specifications,
                    Notes = @Notes
                WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@Category", equipment.Category);
            cmd.Parameters.AddWithValue("@Manufacturer", equipment.Manufacturer);
            cmd.Parameters.AddWithValue("@Model", equipment.Model);
            cmd.Parameters.AddWithValue("@Type", equipment.Type);
            cmd.Parameters.AddWithValue("@FrequencyMHz", (object?)equipment.FrequencyMHz ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PowerWatts", (object?)equipment.PowerWatts ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Voltage", (object?)equipment.Voltage ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Gain", (object?)equipment.Gain ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Specifications", equipment.Specifications);
            cmd.Parameters.AddWithValue("@Notes", equipment.Notes);
            cmd.Parameters.AddWithValue("@Id", equipment.Id);
            cmd.ExecuteNonQuery();
        }

        public static void DeleteEquipment(int id)
        {
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM EquipmentItems WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        // ==================== Saved Calculations ====================
        public static void SaveCalculation(SavedCalculation calc)
        {
            calc.CreatedAt = DateTime.Now;
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO SavedCalculations (ProjectName, ToolName, InputJson, ResultJson, Notes, CreatedAt)
                VALUES (@ProjectName, @ToolName, @InputJson, @ResultJson, @Notes, @CreatedAt)";

            cmd.Parameters.AddWithValue("@ProjectName", calc.ProjectName);
            cmd.Parameters.AddWithValue("@ToolName", calc.ToolName);
            cmd.Parameters.AddWithValue("@InputJson", calc.InputJson);
            cmd.Parameters.AddWithValue("@ResultJson", calc.ResultJson);
            cmd.Parameters.AddWithValue("@Notes", calc.Notes);
            cmd.Parameters.AddWithValue("@CreatedAt", calc.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
        }

        public static List<SavedCalculation> GetAllSavedCalculations()
        {
            var list = new List<SavedCalculation>();
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM SavedCalculations ORDER BY Id DESC";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SavedCalculation
                {
                    Id = reader.GetInt32(0),
                    ProjectName = reader.GetString(1),
                    ToolName = reader.GetString(2),
                    InputJson = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    ResultJson = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    Notes = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    CreatedAt = DateTime.Parse(reader.GetString(6))
                });
            }
            return list;
        }

        public static void DeleteSavedCalculation(int id)
        {
            using var conn = CreateConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM SavedCalculations WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}