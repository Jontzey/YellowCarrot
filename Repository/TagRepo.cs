using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Data;
using YellowCarrot.Model;

namespace YellowCarrot.Repository
{
    internal class TagRepo
    {
        private readonly CarrotContext _context;
        public TagRepo(CarrotContext context)
        {
            this._context = context;
        }
        //public List<Tags> GetTags()
        //{
        //    return _context.Tags.ToList();
        //}
        //public List<Tags> Gettags(Recipe recipeId)
        //{
        //    Tags tags = new Tags();
        //    return _context.recipes.Where(x => x.Tags == tags.TagName).ToList();

        //}
        //public void AddTag(Tags theTag)
        //{
        //    _context.Tags.Add(theTag);
        //}
        //public void RemoveTag(Tags deleteTag)
        //{
        //    _context.Remove(deleteTag);
        //}
        //public void updateTag(Tags UpdateTag)
        //{
        //    _context.Tags.Update(UpdateTag);
        //}
    }
}
