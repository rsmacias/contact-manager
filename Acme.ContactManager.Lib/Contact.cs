namespace Acme.ContactManager.Lib;
public class Contact {
    public int? ID { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string StreetAddress { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }

    private Contact( string first, string last, string street, string city, string state, string code ) {
        ID = default;
        FirstName = first;
        LastName = last;
        StreetAddress = street;
        City = city;
        State = state;
        PostalCode = code;
    }

    private Contact( int ID, string first, string last, string street, string city, string state, string code ) 
        : this (first, last, street, city, state, code) {
        this.ID = ID;
    }

    public static Contact Create( string first, string last, string street, string city, string state, string code ) {
        return new Contact(first, last, street, city, state, code);
    }

    public static Contact CreateWithId(int ID, Contact contact) {
        return new Contact(ID, contact.FirstName, contact.LastName, contact.StreetAddress, contact.City, contact.State, contact.PostalCode);
    }

    public override string ToString() {
        if(ID.HasValue) {
            return $"{ID.Value}\t{FirstName}\t{LastName}\t{StreetAddress}\t{City}\t{State}\t{PostalCode}".Trim();
        } {
            return $"{FirstName}\t{LastName}\t{StreetAddress}\t{City}\t{State}\t{PostalCode}".Trim();
        }
    }

    public override int GetHashCode() {
        return ToString().GetHashCode();
    }
}