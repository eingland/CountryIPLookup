using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CountryIPLookup.Pages
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public IPModel IP { get; set; }

        private IHostingEnvironment _hostingEnvironment;

        private string projectRootFolder;

        public IndexModel(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            projectRootFolder = env.ContentRootPath;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await GetCountryCode(IP.Address);

            return Page();
        }

        public async Task GetCountryCode(string address)
        {
            await Task.Run(() => {
                var uintaddress = ConvertIPToUint(address);
                var lines = System.IO.File.ReadAllLines(Path.Combine(projectRootFolder, "wwwroot/assets/dbip-country-2018-07.csv")).Select(a => a.Split(';'));
                var csv = from line in lines
                          select (line[0].Replace("\"", "").Split(',')).ToArray();
                var code = from range in csv
                           where ConvertIPToUint(range[0]) <= uintaddress && ConvertIPToUint(range[1]) >= uintaddress
                           select (range[2]);
                IP.CountryCode = code.First().Replace("\"", "");
            });
        }

        static public uint ConvertIPToUint(string ipAddress)
        {
            System.Net.IPAddress iPAddress = System.Net.IPAddress.Parse(ipAddress);
            byte[] byteIP = iPAddress.GetAddressBytes();
            uint ipInUint = (uint)byteIP[0] * (256 * 256 * 256);
            ipInUint += (uint) byteIP[1] * (256 * 256);
            ipInUint += (uint) byteIP[2] * 256;
            ipInUint += (uint) byteIP[3];
            return ipInUint;
        }
}

    public class IPModel
    {
        [RegularExpression(@"^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"), Required]
        public string Address { get; set; }
        public string CountryCode { get; set; }
    }
}
