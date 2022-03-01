using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SPARosTelecom.Models;

namespace SPARosTelecom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //Получаем список сотрудников
        [HttpGet]
        public List<Personal> Get()
        {            
            List<Personal> PersList = HomeController.GetPers();
            return PersList;
        }
        //Получаем сотрудника

        [HttpGet("{id}")]
        public Personal GetPersonal(int id)
        {
            List<Personal> PersList = HomeController.GetPers();
            Personal personal = new Personal();
            foreach(var pers in PersList)
            {
                if (pers.Id == id)
                    personal = pers;
            }
            return personal;
        }
        // Запрос на совершение какого-либо действия act
        [HttpGet("{act}/{Body}")]
        public string Get(string act, string Body)
        {
            Personal pers = HomeController.ConvertToPers(Body);
            //Добавить
            if (act == "Add:")
                return HomeController.AddPers(pers);
            //Редактировать
            if (act == "Edit:")
                return HomeController.EditPers(pers);
            //Удалить
            if (act == "Delete:")
                return HomeController.DeletePers(pers);
            return "";
        }        
      
    // POST api/values
    [HttpPost]
        public void Post([FromBody] string Body)
        {
           
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
