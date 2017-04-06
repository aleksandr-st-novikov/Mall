using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public class SettingDocEFContext : ApplicationEFContext
    {
        public async Task PopulateFiledsAsync(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                string field = "f" + i.ToString().PadLeft(3, '0');
                SettingDoc entry = await context.SettingDoc.FirstOrDefaultAsync(s => s.DocField == field);
                if (entry == null)
                {
                    context.SettingDoc.Add(new SettingDoc()
                    {
                        DocField = field
                    });
                }
            }
        }

    }
}
