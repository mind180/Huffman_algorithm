using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HoffmanAlgorithm
{
    //----------------------------------HoffmanEncode----------------------------------
    public class HoffmanEncode
    {
        String data;
        int encodedSize = 0;

        //List of nodes
        List<Node> nodes = new List<Node>();

        Node root;

        //Dictionry for counting characters, init by CountSymbols()
        Dictionary<char, int> dictionaryOfLetters = new Dictionary<char, int>();

        //for associating symbols with Hoffman code, init by CreateTable()
        Dictionary<char, List<byte> > table = new Dictionary<char, List<byte>>();

        //
        List<byte> code = new List<byte>();
        //Stack<byte> code = new Stack<byte>();


        public HoffmanEncode(byte[] data, int count)
        {
            //open data
            this.data = System.Text.Encoding.UTF8.GetString(data, 0, count);

            CountSymbols();
            this.root = BuildTree();
            BuildTable(this.root);
        }

        public HoffmanEncode( Dictionary<char, int> dictionaryOfLetters )
        {
            this.dictionaryOfLetters = dictionaryOfLetters;
            this.root = BuildTree();
        }

        public String Data
        {
            get { return this.data; }
        }


        public int EncodedSize
        {
            get { return encodedSize; }
        }


        //create dictionary symbol=count
        public void CountSymbols()
        {
            //Create Dictionary
            for (int i = 0; i < data.Length; i++)
            {
                if (this.dictionaryOfLetters.ContainsKey(data[i]))
                {
                    this.dictionaryOfLetters[data[i]]++;
                }
                else
                {
                    this.dictionaryOfLetters.Add(data[i], 1);
                }
            }
        }

        //Build Hoffman tree and return root node
        public Node BuildTree()
        {
            foreach (var symbol in dictionaryOfLetters)
            {
                //Add nodes to List
                nodes.Add(new Node(symbol.Key, symbol.Value));
            }

            //Build Hoffman tree
            while (nodes.Count != 1)
            {
                nodes.Sort();
                Node temp1 = nodes.First();
                nodes.Remove(temp1);
                Node temp2 = nodes.First();
                nodes.Remove(temp2);
                nodes.Add(new Node(temp1, temp2));
            }
            return nodes.First();//return root
        }

        public void BuildTable(Node root)
        {
            if (root.left != null)
            {
                code.Add(0);
                BuildTable(root.left);
            }

            if (root.right != null)
            {
                code.Add(1);
                BuildTable(root.right);
            }

            if (root.left == null && root.right == null)
                table[root.Ch] = new List<byte>(code);

            if (code.Count != 0)
                code.RemoveAt(code.Count - 1);
        }

        public void writeBinary( String path )
        {
            this.encodedSize = 0;
            byte[] temp = new byte[1];
            byte count = 0;

            //-----------prepare-----------------
            char[] dataChar = data.ToArray();

            List<byte> bin = new List<byte>();

            for (int i = 0; i < dataChar.Length; i++)
            {
                var vals = table[dataChar[i]];
                foreach (var v in vals)
                    bin.Add(v);
            }
            //--------------------------------

            //---------------------------------write in file--------------------------------
            temp[0] = 0;
            try
            {
                using (FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    for (int i = 0; i < bin.Count; i++)
                    {
                        if (bin[i] == 1)
                        {
                            temp[0] = Convert.ToByte(temp[0] | (1 << (7 - count)));
                            encodedSize++;
                        }

                        count++;
                        if (count == 8)
                        {
                            fs.Write(temp, 0, 1);
                            count = 0;
                            temp[0] = 0;
                        }
                    }
                    if (count < 8)
                        fs.Write(temp, 0, 1);
                }
            }
            catch (SystemException)
            {
                throw new SystemException();
            }
            //--------------------------------write dictionary--------------------------------




            //--------------------------------------------------------------------------------

        }//void writeBinary

        public void writeDictionary(String path) {
             //write didctionary
            //String dictPath = path.Replace(".bin", "_dict.bin");
             
            try{
                using (FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {       
                    fs.Write(this.getDictionary(), 0, getDictionary().Length);
                }
            }
            catch(SystemException){
                throw new SystemException();
            }
        }

        public String getLetterCounterString()
        {
            String letters = "";
            foreach (var d in dictionaryOfLetters)
            {
                letters += d.Key.ToString() + "=" + d.Value.ToString() + ", ";
            }
            letters += "\n";
            return letters;
        }

        public String getHuffmansCodeString()
        {
            String codes = "";
            foreach (var t in table)
            {
                codes += t.Key.ToString() + " = ";
                foreach (var v in t.Value)
                {
                    codes += v.ToString();
                }
                codes += "\n";
            }
            return codes;
        }

        public byte[] getDictionary()
        {
            String dict="";

            foreach ( var letter in dictionaryOfLetters )
            {
                dict += letter.Key.ToString();
                dict += "=";
                dict += letter.Value.ToString() + " ";
            }
            byte[] dictBytes = new byte[dict.Length];
            for (int i = 0; i < dict.Length; i++)
            {
                dictBytes[i] = Convert.ToByte( dict[i] );
            }

            return dictBytes;
        }


        public String getEncodedString()
        {
            String encoded = "";
            char[] dataChar = new char[data.Length];
            dataChar = data.ToArray();

            for (int i = 0; i < dataChar.Length; i++)
            {
                var bin = table[dataChar[i]];
                foreach (var b in bin)
                {
                    encoded += b.ToString();
                }
            }

            return encoded;
        }//String getEncodedString()


        public String decode( byte[] data, int len )
        {
            String decoded = "";
            byte b = 0;
            byte shiffter = 0;
            byte count = 0;
            Node r = root;

            for (int i = 0; i < len; i++)
            {
                for (count = 0; count < 8; count++)
                {
                    shiffter = Convert.ToByte((1 << (7 - count)));
                    b = Convert.ToByte(data[i] & shiffter);
                    if (b == 0) r = r.left;
                    else r = r.right;
                    if (r.left == null & r.right == null)
                    {
                        decoded += r.Ch.ToString();
                        r = root;
                    }
                }
            }
            this.data = decoded;
            return decoded;
        }//String decodeBinary(
        
    }
    //--------------------------------------------------------------------------------------- ~HoffmanEncode
}
