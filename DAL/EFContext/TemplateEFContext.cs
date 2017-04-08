using DAL.Entities;
using DAL.ViewModel;
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
        public async Task<int> SaveTemplateAsync(Template template)
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

        public async Task DeleteTemplateAsync(int templateId)
        {
            Template entry = await context.Template.FindAsync(templateId);
            if (entry != null)
            {
                context.Template.Remove(entry);
            }
            List<TemplateTable> toDelete = await context.TemplateTable.Where(t => t.TemplateId == templateId).ToListAsync();
            if (toDelete.Count() > 0)
            {
                context.TemplateTable.RemoveRange(toDelete);
            }
        }

        public async Task<List<TemplateForDocumentView>> GetTemplateForDocumentByNameAsync(string name)
        {
            List<TemplateForDocumentView> tml = await (from t in context.Template
                                                       join tt in context.TemplateTable on t.Id equals tt.TemplateId
                                                       join sd in context.SettingDoc on tt.SettingDocId equals sd.Id
                                                       where t.Name == name && sd.IsActive == true
                                                       select new TemplateForDocumentView()
                                                       {
                                                           FileldIn = tt.FieldIn,
                                                           FieldOutName = sd.Descr,
                                                           FieldOut = sd.DocField,
                                                           Order = tt.Order,
                                                           Prefix = tt.Prefix,
                                                           Postfix = tt.Postfix,
                                                           Translate = tt.Translate
                                                       }).ToListAsync();

            return tml;
        }

        public async Task<List<string>> GetListTemplateNameAsync()
        {
            return await context.Template.OrderBy(t => t.Name).Select(t => t.Name).ToListAsync();
        }

        //public DbSet<TemplateTable> TemplateTable
        //{
        //    get { return context.TemplateTable; }
        //}

    }
}
