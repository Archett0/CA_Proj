using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CA_Proj.Models
{
    public class User
    {
        public User(int id, string username, string password, string nickname)
		{
			Id = id;
			Username = username;
			Password = password;
			Nickname = nickname;
		}
        public User() { }

		public User(bool Form_Type)
        {
			Id = 0;
        }

		[Key]
		[Column("user_id")]
		public int Id { get; set; }
		[Column("username")]
		public string Username { get; set; }
		[Column("password")]
		public string Password { get; set; }
		[Column("nickname")]
		public string Nickname { get; set; }
    }
}
