using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeDocsWS.Models
{
    public class ImageDetails:TableEntity
    {
        public ImageDetails()
        {
            
        }

        public ImageDetails(Guid guid)
        {
            this.PartitionKey = "image";
            this.RowKey = guid.ToString();

            //we could also store further information about who uploaded it - copyright etc.
            
        }

        
    }
}