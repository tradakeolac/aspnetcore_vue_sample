namespace Saleman.Model.ServiceObjects
{
    using System.Collections.Generic;

    public class ResultServiceObject : ServiceObjectBase
    {
        public bool Status { get; set; }
        public IList<string> Errors { get; set; }

        public readonly static ResultServiceObject Success = new ResultServiceObject() { Status = true };
        public readonly static ResultServiceObject Fail = new ResultServiceObject() { Status = false, Errors = new List<string>() };
    }
}
