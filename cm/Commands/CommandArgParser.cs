using Acme.ContactManager.Lib;

namespace cm;

internal class CommandArgParser {
    public static Contact ContactFromArgs(IReadOnlyDictionary<string, string> args) {
        string first = null;
        string last = null;
        string street = null;
        string city = null;
        string state = null;
        string zip = null;

        foreach (KeyValuePair<string, string> pair in args) {
            switch(pair.Key.ToLower()) {
                case "f":
                case "fn":
                case "first":
                case "firstname":
                    first = pair.Value; break;
                case "l":
                case "ln":
                case "last":
                case "lastname":
                    last = pair.Value; break;
                case "str":
                case "street":
                    street = pair.Value; break;
                case "c":
                case "cty":
                case "city":
                    city = pair.Value; break;
                case "stt":
                case "state":
                    state = pair.Value; break;
                case "pc":
                case "postal":
                case "postalcode":
                case "zip":
                case "zipcode":
                    zip = pair.Value; break;
                default: 
                    break;
            }
        }

        return Contact.Create(first, last, street, city, state, zip);
    }

}