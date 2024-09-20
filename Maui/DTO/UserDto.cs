using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.DTO
{
    public class UserDto : TokenDto
    {
        public string Username {  get; set; }
        public string Email {  get; set; }
        public string Phone {  get; set; }
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
    }
}
