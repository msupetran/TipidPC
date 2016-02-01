using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipidPc.Domain.Models
{
    public enum ItemSection : int
    {
        ForSale = 0,
        WantToBuy = 1
    }

    public enum ItemCondition : int
    {
        BrandNew = 0,
        Used = 1,
        NewlyReplaced = 2,
        Defective = 3
    }

    public enum ItemWarranty : int
    {
        None = 0,
        Personal = 1,
        Shop = 2
    }

    public enum ItemDuration : int
    {
        FifteenDays = 15,
        ThirtyDays = 30
    }

    public class Item : IValidatableObject
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public int EntryId { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        [Required]
        [Range(1, 999999)]
        public decimal Amount { get; set; }
        [Required]
        public ItemSection Section { get; set; }
        [Required]
        public ItemCondition Condition { get; set; }
        [Required]
        public ItemWarranty Warranty { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }
        [Required]
        public ItemDuration Duration { get; set; }
        [Required]
        public DateTime Expiry { get; set; }

        // Navigation properties
        public virtual Header Header { get; set; }
        public virtual Entry Entry { get; set; }
        public virtual Category Category { get; set; }
        public virtual IUser User { get; set; }

        // IValidatableObject members
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check if parameters are valid...
            if (this.Header == null)
            {
                yield return new ValidationResult(
                    "Header is null or empty.",
                    new string[] { "Header" });
            }

            if (this.Entry == null)
            {
                yield return new ValidationResult(
                    "Entry is null or empty.",
                    new string[] { "Entry" });
            }

            if (this.Id == 0 && (
                (this.Expiry - this.Created).Days != TimeSpan.FromDays(15).TotalDays &
                (this.Expiry - this.Created).Days != TimeSpan.FromDays(30).TotalDays))
            {
                yield return new ValidationResult(
                    "Expiry is neither 15 or 30 days from the date of creation.",
                    new string[] { "Expiry" });
            }

            //if (categoryId <= 0)
            //{
            //    throw new ValidationException("Category is invalid.");
            //}

            //if (amount <= 0)
            //{
            //    throw new ValidationException("Amount is invalid");
            //}

            //if (userId <= 0)
            //{
            //    throw new ValidationException("User is invalid.");
            //}
        }
    }
}
