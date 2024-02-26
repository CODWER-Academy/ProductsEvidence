using Microsoft.Win32.SafeHandles;
using Pretclass;
using Producatorclass;

namespace Produsclass;

public class Produs
{
private Guid id = Guid.NewGuid();
public string Name;
public Pret Pret;

//Stoc: intreg > 0

public Producator Producator;

public Produs(string name, Producator producator, Pret pret)
{
    this.Name = name;
    Pret = pret;
    Producator = producator;
}

}