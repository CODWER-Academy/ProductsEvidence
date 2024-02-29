using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Produsclass;

using Pretclass;
namespace Clientclass;

public class Client 
{
    private string[] Inbox = new string[10];
    public string Email;
    public List<Guid> ProduseFavorite = new List<Guid>(); //instead of List<Bool> (from condition) I write Guid to store the id's of favourite products.
    //Because the id is of type GUID => The list contains id's of favourite products (from condition)

    private int messageCount = 0;
    
    public Client(String email, List<Guid> produseFavorite)
    {
        Email = email;
        ProduseFavorite = produseFavorite ?? new List<Guid>();

    }

    public bool Notifica(string mesaj)
{
    Console.WriteLine($"Notifying client: {mesaj}");

    if (mesaj.Length > 60)
    {
        return false;
    }
    //Check if the message already exists in the Inbox
    if (Inbox.Contains(mesaj))
    {
        return false;
    }

    //Find the first available slot in the Inbox and add the message
    for (int messageCount = 0; messageCount < Inbox.Length; messageCount++)
    {
        if (Inbox[messageCount] == null)
        {
            Inbox[messageCount] = mesaj;
            return true;
        }
    }
    // If there are no available slots in the Inbox, throw an exception
    throw new OutOfMemoryException("Inbox is full");
}

    public void ClearInbox() //stergem inboxul la fiecare run ca sa nu se stranga mesajele
    {
        Array.Clear(Inbox, 0, Inbox.Length);
        messageCount = 0;
    }

//Ex 8: Itereaza pe lista de client si afiseaza informatii despre fiecare in felul urmator:
public string[] GetInboxMessages()
    {
        return Inbox.Where(message => !string.IsNullOrEmpty(message)).ToArray();
    }

public static void DisplayClientInformation(List<Client> clients, List<Produs> allProducts)
{
    foreach (var client in clients)
    {
        //Display the email
        Console.WriteLine($"Email: {client.Email}");

        //Display the favorite products by their names
        Console.Write("Produse favorite: ");
        var favoriteProductNames = client.ProduseFavorite
            .Select(productId => allProducts.FirstOrDefault(p => p.Id == productId)?.Name)
            .Where(name => !string.IsNullOrEmpty(name)); //Checks that name is not empty
        Console.WriteLine(string.Join(", ", favoriteProductNames));

        //Display the inbox messages
        Console.WriteLine("Inbox:");
        var messages = client.GetInboxMessages();
        for (int i = 0; i < messages.Length; i++)
        {
            Console.WriteLine($"\t{i + 1}. {messages[i]}");
        }
        Console.WriteLine();
    }
}
}



    