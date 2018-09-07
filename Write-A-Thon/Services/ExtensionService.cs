using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppExtensions;

namespace Write_A_Thon.Services
{
    public class ExtensionService
    {
        public string ExtensionContractName { get; set; }
        AppExtensionCatalog _catalog { get; set; }

        public ExtensionService(string extensionContractName)
        {
            // catalog & contract
            ExtensionContractName = extensionContractName;
            _catalog = AppExtensionCatalog.Open(ExtensionContractName);
            _catalog.PackageInstalled += _catalog_PackageInstalled;
            _catalog.PackageUninstalling += _catalog_PackageUninstalling;
        }

        private void _catalog_PackageUninstalling(AppExtensionCatalog sender, AppExtensionPackageUninstallingEventArgs args)
        {
            
        }

        private void _catalog_PackageInstalled(AppExtensionCatalog sender, AppExtensionPackageInstalledEventArgs args)
        {
            
        }

        public async Task<IReadOnlyList<AppExtension>> GetInstalledExtensions()
        {
            return await _catalog.FindAllAsync();
            
        }



    }
}
