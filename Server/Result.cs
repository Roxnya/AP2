using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Result class.
    /// </summary>
    class Result
    {
        /// <summary>
        /// result get and set
        /// </summary>
        public string Json { get; internal set; }

        /// <summary>
        /// Status get and set
        /// </summary>
        public Status Status { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="json">result</param>
        /// <param name="status">status</param>
        public Result(string json, Status status)
        {
            this.Json = json;
            this.Status = status;
        }
    }

    /// <summary>
    /// Status enum
    /// </summary>
    public enum Status
    {
        Close,
        ReadOnly,
        Communicating
    }


}

