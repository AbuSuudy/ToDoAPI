using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class ToDo
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        [Required]
        public  string Title { get; set; }
        public bool Completed { get; set; }
    }



}
