using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

using Pretclass;
namespace Clientclass;

public class Client 
{
    private string[] Inbox = new string[10];
    public string Email;
    public List<bool> ProduseFavorite = new List<bool>();
    //public Moneda Moneda;
    private int messageCount = 0;
    
    
    public Client()
    {

    }
    
    public bool Notifica(string mesaj)
    {
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
    }
    