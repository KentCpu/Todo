using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Server.Models
{
    public class Todo
    {
        public Guid id { get; set; }
        [Required]
        public string title { get; set; }
        
        public string description { get; set; }
        
        [Required]
        public string dateCompletion { get; set; }

        public bool isCompleted  { get; set; }

        [Required, ForeignKey("User")]
        public Guid userId { get; set; }

        public Todo(Guid id, string title, string description, string dateCompletion, Guid userId, bool isCompleted = false)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.dateCompletion = dateCompletion;
            this.userId = userId;
            this.isCompleted = isCompleted; 
        }
    }
}