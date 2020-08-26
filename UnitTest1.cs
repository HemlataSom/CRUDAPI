using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using CRUDAPI.Controllers;
using CRUDAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrudApiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PostSetsLocationHeader();
        }
     
        [TestMethod]
        public void GetEmp_ShouldNotFindEmp()
        {
            var controller = new EmployeeAPIController(GetTestEmployees());
            var result = controller.GetEmployeeById("1");

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        private List<EmployeeDetail> GetTestEmployees()
        {
            var testEmp = new List<EmployeeDetail>();
            DateTime dt = DateTime.Parse("1978-10-10");
            DateTime dt1 = DateTime.Parse("2020-1-10");
            testEmp.Add(new EmployeeDetail { EmpId = 1, EmpName = "Demo1", Address = "1234 Main St",DateOfBirth= dt,DateOfJoin=dt1,Dept = "IT", IsActive=true,Role= "Software Developer" , Skills= "Dot Net"});
          
            return testEmp;
        }
        [TestMethod]
        public void PostSetsLocationHeader()
        {
            // Arrange
            EmployeeAPIController controller = new EmployeeAPIController();

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("https://crudapi20200824043722.azurewebsites.net/Api/Employee/GetEmployeeDetailsById/12")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "GetEmployeeDetailsById" } });

          
            var response = controller.GetEmployeeById("12");
            // Act
            //Product product = new Product() { Id = 42, Name = "Product1" };
            //var response = controller.Post(product);

            // Assert
            Assert.IsNotNull(response);
           // Assert.AreEqual(testProducts[3].Name, response..Content.Name);
           // Assert.AreEqual("https://crudapi20200824043722.azurewebsites.net/Api/Employee/GetEmployeeDetailsById/12", response..Headers.Location.AbsoluteUri);
        }
    }
}
