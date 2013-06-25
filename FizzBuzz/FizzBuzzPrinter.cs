using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    public class FizzBuzzPrinter
    {
        private static IDictionary<int, string> _defaultRules; 
        public static IDictionary<int, string> DefaultRules
        {
            get
            {
                if(_defaultRules == null)
                {
                    _defaultRules = new Dictionary<int, string>
                    {
                        {3, "Fizz"},
                        {5, "Buzz"}
                    };
                }
                return _defaultRules;
            }
        }

        public IDictionary<int, string> Rules { get; set; }
        public TextWriter Writer { get; set; }

        public FizzBuzzPrinter() : this(new Dictionary<int, string>(DefaultRules), Console.Out)
        {
        }

        public FizzBuzzPrinter(TextWriter writer) : this(DefaultRules, writer)
        {
        }

        public FizzBuzzPrinter(IDictionary<int, string> rules, TextWriter writer)
        {
            Rules = rules;
            Writer = writer;
        }

        public void PrintFizzBuzz(int start, int end)
        {
            PrintFizzBuzz(start, end, Writer, Rules);
        }

        public bool AddRule(int value, string text)
        {
            if(Rules.ContainsKey(value))
                return false;
            Rules.Add(value, text);
            return true;
        }

        public bool RemoveRule(int value)
        {
            return Rules.Remove(value);
        }

        public string GetString(int start, int end)
        {
            return GetString(start, end, Rules);
        } 

        public static string GetString(int start, int end, IDictionary<int, string> rules)
        {
            var buffer = new StringBuilder();
            for(var k = start; k <= end; ++k)
                buffer.AppendLine(PrintNumber(k, rules ?? DefaultRules));
            return buffer.ToString();
        }

        public static void PrintFizzBuzz(int start, int end, TextWriter writer)
        {
            for(var k = start; k <= end; ++k)
            {
                if(k % 3 == 0)
                {
                    if(k % 5 == 0)
                        writer.WriteLine("FizzBuzz");
                    else
                    {
                        writer.WriteLine("Fizz");
                    }
                }
                else if(k % 5 == 0)
                    writer.WriteLine("Buzz");
                else
                    writer.WriteLine(k);
            }
        }

        public static void PrintFizzBuzz(int start, int end, TextWriter writer, IDictionary<int, string> rules)
        {
            for(var k = start; k <= end; ++k)
                writer.WriteLine(PrintNumber(k, rules ?? DefaultRules));
        }

        public static string PrintNumber(int value, IDictionary<int, string> rules)
        {
            if(rules == null)
                return string.Format("{0}", value);
            var buffer = new StringBuilder();
            var keyList = rules.Keys.OrderBy(x => x).ToList();
            foreach(var key in keyList.Where(key => value % key == 0))
            {
                buffer.Append(rules[key]);
            }
            if(buffer.Length == 0)
                buffer.Append(value);
            return buffer.ToString();
        } 
    }
}
