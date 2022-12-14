using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.Model
{
    public class Tags
    {
        // gives a uniqe name that only can appear at once
        // Todo implement uppercase or lowercase = ?
        [Key]
        public int TagId { get; set; }

        public string TagName { get; set; } = null!;
        
        // navigation property
        public int recipeId { get; set; }

        
        public Recipe Recipe { get; set; } = null!;



    }
}
