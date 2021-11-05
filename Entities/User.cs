using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public enum Ganders
    {
        Male,
        Female
    }
    [Table(nameof(User))]
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public Ganders Gander { get; set; }
        public ICollection<TodoItem> TodoItems { get; set; }
    }
    [Table(nameof(TodoItem))]
    public class TodoItem : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public DateTime Date { get; set; }

        public Guid? UserID { get; set; }
        public User User { get; set; }
    }

    public class EntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

    }
}
