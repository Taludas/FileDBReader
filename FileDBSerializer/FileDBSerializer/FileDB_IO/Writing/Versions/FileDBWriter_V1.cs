﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDBSerializing
{
    internal class FileDBWriter_V1 : IFileDBWriter
    {
        public BinaryWriter Writer { get; }

        public FileDBWriter_V1(Stream s)
        {
            Writer = new BinaryWriter(s);
        }

        public void WriteNodeID(FileDBNode n)
        {
            Writer!.Write((ushort)n.ID);
        }

        public void WriteAttrib(Attrib a)
        {
            WriteNodeID(a);
            Writer!.Write7BitEncodedInt(a.Bytesize);
            Writer.Write(a.Content);
        }

        public void WriteTag(Tag t)
        {
            WriteNodeID(t);
            this.WriteNodeCollection(t.Children);
            WriteNodeTerminator();
        }

        public void WriteTagSection(TagSection tagSection)
        {
            int offset = WriteDictionary(tagSection.Tags);
            WriteDictionary(tagSection.Attribs);
            Writer!.Write(offset);
        }

        public int WriteDictionary(Dictionary<ushort, string> dict)
        {
            int offset = (int)Writer!.Position();
            Writer!.Write7BitEncodedInt(dict.Count);
            foreach (KeyValuePair<ushort, String> k in dict)
            {
                Writer.WriteString0(k.Value);
                Writer.Write(k.Key);
            }
            return offset;
        }

        public void WriteMagicBytes() 
        { 
        
        }

        public void WriteNodeTerminator()
        {
            Writer!.Write((Int16)0);
        }
    }
}
