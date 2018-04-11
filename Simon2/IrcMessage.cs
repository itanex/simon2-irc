using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Simon2
{

    // IRC Message format
    // :<prefix> <command> <params> :<trailing>

    // regex for parsing IRC Message
    // ^(:(?<prefix>\S+) )?(?<command>\S+)( (?!:)(?<params>.+?))?( :(?<trail>.+))?$
    public class IrcMessage
    {
        private const string MatchExpression = @"^(:(?<prefix>\S+) )?(?<command>\S+)( (?!:)(?<params>.+?))?( :(?<trail>.+))?$";
        private static readonly Regex Parser = new Regex(MatchExpression, RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        public string RawMessage { get; private set; }

        public string Prefix { get; private set; }
        public string Command { get; private set; }
        public string[] Parameters { get; private set; }
        public string Trailing { get; private set; }

        public bool Success
        {
            get
            {
                return ParseMessage();
            }
        }

        public IrcMessage(string message)
        {
            RawMessage = message;
            ParseMessage();
        }

        private bool ParseMessage()
        {
            var matched = Parser.Match(RawMessage);

            if (matched.Success)
            {
                Prefix = matched.Groups["prefix"].Value;
                Command = matched.Groups["command"].Value;
                Parameters = matched.Groups["params"].Value.Split(' ');
                Trailing = matched.Groups["trail"].Value;

                return true;
            }

            return false;
        }
    }
}
