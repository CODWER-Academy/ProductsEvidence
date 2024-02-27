using System.Collections.Concurrent;
using System.Configuration.Assemblies;
using Catalogclass;
using Clientclass;
using Pretclass;
using Producatorclass;
using Produsclass;
using Reducereclass;

public class Program
{
    public static void Main()
    {
        List<Produs> produse = new List<Produs>
        {
    
        };
        List<Producator> producatori = new List<Producator>();

        Producator clothesProd = new Producator("Clothes_prod", new List<Reducere> {
                new Reducere("Summer Sale", new DateTime(2024, 06, 01), Produs => Produs.Pret.Valoare -= Produs.Pret.Valoare * 0.10m),//maybe the discount logic will be hidden in the reducere class?
                new Reducere("Winter Sale", new DateTime(2024, 12, 01), Produs =>Produs.Pret.Valoare -= Produs.Pret.Valoare * 0.20m)
                });
        
        Producator shoesProd = new Producator("Shoes_prod", new List<Reducere> {
                new Reducere("Easter Sale", new DateTime(2024, 05, 20), Produs => Produs.Pret.Valoare -= Produs.Pret.Valoare * 0.15m)
                });
        Producator jeweleryProd = new Producator("Jewelery_prod", new List<Reducere>{
                new Reducere("Eight March", new DateTime(2024, 03, 08), Produs => Produs.Pret.Valoare -= Produs.Pret.Valoare * 0.10m)  
        });


        producatori.Add(clothesProd);
        producatori.Add(shoesProd);
        producatori.Add(jeweleryProd);

        Produs ciorapi = new Produs("Ciorapi", clothesProd, new Pret(30, Pret.Monede.LEU));
        Produs tricou = new Produs("Tricou", clothesProd, new Pret(20, Pret.Monede.EUR));
        Produs botine = new Produs("Botine", shoesProd, new Pret(120, Pret.Monede.USD));
        Produs cercei = new Produs("Cercei", jeweleryProd, new Pret(300, Pret.Monede.EUR));

       
        Catalog catalog = new Catalog(new DateTime(2024, 02, 25), new DateTime(2024, 03, 10), producatori, produse);

        Pret.Curs[Pret.Monede.LEU] = 1;
        Pret.Curs[Pret.Monede.EUR] = 19.47m;
        Pret.Curs[Pret.Monede.USD] = 17.30m; //curs is instantiated, but it isnt able to make conversions

    }
}


//Rezolvand exercitiul 1, am inteles ca field Producator este adaugat la PRODUS, 
//insa dupa mine ar fi mai bine ca producatorul sa aiba field PRODUS.
//Primul argument ar fi: clasa Catalog e lista de tip produs, pe cand in ex1 b trebuie sa 
//instantiem un catalog REFOLOSIND PRODUCATORII DIN LISTA, respectiv nu se va pastra legatura 
//dintre producator si produs, deaorece ea nicaieri nu este specificata? Producatorii si
// produsele oare nu trebuie sa fie legate? Da, noi avem field producator in classa produs,
// dar o pereche de ciorapi nu pot fi produse de 5 companii, pe cand 5 companii pot sa produca
// cate o pereche de ciorapi. In plus, la ex 1 d, trebuie sa instantiem o lista de clienti 
//(proprietatea ProduseFavorite trebuie sa contina id-uri care se pot regasi in catalog sau nu). 
//In cazul in care producatorii NU contin produse, de unde sa luam ProduseFavorie si id-urile pentru ex1d ?


//Propun sa instantiez catalog cu produse si tot acolo bagam producatorii (vedem ce va fi),
//iar la FavProd, luam pur si simplu productul cu id-ul lui din catalog.