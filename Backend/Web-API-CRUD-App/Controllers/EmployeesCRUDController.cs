using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http.Cors;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Web_API_CRUD_App.Models;

namespace Web_API_CRUD_App.Controllers
{
    [EnableCors(origins: "http://localhost:51728", headers: "*", methods: "*")]
    public class EmployeesCRUDController : ApiController
    {
        private EmployeeEntities db = new EmployeeEntities();
        [ResponseType(typeof(IEnumerable<Employee>))]
        [System.Web.Http.Route("api/GetEmployees")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetEmployees()
        {
            var result = db.Employees.ToList();
            return GetResultResponse(result);
        }
      
        /// <remarks>  
        /// Get Employee Details based on empid  
        /// </remarks>  
       
        [ResponseType(typeof(IEnumerable<Employee>))]
        [System.Web.Http.Route("api/GetEmployee")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetEmployee(int employeeid)
        {
            var result = db.Employees.Where(a => a.EmployeeID == employeeid).ToList();
            return GetResultResponse(result);
        }
       
        /// <remarks>  
        /// Create new employee and return inserted employee details  
        /// </remarks>  
     
        [ResponseType(typeof(IEnumerable<Employee>))]
        [System.Web.Http.Route("api/AddEmployee")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddEmployee(Employee employee)
        {
            var result = db.Employees.Add(employee);
            db.SaveChanges();
            return GetResultResponse(result);
        }
    
        /// <remarks>  
        /// Update Employee Details Based on empid  
        /// </remarks>  
      
        [ResponseType(typeof(IEnumerable<Employee>))]
        [System.Web.Http.Route("api/UpdateEmployee")]
        [System.Web.Http.HttpPut]
        public HttpResponseMessage UpdateEmployee(Employee employee)
        {
            Employee result = db.Employees.Where(a => a.EmployeeID == employee.EmployeeID).FirstOrDefault();
            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
               
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
            }
            return GetResultResponse(result);
        }
      
        /// <remarks>  
        /// Delete Employee record based on empid  
        /// </remarks>  
      
        [ResponseType(typeof(IEnumerable<Employee>))]
        [System.Web.Http.Route("api/DeleteEmployee")]
        [System.Web.Http.HttpDelete]
        public void DeleteEmployee(Employee employee)
        {
            Employee result = db.Employees.Where(a => a.EmployeeID == employee.EmployeeID).FirstOrDefault();
            if (result != null)
            {
                db.Entry(result).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
        /// <summary>  
        /// Get Response for Each result  
        /// </summary>  
     
        public HttpResponseMessage GetResultResponse(object Result)
        {
            HttpResponseMessage response = null;
            try
            {
                response = Request.CreateResponse(HttpStatusCode.OK, Result);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, Result);
            }
            return response;
        }
    }
}