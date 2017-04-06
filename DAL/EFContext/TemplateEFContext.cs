using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public class TemplateEFContext : ApplicationEFContext
    {
        public async Task<int> AddTemplateAsync(Template template)
        {
            Template entry = await context.Template.FirstOrDefaultAsync(t => t.Id == template.Id);
            if (entry != null)
            {
                entry.Name = template.Name;
                entry.Commentary = template.Commentary;
            }
            else
            {
                context.Template.Add(template);
            }
            await context.SaveChangesAsync();

            return template.Id;
        }

        //public DbSet<TemplateTable> TemplateTable
        //{
        //    get { return context.TemplateTable; }
        //}

    }
}
