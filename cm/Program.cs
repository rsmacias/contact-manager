using Acme.ContactManager.Lib;
using System;

namespace cm {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine("Welcome to contact-manger!");

            ContactStore store = new ContactStore();
            Repl repl = new Repl(Console.In, Console.Out, store);
            repl.Run();
        }
    }
}