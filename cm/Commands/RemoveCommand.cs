using Acme.ContactManager.Lib;

namespace cm;

internal class RemoveCommand : Command {
    public RemoveCommand(ContactStore store, IReadOnlyDictionary<string, string> args)
        : base(Commands.Remove, args, store) {
        
    }

    public override IEnumerable<Contact> Execute() {
        return new List<Contact> { Store.Remove(null) };
    }
}