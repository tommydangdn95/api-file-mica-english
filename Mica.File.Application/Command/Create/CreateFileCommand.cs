using MediatR;
using Mica.File.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mica.File.Application.Command.Create
{
    public class CreateFileCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public int FileType { get; set; }
    }
}
