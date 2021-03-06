using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using TheBlogProject.Enums;

namespace TheBlogProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string BlogUserId { get; set; }
        public string ModeratorId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and no more {1} characters long", MinimumLength = 2)]
        [Display(Name = "Comment")]
        public string Body { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Moderated { get; set; }
        public DateTime? Deleted { get; set; }

        //i went in pg admin and made the ModeratedBody not null in order to be true in ModelState.IsValid in my comments 
        //controller after deleating the required feild
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and no more {1} characters long", MinimumLength = 2)]
        [Display(Name = "Moderated Comment")]
        public string ModeratedBody { get; set; }

        public ModerationType ModerationType { get; set; }

        //Navigation proerties
        public virtual Post Post { get; set; }
        public virtual BlogUser BlogUser { get; set; }
        public virtual BlogUser Moderator { get; set; }

    }
}