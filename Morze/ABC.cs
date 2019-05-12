namespace Morze
{
    public class ABC
    {
        public string mKod { get; set; }
        public char betu { get; set; }

        public ABC(string sor)
        {
            string[] t = sor.Split('\t');
            this.betu = char.Parse(t[0]);
            this.mKod = t[1];
            
        }
        public override string ToString()
        {
            return betu+" --> "+mKod;
        }
    }
}