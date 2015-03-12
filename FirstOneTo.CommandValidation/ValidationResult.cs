using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstOneTo.CommandValidation
{
    public class ValidationResult
    {
        public ValidationResult(bool valid, List<string> errorMessage)
        {
            Valid = valid;
            ErrorMessages = errorMessage;
        }
        public bool Valid { get; private set; }
        public List<string> ErrorMessages { get; private set; } 
        public long AggregateId { get; set; }
    }
}
