using System;
using System.Collections.Generic;

namespace regex
{
    public class Parser
    {
        private const char TOKEN_ZERO_MORE = '*';
        private const char TOKEN_EXACTLY_ONE = '.';
        private Stack<char> _ex;
        private Stack<char> _in;
        private string _expression;

        private Parser (string expression)
        {
            _expression = expression;
        }

        public static Parser Instance(string expression)
        {
            return new Parser(expression);
        }

        private bool IsInvalidExpession(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return true;

            return false;
        }

        private Stack<char> FillStack(string source)
        {
            var newStack = new Stack<char>();
            var chars = source.ToCharArray();

            for (int i = chars.Length - 1; i > -1; i--)
            {
                newStack.Push(chars[i]);
            }
            return newStack;
        }

        public bool Matches(string input)
        {
            if(input == null)
                throw new ArgumentNullException("input");

            if (IsInvalidExpession(_expression))
                return false;

            _ex = FillStack(_expression);
            _in = FillStack(input);

            return Matches();
        }

        private bool Matches()
        {
            while (_ex.Count > 0)
            {
                char c = _ex.Pop();

                if(c == TOKEN_ZERO_MORE)
                {
                    PopUntilStopTokenReached();
                }
                else
                {
                    if (_in.Count == 0)
                        return false;

                    char i = _in.Pop();

                    if (c != TOKEN_EXACTLY_ONE && c != i)
                        return false;
                }
            }

            return _ex.Count == 0 && _in.Count == 0;
        }

        private void PopUntilStopTokenReached()
        {
            if (_ex.Count == 0)
            {
                _in.Clear();
                return;
            }

            string[] stopTokens = new string(_ex.ToArray()).Split(new[] { TOKEN_EXACTLY_ONE, TOKEN_ZERO_MORE });

            while (NotOnStopToken(stopTokens))
            {
                _in.Pop();
            }
        }

        private bool NotOnStopToken(string[] stopTokens)
        {
            string s = new string(_in.ToArray());

            for (int i = 0; i < stopTokens.Length; i++)
            {
                if (s.StartsWith(stopTokens[i]))
                    return false;
            }
            return _in.Count > 0;
        }
    }
}
