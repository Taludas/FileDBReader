﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDBSerializing
{
    public class FileDBDocument_V3 : FileDBDocument_V2
    {
        new public FileDBDocumentVersion VERSION { get; } = FileDBDocumentVersion.Version3;
    }
}
