using Produsclass;

namespace Reducereclass;
public class Reducere{
    public string Name;
    public DateTime StartData;
    public DateTime StopData;
    public Action<Produs> DiscountAction;
    
    private delegate void Aplica(Produs produs);
    private Aplica _aplica;

    public Reducere(string name, DateTime startData, DateTime stopData, Action<Produs> discountAction)
    {
        this.Name = name;
        this.StartData = startData;
        this.StopData = stopData;
        DiscountAction = discountAction;

    }
    
      public void AplicaReducere(Produs product)
    {
        // This is where you apply the discount logic to the product.
        DiscountAction.Invoke(product);
    }
    
    //reducerile existente:
    public static void SummerSaleDiscount(Produs produs)
        {
            produs.Pret.Valoare -= produs.Pret.Valoare * 0.10m;
        }

        public static void WinterSaleDiscount(Produs produs)
        {
            produs.Pret.Valoare -= produs.Pret.Valoare * 0.20m;
        }

        public static void EasterSaleDiscount(Produs produs)
        {
            produs.Pret.Valoare -= produs.Pret.Valoare * 0.15m;
        }

        public static void EightMarchDiscount(Produs produs)
        {
            produs.Pret.Valoare -= produs.Pret.Valoare * 0.10m;
        }
    }
