using MySql.Data.MySqlClient;
using SPARosTelecom.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SPARosTelecom.Controllers
{
    public class HomeController
    {
        public static string connectionString;
        static string sqlExpression = "SELECT * FROM Personals";
        //Получить список сотрудников
        public static List<Personal> GetPers()
        {
            List<Personal> PersList = new List<Personal>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                Personal pers = new Personal();
                                pers.Id = Convert.ToInt32(reader.GetValue(0));
                                pers.Name = Convert.ToString(reader.GetValue(1));
                                pers.SurName = Convert.ToString(reader.GetValue(2));
                                pers.DateOfBirth = Convert.ToDateTime(reader.GetValue(3)).ToShortDateString();
                                pers.Position = Convert.ToString(reader.GetValue(4));
                                pers.Salary = Convert.ToInt32(reader.GetValue(5));
                                pers.DateOfEmployed = Convert.ToDateTime(reader.GetValue(6)).ToShortDateString();
                                PersList.Add(pers);
                            }
                            catch { }
                        }
                    }

                    reader.Close();
                }
            }
            catch
            {
                return null;
            }
            return PersList;
        }
        //Добавить Нового Сотрудника
        public static string AddPers(Personal pers)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                    DataSet ds = new DataSet();
                    // Заполняем Dataset
                    adapter.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    DataRow newRow = dt.NewRow();
                    newRow["Имя"] = pers.Name;
                    newRow["Фамилия"] = pers.SurName;
                    newRow["Дата рождения"] = pers.DateOfBirth;
                    newRow["Должность"] = pers.Position;
                    newRow["Зарплата"] = pers.Salary;
                    newRow["Дата устройства в компанию"] = pers.DateOfEmployed;
                    dt.Rows.Add(newRow);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.Update(ds);
                    ds.Clear();
                    adapter.Fill(ds);
                }
                return "Сотрудник добавлен";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        //Редактировать данные о сотруднике
        public static string EditPers(Personal pers)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = $"UPDATE Personals SET Имя = N'{pers.Name}', Фамилия = N'{pers.SurName}', [Дата рождения] = '{pers.DateOfBirth} ', Должность = N'{pers.Position}', Зарплата = '{pers.Salary}', [Дата устройства в компанию] = '{pers.DateOfEmployed}' WHERE Id = '{pers.Id}' ";

                    using (SqlCommand com = new SqlCommand(queryString, connection))
                    {
                        com.ExecuteNonQuery();
                    }
                }
                return "Изменения внесены";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //Удалить сотрудника
        public static string DeletePers(Personal pers)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = $"DELETE FROM Personals WHERE Id = '{pers.Id}'";
                    using (SqlCommand com = new SqlCommand(queryString, connection))
                    {
                        com.ExecuteNonQuery();
                    }
                }
                return "Сотрудник удален";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //Возвращаем модель сотрудника из тела запроса
        public static Personal ConvertToPers(string Body)
        {
            var parametrs = Body.Split('&');
            try
            {
                Personal pers = new Personal();
                pers.Id = Convert.ToInt32(parametrs[0]);
                pers.Name = $@"{parametrs[1]}";
                pers.SurName = parametrs[2];
                pers.DateOfBirth = parametrs[3];
                pers.Position = parametrs[4];
                pers.Salary = Convert.ToInt32(parametrs[5]);
                pers.DateOfEmployed = parametrs[6];
                return pers;
            }
            catch { return null; }
        }
    }
}
