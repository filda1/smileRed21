using smileRed25.Domain;

namespace smileRed25.Models
{
    public class UserView
    {

        public int UserId { get; set; }

        public int TypeofUserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Passwords { get; set; }

        public string Password { get; set; }

        public byte[] ImageArray { get; set; }

        public string Telephone { get; set; }

        public string Address { get; set; }

        public string Location { get; set; }

        public int Code { get; set; }

        public int Door { get; set; }

        public string ImagePath { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return "medium.png";
                }

                return string.Format(

                    "http://aurora.somee.com/{0}",
                    ImagePath.Substring(1));
            }

        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
            set
            {

            }
        }

        public bool Active { get; set; }
    }
}