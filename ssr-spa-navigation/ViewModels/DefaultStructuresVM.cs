using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssr_spa_navigation.ViewModels
{
    public class DefaultStructureVM
    {
        public HeaderVM Header { get; set; }
        public FooterVM Footer { get; set; }
        public  SidebarVM Sidebar { get; set; }

        public DefaultStructureVM()
        {
            Header = new HeaderVM();
            Footer = new FooterVM();
            Sidebar = new SidebarVM();
        }
    }

    public class NoSidebarStructureVM
    {
        public HeaderVM Header { get; set; }
        public FooterVM Footer { get; set; }

        public NoSidebarStructureVM()
        {
            Header = new HeaderVM();
            Footer = new FooterVM();
        }
    }
}
