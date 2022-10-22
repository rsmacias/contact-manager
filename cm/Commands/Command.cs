using Acme.ContactManager.Lib;

namespace cm;

internal abstract class Command : ICommand  {
    protected ContactStore Store { get; }
    public string Verb { get; }
    public IReadOnlyDictionary<string, string> Args { get; }
    protected Command(string verb, IReadOnlyDictionary<string, string> args, ContactStore store) {
        Verb = verb;
        Args = args;
        Store = store;
    }    
    public abstract IEnumerable<Contact> Execute();
}