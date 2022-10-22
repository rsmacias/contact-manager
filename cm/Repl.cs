using Acme.ContactManager.Lib;
using System.Diagnostics;

namespace cm;

// Read-Eval-Print Loop Interface
internal class Repl {

    readonly TextReader input;
    readonly TextWriter output;
    readonly CommandFactory factory;

    internal Repl(TextReader input, TextWriter output, ContactStore store) {
        this.input = input;
        this.output = output;
        this.factory = new CommandFactory(store);
    }

    internal void Run() {
        bool quitSeen = false; 

        while(!quitSeen) {
            ICommand cmd = NextCommand();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            switch(cmd.Verb){
                case Commands.Quit:
                    quitSeen = true;
                    break;
                default:
                    cmd.Execute();
                    break;
            }

            timer.Stop();
        }
    }

    private ICommand NextCommand() {
        Prompt();
        return TryMapToCommand(Read());
    }

    private ICommand TryMapToCommand(string line) {
        string verb; 
        IReadOnlyDictionary<string, string> args;

        if (ParseLine(line, out verb, out args)) {
            switch(verb) {
                case Commands.Add:
                    return factory.Add(args);
                case Commands.Remove:
                    return factory.Remove(args);
                case Commands.List:
                    return factory.List(args);
                case Commands.Quit:
                    return factory.Quit();
                default:
                    return factory.Unknown();
            }
        }

        return factory.SyntaxError();
    }

    private bool ParseLine(string line, out string verb, out IReadOnlyDictionary<string, string> args) {
        verb = line.Contains("quit") ? Commands.Quit : Commands.Add;
        var fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        fields["f"] = "Robert";
        fields["l"] = "Macias";
        fields["str"] = "Happy Straze";
        fields["c"] = "Guayaquil";
        fields["stt"] = "Guayas";
        fields["pc"] = "18345";
        args = fields;
        return true;
    }

    private string Read() {
        return input.ReadLine();
    }

    private void Prompt() {
        output.Write("> ");
        output.Flush();
    }
}