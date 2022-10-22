using Acme.ContactManager.Lib;

namespace cm;

internal class UnknownCommand : ICommand {
    public string Verb => Commands.Error;

    public IReadOnlyDictionary<string, string> Args => new Dictionary<string, string>(0);

    public IEnumerable<Contact> Execute() {
        return new List<Contact>(0);
    }
}