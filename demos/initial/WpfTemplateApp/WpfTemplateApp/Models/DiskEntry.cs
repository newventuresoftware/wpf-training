using System.Collections.Generic;

namespace WpfTemplateApp.Models
{
    public abstract class DiskEntry
    {
        public static IList<DiskEntry> Folders { get; private set; }
        static DiskEntry()
        {
            Folders = new List<DiskEntry>()
        {
            new Folder()
            {
                Name = "Folder 1",
                Children = new List<DiskEntry>()
                {
                    new File() { Name = "picture 1.jpg" },
                    new File() { Name = "picture 2.jpg" },
                }
            },
            new Folder()
            {
                Name = "Folder 2",
                Children = new List<DiskEntry>()
                {
                    new Folder() { Name = "Empty" },
                    new File() { Name = "picture 3.jpg" },
                    new File() { Name = "picture 4.jpg" },
                }
            },
            new File() { Name = "picture.jpg" },
        };
        }

        public string Name { get; set; }
        public IList<DiskEntry> Children { get; set; }
    }

    public class Folder : DiskEntry
    {
        public Folder()
        {
            this.Children = new List<DiskEntry>();
        }
    }

    public class File : DiskEntry
    {
    }
}
