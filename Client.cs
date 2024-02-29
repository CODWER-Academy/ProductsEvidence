using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

using Pretclass;
namespace Clientclass;

public class Client 
{
    private string[] Inbox = new string[10];
    public string Email;
    public List<Guid> ProduseFavorite = new List<Guid>(); //instead of Bool (from condition) I write Guid to store the id's of favourite products.
    //public Moneda Moneda;
    private int messageCount = 0;
    
    
    public Client(List<Guid> produseFavorite)
    {
        ProduseFavorite = produseFavorite ?? new List<Guid>();

    }

    public bool Notifica(string mesaj)
    {
         Console.WriteLine($"Notifying client: {mesaj}");
        //mesaj = Email;
        if( mesaj.Length > 60)
        {
            return false;

        }
        int messageCount = 0;
        {
        for(; messageCount < Inbox.Length; messageCount++){
            if (Inbox[messageCount] == null)
            {
                Inbox[messageCount] = mesaj;
                return true;
            }
        }
        throw new OutOfMemoryException("Inbox is full");
        }
        }
        
    public void ClearInbox()
    {
        Array.Clear(Inbox, 0, Inbox.Length);
        messageCount = 0;
    }
    ////delete:
     public void PrintInboxMessages()
    {
        Console.WriteLine($"Inbox messages for {Email}:");
        foreach (var message in Inbox)
        {
            if (message != null)
            {
                Console.WriteLine(message);
            }
        }
    }

/////ex 8 start///////////////


    }
    