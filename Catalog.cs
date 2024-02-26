using Produsclass;
using Reducereclass;

namespace Catalogclass;

public class Catalog
{
    public List<Produs> Produse;
    public DateTime PerioadaStart;
    public DateTime PerioadaStop;
    public Reducere Reducere;
    public List<Reducere> Reduceri;
    

    public Catalog()
    {
        Produse = new List<Produs>();
        Reduceri = new List<Reducere>();
    }

}