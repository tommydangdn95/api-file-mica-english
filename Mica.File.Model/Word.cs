using Core.Models;
using System;

namespace Mica.File.Models
{
    public class Word : Term
    {
        public string Sysnonym { get; set; }
        public string Mean { get; set; }
    }
}
