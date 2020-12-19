using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_manager.Models
{
    public class User : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Remote("IsAlreadySigned", "User", HttpMethod = "POST", ErrorMessage = "Email already exists in database.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        public ICollection<TaskBoard> TaskBoards { get; set; }
        public virtual ICollection<TaskBoard> MemberBoards { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<TaskSubelement> TaskSubelements { get; set; }

        public User()
        {
            TaskBoards = new List<TaskBoard>();
            MemberBoards = new List<TaskBoard>();
            Tasks = new List<Task>();
            TaskSubelements = new List<TaskSubelement>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if(string.IsNullOrWhiteSpace(this.FirstName))
            {
                errors.Add(new ValidationResult("Enter the FirstName"));
            }
            if (string.IsNullOrWhiteSpace(this.LastName))
            {
                errors.Add(new ValidationResult("Enter the LastName"));
            }

            return errors;
        }
    }
}