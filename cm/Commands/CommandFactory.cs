using Acme.ContactManager.Lib;

namespace cm;

internal class CommandFactory {
    readonly ContactStore store;
    public CommandFactory(ContactStore store) {
        this.store = store;
    }

    public ICommand Add(IReadOnlyDictionary<string, string> args) {
        return new AddCommand(store, args);
    }

    public ICommand Remove(IReadOnlyDictionary<string, string> args) {
        return new RemoveCommand(store, args);
    }

    public ICommand List(IReadOnlyDictionary<string, string> args) {
        return new ListCommand(store, args);
    }

    public ICommand Quit() {
        return new QuitCommand();
    }

    public ICommand SyntaxError() {
        return new SyntaxErrorCommand();
    }

    public ICommand Unknown() {
        return new UnknownCommand();
    }
}