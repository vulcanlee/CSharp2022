using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebApplication2
{
    public class CustomVirtualFile : VirtualFile
    {
        private readonly string _content;
        public bool Exists
        {
            get { return (_content != null); }
        }
        public CustomVirtualFile(string virtualPath, string body)
            : base(virtualPath)
        {
            _content = body;
        }
        public override Stream Open()
        {
            return new MemoryStream(System.Text.Encoding.Default.GetBytes(_content), false);
        }
    }
}