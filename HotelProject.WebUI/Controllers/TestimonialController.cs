using HotelProject.WebUI.Models.Staff;
using HotelProject.WebUI.Models.Testimonial;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelProject.WebUI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7034/api/Testimonial/");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                //responsemessage dan gelen contenti yani içeriği string olarak oku
                var values = JsonConvert.DeserializeObject<List<TestimonialViewModel>>(jsonData);
                return View(values);
            }
            return View();
        }


        [HttpGet]
        public IActionResult AddTestimonail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTestimonial(TestimonialViewModel testimonialViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(testimonialViewModel);
            //biz bir data gönderiyoruz biz bunu apiye gönderirken onu parametreden gelen değeri json a dönüştürücez
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            //stringcontent benim içeriğimizin dönüşümü için kullanacağım sınıf, utf8 türkçe karakteri destekleyecek şekilde getiriyo
            var responseMessage = await client.PostAsync("https://localhost:7034/api/Testimonial/", stringContent);
            //ekleme için postasync kullanıyoruz
            //stringcontent nesnesinin içinde datam ve datanın kodlanmış hali ve türü bulunuyor
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7034/api/Testimonial/{id}");
            //silme işlemi için deleteasync
            //$ işareti oraya id geleceğini parametre geleceğini belirtiyo
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7034/api/Testimonial/{id}");
            //getasync ile id ye göre veri getirme işlemi yapıyoruz
            //$ işareti oraya id geleceğini parametre geleceğini belirtiyo
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateStaffViewModel>(jsonData);
                return View(values);
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(TestimonialViewModel testimonialViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(testimonialViewModel);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7034/api/Testimonial/", stringContent);
            //putasync ile güncelleme işlemi yapıyoruz

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}
