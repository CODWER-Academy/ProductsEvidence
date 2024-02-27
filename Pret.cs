using Microsoft.Win32.SafeHandles;

namespace Pretclass;

public class Pret 
{

public static IDictionary<Monede, decimal> Curs = new Dictionary<Monede, decimal>(){
    };
    public enum Monede
    {
        LEU, EUR, USD
    }
    public Monede Moneda;
    public decimal Valoare;
    public delegate decimal Deleg_UpdatePret();
    public event Deleg_UpdatePret User_UpdatePret;

    public Pret(decimal valoare, Monede moneda)
    {
        Valoare = valoare;
        Moneda = moneda;
    }

    public decimal ValoareCurs(Monede Moneda)
    {
        return Curs[Moneda];
    }
    
    public decimal OnUser_UpdatePret()
    {
    return 0.2m;
    }
    
}