using Catalogclass;

using Pretclass;
using Producatorclass;

namespace Produsclass;

public class Produs
{
public readonly Guid Id = Guid.NewGuid();
public string Name;
public Pret Pret;
private int _stoc;
private int _stocAnterior; //contine valoarea anterioara a stocului

public Producator Producator;
public Catalog catalog;
public delegate void StockChangeHandler(Produs produs);
public event StockChangeHandler StockChanged;

 public int Stoc
{
    get => _stoc;
    set
    {
        // Before updating the stock, save the current value to _stocAnterior
        if(_stoc != value) // Check if the value is actually different
        {
            Console.WriteLine($"Stock for {Name} is changing from {_stoc} to {value}.");
            _stocAnterior = _stoc;
            _stoc = value;

            // After the stock is updated, if the new value is greater than 0 and the old value was 0,
            // raise the StockChanged event.
            if (_stoc > 0 && _stocAnterior == 0)
            {
                Console.WriteLine($"Stock for {Name} was 0 and is now greater than 0. Raising StockChanged event.");
                StockChanged?.Invoke(this);
            }
        }
    }
}

public Produs(string name, Producator producator, Pret pret)
{
//proprietatea ProduseFavorite trebuie sa contina id-uri care se pot regasi in catalog sau nu
    this.Name = name;
    Pret = pret;
    Producator = producator;
}

public int GetStocAnterior(){
    return _stocAnterior;
}
}