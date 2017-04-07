using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public class DocumentEFContext : ApplicationEFContext
    {
        public async Task<int> SaveDocumentAsync(Document document)
        {
            Document entry = await context.Document.FirstOrDefaultAsync(t => t.Id == document.Id);
            if (entry != null)
            {
                entry.DateDocument = document.DateDocument;
            }
            else
            {
                context.Document.Add(document);
            }
            await context.SaveChangesAsync();

            return document.Id;
        }

        public async Task DeleteDocumentAsync(int documentId)
        {
            Document entry = await context.Document.FindAsync(documentId);
            if (entry != null)
            {
                context.Document.Remove(entry);
            }
            List<DocumentTable> toDelete = await context.DocumentTable.Where(t => t.DocumentId == documentId).ToListAsync();
            if (toDelete.Count() > 0)
            {
                context.DocumentTable.RemoveRange(toDelete);
            }
        }

    }
}
