using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public class BrandEFContext : ApplicationEFContext
    {
        public async Task AddBrandAsync(Brand brand)
        {
            Brand entry = await context.Brand.FirstOrDefaultAsync(t => t.Id == brand.Id);
            if (entry != null)
            {
                entry.Name = brand.Name;
            }
            else
            {
                context.Brand.Add(brand);
            }
        }

    }
}
