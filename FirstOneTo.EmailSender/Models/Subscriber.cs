using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage.Table;

namespace FirstOneTo.EmailSender.Models
{
    public class Subscriber : TableEntity
    {
        [Required]
        [Display(Name = "List Name")]
        public string ListName
        {
            get { return PartitionKey; }
            set { PartitionKey = value; }
        }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress
        {
            get { return RowKey; }
            set { RowKey = value; }
        }

        public string SubscriberGUID { get; set; }

        public bool? Verified { get; set; }
    }
}