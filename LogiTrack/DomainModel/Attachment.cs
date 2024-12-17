using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrack.DomainModel
{
    public class Attachment
    {
        public Guid Id { get; private set; }
        public string FilePath { get; private set; }

        public Attachment(string filePath)
        {
            Id = Guid.NewGuid();
            FilePath = filePath;
        }
    }
}
