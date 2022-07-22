using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuleEngineService.Models
{
    public class Rule
    {
        public string RuleName { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public string Operator { get; set; }
        public string ErrorMessage { get; set; }
        public bool Enabled { get; set; } = true;
        public string Expression { get; set; }
        public string SuccessEvent { get; set; }

    }
}
