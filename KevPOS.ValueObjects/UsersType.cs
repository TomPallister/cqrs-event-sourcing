namespace KevPOS.ValueObjects
{
    public class UsersType
    {
        public UsersType(UserType userType)
        {
            UserType = userType;
        }

        private UserType UserType { get; set; }

        public UserType ToUserType()
        {
            return UserType;
        }
    }
}