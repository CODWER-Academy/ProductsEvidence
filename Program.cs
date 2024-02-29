using System.Collections.Concurrent;
using System.Configuration.Assemblies;
using Catalogclass;
using Clientclass;
using Pretclass;
using Producatorclass;
using Produsclass;
using Reducereclass;
using ExtensionMethdDemo;
using System.ComponentModel.Design;

public class Program
{
    public static void Main()
    {
        Pret.InitializeCurrencies();
        
        List<Producator> producatori = new List<Producator>();

        Producator clothesProd = new Producator("Clothes_prod", new List<Reducere> {
                new Reducere("Summer Sale", new DateTime(2024, 03, 06), new DateTime(2024, 03, 07), Produs => Produs.Pret.Valoare -= Produs.Pret.Valoare * 0.10m),//maybe the discount logic will be hidden in the reducere class?
                new Reducere("Winter Sale", new DateTime(2024, 12, 01), new DateTime(2024, 01, 20), Produs =>Produs.Pret.Valoare -= Produs.Pret.Valoare * 0.20m)
                });
        
        Producator shoesProd = new Producator("Shoes_prod", new List<Reducere> {
                new Reducere("Easter Sale", new DateTime(2024, 04, 20), new DateTime(2024, 05, 11), Produs => Produs.Pret.Valoare -= Produs.Pret.Valoare * 0.15m)
                });
        Producator jeweleryProd = new Producator("Jewelery_prod", new List<Reducere>{
                new Reducere("Eight March", new DateTime(2024, 03, 06), new DateTime(2024, 03, 10), Produs => Produs.Pret.Valoare -= Produs.Pret.Valoare * 0.10m)  
        });


        producatori.Add(clothesProd);
        producatori.Add(shoesProd);
        producatori.Add(jeweleryProd);

        ////////////////////
        List<Produs> produse = new List<Produs>();
        Produs ciorapi = new Produs("Ciorapi", clothesProd, new Pret(10, Pret.Monede.LEU));
        ciorapi.Stoc = 0;
        Produs botine = new Produs("Botine", shoesProd, new Pret(130, Pret.Monede.USD));
        botine.Stoc = 20;
        Produs cercei = new Produs("Cercei", jeweleryProd, new Pret(400, Pret.Monede.EUR));
        cercei.Stoc = 10;
        //tricou NOT in catalog si nu e in stoc
        Produs tricou = new Produs("Tricou", clothesProd, new Pret(5, Pret.Monede.EUR));
        tricou.Stoc = 0;
        
        produse.Add(ciorapi);
        produse.Add(botine);
        produse.Add(cercei);       

        cercei.Pret.Valoare = 300; 
        
        ///////////////////
        List<Client> clienti = new List<Client>();

        Client client1 = new Client(new List<Guid> {tricou.Id, ciorapi.Id} );
        Client client2 = new Client(new List<Guid> {cercei.Id, ciorapi.Id} );
        Client client3 = new Client(new List<Guid> {tricou.Id, botine.Id, cercei.Id} );

        clienti.Add(client1);
        clienti.Add(client2);
        clienti.Add(client3);

       //////////////////////
        Catalog catalog = new Catalog(new DateTime(2024, 3, 04), new DateTime(2024, 04, 19), producatori, new List<Produs>());
        foreach (var produs in produse)
        {
                catalog.AddProdus(produs);
        }


        foreach (var client in clienti)
        {
        client.ClearInbox();
        }

        catalog.SubcsribeToUpdatePret(client1);
        catalog.SubcsribeToUpdatePret(client2);
        catalog.SubcsribeToUpdatePret(client3);

/////ex 7
catalog.SetDelegatreducere(catalog.selectedDiscount);

        // Conditional compilation directive for DEBUG mode
        #if DEBUG
        // In DEBUG mode, use the selected discount
        catalog.AplicaReduceri(catalog.delegatReducereInstance);
        #else
        // In RELEASE mode, apply all discounts
        catalog.AplicaReduceri();
        #endif

    /////////////////////start testing the discount applying
 Console.WriteLine("Before applying discounts:");
    foreach (var produs in produse)
    {
        Console.WriteLine($"Product: {produs.Name}, Price: {produs.Pret.Valoare}, Stock: {produs.Stoc}");
    }
catalog.AplicaReduceriProducator();

    // After applying discounts
    Console.WriteLine("\nAfter applying discounts:");
    foreach (var produs in produse)
    {
        Console.WriteLine($"Product: {produs.Name}, Price: {produs.Pret.Valoare}, Stock: {produs.Stoc}");
    }

    // Check if discounts were applied correctly and stock was incremented
    bool areDiscountsCorrect = true; // Assume true until proven otherwise
    foreach (var produs in produse)
    {
        decimal pretInEuro = Pret.GetCurrencyRate(produs.Pret.Moneda) * produs.Pret.Valoare;
        if (pretInEuro < 10 && produs.Stoc != 100)
        {
            Console.WriteLine($"Error: Stock for {produs.Name} was not incremented correctly.");
            areDiscountsCorrect = false;
        }
    }

    if (areDiscountsCorrect)
    {
        Console.WriteLine("All discounts were applied correctly.");
    }
    else
    {
        Console.WriteLine("There were errors in applying discounts.");
    }
    /////ex 8 start//////
    

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
}



