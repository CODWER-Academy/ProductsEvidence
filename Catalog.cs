using Producatorclass;
using Produsclass;
using Reducereclass;

namespace Catalogclass;

public class Catalog
{
    public List<Produs> Produse;
    public DateTime PerioadaStart;
    public DateTime PerioadaStop;
    public List<Reducere> Reduceri;
    public List<Producator> Producatori; //pentru ex 1b
    
//In pofida la conditia initiala despre declararea clasei Catalog,
// sunt nevoia sa mai adaug Producator ca sa indeplinesc conditia 1b
    public Catalog(DateTime perioadaStart, DateTime perioadaStop, List<Producator> producatori, List<Produs> produse)
    {
        Produse = produse;
        PerioadaStart = perioadaStart;
        PerioadaStop = perioadaStop;
        Producatori = producatori;
    }

}