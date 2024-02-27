using Microsoft.Win32.SafeHandles;
using Pretclass;
using Producatorclass;

namespace Produsclass;

public class Produs
{
private Guid Id = Guid.NewGuid();
public string Name;
public Pret Pret;
private int _stoc;

public Producator Producator;

public int Stoc{
    get
    {
        return _stoc;
    }
    set
    {
    if (value > 0)
    {
        _stoc = value;
    }
    else{
        throw new ArgumentOutOfRangeException(nameof(Stoc), "Stoc must be greater thatn 0.");
    }
    }
}

public Produs(string name, Producator producator, Pret pret)
{
    //Id = id;
    this.Name = name;
    Pret = pret;
    Producator = producator;
    //Stoc = stoc;
}

}