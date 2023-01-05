using GameStore_MVC.data;
using Microsoft.AspNetCore.Mvc;

namespace GameStore_MVC.Controllers
{
    public class GameController : Controller
    {
        private readonly UygulamaDbContext _db;

        public GameController(UygulamaDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var list = _db.Games.ToList();
            return View(list);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Add(Game game)
        {
            try
            {
                Random rnd = new Random();
                string first = rnd.Next(100000, 999999).ToString();
                string second = rnd.Next(1000000, 9999999).ToString();
                game.BarcodeNo = Convert.ToInt64(first + second);

                while (_db.Games.ToList().Contains(_db.Games.FirstOrDefault(x => x.BarcodeNo == game.BarcodeNo)))
                {
                     first = rnd.Next(100000, 999999).ToString();
                     second = rnd.Next(1000000, 9999999).ToString();
                    game.BarcodeNo = Convert.ToInt64(first + second);
                }

                if (game.Price <= 0)
                    throw new Exception("Format Exception!");
                if (_db.Games.FirstOrDefault(x => x.Name == game.Name)?.onePlatform == true)
                {
                    throw new Exception("If Game is only for one platform you cannot add the same game again!");
                }

                if (_db.Games.FirstOrDefault(u => u.Name == game.Name) != null)
                    throw new Exception("The product that you want to add is already added to the list!");

                _db.Games.Add(game);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                TempData["Durum"] = "Error! " + ex.Message;
                return View();
            }
            //Ekledikten sonra yine ürün listesine git
            TempData["Durum"] = "Successfull!";
            return RedirectToAction("Index");
        }
        public IActionResult Update(int Id)
        {
            Game updatingGame = _db.Games.Find(Id);

            return View(updatingGame);
        }
        [HttpPost]
        public IActionResult Update(Game game)
        {
            try
            {
                //db'ye güncelleme olacak
                if (game.Price <= 0)
                    throw new Exception("Format Exception!");

                var otherProducts = _db.Games.Except(_db.Games.Where(u => u.Id == game.Id)).ToList();


                if (otherProducts.Any(u => u.Name == game.Name))
                    throw new Exception("The product name that you want to update belongs to another product!");

                _db.Games.Update(game);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["Durum"] = "Error! " + ex.Message;
                return View();
            }
            //Ekledikten sonra yine ürün listesine git
            TempData["Durum"] = "Successfully Updated!";
            return RedirectToAction("Index");

        }
        public IActionResult Delete(int Id)
        {
            var deletingGame = _db.Games.Find(Id);
            return View(deletingGame);
        }
        [HttpPost]
        public IActionResult Delete(Game game)
        {
            try
            {
                _db.Games.Remove(game);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["Durum"] = "Error! " + ex.Message;
                return View();
            }
            TempData["Durum"] = "Successfully Deleted!";
            return RedirectToAction("Index");
        }
    }
}
