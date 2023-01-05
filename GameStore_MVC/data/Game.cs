namespace GameStore_MVC.data
{
    public class Game
    {
        public Game()
        {
            
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string Platform { get; set; } = null!;
        public long BarcodeNo { get; set; }

        public bool onePlatform { get; set; }
    }
}
