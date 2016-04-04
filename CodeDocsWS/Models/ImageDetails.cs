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
        public string BlobUrl { get; set; }

        public string ThumbNailUrl { get; set; }
        public ImageDetails()
        {
            
        }

        public ImageDetails(Guid guid, string blobUrl)
        {
            this.PartitionKey = "image";
            this.RowKey = guid.ToString();
            this.BlobUrl = blobUrl;                       
        }

        pu

        
    }
}