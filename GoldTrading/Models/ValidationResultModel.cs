using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldTrading.Models
{
    // Model สำหรับคืนค่าผลการตรวจสอบ
    public class ValidationResultModel
    {
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; } = new List<string>();
    }
}
