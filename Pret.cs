using Microsoft.Win32.SafeHandles;

namespace Pretclass;

public class Pret 
{

static IDictionary<Monede, decimal> Curs = new Dictionary<Monede, decimal>(){
    };
    public enum Monede
    {
        LEU, EUR, USD
    }
    public Monede Moneda;
    public decimal Valoare;

    public Pret()
    {

    
    }

    public decimal ValoareCurs(Monede Moneda)
    {
        return Curs[Moneda];
    }
    
    
}