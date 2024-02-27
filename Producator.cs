using Produsclass;
using Reducereclass;

namespace Producatorclass;
public class Producator 
{
    public string Name;
    //creez field Produs pentru a face ex1 d
    //public List<Produs> Produse;
    public List<Reducere> Reduceri; //maybe the type will be changed

    public Producator(string name, List<Reducere> reduceri)
    {
        Name = name;
        Reduceri = reduceri;
        //Produse = new List<Produs>();

    }
//     public void AddProduct(Produs produs)

// {
//     Produse.Add(produs);
// }
}