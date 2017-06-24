using System;


namespace MB.Services.Models
{

    public partial class WUserBase
    {
        public string access_token { get; set; }
        public string openid { get; set; }
        public string code { get; set; }

        //       { "access_token":"ACCESS_TOKEN",    
        //"expires_in":7200,    
        //"refresh_token":"REFRESH_TOKEN",    
        //"openid":"OPENID",    
        //"scope":"SCOPE" } 
    }
}
