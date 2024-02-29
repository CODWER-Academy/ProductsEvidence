using Produsclass;

namespace Reducereclass;
public class Reducere{
    public string Name;
    public DateTime StartData;
    public DateTime StopData;
    public Action<Produs> DiscountAction;
    
    public delegate void Aplica(Produs produs);
    private Aplica _aplica;
    //Primeste Produs //are usually stored as a private members
    //delegates are passed as parameters

    public Reducere(string name, DateTime startData, DateTime stopData, Action<Produs> discountAction){

        this.Name = name;
        this.StartData = startData;
        this.StopData = stopData;
        DiscountAction = discountAction;

    }
     public void ApplyDiscount(Produs product)
    {
        // This is where you apply the discount logic to the product.
        DiscountAction.Invoke(product);
    }
    }
