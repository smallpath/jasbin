using System;
using System.Collections.Generic;
using System.Linq;

namespace jsxbin_to_jsx.JsxbinDecoding
{
    public class ArgumentList : AbstractNode
    {
        bool isMultiAssignment;

        public override string Marker
        {
            get { return Convert.ToChar(0x52).ToString(); }
        }

        public override NodeType NodeType
        {
            get
            {
                return NodeType.ArgumentList;
            }
        }

        public List<INode> Arguments { get; set; }

        public override void Decode()
        {
            Arguments = DecodeChildren();
            isMultiAssignment = DecodeBool();
        }

        public override string PrettyPrint()
        {
            if (Arguments.Count == 0)
            {
                return "";
            }
            if (Arguments.Count == 1)
            {
                return Arguments.First().PrettyPrint();
            }
            if (isMultiAssignment)
            {
                var cleanRest = Arguments.Skip(1).Select(arg => arg.PrettyPrint().Replace("var ", ""));
                string head = Arguments.First().PrettyPrint();
                return head + ", " + string.Join(", ", cleanRest);
            }
            else
            {
                return string.Join(", ", Arguments.Select(a => a.PrettyPrint()));
            }
        }
    }
}
