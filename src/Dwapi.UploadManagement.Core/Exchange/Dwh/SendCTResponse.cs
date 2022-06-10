﻿using System.Collections.Generic;
using System.Linq;

 namespace Dwapi.UploadManagement.Core.Exchange.Dwh
 {
     public class SendCTResponse
     {
         public string JobId { get; set; }
         public List<string> BatchKey { get; set; }

         public bool IsValid()
         {
             return BatchKey.Any();
         }

         public override string ToString()
         {
             return $"{BatchKey}";
         }
     }
 }
