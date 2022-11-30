using System.Collections.Generic;

namespace InsuranceMemberPortal.DTO
{
    public class ResponseDTO
    {
        public bool IsSucceess { get; set; }
        public string Message { get; set; }

        public object Result { get; set; }

        public List<string> Errors { get; set; }
    }
}
