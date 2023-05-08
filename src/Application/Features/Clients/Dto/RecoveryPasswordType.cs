using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Clients.Dto
{
    public class RecoveryPassword
    {
        public bool IsValid { get; set; }
        public RecoveryPasswordMessage[] Messages { get; set; }
        public RecoveryPasswordMessage Message { get; set; }
        public bool Data { get; set; }
    }

    public partial class RecoveryPasswordMessage
    {
        public string TextMessage { get; set; }
        public long MessageType { get; set; }
        public object Title { get; set; }
        public string Text { get; set; }
    }
}
