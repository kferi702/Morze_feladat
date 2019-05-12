namespace Morze
{
    public class Idezet
    {
        public string szerzo { get; set; }
        public string idezet { get; set; }

        public Idezet(string sor)
        {
            string[] t= sor.Split(':');
            this.szerzo = t[0];
            this.idezet = t[1];
            if (t.Length == 3)
                this.idezet += ":"+t[2];
        }
        public override string ToString()
        {
            return szerzo+" "+idezet;
        }
    }
}