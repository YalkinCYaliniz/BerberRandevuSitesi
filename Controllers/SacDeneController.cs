using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BerberRandevuSitesi.Controllers
{
    public class SacDeneController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(string hairStyle, IFormFile uploadedPhoto)
        {
            if (uploadedPhoto == null || uploadedPhoto.Length == 0 || string.IsNullOrEmpty(hairStyle))
            {
                ViewBag.ErrorMessage = "Lütfen bir fotoğraf seçin ve saç modeli belirleyin!";
                return View();
            }

            using (var memoryStream = new MemoryStream())
            {
                await uploadedPhoto.CopyToAsync(memoryStream);
                var imageData = memoryStream.ToArray();

                var sacDeneModel = new SacDeneModel();
                var result = await sacDeneModel.GetStyledImage(hairStyle, imageData);

                if (result == null)
                {
                    ViewBag.ErrorMessage = "Saç modeli uygulama başarısız oldu. API'den geçerli bir yanıt alınamadı.";
                    return View();
                }

                ViewBag.StyledImageUrl = result;
                return View();
            }
        }
    }
}
