using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace Countries.TableDataService.cs
{
    public partial class ContextServices<T>
    { 
        private string connectionString = ConfigurationManager
            .ConnectionStrings["appConnection"]
            .ConnectionString;
        public ContextServices()
        {

        }
        public List<object> GetAll()
        {
            Type itemType = typeof(T);
            PropertyInfo[] properties = itemType.GetProperties();
            object itemObject = Activator.CreateInstance(itemType);

            ConstructorInfo constructor = itemType.GetConstructor(new Type[] { });
            object itemExemplarObject;

            var data = new List<object>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = $"select * from {itemType.Name}s";
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        itemExemplarObject = constructor.Invoke(new object[] { });
                        foreach (var property in properties)
                        {

                            Type propertyType = property.PropertyType;
                            object obj = dataReader[$"{property.Name}"];
                            property.SetValue(itemExemplarObject, obj);
                        }
                        data.Add(itemExemplarObject);
                        itemExemplarObject = null;
                    }
                }
                #region catch
                catch (SqlException exeption)
                {
                    Console.WriteLine($"Insert exeption/n{exeption.ToString()}");
                }
                catch (Exception exeption)
                {
                    Console.WriteLine($"Not sql exeption{exeption.ToString()}");
                }
            }
            #endregion
            return data;
        }
    }
}
