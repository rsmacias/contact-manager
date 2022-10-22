using Acme.ContactManager.Lib;

namespace cm;

internal class QuitCommand : ICommand {
    public string Verb => Commands.Quit;

    public IReadOnlyDictionary<string, string> Args => new Dictionary<string, string>(0);

    public IEnumerable<Contact> Execute() {
        return new List<Contact>(0);
    }
}