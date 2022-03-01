using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPARosTelecom.Models
{
    public class Personal
    {        
        public int Id { get; set; }

        //Имя
        public string Name { get; set; }

        //Фамилия
        public string SurName { get; set; }

        //Дата рождения
        public string DateOfBirth { get; set; }

        //Должность
        public string Position { get; set; }

        //Зарплата
        public int Salary { get; set; }

        //Дата устройства в компанию
        public string DateOfEmployed { get; set; }
    }
}
