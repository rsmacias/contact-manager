using Acme.ContactManager.Lib;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace cm;

// Read-Eval-Print Loop Interface
internal class Repl {

    readonly TextReader input;
    readonly TextWriter output;
    readonly CommandFactory factory;

    readonly Regex verbRegex = new Regex(@"^(?<verb>\w+)");
    readonly Regex fieldsRegex = new Regex(@"(?<field>\w+)=(?<value>[^;]+)");

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
                case Commands.List:
                    PrintList(cmd.Execute());
                    break;
                default:
                    cmd.Execute();
                    break;
            }

            timer.Stop();
        }
    }

    private void PrintList(IEnumerable<Contact> contacts) {
        output.WriteLine($"ID\tFirst Name\tLast Name\tStreet Address\tCity\tState\tPostal Code".Trim());
        foreach (Contact contact in contacts)
            output.WriteLine(contact.ToString());
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
        bool parsedVerb = false;
        Dictionary<string, string> fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        Match verbMatch = verbRegex.Match(line.TrimStart());
        if (verbMatch.Success) {
            parsedVerb = true;
            verb = verbMatch.Value;
            foreach (Match match in fieldsRegex.Matches(line))
                fields[match.Groups["field"].Value] = match.Groups["value"].Value;
        } else { 
            verb = Commands.Error;
            fields["message"] = $"Unable to parse verb. ({line})";
        }

        args = fields;
        return parsedVerb;
    }

    private string Read() {
        return input.ReadLine();
    }

    private void Prompt() {
        output.Write("> ");
        output.Flush();
    }
}