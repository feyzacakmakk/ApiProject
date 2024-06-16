using HotelProject.WebUI.Models.Staff;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace HotelProject.WebUI.Controllers
{
    public class StaffController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StaffController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client=_httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7034/api/Staff/");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                //responsemessage dan gelen contenti yani içeriği string olarak oku
                var values=JsonConvert.DeserializeObject<List<StaffViewModel>>(jsonData);
                return View(values);
            }
            return View();
        }


        [HttpGet]
        public IActionResult AddStaff()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff(AddStaffViewModel addStaffViewModel)
        {
            var client=_httpClientFactory.CreateClient();
            var jsonData=JsonConvert.SerializeObject(addStaffViewModel);
            //biz bir data gönderiyoruz biz bunu apiye gönderirken onu parametreden gelen değeri json a dönüştürücez
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //stringcontent benim içeriğimizin dönüşümü için kullanacağım sınıf, utf8 türkçe karakteri destekleyecek şekilde getiriyo
            var responseMessage = await client.PostAsync("https://localhost:7034/api/Staff/", stringContent);
            //ekleme için postasync kullanıyoruz
            //stringcontent nesnesinin içinde datam ve datanın kodlanmış hali ve türü bulunuyor
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7034/api/Staff/{id}");
            //silme işlemi için deleteasync
            //$ işareti oraya id geleceğini parametre geleceğini belirtiyo
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStaff(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7034/api/Staff/{id}");
            //getasync ile id ye göre veri getirme işlemi yapıyoruz
            //$ işareti oraya id geleceğini parametre geleceğini belirtiyo
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData=await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateStaffViewModel>(jsonData);
                return View(values);
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateStaff(UpdateStaffViewModel updateStaffViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateStaffViewModel);
            StringContent stringContent=new StringContent(jsonData,Encoding.UTF8,"application/json");
            var responseMessage = await client.PutAsync("https://localhost:7034/api/Staff/",stringContent);
            //putasync ile güncelleme işlemi yapıyoruz
            
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
