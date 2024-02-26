using System.Configuration.Assemblies;
using Clientclass;
using Producatorclass;
using Produsclass;
using Reducereclass;

public class Program
{
    public static void Main()
    {
        // List<Reducere> listaReduceri = new List<Reducere>();
        // Producator producator = new Producator("Avicola", listaReduceri);
        List<Producator> producatori = new List<Producator>
        {
            new Producator("Clothes_prod", new List<Reducere> {
                new Reducere("Summer Sale", new DateTime(2024, 06, 01), Produs => {Produs.Pret.Valoare -= Produs.Pret.Valoare * 0.10m;}),//maybe the discount logic will be hidden in the reducere class?
                new Reducere("Winter Sale", new DateTime(2024, 12, 01), Produs => {})
            }),
            new Producator("Shoes_prod", new List<Reducere>{
                new Reducere("Easter Sale", new DateTime(2024, 05, 20), produs => {})
            }),
            //More producatori
        };

    }
}