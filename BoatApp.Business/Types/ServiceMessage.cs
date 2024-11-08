using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Business.Types
{
    public class ServiceMessage
    {
        public bool IsSucceeded { get; set; } // Yazım hatası düzeltildi
        public string Message { get; set; }
        public bool IsSucced { get; internal set; }
    }

    public class ServiceMessage<T>
    {
        public bool IsSucceeded { get; set; } // Yazım hatası düzeltildi
        public string Message { get; set; }
        public T? Data { get; set; }
        public bool IsScuccw { get; internal set; }
        public bool IsSucced { get; internal set; }
    }
}

