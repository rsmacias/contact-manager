using Acme.ContactManager.Lib;

namespace cm;

internal class AddCommand : Command {
    public AddCommand(ContactStore store, IReadOnlyDictionary<string, string> args)
        : base(Commands.Add, args, store) {
        
    }

    public override IEnumerable<Contact> Execute() {
        return new List<Contact> { Store.Add(CommandArgParser.ContactFromArgs(Args)) };
    }
}