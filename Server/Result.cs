using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Result
    {
        public string Json { get; internal set; }
        public Status Status { get; internal set; }

        public Result(string json, Status status)
        {
            this.Json = json;
            this.Status = status;
        }
    }

    public enum Status
    {
        Close,
        ReadOnly,
        Communicating
    }


}

