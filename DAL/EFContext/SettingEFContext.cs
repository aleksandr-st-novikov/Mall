using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public class SettingEFContext : ApplicationEFContext
    {
        public string SetVersionDB(string version)
        {
            Setting entry = context.Setting.FirstOrDefault(s => s.Name == Data.settingVersionDBName);
            if(entry == null)
            {
                context.Setting.Add(new Setting { Name = Data.settingVersionDBName, Value = "1.0.0" });
            }
            else
            {
                entry.Value = version;
            }
            context.SaveChanges();
            return context.Setting.FirstOrDefault(s => s.Name == Data.settingVersionDBName).Value;
        }

        public async Task<string> GetVersionDBAsync()
        {
            Setting setting = await context.Setting.FirstOrDefaultAsync(s => s.Name == Data.settingVersionDBName);
            return setting.Value;
        }

    }
}
