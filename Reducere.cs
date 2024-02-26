using Produsclass;

namespace Reducereclass;
public class Reducere{
    public string Name;
    public DateTime Data;
    //public Produs Produs;
    
    public delegate void Aplica(Produs produs);
    private Aplica _aplica;
    //Primeste Produs //are usually stored as a private members
    //delegates are passed as parameters

    public Reducere(string name, DateTime data, Aplica aplica){

       _aplica = aplica;
        this.Name = name;
        this.Data = data;

    }
    public void ApplyDiscount(Produs produs) //to execute the delegate on a Produs object
    {
        _aplica(produs);
    }

}