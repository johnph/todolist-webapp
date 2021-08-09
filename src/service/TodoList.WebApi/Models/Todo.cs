using System;

namespace TodoList.WebApi.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string Owner { get; set; }

    }
}
