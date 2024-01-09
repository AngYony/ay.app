namespace Demo
{
    public class Wy
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public IEnumerable<object> GetWy()
        {
            yield return Name;
            yield return Age;
        }
    }
}
