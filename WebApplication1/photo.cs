namespace AnnouncmentSite
{
    public class photo
    {

        public FileStream filep { get; set; }
        public int id { get; set; }
        public photo(FileStream file,int id)
        {
            this.filep = file;
            this.id = id;
        }

    }
}
