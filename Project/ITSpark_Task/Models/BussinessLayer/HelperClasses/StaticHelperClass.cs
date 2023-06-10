namespace ITSpark_Task.Models.BussinessLayer.HelperClasses
{
   public enum Cities
    {
        Cairo,
        Alexandria,
        Monofia,
        Banha,
        Assyout,
        Essmailia
    }
    public class StaticHelperClass
    {
        public static Cities StudentCities { get; set; } = new Cities();
    }
}
