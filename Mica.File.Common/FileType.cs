using System;
using System.ComponentModel;

namespace Mica.File.Common
{
    public enum FileType
    {
        [Description("File Type Image")]
        Image = 0,

        [Description("File Type Office")]
        Office = 1
    }
}
