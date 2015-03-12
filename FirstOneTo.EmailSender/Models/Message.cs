using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage.Table;

namespace FirstOneTo.EmailSender.Models
{
    public class Message : TableEntity
    {
        private long _messageRef;
        private DateTime? _scheduledDate;

        public Message()
        {
            MessageRef = DateTime.Now.Ticks;
            Status = "Pending";
        }

        [Required]
        [Display(Name = "Scheduled Date")]
        // DataType.Date shows Date only (not time) and allows easy hook-up of jQuery DatePicker
        [DataType(DataType.Date)]
        public DateTime? ScheduledDate
        {
            get { return _scheduledDate; }
            set
            {
                _scheduledDate = value;
                PartitionKey = value.Value.ToString("yyyy-MM-dd");
            }
        }

        public long MessageRef
        {
            get { return _messageRef; }
            set
            {
                _messageRef = value;
                RowKey = "message" + value;
            }
        }

        [Required]
        [Display(Name = "List Name")]
        public string ListName { get; set; }

        [Required]
        [Display(Name = "Subject Line")]
        public string SubjectLine { get; set; }

        // Pending, Queuing, Processing, Complete
        public string Status { get; set; }
    }
}