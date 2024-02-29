using Clientclass;
using ExtensionMethdDemo;
using Pretclass;
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
   private Dictionary<Produs, Dictionary<Client, Pret.Update_Pret>> subscribedClientsHandlers;//dictionar pentru a tine cont de produsele care isi schimba pretul

    public delegate void delegatReducere(Produs produs);
    public delegatReducere selectedDiscount = (produs) => //pentru a creea o reducere specifica la anumite produse
    {
     produs.Pret.Valoare -= produs.Pret.Valoare * 0.50m;
    };
    public delegatReducere delegatReducereInstance;
    public Catalog(DateTime perioadaStart, DateTime perioadaStop, List<Producator> producatori, List<Produs> produse)
    {
        Produse = produse;
        PerioadaStart = perioadaStart;
        PerioadaStop = perioadaStop;
        Producatori = producatori;
        subscribedClientsHandlers = new Dictionary<Produs, Dictionary<Client, Pret.Update_Pret>>();
    foreach (var produs in Produse)
    {
        subscribedClientsHandlers[produs] = new Dictionary<Client, Pret.Update_Pret>();
    }

    }

public void SubcsribeToUpdatePret(Client client) //metoda publica prin care clientii se pot abona la respectiv-ul catalog
{
    Console.WriteLine($"Subscribing {client.Email} to updates...");
    
    foreach (var produs in Produse)
    {
        // Check if the client is already subscribed to updates for this product
        if (subscribedClientsHandlers.TryGetValue(produs, out var clientHandlers) && clientHandlers.ContainsKey(client))
        {
            // If already subscribed, skip to the next product
            continue;
        }

        if (!subscribedClientsHandlers.TryGetValue(produs, out clientHandlers))
        {
            clientHandlers = new Dictionary<Client, Pret.Update_Pret>();
            subscribedClientsHandlers[produs] = clientHandlers;
        }

        // Create the update handler
        Pret.Update_Pret handler = (oldPret, newPret) =>
        {
            if (client.ProduseFavorite.Contains(produs.Id))
            {
                decimal oldPriceInClientCurrency = oldPret * Pret.GetCurrencyRate(produs.Pret.Moneda);
                decimal newPriceInClientCurrency = newPret * Pret.GetCurrencyRate(produs.Pret.Moneda);
                var mesaj = $"Pret-ul produsului {produs.Name} s-a schimbat de la {oldPriceInClientCurrency} la {newPriceInClientCurrency} in {produs.Pret.Moneda}";
                client.Notifica(mesaj);
            }
        };

        // Subscribe to the event
        produs.Pret.OnUpdate_Pret += handler;
        // Update the handler reference in the dictionary
        clientHandlers[client] = handler;
    }
}


public void AplicaReduceriProducator(delegatReducere delegatSpecific = null)
//itereaza prin lista de produse si pentru fiecare produs aplica toate reducerile
//producatorului care se incadreaza in perioada catalogului
{
    foreach (var produs in Produse)
    {
        if (delegatSpecific != null)
        {
            //Apply the specific discount using the delegate
            delegatSpecific(produs);
        }
        else
        {
            //No specific delegate provided, apply all discounts that are within the catalog period
            foreach (var reducere in produs.Producator.Reduceri)
            {
                if (reducere.StartData.IsInRange(this.PerioadaStart, this.PerioadaStop))
                {
                    //Aplica discountul pentru produse
                    reducere.AplicaReducere(produs);
                }
            }
        }

        // pentru produsele cu stoc-ul 0 si pretul redus sub 10 euro stocul va fi incrementat cu 100 de bucati
        decimal pretInEuro = Pret.GetCurrencyRate(produs.Pret.Moneda) * produs.Pret.Valoare;
        if (produs.Stoc == 0 && pretInEuro < 10)
        {
            produs.Stoc += 100; 
        }
    }
}

    public void AplicaReduceri(delegatReducere delegatGeneric = null)
    //metoda care primeste optional un delegat generic ce permite selectarea unei reduceri 
    //din cele disponibile in catalog
{
    if (delegatGeneric != null)
    {
        foreach(var produs in Produse)
        {
            delegatGeneric(produs);
            // pentru produsele cu stoc-ul 0 si pretul redus sub 10 euro stocul va fi incrementat cu 100 de bucati
            decimal pretInEuro = Pret.GetCurrencyRate(produs.Pret.Moneda) * produs.Pret.Valoare;
            if (produs.Stoc == 0 && pretInEuro < 10)
            {
                produs.Stoc += 100;
            }
        }
    }
    else
    {
        AplicaReduceriProducator(); //this method should handle the stock check internally
    }
}

    public void SetDelegatreducere(delegatReducere newDelegatReducere){ //to set specific discount for specific products
        delegatReducereInstance = newDelegatReducere;
    }

    public void UnsubscribeToUpdatePret(Client client)  
    //o metoda publica prin care clientii se pot dezabona de la respectivul catalog (in cazul in
//care un client se dezaboneaza si nu se regaseste in lista de abonati, o exceptie trebuie aruncata)
{
    bool isSubscribed = false;
    foreach (var pair in subscribedClientsHandlers)
    {
        if (pair.Value.TryGetValue(client, out Pret.Update_Pret handler))
        {
            // Unsubscribe using the stored delegate
            pair.Key.Pret.OnUpdate_Pret -= handler;
            pair.Value.Remove(client);
            isSubscribed = true;
            Console.WriteLine("You are unsubscribed from updates for " + pair.Key.Name);
        }
    }

    if (!isSubscribed)
    {
        throw new InvalidOperationException("Client is not subscribed to the catalog");
    }
}

    public void NotifyStockChange(Produs produsModificat)
{
    // Metoda publica care poate fi apelata pentru a urmari schimbarile de stoc in Produs class
    NotificaModificariStoc(produsModificat);
}

 public void AddProdus(Produs produs)
{
    produs.StockChanged -= HandleStockChanged; //Unsubscribe first to prevent multiple subscriptions
    produs.StockChanged += HandleStockChanged; //csubscribe to the event
    Produse.Add(produs);
    if (!subscribedClientsHandlers.ContainsKey(produs))
    {
        subscribedClientsHandlers[produs] = new Dictionary<Client, Pret.Update_Pret>();
    }
}

    private void HandleStockChanged(Produs produs)
    {
        NotificaModificariStoc(produs);
    }


    private void NotificaModificariStoc(Produs produsModificat){ //metoda privata care incapsuleaza logica pentru notifcicarea clientilor
    //despre schimbarile de stoc
    // Check if the stock was 0 before and now is greater than 0
    if (produsModificat.GetStocAnterior() == 0 && produsModificat.Stoc > 0)
    {
        if (subscribedClientsHandlers.TryGetValue(produsModificat, out Dictionary<Client, Pret.Update_Pret> clientHandlers))
        {
            foreach (var clientHandler in clientHandlers)
            {
                Client client = clientHandler.Key;
                // Check if the product is in the client's list of favorite products
                if (client.ProduseFavorite.Contains(produsModificat.Id))
                {
                    var mesaj = $"Produsul {produsModificat.Name} este acum disponibil in stoc!";
                    client.Notifica(mesaj);
                }
            }
        }
    }
}
 public void SetDelegatReducere(delegatReducere newDelegatReducere) //metoda pentru a aplica discountul la cazuri specifice
    {
        delegatReducereInstance = newDelegatReducere;
    }
}


