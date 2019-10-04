using System;
using System.Windows.Forms;
using NiL.JS.BaseLibrary;
using NiL.JS.Core;
using NiL.JS.Extensions;

namespace IntelligentEscaper
{
    public partial class MainForm : Form
    {
        private readonly Context context = new Context();
        string swap = "";

        public MainForm()
        {
            context.Eval(
                "function ReverseString(str)\t//Helper Function\r\n{\r\n\tvar newstr = \"\";\r\n\t\r\n\tfor (var i = str.length - 1; i >= 0; i--)\r\n\t\tnewstr += str.charAt(i);\r\n\r\n\treturn newstr;\r\n}\r\n\r\nfunction LastOccurrenceOfChar(str, char)\t//Helper Function\r\n{\r\n\tvar found = ReverseString(str).indexOf(char);\r\n\t\r\n\tif (found != -1)\r\n\t\treturn str.length - 1 - found;\r\n\telse\r\n\t\treturn -1;\r\n}\r\n\r\nfunction CharToEncoded(char)\r\n{\r\n\tvar tmpstr = char.charCodeAt(0).toString(16).toUpperCase();\r\n\tvar newstr = \"\";\r\n\t\t\r\n\tif (tmpstr.length < 2)\r\n\t\tnewstr = \"%0\" + tmpstr;\r\n\telse if (tmpstr.length == 2)\r\n\t\tnewstr = \"%\" + tmpstr;\r\n\telse\r\n\t\tnewstr = \"%u\" + tmpstr;\r\n\r\n\treturn newstr;\r\n}\r\n\r\nfunction Escape(str, forced)\r\n{\r\n\tif (forced == null)\r\n\t\tforced = false;\r\n\t\r\n\tif (forced)\r\n\t\treturn ForcedEscape(str);\r\n\telse\r\n\t\treturn escape(str);\r\n}\r\n\r\nfunction ForcedEscape(str)\r\n{\r\n\tvar newstr = \"\";\r\n\tvar tmpstr;\r\n\r\n\tfor (var i = 0; i < str.length; i++)\r\n\t\tnewstr += CharToEncoded(str.charAt(i));\r\n\t\r\n\treturn newstr;\r\n}\r\n\r\nfunction ParseURLParameters(str)\r\n{\r\n\tvar anchortext = \"\";\r\n\tvar anchorfound = LastOccurrenceOfChar(str, \"#\");\r\n\t\r\n\tif (anchorfound != -1)\r\n\t{\r\n\t\tanchortext = str.substr(anchorfound);\r\n\t\tstr = str.substr(0, anchorfound);\r\n\t}\r\n\t\r\n\tvar paramsfound = str.indexOf(\"?\");\r\n\t\r\n\tif (paramsfound != -1)\r\n\t\tstr = str.substr(0, paramsfound) + \"\\n\\n\" + str.substr(paramsfound+1);\r\n\t\r\n\treturn unescape(str.replace(/&/g, \"\\n\").replace(/\\+/g, \" \")) + (anchortext==\"\"?\"\":\"\\n\\n\" + anchortext);\r\n}\r\n\r\nfunction UnescapeURLSpecialChars(str)\r\n{\r\n\tstr = str.replace(/%2F/g, \"/\");\r\n\t\r\n\treturn str;\r\n}\r\n\r\nfunction UnescapeHostSpecialChars(str)\r\n{\r\n\tstr = str.replace(/%2E/g, \".\");\r\n\t\r\n\tvar colon = str.indexOf(\"%3A\");\t\t//%3A = :\r\n\t\r\n\tif (colon != -1)\r\n\t\tstr = str.substr(0, colon) + unescape(str.substr(colon));\r\n\t\r\n\treturn str;\r\n}\r\n\r\nfunction EscapeURL(str, forced, forcehost)\r\n{\r\n\tif (forced == null)\r\n\t\tforced = false;\r\n\t\t\r\n\tif (forcehost == null)\r\n\t\tforcehost = false;\r\n\t\r\n\tvar anchortext = \"\";\r\n\tvar anchorfound = LastOccurrenceOfChar(str, \"#\");\r\n\t\r\n\tif (anchorfound != -1)\r\n\t{\r\n\t\tanchortext = str.substr(anchorfound);\r\n\t\tstr = str.substr(0, anchorfound);\r\n\t}\r\n\t\r\n\tvar newstr = \"\";\r\n\tvar processed = 0;\r\n\t\r\n\tvar protocol = str.indexOf(\"//\");\r\n\t\r\n\tif (protocol != -1)\r\n\t{\r\n\t\tnewstr += str.substr(processed, protocol + 2);\r\n\t\tprocessed += protocol + 2;\r\n\t}\r\n\t\r\n\tvar firstslash = str.substr(processed).indexOf(\"/\");\r\n\t\r\n\tif (firstslash != -1)\r\n\t{\r\n\t\tif (forcehost)\r\n\t\t\tnewstr += UnescapeHostSpecialChars(Escape(str.substr(processed, firstslash + 1), true));\r\n\t\telse\r\n\t\t\tnewstr += str.substr(processed, firstslash + 1);\r\n\r\n\t\tprocessed += firstslash + 1;\r\n\t}\r\n\t\r\n\tvar paramsbegin = str.substr(processed).indexOf(\"?\");\r\n\t\r\n\tif (paramsbegin != -1)\r\n\t{\r\n\t\tif (firstslash != -1)\r\n\t\t{\r\n\t\t\tnewstr += Escape(str.substr(processed, paramsbegin), forced);\r\n\t\t\tprocessed += paramsbegin;\r\n\t\t}\r\n\t\telse\r\n\t\t{\r\n\t\t\tif (forcehost)\r\n\t\t\t\tnewstr += UnescapeHostSpecialChars(Escape(str.substr(processed, paramsbegin), true));\r\n\t\t\telse\r\n\t\t\t\tnewstr += str.substr(processed, paramsbegin);\r\n\r\n\t\t\tprocessed += paramsbegin;\r\n\t\t}\r\n\t\t\r\n\t\tnewstr += \"?\";\r\n\t\t\r\n\t\tvar params_str = str.substr(processed + 1);\r\n\t\t\r\n\t\tvar params = params_str.split(\"&\");\r\n\t\t\r\n\t\tfor (var i = 0; i < params.length; i++)\r\n\t\t{\r\n\t\t\tvar param = params[i];\r\n\t\t\t\r\n\t\t\tif (i != 0)\r\n\t\t\t\tnewstr += \"&\";\r\n\t\t\t\r\n\t\t\tnewstr += EscapeParameter(param, forced);\r\n\t\t}\r\n\t}\r\n\telse\r\n\t\tif (firstslash != -1)\r\n\t\t\tnewstr += Escape(str.substr(processed), forced);\r\n\t\telse\r\n\t\t{\r\n\t\t\tif (forcehost)\r\n\t\t\t\tnewstr += UnescapeHostSpecialChars(Escape(str.substr(processed), true));\r\n\t\t\telse\r\n\t\t\t\tnewstr += str.substr(processed);\r\n\t\t}\r\n\t\r\n\tif (forced)\r\n\t\tnewstr = UnescapeURLSpecialChars(newstr);\r\n\t\r\n\treturn newstr + anchortext;\r\n}\r\n\r\nfunction EscapeParameter(str, forced)\r\n{\r\n\tif (forced == null)\r\n\t\tforced = false;\r\n\r\n\tvar equal = str.indexOf(\"=\");\r\n\t\r\n\tif (equal != -1)\r\n\t\treturn Escape(str.substr(0, equal), forced) + \"=\" + Escape(str.substr(equal+1), forced);\r\n\telse\r\n\t\treturn Escape(str, forced);\r\n}\r\n\r\nfunction MakeURLWithParameters(str)\r\n{\r\n\tvar newstr = \"\";\r\n\t\r\n\tvar lines = str.split(\"\\n\");\r\n\tvar params = 0;\r\n\t\r\n\tfor (var i = 0; i < lines.length; i++)\r\n\t\tif (i == 0)\r\n\t\t\tnewstr += EscapeURL(lines[i]);\r\n\t\telse\r\n\t\t{\r\n\t\t\tvar line = lines[i];\r\n\t\t\t\r\n\t\t\tif (line != \"\")\r\n\t\t\t{\r\n\t\t\t\tif (i == (lines.length-1) && line.charAt(0) == \"#\")\r\n\t\t\t\t\tnewstr += line;\r\n\t\t\t\telse\r\n\t\t\t\t{\r\n\t\t\t\t\tif (params == 0)\r\n\t\t\t\t\t\tnewstr += \"?\";\r\n\t\t\t\t\telse\r\n\t\t\t\t\t\tnewstr += \"&\";\r\n\t\t\t\t\t\r\n\t\t\t\t\tnewstr += EscapeParameter(line);\r\n\t\t\t\t\t\r\n\t\t\t\t\tparams++;\r\n\t\t\t\t}\r\n\t\t\t}\r\n\t\t}\r\n\t\r\n\treturn newstr;\r\n}\r\n\r\nfunction IntelligentUnescape(str)\r\n{\r\n\tvar laststr;\r\n\tvar newstr = str;\r\n\t\r\n\tdo\r\n\t{\r\n\t\tlaststr = newstr;\r\n\t\tnewstr = unescape(newstr);\r\n\t} while (laststr != newstr);\r\n\t\r\n\treturn newstr;\r\n}\r\n\r\nfunction IntelligentEscape(str)\r\n{\r\n\treturn escape(IntelligentUnescape(str));\r\n}\r\n\r\nfunction EscapeChar(str, char)\r\n{\r\n\tif (char.length > 0)\r\n\t{\r\n\t\tvar newstr = \"\";\r\n\t\t\r\n\t\tvar fchar = char.charAt(0);\r\n\t\tvar fcharenc = CharToEncoded(fchar);\r\n\t\t\r\n\t\tvar tmpchr;\r\n\t\t\r\n\t\tfor (var i = 0; i < str.length; i++)\r\n\t\t{\r\n\t\t\ttmpchr = str.charAt(i);\r\n\t\t\t\r\n\t\t\tif (tmpchr != fchar)\r\n\t\t\t\tnewstr += tmpchr;\r\n\t\t\telse\r\n\t\t\t\tnewstr += fcharenc;\r\n\t\t}\r\n\t\t\r\n\t\treturn newstr;\r\n\t}\r\n\telse\r\n\t\treturn str;\r\n}\r\n");
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("Escape").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text, true}).ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("IntelligentUnescape").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button3_Click_1(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("Escape").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("EscapeURL").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("EscapeURL").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text, true}).ToString();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("EscapeURL").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text, true, true}).ToString();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("IntelligentEscape").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("ParseURLParameters").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("MakeURLWithParameters").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("EscapeChar").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text, "+"}).ToString();
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("EscapeChar").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text, "#"}).ToString();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("encodeURI").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("decodeURI").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("encodeURIComponent").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("decodeURIComponent").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text}).ToString();
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            swap = richTextBox1.Text;
            var concatFunction = context.GetVariable("EscapeChar").As<Function>();
            richTextBox2.Text = concatFunction.Call(new Arguments {richTextBox1.Text, textBox1.Text}).ToString();
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox2.Text;
            richTextBox2.Text = swap;
            swap = richTextBox1.Text;
        }
    }
}