using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NiL.JS.BaseLibrary;
using NiL.JS.Core;
using NiL.JS.Extensions;

namespace IntelligentEscaper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var context = new Context();

            context.Eval("function CharToEncoded(char)\r\n{\r\n\tvar tmpstr = char.charCodeAt(0).toString(16).toUpperCase();\r\n\tvar newstr = \"\";\r\n\t\t\r\n\tif (tmpstr.length < 2)\r\n\t\tnewstr = \"%0\" + tmpstr;\r\n\telse if (tmpstr.length == 2)\r\n\t\tnewstr = \"%\" + tmpstr;\r\n\telse\r\n\t\tnewstr = \"%u\" + tmpstr;\r\n\r\n\treturn newstr;\r\n}  function ForcedEscape(str)\r\n{\r\n\tvar newstr = \"\";\r\n\tvar tmpstr;\r\n\r\n\tfor (var i = 0; i < str.length; i++)\r\n\t\tnewstr += CharToEncoded(str.charAt(i));\r\n\t\r\n\treturn newstr;\r\n}");

            var concatFunction = context.GetVariable("ForcedEscape").As<Function>();

            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var context = new Context();

            context.Eval("function IntelligentUnescape(str)\r\n{\r\n\tvar laststr;\r\n\tvar newstr = str;\r\n\t\r\n\tdo\r\n\t{\r\n\t\tlaststr = newstr;\r\n\t\tnewstr = unescape(newstr);\r\n\t} while (laststr != newstr);\r\n\t\r\n\treturn newstr;\r\n}");

            var concatFunction = context.GetVariable("IntelligentUnescape").As<Function>();

            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            richTextBox1 = richTextBox2;
        }
    }
}
