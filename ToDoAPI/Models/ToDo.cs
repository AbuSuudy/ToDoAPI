using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoAPI.Models
{
    public class ToDo
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [Required]
        [JsonPropertyName("title")]
        public  string Title { get; set; }
        [JsonPropertyName("completed")]
        public bool Completed { get; set; }
    }



}
