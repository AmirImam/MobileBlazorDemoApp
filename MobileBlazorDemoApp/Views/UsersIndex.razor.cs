using Entities;
using MobileBlazorDemoApp.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBlazorDemoApp.Views
{
    public partial class UsersIndex : ParentComponent
    {
        List<User> usersList = new List<User>();
        Random random = new Random();
        User user = new User();
        string RandomName(bool isMail)
        {
            string firstName = isMail == true ? MaleNames[random.Next(0, MaleNames.Length - 1)] : FemaleNames[random.Next(0, MaleNames.Length - 1)];
            string name = $"{firstName} {MaleNames[random.Next(0, MaleNames.Length - 1)]}";
            return name;
        }
        string[] FemaleNames
            => new string[] { "Rania", "Doaa", "Mona", "Noha", "Sara", "Omayma", "Israa" };
        string[] MaleNames
            => new string[] { "Amir", "Mohamed", "Ahmad", "Ali", "Ibrahim", "Sayed", "Ayman" };
        void InitUsers()
        {
            for (int i = 0; i < 30; i++)
            {
                Ganders gander = random.Next(0, 2) == 0 ? Ganders.Male : Ganders.Female;
                bool isMail = gander == Ganders.Male;

                usersList.Add(new User()
                {
                    Id = Guid.NewGuid(),
                    Name = RandomName(isMail),
                    Gander = gander,
                    ImageUrl = GetImageUrl(isMail)
                });
            }
            StateHasChanged();
        }
        private string GetImageUrl(bool isMail)
        {
            string ganderName = isMail ? "men" : "women";
            string url = $"https://randomuser.me/api/portraits/{ganderName}/{random.Next(1, 80)}.jpg";
            return url;
        }
        protected override void OnInitialized()
        {
            InitUsers();
        }

        private void SwitchGander()
        {
            if (user.Gander == Ganders.Male)
            {
                user.Gander = Ganders.Female;
            }
            else
            {
                user.Gander = Ganders.Male;
            }
            StateHasChanged();
        }

        private void AddUser()
        {
            var _user = new User()
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Gander = user.Gander,
                ImageUrl = GetImageUrl(user.Gander == Ganders.Male)
            };
            usersList.Add(_user);
            user = new User();
            StateHasChanged();
        }
    }
}
