namespace Acme.ContactManager.Lib;

public class ContactStore : IContactStore {
    readonly Contact[] contacts = new Contact[100];
    int contactCount = 0;
    int nextId = 1;

    public Contact Add (Contact contact) {
        if (contact == null)
            throw new ArgumentNullException("Add: null contact provided (skipping)");

        Contact withId = Contact.CreateWithId(nextId++, contact);

        var added = InsertedSorted(withId);

        return added;
    }

    public Contact Remove (Contact contact) {
        Contact removed = null;

        if (contact != null) {
            // 1.- Find the contact to remove
            int foundIndex = -1;
            for(int i=0; i<contactCount; i++) {
                if(contact.CompareTo(contacts[i]) == 0) {
                    foundIndex = i;
                    break;
                }
            }
            // 2.- Remove the found contact by relocating elements above 
            if(foundIndex >= 0) {
                removed = contacts[foundIndex];
                for (int i=foundIndex; i<contactCount-1; i++) 
                    contacts[i] = contacts[i+1];

                contacts[contactCount-1] = null; // Ensuring removing always let final spot empty
                // 3.- Update counter
                contactCount--;
            }
        }
        
        return removed;
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

    private Contact InsertedSorted(Contact contact) {
        // 0.- Validate full container
        if(contactCount == contacts.Length)
            return null; // Add: inserting contact failed - contact list full

        // 1.- Place it at the end
        contacts[contactCount] = contact;

        // 2.- Sort the array 
        // walk it forward to it's appropiate spot 
        for (int i=contactCount; i>0; i--) {
            if(contacts[i].CompareTo(contacts[i-1]) <= 0) {
                // Swaping spots
                Swap(i, i-1);
            }
        }       

        // 3.- Update counter
        contactCount++;

        return contact;
    }
}