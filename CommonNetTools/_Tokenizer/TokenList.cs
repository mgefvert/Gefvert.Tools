﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonNetTools
{
    public class TokenList : List<Token>
    {
        public TokenList()
        {
        }

        public TokenList(IEnumerable<Token> tokens)
        {
            AddRange(tokens);
        }

        public void RemoveValues(params int[] values)
        {
            RemoveAll(token => values.Contains(token.Value));
        }

        public List<TokenList> Split(int splitValue)
        {
            var result = new List<TokenList>();

            var list = new TokenList();
            result.Add(list);

            foreach (var token in this)
            {
                if (token.Value == splitValue)
                    result.Add(list = new TokenList());
                else
                    list.Add(token);
            }

            return result;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var token in this)
            {
                result.Append(token.Text);
                if (token.Section.Any())
                {
                    result.Append(token.Section);
                    result.Append((token.Definition as TokenSectionDefinition)?.EndText);
                }
            }

            return result.ToString();
        }

        public void Trim(params int[] values)
        {
            TrimStart(values);
            TrimEnd(values);
        }

        private void TrimEnd(int[] values)
        {
            while (Count > 0 && values.Contains(this[Count-1].Value))
                RemoveAt(Count-1);
        }

        private void TrimStart(int[] values)
        {
            while (Count > 0 && values.Contains(this[0].Value))
                RemoveAt(0);
        }
    }
}