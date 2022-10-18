namespace Acme.ContactManager.Lib;

public interface IContactStore {
    Contact Add (Contact contact);
    Contact Remove (Contact contact);
    IEnumerable<Contact> Contacts { get; }
}