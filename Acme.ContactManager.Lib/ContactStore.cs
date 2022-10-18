namespace Acme.ContactManager.Lib;

public class ContactStore : IContactStore {
    readonly Contact[] contacts = new Contact[100];
    int contactCount = 0;
    int nextId = 1;

    public Contact Add (Contact contact) {
        throw new NotImplementedException();
    }

    public Contact Remove (Contact contact) {
        throw new NotImplementedException();
    }

    public IEnumerable<Contact> Contacts { 
        get {
            for(int i=0; i<contactCount; i++) 
                yield return contacts[i];
        } 
    }
}