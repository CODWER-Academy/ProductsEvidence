namespace Pretclass;

public class Pret 
{

public static IDictionary<Monede, decimal> Curs = new Dictionary<Monede, decimal>(){
    };
       public static decimal GetCurrencyRate(Monede monede) //face convsersie dintr-o valuta in alta
    {
        if (Curs.TryGetValue(monede, out var rate))
        {
            return rate;
        }
        else
        {
            throw new ArgumentException("Currency not supported or not defined in the dictionary.", nameof(monede));
        }
    }

    public static void InitializeCurrencies() //Stabilim cursul current valutar
    {
        Curs[Monede.LEU] = 1;
        Curs[Monede.EUR] = 19.47m;
        Curs[Monede.USD] = 17.30m;
    }

    public enum Monede
    {
        LEU, EUR, USD
    }
    public Monede Moneda;
    private decimal _valoare;
    public decimal Valoare
    {
        get {return _valoare;}
        set
        {
            if(_valoare != value)
            {
                decimal oldValoare = _valoare;
                _valoare = value;
                OnUpdate_Pret?.Invoke(oldValoare, value); //pentru a tine la curent subscriberii despre schimbarile preturilor
            }
        }
    }
    public delegate void Update_Pret(decimal oldPret, decimal newPret); //pentru a tine la curent subscriberii despre schimbarile preturilor
    public event Update_Pret OnUpdate_Pret;

   

    public Pret(decimal valoare, Monede moneda)
    {
        Valoare = valoare;
        Moneda = moneda;
    }

    protected virtual void InvokeUser_UpdatePret(Decimal oldPret, decimal newPret)
    {//invoke the event to notufy all subscibed methods about the change
    if (OnUpdate_Pret != null)
    {
        OnUpdate_Pret(oldPret, newPret);
    }
    }
}