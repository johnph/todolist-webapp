using System;

namespace TodoList.WebApp.Models
{
    public class TodoModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
