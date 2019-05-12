namespace Morze
{
    public class MorzeKod
    {
        public string szerzo { get; set; }
        public string idezet { get; set; }

        public MorzeKod(string sor)
        {
            string[] t = sor.Split(';');
            this.szerzo = t[0];
            this.idezet = t[1];
        }
        public override string ToString()
        {
            return szerzo+"   "+idezet;
        }

    }
}