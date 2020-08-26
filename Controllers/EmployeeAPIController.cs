using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
//using System.Web.Mvc;
using CRUDAPI.Models;

namespace CRUDAPI.Controllers
{
   [RoutePrefix("Api/Employee")]
    public class EmployeeAPIController : ApiController
    {        
        
        //List<EmployeeDetail> Employees = new List<EmployeeDetail>();

        //public EmployeeAPIController() { }

        //public EmployeeAPIController(List<EmployeeDetail> Employees)
        //{
        //    this.Employees = Employees;
        //}
        
        WebApiDbEntities objEntity = new WebApiDbEntities();

        [HttpGet]
        [Route("AllEmployeeDetails")]
        public IQueryable<EmployeeDetail> GetEmaployee()
        {
            try
            {               
                return objEntity.EmployeeDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployeeDetailsById/{employeeId}")]
        public IHttpActionResult GetEmployeeById(string employeeId)
        {
            EmployeeDetail objEmp = new EmployeeDetail();
            int ID = Convert.ToInt32(employeeId);
            try
            {
                 objEmp = objEntity.EmployeeDetails.Find(ID);
                if (objEmp == null)
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
           
            return Ok(objEmp);
        }

        [HttpPost]
        [Route("InsertEmployeeDetails")]
        public IHttpActionResult PostEmployee(EmployeeDetail data)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.EmployeeDetails.Add(data);
                objEntity.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }



            return Ok(data);
        }
        
        [HttpPut]
        [Route("UpdateEmployeeDetails")]
        public IHttpActionResult PutEmployeeMaster(EmployeeDetail employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                EmployeeDetail objEmp = new EmployeeDetail();
                objEmp = objEntity.EmployeeDetails.Find(employee.EmpId);
                if (objEmp != null)
                {
                    objEmp.EmpName = employee.EmpName;
                    objEmp.Address = employee.Address;
                    objEmp.Role    = employee.Role;
                    objEmp.DateOfBirth = employee.DateOfBirth;
                    objEmp.DateOfJoin = employee.DateOfJoin;
                    objEmp.Skills = employee.Skills;
                    objEmp.Dept = employee.Dept;
                    objEmp.IsActive = employee.IsActive;

                }
                int i = this.objEntity.SaveChanges();

            }
            catch(Exception)
            {
                throw;
            }
            return Ok(employee);
        }
        [HttpDelete]
        [Route("DeleteEmployeeDetails")]
        public IHttpActionResult DeleteEmployeeDelete(int id)
        {
            //int empId = Convert.ToInt32(id);
            EmployeeDetail Employee = objEntity.EmployeeDetails.Find(id);
            if (Employee == null)
            {
                return NotFound();
            }

            objEntity.EmployeeDetails.Remove(Employee);
            objEntity.SaveChanges();

            return Ok(Employee);
        }
    }

   
}
