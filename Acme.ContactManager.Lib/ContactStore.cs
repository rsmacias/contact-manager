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

        // Sort the array 
        // walk it forward to it's appropiate spot 
        for (int i=contactCount; i>0; i--) {
            if(contacts[i].CompareTo(contacts[i-1]) <= 0) {
                // Swaping spots
                Swap(i, i-1);
            }
        }

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

    private void Swap(int index1, int index2) {
        Contact temp = contacts[index1];
        contacts[index1] = contacts[index2];
        contacts[index2] = temp;
    }
}