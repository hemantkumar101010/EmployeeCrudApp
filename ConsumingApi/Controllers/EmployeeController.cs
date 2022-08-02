using ConsumingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumingApi.Controllers
{
    public class EmployeeController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7261/api");
        HttpClient client;

        public EmployeeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Get()
        {
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();

            HttpResponseMessage responseMessage = client.GetAsync(baseAddress + "/Employees").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                employeeList = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);
            }
            return View(employeeList);

        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(EmployeeViewModel employeeViewModel)
        {
            var postTask = client.PostAsJsonAsync<EmployeeViewModel>(baseAddress + "/Employees", employeeViewModel);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Get");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(employeeViewModel);
        }
      

    

        //[HttpPost]
        //public ActionResult Create(EmployeeViewModel employeeViewModel)
        //{

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:7261/api/Employees");
        //        //HTTP POST 
        //        var postTask = client.PostAsJsonAsync<EmployeeViewModel>("Employees", employeeViewModel);
        //        postTask.Wait();
        //        var result = postTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Get");
        //        }
        //    }
        //    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
        //    return View(employeeViewModel);
        //}


        public ActionResult Update(int id)
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            HttpResponseMessage responseMessage = client.GetAsync(baseAddress + "/Employees/" + id.ToString()).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<EmployeeViewModel>(data);
            }
            return View(employee);
        }

    
        public ActionResult UpdateEmp(EmployeeViewModel employeeViewModel)
        {
            var putTask = client.PutAsJsonAsync<EmployeeViewModel>(baseAddress + "/Employees/" + employeeViewModel.Id.ToString(), employeeViewModel);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Get");
            }
            return View(employeeViewModel);
        }
       
        public ActionResult Delete(int id)
        {

                //HTTP DELETE
                var deleteTask = client.DeleteAsync(baseAddress + "/Employees/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {

                return RedirectToAction("Get");
            }

            return RedirectToAction("Delete");
        }

    }
}
