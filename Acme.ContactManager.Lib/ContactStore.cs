namespace Acme.ContactManager.Lib;

public class ContactStore : IContactStore {
    readonly Contact[] contacts = new Contact[100];
    int contactCount = 0;
    int nextId = 1;

    public Contact Add (Contact contact) {
        if (contact == null)
            throw new ArgumentNullException("Add: null contact provided (skipping)");

        Contact withId = Contact.CreateWithId(nextId++, contact);

        if(contactCount == contacts.Length)
            return null; // Add: inserting contact failed - contact list full

        // place it at the end
        contacts[contactCount] = withId;

        contactCount++;
        return withId;
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