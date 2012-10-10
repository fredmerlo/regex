using System;
using System.Collections.Generic;

namespace regex
{
    public class Parser
    {
        private const string sTOKEN_ZERO_MORE = "*";
        private const string sTOKEN_EXACTLY_ONE = ".";
        private const char cTOKEN_ZERO_MORE = '*';
        private const char cTOKEN_EXACTLY_ONE = '.';

        private string _expression;
        private string[] _tok; 

        private Parser(string expression)
        {
            _expression = expression;
        }

        public static Parser Instance(string expression)
        {
            return new Parser(expression);
        }

        private bool IsInvalidExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return true;

            _tok = tokenize(_expression);
            return false;
        }

        public bool Matches(string input)
        {
            if (input == null || IsInvalidExpression(_expression))
                return false;

            for (int x = 0; x < _tok.Length; x++ )
            {
                if(_tok[x] == sTOKEN_ZERO_MORE)
                {
                    input = ReadStar(input, x + 1);
                }
                else
                {
                    input = ReadOne(input, x);
                }
            }

            return input.Length == 0 && _tok.Length > 0;
        }

        private string[] tokenize(string expression)
        {
            string others = string.Empty;
            List<string> tokens = new List<string>();

            for (int x = 0; x < expression.Length; x++)
            {
                if(expression[x] == cTOKEN_EXACTLY_ONE || expression[x] == cTOKEN_ZERO_MORE)
                {
                    if(others.Length > 0)
                    {
                        tokens.Add(others);
                        others = string.Empty;
                    }
                    tokens.Add(Convert.ToString(expression[x]));
                }
                else
                {
                    others = others + expression[x];
                }
            }

            if(others.Length > 0)
            {
                tokens.Add(others);
            }

            return tokens.ToArray();
        }

        private string ReadOne(string inp, int i)
        {
            if (string.Empty == inp)
            {
                _tok = new string[] {};
                return inp;
            }

            if (_tok[i] == sTOKEN_EXACTLY_ONE || inp.StartsWith(_tok[i]))
            {
                return inp.Remove(0, _tok[i].Length);
            }

            _tok = new string[]{};
            return inp;
        }

        private string ReadStar(string inp, int i)
        {
            if (i >= _tok.Length)
                return string.Empty;

            while(inp.Length > 0)
            {
                if (_tok[i] == sTOKEN_EXACTLY_ONE || inp.StartsWith(_tok[i]))
                {
                    return inp;
                }

                inp = inp.Remove(0, 1);
            }
            return inp;
        }
    }
}
