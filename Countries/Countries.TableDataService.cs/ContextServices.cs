using System;
using System.Data;
using System.Data.SqlClient;

namespace Countries.TableDataService.cs
{
    public partial class ContextServices<T>
    {
        public void Add(T item)
        {
            string dbCommand = string.Empty;
            SqlTransaction transaction = null;
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();

                    transaction = connection.BeginTransaction();
                    command.Transaction = transaction;

                    foreach (var property in GetProperties(ref dbCommand))
                    {
                        command.CommandText = dbCommand;
                        if (property.Name != "Id")
                        {
                            var propertyParametr = new SqlParameter();

                            propertyParametr.ParameterName = $"@{property.Name}";

                            if (SqlTypeIdentifier(property.PropertyType) != SqlDbType.Xml)  //заглушка на случай если придет неизвестный тип
                                propertyParametr.SqlDbType = SqlTypeIdentifier(property.PropertyType);

                            propertyParametr.SqlValue = property.GetValue(item);
                            command.Parameters.Add(propertyParametr);
                        }

                    }
                    var affectedRows = command.ExecuteNonQuery();
                    if (affectedRows < 1) throw new Exception("No rows affected");

                    transaction.Commit();
                }
                catch (SqlException exeption)
                {
                    Console.WriteLine($"Insert exeption/n{exeption.ToString()}");
                }
                catch (Exception exeption)
                {
                    Console.WriteLine($"Not sql exeption{exeption.ToString()}");
                }
            }
        }
    }
}
