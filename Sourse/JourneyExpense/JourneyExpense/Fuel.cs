namespace JourneyExpense
{
    class Fuel
    {
        public string name { get; set; }
        public string octaneNumber { get; set; }
        public double price { get; set; }
        public Fuel() { }
        public Fuel(string name,string octaneNumber, double price)
        {
            this.name = name;
            this.octaneNumber = octaneNumber;
            this.price = price;
        }
    }
}
