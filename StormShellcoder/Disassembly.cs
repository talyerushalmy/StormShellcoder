using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StormShellcoder
{
    class DisassemblyLine
    {
        private string offset;
        private string opcodes;
        private string asm;

        public DisassemblyLine(string raw_line)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            List<string> words = new List<string>();

            string single_space_line = regex.Replace(raw_line, " ");
            Debug.WriteLine(single_space_line);

            foreach (string word in single_space_line.Split(' '))
            {
                Debug.WriteLine('x' + word + 'x');
                if (!word.Equals(""))
                {
                    words.Add(word);
                }
            }

            this.offset = words[0];
            this.opcodes = words[1];
            this.asm = String.Join(" ", words.GetRange(2, words.Count - 2));

            Debug.WriteLine("offset: " + offset);
            Debug.WriteLine("opcodes: " + opcodes);
            Debug.WriteLine("asm: " + asm);
        }

        public string getOffset()
        {
            return this.offset;
        }

        public void setOffset(string offset)
        {
            this.offset = offset;
        }

        public string getOpcodes()
        {
            return this.opcodes;
        }

        public void setOpcodes(string opcodes)
        {
            this.opcodes = opcodes;
        }

        public string getAsm()
        {
            return this.asm;
        }

        public void setAsm(string asm)
        {
            this.asm = asm;
        }
    }

    class Disassembly
    {
        private List<DisassemblyLine> lines;

        public Disassembly(string raw_disasm)
        {
            lines = new List<DisassemblyLine>();

            foreach (string line in raw_disasm.Split('\n'))
            {
                if (line != null && !line.Equals(""))
                    lines.Add(new DisassemblyLine(line));
            }
        }

        public List<DisassemblyLine> getLines()
        {
            return this.lines;
        }

        public string getAllOpcodes()
        {
            string st = "";
            foreach (DisassemblyLine dl in this.lines)
            {
                foreach (string _byte in DataManipulation.splitEveryNChars(dl.getOpcodes(), 2))
                {
                    st += _byte + '-';
                }
            }
            if (st.LastIndexOf('-') >= 0)
                st = st.Remove(st.LastIndexOf('-'), 1);

            return st;
        }

        public uint getSize()
        {
            return Convert.ToUInt32((getAllOpcodes().Length + 1) / 3);
        }
    }
}
