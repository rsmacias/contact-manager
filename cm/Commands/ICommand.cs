using Acme.ContactManager.Lib;

namespace cm {
    internal interface ICommand {
        string Verb { get; }
        IReadOnlyDictionary<string, string> Args { get; }
        IEnumerable<Contact> Execute();
    }
}