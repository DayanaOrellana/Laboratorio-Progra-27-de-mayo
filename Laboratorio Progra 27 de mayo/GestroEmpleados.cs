using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading.Tasks;
using static Laboratorio_Progra_27_de_mayo.Empleados;

namespace Laboratorio_Progra_27_de_mayo
{
   
    

        public class GestorEmpleados
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }

            public int Edad { get; set; }
            
            public int Sueldo { get; set; }
            public string Cargo { get; set; }

            private string connectionString;

            public GestorEmpleados(string databasePath)
            {
                connectionString = $"Data Source={databasePath};Version=3;";
            }

        public GestorEmpleados()
        {
        }

        //estoy usando sqlite y la libreria System.Data.SQLite
        public void CrearTabla()
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "CREATE TABLE IF NOT EXISTS Empleados (Id INTEGER PRIMARY KEY AUTOINCREMENT, Nombre VARCHAR(100), Apellido VARCHAR(100), Edad INTEGER, Cargo VARCHAR(100))";
                    SQLiteCommand command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            public void AgregarEmpleado(Empleado empleado)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Empleados (Nombre, Apellido, Edad, Cargo) " +
                    "VALUES (@Nombre, @Apellido, @Edad, @Cargo)";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                        command.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                        command.Parameters.AddWithValue("@Edad", empleado.Edad);
                        command.Parameters.AddWithValue("@Cargo", empleado.Cargo);

                        command.ExecuteNonQuery();
                    }
                }
            }

            public List<Empleado> ObtenerEmpleados()
            {
                List<Empleado> empleados = new List<Empleado>();
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Empleados";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string nombre = reader.GetString(1);
                                string apellido = reader.GetString(2);
                                int edad = reader.GetInt32(3);
                                string cargo = reader.GetString(4);
                                Empleado empleado = new Empleado(id, nombre, apellido, edad, cargo);
                                empleados.Add(empleado);
                            }
                        }
                    }
                }
                return empleados;
            }








        }


    
}
