using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage.Table;

namespace FirstOneTo.EmailSender.Models
{
    public class MailingList : TableEntity
    {
        public MailingList()
        {
            RowKey = "mailinglist";
        }

        [Required]
        [RegularExpression(@"[\w]+",
            ErrorMessage = @"Only alphanumeric characters and underscore (_) are allowed.")]
        [Display(Name = "List Name")]
        public string ListName
        {
            get { return PartitionKey; }
            set { PartitionKey = value; }
        }

        [Required]
        [Display(Name = "'From' Email Address")]
        public string FromEmailAddress { get; set; }

        public string Description { get; set; }
    }
}