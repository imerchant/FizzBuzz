using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearMeasureInterview.Tests
{
    class StringWriter : TextWriter, IEquatable<StringWriter>
    {
        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        private StringBuilder Buffer { get; set; }

        public StringWriter()
        {
            Buffer = new StringBuilder();
        }

        public override void WriteLine()
        {
            Buffer.AppendLine();
        }

        public override void WriteLine(string value)
        {
            Buffer.AppendLine(value);
        }

        public override void WriteLine(int value)
        {
            Buffer.Append(value);
            Buffer.AppendLine();
        }

        public bool Equals(StringWriter other)
        {
            if(other == null) return false;
            if(other == this) return true;
            return ToString() == other.ToString();
        }

        public override string ToString()
        {
            return Buffer.ToString();
        }
    }
}
