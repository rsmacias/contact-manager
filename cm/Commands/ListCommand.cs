using Acme.ContactManager.Lib;

namespace cm;

internal class ListCommand : Command {
    public ListCommand(ContactStore store, IReadOnlyDictionary<string, string> args)
        : base(Commands.List, args, store) {
        
    }

    public override IEnumerable<Contact> Execute() {
        return Store.Contacts;
    }
}